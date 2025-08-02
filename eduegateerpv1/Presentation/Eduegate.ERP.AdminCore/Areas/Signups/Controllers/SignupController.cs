using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.OnlineExam.Exam;
using Eduegate.Web.Library.OnlineExam.OnlineExam;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Services.Contracts.SignUp.SignUps;

namespace Eduegate.ERP.Admin.Areas.Signups.Controllers
{
    [Area("Signups")]
    public class SignupController : BaseSearchController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetSignupGroupDetailsByID(int signupGroupID)
        {
            var groupDetails = ClientFactory.SignupServiceClient(CallContext).GetSignupGroupDetailsByID(signupGroupID);

            if (groupDetails == null)
            {
                return Json(new { IsError = true, Response = groupDetails });
            }
            else
            {
                return Json(new { IsError = false, Response = groupDetails });
            }
        }

        public ActionResult MeetingFollowups()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetEmployeesActiveSignups()
        {
            var loginID = CallContext?.LoginID;

            var employeeDetails = ClientFactory.EmployeeServiceClient(CallContext).GetEmployeeDataByLogin(loginID);

            var signupDetails = employeeDetails != null ? ClientFactory.SignupServiceClient(CallContext).GetActiveSignUpDetailsByEmployeeID(employeeDetails.EmployeeIID) : null;

            if (signupDetails == null)
            {
                return Json(new { IsError = true, Response = signupDetails });
            }
            else
            {
                return Json(new { IsError = false, Response = signupDetails });
            }
        }

        public ActionResult FollowupDetails()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveSignupSlotRemarks([FromBody] SignupSlotRemarkMapDTO slotRemarkMap)
        {
            var returnResult = ClientFactory.SignupServiceClient(CallContext).SaveSignupSlotRemarkMap(slotRemarkMap);

            if (returnResult == null)
            {
                return Json(new { IsError = true, Response = returnResult });
            }
            else
            {
                return Json(new { IsError = false, Response = returnResult });
            }
        }

        [HttpGet]
        public JsonResult GetAvailableSlotsByDate(string stringDate)
        {
            var slotList = ClientFactory.SignupServiceClient(CallContext).GetAvailableSlotsByDate(stringDate);

            if (slotList == null || slotList.Count == 0)
            {
                return Json(new { IsError = true, Response = slotList });
            }
            else
            {
                return Json(new { IsError = false, Response = slotList });
            }
        }

    }
}