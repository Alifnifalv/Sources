using Eduegate.Domain;
using Eduegate.Domain.Payroll;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Web.Library.HR.Career;
using Microsoft.AspNetCore.Mvc;

namespace Eduegate.ERP.Admin.Areas.HR.Controllers
{
    [Area("HR")]
    public class JobOpeningController : BaseSearchController
    {

        [HttpGet]
        public IActionResult GetApplicantsForShortList(long ID = 0)
        {
            try
            {
                var vm = JobApplicationManagementViewModel.FromDTOtoVM(new RecruitmentBL(CallContext).GetApplicantsForShortList(ID,false));

                return Json(vm);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<JobOpeningController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, UserMessage = ex.Message });
            }

        }

        [HttpGet]
        public IActionResult GetShortListedApplicants(long ID = 0)
        {
            try
            {
                var vm = JobInterviewManagementViewModel.FromDTOtoVM(new RecruitmentBL(CallContext).GetApplicantsForShortList(ID, true));
                    
                return Json(vm);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<JobOpeningController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, UserMessage = ex.Message });
            }

        }
        

        [HttpGet]
        public IActionResult GetCandidateFromInterviewMap(long ID = 0)
        {
            try
            {
                var vm = Eduegate.Web.Library.ViewModels.Payroll.EmployeeViewModel.PackageNegotiationFromDTOtoVM(new EmployeeBL(CallContext).GetCandidateFromInterviewMap(ID));
                    
                return Json(vm);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<JobOpeningController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, UserMessage = ex.Message });
            }

        }

        public JsonResult GetJobDescriptionByJDMasterID(long JDMasterID)
        {
            var result = new RecruitmentBL(CallContext).GetJobDescriptionByJDMasterID(JDMasterID);
            return Json(result);
        }
    }
}