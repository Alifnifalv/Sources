using BoldReports.Linq;
using Eduegate.Domain.Entity.HR.Models.Payroll;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.CommonDTO;
using Eduegate.Services.Contracts.HR.Payroll;
using Eduegate.Web.Library.HR.Payroll;
using Eduegate.Web.Library.ViewModels;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Eduegate.ERP.AdminCore.Areas.Payroll.Controllers
{
    [Area("Payroll")]
    public class LSProvisionController : BaseSearchController
    {
        // GET: Payroll/LSProvision
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GenerateLeaveSalaryProvision(LSProvisionViewModel lsProvision)
        {
            try
            {
                dynamic response = new { IsFailed = 1, Message = "Something went wrong!" };
                List<KeyValueDTO> employeelist = new List<KeyValueDTO>();
                List<KeyValueDTO> departmentlist = new List<KeyValueDTO>();
                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

                if (lsProvision.EmployeeList != null && lsProvision.EmployeeList.Count() > 0)
                {
                    foreach (KeyValueViewModel vm in lsProvision.EmployeeList)
                    {
                        employeelist.Add(new KeyValueDTO { Key = vm.Key, Value = vm.Value });
                    }
                }
                if (lsProvision.Department != null && lsProvision.Department.Value != null)
                {
                    departmentlist.Add(new KeyValueDTO { Key = lsProvision.Department.Key, Value = lsProvision.Department.Value });
                }
                if (departmentlist.Count() == 0 && employeelist.Count() == 0)
                {
                    response = new { IsError = 1, Response = "Please Select Department or employees!" };
                    return Json(response);
                }
                OperationResultWithIDsDTO operationResultWithIDsDTOs = ClientFactory.EmployeeServiceClient(CallContext).GenerateLeaveSalaryProvision(new Services.Contracts.HR.Payroll.EmployeeLSProvisionHeadDTO()
                {
                    Department = departmentlist,
                    Employees = employeelist,
                    BranchID = lsProvision.BranchID,
                    IsRegenerate = lsProvision.IsRegenerate,
                    SalaryComponentID = lsProvision.LSSalaryComponentID,
                    EmployeeLSProvisionHeadIID = lsProvision.EmployeeLSProvisionHeadIID,
                    EntryDate = string.IsNullOrEmpty(lsProvision.EntryDateToString) ? (DateTime?)null : DateTime.ParseExact(lsProvision.EntryDateToString, dateFormat, CultureInfo.InvariantCulture)
                });
                if (operationResultWithIDsDTOs != null && operationResultWithIDsDTOs.operationResult == Framework.Contracts.Common.Enums.OperationResult.Success)
                {
                    if (operationResultWithIDsDTOs.EmployeeLSProvisionHead != null && operationResultWithIDsDTOs.EmployeeLSProvisionHead.LSProvisionDetails.Count() > 0)
                    {
                        var lSProvisionDetailsList = new List<LSProvisionDetailsViewModel>();
                        foreach (EmployeeLSProvisionDetailDTO dto in operationResultWithIDsDTOs.EmployeeLSProvisionHead.LSProvisionDetails)
                        {
                            var vieModelData = new LSProvisionDetailsViewModel()
                            {
                                EmployeeLSProvisionHeadID = dto.EmployeeLSProvisionHeadID,
                                EmployeeLSProvisionDetailIID = dto.EmployeeLSProvisionDetailIID,
                                LastLeaveSalaryDateString = dto.LastLeaveSalaryDate.HasValue ? dto.LastLeaveSalaryDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                                DOJString = dto.DOJ.HasValue ? dto.DOJ.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                                BasicSalary = dto.BasicSalary ?? 0,
                                Category = dto.Category,
                                Department = dto.Department,
                                EmployeeCode = dto.EmployeeCode,
                                EmployeeName = dto.EmployeeName,
                                DOJ = dto.DOJ,
                                Balance = dto.Balance,
                                OpeningAmount = dto.OpeningAmount,
                                NoofLeaveSalaryDays = dto.NoofLeaveSalaryDays,
                                LastLeaveSalaryDate = dto.LastLeaveSalaryDate,
                                LeaveSalaryAmount = dto.LeaveSalaryAmount,
                                EmployeeID = dto.EmployeeID,

                            };
                            lSProvisionDetailsList.Add(vieModelData);
                        }
                        lsProvision.EntryNumber = operationResultWithIDsDTOs.EmployeeLSProvisionHead.EntryNumber;
                        lsProvision.EmployeeLSProvisionHeadIID = operationResultWithIDsDTOs.EmployeeLSProvisionHead.EmployeeLSProvisionHeadIID;
                        lsProvision.LSProvisionTab.LSProvisionDetail = lSProvisionDetailsList;
                        return Json(new { IsError = false, Response = lsProvision });

                    }
                    else
                        return Json(new { IsError = true, Response = "No Details found!" });
                }
                else
                    return Json(new { IsError = true, Response = operationResultWithIDsDTOs.Message });

            }
            catch (Exception ex)
            {

                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                          ? ex.InnerException?.Message : ex.Message;
                return Json(new { IsError = true, Response = errorMessage });

            }
        }
        public JsonResult AccountPosting(string referenceIDs, long? documentTypeID)
        {
            var result = false;

            if (referenceIDs.Count() > 0)
            {

                var result1 = ClientFactory.EmployeeServiceClient(CallContext).UpdateLSAccountPostStatus(new Services.Contracts.HR.Payroll.EmployeeLSProvisionHeadDTO()
                {
                    IsaccountPosted = true,
                    EmployeeLSProvisionHeadIID = long.Parse(referenceIDs),

                });

                var transDate = DateTime.Now;
                int type = documentTypeID != null && documentTypeID != 0 ? (int)documentTypeID : 300;
                var loginID = CallContext.LoginID.HasValue ? (int)CallContext.LoginID.Value : 0;
                result = ClientFactory.AccountingServiceClient(CallContext).AccountMergeWithMultipleTransactionIDs(referenceIDs, transDate, loginID, type);

            }
            return Json(result);
        }
        public JsonResult GetEmployeesByDepartmentBranch(long? departmentID, int? branchID)
        {
            departmentID = departmentID != null ? departmentID : 0;
            var employees = ClientFactory.EmployeeServiceClient(CallContext).GetEmployeesByDepartmentBranch(departmentID.Value, branchID);

            return Json(employees);
        }

    }
}
