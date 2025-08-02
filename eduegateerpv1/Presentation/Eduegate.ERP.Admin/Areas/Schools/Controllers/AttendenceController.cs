using System;
using System.Globalization;
using System.Web.Mvc;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Services.Client.Factory;
using Eduegate.Web.Library.School.Attendences;
using Eduegate.Web.Library.ViewModels;

namespace Eduegate.ERP.Admin.Areas.Schools.Controllers
{
    public class AttendenceController : BaseSearchController
    {
        // GET: Schools/Attendence
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StaffAttendence()
        {
            // call the service and get the meta data
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList(Services.Contracts.Enums.SearchView.StaffAttendence);
            ViewBag.HasExportAccess = ClientFactory.UserServiceClient(CallContext).HasClaimAccessByResourceID("DATAEXPORTACCESS", CallContext.LoginID.Value);
            ViewBag.HasSortableFeature = ClientFactory.UserServiceClient(CallContext).HasClaimAccessByResourceID("SORTABLEGRIDFEATURE", CallContext.LoginID.Value);
            Helpers.PortalWebHelper.GetDefaultSettingsToViewBag(CallContext, this.ViewBag);

            return View(new SearchListViewModel
            {
                ControllerName = "Schools/Attendence",
                ViewName = Eduegate.Infrastructure.Enums.SearchView.StaffAttendence,
                ViewTitle = metadata.ViewName,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                FilterColumns = metadata.QuickFilterColumns,
                HasFilters = metadata.HasFilters,
                UserValues = metadata.UserValues,
                EnabledSortableGrid = false,
                ViewFullPath = metadata.ViewFullPath,
                ViewActions = metadata.ViewActions,

                HasChild = metadata.HasChild,
                ChildView = metadata.ChildView,
                ChildFilterField = metadata.ChildFilterField,
                ActualControllerName = metadata.ControllerName,
                IsRowClickForMultiSelect = metadata.IsRowClickForMultiSelect,
                IsMasterDetail = metadata.IsMasterDetail,
                IsEditableLink = metadata.IsEditable,
                IsGenericCRUDSave = metadata.IsGenericCRUDSave,
                IsReloadSummarySmartViewAlways = metadata.IsReloadSummarySmartViewAlways,
                JsControllerName = metadata.JsControllerName,
                RuntimeFilter = string.Empty,
                HasSortable = false
            });
        }

        public ActionResult StudentAttendence()
        {
            // call the service and get the meta data
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList(Services.Contracts.Enums.SearchView.StudentAttendence);
            ViewBag.HasExportAccess = ClientFactory.UserServiceClient(CallContext).HasClaimAccessByResourceID("DATAEXPORTACCESS", CallContext.LoginID.Value);
            ViewBag.HasSortableFeature = ClientFactory.UserServiceClient(CallContext).HasClaimAccessByResourceID("SORTABLEGRIDFEATURE", CallContext.LoginID.Value);
            Helpers.PortalWebHelper.GetDefaultSettingsToViewBag(CallContext, this.ViewBag);

            return View(new SearchListViewModel
            {
                ControllerName = "Schools/Attendence",
                ViewName = Eduegate.Infrastructure.Enums.SearchView.StudentAttendence,
                ViewTitle = metadata.ViewName,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                FilterColumns = metadata.QuickFilterColumns,
                HasFilters = metadata.HasFilters,
                UserValues = metadata.UserValues,
                EnabledSortableGrid = false,
                ViewFullPath = metadata.ViewFullPath,
                ViewActions = metadata.ViewActions,

                HasChild = metadata.HasChild,
                ChildView = metadata.ChildView,
                ChildFilterField = metadata.ChildFilterField,
                ActualControllerName = metadata.ControllerName,
                IsRowClickForMultiSelect = metadata.IsRowClickForMultiSelect,
                IsMasterDetail = metadata.IsMasterDetail,
                IsEditableLink = metadata.IsEditable,
                IsGenericCRUDSave = metadata.IsGenericCRUDSave,
                IsReloadSummarySmartViewAlways = metadata.IsReloadSummarySmartViewAlways,
                JsControllerName = metadata.JsControllerName,
                RuntimeFilter = string.Empty,
                HasSortable = false
            });
        }

        //get present status
        public ActionResult GetPresentStatuses()
        {
            var presentStatuses = ClientFactory.SchoolServiceClient(CallContext).GetPresentStatuses();
            return Json(presentStatuses, JsonRequestBehavior.AllowGet);
        }
        //end get present status

        //get staff present status
        public ActionResult GetStaffPresentStatuses()
        {
            var presentStatuses = ClientFactory.SchoolServiceClient(CallContext).GetStaffPresentStatuses();
            return Json(presentStatuses, JsonRequestBehavior.AllowGet);
        }
        //end get staff present status

