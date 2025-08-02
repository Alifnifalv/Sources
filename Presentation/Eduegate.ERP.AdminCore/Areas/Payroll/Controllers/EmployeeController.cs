using Microsoft.AspNetCore.Mvc;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Payroll;
using Eduegate.Web.Library.HR.Payroll;
using Eduegate.Services.Client.Factory;
using Eduegate.Infrastructure.Enums;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.HR.Payroll;
using System.Globalization;
using Eduegate.Services.Contracts.CommonDTO;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Services.Contracts.Contents;
using Eduegate.Services.Contracts.Framework;
using Hangfire;
using Eduegate.Domain.Entity.HR.Payroll;

namespace Eduegate.ERP.Admin.Areas.Payroll.Controllers
{
    [Area("Payroll")]
    public class EmployeeController : BaseSearchController
    {
        // GET: Payroll/Employee
        public ActionResult Index()
        {
            return View();
        }
        public EmployeeController(IBackgroundJobClient backgroundJobs)
        {
            _backgroundJobs = backgroundJobs ?? throw new ArgumentNullException(nameof(backgroundJobs));
        }
        public async Task<IActionResult> List()
        {
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.Employee);

            return View(new SearchListViewModel
            {
                ViewName = Eduegate.Infrastructure.Enums.SearchView.Employee,
                ControllerName = "Payroll/Employee",
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                //InfoBar = ""
                HasFilters = metadata.HasFilters,
                FilterColumns = metadata.QuickFilterColumns,
                UserValues = metadata.UserValues
            });
        }

        [HttpGet]
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {
            runtimeFilter = " CompanyId = " + CallContext.CompanyID.ToString();
            return base.SearchData(Eduegate.Infrastructure.Enums.SearchView.Employee, currentPage, orderBy, runtimeFilter);
        }

        [HttpGet]
        public Task<IActionResult> SearchSummaryData()
        {
            return base.SearchSummaryData(Eduegate.Infrastructure.Enums.SearchView.EmployeeSummary);
        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View();
        }

