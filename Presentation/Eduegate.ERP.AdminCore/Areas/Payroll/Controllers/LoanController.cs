using Eduegate.Domain.Entity.HR.Loan;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Services.Contracts.HR.Payroll;
using Eduegate.Web.Library.HR.Loans;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Eduegate.Services.Client.Factory;
using Eduegate.Web.Library.HR.Payroll;
using Eduegate.Services.Contracts.HR.Loan;
using Eduegate.Web.Library.School.Fees;
using Eduegate.Framework.Web.Library.Common;
namespace Eduegate.ERP.AdminCore.Areas.Payroll.Controllers
{
    [Area("Payroll")]
    public class LoanController : BaseSearchController
    {
        // GET: Payroll/Loan
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Loan(long LoanRequestID = 0, long LoanHeadID = 0)
        {
            var vm = new LoanApprovalViewModel();
            vm.LoanRequestID = LoanRequestID;
            vm.LoanHeadIID = LoanHeadID;
            if (LoanHeadID == 0)
            {
                vm.IsApproveLoan = true;
                vm.IsLoanEntry = false;

            }
            else
            {
                vm.IsLoanEntry = true;
                vm.IsApproveLoan = false;
            }

            return View(vm);
        }
        [HttpGet]
        public JsonResult FillLoanData(long? loanRequestID, long? loanHeadID)
        {

            try
            {
                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");


                var loanHeadDTO = ClientFactory.EmployeeServiceClient(CallContext).FillLoanData(loanRequestID, loanHeadID);


                var loanData = new LoanApprovalViewModel()
                {
                    EmployeeID = loanHeadDTO.EmployeeID,
                    LoanHeadIID = loanHeadDTO.LoanHeadIID,
                    //Employee = KeyValueViewModel.ToViewModel(loanHeadDTO.Employee),
                    LoanRequestID = loanHeadDTO.LoanRequestID,
                    LoanTypeID = loanHeadDTO.LoanTypeID,
                    LoanType = loanHeadDTO.LoanTypeID.HasValue ? loanHeadDTO.LoanTypeID.ToString() : null,
                    LoanRequestNo = loanHeadDTO.LoanRequestNo,
                    LoanStatus = loanHeadDTO.LoanStatusID.HasValue ? loanHeadDTO.LoanStatusID.ToString() : null,
                    NoofInstallments = loanHeadDTO.NoOfInstallments,
                    LoanDate = loanHeadDTO.LoanDate,
                    PaymentstartdateString = loanHeadDTO.PaymentStartDate.HasValue ? loanHeadDTO.PaymentStartDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                    LoanAmount = loanHeadDTO.LoanAmount,
                    InstallmentAmount = loanHeadDTO.InstallmentAmount,
                    Remarks = loanHeadDTO.Remarks,
                    LoanDateString = loanHeadDTO.LoanDate.HasValue ? loanHeadDTO.LoanDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null


                };
                loanData.LoanApprovalInstallments = new List<LoanApprovalInstallmentViewModel>();
                foreach (var details in loanHeadDTO.LoanDetailDTOs)
                {
                    loanData.LoanApprovalInstallments.Add(new LoanApprovalInstallmentViewModel()
                    {
                        LoanDetailID = details.LoanDetailID,
                        LoanHeadID = details.LoanHeadID,
                        InstallmentDate = details.InstallmentDate,
                        InstallmentDateString = details.InstallmentDate.HasValue ? details.InstallmentDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        InstallmentReceivedDateString = details.InstallmentReceivedDate.HasValue ? details.InstallmentReceivedDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        InstallmentReceivedDate = details.InstallmentReceivedDate,
                        InstallmentAmount = details.InstallmentAmount,
                        Remarks = details.Remarks,
                        LoanEntryStatusID = details.LoanEntryStatusID,
                        LoanEntryStatus = details.LoanEntryStatusID.HasValue ? details.LoanEntryStatusID.ToString() : null,
                        IsDisableStatus = details.LoanEntryStatusID == 4 || details.LoanEntryStatusID == 5 ? true : false,

                    });
                }


                return Json(new { IsError = false, Response = loanData });

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

        [HttpGet]
        public JsonResult GetTotalSalaryAmount(long employeeID, DateTime? loanDate)
        {
            try
            {
                var totalSalaryAmount = ClientFactory.EmployeeServiceClient(CallContext).GetTotalSalaryAmount(employeeID, loanDate);
                return Json(new { IsError = false, Response = totalSalaryAmount });
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
    }
}
