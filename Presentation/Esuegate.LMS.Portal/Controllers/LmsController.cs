using Eduegate.Application.Mvc;
using Microsoft.AspNetCore.Mvc;
using Eduegate.Services.Contracts.SignUp.SignUps;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Domain.Lms.Lms;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.SignUp.Meeting;
using Eduegate.Services.Contracts.Lms;
using Eduegate.Services.Contracts.School.Academics;

namespace Eduegate.LMS.Portal.Controllers
{
    public class LmsController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Event()
        {
            return View();
        }

        public ActionResult LessonplanList()
        {
            return View();
        }

        public ActionResult Lessonplan(long? LessonPlanID)
        {
            ViewBag.LessonPlanID = LessonPlanID;
            return View();
        }

        public ActionResult AssignmentList()
        {
            return View();
        }

        public ActionResult Assignment(long? AssignmentID)
        {
            ViewBag.AssignmentID = AssignmentID;
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
            var signUpDetails = new LmsBL(CallContext).FillSignUpDetailsByGroupID(groupID);

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
        public ActionResult SaveSelectedSignUpSlot(LmsSlotMapDTO signUpSlotMap)
        {
            var returnResult = new LmsBL(CallContext).SaveSelectedSignUpSlot(signUpSlotMap);

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
            var signupGroups = new LmsBL(CallContext).GetActiveSignupGroups();

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
        public ActionResult CancelSelectedSignUpSlot(LmsSlotMapDTO signUpSlotMap)
        {
            var returnResult = new LmsBL(CallContext).CancelSelectedSignUpSlot(signUpSlotMap);

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
            var signUpDetails = new LmsBL(CallContext).GetMeetingRequestSlotsByEmployeeID(employeeID, reqSlotDateString);

            if (signUpDetails == null)
            {
                return Json(new { IsError = true, Response = "There are some issues.Please try after sometime!" });
            }
            else
            {
                return Json(new { IsError = false, Response = signUpDetails });
            }
        }

        [HttpGet]
        public JsonResult GetLessonPlanByLessonID(long LessonPlanID)
        {
            var LessonPlanDetails = new LmsBL(CallContext).GetLessonPlanByLessonID(LessonPlanID);

            if (LessonPlanDetails == null)
            {
                return Json(new { IsError = true, Response = "There are some issues.Please try after sometime!" });
            }
            else
            {
                return Json(new { IsError = false, Response = LessonPlanDetails });
            }
        }



        [HttpGet]
        public JsonResult GetLessonPlanListBySubject(long studentID)
        {
            var LessonPlanDetails = new LmsBL(CallContext).GetLessonPlanListBySubject(studentID);

            if (LessonPlanDetails == null)
            {
                return Json(new { IsError = true, Response = "There are some issues.Please try after sometime!" });
            }
            else
            {
                return Json(new { IsError = false, Response = LessonPlanDetails });
            }
        }

        [HttpGet]
        public JsonResult GetAssignmentByAssignmentID(long AssignmentID)
        {
            var LessonPlanDetails = new LmsBL(CallContext).GetAssignmentByAssignmentID(AssignmentID);

            if (LessonPlanDetails == null)
            {
                return Json(new { IsError = true, Response = "There are some issues.Please try after sometime!" });
            }
            else
            {
                return Json(new { IsError = false, Response = LessonPlanDetails });
            }
        }

        
        [HttpPost]
        public ActionResult SubmitAssignment(AssignmentDTO assignmentDTO)
        {
            var returnResult = new LmsBL(CallContext).SubmitStudentAssignment(assignmentDTO);

            if (returnResult.operationResult == OperationResult.Success)
            {
                return Json(new { IsError = false, Response = returnResult.Message });
            }
            else
            {
                return Json(new { IsError = true, Response = returnResult.Message });
            }
        }

    }
}