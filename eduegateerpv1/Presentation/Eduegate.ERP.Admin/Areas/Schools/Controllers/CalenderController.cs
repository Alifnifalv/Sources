using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Web.Library.School.Academics;
using Eduegate.Web.Library.School.Attendences;
using Eduegate.Web.Library.ViewModels;

namespace Eduegate.ERP.Admin.Areas.Schools.Controllers
{
    public class CalenderController : BaseSearchController
    {
        // GET: Schools/Calender
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AcademicCalender()
        {
            // call the service and get the meta data
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList(Services.Contracts.Enums.SearchView.StaffAttendence);
            ViewBag.HasExportAccess = ClientFactory.UserServiceClient(CallContext).HasClaimAccessByResourceID("DATAEXPORTACCESS", CallContext.LoginID.Value);
            ViewBag.HasSortableFeature = ClientFactory.UserServiceClient(CallContext).HasClaimAccessByResourceID("SORTABLEGRIDFEATURE", CallContext.LoginID.Value);
            Helpers.PortalWebHelper.GetDefaultSettingsToViewBag(CallContext, this.ViewBag);

            return View(new SearchListViewModel
            {
                ControllerName = "Schools/AcademicCalender",
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
                RuntimeFilter = string.Empty
            });
        }
     
        public JsonResult GetAcademicCalenderByAcademicYear(int academicYearID, int year, int academicCalendarStatusID,long academicCalendarID)
        {
            var calendarList = ClientFactory.SchoolServiceClient(CallContext).GetAcademicCalenderByAcademicYear(academicYearID, year, academicCalendarStatusID, academicCalendarID);
            return Json(calendarList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAcademicCalenderByMonthYear(int month, int year)
        {
            var calendarList = ClientFactory.SchoolServiceClient(CallContext).GetAcademicCalenderByMonthYear(month, year);
            return Json(calendarList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveAcademicCalendar(AcademicCalendarEventsInfoViewModel calendarInfo)
        {
            string id = "0";
            var dateFormat = Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue("DateFormat");
          
            var startDate = DateTime.ParseExact(calendarInfo.StartDateString, dateFormat, CultureInfo.InvariantCulture);
            var endDate = DateTime.ParseExact(calendarInfo.EndDateString, dateFormat, CultureInfo.InvariantCulture);

            int res = DateTime.Compare(startDate,endDate);

            if (res>0)
            {
                return Json("#103", JsonRequestBehavior.AllowGet);
            }
            // var startDate = new DateTime(calendarInfo.StartDateString);
            var acadamicCalendarDTO = new AcadamicCalendarDTO()
            {
                AcademicCalendarID = calendarInfo.AcademicCalendarID,
                AcademicCalendarStatusID = calendarInfo.AcademicCalendarStatusID,
                AcademicYearID = calendarInfo.AcademicYearID,
              
            };
            var academicYearCalendarEventDTO = new AcademicYearCalendarEventDTO()
            {
                AcademicCalendarID = calendarInfo.AcademicCalendarID,
                AcademicYearCalendarEventIID= calendarInfo.AcademicYearCalendarEventIID,
                AcademicCalendarEventTypeID = calendarInfo.AcademicCalendarEventTypeID,
                EventTitle = calendarInfo.EventTitle,
                Description = calendarInfo.Description,
                StartDate = startDate,
                EndDate = endDate,
                ColorCode = calendarInfo.ColorCode,
                IsThisAHoliday= calendarInfo.IsThisAHoliday,
                NoofHours = calendarInfo.NoofHours
            };

            acadamicCalendarDTO.AcademicYearCalendarEventDTO.Add(academicYearCalendarEventDTO);

            id = ClientFactory.SchoolServiceClient(CallContext).SaveAcademicCalendar(acadamicCalendarDTO);

            return Json(id, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteAcademicCalendarEvent(long academicYearCalendarEventIID, long academicYearCalendarID)
        {
            ClientFactory.SchoolServiceClient(CallContext).DeleteAcademicCalendarEvent(academicYearCalendarEventIID,  academicYearCalendarID);
            return Json(academicYearCalendarEventIID, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAcademicMonthAndYearByCalendarID(long? calendarID)
        {
            var calendarList = ClientFactory.SchoolServiceClient(CallContext).GetAcademicMonthAndYearByCalendarID(calendarID);
            return Json(calendarList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCalendarEventsByCalendarID(long calendarID)
        {
            var calendarList = ClientFactory.SchoolServiceClient(CallContext).GetCalendarEventsByCalendarID(calendarID);
            return Json(calendarList, JsonRequestBehavior.AllowGet);
        }

    }
}