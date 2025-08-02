using Eduegate.Domain.Entity.HR.Models.Payroll;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Contracts.Common;

using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.CommonDTO;
using Eduegate.Services.Contracts.HR.Payroll;
using Eduegate.Web.Library.HR.Payroll;
using Eduegate.Web.Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Eduegate.ERP.AdminCore.Areas.Payroll.Controllers
{
    [Area("Payroll")]
    public class ESBProvisionController : BaseSearchController
    {
        // GET: Payroll/ESBProvision
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GenerateESBProvision(ESBProvisionViewModel esbProvision)
        {
            try
            {
                dynamic response = new { IsFailed = 1, Message = "Something went wrong!" };
                List<KeyValueDTO> employeelist = new List<KeyValueDTO>();
                List<KeyValueDTO> departmentlist = new List<KeyValueDTO>();
                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
                if (esbProvision.EmployeeList != null && esbProvision.EmployeeList.Count() > 0)
                {
                    foreach (KeyValueViewModel vm in esbProvision.EmployeeList)
                    {
                        employeelist.Add(new KeyValueDTO { Key = vm.Key, Value = vm.Value });
                    }
                }
                if (esbProvision.Department != null && esbProvision.Department.Value != null)
                {
                    departmentlist.Add(new KeyValueDTO { Key = esbProvision.Department.Key, Value = esbProvision.Department.Value });
                }
                if (departmentlist.Count() == 0 && employeelist.Count() == 0)
                {
                    response = new { IsError = 1, Response = "Please Select Department or employees!" };
                    return Json(response);
                }
                OperationResultWithIDsDTO operationResultWithIDsDTOs = ClientFactory.EmployeeServiceClient(CallContext).GenerateESBProvision(new Services.Contracts.HR.Payroll.EmployeeESBProvisionHeadDTO()
                {
                    Department = departmentlist,
                    Employees = employeelist,
                    BranchID = esbProvision.BranchID,
                    IsRegenerate = esbProvision.IsRegenerate,
                    SalaryComponentID =esbProvision.ESBSalaryComponentID,
                    EmployeeESBProvisionHeadIID = esbProvision.EmployeeESBProvisionHeadIID,
                    IsaccountPosted=esbProvision.IsaccountPosted,
                    
                    EntryDate = string.IsNullOrEmpty(esbProvision.EntryDateToString) ? (DateTime?)null : DateTime.ParseExact(esbProvision.EntryDateToString, dateFormat, CultureInfo.InvariantCulture)
                });

                var settlementData = new EmployeeLeaveSalarySettlementViewModel();
                if (operationResultWithIDsDTOs != null && operationResultWithIDsDTOs.operationResult == Framework.Contracts.Common.Enums.OperationResult.Success)
                {


                    if (operationResultWithIDsDTOs.EmployeeESBProvisionHead != null && operationResultWithIDsDTOs.EmployeeESBProvisionHead.ESBProvisionDetails.Count > 0)
                    {
                        var esbProvisionDetailsList = new List<ESBProvisionDetailsViewModel>();
                        foreach (EmployeeESBProvisionDetailDTO dto in operationResultWithIDsDTOs.EmployeeESBProvisionHead.ESBProvisionDetails)
                        {
                            var vieModelData = new ESBProvisionDetailsViewModel()
                            {
                                DOJString = dto.DOJ.HasValue ? dto.DOJ.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                                BasicSalary = dto.BasicSalary ?? 0,
                                Category = dto.Category,
                                Department = dto.Department,
                                EmployeeCode = dto.EmployeeCode,
                                EmployeeName = dto.EmployeeName,                                
                                DOJ = dto.DOJ,
                                ESBAmount = dto.ESBAmount,
                                EmployeeID = dto.EmployeeID,
                                Balance=dto.Balance,
                                OpeningAmount=dto.OpeningAmount,
                                EmployeeESBProvisionHeadID = dto.EmployeeESBProvisionHeadID,
                                EmployeeESBProvisionDetailIID = dto.EmployeeESBProvisionDetailIID,
                            };
                            esbProvisionDetailsList.Add(vieModelData);
                        }
                        esbProvision.EntryNumber = operationResultWithIDsDTOs.EmployeeESBProvisionHead.EntryNumber;
                        esbProvision.EmployeeESBProvisionHeadIID = operationResultWithIDsDTOs.EmployeeESBProvisionHead.EmployeeESBProvisionHeadIID;
                        esbProvision.ESBProvisionTab.ESBProvisionDetail = esbProvisionDetailsList;
                        return Json(new { IsError = false, Response = esbProvision });

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
                var result1 = ClientFactory.EmployeeServiceClient(CallContext).UpdateESBAccountPostStatus(new Services.Contracts.HR.Payroll.EmployeeESBProvisionHeadDTO()
                {
                    IsaccountPosted = true,
                    EmployeeESBProvisionHeadIID = long.Parse(referenceIDs),

                });
                var transDate = DateTime.Now;
                int type = documentTypeID != null && documentTypeID != 0 ? (int)documentTypeID : 301;
                var loginID = CallContext.LoginID.HasValue ? (int)CallContext.LoginID.Value : 0;
                result = ClientFactory.AccountingServiceClient(CallContext).AccountMergeWithMultipleTransactionIDs(referenceIDs, transDate, loginID, type);
            }
            return Json(result);
        }

        public JsonResult GetEmployeesByDepartment(long? departmentID)
        {
            departmentID = departmentID != null ? departmentID : 0;
            var employees = ClientFactory.EmployeeServiceClient(CallContext).GetEmployeesByDepartment(departmentID.Value);

            return Json(employees);
        }
    }
}
