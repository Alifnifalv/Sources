using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Extensions;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Common;
using Eduegate.Web.Library.ViewModels.HR;
using Eduegate.Web.Library.ViewModels.Payroll;
using Eduegate.Services.Client.Factory;
using Eduegate.Domain;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Eduegate.ERP.Admin.Areas.HR.Controllers
{
    [Area("HR")]
    public class EmploymentRequestController : BaseSearchController
    {
        // GET: Payroll/Employee
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.EmploymentRequest);

            return View(new SearchListViewModel
            {
                ViewName = Eduegate.Infrastructure.Enums.SearchView.EmploymentRequest,
                ControllerName = "HR/EmploymentRequest",
                HeaderList = metadata.Columns,
                SummaryHeaderList = new List<Services.Contracts.Search.ColumnDTO>(),
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                InfoBar = @"<li class='status-label-mobile'>
                            <div class='right status-label'>
                                <div class='status-label-color'><label class='status-color-label purple'></label>Submitted by Dept</div>
                                <div class='status-label-color'><label class='status-color-label yellow'></label>Visa company updated by Personnel</div>
                                <div class='status-label-color'><label class='status-color-label lightblue'></label>Approved by management</div>
                                <br/>
                                <div class='status-label-color'><label class='status-color-label orange'></label>Updated in Passport</div>
                                <div class='status-label-color'><label class='status-color-label blue'></label>Candidate joins and Joining memo send</div>
                                <div class='status-label-color'><label class='status-color-label green'></label>Completed</div>
                            </div>
                        </li>",
                HasFilters = metadata.HasFilters,
                FilterColumns = metadata.QuickFilterColumns,
                UserValues = metadata.UserValues
            });
        }

        [HttpGet]
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {
            return base.SearchData(Eduegate.Infrastructure.Enums.SearchView.EmploymentRequest, currentPage, orderBy, runtimeFilter);
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
            var viewModel = new CRUDMasterDetailViewModel();
            viewModel.Name = "EmploymentRequest";
            viewModel.ListActionName = "EmploymentRequest";
            viewModel.ListButtonDisplayName = "Lists";
            viewModel.DisplayName = "EmploymentRequest";
            viewModel.PrintPreviewReportName = "EmploymentRequest";
            //viewModel.JsControllerName = "EmploymentRequestController";


            viewModel.Model = new EmploymentRequestViewModel()
            {
                MasterViewModel = new EmploymentRequestMasterViewModel()
                {
                    //EMP_REQ_NO = 1,
                    //EMP_NO = 2

                }

            };

            viewModel.MasterViewModel = new EmploymentRequestMasterViewModel() { IsSummaryPanel = false, isNewRequest = ID != 0 ? false : default(bool?) };

            viewModel.SummaryViewModel = new BaseMasterViewModel();

            viewModel.IID = ID;

            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Nationality", Url = "Mutual/GetLookUpData?lookType=Nationality" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "EmploymentType", Url = "Mutual/GetLookUpData?lookType=EmploymentType" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "RecruitmentType", Url = "Mutual/GetLookUpData?lookType=RecruitmentType" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Department", Url = "Mutual/GetLookUpData?lookType=HRDepartment" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "PayComp", Url = "Mutual/GetLookUpData?lookType=PayComp" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "GroupDesignation", Url = "Mutual/GetLookUpData?lookType=GroupDesignation" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "ProductiveType", Url = "Mutual/GetLookUpData?lookType=ProductiveType" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "PeriodSalary", Url = "Mutual/GetLookUpData?lookType=PeriodSalary" });

            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Gender", Url = "Mutual/GetLookUpData?lookType=HRGender" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "EmployeeList", Url = "Mutual/GetLookUpData?lookType=EmployeeList" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "MaritalStatus", Url = "Mutual/GetLookUpData?lookType=HRMaritalStatus" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "ContractType", Url = "Mutual/GetLookUpData?lookType=ContractType" });

            viewModel.EntityType = Framework.Enums.EntityTypes.Transaction.ToString();

            TempData["viewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(viewModel);
            return RedirectToAction("CreateMasterDetail", "CRUD", new { area = "" });
        }

        [HttpGet]
        public ActionResult Edit(long ID = 0)
        {
            return RedirectToAction("Create", new { ID = ID });
        }

        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            var dto = ClientFactory.EmployementServiceClient(CallContext).GetEmploymentRequest(ID);
            var vm = EmploymentRequestViewModel.ToVM(dto);
            vm.MasterViewModel.isNewRequest = false;
            return Json(vm);
        }

        [HttpPost]
        public JsonResult ValidateField(EmploymentRequestViewModel vm, string fieldName)
        {
            var converteddto = EmploymentRequestViewModel.ToDTO(vm);
            var keyvalueDTO = ClientFactory.EmployementServiceClient(CallContext).ValidateEmploymentRequest(converteddto, fieldName);
            return Json(new { IsError = keyvalueDTO.Key.ToLower() == "false" ? true : false, UserMessage = keyvalueDTO.Value });
        }


        [HttpPost]
        public JsonResult Save(string model)
        {
            var jsonData = JObject.Parse(model);
            var vm = JsonConvert.DeserializeObject<EmploymentRequestViewModel>(jsonData.SelectToken("data").ToString());

            var serviceClient = ClientFactory.EmployementServiceClient(CallContext);
            var converteddto = EmploymentRequestViewModel.ToDTO(vm);
            var keyvalueDTO = serviceClient.ValidateEmploymentRequest(converteddto, string.Empty);

            if (keyvalueDTO.Key.ToLower() == "false")
            {
                return Json(new { IsError = true, UserMessage = keyvalueDTO.Value });
            }

            var dto = ClientFactory.EmployementServiceClient(CallContext).SaveEmploymentRequest(converteddto);

            var updatedVM = EmploymentRequestViewModel.ToVM(dto);
            updatedVM.MasterViewModel.isNewRequest = false;
            return Json(updatedVM);
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

        [HttpGet]
        public ActionResult WorkFlow(int requestID)
        {
            var dto = ClientFactory.EmployementServiceClient(CallContext).GetEmploymentRequest(requestID);
            var vm = EmploymentRequestViewModel.ToVM(dto);

            if (vm.MasterViewModel.VisaCompany.IsNotNull() && string.IsNullOrEmpty(vm.MasterViewModel.VisaCompany.Key))
            {
                var visaCompany = ClientFactory.EmployementServiceClient(CallContext).GetDefaultVisaCompany(long.Parse(vm.MasterViewModel.Shop.Key));

                if (visaCompany.IsNotNull() && visaCompany.IsNotDefault())
                {
                    vm.MasterViewModel.VisaCompany.Key = visaCompany.Key;
                    vm.MasterViewModel.VisaCompany.Value = visaCompany.Value;
                }
            }

            return View(vm);    
        }

        [HttpGet]
        public JsonResult GetQuotaType()
        {
            var VMList = new List<KeyValueViewModel>();
            var quotaType = ClientFactory.ReferenceDataServiceClient(CallContext).GetLookUpData(Eduegate.Services.Contracts.Enums.LookUpTypes.QuotaType, string.Empty, 0, 0);

            foreach (var quota in quotaType)
            {
                VMList.Add(KeyValueViewModel.ToViewModel(quota));
            }

            return Json(VMList);
        }

        [HttpGet]
        public JsonResult GetVisaCompany(int deptNo)
        {
            var VMList = new List<KeyValueViewModel>();
            var visaCompany = ClientFactory.EmployementServiceClient(CallContext).GetVisaCompany(long.Parse(deptNo.ToString()));
            foreach (var visaCo in visaCompany)
            {
                VMList.Add(KeyValueViewModel.ToViewModel(visaCo));
            }

            return Json(VMList);
        }

        [HttpPost]
        public JsonResult SaveWorkFlow(EmploymentRequestViewModel vm)
        {
            var serviceClient = ClientFactory.EmployementServiceClient(CallContext);
            var converteddto = EmploymentRequestViewModel.ToDTO(vm);
            converteddto.EmpRequestStatus = new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = "1", Value = "" };

            var dto = ClientFactory.EmployementServiceClient(CallContext).SaveWorkFlow(converteddto);

            var updatedVM = EmploymentRequestViewModel.ToVM(dto);

            return Json(updatedVM);
        }


        [HttpGet]
        public JsonResult GetEmploymentRequestStatus(long empReqNo)
        {
            var VMList = new List<KeyValueViewModel>();
            var requestStatuses = ClientFactory.EmployementServiceClient(CallContext).GetEmploymentRequestStatus(empReqNo);
            foreach (var status in requestStatuses)
            {
                VMList.Add(KeyValueViewModel.ToViewModel(status));
            }

            return Json(VMList);
        }

    }
}