        public ActionResult GetStaffAttendanceByMonthYear(int month, int year)
        {
            var attendence = ClientFactory.SchoolServiceClient(CallContext).GetStaffAttendanceByMonthYear(month, year);
            return Json(attendence, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetStudentAttendenceByYearMonth(int month, int year, int classId,int sectionId)
        {
            var attendence = ClientFactory.SchoolServiceClient(CallContext).GetStudentAttendenceByYearMonth(month, year,classId,sectionId);
            return Json(attendence, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateAllStudentAttendence(int classId, int sectionId, string AttendenceDateString, byte? PStatus)
        {
            string message = "0#Something went wrong!";
            var dateFormat = Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue("DateFormat");
            var listStudents = ClientFactory.SchoolServiceClient(CallContext).GetClasswiseStudentData(classId, sectionId);
            var attendenceDate = DateTime.ParseExact(AttendenceDateString, dateFormat, CultureInfo.InvariantCulture);
            foreach (var student in listStudents)
            {
                if (attendenceDate >= student.FeeStartDate)
                {
                    var attendence = ClientFactory.SchoolServiceClient(CallContext).GetStudentAttendence(student.StudentIID, attendenceDate);
                    byte? unmarkedStatusID = 9;

                    if (attendence == null || attendence.PresentStatusID == unmarkedStatusID)
                    {
                        message = ClientFactory.SchoolServiceClient(CallContext).SaveStudentAttendence(new Services.Contracts.School.Attendences.StudentAttendenceDTO()
                        {
                            AttendenceDate = attendenceDate,
                            AttendenceReasonID = null,
                            PresentStatusID = PStatus,
                            Reason = "",
                            StudentID = student.StudentIID,
                            StudentAttendenceIID = attendence == null ? 0 : attendence.StudentAttendenceIID,
                            ClassID = classId,
                            SectionID = sectionId
                        });
                    }
                }
                
            }
            string[] resp = message.Split('#');
            dynamic response = new { IsFailed = (resp[0] == "0"), Message = resp[1] };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateAllStaffAttendance(long calendarID, string AttendenceDateString, byte? PStatus)
        {
            string message = "0#Something went wrong!";
            var dateFormat = Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue("DateFormat");
            var attendenceDate = DateTime.ParseExact(AttendenceDateString, dateFormat, CultureInfo.InvariantCulture);

            var listEmployees= ClientFactory.SchoolServiceClient(CallContext).GetEmployeesByCalendarID(calendarID);

            foreach (var employee in listEmployees)
            {
                var attendence = ClientFactory.SchoolServiceClient(CallContext).GetStaffAttendence(employee.EmployeeIID, attendenceDate);

                message = ClientFactory.SchoolServiceClient(CallContext).SaveStaffAttendance(new Services.Contracts.School.Attendences.StaffAttendenceDTO()
                {
                    AttendenceDate = attendenceDate,
                    AttendenceReasonID = null,
                    PresentStatusID = PStatus,
                    Reason = "",
                    EmployeeID = employee.EmployeeIID,
                    StaffAttendenceIID = attendence == null ? 0 : attendence.StaffAttendenceIID,
                });
            }
            string[] resp = message.Split('#');
            dynamic response = new { IsFailed = (resp[0] == "0"), Message = resp[1] };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveAttendence(SaveAttendenceInfoViewModel attendenceInfo)
        {
            string message = "0#Something went wrong!";
            var dateFormat = Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue("DateFormat");
            var attendenceDate = DateTime.ParseExact(attendenceInfo.AttendenceDateString, dateFormat, CultureInfo.InvariantCulture);
            if (attendenceInfo.StudentID.HasValue && attendenceInfo.StudentID.Value != 0)
            {
                var attendence = ClientFactory.SchoolServiceClient(CallContext).GetStudentAttendence(attendenceInfo.StudentID.Value, attendenceDate);
                message = ClientFactory.SchoolServiceClient(CallContext).SaveStudentAttendence(new Services.Contracts.School.Attendences.StudentAttendenceDTO()
                {
                    AttendenceDate = attendenceDate,
                    AttendenceReasonID = attendenceInfo.AttendenceReasonID,
                    PresentStatusID = attendenceInfo.PresentStatusID,
                    Reason = attendenceInfo.Reason,
                    StudentID = attendenceInfo.StudentID,
                    StudentAttendenceIID = attendence == null ? 0 : attendence.StudentAttendenceIID,
                    ClassID = attendenceInfo.ClassID,
                    SectionID = attendenceInfo.SectionID
                });
            }

            if (attendenceInfo.StaffID.HasValue && attendenceInfo.StaffID.Value != 0)
            {
                var attendence = ClientFactory.SchoolServiceClient(CallContext).GetStaffAttendence(attendenceInfo.StaffID.Value, attendenceDate);

                ClientFactory.SchoolServiceClient(CallContext).SaveStaffAttendance(new Services.Contracts.School.Attendences.StaffAttendenceDTO()
                {
                    AttendenceDate = attendenceDate,
                    AttendenceReasonID = attendenceInfo.AttendenceReasonID,
                    PresentStatusID = attendenceInfo.PresentStatusID,
                    Reason = attendenceInfo.Reason,
                    EmployeeID = attendenceInfo.StaffID,
                    StaffAttendenceIID = attendence == null ? 0 : attendence.StaffAttendenceIID
                });
            }

            string[] resp = message.Split('#');
            dynamic response = new { IsFailed = (resp[0] == "0"), Message = resp[1] };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

    }
}