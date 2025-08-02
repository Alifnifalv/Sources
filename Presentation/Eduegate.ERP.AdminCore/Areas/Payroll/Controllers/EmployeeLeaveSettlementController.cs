using Eduegate.ERP.Admin.Controllers;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.HR.Payroll;
using Eduegate.Web.Library.HR.Payroll;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Domain.Entity.HR.Payroll;
using Org.BouncyCastle.Ocsp;
using Eduegate.Services.Contracts.CommonDTO;
namespace Eduegate.ERP.AdminCore.Areas.Payroll.Controllers
{
    [Area("Payroll")]
    public class EmployeeLeaveSettlementController : BaseSearchController
    {
        // GET: Payroll/Employee
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetSettlementDateDetails(string salaryCalculationDateString, byte? employeeSalarySettlementTypeID)
        {
            try
            {
                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
                var salaryCalculationDate = DateTime.ParseExact(salaryCalculationDateString, dateFormat, CultureInfo.InvariantCulture);
                var employeeSettlementDTO = new EmployeeSettlementDTO();
                employeeSettlementDTO = ClientFactory.EmployeeServiceClient(CallContext).GetSettlementDateDetails(new EmployeeSettlementDTO() { SalaryCalculationDate = salaryCalculationDate, EmployeeSettlementTypeID = employeeSalarySettlementTypeID });


                var settlementData = new EmployeeLeaveSalarySettlementViewModel();
                var finalsettlementData = new EmployeeFinalSettlementViewModel();

                if (employeeSettlementDTO != null)
                {
                    if (employeeSalarySettlementTypeID == 1)
                    {
                        settlementData = new EmployeeLeaveSalarySettlementViewModel();
                        settlementData = new EmployeeLeaveSalarySettlementViewModel()
                        {
                            NoofDaysInTheMonthLS = employeeSettlementDTO.NoofDaysInTheMonthLS,
                            MCTabEmployeeLeaveDatails = new TabEmployeeLeaveDatailsViewModel()
                            {
                                NoofDaysInTheMonth = employeeSettlementDTO.NoofDaysInTheMonth,
                            }
                        };
                        return Json(new { IsError = false, Response = settlementData });
                    }
                    else
                    {
                        finalsettlementData = new EmployeeFinalSettlementViewModel();

                        finalsettlementData = new EmployeeFinalSettlementViewModel()
                        {
                            NoofDaysInTheMonthLS = employeeSettlementDTO.NoofDaysInTheMonthLS,
                            EndofServiceDays = employeeSettlementDTO.EndofServiceDaysPerYear,
                            NoofDaysInTheMonthEoSB = employeeSettlementDTO.NoofDaysInTheMonthEoSB,
                            MCTabEmployeeLeaveDatails = new TabEmployeeLeaveDatailsViewModel()
                            {
                                NoofDaysInTheMonth = employeeSettlementDTO.NoofDaysInTheMonth,
                            }
                        };
                        return Json(new { IsError = false, Response = finalsettlementData });
                    }
                }
                return Json(new { IsError = true, Response = "No Data found!" });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    IsError = true,
                    Response = ex.Message
                });
            }
        }

        #region Leave Salary Settlement
        public JsonResult GetEmployeeDetailsToSettlement(string employeeID, string salaryCalculationDateString, decimal? noofDaysInTheMonth, decimal? noofDaysInTheMonthLS, byte? employeeSalarySettlementTypeID)
        {
            try
            {
                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
                var salaryCalculationDate = DateTime.ParseExact(salaryCalculationDateString, dateFormat, CultureInfo.InvariantCulture);
                var employeeSettlementDTO = new EmployeeSettlementDTO();
                var operationResultDTOs = ClientFactory.EmployeeServiceClient(CallContext)
                    .GetEmployeeDetailsToSettlement(
                    new EmployeeSettlementDTO() 
                    {
                        EmployeeID = long.Parse(employeeID),
                        SalaryCalculationDate = salaryCalculationDate, 
                        NoofDaysInTheMonth = noofDaysInTheMonth,
                        NoofDaysInTheMonthLS = noofDaysInTheMonthLS,
                        EmployeeSettlementTypeID = employeeSalarySettlementTypeID , 
                        IsFromSalarySlipGeneration =false
                    });

                var data = new EmployeeSettlementDTO();
                var settlementData = new EmployeeLeaveSalarySettlementViewModel();

                if (operationResultDTOs.operationResult != Framework.Contracts.Common.Enums.OperationResult.Error)
                {
                    data = operationResultDTOs.EmployeeSettlementDTO;
                    settlementData = new EmployeeLeaveSalarySettlementViewModel()
                    {
                        BranchID = data.BranchID,
                        EmployeeCode = data.EmployeeCode,
                        Remarks = data.Remarks,
                        MCTabEmployeeLeaveDatails = new TabEmployeeLeaveDatailsViewModel()
                        {
                            NoofDaysInTheMonth = data.NoofDaysInTheMonth,
                            LossofPay = data.NoofLOPDays,
                            AnnualLeaveEntitilements = data.AnnualLeaveEntitilements,
                            EarnedLeave = data.EarnedLeave,
                            LeaveDueFrom = data.LeaveDueFrom,
                            LeaveStartDate = data.LeaveStartDate,
                            LeaveEndDate = data.LeaveEndDate,
                            NoofSalaryDays = data.NoofSalaryDays,
                            DateOfJoining = data.DateOfJoining,
                            DateOfJoiningString = data.DateOfJoining.HasValue ? data.DateOfJoining.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                            LeaveDueFromString = data.LeaveDueFrom.HasValue ? data.LeaveDueFrom.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                            LeaveStartDateString = data.LeaveStartDate.HasValue ? data.LeaveStartDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                            LeaveEndDateString = data.LeaveEndDate.HasValue ? data.LeaveEndDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,

                        },
                        MCTabSalaryStructure = new TabSalaryStructureViewModel()
                        {

                            SalaryComponents = (from sk in data.SalarySlipDTOs
                                                where sk.SalaryComponentKeyValue != null && sk.SalaryComponentKeyValue.Key != null
                                                select new EmployeeSettlementComponentsViewModel()
                                                {
                                                    Deduction = sk.Amount.Value < 0 ? sk.Amount.Value * -1 : (decimal?)null,
                                                    Earnings = sk.Amount.Value > 0 ? sk.Amount.Value * 1 : (decimal?)null,
                                                    Description = sk.Description,
                                                    NoOfDays = sk.NoOfDays,
                                                    SalaryComponentID = sk.SalaryComponentID,
                                                    SalaryComponent = new KeyValueViewModel()
                                                    {
                                                        Key = sk.SalaryComponentKeyValue.Key.ToString(),
                                                        Value = sk.SalaryComponentKeyValue.Value.ToString()
                                                    }
                                                }
                                            ).ToList(),
                        },
                    };

                    return Json(new { IsError = false, Response = settlementData });
                }
                else
                {
                    return Json(new { IsError = true, Response = operationResultDTOs.Message });
                }
            }
            catch (Exception ex)
            {
                return Json(new { IsError = true, Response = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult SaveSalarySettlement(EmployeeSettlementDTO settlementInfo)
        {
            dynamic response = new { IsFailed = 1, Message = "Something went wrong!" };
            var dto = new EmployeeSettlementDTO();

            dto.EmployeeID = settlementInfo.EmployeeID;

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            dto.SalaryCalculationDate = string.IsNullOrEmpty(settlementInfo.SalaryCalculationDateString) ? (DateTime?)null : DateTime.ParseExact(settlementInfo.SalaryCalculationDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.EmployeeSettlementDate = string.IsNullOrEmpty(settlementInfo.EmployeeSettlementDateString) ? (DateTime?)null : DateTime.ParseExact(settlementInfo.EmployeeSettlementDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.LeaveDueFrom = string.IsNullOrEmpty(settlementInfo.LeaveDueFromString) ? (DateTime?)null : DateTime.ParseExact(settlementInfo.LeaveDueFromString, dateFormat, CultureInfo.InvariantCulture);
            dto.LeaveStartDate = string.IsNullOrEmpty(settlementInfo.LeaveStartDateString) ? (DateTime?)null : DateTime.ParseExact(settlementInfo.LeaveStartDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.LeaveEndDate = string.IsNullOrEmpty(settlementInfo.LeaveEndDateString) ? (DateTime?)null : DateTime.ParseExact(settlementInfo.LeaveEndDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.DateOfLeaving = string.IsNullOrEmpty(settlementInfo.DateOfLeavingString) ? (DateTime?)null : DateTime.ParseExact(settlementInfo.DateOfLeavingString, dateFormat, CultureInfo.InvariantCulture);
            dto.EmployeeSettlementTypeID = settlementInfo.EmployeeSettlementTypeID;
            dto.AnnualLeaveEntitilements = settlementInfo.AnnualLeaveEntitilements;
            dto.EarnedLeave = settlementInfo.EarnedLeave;
            dto.BranchID = settlementInfo.BranchID;
            dto.LossofPay = settlementInfo.LossofPay;
            dto.NoofDaysInTheMonth = settlementInfo.NoofDaysInTheMonth;
            dto.NoofSalaryDays = settlementInfo.NoofSalaryDays;
            dto.NoofDaysInTheMonthLS = settlementInfo.NoofDaysInTheMonthLS;
            dto.NoofDaysInTheMonthEoSB = settlementInfo.NoofDaysInTheMonthEoSB;
            dto.EndofServiceDaysPerYear = settlementInfo.EndofServiceDaysPerYear;
            dto.SalarySlipDTOs = new List<SalarySlipDTO>();
            foreach (var e in settlementInfo.SalarySlipDTOs)
            {
                dto.SalarySlipDTOs.Add(new SalarySlipDTO()
                {
                    SalarySlipIID = e.SalarySlipIID,
                    SalaryComponentID = e.SalaryComponentID,
                    Amount = e.EarningAmount.HasValue ? e.EarningAmount : e.DeductingAmount.HasValue ? e.DeductingAmount * -1 : (decimal?)null,
                    Description = e.Description,
                    NoOfDays = e.NoOfDays,
                    NoOfHours = e.NoOfHours,
                    ReportContentID = e.ReportContentID,

                });
            }
            OperationResultWithIDsDTO salarydt = ClientFactory.EmployeeServiceClient(CallContext).SaveSalarySettlement(dto);
            if (salarydt != null && salarydt.SalarySlipIDList != null && salarydt.SalarySlipIDList.Count() > 0)
            {
                //Return SalarySlipPDF as Byte file
                var contentData = ClientFactory.ReportGenerationServiceClient(CallContext).GenerateSalarySlipContentFile(salarydt.SalarySlipIDList);
                //Save SalarySlipPDF in Content Table 
                var savedContentata = ClientFactory.ContentServicesClient(CallContext).SaveBulkContentFiles(contentData);
                // Update Salary slip with content ID
                var resultData = ClientFactory.EmployeeServiceClient(CallContext).UpdateSalarySlipPDF(savedContentata);
                response = new { IsFailed = false, Message = resultData.Message };
            }
            else
            {
                response = new { IsFailed = true, Message = salarydt.Message };
            }

            return Json(response);
        }
        #endregion

        #region Employee Final Settlement

        public JsonResult GetEmployeeDetailsToFinalSettlement(string employeeID, string salaryCalculationDateString, string dateOfLeavingString, decimal? endofServiceDays, decimal? noofDaysInTheMonth, decimal? noofDaysInTheMonthLS, decimal? noofDaysInTheMonthEoSB, byte? employeeSalarySettlementTypeID)
        {
            try
            {
                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
                var salaryCalculationDate = DateTime.ParseExact(salaryCalculationDateString, dateFormat, CultureInfo.InvariantCulture);
                var dateOfLeavingDate = DateTime.ParseExact(dateOfLeavingString, dateFormat, CultureInfo.InvariantCulture);
                var employeeSettlementDTO = new EmployeeSettlementDTO();
                var operationResultDTOs = ClientFactory.EmployeeServiceClient(CallContext).GetEmployeeDetailsToSettlement(
                    new EmployeeSettlementDTO()
                    {
                        EmployeeID = long.Parse(employeeID),
                        SalaryCalculationDate = salaryCalculationDate,
                        DateOfLeaving = dateOfLeavingDate,
                        EndofServiceDaysPerYear = endofServiceDays,
                        NoofDaysInTheMonth = noofDaysInTheMonth,
                        NoofDaysInTheMonthLS = noofDaysInTheMonthLS,
                        NoofDaysInTheMonthEoSB = noofDaysInTheMonthEoSB,
                        EmployeeSettlementTypeID = employeeSalarySettlementTypeID
                    });

                var data = new EmployeeSettlementDTO();
                var settlementData = new EmployeeFinalSettlementViewModel();

                if (operationResultDTOs != null && operationResultDTOs.EmployeeSettlementDTO != null)
                {
                    data = operationResultDTOs.EmployeeSettlementDTO;
                    settlementData = new EmployeeFinalSettlementViewModel()
                    {
                        BranchID = data.BranchID,
                        EmployeeCode = data.EmployeeCode,
                        Remarks = data.Remarks,
                        //EndofServiceDays = endofServiceDays,
                        //NoofDaysInTheMonth = noofDaysInTheMonthES,
                        //NoofDaysInTheMonthEoSB = noofDaysInTheMonthEoSB,
                        MCTabEmployeeLeaveDatails = new TabEmployeeLeaveDatailsViewModel()
                        {

                            DateOfJoining = data.DateOfJoining,
                            NoofDaysInTheMonth = data.NoofDaysInTheMonth,
                            LossofPay = data.NoofLOPDays,
                            EarnedLeave = data.EarnedLeave,
                            LeaveDueFrom = data.LeaveDueFrom,
                            LeaveStartDate = data.LeaveStartDate,
                            LeaveEndDate = data.LeaveEndDate,
                            NoofSalaryDays = data.NoofSalaryDays,
                            AnnualLeaveEntitilements = data.AnnualLeaveEntitilements,
                            DateOfJoiningString = data.DateOfJoining.HasValue ? data.DateOfJoining.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                            LeaveDueFromString = data.LeaveDueFrom.HasValue ? data.LeaveDueFrom.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                            LeaveStartDateString = data.LeaveStartDate.HasValue ? data.LeaveStartDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                            LeaveEndDateString = data.LeaveEndDate.HasValue ? data.LeaveEndDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,

                        },
                        MCTabSalaryStructure = new TabSalaryStructureViewModel()
                        {

                            SalaryComponents = (from sk in data.SalarySlipDTOs
                                                where sk.SalaryComponentKeyValue != null && sk.SalaryComponentKeyValue.Key != null
                                                select new EmployeeSettlementComponentsViewModel()
                                                {
                                                    Deduction = sk.Amount.Value < 0 ? sk.Amount.Value * -1 : (decimal?)null,
                                                    Earnings = sk.Amount.Value > 0 ? sk.Amount.Value * 1 : (decimal?)null,
                                                    Description = sk.Description,
                                                    NoOfDays = sk.NoOfDays,
                                                    SalaryComponentID = sk.SalaryComponentID,
                                                    SalaryComponent = new KeyValueViewModel()
                                                    {
                                                        Key = sk.SalaryComponentKeyValue.Key.ToString(),
                                                        Value = sk.SalaryComponentKeyValue.Value.ToString()
                                                    }
                                                }
                                            ).ToList(),
                        },
                    };

                    return Json(new { IsError = false, Response = settlementData });
                }
                else
                {
                    return Json(new { IsError = true, Response = operationResultDTOs.Message });
                }
            }
            catch (Exception ex)
            {
                return Json(new { IsError = true, Response = ex.Message });
            }
        }

        #endregion 

    }
}