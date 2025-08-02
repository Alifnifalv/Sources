using Eduegate.Application.Mvc;
using Microsoft.AspNetCore.Mvc;
using Eduegate.Services.Contracts.SignUp.SignUps;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Domain.SignUp.SignUps;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.SignUp.Meeting;

namespace Eduegate.Signup.Portal.Controllers
{
    public class SignUpController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Event()
        {
            return View();
        }

        public ActionResult EventDetails(int? groupID)
        {
            ViewBag.SignUpGroupID = groupID;
            return View();
        }

        [HttpGet]
        public JsonResult FillSignUpDetailsByGroupID(int groupID)
        {
            var signUpDetails = new SignUpBL(CallContext).FillSignUpDetailsByGroupID(groupID);

            if (signUpDetails == null)
            {
                return Json(new { IsError = true, Response = "There are some issues.Please try after sometime!" });
            }
            else
            {
                return Json(new { IsError = false, Response = signUpDetails });
            }
        }

        [HttpPost]
        public ActionResult SaveSelectedSignUpSlot(SignupSlotMapDTO signUpSlotMap)
        {
            var returnResult = new SignUpBL(CallContext).SaveSelectedSignUpSlot(signUpSlotMap);

            if (returnResult.operationResult == OperationResult.Success)
            {
                return Json(new { IsError = false, Response = returnResult.Message });
            }
            else
            {
                return Json(new { IsError = true, Response = returnResult.Message });
            }
        }

        [HttpGet]
        public JsonResult GetActiveSignupGroups()
        {
            var signupGroups = new SignUpBL(CallContext).GetActiveSignupGroups();

            if (signupGroups == null && signupGroups.Count == 0)
            {
                return Json(new { IsError = true, Response = "There are some issues.Please try after sometime!" });
            }
            else
            {
                return Json(new { IsError = false, Response = signupGroups });
            }
        }

        [HttpPost]
        public ActionResult CancelSelectedSignUpSlot(SignupSlotMapDTO signUpSlotMap)
        {
            var returnResult = new SignUpBL(CallContext).CancelSelectedSignUpSlot(signUpSlotMap);

            if (returnResult.operationResult == OperationResult.Success)
            {
                return Json(new { IsError = false, Response = returnResult.Message });
            }
            else
            {
                return Json(new { IsError = true, Response = returnResult.Message });
            }
        }

        public ActionResult MeetingRequestList()
        {
            return View();
        }

        public ActionResult MeetingRequest()
        {
            var vm = new MeetingRequestDTO();
            return View(vm);
        }

        [HttpGet]
        public JsonResult GetSchoolPrincipalOrHeadMistress(byte schoolID)
        {
            var result = ClientFactory.EmployeeServiceClient(CallContext).GetSchoolPrincipalOrHeadMistress(schoolID);

            if (result == null)
            {
                return Json(new { IsError = true, Response = "There are some issue !" });
            }
            else
            {
                return Json(new { IsError = false, Response = result });
            }
        }

        [HttpGet]
        public JsonResult GetSchoolWisePrincipal(byte schoolID)
        {
            var result = ClientFactory.EmployeeServiceClient(CallContext).GetSchoolWisePrincipal(schoolID);

            if (result == null)
            {
                return Json(new { IsError = true, Response = "There are some issue !" });
            }
            else
            {
                return Json(new { IsError = false, Response = result });
            }
        }

        [HttpGet]
        public JsonResult GetClassHeadTeacher(int classID, int sectionID, int academicYearID)
        {
            var result = ClientFactory.SchoolServiceClient(CallContext).GetClassHeadTeacher(classID, sectionID, academicYearID);

            if (result == null)
            {
                return Json(new { IsError = true, Response = "There are some issue !" });
            }
            else
            {
                return Json(new { IsError = false, Response = result });
            }
        }

        [HttpGet]
        public JsonResult GetClassCoordinator(int classID, int sectionID, int academicYearID)
        {
            var result = ClientFactory.SchoolServiceClient(CallContext).GetClassCoordinator(classID, sectionID, academicYearID);

            if (result == null)
            {
                return Json(new { IsError = true, Response = "There are some issue !" });
            }
            else
            {
                return Json(new { IsError = false, Response = result });
            }
        }

        [HttpGet]
        public JsonResult GetAssociateTeachers(int classID, int sectionID, int academicYearID)
        {
            var result = ClientFactory.SchoolServiceClient(CallContext).GetAssociateTeachers(classID, sectionID, academicYearID);

            if (result.Count == 0)
            {
                return Json(new { IsError = true, Response = "There are some issue !" });
            }
            else
            {
                return Json(new { IsError = false, Response = result });
            }
        }

        [HttpGet]
        public JsonResult GetOtherTeachersByClass(int classID, int sectionID, int academicYearID)
        {
            var result = ClientFactory.SchoolServiceClient(CallContext).GetOtherTeachersByClass(classID, sectionID, academicYearID);

            if (result.Count == 0)
            {
                return Json(new { IsError = true, Response = "There are some issue !" });
            }
            else
            {
                return Json(new { IsError = false, Response = result });
            }
        }

        [HttpGet]
        public JsonResult GetMeetingRequestSlotsByEmployeeID(long employeeID, string reqSlotDateString)
        {
            var signUpDetails = new SignUpBL(CallContext).GetMeetingRequestSlotsByEmployeeID(employeeID, reqSlotDateString);

            if (signUpDetails == null)
            {
                return Json(new { IsError = true, Response = "There are some issues.Please try after sometime!" });
            }
            else
            {
                return Json(new { IsError = false, Response = signUpDetails });
            }
        }

        [HttpPost]
        public ActionResult SubmitMeetingRequest(MeetingRequestDTO meetingRequestDTO)
        {
            var returnResult = new SignUpBL(CallContext).SaveMeetingRequest(meetingRequestDTO);

            if (returnResult.operationResult == OperationResult.Success)
            {
                return Json(new { IsError = false, Response = returnResult.Message });
            }
            else
            {
                return Json(new { IsError = true, Response = returnResult.Message });
            }
        }

        [HttpGet]
        public JsonResult GetMeetingRequestsByParentID(long? parentID)
        {
            var meetingRequests = new SignUpBL(CallContext).GetMeetingRequestsByParentID(parentID);

            if (meetingRequests == null && meetingRequests.Count == 0)
            {
                return Json(new { IsError = true, Response = "There are some issues.Please try after sometime!" });
            }
            else
            {
                return Json(new { IsError = false, Response = meetingRequests });
            }
        }

        public ActionResult MeetingRemarks()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetParentAllotedMeetings(long? parentID)
        {
            var meetingRequests = new SignUpBL(CallContext).GetParentAllotedMeetings(parentID);

            if (meetingRequests == null && meetingRequests.Count == 0)
            {
                return Json(new { IsError = true, Response = "There are some issues.Please try after sometime!" });
            }
            else
            {
                return Json(new { IsError = false, Response = meetingRequests });
            }
        }

        [HttpPost]
        public ActionResult SubmitMeetingRemarks([FromBody] SignupSlotRemarkMapDTO remarkEntry)
        {
            var returnResult = ClientFactory.SignupServiceClient(CallContext).SaveSignupSlotRemarkMap(remarkEntry);

            if (returnResult == null)
            {
                return Json(new { IsError = true, Response = returnResult });
            }
            else
            {
                return Json(new { IsError = false, Response = returnResult });
            }
        }

    }
}