        [HttpGet]
        public ActionResult Create(long ID = 0)
        {
            var viewModel = new CRUDViewModel();
            viewModel.Name = "Employee";
            viewModel.ListActionName = "Employee";
            viewModel.ViewModel = new EmployeeViewModel();
            viewModel.IID = ID;
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "BranchGroup", Url = "Mutual/GetLookUpData?lookType=BranchGroup" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Warehouse", Url = "Mutual/GetLookUpData?lookType=Warehouse" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "EmployeeRole", Url = "Mutual/GetLookUpData?lookType=EmployeeRole" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Designation", Url = "Mutual/GetLookUpData?lookType=Designation" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Branch", Url = "Mutual/GetLookUpData?lookType=Branch" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "JobType", Url = "Mutual/GetLookUpData?lookType=JobType" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Gender", Url = "Mutual/GetLookUpData?lookType=Gender" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Department", Url = "Mutual/GetLookUpData?lookType=Department" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "MaritalStatus", Url = "Mutual/GetLookUpData?lookType=MaritalStatus" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "LoginUserStatus", Url = "Mutual/GetLookUpData?lookType=LoginUserStatus" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Title", Url = "Mutual/GetLookUpData?lookType=Title" });
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Employee", Url = "Payroll/Employee/GetEmployees" });

            TempData["viewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(viewModel);
            return RedirectToAction("Create", "CRUD", new { area = "" });
        }

        [HttpGet]
        public ActionResult Edit(long ID = 0)
        {
            return RedirectToAction("Create", new { ID = ID });
        }

        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            var vm = EmployeeViewModel.FromDTO(ClientFactory.EmployeeServiceClient(CallContext).GetEmployee(ID));
            return Json(vm);
        }
        [HttpGet]
        public ActionResult GetTicketEntitilement(long ticketEntitilementID = 0)
        {
            var data = ClientFactory.FrameworkServiceClient(CallContext).GetScreenData((long)Screens.EmployeeTicketEntitilement, ticketEntitilementID);
            return Json(data);
        }
        [HttpGet]
        public ActionResult GetComponentVariable(int SalaryComponentID = 0)
        {
            var data = ClientFactory.SettingServiceClient(CallContext)
                .GetSettingByGroup("airfare");
            var salaryComponentVariableList = new List<EmployeeSalaryStructureVariableMapViewModel>();
            foreach (var dat in data)
            {
                var salaryComponentVariableMap = new EmployeeSalaryStructureVariableMapViewModel()
                {
                    VariableValue = dat.SettingValue,
                    VariableKey = dat.Description,
                };

                salaryComponentVariableList.Add(salaryComponentVariableMap);
            }
            return Json(salaryComponentVariableList);

        }

        [HttpPost]
        public JsonResult Save([FromBody] EmployeeViewModel vm)
        {
            try
            {
                if (vm.AirfareTicketDetail != null && vm.AirfareTicketDetail.ISTicketEligible == true)
                {
                    if ( string.IsNullOrEmpty(vm.AirfareTicketDetail.TicketEligibleFromDateString))
                    {
                        throw new Exception("Please select Date from when the ticket is eligible, as you have marked the ticket as eligible!");
                    }
                    if (vm.AirfareTicketDetail.EmployeeCountryAirport==null || string.IsNullOrEmpty(vm.AirfareTicketDetail.EmployeeCountryAirport.Key))
                    {
                        throw new Exception("Please Select Employee Country Airport ,as you have marked the ticket as eligible!");
                    }
                    if (vm.AirfareTicketDetail.EmployeeNearestAirport== null || string.IsNullOrEmpty(vm.AirfareTicketDetail.EmployeeNearestAirport.Key))
                    {
                        throw new Exception("Please Select Employee Nearest Airport ,as you have marked the ticket as eligible!");
                    }
                    if (vm.AirfareTicketDetail.TicketEntitilement== null || string.IsNullOrEmpty(vm.AirfareTicketDetail.TicketEntitilement.Key))
                    {
                        throw new Exception("Please Select Ticket Entitilement ,as you have marked the ticket as eligible!");
                    };
                    if (vm.AirfareTicketDetail.FlightClass == null || string.IsNullOrEmpty(vm.AirfareTicketDetail.FlightClass.Key))
                    {
                        throw new Exception("Please Select Flight Class,as you have marked the ticket as eligible!");
                    };
                }
                var updatedVM = EmployeeViewModel.FromDTO(ClientFactory.EmployeeServiceClient(CallContext).SaveEmployee(EmployeeViewModel.ToDTO(vm, base.CallContext)));
                return Json(updatedVM);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(ex.Message);
            }
        }

        public JsonResult GetEmployees()
        {
            return Json(EmployeeViewModel.ToKeyValueViewModel(ClientFactory.EmployeeServiceClient(CallContext).GetEmployees()));
        }

        public JsonResult SearchEmployee(string searchText)
        {
            return Json(EmployeeViewModel.ToKeyValueViewModel(ClientFactory.EmployeeServiceClient(CallContext).SearchEmployee(searchText, Convert.ToInt32(new Domain.Setting.SettingBL().GetSettingValue<string>("Select2DataSize")))));
        }

        public JsonResult GetEmployeesByRoleID(int roleID)
        {
            return Json(EmployeeViewModel.ToKeyValueViewModel(ClientFactory.EmployeeServiceClient(CallContext).GetEmployeesByRoles(roleID)));
        }

        //public JsonResult GetEmployeeDepartments(int departmentID)
        //{
        //    return Json(EmployeeViewModel.ToKeyValueViewModel(ClientFactory.EmployeeServiceClient(CallContext).GetEmployeeDepartments(departmentID)));
        //}

        public JsonResult GetEmployeeComponents(int SalaryStructureID)
        {
            var data = ClientFactory.FrameworkServiceClient(CallContext).GetScreenData((int)Screens.SalaryStructure, SalaryStructureID);
            var salary = new SalaryStructureViewModel();
            salary = salary.ToVM(salary.ToDTO(data)) as SalaryStructureViewModel;
            var employeecomponent = new List<EmployeeSalaryStructureComponentViewModel>();
            foreach (var dat in salary.SalaryComponents)
            {
                List<EmployeeSalaryStructureVariableMapViewModel> salaryComponentVariableMapDTO = new List<EmployeeSalaryStructureVariableMapViewModel>();
                foreach (var componentVariableMap in dat.SalaryComponentVariableMap)
                {
                    if (!string.IsNullOrEmpty(componentVariableMap.VariableKey) && !string.IsNullOrEmpty(componentVariableMap.VariableValue))
                    {
                        var componentVariableMapDTO = new EmployeeSalaryStructureVariableMapViewModel()
                        {
                            VariableValue = componentVariableMap.VariableValue,
                            VariableKey = componentVariableMap.VariableKey,
                        };

                        salaryComponentVariableMapDTO.Add(componentVariableMapDTO);
                    }
                }
                var salaryType = new EmployeeSalaryStructureComponentViewModel()
                {
                    //Amount = dat.MinAmount,
                    Earnings = dat.MinAmount,
                    SalaryComponent = dat.SalaryComponent,
                    EmployeeSalaryStructureVariableMap = salaryComponentVariableMapDTO
                };

                employeecomponent.Add(salaryType);
            }
            return Json(employeecomponent);
        }

        [HttpGet]
        public JsonResult GetCollectTimeSheetsData(long employeeID, string fromDate, string toDate)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var timeFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("TimeFormatWithoutSecond");
            EmployeeTimeSheetsWeeklyDTO data = ClientFactory.SchoolServiceClient(CallContext).GetCollectTimeSheetsData(employeeID, DateTime.ParseExact(fromDate, dateFormat, CultureInfo.InvariantCulture), DateTime.ParseExact(toDate, dateFormat, CultureInfo.InvariantCulture));
            List<EmployeeTimeSheetsWeeklyViewModel> feeTypes = new List<EmployeeTimeSheetsWeeklyViewModel>();
            List<EmployeeTimeSheetsTimeViewModel> listvm = new List<EmployeeTimeSheetsTimeViewModel>();

            var sheetdto = new List<EmployeeTimeSheetsTimeDTO>();
            if (data != null)
            {
                foreach (var split in data.TimeSheet)
                {
                    var timesheetcollection = new EmployeeTimeSheetsTimeViewModel()
                    {
                        EmployeeTimeSheetIID = split.EmployeeTimeSheetIID,
                        OTHours = split.OTHours,
                        NormalHours = split.NormalHours,
                        TimesheetDate = split.TimesheetDate,
                        TimesheetDateString = split.TimesheetDate.ToString(dateFormat, CultureInfo.InvariantCulture),
                        Task = new KeyValueViewModel()
                        {
                            Key = Convert.ToString(split.TaskID),
                            Value = split.Task.Value
                        },
                        TimesheetEntryStatusID = split.TimesheetEntryStatusID,
                        EntryStatus = split.TimesheetEntryStatusID.HasValue ? split.TimesheetEntryStatusID.ToString() : null,
                        TimesheetTimeType = split.TimesheetTimeTypeID.HasValue ? split.TimesheetTimeTypeID.ToString() : null,
                        TimesheetTimeTypeID = split.TimesheetTimeTypeID,
                        //TimesheetTimeType = split.TimesheetTimeType,
                        //EntryStatus = split.TimesheetEntryStatus.Value,
                        //FromTime = split.FromTime,
                        //ToTime = split.ToTime,
                        Remarks = split.Remarks,
                        FromTimeString = split.FromTime.HasValue ? DateTime.Parse(split.FromTime.Value.ToString()).ToString(timeFormat) : null,
                        ToTimeString = split.ToTime.HasValue ? DateTime.Parse(split.ToTime.Value.ToString()).ToString(timeFormat) : null,
                    };

                    listvm.Add(timesheetcollection);
                }
            }

            if (listvm == null)
            {
                return Json(new { IsError = true, Response = "There are some issues.Please try after sometime!" });
            }
            else
            {
                return Json(new { IsError = false, Response = listvm });
            }

        }

        //[HttpPost]
        //public ActionResult SaveSalaryDetail(SalarySlipViewModel salaryInfo)
        //{
        //    string salarydt = "0#Something went wrong!";
        //    List<KeyValueDTO> employeelist = new List<KeyValueDTO>();
        //    List<KeyValueDTO> departmentlist = new List<KeyValueDTO>();
        //    var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

        //    foreach (KeyValueViewModel vm in salaryInfo.Employee)
        //    {
        //        employeelist.Add(new KeyValueDTO { Key = vm.Key, Value = vm.Value }
        //            );
        //    }
        //    foreach (KeyValueViewModel vm in salaryInfo.Department)
        //    {
        //        departmentlist.Add(new KeyValueDTO { Key = vm.Key, Value = vm.Value }
        //            );
        //    }
        //    //salarydt = ClientFactory.EmployeeServiceClient(CallContext).SaveSalaryDetail(new Services.Contracts.HR.Payroll.SalarySlipDTO()
        //    //{
        //    //    Department = departmentlist,
        //    //    Employees=employeelist,
        //    //    IsRegenerate=salaryInfo.IsRegenerate,
        //    //    SlipDate = string.IsNullOrEmpty(salaryInfo.SlipDateString) ? (DateTime?)null : DateTime.ParseExact(salaryInfo.SlipDateString, dateFormat, CultureInfo.InvariantCulture),
        //    //});
        //    string[] resp = salarydt.Split('#');
        //    dynamic response = new { IsFailed = (resp[0] == "0"), Message = resp[1] };
        //    return Json(response);
        //}

        public JsonResult GetEmployeeByBranch(int? branchID)
        {
            var listEmployees = ClientFactory.EmployeeServiceClient(CallContext).GetEmployeeByBranch(branchID);
            return Json(listEmployees);
        }

        [HttpPost]
        public ActionResult GenerateSalarySlip(SalarySlipViewModel salaryInfo)
        {
            try
            {
                dynamic response = new { IsFailed = 1, Message = "Something went wrong!" };
                List<KeyValueDTO> employeelist = new List<KeyValueDTO>();
                List<KeyValueDTO> departmentlist = new List<KeyValueDTO>();
                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

                foreach (KeyValueViewModel vm in salaryInfo.Employee)
                {
                    employeelist.Add(new KeyValueDTO { Key = vm.Key, Value = vm.Value }
                        );
                }
                foreach (KeyValueViewModel vm in salaryInfo.Department)
                {
                    departmentlist.Add(new KeyValueDTO { Key = vm.Key, Value = vm.Value }
                        );
                }
                if (departmentlist.Count() == 0 && employeelist.Count() == 0)
                {
                    response = new { IsFailed = 1, Message = "Please Select Department or employees!" };
                    return Json(response);
                }
                OperationResultWithIDsDTO salarydt = ClientFactory.EmployeeServiceClient(CallContext).GenerateSalarySlip(new Services.Contracts.HR.Payroll.SalarySlipDTO()
                {
                    Department = departmentlist,
                    Employees = employeelist,
                    IsRegenerate = salaryInfo.IsRegenerate,
                    IsGenerateLeaveOrVacationsalary = salaryInfo.IsGenerateLeaveOrVacationsalary,
                    SlipDate = string.IsNullOrEmpty(salaryInfo.SlipDateString) ? (DateTime?)null : DateTime.ParseExact(salaryInfo.SlipDateString, dateFormat, CultureInfo.InvariantCulture)
                });

                if (salarydt != null && salarydt.SalarySlipIDList != null && salarydt.SalarySlipIDList.Count() > 0)
                {
                    _backgroundJobs.Enqueue(() => GenerateAndSaveSalarySlips(salarydt.SalarySlipIDList, CallContext));
                    response = new { IsFailed = false, Message = "Salaryslip is being generated in the background. This may take a few moments." };
                }
                else
                {
                    response = new { IsFailed = true, Message = salarydt.Message };
                }

                return Json(response);
            }
            catch (Exception ex)
            {

                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                          ? ex.InnerException?.Message : ex.Message;
                return Json(new { IsError = true, Response = errorMessage });

            }
        }

        public void GenerateAndSaveSalarySlips(List<SalarySlipDTO> salarySlipIDList, Eduegate.Framework.CallContext _context)
        {
            try
            {
                if (CallContext.LoginID == null)
                {
                    CallContext = _context;
                }

                // Return SalarySlipPDF as Byte file
                var contentData = ClientFactory.ReportGenerationServiceClient(CallContext).GenerateSalarySlipContentFile(salarySlipIDList);
                // Save SalarySlipPDF in Content Table
                var savedContentData = ClientFactory.ContentServicesClient(CallContext).SaveBulkContentFiles(contentData);
                // Update Salary slip with content ID
                var resultData = ClientFactory.EmployeeServiceClient(CallContext).UpdateSalarySlipPDF(savedContentData);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                               ? ex.InnerException?.Message : ex.Message;

                Eduegate.Logger.LogHelper<string>.Fatal($"The Salary slip generation process has failed. Error message: {errorMessage}", ex);

            }
        }


        [HttpPost]
        public ActionResult ModifySalarySlip(SalarySlipDTO salaryInfo)
        {
            dynamic response = new { IsFailed = 1, Message = "Something went wrong!" };
            var dto = new SalarySlipDTO();
            dto.SalarySlipIID = salaryInfo.SalarySlipIID;
            dto.EmployeeID = salaryInfo.EmployeeID;
            dto.EmployeeCode = salaryInfo.EmployeeCode;
            dto.BranchID = salaryInfo.BranchID;
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            dto.SlipDate = string.IsNullOrEmpty(salaryInfo.SlipDateString) ? (DateTime?)null : DateTime.ParseExact(salaryInfo.SlipDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.SalaryComponent = new List<SalarySlipComponentDTO>();
            foreach (var e in salaryInfo.SalaryComponent)
            {
                dto.SalaryComponent.Add(new SalarySlipComponentDTO()
                {
                    SalarySlipIID = e.SalarySlipIID,
                    SalaryComponentID = e.SalaryComponentID,
                    Amount = e.Earnings.HasValue ? e.Earnings : e.Deduction.HasValue ? e.Deduction * -1 : (decimal?)null,
                    Description = e.Description,
                    NoOfDays = e.NoOfDays,
                    NoOfHours = e.NoOfHours,
                    ReportContentID = e.ReportContentID,

                });
            }
            OperationResultWithIDsDTO salarydt = ClientFactory.EmployeeServiceClient(CallContext).ModifySalarySlip(dto);
            if (salarydt != null && salarydt.SalarySlipIDList != null && salarydt.SalarySlipIDList.Count() > 0)
            {
                //Return SalarySlipPDF as Byte file
                var contentData = ClientFactory.ReportGenerationServiceClient(CallContext).GenerateSalarySlipContentFile(salarydt.SalarySlipIDList);
                //Save SalarySlipPDF in Content Table 
                var savedContentata = ClientFactory.ContentServicesClient(CallContext).SaveBulkContentFiles(contentData);
                // Update Salary slip with content ID
                var resultData = ClientFactory.EmployeeServiceClient(CallContext).UpdateSalarySlipPDF(savedContentata);
                response = new { IsFailed = 0, Message = resultData.Message };
            }
            else
            {
                response = new { IsFailed = 1, Message = salarydt.Message };
            }

            return Json(response);
        }

        public ActionResult SalarySlipPublish()
        {
            return View();
        }

        public JsonResult GetSalarySlipEmployeeData(int departmentID, int month, int year, long employeeID)
        {
            var EmployeeSalarySlips = ClientFactory.EmployeeServiceClient(CallContext).GetSalarySlipEmployeeData(departmentID, month, year, employeeID);
            var slipList = new List<SalarySlipEmployeeViewModel>();

            foreach (var data in EmployeeSalarySlips)
            {
                slipList.Add(new SalarySlipEmployeeViewModel()
                {
                    SalarySlipIID = data.SalarySlipIID,
                    EmployeeID = data.EmployeeID,
                    EmployeeName = data.EmployeeName,
                    SlipDate = data.SlipDate,
                    IsSelected = data.IsSelected,
                    EmailAddress = data.EmailAddress,
                    WorkingDays = data.WorkingDays,
                    NormalHrs = data.NormalHours,
                    OTHrs = data.OTHours,
                    LOPDays = data.LOPDays,
                    IsVerified = data.IsVerified,
                    PublishStatusID = data.SlipPublishStatusID,
                    PublishStatus = data.SlipPublishStatusID.HasValue ? new KeyValueViewModel() { Key = data.SlipPublishStatusID.ToString(), Value = data.SlipPublishStatusName } : new KeyValueViewModel(),
                    ReportContentID = data.ReportContentID,
                    EarningAmount = data.EarningAmount,
                    DeductingAmount = data.DeductingAmount,
                    AmountToPay = data.AmountToPay,
                    BranchID = data.BranchID,
                });
            }

            return Json(slipList);
        }

        [HttpPost]
        public ActionResult UpdateSalarySlipIsVerifiedData(long salarySlipID, bool isVerifiedStatus)
        {
            var message = new OperationResultDTO();

            message = ClientFactory.EmployeeServiceClient(CallContext).UpdateSalarySlipIsVerifiedData(salarySlipID, isVerifiedStatus);

            if (message.operationResult == OperationResult.Error)
            {
                return Json(new { IsError = true, Response = message.Message });
            }
            else
            {
                return Json(new { IsError = false, Response = message.Message });
            }
        }

        [HttpPost]
        public ActionResult PublishSalarySlips([FromBody] List<SalarySlipEmployeeViewModel> salarySlips)
        {
            var message = new OperationResultDTO();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var salarySlipDTOList = new List<SalarySlipDTO>();

            foreach (var slip in salarySlips)
            {
                salarySlipDTOList.Add(new SalarySlipDTO()
                {
                    SalarySlipIID = slip.SalarySlipIID,
                    EmployeeID = slip.EmployeeID,
                    SlipDate = string.IsNullOrEmpty(slip.SlipDate) ? (DateTime?)null : DateTime.ParseExact(slip.SlipDate, dateFormat, CultureInfo.InvariantCulture),
                    SalarySlipStatusID = slip.PublishStatus != null && !string.IsNullOrEmpty(slip.PublishStatus.Key) ? byte.Parse(slip.PublishStatus.Key) : (byte?)null,
                });
            }

            message = ClientFactory.EmployeeServiceClient(CallContext).PublishSalarySlips(salarySlipDTOList);

            if (message.operationResult == OperationResult.Error)
            {
                return Json(new { IsError = true, Response = message.Message });
            }
            else
            {
                return Json(new { IsError = false, Response = message.Message });
            }
        }

        [HttpPost]
        public ActionResult EmailSalarySlips([FromBody] List<SalarySlipEmployeeViewModel> salarySlips)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            try
            {
                var contentFileDTO = new List<ContentFileDTO>();
                if (salarySlips.Any())
                {
                    salarySlips.All(x =>
                    {
                        contentFileDTO.Add(new ContentFileDTO()
                        {
                            ContentFileIID = x.ReportContentID.Value,
                            ReferenceID = x.SalarySlipIID
                        });
                        return true;
                    });
                }

                var contentata = ClientFactory.ContentServicesClient(CallContext).GetContentFileList(contentFileDTO);
                var salarySlipDTOList = new List<SalarySlipDTO>();

                foreach (var slip in salarySlips)
                {
                    salarySlipDTOList.Add(new SalarySlipDTO()
                    {
                        SalarySlipIID = slip.SalarySlipIID,
                        SlipDate = string.IsNullOrEmpty(slip.SlipDate) ? (DateTime?)null : DateTime.ParseExact(slip.SlipDate, dateFormat, CultureInfo.InvariantCulture),
                        ReportData = contentata.Where(x => x.ContentFileIID == slip.ReportContentID).Select(y => y.ContentData).FirstOrDefault(),
                        EmployeeWorkEmail = slip.EmailAddress,
                        BranchID = slip.BranchID,
                        ReportName = contentata.Where(x => x.ContentFileIID == slip.ReportContentID).Select(y => y.ContentFileName).FirstOrDefault(),
                        EmployeeCode = slip.EmployeeName.Substring(0, slip.EmployeeName.IndexOf('-'))
                    });
                }

                //send salaryslip to employees
                ClientFactory.ReportGenerationServiceClient(CallContext).MailSalaryslip(salarySlipDTOList);

                return Json(new { IsError = false, Response = "Mail sent successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { IsError = true, Response = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult ApplyAndUpdateSlipData([FromBody] SalarySlipEmployeeViewModel salarySlip)
        {
            var message = new OperationResultDTO();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            var salarySlipDTO = new SalarySlipDTO()
            {
                SalarySlipIID = salarySlip.SalarySlipIID,
                EmployeeID = salarySlip.EmployeeID,
                SlipDate = string.IsNullOrEmpty(salarySlip.SlipDate) ? (DateTime?)null : DateTime.ParseExact(salarySlip.SlipDate, dateFormat, CultureInfo.InvariantCulture),
                SalarySlipStatusID = salarySlip.PublishStatus != null && !string.IsNullOrEmpty(salarySlip.PublishStatus.Key) ? byte.Parse(salarySlip.PublishStatus.Key) : (byte?)null,
            };

            message = ClientFactory.EmployeeServiceClient(CallContext).UpdateSlipData(salarySlipDTO);

            if (message.operationResult == OperationResult.Error)
            {
                return Json(new { IsError = true, Response = message.Message });
            }
            else
            {
                return Json(new { IsError = false, Response = message.Message });
            }
        }

        public ActionResult EmployeeTimeSheetApproval()
        {
            var vm = new EmployeeTimeSheetApprovalViewModel();

            return View(vm);
        }

        [HttpGet]
        public JsonResult GetWorkingHourDetailsByEmployee(long employeeID)
        {
            var calendarEntries = ClientFactory.EmployeeServiceClient(CallContext).GetWorkingHourDetailsByEmployee(employeeID);

            if (calendarEntries == null || calendarEntries.Count == 0)
            {
                return Json(new { IsError = true, Response = "There are some issues.Please try after sometime!" });
            }
            else
            {
                return Json(new { IsError = false, Response = calendarEntries });
            }

        }

        [HttpGet]
        public JsonResult GetPendingTimeSheetsDatas(long employeeID, string dateFromString, string dateToString)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var timeFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("TimeFormatWithoutSecond");

            var timeSheetApprovals = new List<EmployeeTimeSheetApprovalViewModel>();

            var dateFrom = DateTime.ParseExact(dateFromString, dateFormat, CultureInfo.InvariantCulture);

            var dateTo = DateTime.ParseExact(dateToString, dateFormat, CultureInfo.InvariantCulture);

            var timeSheetDatas = ClientFactory.EmployeeServiceClient(CallContext).GetPendingTimeSheetsDatas(employeeID, dateFrom, dateTo);

            if (timeSheetDatas != null && timeSheetDatas.Count > 0)
            {
                foreach (var sheet in timeSheetDatas)
                {
                    var timeSheetTimeDetails = new List<EmployeeTimeSheetApprovalTimeViewModel>();
                    foreach (var timeDetail in sheet.TimeSheetDetails)
                    {
                        timeSheetTimeDetails.Add(new EmployeeTimeSheetApprovalTimeViewModel()
                        {
                            EmployeeTimeSheetIID = timeDetail.EmployeeTimeSheetIID,
                            FromTimeString = timeDetail.FromTime.HasValue ? DateTime.Parse(timeDetail.FromTime.Value.ToString()).ToString(timeFormat) : null,
                            ToTimeString = timeDetail.ToTime.HasValue ? DateTime.Parse(timeDetail.ToTime.Value.ToString()).ToString(timeFormat) : null,
                            NormalHours = timeDetail.SheetNormalHours.HasValue ? timeDetail.SheetNormalHours : 0,
                            OTHours = timeDetail.SheetOTHours.HasValue ? timeDetail.SheetOTHours : 0,
                            TimesheetTimeTypeID = timeDetail.TimesheetTimeTypeID,
                            TimesheetTimeTypeName = string.IsNullOrEmpty(timeDetail.TimesheetTimeTypeName) ? "NA" : timeDetail.TimesheetTimeTypeName
                        });
                    }

                    timeSheetApprovals.Add(new EmployeeTimeSheetApprovalViewModel()
                    {
                        IsSelected = false,
                        EmployeeTimeSheetApprovalIID = sheet.EmployeeTimeSheetApprovalIID,
                        EmployeeTimeSheetID = sheet.EmployeeTimeSheetID,
                        EmployeeID = sheet.EmployeeID,
                        Employee = sheet.EmployeeID.HasValue ? new KeyValueViewModel()
                        {
                            Key = sheet.EmployeeID.ToString(),
                            Value = sheet.EmployeeName
                        } : new KeyValueViewModel(),
                        TimesheetDateString = sheet.TimeSheetDate.HasValue ? sheet.TimeSheetDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        TotalNormalHours = sheet.TotalNormalHours,
                        TotalOverTimeHours = sheet.TotalOTHours,
                        TimeSheetTimeDetails = timeSheetTimeDetails,
                        ApprovedNormalHours = sheet.TotalNormalHours,
                        ApprovedOTHours = sheet.TotalOTHours,
                        TimesheetTimeType = sheet.TimesheetTimeTypeID.HasValue ? new KeyValueViewModel()
                        {
                            Key = sheet.TimesheetTimeTypeID.ToString(),
                            Value = sheet.TimesheetTimeTypeName,
                        } : new KeyValueViewModel(),
                    });
                }
            }

            return Json(timeSheetApprovals);
        }

        [HttpPost]
        public ActionResult SavePendingTimesheetApprovalData([FromBody] EmployeeTimeSheetApprovalViewModel timeSheetApproval)
        {
            var message = new OperationResultDTO();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            var timesheetDetails = new List<EmployeeTimeSheetApprovalTimeDTO>();

            foreach (var detail in timeSheetApproval.TimeSheetTimeDetails)
            {
                timesheetDetails.Add(new EmployeeTimeSheetApprovalTimeDTO()
                {
                    EmployeeTimeSheetIID = detail.EmployeeTimeSheetIID,
                    EmployeeID = timeSheetApproval.EmployeeID,
                    TimesheetTimeTypeID = timeSheetApproval.TimesheetTimeType != null && !string.IsNullOrEmpty(timeSheetApproval.TimesheetTimeType.Key) ? byte.Parse(timeSheetApproval.TimesheetTimeType.Key) : (byte?)null,
                    TimesheetEntryStatusID = timeSheetApproval.TimesheetApprovalStatus != null && !string.IsNullOrEmpty(timeSheetApproval.TimesheetApprovalStatus.Key) ? byte.Parse(timeSheetApproval.TimesheetApprovalStatus.Key) : (byte?)null,
                });
            }

            var timesheetApprovalDTO = new EmployeeTimeSheetApprovalDTO()
            {
                EmployeeTimeSheetApprovalIID = timeSheetApproval.EmployeeTimeSheetApprovalIID,
                EmployeeTimeSheetID = timeSheetApproval.EmployeeTimeSheetID,
                EmployeeID = timeSheetApproval.EmployeeID,
                NormalHours = timeSheetApproval.ApprovedNormalHours,
                OTHours = timeSheetApproval.ApprovedOTHours,
                TimeSheetDate = string.IsNullOrEmpty(timeSheetApproval.TimesheetDateString) ? (DateTime?)null : DateTime.ParseExact(timeSheetApproval.TimesheetDateString, dateFormat, CultureInfo.InvariantCulture),
                TimesheetDateFrom = string.IsNullOrEmpty(timeSheetApproval.DateFromString) ? DateTime.ParseExact(timeSheetApproval.TimesheetDateString, dateFormat, CultureInfo.InvariantCulture) : DateTime.ParseExact(timeSheetApproval.DateFromString, dateFormat, CultureInfo.InvariantCulture),
                TimesheetDateTo = string.IsNullOrEmpty(timeSheetApproval.DateToString) ? (DateTime?)null : DateTime.ParseExact(timeSheetApproval.DateToString, dateFormat, CultureInfo.InvariantCulture),
                TimesheetTimeTypeID = timeSheetApproval.TimesheetTimeType != null && !string.IsNullOrEmpty(timeSheetApproval.TimesheetTimeType.Key) ? byte.Parse(timeSheetApproval.TimesheetTimeType.Key) : (byte?)null,
                TimesheetApprovalStatusID = timeSheetApproval.TimesheetApprovalStatus != null && !string.IsNullOrEmpty(timeSheetApproval.TimesheetApprovalStatus.Key) ? byte.Parse(timeSheetApproval.TimesheetApprovalStatus.Key) : (byte?)null,
                Remarks = timeSheetApproval.Remarks,
                TimeSheetDetails = timesheetDetails
            };

            message = ClientFactory.EmployeeServiceClient(CallContext).SavePendingTimesheetApprovalData(timesheetApprovalDTO);

            if (message.operationResult == OperationResult.Error)
            {
                return Json(new { IsError = true, Response = message.Message });
            }
            else
            {
                return Json(new { IsError = false, Response = message.Message });
            }
        }

        [HttpPost]
        public ActionResult PublishPendingTimesheets([FromBody] List<EmployeeTimeSheetApprovalViewModel> timesheets)
        {
            var message = new OperationResultDTO();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            try
            {
                foreach (var sheet in timesheets)
                {
                    var timesheetDetails = new List<EmployeeTimeSheetApprovalTimeDTO>();

                    foreach (var detail in sheet.TimeSheetTimeDetails)
                    {
                        timesheetDetails.Add(new EmployeeTimeSheetApprovalTimeDTO()
                        {
                            EmployeeTimeSheetIID = detail.EmployeeTimeSheetIID,
                            EmployeeID = sheet.EmployeeID,
                            TimesheetTimeTypeID = sheet.TimesheetTimeType != null && !string.IsNullOrEmpty(sheet.TimesheetTimeType.Key) ? byte.Parse(sheet.TimesheetTimeType.Key) : (byte?)null,
                            TimesheetEntryStatusID = sheet.TimesheetApprovalStatus != null && !string.IsNullOrEmpty(sheet.TimesheetApprovalStatus.Key) ? byte.Parse(sheet.TimesheetApprovalStatus.Key) : (byte?)null,
                        });
                    }

                    var timesheetApprovalDTO = new EmployeeTimeSheetApprovalDTO()
                    {
                        EmployeeTimeSheetApprovalIID = sheet.EmployeeTimeSheetApprovalIID,
                        EmployeeTimeSheetID = sheet.EmployeeTimeSheetID,
                        EmployeeID = sheet.EmployeeID,
                        NormalHours = sheet.ApprovedNormalHours,
                        OTHours = sheet.ApprovedOTHours,
                        TimeSheetDate = string.IsNullOrEmpty(sheet.TimesheetDateString) ? (DateTime?)null : DateTime.ParseExact(sheet.TimesheetDateString, dateFormat, CultureInfo.InvariantCulture),
                        TimesheetDateFrom = DateTime.ParseExact(sheet.TimesheetDateString, dateFormat, CultureInfo.InvariantCulture),
                        TimesheetDateTo = DateTime.ParseExact(sheet.TimesheetDateString, dateFormat, CultureInfo.InvariantCulture),
                        TimesheetTimeTypeID = sheet.TimesheetTimeType != null && !string.IsNullOrEmpty(sheet.TimesheetTimeType.Key) ? byte.Parse(sheet.TimesheetTimeType.Key) : (byte?)null,
                        TimesheetApprovalStatusID = sheet.TimesheetApprovalStatus != null && !string.IsNullOrEmpty(sheet.TimesheetApprovalStatus.Key) ? byte.Parse(sheet.TimesheetApprovalStatus.Key) : (byte?)null,
                        Remarks = sheet.Remarks,
                        TimeSheetDetails = timesheetDetails
                    };

                    message = ClientFactory.EmployeeServiceClient(CallContext).SavePendingTimesheetApprovalData(timesheetApprovalDTO);
                }

                if (message.operationResult == OperationResult.Error)
                {
                    return Json(new { IsError = true, Response = message.Message });
                }
                else
                {
                    return Json(new { IsError = false, Response = message.Message });
                }
            }
            catch (Exception ex)
            {
                return Json(new { IsError = true, Response = ex.Message });
            }
        }

        [HttpGet]
        public JsonResult GetApprovedTimeSheetsDatas(long employeeID, string dateFromString, string dateToString)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var timeFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("TimeFormatWithoutSecond");

            var timeSheetApprovals = new List<EmployeeTimeSheetApprovalViewModel>();

            var dateFrom = DateTime.ParseExact(dateFromString, dateFormat, CultureInfo.InvariantCulture);

            var dateTo = DateTime.ParseExact(dateToString, dateFormat, CultureInfo.InvariantCulture);

            var approvedSheets = ClientFactory.EmployeeServiceClient(CallContext).GetApprovedTimeSheetsDatas(employeeID, dateFrom, dateTo);

            if (approvedSheets != null && approvedSheets.Count > 0)
            {
                foreach (var sheet in approvedSheets)
                {
                    var timeSheetTimeDetails = new List<EmployeeTimeSheetApprovalTimeViewModel>();
                    foreach (var timeDetail in sheet.TimeSheetDetails)
                    {
                        timeSheetTimeDetails.Add(new EmployeeTimeSheetApprovalTimeViewModel()
                        {
                            EmployeeTimeSheetIID = timeDetail.EmployeeTimeSheetIID,
                            FromTimeString = timeDetail.FromTime.HasValue ? DateTime.Parse(timeDetail.FromTime.Value.ToString()).ToString(timeFormat) : null,
                            ToTimeString = timeDetail.ToTime.HasValue ? DateTime.Parse(timeDetail.ToTime.Value.ToString()).ToString(timeFormat) : null,
                            TimesheetTimeTypeID = timeDetail.TimesheetTimeTypeID,
                            TimesheetTimeTypeName = timeDetail.TimesheetTimeTypeName,
                            TaskID = timeDetail.TaskID.HasValue ? Convert.ToInt64(timeDetail.TaskID) : 0,
                            TaskName = timeDetail.TaskName,
                        });
                    }

                    timeSheetApprovals.Add(new EmployeeTimeSheetApprovalViewModel()
                    {
                        IsExpand = false,
                        EmployeeTimeSheetApprovalIID = sheet.EmployeeTimeSheetApprovalIID,
                        EmployeeTimeSheetID = sheet.EmployeeTimeSheetID,
                        EmployeeID = sheet.EmployeeID,
                        Employee = sheet.EmployeeID.HasValue ? new KeyValueViewModel()
                        {
                            Key = sheet.EmployeeID.ToString(),
                            Value = sheet.EmployeeName
                        } : new KeyValueViewModel(),
                        DateFromString = sheet.TimesheetDateFrom.HasValue ? sheet.TimesheetDateFrom.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        DateToString = sheet.TimesheetDateTo.HasValue ? sheet.TimesheetDateTo.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        TimesheetTimeTypeID = sheet.TimesheetTimeTypeID,
                        TimesheetTimeType = new KeyValueViewModel()
                        {
                            Key = sheet.TimesheetTimeTypeID.HasValue ? sheet.TimesheetTimeTypeID.ToString() : null,
                            Value = sheet.TimesheetTimeTypeName
                        },
                        ApprovedNormalHours = sheet.NormalHours,
                        ApprovedOTHours = sheet.OTHours,
                        TimeSheetTimeDetails = timeSheetTimeDetails
                    });
                }
            }

            return Json(timeSheetApprovals);
        }

        #region Save manual entry data for each row, but currently, it is not being used
        [HttpPost]
        public ActionResult SaveTimesheetManualEntryData(EmployeeTimeSheetApprovalViewModel timeSheetApproval)
        {
            try
            {
                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

                //Convert dates
                timeSheetApproval.DateFromString = string.IsNullOrEmpty(timeSheetApproval.DateFromString) ? null : Convert.ToDateTime(timeSheetApproval.DateFromString).ToString(dateFormat, CultureInfo.InvariantCulture);
                timeSheetApproval.DateToString = string.IsNullOrEmpty(timeSheetApproval.DateToString) ? null : Convert.ToDateTime(timeSheetApproval.DateToString).ToString(dateFormat, CultureInfo.InvariantCulture);

                var crudSave = ClientFactory.FrameworkServiceClient(CallContext).SaveCRUDData(new CRUDDataDTO() { ScreenID = (long)Screens.EmployeeTimeSheetApproval, Data = timeSheetApproval.AsDTOString(timeSheetApproval.ToDTO(CallContext)) });

                if (crudSave.IsError)
                {
                    return Json(new { IsError = true, Response = crudSave.ErrorMessage });
                }
                else
                {
                    return Json(new { IsError = false, Response = "Successfully saved!" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { IsError = true, Response = ex.Message });
            }
        }
        #endregion

        [HttpPost]
        public ActionResult SaveSelectedTimesheetEntries([FromBody] List<EmployeeTimeSheetApprovalViewModel> timeSheetApprovalLists)
        {
            try
            {
                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

                foreach (var timeSheetApproval in timeSheetApprovalLists)
                {
                    var crudSave = ClientFactory.FrameworkServiceClient(CallContext).SaveCRUDData(new CRUDDataDTO() { ScreenID = (long)Screens.EmployeeTimeSheetApproval, Data = timeSheetApproval.AsDTOString(timeSheetApproval.ToDTO(CallContext)) });

                    if (crudSave.IsError)
                    {
                        return Json(new { IsError = true, Response = crudSave.ErrorMessage });
                    }
                }

                return Json(new { IsError = false, Response = "Successfully saved!" });
            }
            catch (Exception ex)
            {
                return Json(new { IsError = true, Response = ex.Message });
            }
        }

        [HttpGet]
        public JsonResult LeaveGroupChanges(int? LeaveGroupID)
        {
            var leaveMapList = ClientFactory.EmployeeServiceClient(CallContext).LeaveGroupChanges(LeaveGroupID);

            var leaveList = new List<EmployeeLeaveTypeViewModel>();

            foreach (var data in leaveMapList)
            {
                leaveList.Add(new EmployeeLeaveTypeViewModel()
                {
                    EmployeeSalaryStructureComponentMapID = null,
                    EmployeeSalaryStructureID = null,
                    LeaveTypeID = data.LeaveTypeID,
                    LeaveType = KeyValueViewModel.ToViewModel(data.LeaveType),
                    AllocatedLeaves = (decimal?)(data.NoofLeaves)
                });
            }

            return Json(leaveList);
        }

        [HttpGet]
        public JsonResult GetEmployeeDatasByLogin()
        {
            try
            {
                var employeeData = ClientFactory.EmployeeServiceClient(CallContext).GetEmployeeDataByLogin(CallContext.LoginID);

                return Json(new { IsError = false, Response = employeeData });
            }
            catch (Exception ex)
            {
                return Json(new { IsError = true, Response = ex.Message });
            }
        }

        public ActionResult VerifyAllSalarySlips(string salarySlipIDString)
        {
            var salarySlipIDs = salarySlipIDString.Split(',')
                           .Select(id => long.Parse(id))
                           .ToList();

            dynamic response = new { IsFailed = 1, Message = "Something went wrong!" };

            _backgroundJobs.Enqueue(() => VerifyAllSalarySlipsEnqueue(salarySlipIDs, CallContext));
            response = new { IsFailed = false, Message = "Salaryslip is being generated in the background. This may take a few moments." };

            return Json(response);
        }

        public void VerifyAllSalarySlipsEnqueue(List<long> salarySlipIDs, Eduegate.Framework.CallContext _context)
        {

            try
            {
                foreach (var salarySlipID in salarySlipIDs)
                {
                    var salarySlipDatas = ClientFactory.EmployeeServiceClient(CallContext).GetSalarySlipByID(salarySlipID);

                    ClientFactory.EmployeeServiceClient(CallContext).UpdateSalarySlipIsVerifiedData(salarySlipID, true);

                    ClientFactory.EmployeeServiceClient(CallContext).UpdateSlipData(salarySlipDatas);
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                               ? ex.InnerException?.Message : ex.Message;

                Eduegate.Logger.LogHelper<string>.Fatal($"The Salary slip verified process has failed. Error message: {errorMessage}", ex);

            }
        }

        public JsonResult FillEmployeeBasicDetailsByEmpID(long employeeID)
        {
            var result = new Eduegate.Domain.Repository.Payroll.EmployeeRepository().GetEmployeeDetailsByEmployeeIID(employeeID);
            return Json(result);
        }

        public JsonResult GetSchoolPrincipal(byte schoolID)
        {
            var result = ClientFactory.EmployeeServiceClient(CallContext).GetSchoolPrincipal(schoolID);
            return Json(result);
        }

        public JsonResult GetSchoolVicePrincipal(byte schoolID)
        {
            var result = ClientFactory.EmployeeServiceClient(CallContext).GetSchoolVicePrincipal(schoolID);
            return Json(result);
        }

        public JsonResult GetSchoolHeadMistress(byte schoolID)
        {
            var result = ClientFactory.EmployeeServiceClient(CallContext).GetSchoolHeadMistress(schoolID);
            return Json(result);
        }

        public JsonResult GetClassTeachers(int classID, int sectionID, int academicYearID)
        {
            var result = ClientFactory.SchoolServiceClient(CallContext).GetClassHeadTeacher(classID, sectionID, academicYearID);
            return Json(result);
        }

        public JsonResult GetClassCoordinator(int classID, int sectionID, int academicYearID)
        {
            var result = ClientFactory.SchoolServiceClient(CallContext).GetClassCoordinator(classID, sectionID, academicYearID);
            return Json(result);
        }

        public JsonResult GetClassAssociateTeachers(int classID, int sectionID, int academicYearID)
        {
            var result = ClientFactory.SchoolServiceClient(CallContext).GetAssociateTeachers(classID, sectionID, academicYearID);
            return Json(result);
        }

        public JsonResult GetClassOtherTeachers(int classID, int sectionID, int academicYearID)
        {
            var result = ClientFactory.SchoolServiceClient(CallContext).GetOtherTeachersByClass(classID, sectionID, academicYearID);
            return Json(result);
        }

    }
}