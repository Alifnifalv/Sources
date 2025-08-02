using Dapper;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Frameworks.Mvc.Controllers;
using Eduegate.Infrastructure.Enums;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.Framework;
using Eduegate.Services.Contracts.OnlineExam.Exam;
using Eduegate.Services.Contracts.Schedulers;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Services.Contracts.School.Transports;
using Eduegate.Services.Contracts.Settings;
using Eduegate.Web.Library.CRM.Leads;
using Eduegate.Web.Library.OnlineExam.OnlineExam;
using Eduegate.Web.Library.School.Academics;
using Eduegate.Web.Library.School.Exams;
using Eduegate.Web.Library.School.Fees;
using Eduegate.Web.Library.School.Students;
using Eduegate.Web.Library.School.Transports;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Notification;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace Eduegate.ERP.Admin.Areas.Schools.Controllers
{
    public class SchoolController : BaseController
    {
        // GET: Schools/School
        public ActionResult Index()
        {
            return View();
        }

        #region MARK ENTRY

        public ActionResult MarkEntry()
        {
            // call the service and get the meta data
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList(Services.Contracts.Enums.SearchView.MarkEntry);
            ViewBag.HasExportAccess = ClientFactory.UserServiceClient(CallContext).HasClaimAccessByResourceID("DATAEXPORTACCESS", CallContext.LoginID.Value);
            ViewBag.HasSortableFeature = ClientFactory.UserServiceClient(CallContext).HasClaimAccessByResourceID("SORTABLEGRIDFEATURE", CallContext.LoginID.Value);
            Helpers.PortalWebHelper.GetDefaultSettingsToViewBag(CallContext, this.ViewBag);

            return View(new SearchListViewModel
            {
                ControllerName = "Schools/School",
                ViewName = Eduegate.Infrastructure.Enums.SearchView.MarkEntry,
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


        //To Get Mark Entry
        public ActionResult GetCoScholasticEntry(int classID, int sectionId, long examID, long termID, int skillGroupID, long skillSetID,
            int academicYearID, int? skillID, short? languageTypeID, int? subjectMapID)
        {
            var entry = ClientFactory.SchoolServiceClient(CallContext).GetCoScholasticEntry(
                new MarkEntrySearchArgsDTO
                {
                    ClassID = classID,
                    SectionID = sectionId,
                    ExamID = examID,
                    TermID = termID,
                    SkillGroupID = skillGroupID,
                    SkillSetID = skillSetID,
                    AcademicYearID = academicYearID,
                    SkillID = skillID,
                    LanguageTypeID = languageTypeID,
                    SubjectMapID = subjectMapID
                });

            var response = new MarksEntryViewModel
            {
                Students = (from s in entry
                            select new StudentCoScholasticEntryViewModel
                            {
                                EnrolNo = s.AdmissionNumber,
                                StudentIID = s.StudentID.Value,
                                StudentName = s.Student.Value,
                                MarkStatusID = Convert.ToString(s.MarkStatusID),
                                SkillGroups = (from sg in s.MarkRegisterSkillGroupDTO
                                               select new SkillGroupCoScholasticEntryViewModel
                                               {
                                                   SkillGroupID = sg.SkillGroupMasterID.Value,
                                                   Skills = (from sk in sg.MarkRegisterSkillsDTO
                                                             select new SkillCoScholasticEntryViewModel
                                                             {
                                                                 SkillID = sk.SkillMasterID,
                                                                 SkillName = sk.Skill,
                                                                 GradeID = Convert.ToString(sk.MarksGradeMapID),
                                                                 MarkRegisterSkillGroupID = sk.MarkRegisterSkillGroupID,
                                                                 MarkRegisterSkillID = sk.MarkRegisterSkillIID,
                                                                 MarkRegisterID = sk.MarkRegisterID,
                                                                 MarkGradeMapList = sk.MarkGradeMapList
                                                             }
                                                            ).ToList(),
                                               }).ToList()

                            }).ToList()
            };


            return Json(response, JsonRequestBehavior.AllowGet);
            //skills.Add(new SkillGradeEntryViewModel
            //{
            //    SkillID = 1,
            //    SkillName = "Reading",
            //    GradeID = 1,
            //});
            //skills.Add(new SkillGradeEntryViewModel
            //{
            //    SkillID = 2,
            //    SkillName = "Writing",
            //    GradeID = 2,
            //});
            //var students = new List<StudentGradeEntryViewModel>();
            //students.Add(new StudentGradeEntryViewModel
            //{
            //    StudentIID = 1,
            //    EnrolNo = "001",
            //    StudentName = "Aditi",
            //    SkillGroups = new SkillGroupGradeEntryViewModel
            //    {
            //        SkillGroupID = 1,
            //        Skills = skills,
            //    }
            //});
            //students.Add(new StudentGradeEntryViewModel
            //{
            //    StudentIID = 2,
            //    EnrolNo = "002",
            //    StudentName = "Mihaan",
            //    SkillGroups = new SkillGroupGradeEntryViewModel
            //    {
            //        SkillGroupID = 1,
            //        Skills = skills,
            //    }
            //});
            //var RespModel = new GradeEntryViewModel
            //{
            //    IsError = false,
            //    Students = students
            //};


        }
        //End To Get Mark Entry

        public ActionResult MarkPublish()
        {
            // call the service and get the meta data
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList(Services.Contracts.Enums.SearchView.MarkPublish);
            ViewBag.HasExportAccess = ClientFactory.UserServiceClient(CallContext).HasClaimAccessByResourceID("DATAEXPORTACCESS", CallContext.LoginID.Value);
            ViewBag.HasSortableFeature = ClientFactory.UserServiceClient(CallContext).HasClaimAccessByResourceID("SORTABLEGRIDFEATURE", CallContext.LoginID.Value);
            Helpers.PortalWebHelper.GetDefaultSettingsToViewBag(CallContext, this.ViewBag);

            return View(new SearchListViewModel
            {
                ControllerName = "Schools/School",
                ViewName = Eduegate.Infrastructure.Enums.SearchView.MarkPublish,
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

        [HttpPost]
        public ActionResult SaveCoScholasticEntry(MarksEntryViewModel model)
        {
            string rtnMsg = "0#Something went wrong!";

            if (model.Students.Count == 0)
            {
                rtnMsg = "0#Please enter mark details for at least a student!";
            }
            else
            {
                var markRegisterDetails = new List<MarkRegisterDetailsDTO>();

                markRegisterDetails = (from x in model.Students
                                       where x.MarkStatusID != null || x.SkillGroups.Any(z => z.Skills.Any(sk => sk.GradeID != null))
                                       select new MarkRegisterDetailsDTO
                                       {
                                           StudentID = x.StudentIID,
                                           MarkStatusID = String.IsNullOrEmpty(x.MarkStatusID) ? null : (byte.Parse(x.MarkStatusID) as byte?),
                                           MarkRegisterSkillGroupDTO = (from y in x.SkillGroups
                                                                        select new MarkRegisterSkillGroupDTO
                                                                        {
                                                                            SkillGroupMasterID = model.SkillGroupID,
                                                                            MarkRegisterSkillsDTO = (from z in y.Skills
                                                                                                     select new MarkRegisterSkillsDTO
                                                                                                     {
                                                                                                         SkillGroupMasterID = model.SkillGroupID,
                                                                                                         SkillMasterID = z.SkillID,
                                                                                                         MarksGradeMapID = String.IsNullOrEmpty(z.GradeID) ? null : (byte.Parse(z.GradeID) as byte?),
                                                                                                         MarkRegisterSkillGroupID = z.MarkRegisterSkillGroupID ?? 0,
                                                                                                         MarkRegisterSkillIID = z.MarkRegisterSkillID ?? 0,
                                                                                                         MarkRegisterID = z.MarkRegisterID ?? 0

                                                                                                     }).ToList()
                                                                        }).ToList()
                                       }).ToList();
                if (markRegisterDetails.Count > 0)
                {
                    rtnMsg = ClientFactory.SchoolServiceClient(CallContext).SaveCoScholasticEntry(new MarkRegisterDTO()
                    {
                        ClassID = model.ClassID,
                        AcademicYearID = model.AcademicYearID,
                        SectionID = model.SectionId,
                        ExamID = model.ExamID,
                        ExamGroupID = model.ExamGroupID,
                        SkillGroupID = model.SkillGroupID,
                        SkillSetID = model.SkillSetID,
                        SkillID = model.SkillID,
                        MarkRegistersDetails = markRegisterDetails

                    });
                }
                else
                {
                    rtnMsg = "0#Please enter mark details for at least a student!";
                }
            }
            string successMsg = "Saved successfully!";
            dynamic response = new { IsError = false, Message = successMsg };
            if (!string.IsNullOrEmpty(rtnMsg))
            {

                string[] resp = rtnMsg.Split('#');
                response = new { IsError = (resp[0] == "0"), Message = (resp.Length > 1 ? resp[1] : successMsg) };
            }
            else
            {
                response = new { IsError = true, Message = "Something went wrong!" };
            }
            //}
            //return View();
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        //Save MarkListd
        [HttpPost]
        public ActionResult SaveScholasticInternalEntry(MarksEntryViewModel model)
        {
            string rtnMsg = "0#Something went wrong!";

            if (model.Students.Count == 0)
            {
                rtnMsg = "0#Please enter mark details for at least a student!";
            }
            else
            {
                var markRegisterDetails = new List<MarkRegisterDetailsDTO>();

                markRegisterDetails = (from x in model.Students
                                       where x.MarkStatusID != null || x.SkillGroups.Any(z => z.Skills.Any(sk => sk.GradeID != null))
                                       select new MarkRegisterDetailsDTO
                                       {
                                           StudentID = x.StudentIID,
                                           MarkStatusID = String.IsNullOrEmpty(x.MarkStatusID) ? null : (byte.Parse(x.MarkStatusID) as byte?),
                                           MarkRegisterSkillGroupDTO = (from y in x.SkillGroups
                                                                        select new MarkRegisterSkillGroupDTO
                                                                        {
                                                                            SkillGroupMasterID = model.SkillGroupID,
                                                                            SubjectID = model.SubjectID,
                                                                            MarkRegisterSkillsDTO = (from z in y.Skills
                                                                                                     select new MarkRegisterSkillsDTO
                                                                                                     {
                                                                                                         SkillGroupMasterID = model.SkillGroupID,
                                                                                                         SkillMasterID = z.SkillID,
                                                                                                         MarksGradeMapID = String.IsNullOrEmpty(z.GradeID) ? null : (byte.Parse(z.GradeID) as byte?),
                                                                                                         MarkRegisterSkillGroupID = z.MarkRegisterSkillGroupID ?? 0,
                                                                                                         MarkRegisterSkillIID = z.MarkRegisterSkillID ?? 0,
                                                                                                         MarkRegisterID = z.MarkRegisterID ?? 0,
                                                                                                         MarksObtained = z.MarksObtained

                                                                                                     }).ToList()
                                                                        }).ToList()
                                       }).ToList();
                if (markRegisterDetails.Count > 0)
                {
                    rtnMsg = ClientFactory.SchoolServiceClient(CallContext).SaveScholasticInternalEntry(new MarkRegisterDTO()
                    {
                        ClassID = model.ClassID,
                        AcademicYearID = model.AcademicYearID,
                        SectionID = model.SectionId,
                        ExamID = model.ExamID,
                        ExamGroupID = model.ExamGroupID,
                        SkillGroupID = model.SkillGroupID,
                        SkillSetID = model.SkillSetID,
                        SkillID = model.SkillID,
                        SubjectID = model.SubjectID,
                        MarkRegistersDetails = markRegisterDetails

                    });
                }
                else
                {
                    rtnMsg = "0#Please enter mark details for at least a student!";
                }
            }
            string successMsg = "Saved successfully!";
            dynamic response = new { IsError = false, Message = successMsg };
            if (!string.IsNullOrEmpty(rtnMsg))
            {

                string[] resp = rtnMsg.Split('#');
                response = new { IsError = (resp[0] == "0"), Message = (resp.Length > 1 ? resp[1] : successMsg) };
            }
            else
            {
                response = new { IsError = true, Message = "Something went wrong!" };
            }
            //}
            //return View();
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        //End Save MarkList

        public ActionResult GetScholasticInternalEntry(int classID, int sectionId, long examID, long termID, int skillGroupID, long skillSetID,
     int academicYearID, int? skillID, short? languageTypeID, int? subjectMapID, long subjectID)
        {
            var entry = ClientFactory.SchoolServiceClient(CallContext).GetScholasticInternalEntry(
                new MarkEntrySearchArgsDTO
                {
                    ClassID = classID,
                    SectionID = sectionId,
                    ExamID = examID,
                    TermID = termID,
                    SkillGroupID = skillGroupID,
                    SkillSetID = skillSetID,
                    AcademicYearID = academicYearID,
                    SkillID = skillID,
                    LanguageTypeID = languageTypeID,
                    SubjectMapID = subjectMapID,
                    SubjectId = subjectID
                });

            var response = new MarksEntryViewModel
            {
                Students = (from s in entry
                            select new StudentCoScholasticEntryViewModel
                            {
                                EnrolNo = s.AdmissionNumber,
                                StudentIID = s.StudentID.Value,
                                StudentName = s.Student.Value,
                                MarkStatusID = Convert.ToString(s.MarkStatusID),
                                SkillGroups = (from sg in s.MarkRegisterSkillGroupDTO
                                               select new SkillGroupCoScholasticEntryViewModel
                                               {
                                                   SkillGroupID = sg.SkillGroupMasterID.Value,
                                                   Skills = (from sk in sg.MarkRegisterSkillsDTO
                                                             select new SkillCoScholasticEntryViewModel
                                                             {
                                                                 SkillID = sk.SkillMasterID,
                                                                 SkillName = sk.Skill,
                                                                 GradeID = Convert.ToString(sk.MarksGradeMapID),
                                                                 MarkRegisterSkillGroupID = sk.MarkRegisterSkillGroupID,
                                                                 MarkRegisterSkillID = sk.MarkRegisterSkillIID,
                                                                 MarkRegisterID = sk.MarkRegisterID,
                                                                 MarkGradeMapList = sk.MarkGradeMapList,
                                                                 GradeMarkRangeList = sk.GradeMarkRangeList,
                                                                 MarksObtained = sk.MarksObtained,
                                                                 MaxMark = sk.MaximumMark,
                                                                 ConvertionFactor = sk.ConvertionFactor,
                                                                 TotalPercentage = sk.TotalPercentage,
                                                                 TotalMark = sk.TotalMark,
                                                             }
                                                            ).ToList(),
                                               }).ToList()

                            }).ToList()
            };


            return Json(response, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetMarkEntry(int classID, int sectionId, long termID, long subjectId, long examID, int academicYearID
            , short? languageTypeID, int? subjectMapID)
        {
            List<StudentMarkEntryViewModel> studEntry = new List<StudentMarkEntryViewModel>();
            //studEntry.Add(new StudentMarkEntryViewModel
            //{
            //    EnrolNo = "001",
            //    StudentID = 1,
            //    StudentName = "Aditi",
            //    PresentStatusID = 1,
            //    MarksObtained = 50,
            //    TotalPercentage = 75,
            //    TotalMark = 100,
            //    GradeID = 1,
            //});
            //var response = new MarksEntryViewModel
            //{
            //    MarkEntryStudents = studEntry
            //};
            var entry = ClientFactory.SchoolServiceClient(CallContext).GetMarkEntry(
                new MarkEntrySearchArgsDTO
                {
                    ClassID = classID,
                    SectionID = sectionId,
                    TermID = termID,
                    SubjectId = subjectId,
                    ExamID = examID,
                    AcademicYearID = academicYearID,
                    LanguageTypeID = languageTypeID,
                    SubjectMapID = subjectMapID
                });

            var response = new MarksEntryViewModel
            {
                MarkEntryStudents = (from s in entry
                                     select new StudentMarkEntryViewModel
                                     {
                                         EnrolNo = s.EnrolNo,
                                         StudentID = s.StudentID,
                                         StudentName = s.StudentName,
                                         MarkStatusID = Convert.ToString(s.PresentStatusID),
                                         MarksObtained = s.MarksObtained,
                                         MaxMark = s.MaxMark,
                                         MarkConvertionFactor = s.MarkConvertionFactor,
                                         TotalPercentage = s.TotalPercentage,
                                         TotalMark = s.TotalMark,
                                         GradeID = Convert.ToString(s.GradeID),
                                         MarkRegisterID = s.MarkRegisterID,
                                         MarkRegisterSubjectMapID = s.MarkRegisterSubjectMapID,

                                     }).ToList()
            };


            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveMarkEntry(MarksEntryViewModel model)
        {
            string rtnMsg = "0#Something went wrong!";

            if (model.MarkEntryStudents.Count == 0)
            { rtnMsg = "0#Please enter mark details for at least a student!"; }
            else
            {
                var studentMarkEntryList = (from s in model.MarkEntryStudents
                                            where (s.MarkStatusID != null || (s.MarksObtained == 0 || (s.MarksObtained != null && s.GradeID != null || s.MarkRegisterSubjectMapID != null)))
                                            select new StudentMarkEntryDTO
                                            {

                                                EnrolNo = s.EnrolNo,
                                                StudentID = s.StudentID,
                                                StudentName = s.StudentName,
                                                SubjectID = model.SubjectID,
                                                PresentStatusID = String.IsNullOrEmpty(s.MarkStatusID) ? null : (byte.Parse(s.MarkStatusID) as byte?),
                                                MarksObtained = s.MarksObtained,
                                                MaxMark = s.MaxMark,
                                                TotalPercentage = s.TotalPercentage,
                                                MarkRegisterID = s.MarkRegisterID,
                                                MarkRegisterSubjectMapID = s.MarkRegisterSubjectMapID,
                                                TotalMark = s.TotalMark.HasValue ? s.TotalMark.Value : 0,
                                                MarkConvertionFactor = s.MarkConvertionFactor,
                                                GradeID = String.IsNullOrEmpty(s.GradeID) ? null : (long.Parse(s.GradeID) as long?),
                                            }).ToList();
                if (studentMarkEntryList.Count > 0)
                {
                    rtnMsg = ClientFactory.SchoolServiceClient(CallContext).SaveMarkEntry(new MarkRegisterDTO()
                    {
                        ClassID = model.ClassID,
                        AcademicYearID = model.AcademicYearID,
                        SectionID = model.SectionId,
                        ExamGroupID = model.ExamGroupID,
                        ExamID = model.ExamID,
                        SubjectID = model.SubjectID,
                        StudentMarkEntryList = studentMarkEntryList

                    });
                }
                else
                {
                    rtnMsg = "0#Please enter mark details for at least a student!";
                }
            }
            string successMsg = "Saved successfully!";
            dynamic response = new { IsError = false, Message = successMsg };
            if (!string.IsNullOrEmpty(rtnMsg))
            {

                string[] resp = rtnMsg.Split('#');
                response = new { IsError = (resp[0] == "0"), Message = (resp.Length > 1 ? resp[1] : successMsg) };
            }
            else
            {
                response = new { IsError = true, Message = "Something went wrong!" };
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSubjectsAndMarksToPublish(int classID, int sectionID, long termID, long examID, int academicYearID)
        {
            List<StudentMarkEntryViewModel> studEntry = new List<StudentMarkEntryViewModel>();

            var entry = ClientFactory.SchoolServiceClient(CallContext).GetSubjectsAndMarksToPublish(
                new MarkEntrySearchArgsDTO { ClassID = classID, SectionID = sectionID, TermID = termID, ExamID = examID, AcademicYearID = academicYearID });


            var approvalResult = (from s in entry
                            select new MarkApprovalViewModel
                            {
                                ClassID = s.ClassID,
                                SectionID = s.SectionID,
                                EnrollNumber = s.AdmissionNumber,
                                StudentID = s.StudentID,
                                IsSelected = false,
                                MarkEntryStatusID = s.MarkEntryStatusID,
                                MarkEntryStatus = string.IsNullOrEmpty(s.MarkEntryStatusName) ? new KeyValueViewModel() : new KeyValueViewModel() { Key = s.MarkEntryStatusID.ToString(), Value = s.MarkEntryStatusName },
                                PreviousMarkEntryStatusID = s.MarkEntryStatusID,
                                Student = s.Student != null ? new KeyValueViewModel() { Key = s.Student.Key, Value = s.Student.Value } : new KeyValueViewModel(),
                                StudentFeeDefaulterStatus = s.FeeDefaulterStatus,
                                StudentMarkList = (from sg in s.MarkRegisterSplitDTO
                                                   select new MarkApprovalMarkListViewModel
                                                   {
                                                       MarksObtained = sg.Mark,
                                                       SubjectID = sg.SubjectID,
                                                       SubjectName = sg.Subject,
                                                       MarkRegisterID = sg.MarkRegisterID,
                                                       MarkRegisterSubjectMapID = sg.MarkRegisterSubjectMapIID,
                                                       MarksGradeMapID = sg.MarksGradeMapID,
                                                       MarkEntryStatusID = sg.MarkEntryStatusID,
                                                       MarkEntryStatus = string.IsNullOrEmpty(sg.MarkEntryStatusName) ? new KeyValueViewModel() : new KeyValueViewModel() { Key = sg.MarkEntryStatusID.ToString(), Value = sg.MarkEntryStatusName },
                                                       SkillGroup = (from sk in sg.MarkRegisterSkillGroupDTO
                                                                     select new MarkListSkillGroupViewModel
                                                                     {
                                                                         MarkRegisterSkillGroupIID = sk.MarkRegisterSkillGroupIID,
                                                                         MarkRegisterSubjectMapID = sk.MarkRegisterSubjectMapID,
                                                                         SkillGroupMasterID = sk.SkillGroupMasterID,
                                                                         SkillGroup = sk.SkillGroup,
                                                                         MarkEntryStatusID = sk.MarkEntryStatusID,
                                                                         MarkEntryStatus = string.IsNullOrEmpty(sk.MarkEntryStatusName) ? new KeyValueViewModel() : new KeyValueViewModel() { Key = sk.MarkEntryStatusID.ToString(), Value = sk.MarkEntryStatusName },
                                                                         Skills = (from mk in sk.MarkRegisterSkillsDTO
                                                                                   select new MarkListSkillGroupSkillViewModel
                                                                                   {
                                                                                       SkillGroupMasterID = mk.SkillGroupMasterID,
                                                                                       SkillGroup = mk.SkillGroup,
                                                                                       SkillMasterID = mk.SkillMasterID,
                                                                                       Skill = mk.Skill,
                                                                                       MarkEntryStatusID = mk.MarkEntryStatusID,
                                                                                       MarkEntryStatus = string.IsNullOrEmpty(mk.MarkEntryStatusName) ? new KeyValueViewModel() : new KeyValueViewModel() { Key = mk.MarkEntryStatusID.ToString(), Value = mk.MarkEntryStatusName },
                                                                                       MarksObtained = mk.MarksObtained,
                                                                                       MarksGradeMapID = mk.MarksGradeMapID,
                                                                                       MarkGradeMap = mk.MarkGradeMap,
                                                                                       MarkRegisterSkillGroupID = mk.MarkRegisterSkillGroupID,
                                                                                       MarkRegisterSkillIID = mk.MarkRegisterSkillIID,
                                                                                       MarkRegisterID = mk.MarkRegisterID,
                                                                                   }).ToList(),
                                                                     }).ToList(),
                                                   }).ToList(),

                                StudentSkillList = (from sm in s.MarkRegisterSkillGroupDTO
                                                    select new MarkApprovalSkillListViewModel
                                                    {
                                                        MarkRegisterSkillGroupIID = sm.MarkRegisterSkillGroupIID,
                                                        MarkRegisterSubjectMapID = sm.MarkRegisterSubjectMapID,
                                                        SkillGroupMasterID = sm.SkillGroupMasterID,
                                                        SkillGroup = sm.SkillGroup,
                                                        MarkEntryStatusID = sm.MarkEntryStatusID,
                                                        MarkEntryStatus = string.IsNullOrEmpty(sm.MarkEntryStatusName) ? new KeyValueViewModel() : new KeyValueViewModel() { Key = sm.MarkEntryStatusID.ToString(), Value = sm.MarkEntryStatusName },
                                                        SkillList = (from smk in sm.MarkRegisterSkillsDTO
                                                                     select new MarkListSkillGroupSkillViewModel
                                                                     {
                                                                         SkillGroupMasterID = smk.SkillGroupMasterID,
                                                                         SkillGroup = smk.SkillGroup,
                                                                         SkillMasterID = smk.SkillMasterID,
                                                                         Skill = smk.Skill,
                                                                         MarkEntryStatusID = smk.MarkEntryStatusID,
                                                                         MarkEntryStatus = string.IsNullOrEmpty(smk.MarkEntryStatusName) ? new KeyValueViewModel() : new KeyValueViewModel() { Key = smk.MarkEntryStatusID.ToString(), Value = smk.MarkEntryStatusName },
                                                                         MarksObtained = smk.MarksObtained,
                                                                         MarksGradeMapID = smk.MarksGradeMapID,
                                                                         MarkGradeMap = smk.MarkGradeMap,
                                                                         MarkRegisterSkillGroupID = smk.MarkRegisterSkillGroupID,
                                                                         MarkRegisterSkillIID = smk.MarkRegisterSkillIID,
                                                                         MarkRegisterID = smk.MarkRegisterID,
                                                                     }).ToList(),
                                                    }).ToList(),
                            }).ToList();

            var result = Json(approvalResult, JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = int.MaxValue;

            return result;
        }

        [HttpPost]
        public ActionResult UpdateMarkEntryStatus(MarkApprovalViewModel model)
        {
            var markRegisterSplit = new List<MarkRegisterDetailsSplitDTO>();
            var skillGroupList = new List<MarkRegisterSkillGroupDTO>();
            var studentEntryDetails = new List<MarkRegisterDetailsDTO>();
            var studentMarkList = new List<StudentMarkEntryDTO>();

            if (model.StudentMarkList != null)
            {
                markRegisterSplit = (from split in model.StudentMarkList
                                     where (split.MarkEntryStatusID != null || split.MarkRegisterSubjectMapID != null)
                                     select new MarkRegisterDetailsSplitDTO
                                     {
                                         Mark = split.MarksObtained,
                                         MarkRegisterID = split.MarkRegisterID,
                                         MarksGradeMapID = split.MarksGradeMapID,
                                         MarkRegisterSubjectMapIID = split.MarkRegisterSubjectMapID.Value,
                                         SubjectID = split.SubjectID,
                                         Subject = split.SubjectName,
                                         MarkEntryStatusID = split.MarkEntryStatusID,
                                         //MarkRegisterSkillGroupDTO = markRegisterSkill,
                                     }).ToList();
            }

            if (model.StudentSkillList != null)
            {
                skillGroupList = (from skillGroup in model.StudentSkillList
                                  where (skillGroup.MarkEntryStatusID != null)
                                  select new MarkRegisterSkillGroupDTO
                                  {
                                      StudentID = skillGroup.StudentID,
                                      StudentName = skillGroup.StudentName,
                                      SkillGroupMasterID = skillGroup.SkillGroupMasterID,
                                      SkillGroup = skillGroup.SkillGroup,
                                      MarkEntryStatusID = skillGroup.MarkEntryStatusID,
                                      MarkRegisterSkillsDTO = (from skill in skillGroup.SkillList
                                                               select new MarkRegisterSkillsDTO
                                                               {
                                                                   SkillGroupMasterID = skill.SkillGroupMasterID,
                                                                   SkillGroup = skill.SkillGroup,
                                                                   SkillMasterID = skill.SkillMasterID,
                                                                   Skill = skill.Skill,
                                                                   MarkEntryStatusID = skillGroup.MarkEntryStatusID,
                                                                   MarksObtained = skill.MarksObtained,
                                                                   MarksGradeMapID = skill.MarksGradeMapID,
                                                                   MarkGradeMap = skill.MarkGradeMap,
                                                                   MarkRegisterSkillGroupID = skill.MarkRegisterSkillGroupID,
                                                                   MarkRegisterSkillIID = skill.MarkRegisterSkillIID,
                                                                   MarkRegisterID = skill.MarkRegisterID,
                                                               }).ToList(),
                                  }).ToList();
            }

            if (model.StudentMarkList != null)
            {
                studentEntryDetails = (from s in model.StudentMarkList
                                       where (s.MarkEntryStatusID != null || s.MarkRegisterSubjectMapID != null)
                                       select new MarkRegisterDetailsDTO
                                       {
                                           StudentID = s.StudentID,
                                           Student = s.StudentID != 0 ? new KeyValueDTO()
                                           {
                                               Key = s.StudentID.ToString(),
                                               Value = s.StudentName
                                           } : new KeyValueDTO(),
                                           ClassID = model.ClassID,
                                           SectionID = model.SectionID,
                                           AdmissionNumber = s.EnrollNumber,
                                           IsSelected = s.IsSelected,
                                           MarkEntryStatusID = s.MarkEntryStatusID,
                                           MarkStatusID = String.IsNullOrEmpty(s.PresentStatusID) ? null : (byte.Parse(s.PresentStatusID) as byte?),
                                           MarkRegisterID = s.MarkRegisterID,
                                           MarkRegisterSplitDTO = markRegisterSplit,
                                           MarkRegisterSkillGroupDTO = skillGroupList,
                                       }).ToList();

                studentMarkList = (from s in model.StudentMarkList
                                   where (s.MarkEntryStatusID != null || s.MarkRegisterSubjectMapID != null)
                                   select new StudentMarkEntryDTO
                                   {
                                       EnrolNo = s.EnrollNumber,
                                       StudentID = s.StudentID,
                                       StudentName = s.StudentName,
                                       SubjectID = s.SubjectID,
                                       PresentStatusID = String.IsNullOrEmpty(s.PresentStatusID) ? null : (byte.Parse(s.PresentStatusID) as byte?),
                                       MarksObtained = s.MarksObtained,
                                       MaxMark = s.MaxMark,
                                       TotalPercentage = s.TotalPercentage,
                                       MarkRegisterID = s.MarkRegisterID,
                                       MarkRegisterSubjectMapID = s.MarkRegisterSubjectMapID,
                                       //TotalMark = s.TotalMark,
                                       GradeID = String.IsNullOrEmpty(s.GradeID) ? null : (long.Parse(s.GradeID) as long?),
                                       MarkEntryStatusID = s.MarkEntryStatusID
                                   }).ToList();
            }

            if (studentMarkList.Count > 0 || skillGroupList.Count > 0)
            {
                var markRegDTO = new MarkRegisterDTO()
                {
                    SchoolID = model.SchoolID.HasValue ? model.SchoolID : CallContext.SchoolID.HasValue ? (byte?)CallContext.SchoolID : null,
                    AcademicYearID = model.AcademicYearID,
                    ClassID = model.ClassID,
                    Class = model.ClassID.HasValue ? new KeyValueDTO()
                    {
                        Key = model.Class?.Key,
                        Value = model.Class?.Value
                    } : new KeyValueDTO(),
                    SectionID = model.SectionID,
                    ExamGroupID = model.ExamGroupID,
                    ExamID = model.ExamID,
                    MarkRegistersDetails = studentEntryDetails,
                    StudentMarkEntryList = studentMarkList,
                    MarkRegisterSkillGroupDTOs = skillGroupList,
                };

                var updateStatus = ClientFactory.SchoolServiceClient(CallContext).UpdateMarkEntryStatus(markRegDTO);

                GenerateAndSaveProgressReports(markRegDTO);

                return Json(new { IsError = false, Response = "Successfully saved" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var message = "Please select at least one entry before submitting mark details.";
                return Json(new { IsError = true, Response = message }, JsonRequestBehavior.AllowGet);
            }         
        }
        #endregion MARK ENTRY

        public ActionResult GetStudentFromApplicationID(long applicationID)
        {
            var data = ClientFactory.FrameworkServiceClient(CallContext).GetScreenData((int)Screens.StudentApplication, applicationID);
            var viewVm = new StudentApplicationViewModel();
            viewVm = viewVm.ToVM(viewVm.ToDTO(data)) as StudentApplicationViewModel;
            var studentVM = new StudentViewModel();
            return Json(studentVM.FromStudentApplicationVM(viewVm), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetStudentApplicationIDforCandidate(long applicationID)
        {
            var data = ClientFactory.FrameworkServiceClient(CallContext).GetScreenData((int)Screens.StudentApplication, applicationID);
            var viewVm = new StudentApplicationViewModel();
            viewVm = viewVm.ToVM(viewVm.ToDTO(data)) as StudentApplicationViewModel;
            var candidateVM = new CandidateViewModel();
            return Json(candidateVM.FromStudentApplicationVM(viewVm), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLeadDataFromLeadID(long leadID)
        {
            var data = ClientFactory.FrameworkServiceClient(CallContext).GetScreenData((int)Screens.Lead, leadID);
            var viewVm = new LeadViewModel();
            viewVm = viewVm.ToVM(viewVm.ToDTO(data)) as LeadViewModel;
            var studentApplicationVM = new StudentApplicationViewModel();
            return Json(studentApplicationVM.FromLeadApplicationVM(viewVm), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetParentDetailFromParentID(long parentID)
        {
            var dateFormat = Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue("DateFormat");
            var parent = ClientFactory.SchoolServiceClient(CallContext).GetParentDetailFromParentID(parentID);
            var data = new GaurdianViewModel()
            {
                #region Data pass to viewModel
                ParentCode = parent.ParentCode,
                FatherFirstName = parent.FatherFirstName,
                FatherMiddleName = parent.FatherMiddleName,
                FatherLastName = parent.FatherLastName,
                FatherCompanyName = parent.FatherCompanyName,
                FatherOccupation = parent.FatherOccupation,
                FatherPhoneNumber = parent.PhoneNumber,
                FatherMobileNumberTwo = parent.FatherMobileNumberTwo,
                FatherEmailID = parent.FatherEmailID,
                FatherPassportNumber = parent.FatherPassportNumber,
                FatherCountryofIssueID = parent.FatherPassportCountryofIssueID == null ? (int?)null : parent.FatherPassportCountryofIssueID,
                FatherCountryofIssue = parent.FatherPassportCountryofIssueID.ToString(),
                FatherPassportNoIssueString = parent.FatherPassportNoIssueDate.HasValue ? parent.FatherPassportNoIssueDate.Value.ToString(dateFormat) : null,
                FatherPassportNoExpiryString = parent.FatherPassportNoExpiryDate.HasValue ? parent.FatherPassportNoExpiryDate.Value.ToString(dateFormat) : null,
                FatherNationalID = parent.FatherNationalID,
                FatherNationalDNoIssueDateString = parent.FatherNationalDNoIssueDate.HasValue ? parent.FatherNationalDNoIssueDate.Value.ToString(dateFormat) : null,
                FatherNationalDNoExpiryDateString = parent.FatherNationalDNoExpiryDate.HasValue ? parent.FatherNationalDNoExpiryDate.Value.ToString(dateFormat) : null,
                FatherCountryID = parent.FatherCountryID == null ? (int?)null : parent.FatherCountryID,
                FatherCountry = parent.FatherCountryID.ToString(),
                CanYouVolunteerToHelpOneString = parent.CanYouVolunteerToHelpOneID.ToString(),
                CanYouVolunteerToHelpOneID = parent.CanYouVolunteerToHelpOneID == null ? (byte?)null : parent.CanYouVolunteerToHelpOneID,
                //Guardian Details ToVm
                GuardianFirstName = parent.GuardianFirstName,
                GuardianMiddleName = parent.GuardianMiddleName,
                GuardianLastName = parent.GuardianLastName,
                GuardianType = parent.GuardianTypeID.HasValue ? parent.GuardianTypeID.ToString() : null,
                GuardianOccupation = parent.GuardianOccupation,
                GuardianCompanyName = parent.GuardianCompanyName,
                GuardianPhone = parent.GuardianPhone,
                GaurdianEmail = parent.GaurdianEmail,
                GuardianNationalityID = parent.GuardianNationalityID == null ? (int?)null : parent.GuardianNationalityID,
                GuardianNationality = parent.GuardianNationalityID.ToString(),
                GuardianNationalID = parent.GuardianNationalID,
                GuardianNationalIDNoIssueDateString = parent.GuardianNationalIDNoIssueDate.HasValue ? parent.GuardianNationalIDNoIssueDate.Value.ToString(dateFormat) : null,
                GuardianNationalIDNoExpiryDateString = parent.GuardianNationalIDNoExpiryDate.HasValue ? parent.GuardianNationalIDNoExpiryDate.Value.ToString(dateFormat) : null,
                GuardianPassportNumber = parent.GuardianPassportNumber,
                GuardianCountryofIssueID = parent.GuardianCountryofIssueID == null ? (int?)null : parent.GuardianCountryofIssueID,
                GuardianCountryofIssue = parent.GuardianCountryofIssueID.ToString(),
                GuardianPassportNoIssueString = parent.GuardianPassportNoIssueDate.HasValue ? parent.GuardianPassportNoIssueDate.Value.ToString(dateFormat) : null,
                GuardianPassportNoExpiryString = parent.GuardianPassportNoExpiryDate.HasValue ? parent.GuardianPassportNoExpiryDate.Value.ToString(dateFormat) : null,
                MotherFirstName = parent.MotherFirstName,
                MotherMiddleName = parent.MotherMiddleName,
                MotherLastName = parent.MotherLastName,
                MotherCompanyName = parent.MotherCompanyName,
                MotherOccupation = parent.MotherOccupation,
                MotherPhone = parent.MotherPhone,
                MotherEmailID = parent.MotherEmailID,

                MotherPassportNumber = parent.MotherPassportNumber,
                MotherCountryofIssueID = parent.MotherPassportCountryofIssueID == null ? (int?)null : parent.MotherPassportCountryofIssueID,
                MotherCountryofIssue = parent.MotherPassportCountryofIssueID.ToString(),
                MotherPassportNoIssueString = parent.MotherPassportNoIssueDate.HasValue ? parent.MotherPassportNoIssueDate.Value.ToString(dateFormat) : null,
                MotherPassportNoExpiryString = parent.MotherPassportNoExpiryDate.HasValue ? parent.MotherPassportNoExpiryDate.Value.ToString(dateFormat) : null,

                MotherNationalID = parent.MotherNationalID,
                MotherNationalDNoIssueDateString = parent.MotherNationalDNoIssueDate.HasValue ? parent.MotherNationalDNoIssueDate.Value.ToString(dateFormat) : null,
                MotherNationaIDNoExpiryDateString = parent.MotherNationalDNoExpiryDate.HasValue ? parent.MotherNationalDNoExpiryDate.Value.ToString(dateFormat) : null,

                MotherCountryID = parent.MotherCountryID == null ? (int?)null : parent.MotherCountryID,
                MotherCountry = parent.MotherCountryID.ToString(),

                ParentIID = parent.ParentIID,
                ParentStudentMapIID = parent.ParentStudentMapIID,

                BuildingNo = parent.BuildingNo,
                FlatNo = parent.FlatNo,
                StreetNo = parent.StreetNo,
                StreetName = parent.StreetName,
                LocationNo = parent.LocationNo,
                LocationName = parent.LocationName,
                ZipNo = parent.ZipNo,
                PostBoxNo = parent.PostBoxNo,
                City = parent.City,
                Country = parent.CountryID.HasValue ? parent.CountryID.ToString() : null,
                CanYouVolunteerToHelpTwoString = parent.CanYouVolunteerToHelpTwoID.ToString(),
                CanYouVolunteerToHelpTwoID = parent.CanYouVolunteerToHelpTwoID == null ? (byte?)null : parent.CanYouVolunteerToHelpTwoID,
                #endregion
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GeStudentFromStudentsID(long StudentID)
        {
            var data = ClientFactory.FrameworkServiceClient(CallContext).GetScreenData((int)Screens.Student, StudentID);
            var viewVm = new StudentViewModel();
            viewVm = viewVm.ToVM(data) as StudentViewModel;
            var feeCollectVM = new FeeCollectionViewModel();
            return Json(feeCollectVM.FromStudentDataFromStudentVM(viewVm), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCurrencyDetails()
        {
            var data = ClientFactory.SchoolServiceClient(CallContext).GetCurrencyDetails();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetStudentDetailFromStudentID(long StudentID)
        {
            var student = ClientFactory.SchoolServiceClient(CallContext).GetStudentDetailFromStudentID(StudentID);
            return Json(student, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEmployeeFromEmployeeID(long employeeID)
        {
            var employee = ClientFactory.SchoolServiceClient(CallContext).GetEmployeeFromEmployeeID(employeeID);
            return Json(employee, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchStudent(string classID, string sectionID)
        {
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetClassFees(int classId, long studentId)
        {
            List<StudentFeeDueDTO> data = ClientFactory.SchoolServiceClient(CallContext).FillFeeDue(classId, studentId);
            //var data = ClientFactory.FrameworkServiceClient(CallContext).GetScreenData((int)Screens.FeeMasterClassMaps, classFeeID);
            var feeCollection = new FeeMasterClassMapViewModel();
            //feeCollection = feeCollection.ToVM(feeCollection.ToDTO(data)) as FeeMasterClassMapViewModel;
            var dateFormat = Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue("DateFormat");
            var feeTypes = new List<FeeCollectionFeeTypeViewModel>();
            foreach (var dat in data)
            {
                var collectionFeeType = (from feeType in dat.FeeDueFeeTypeMap
                                         select new FeeCollectionFeeTypeViewModel()
                                         {
                                             InvoiceNo = dat.InvoiceNo,
                                             Amount = feeType.Amount,
                                             FeePeriodID = feeType.FeePeriodID,
                                             StudentFeeDueID = feeType.StudentFeeDueID,
                                             FeeDueFeeTypeMapsID = feeType.FeeDueFeeTypeMapsIID,
                                             FeeMaster = feeType.FeeMaster.Value,
                                             FeePeriod = feeType.FeePeriodID.HasValue ? feeType.FeePeriod.Value : null,
                                             FeeMasterID = int.Parse(feeType.FeeMaster.Key),
                                             InvoiceDateString = feeType.InvoiceDate == null ? "" : Convert.ToDateTime(feeType.InvoiceDate).ToString(dateFormat),
                                             //TaxAmount = feeType.TaxAmount,
                                             //TaxPercentage = feeType.TaxPercentage,
                                             //FeeMonthly = (from split in feeType.FeeDueMonthlySplit
                                             //              select new FeeAssignMonthlySplitViewModel()
                                             //              {
                                             //                  Amount = split.Amount,
                                             //                  MonthID = split.MonthID,
                                             //                  //TaxAmount = split.TaxAmount,
                                             //                  //TaxPercentage = split.TaxPercentage,
                                             //                  MonthName = split.MonthID == 0 ? null : new DateTime(2010, split.MonthID, 1).ToString("MMM")
                                             //              }).ToList(),
                                         });

                feeTypes.AddRange(collectionFeeType);
            }

            return Json(feeTypes, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetFeesByInvoiceNo(long studentFeeDueID)
        {
            List<StudentFeeDueDTO> data = ClientFactory.SchoolServiceClient(CallContext).GetFeesByInvoiceNo(studentFeeDueID);
            var feeCollection = new FeeCollectionViewModel();
            var dateFormat = Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue("DateFormat");
            var feeTypes = new List<FeeCollectionFeeTypeViewModel>();
            var feeFines = new List<FeeCollectionFineViewModel>();

            #region Code Created By Sudish 20210604 -> Allocate Fee Data to Fee Model
            data.All(w =>
            {
                feeCollection.FeeTypes = (from x in w.FeeDueFeeTypeMap
                                          where (x.FeeMasterID ?? 0) > 0  && ( x.Amount - (x.CreditNoteAmount ?? 0)- (x.CollectedAmount ?? 0))!=0
                                          select new FeeCollectionFeeTypeViewModel()
                                          {
                                              Amount = x.Amount ?? 0,
                                              Balance =0,
                                              NowPaying = x.Amount - (x.CollectedAmount ?? 0) - (x.CreditNoteAmount ?? 0),
                                              CreditNote = x.CreditNoteAmount,
                                              PrvCollect=x.CollectedAmount,
                                              CreditNoteFeeTypeMapID = x.CreditNoteFeeTypeMapID,
                                              FeeDueFeeTypeMapsID = x.FeeDueFeeTypeMapsIID,
                                              FeeMaster = x.FeeMaster.Value,
                                              FeeMasterID = x.FeeMasterID,
                                              FeeMonthly = (from n in x.FeeDueMonthlySplit
                                                            select new FeeAssignMonthlySplitViewModel()
                                                            {
                                                                Amount = n.Amount,
                                                                Balance = 0,
                                                                NowPaying = n.Amount - (n.CollectedAmount ?? 0)- (n.CreditNoteAmount ?? 0),
                                                                PrvCollect=n.CollectedAmount??0,
                                                                CreditNote = n.CreditNoteAmount??0,
                                                                CreditNoteFeeTypeMapID = n.CreditNoteFeeTypeMapID,
                                                                FeeDueMonthlySplitID = n.FeeDueMonthlySplitIID,
                                                                IsRowSelected = true,
                                                                MonthID = n.MonthID,
                                                                MonthName = n.MonthID == 0 ? null : new DateTime(2010, n.MonthID, 1).ToString("MMM") + " " + n.Year,
                                                                Year = n.Year
                                                            }).ToList(),
                                              FeePeriod = x.FeePeriod.Value,
                                              FeePeriodID = x.FeePeriodID,
                                              FeeStructureFeeMapID = x.FeeStructureFeeMapID,
                                              InvoiceDate = x.InvoiceDate,
                                              InvoiceDateString = x.InvoiceDate == null ? "" : Convert.ToDateTime(x.InvoiceDate).ToString(dateFormat),
                                              InvoiceNo = x.InvoiceNo,
                                              StudentFeeDueID = x.StudentFeeDueID
                                          }
                                            ).ToList();
                feeCollection.FeeFines = (from n in w.FeeFineMap
                                          where (n.FineMasterID ?? 0) > 0
                                          select new FeeCollectionFineViewModel()
                                          {
                                              Amount = n.Amount,
                                              FeeDueFeeTypeMapsID = n.FeeDueFeeTypeMapsID,
                                              Fine = n.FineName,
                                              FineMasterID = n.FineMasterID,
                                              FineMasterStudentMapID = n.FineMasterStudentMapID,
                                              InvoiceDate = n.InvoiceDate,
                                              InvoiceDateString = n.InvoiceDate == null ? "" : Convert.ToDateTime(n.InvoiceDate).ToString(dateFormat),
                                              InvoiceNo = n.InvoiceNo,
                                              StudentFeeDueID = n.StudentFeeDueID,
                                          }
                                           ).ToList();
                return true;
            });

            return Json(feeCollection, JsonRequestBehavior.AllowGet);
            #endregion

            foreach (var dat in data)
            {
                var collectionFeeType = (from feeType in dat.FeeDueFeeTypeMap
                                         where (feeType.FeeMasterID ?? 0) != 0
                                         select new FeeCollectionFeeTypeViewModel()
                                         {
                                             InvoiceNo = dat.InvoiceNo,
                                             Amount = feeType.Amount,
                                             CreditNote = feeType.CreditNoteAmount.HasValue ? feeType.CreditNoteAmount.Value : 0,
                                             Balance = feeType.Balance,
                                             FeePeriodID = feeType.FeePeriodID,
                                             CreditNoteFeeTypeMapID = feeType.CreditNoteFeeTypeMapID.HasValue ? feeType.CreditNoteFeeTypeMapID.Value : (long?)null,
                                             StudentFeeDueID = feeType.StudentFeeDueID,
                                             FeeStructureFeeMapID = feeType.FeeStructureFeeMapID,
                                             FeeDueFeeTypeMapsID = feeType.FeeDueFeeTypeMapsIID,
                                             FeeMaster = feeType.FeeMaster.Value,
                                             FeePeriod = feeType.FeePeriodID.HasValue ? feeType.FeePeriod.Value : null,
                                             FeeMasterID = int.Parse(feeType.FeeMaster.Key),
                                             InvoiceDateString = feeType.InvoiceDate == null ? "" : Convert.ToDateTime(feeType.InvoiceDate).ToString(dateFormat),
                                             InvoiceDate = feeType.InvoiceDate == null ? (DateTime?)null : Convert.ToDateTime(feeType.InvoiceDate),
                                             FeeMonthly = (from split in feeType.FeeDueMonthlySplit
                                                           select new FeeAssignMonthlySplitViewModel()
                                                           {
                                                               Amount = split.Amount,
                                                               MonthID = split.MonthID,
                                                               Year = split.Year,
                                                               FeeDueMonthlySplitID = split.FeeDueMonthlySplitIID,
                                                               IsRowSelected = true,
                                                               MonthName = split.MonthID == 0 ? null : new DateTime(2010, split.MonthID, 1).ToString("MMM") + " " + split.Year,
                                                               CreditNote = split.CreditNoteAmount.HasValue ? split.CreditNoteAmount.Value : 0,
                                                               Balance = split.Balance,
                                                               CreditNoteFeeTypeMapID = split.CreditNoteFeeTypeMapID.HasValue ? split.CreditNoteFeeTypeMapID.Value : (long?)null
                                                           }).ToList(),
                                         });

                foreach (FeeCollectionFeeTypeViewModel feeType in collectionFeeType)
                {

                    feeType.CreditNote = feeType.FeeMonthly.Count > 0 ? feeType.FeeMonthly.Select(x => x.CreditNote).Sum() : feeType.CreditNote;
                    feeType.Balance = feeType.FeeMonthly.Count > 0 ? feeType.FeeMonthly.Select(x => x.Balance).Sum() : feeType.Balance;
                    feeTypes.Add(feeType);
                }


                var collectionFines = (from feeFine in dat.FeeFineMap
                                       select new FeeCollectionFineViewModel()
                                       {

                                           Amount = feeFine.Amount,
                                           Fine = feeFine.FineName,
                                           InvoiceNo = dat.InvoiceNo,
                                           FineMasterID = feeFine.FineMasterID,
                                           StudentFeeDueID = feeFine.StudentFeeDueID,
                                           FeeDueFeeTypeMapsID = feeFine.FeeDueFeeTypeMapsID,
                                           FineMasterStudentMapID = feeFine.FineMasterStudentMapID,
                                           InvoiceDateString = feeFine.InvoiceDate == null ? "" : Convert.ToDateTime(feeFine.InvoiceDate).ToString(dateFormat),
                                           InvoiceDate = feeFine.InvoiceDate == null ? (DateTime?)null : Convert.ToDateTime(feeFine.InvoiceDate)
                                       });

                feeFines.AddRange(collectionFines);

            }
            feeCollection.FeeTypes = feeTypes;
            feeCollection.FeeFines = feeFines;
            return Json(feeCollection, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetIssuedCreditNotesForCollectedFee(long studentId)
        {
            List<FeeCollectionPreviousFeesDTO> data = ClientFactory.SchoolServiceClient(CallContext).GetIssuedCreditNotesForCollectedFee(studentId);

            var feeCollection = new FeeCollectionViewModel();
            //feeCollection = feeCollection.ToVM(feeCollection.ToDTO(data)) as FeeMasterClassMapViewModel;
            var dateFormat = Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue("DateFormat");

            //var PreviousFees = new List<FeeCollectionPreviousFeesViewModel>();

            //foreach (var dat in data)
            //{
            //    var collectionFeeType = new FeeCollectionPreviousFeesViewModel()
            //    {
            //        InvoiceNo = dat.InvoiceNo,
            //        Amount = dat.Amount,
            //        CreditNote = dat.CreditNoteAmount.HasValue ? -dat.CreditNoteAmount.Value : 0,
            //        Balance = dat.Amount + (dat.CreditNoteAmount.HasValue ? dat.CreditNoteAmount.Value : 0),
            //        FeePeriodID = dat.FeePeriodID,
            //        StudentFeeDueID = dat.StudentFeeDueID,
            //        FeeDueFeeTypeMapsID = dat.FeeDueFeeTypeMapsID,
            //        FeeStructureFeeMapID = dat.FeeStructureFeeMapID,
            //        FeeMaster = dat.FeeMaster,
            //        FeePeriod = dat.FeePeriodID.HasValue ? dat.FeePeriod : null,
            //        FeeMasterID = dat.FeeMasterID,
            //        InvoiceDateString = dat.InvoiceDate == null ? "" : Convert.ToDateTime(dat.InvoiceDate).ToString(dateFormat),
            //        InvoiceDate = dat.InvoiceDate == null ? (DateTime?)null : Convert.ToDateTime(dat.InvoiceDate),
            //        FeeMonthly = (from split in dat.MontlySplitMaps
            //                      select new FeeAssignMonthlySplitViewModel()
            //                      {
            //                          Year = split.Year,                                      
            //                          IsRowSelected = true,
            //                          Amount = split.Amount,
            //                          MonthID = split.MonthID,
            //                          FeeDueMonthlySplitID = split.FeeDueMonthlySplitID,                                     
            //                          MonthName = split.MonthID == 0 ? null : new DateTime(2010, split.MonthID, 1).ToString("MMM"),
            //                          CreditNote = split.CreditNoteAmount.HasValue ? -split.CreditNoteAmount.Value : 0,
            //                          Balance = split.Amount + (split.CreditNoteAmount.HasValue ? split.CreditNoteAmount.Value : 0),
            //                      }).ToList(),
            //    };

            //    PreviousFees.Add(collectionFeeType);

            //}
            //feeCollection.PreviousFees = PreviousFees;

            return Json(feeCollection, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult FillFeeDueDataForSettlement(long studentId, int academicId)
        {
            var dateFormat = Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue("DateFormat");
            List<FeeCollectionDTO> data = ClientFactory.SchoolServiceClient(CallContext).FillFeeDueDataForSettlement(studentId, academicId);
            //DateTime settlementDate = new DateTime();
            var feeTypes = new List<FinalSettlementFeeTypeViewModel>();
            foreach (var dat in data)
            {
                var collectionFeeType = (from feeType in dat.FeeTypes
                                         select new FinalSettlementFeeTypeViewModel()
                                         {
                                             InvoiceNo = feeType.InvoiceNo,
                                             // FeeDueAmount = feeType.IsRefundable == false ? feeType.Amount : 0,
                                             FeeDueAmount = feeType.Amount,// feeType.IsRefundable == false ? feeType.Amount : 0,
                                             FeePeriodID = feeType.FeePeriodID,
                                             StudentFeeDueID = feeType.StudentFeeDueID,
                                             FeeDueFeeTypeMapsID = feeType.FeeDueFeeTypeMapsID,
                                             FeeCollectionFeeTypeMapsIID = feeType.FeeCollectionFeeTypeMapsIID,
                                             CollectedAmount = feeType.CollectedAmount,
                                             CreditNote = feeType.CreditNoteAmount,
                                             //PayableAmount = feeType.IsRefundable == false ? (feeType.Amount - feeType.CollectedAmount) : 0,
                                             PayableAmount = feeType.IsRefundable == false ? (feeType.CollectedAmount < (feeType.Amount - feeType.CreditNoteAmount) ?
                                             (feeType.Amount - feeType.CreditNoteAmount) - feeType.CollectedAmount : 0) : 0,
                                             FeeCollectionStatus = true,
                                             IsRefundable = feeType.IsRefundable, //feeType.IsRefundable,
                                             Refund = feeType.RefundAmount,
                                             Amount = feeType.ReceivableAmount - feeType.RefundAmount,// (feeType.IsRefundable == false ? (feeType.Amount - feeType.CollectedAmount) : 0) - (feeType.IsRefundable == true ? feeType.CollectedAmount : 0),
                                             FeeMaster = feeType.FeeMaster,
                                             FeePeriod = feeType.FeePeriodID.HasValue ? feeType.FeePeriod : null,
                                             FeeMasterID = feeType.FeeMasterID,
                                             InvoiceDateString = feeType.InvoiceDate == null ? "" : Convert.ToDateTime(feeType.InvoiceDate).ToString(dateFormat),
                                             InvoiceDate = feeType.InvoiceDate == null ? (DateTime?)null : Convert.ToDateTime(feeType.InvoiceDate),
                                             FeeDueMonthlyFinal = (from split in feeType.MontlySplitMaps
                                                                   select new FeeDueMonthlyFinalViewModel()
                                                                   {
                                                                       Amount = split.Amount,
                                                                       MonthID = split.MonthID,
                                                                       Year = split.Year,
                                                                       PrvCollect = split.CollectedAmount,
                                                                       CreditNote = split.CreditNoteAmount,
                                                                       Balance = (split.ReceivableAmount ?? 0) - (split.RefundAmount ?? 0),
                                                                       FeeDueMonthlySplitID = split.FeeDueMonthlySplitID,
                                                                       PayableAmount = split.ReceivableAmount,//feeType.IsRefundable == false ? ((split.CollectedAmount ?? 0) < ((split.Amount ?? 0) - (split.CreditNoteAmount ?? 0)) ? ((split.Amount ?? 0) - (split.CollectedAmount ?? 0) - (split.CreditNoteAmount ?? 0)) : 0) : 0,
                                                                       Refund = split.RefundAmount,//feeType.IsRefundable == true ? (split.CollectedAmount ?? 0) : ((split.CollectedAmount ?? 0) > ((split.Amount ?? 0) - (split.CreditNoteAmount ?? 0))) ? ((split.CollectedAmount ?? 0) - (split.Amount ?? 0) - (split.CreditNoteAmount ?? 0)) : 0,
                                                                       FeeCollectionStatus = split.CollectedAmount.HasValue ? true : false,
                                                                       MonthName = split.MonthID == 0 ? null : new DateTime(2010, split.MonthID, 1).ToString("MMM") + " " + split.Year
                                                                   }).ToList(),
                                         }).ToList();



                feeTypes.AddRange(collectionFeeType);
                // FinalSettlementFeeTypeViewModel
                //decimal refund = 0;
                //decimal payAmt = 0;
                //foreach (FeeDueFeeTypeMapDTO dataDto in data)
                //{
                //    foreach (FeeDueMonthlySplitDTO MonthlyDto in dataDto.FeeDueMonthlySplit)
                //    {
                //        if (settlementDate.Month >= MonthlyDto.MonthID && settlementDate.Year == MonthlyDto.Year)
                //        {
                //            payAmt = MonthlyDto.Amount.Value;

                //        }
                //        else
                //            refund

                //    }
                //}
            }


            return Json(feeTypes, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult FillPendingFees(int classId, long studentId)
        {
            List<StudentFeeDueDTO> data = ClientFactory.SchoolServiceClient(CallContext).FillPendingFees(classId, studentId);
            var feeCollection = new FeeMasterClassMapViewModel();
            var feeTypes = new List<FeeCollectionPendingInvoiceViewModel>();
            var dateFormat = Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue("DateFormat");

            foreach (var dat in data)
            {
                if ((dat.InvoiceAmount - (dat.CollectedAmount??0) -(dat.CreditNoteAmount ?? 0)) !=0)
                {
                    feeTypes.Add(new FeeCollectionPendingInvoiceViewModel()
                    {
                        InvoiceNo = dat.InvoiceNo,
                        Amount = dat.InvoiceAmount,
                        StudentFeeDueID = dat.StudentFeeDueIID,
                        FeePeriodID = dat.FeePeriodId,
                        InvoiceDate = dat.InvoiceDate == null ? "" : Convert.ToDateTime(dat.InvoiceDate).ToString(dateFormat),
                        Remarks = dat.Remarks,
                        IsExternal = dat.IsExternal,
                        FeeMasterID = dat.FeeMasterID,
                        CrDrAmount = dat.CreditNoteAmount,
                        CollAmount=dat.CollectedAmount,
                        Balance = dat.InvoiceAmount - (dat.CreditNoteAmount ?? 0) -(dat.CollectedAmount??0)
                    });
                }
            }

            return Json(feeTypes, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSplitUpPeriod(int periodID)
        {
            var data = ClientFactory.FrameworkServiceClient(CallContext).GetScreenData((int)Screens.FeePeriods, periodID);
            var periods = new List<FeeMonthlySplitViewModel>();
            var feePeriod = new FeePeriodsViewModel();
            feePeriod = feePeriod.ToVM(data) as FeePeriodsViewModel;

            for (int i = 0; i < feePeriod.NumberOfPeriods; i++)
            {
                periods.Add(new FeeMonthlySplitViewModel()
                {
                    MonthID = (feePeriod.PeriodFrom.Value.Month + i) > 12 ? ((feePeriod.PeriodFrom.Value.Month + i) - 12) : feePeriod.PeriodFrom.Value.Month + i,
                    MonthName = feePeriod.PeriodFrom.Value.AddMonths(i).ToString("MMM") + " " + feePeriod.PeriodFrom.Value.AddMonths(i).Date.Year,
                    Year = feePeriod.PeriodFrom.Value.AddMonths(i).Date.Year,
                });
            }

            return Json(periods, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSplitUpPeriodMonthYear(int periodID)
        {
            var data = ClientFactory.FrameworkServiceClient(CallContext).GetScreenData((int)Screens.FeePeriods, periodID);
            var creditNoteFeeType = new CreditNoteFeeTypeViewModel();
            var months = new List<KeyValueViewModel>();
            var years = new List<KeyValueViewModel>();
            creditNoteFeeType.MonthList = new List<KeyValueViewModel>();
            creditNoteFeeType.YearList = new List<KeyValueViewModel>();
            var feePeriod = new FeePeriodsViewModel();
            feePeriod = feePeriod.ToVM(data) as FeePeriodsViewModel;

            for (int i = 0; i < feePeriod.NumberOfPeriods; i++)
            {

                months.Add(new KeyValueViewModel { Key = ((feePeriod.PeriodFrom.Value.Month + i) > 12 ? ((feePeriod.PeriodFrom.Value.Month + i) - 12) : feePeriod.PeriodFrom.Value.Month + i).ToString(), Value = feePeriod.PeriodFrom.Value.AddMonths(i).ToString("MMM") });

            }
            years.Add(new KeyValueViewModel
            {
                Key = feePeriod.PeriodFrom.Value.Date.Year.ToString(),
                Value = feePeriod.PeriodFrom.Value.Date.Year.ToString()
            });
            if (feePeriod.PeriodFrom.Value.Date.Year != feePeriod.PeriodTo.Value.Date.Year)
            {
                years.Add(new KeyValueViewModel
                {
                    Key = feePeriod.PeriodTo.Value.Date.Year.ToString(),
                    Value = feePeriod.PeriodTo.Value.Date.Year.ToString()
                });
            }
            creditNoteFeeType.MonthList.AddRange(months);
            creditNoteFeeType.YearList.AddRange(years);
            return Json(creditNoteFeeType, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetClassStudents(int classId, int sectionId)
        {
            var listStudents = ClientFactory.SchoolServiceClient(CallContext).GetClassStudents(classId, sectionId);
            return Json(listStudents, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetClasswiseStudentData(int classId, int sectionId)
        {
            var listStudents = ClientFactory.SchoolServiceClient(CallContext).GetClasswiseStudentData(classId, sectionId);
            return Json(listStudents, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetClasswiseRemarksEntryStudentData(int classId, int sectionId, int examGroupID)
        {

            var listStudents = ClientFactory.SchoolServiceClient(CallContext).GetClasswiseRemarksEntryStudentData(classId, sectionId, examGroupID);
            var studList = new List<RemarksEntryStudentListViewModel>();

            foreach (var dat in listStudents)
            {

                studList.Add(new RemarksEntryStudentListViewModel()
                {
                    StudentID = dat.StudentID,
                    StudentName = dat.StudentName,
                    RemarksEntryStudentMapIID = dat.RemarksEntryStudentMapIID,
                    Remarks1 = dat.Remarks1,
                });
            }

            return Json(studList, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetHealthEntryStudentData(int classId, int sectionId, int academicYearID, int examGroupID)
        {

            var listStudents = ClientFactory.SchoolServiceClient(CallContext).GetHealthEntryStudentData(classId, sectionId, academicYearID, examGroupID);
            var studList = new List<HealthEntryStudentListViewModel>();

            foreach (var dat in listStudents)
            {

                studList.Add(new HealthEntryStudentListViewModel()
                {
                    StudentID = dat.StudentID,
                    HealthEntryStudentMapIID = dat.HealthEntryStudentMapIID,
                    StudentName = dat.StudentName,
                    Height = dat.Height,
                    Weight = dat.Weight,
                    BMS = dat.BMS,
                    Remarks = dat.Remarks,
                });
            }

            return Json(studList, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetStudentandStaffsByRouteIDforEvent(int routeID, string IsRouteType)
        {
            var listDatas = ClientFactory.SchoolServiceClient(CallContext).GetStudentandStaffsByRouteIDforEvent(routeID, IsRouteType);
            return Json(listDatas, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStudentTransportDetailsByStudentID(int studentID, string IsRouteType)
        {
            var data = ClientFactory.SchoolServiceClient(CallContext).GetStudentTransportDetailsByStudentID(studentID, IsRouteType);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStaffTransportDetailsByStaffID(int staffID, string IsRouteType)
        {
            var data = ClientFactory.SchoolServiceClient(CallContext).GetStaffTransportDetailsByStaffID(staffID, IsRouteType);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStudentRouteDetailsList(int routeId, int academicId)
        {
            var listStudents = ClientFactory.SchoolServiceClient(CallContext).GetStudentDatasFromRouteID(routeId, academicId);
            return Json(listStudents, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillClassWiseSubjects(int classID, int sectionID, int IID)
        {
            var listSubjects = ClientFactory.SchoolServiceClient(CallContext).FillClassandSectionWiseSubjects(classID, sectionID, IID);
            return Json(listSubjects, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStaffRouteDetailsList(int routeId, int academicId)
        {
            var listStaffs = ClientFactory.SchoolServiceClient(CallContext).GetStaffDatasFromRouteID(routeId, academicId);
            return Json(listStaffs, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveStudentFeeDue(FeeDueGenerationViewModel feeDueInfo)
        {
            string feeDueID = "0#Something went wrong!";
            var dateFormat = Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue("DateFormat");

            //if (!string.IsNullOrEmpty(feeDueInfo.Class.Key))
            //{
            List<KeyValueDTO> feePerioList = new List<KeyValueDTO>();
            if (feeDueInfo.FeePeriod.Any())
                feePerioList.AddRange((from n in feeDueInfo.FeePeriod select new KeyValueDTO() { Key = n.Key, Value = n.Value }));
            List<KeyValueDTO> studentList = new List<KeyValueDTO>();
            if (feeDueInfo.Student.Any())
                studentList.AddRange((from n in feeDueInfo.Student select new KeyValueDTO() { Key = n.Key, Value = n.Value }));
            List<KeyValueDTO> feeMasterList = new List<KeyValueDTO>();
            if (feeDueInfo.FeeMaster.Any())
                feeMasterList.AddRange((from n in feeDueInfo.FeeMaster select new KeyValueDTO() { Key = n.Key, Value = n.Value }));
            //foreach (KeyValueViewModel vm in feeDueInfo.FeePeriod)
            //{
            //    feePerioList.Add(new KeyValueDTO { Key = vm.Key, Value = vm.Value }
            //        );
            //}
            //foreach (KeyValueViewModel vm in feeDueInfo.Student)
            //{
            //    studentList.Add(new KeyValueDTO { Key = vm.Key, Value = vm.Value }
            //        );
            //}
            feeDueID = ClientFactory.SchoolServiceClient(CallContext).SaveStudentFeeDue(new Services.Contracts.School.Fees.StudentFeeDueDTO()
            {

                FeePeriod = feePerioList,
                Student = studentList,
                FeeMaster = feeMasterList,
                DueDate = string.IsNullOrEmpty(feeDueInfo.FeeDueGenerationDateString) ? (DateTime?)null : DateTime.ParseExact(feeDueInfo.FeeDueGenerationDateString, dateFormat, CultureInfo.InvariantCulture),
                InvoiceDate = string.IsNullOrEmpty(feeDueInfo.InvoiceDateString) ? (DateTime?)null : DateTime.ParseExact(feeDueInfo.InvoiceDateString, dateFormat, CultureInfo.InvariantCulture),
                ClassId = string.IsNullOrEmpty(feeDueInfo.Class.Key) ? (int?)null : int.Parse(feeDueInfo.Class.Key),
                SectionId = string.IsNullOrEmpty(feeDueInfo.Section.Key) ? (int?)null : int.Parse(feeDueInfo.Section.Key),
                AcadamicYearID = string.IsNullOrEmpty(feeDueInfo.Academic.Key) ? (int?)null : int.Parse(feeDueInfo.Academic.Key),
                FeeMasterAmount = feeDueInfo.Amount.HasValue ? feeDueInfo.Amount : 0,

            });

            //}
            string successMsg = "Fee invoice generated successfully";
            dynamic response = new { IsFailed = false, Message = successMsg };
            if (!string.IsNullOrEmpty(feeDueID))
            {

                string[] resp = feeDueID.Split('#');
                response = new { IsFailed = (resp[0] == "0"), Message = (resp.Length > 1 ? resp[1] : successMsg) };
            }
            else
            {
                response = new { IsFailed = true, Message = "There are no fee dues to be generated" };
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult GetPickUpBusSeatAvailabilty(long RouteStopMapId, int academicYearID)
        {
            StudentRouteStopMapDTO data = ClientFactory.SchoolServiceClient(CallContext).GetPickUpBusSeatAvailabilty(RouteStopMapId, academicYearID);
            StudentRouteStopMapViewModel viewModel = new StudentRouteStopMapViewModel();
            if (data != null)
            {
                viewModel = new StudentRouteStopMapViewModel()
                {
                    PickupStopMapID = data.PickupStopMapID,
                    PickupBusNumber = data.PickUpRouteCode,
                    PickupSeatAvailability = new PickupSeatAvailabilityViewModel()
                    {
                        AllowSeatCapacity = data.PickupSeatMap.AllowSeatCapacity == null ? 0 : data.PickupSeatMap.AllowSeatCapacity,
                        MaximumSeatCapacity = data.PickupSeatMap.MaximumSeatCapacity == null ? 0 : data.PickupSeatMap.MaximumSeatCapacity,
                        SeatOccupied = data.PickupSeatMap.SeatOccupied == null ? 0 : data.PickupSeatMap.SeatOccupied,
                        SeatAvailability = data.PickupSeatMap.SeatAvailability == null ? 0 : data.PickupSeatMap.SeatAvailability,

                    },
                };
            }
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetPickUpBusSeatStaffAvailabilty(long RouteStopMapId, int academicYearID)
        {
            StaffRouteStopMapDTO data = ClientFactory.SchoolServiceClient(CallContext).GetPickUpBusSeatStaffAvailabilty(RouteStopMapId, academicYearID);
            StudentRouteStopMapViewModel viewModel = new StudentRouteStopMapViewModel();
            if (data != null)
            {
                viewModel = new StudentRouteStopMapViewModel()
                {
                    PickupStopMapID = data.PickupStopMapID,
                    PickupBusNumber = data.PickUpRouteCode,
                    PickupSeatAvailability = new PickupSeatAvailabilityViewModel()
                    {
                        AllowSeatCapacity = data.PickupSeatMap.AllowSeatCapacity == null ? 0 : data.PickupSeatMap.AllowSeatCapacity,
                        MaximumSeatCapacity = data.PickupSeatMap.MaximumSeatCapacity == null ? 0 : data.PickupSeatMap.MaximumSeatCapacity,
                        SeatOccupied = data.PickupSeatMap.SeatOccupied == null ? 0 : data.PickupSeatMap.SeatOccupied,
                        SeatAvailability = data.PickupSeatMap.SeatAvailability == null ? 0 : data.PickupSeatMap.SeatAvailability,

                    },
                };
            }
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        //[HttpGet]
        public JsonResult GetFineAmount(int fineMasterID)
        {
            var productDet = ClientFactory.SchoolServiceClient(CallContext).GetFineAmount(fineMasterID);
            return Json(productDet, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetDropBusSeatAvailabilty(long RouteStopMapId, int academicYearID)
        {
            StudentRouteStopMapDTO data = ClientFactory.SchoolServiceClient(CallContext).GetDropBusSeatAvailabilty(RouteStopMapId, academicYearID);
            StudentRouteStopMapViewModel viewModel = new StudentRouteStopMapViewModel();
            if (data != null)
            {

                viewModel = new StudentRouteStopMapViewModel()
                {
                    DropStopMapID = data.DropStopMapID,
                    DropBusNumber = data.DropRouteCode,
                    DropSeatAvailability = new DropSeatAvailabilityViewModel()
                    {
                        AllowSeatCapacity = data.DropSeatMap.AllowSeatCapacity == null ? 0 : data.DropSeatMap.AllowSeatCapacity,
                        MaximumSeatCapacity = data.DropSeatMap.MaximumSeatCapacity == null ? 0 : data.DropSeatMap.MaximumSeatCapacity,
                        SeatOccupied = data.DropSeatMap.SeatOccupied == null ? 0 : data.DropSeatMap.SeatOccupied,
                        SeatAvailability = data.DropSeatMap.SeatAvailability == null ? 0 : data.DropSeatMap.SeatAvailability,

                    },
                };
            }
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetDropBusSeatStaffAvailabilty(long RouteStopMapId, int academicYearID)
        {
            StaffRouteStopMapDTO data = ClientFactory.SchoolServiceClient(CallContext).GetDropBusSeatStaffAvailabilty(RouteStopMapId, academicYearID);
            StudentRouteStopMapViewModel viewModel = new StudentRouteStopMapViewModel();
            if (data != null)
            {

                viewModel = new StudentRouteStopMapViewModel()
                {
                    DropStopMapID = data.DropStopMapID,
                    DropBusNumber = data.DropRouteCode,
                    DropSeatAvailability = new DropSeatAvailabilityViewModel()
                    {
                        AllowSeatCapacity = data.DropSeatMap.AllowSeatCapacity == null ? 0 : data.DropSeatMap.AllowSeatCapacity,
                        MaximumSeatCapacity = data.DropSeatMap.MaximumSeatCapacity == null ? 0 : data.DropSeatMap.MaximumSeatCapacity,
                        SeatOccupied = data.DropSeatMap.SeatOccupied == null ? 0 : data.DropSeatMap.SeatOccupied,
                        SeatAvailability = data.DropSeatMap.SeatAvailability == null ? 0 : data.DropSeatMap.SeatAvailability,

                    },
                };
            }
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetClassByExam(int examId)
        {
            var listStudents = ClientFactory.SchoolServiceClient(CallContext).GetClassByExam(examId);
            return Json(listStudents, JsonRequestBehavior.AllowGet);

        }

        public class errormsg
        {
            public bool status { get; set; }
            public string message { get; set; }
            public string Filed { get; set; }
        }

        [HttpPost]
        public JsonResult GetFeePeriodMonthlySplit(string[] feePeriodIds)
        {
            var _period_IDs = new List<int>();
            if (feePeriodIds != null && feePeriodIds.Any())
                _period_IDs = feePeriodIds.Select(w => int.Parse(w)).ToList();
            var lstPeriods = ClientFactory.SchoolServiceClient(CallContext).GetFeePeriodMonthlySplit(_period_IDs);
            var monthlySplits = new List<StudentRouteMonthlySplitViewModel>();
            foreach (FeePeriodsDTO lstprds in lstPeriods)
            {
                for (int i = 0; i < lstprds.NumberOfPeriods; i++)
                {
                    monthlySplits.Add(new StudentRouteMonthlySplitViewModel()
                    {
                        MonthID = (lstprds.PeriodFrom.Month + i) > 12 ? ((lstprds.PeriodFrom.Month + i) - 12) : lstprds.PeriodFrom.Month + i,
                        MonthName = lstprds.PeriodFrom.AddMonths(i).ToString("MMM") + " " + lstprds.PeriodFrom.AddMonths(i).Date.Year,
                        Year = lstprds.PeriodFrom.AddMonths(i).Date.Year,
                        FeePeriodID = lstprds.FeePeriodID

                    });
                }
            }
            return Json(monthlySplits, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult checkAcademicYearValidation(string fDate, string toDate)
        {
            var result = new errormsg();
            var dateFormat = Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue("DateFormat");
            var temServerDateStart = "01/04/2021";
            var temServerDateEnd = "31/03/2022";

            DateTime? ServerDateStart = string.IsNullOrEmpty(temServerDateStart) ? (DateTime?)null : DateTime.ParseExact(temServerDateStart, dateFormat, CultureInfo.InvariantCulture);
            DateTime? ServerDateEnd = string.IsNullOrEmpty(temServerDateEnd) ? (DateTime?)null : DateTime.ParseExact(temServerDateEnd, dateFormat, CultureInfo.InvariantCulture);
            if (toDate != null && fDate != "")
            {
                DateTime? fromDate = string.IsNullOrEmpty(fDate) ? (DateTime?)null : DateTime.ParseExact(fDate, dateFormat, CultureInfo.InvariantCulture);

                if (fromDate < ServerDateStart || fromDate > ServerDateEnd)
                {
                    result.status = false;
                    result.message = "Select Date Between 01/04/2021 and 31/03/2022";
                    result.Filed = "dateFrom";
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            if (toDate != null && toDate != "")
            {
                DateTime? dateTo = string.IsNullOrEmpty(toDate) ? (DateTime?)null : DateTime.ParseExact(toDate, dateFormat, CultureInfo.InvariantCulture);
                if (dateTo < ServerDateStart || dateTo > ServerDateEnd)
                {
                    result.status = false;
                    result.message = "Select Date Between 01/04/2021 and 31/03/2022";
                    result.Filed = "dateTo";
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

            }
            if (toDate != null && fDate != "" && toDate != null && toDate != "")
            {
                DateTime? fromDate = string.IsNullOrEmpty(fDate) ? (DateTime?)null : DateTime.ParseExact(fDate, dateFormat, CultureInfo.InvariantCulture);
                DateTime? dateTo = string.IsNullOrEmpty(toDate) ? (DateTime?)null : DateTime.ParseExact(toDate, dateFormat, CultureInfo.InvariantCulture);

                if (fromDate > dateTo)
                {
                    result.status = false;
                    result.message = "Date from must be less then Date to";
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            result.status = true;
            result.message = "Success";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //Get Accademic Year Months
        [HttpGet]
        public JsonResult GetAcademicYearMonthlyWise(string fDate, string toDate, int PickupID, int DropID)
        {

            string dateString = null;
            CultureInfo provider = CultureInfo.InvariantCulture;
            // It throws Argument null exception  
            var dateFormat = Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue("DateFormat");
            DateTime? fromDate = string.IsNullOrEmpty(fDate) ? (DateTime?)null : DateTime.ParseExact(fDate, dateFormat, CultureInfo.InvariantCulture);
            DateTime? dateTo = string.IsNullOrEmpty(toDate) ? (DateTime?)null : DateTime.ParseExact(toDate, dateFormat, CultureInfo.InvariantCulture);

            //DateTime fromDate = Convert.ToDateTime(fDate); //DateTime.ParseExact(fDate.ToString(), "mm/dd/yyyy", provider);
            //DateTime dateTo = Convert.ToDateTime(toDate); //DateTime.ParseExact(fDate.ToString(), "mm/dd/yyyy", provider);

            String[] dataResult = null;
            using (SqlConnection conn = new SqlConnection(ConfigurationExtensions.GetConnectionString("dbEduegateERPContext").ToString()))
            using (SqlCommand cmd = new SqlCommand("[schools].[SPS_ACADEMIC_YEAR_MONTH_WISE]", conn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@sDate", SqlDbType.DateTime));
                adapter.SelectCommand.Parameters["@sDate"].Value = fromDate;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@eDate", SqlDbType.DateTime));
                adapter.SelectCommand.Parameters["@eDate"].Value = dateTo;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@PickupID", SqlDbType.Int));
                adapter.SelectCommand.Parameters["@PickupID"].Value = PickupID;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@DropID", SqlDbType.Int));
                adapter.SelectCommand.Parameters["@DropID"].Value = DropID;

                DataSet dt = new DataSet();
                adapter.Fill(dt);
                DataTable dataTable = null;

                if (dt.Tables.Count > 0)
                {
                    if (dt.Tables[0].Rows.Count > 0)
                    {
                        dataTable = dt.Tables[0];
                    }
                }
                if (dataTable != null)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        string rowValue = row.ItemArray[0].ToString().TrimEnd('_');
                        dataResult = (rowValue.Split('_'));
                    }
                }


            }
            return Json(dataResult, JsonRequestBehavior.AllowGet);
        }
        //Get Accademic Year Months

        public JsonResult GetExamSubjectsMarks(long studentId, long examId, int ClassId)
        {
            List<MarkRegisterDetailsSplitDTO> listSubjects = ClientFactory.SchoolServiceClient(CallContext).GetExamSubjectsMarks(studentId, examId, ClassId);
            var SubjectList = new List<MarkRegisterDetailsSplitViewModel>();
            foreach (var data in listSubjects)
            {
                var sub = new MarkRegisterDetailsSplitViewModel()
                {
                    typeId = 1,
                    IsExpand = true,
                    Mark = data.Mark,
                    Subject = data.Subject,
                    SubjectID = data.SubjectID,
                    IsAbsent = data.IsAbsent,
                    IsPassed = data.IsPassed,
                    MinimumMark = data.MinimumMark,
                    MaximumMark = data.MaximumMark,
                    MarksGradeID = data.MarksGradeID,
                    MarksGradeMapID = data.MarksGradeMapID,
                    MarkRegisterID = data.MarkRegisterID,
                    MarkRegisterSubjectMapIID = data.MarkRegisterSubjectMapIID,
                    MarkRegSkillGroupSplit = (from skillGrpList in data.MarkRegisterSkillGroupDTO
                                              select new MarkRegSkillGroupSplitViewModel()
                                              {
                                                  typeId = 2,
                                                  IsExpand = true,
                                                  IsAbsent = skillGrpList.IsAbsent,
                                                  IsPassed = skillGrpList.IsPassed,
                                                  SkillGroupID = skillGrpList.SkillGroupMasterID,
                                                  MinimumMark = skillGrpList.MinimumMark,
                                                  MaximumMark = skillGrpList.MaximumMark,
                                                  SkillGroup = skillGrpList.SkillGroup,
                                                  Mark = skillGrpList.MarkObtained,
                                                  MarksGradeID = skillGrpList.MarksGradeID,
                                                  MarkRegisterSubjectMapID = skillGrpList.MarkRegisterSubjectMapID,
                                                  MarksGradeMapID = skillGrpList.MarksGradeMapID,
                                                  MarkRegisterSkillGroupIID = skillGrpList.MarkRegisterSkillGroupIID,
                                                  MarkRegSkillSplit = (from skillList in skillGrpList.MarkRegisterSkillsDTO
                                                                       select new MarkRegSkillSplitViewModel()
                                                                       {
                                                                           typeId = 3,
                                                                           IsAbsent = skillList.IsAbsent,
                                                                           IsPassed = skillList.IsPassed,
                                                                           SkillGroupMasterID = skillList.SkillGroupMasterID,
                                                                           MinimumMark = skillList.MinimumMark,
                                                                           MaximumMark = skillList.MaximumMark,
                                                                           SkillMasterID = skillList.SkillMasterID,
                                                                           Skill = skillList.Skill,
                                                                           Mark = skillList.MarksObtained,
                                                                           MarksGradeID = skillList.MarksGradeID,
                                                                           MarksGradeMapID = skillList.MarksGradeMapID,
                                                                           MarkRegisterSkillGroupID = skillList.MarkRegisterSkillGroupID,

                                                                       }).ToList()
                                              }).ToList(),
                };

                SubjectList.Add(sub);
            }

            return Json(SubjectList, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public JsonResult GetGradeByExamSubjects(long examId, int classId, long subjectID, int typeId)
        {
            var grades = ClientFactory.SchoolServiceClient(CallContext).GetGradeByExamSubjects(examId, classId, subjectID, typeId);
            return Json(grades, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetStudentSkills(long skillId)
        {
            var studSkills = ClientFactory.SchoolServiceClient(CallContext).GetStudentSkills(skillId);
            return Json(studSkills, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCollectFeeAccountData(string fromDate, string toDate, string cashierID)
        {
            var dateFormat = ConfigurationExtensions.GetAppConfigValue("DateFormat");
            DateTime fDate = DateTime.ParseExact(fromDate, dateFormat, CultureInfo.InvariantCulture);
            DateTime tDate = DateTime.ParseExact(toDate, dateFormat, CultureInfo.InvariantCulture);
            List<CollectFeeAccountDetailDTO> data = ClientFactory.SchoolServiceClient(CallContext).GetCollectFeeAccountData(fDate, tDate, long.Parse(cashierID));
            List<CollectFeeAccountPostingDetailViewModel> feeTypes = new List<CollectFeeAccountPostingDetailViewModel>();
            foreach (var dat in data)
            {
                var collectionFeeType = new CollectFeeAccountPostingDetailViewModel()
                {
                    CollectionDate = dat.CollectionDateString,
                    ReceiptNo = dat.ReceiptNo,
                    IsPosted = dat.IsPosted,
                    StudentId = dat.StudentId.Value,
                    Student = dat.Student,
                    GroupTransactionNumber = dat.GroupTransactionNumber == null ? "" : dat.GroupTransactionNumber,
                    CashAmount = dat.CollectFeePaymentModeList.Where(x => x.PaymentModeName.ToUpper().Contains("CASH")).Select(y => y.Amount).FirstOrDefault(),
                    CardAmount = dat.CollectFeePaymentModeList.Where(x => x.PaymentModeName.ToUpper().Contains("CARD")).Select(y => y.Amount).FirstOrDefault(),
                    Cheque = dat.CollectFeePaymentModeList.Where(x => x.PaymentModeName.ToUpper().Contains("CHEQUE")).Select(y => y.Amount).FirstOrDefault(),
                    BankAmount = dat.CollectFeePaymentModeList.Where(x => x.PaymentModeName.ToUpper().Contains("BANK")).Select(y => y.Amount).FirstOrDefault(),
                    OnlineDirectAmount = dat.CollectFeePaymentModeList.Where(x => x.PaymentModeName.ToUpper().Contains("ONLINEDIRECT")).Select(y => y.Amount).FirstOrDefault(),
                    OtherAmount = dat.CollectFeePaymentModeList.Where(x => x.PaymentModeName.ToUpper().Contains("OTHERS")).Select(y => y.Amount).FirstOrDefault(),
                    SplitData = (from split in dat.FeeAccountSplit
                                 select new CollectFeeAccountPostingSplitViewModel()
                                 {
                                     Amount = dat.CollectionType == 0 ? split.Amount.Value : -split.Amount.Value,
                                     FeeMaster = split.FeeMaster,
                                     AccountID = split.AccountID
                                 }).ToList()
                };
                feeTypes.Add(collectionFeeType);
            }
            var paymodeData = new FeeAccountPostingPayModeViewModel()
            {
                BankAmount = feeTypes.Select(x => x.BankAmount).Sum(),
                CashAmount = feeTypes.Select(x => x.CashAmount).Sum(),
                Cheque = feeTypes.Select(x => x.Cheque).Sum(),
                CardAmount = feeTypes.Select(x => x.CardAmount).Sum(),
                OnlineDirectAmount = feeTypes.Select(x => x.OnlineDirectAmount).Sum(),
                OtherAmount = feeTypes.Select(x => x.OtherAmount).Sum()
            };
            var collectFeeAccountPostingViewModel = new CollectFeeAccountPostingViewModel();
            var payModeViewModel = new List<FeeAccountPostingPayModeViewModel>();
            payModeViewModel.Add(paymodeData);
            collectFeeAccountPostingViewModel.DetailData = feeTypes;
            collectFeeAccountPostingViewModel.PayModeData = payModeViewModel;
            if (feeTypes == null)
            {
                return Json(new { IsError = true, Response = "There are some issues.Please try after sometime!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { IsError = false, Response = collectFeeAccountPostingViewModel }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult FeeAccountPosting(string fromDate, string toDate, string cashierID)
        {
            var dateFormat = ConfigurationExtensions.GetAppConfigValue("DateFormat");
            DateTime fDate = DateTime.ParseExact(fromDate, dateFormat, CultureInfo.InvariantCulture);
            DateTime tDate = DateTime.ParseExact(toDate, dateFormat, CultureInfo.InvariantCulture);
            string feeDueID = "0#Something went wrong!";
            feeDueID = ClientFactory.SchoolServiceClient(CallContext).FeeAccountPosting(fDate, tDate, long.Parse(cashierID));
            string[] resp = feeDueID.Split('#');
            dynamic response = new { IsFailed = (resp[0] == "0"), Message = resp[1] };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetSiblingDueDetailsFromStudentID(long StudentID)
        {
            string siblingDueDetails = null;
            List<FeeCollectionDTO> data = ClientFactory.SchoolServiceClient(CallContext).GetSiblingDueDetailsFromStudentID(StudentID);

            var studentSiblingFeeDueDetails = new List<FeeCollectionViewModel>();

            if (data.Count > 0)
            {
                foreach (var dueDet in data)
                {
                    var bk = new FeeCollectionViewModel()
                    {
                        AdmissionNumber = dueDet.AdmissionNo,
                        Student = new KeyValueViewModel() { Key = dueDet.StudentID.ToString(), Value = dueDet.StudentName },
                        TotalAmount = dueDet.Amount,
                    };

                    studentSiblingFeeDueDetails.Add(bk);
                    siblingDueDetails = string.Concat(siblingDueDetails, bk.AdmissionNumber, "-", bk.Student.Value, " Due=" + bk.TotalAmount, "</br>");
                }
            }
            else
            {
                siblingDueDetails = "Not any Sibling available for selected Student";
            }
            if (studentSiblingFeeDueDetails == null)
            {
                return Json(new { IsError = true, Response = "There are some issues.Please try after sometime!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { IsError = false, Response = siblingDueDetails }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetGradeByExamSkill(long examId, int skillId, int subjectId, int classId, int markGradeID)
        {
            var grades = ClientFactory.SchoolServiceClient(CallContext).GetGradeByExamSkill(examId, skillId, subjectId, classId, markGradeID);
            return Json(grades, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFeeStructure(int academicYearID)
        {
            var feeStructureList = ClientFactory.SchoolServiceClient(CallContext).GetFeeStructure(academicYearID);
            return Json(feeStructureList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFeePeriod(int academicYearID, long studentID)
        {
            var listFeePeriod = ClientFactory.SchoolServiceClient(CallContext).GetFeePeriod(academicYearID, studentID);
            return Json(listFeePeriod, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTransportFeePeriod(int academicYearID)
        {
            var listFeePeriod = ClientFactory.SchoolServiceClient(CallContext).GetTransportFeePeriod(academicYearID);
            return Json(listFeePeriod, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetStudentsCount()
        {
            var count = ClientFactory.SchoolServiceClient(CallContext).GetStudentsCount();
            dynamic response = new { TransactionCount = count, Amount = 0 };
            return Json(response);
        }

        public ActionResult GetGroupCodeByParentGroup(long parentGroupID)
        {
            var employee = ClientFactory.SchoolServiceClient(CallContext).GetGroupCodeByParentGroup(parentGroupID);
            return Json(employee, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAccountCodeByGroup(long groupID)
        {
            var employee = ClientFactory.SchoolServiceClient(CallContext).GetAccountCodeByGroup(groupID);
            return Json(employee, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSubGroup(int mainGroupID)
        {
            var subGroupList = ClientFactory.SchoolServiceClient(CallContext).GetSubGroup(mainGroupID);
            return Json(subGroupList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAccountGroup(int subGroupID)
        {
            var accountGroupList = ClientFactory.SchoolServiceClient(CallContext).GetAccountGroup(subGroupID);
            return Json(accountGroupList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAccountByGroupID(int groupID)
        {
            var accountList = ClientFactory.SchoolServiceClient(CallContext).GetAccountByGroupID(groupID);
            return Json(accountList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCostCenterByAccount(long accountID)
        {
            var costCenterListList = ClientFactory.SchoolServiceClient(CallContext).GetCostCenterByAccount(accountID);
            return Json(costCenterListList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAccountByPayementModeID(long paymentModeID)
        {
            var accountList = ClientFactory.SchoolServiceClient(CallContext).GetAccountByPayementModeID(paymentModeID);
            return Json(accountList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAccountGroupByAccountID(long accountID)
        {
            var accountGroupList = ClientFactory.SchoolServiceClient(CallContext).GetAccountGroupByAccountID(accountID);
            return Json(accountGroupList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCastByRelegion(int relegionID)
        {
            var castList = ClientFactory.SchoolServiceClient(CallContext).GetCastByRelegion(relegionID);
            return Json(castList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStreamByStreamGroup(byte? streamGroupID)
        {
            var streamList = ClientFactory.SchoolServiceClient(CallContext).GetStreamByStreamGroup(streamGroupID);
            return Json(streamList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStreamCompulsorySubjects(byte? streamID)
        {
            var streamList = ClientFactory.SchoolServiceClient(CallContext).GetStreamCompulsorySubjects(streamID);
            return Json(streamList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStreamOptionalSubjects(byte? streamID)
        {
            var streamList = ClientFactory.SchoolServiceClient(CallContext).GetStreamOptionalSubjects(streamID);
            return Json(streamList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSubSkillByGroup(int skillGroupID)
        {
            var castList = ClientFactory.SchoolServiceClient(CallContext).GetSubSkillByGroup(skillGroupID);
            return Json(castList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAgeCriteriaByClassID(int classId, int academicYearID)
        {
            var ageList = ClientFactory.SchoolServiceClient(CallContext).GetAgeCriteriaByClassID(classId, academicYearID);

            return Json(ageList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAcademicYearBySchool(int schoolID)
        {
            var academicyearList = ClientFactory.SchoolServiceClient(CallContext).GetAcademicYearBySchool(schoolID);
            return Json(academicyearList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllAcademicYearBySchool(int schoolID)
        {
            var academicyearList = ClientFactory.SchoolServiceClient(CallContext).GetAcademicYearBySchool(schoolID);
            return Json(academicyearList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetClassesBySchool(byte schoolID)
        {
            var academicyearList = ClientFactory.SchoolServiceClient(CallContext).GetClassesBySchool(schoolID);
            return Json(academicyearList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSectionsBySchool(byte schoolID)
        {
            var sectionsList = ClientFactory.SchoolServiceClient(CallContext).GetSectionsBySchool(schoolID);
            return Json(sectionsList, JsonRequestBehavior.AllowGet);
        }

        public string GetProgressReportName(long schoolID, int? classID)
        {
            var sectionsList = ClientFactory.SchoolServiceClient(CallContext).GetProgressReportName(schoolID, classID);
            return sectionsList;
        }

        public JsonResult GetClasseByAcademicyear(int academicyearID)
        {
            var academicyearList = ClientFactory.SchoolServiceClient(CallContext).GetClasseByAcademicyear(academicyearID);
            return Json(academicyearList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRouteStopsByRoute(int routeID)
        {
            var routeStopList = ClientFactory.SchoolServiceClient(CallContext).GetRouteStopsByRoute(routeID);
            return Json(routeStopList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSubjectByClass(int classID)
        {
            var subjectList = ClientFactory.SchoolServiceClient(CallContext).GetSubjectByClass(classID);
            return Json(subjectList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSubjectbyQuestionGroup(long questionGroupID)
        {
            var subjectList = ClientFactory.SchoolServiceClient(CallContext).GetSubjectbyQuestionGroup(questionGroupID);
            return Json(subjectList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetQuestionDetailsByQuestionID(long questionID)
        {
            var result = ClientFactory.SchoolServiceClient(CallContext).GetQuestionDetailsByQuestionID(questionID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLibraryStudentFromStudentID(long studentID)
        {
            var student = ClientFactory.SchoolServiceClient(CallContext).GetLibraryStudentFromStudentID(studentID);
            return Json(student, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLibraryStaffFromEmployeeID(long employeeID)
        {
            var employee = ClientFactory.SchoolServiceClient(CallContext).GetLibraryStaffFromEmployeeID(employeeID);
            return Json(employee, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetStudentTransportApplication(long TransportApplctnStudentMapIID)
        {
            var data = ClientFactory.SchoolServiceClient(CallContext).GetStudentTransportApplication(TransportApplctnStudentMapIID);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAccountCodeByLedger(int ledgerGroupID)
        {
            var accountCodeList = ClientFactory.SchoolServiceClient(CallContext).GetAccountCodeByLedger(ledgerGroupID);
            return Json(accountCodeList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult FillClassStudents(long classID, long sectionID)
        {
            List<SubjectMarkEntryDetailDTO> data = ClientFactory.SchoolServiceClient(CallContext).FillClassStudents(classID, sectionID);
            var subjectMarks = new List<SubjectMarkEntryDetailViewModel>();

            foreach (var dat in data)
            {

                subjectMarks.Add(new SubjectMarkEntryDetailViewModel()
                {
                    IsExpand = false,
                    StudentID = dat.StudentID,
                });
            }

            return Json(subjectMarks, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetSubjectsMarkData(long examID, int classID, int sectionID, int subjectID)
        {
            List<SubjectMarkEntryDetailDTO> listSubjects = ClientFactory.SchoolServiceClient(CallContext).GetSubjectsMarkData(examID, classID, sectionID, subjectID);
            var SubjectList = new List<SubjectMarkEntryDetailViewModel>();
            foreach (var data in listSubjects)
            {
                var sub = new SubjectMarkEntryDetailViewModel()
                {
                    typeId = 1,
                    IsExpand = true,
                    Mark = data.Mark,
                    Grade = data.Grade,
                    StudentName = data.StudentName,
                    StudentID = data.StudentID,
                    SubjectID = data.SubjectID,
                    IsAbsent = data.IsAbsent,
                    IsPassed = data.IsPassed,
                    MinimumMark = data.MinimumMark,
                    MaximumMark = data.MaximumMark,
                    MarksGradeID = data.MarksGradeID,
                    MarksGradeMapID = data.MarksGradeMapID,
                    MarkRegisterID = data.MarkRegisterID,
                    MarkRegisterSubjectMapIID = data.MarkRegisterSubjectMapIID,
                    SubjectMarkSkillGroup = (from skillGrpList in data.MarkRegisterSkillGroupDTO
                                             select new SubjectMarkSkillGroupViewModel()
                                             {
                                                 typeId = 2,
                                                 IsExpand = true,
                                                 IsAbsent = skillGrpList.IsAbsent,
                                                 IsPassed = skillGrpList.IsPassed,
                                                 SkillGroupID = skillGrpList.SkillGroupMasterID,
                                                 MinimumMark = skillGrpList.MinimumMark,
                                                 MaximumMark = skillGrpList.MaximumMark,
                                                 SkillGroup = skillGrpList.SkillGroup,
                                                 Grade = skillGrpList.Grade,
                                                 Mark = skillGrpList.MarkObtained,
                                                 MarksGradeID = skillGrpList.MarksGradeID,
                                                 MarkRegisterSubjectMapID = skillGrpList.MarkRegisterSubjectMapID,
                                                 MarksGradeMapID = skillGrpList.MarksGradeMapID,
                                                 MarkRegisterSkillGroupIID = skillGrpList.MarkRegisterSkillGroupIID,
                                                 SubjectMarkSkill = (from skillList in skillGrpList.MarkRegisterSkillsDTO
                                                                     select new SubjectMarkSkillViewModel()
                                                                     {
                                                                         typeId = 3,
                                                                         IsAbsent = skillList.IsAbsent,
                                                                         IsPassed = skillList.IsPassed,
                                                                         SkillGroupMasterID = skillList.SkillGroupMasterID,
                                                                         MinimumMark = skillList.MinimumMark,
                                                                         MaximumMark = skillList.MaximumMark,
                                                                         SkillMasterID = skillList.SkillMasterID,
                                                                         Skill = skillList.Skill,
                                                                         Mark = skillList.MarksObtained,
                                                                         Grade = skillList.Grade,
                                                                         MarksGradeID = skillList.MarksGradeID,
                                                                         MarksGradeMapID = skillList.MarksGradeMapID,
                                                                         MarkRegisterSkillIID = skillList.MarkRegisterSkillIID,
                                                                         MarkRegisterSkillGroupID = skillList.MarkRegisterSkillGroupID,

                                                                     }).ToList()
                                             }).ToList(),
                };

                SubjectList.Add(sub);
            }

            return Json(SubjectList, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult GetUnitDataByUnitGroup(int groupID)
        {
            var unitData = ClientFactory.SchoolServiceClient(CallContext).GetUnitDataByUnitGroup(groupID);
            return Json(unitData, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetUnitByUnitGroup(int groupID)
        {
            var productDet = ClientFactory.SchoolServiceClient(CallContext).GetUnitByUnitGroup(groupID);
            return Json(productDet, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAcademicYear(int academicYearID)
        {
            var academicYear = ClientFactory.SchoolServiceClient(CallContext).GetAcademicYear(academicYearID);
            return Json(academicYear, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetExamMarkDetails(long subjectID, long examID)
        {
            var marks = ClientFactory.SchoolServiceClient(CallContext).GetExamMarkDetails(subjectID, examID);
            return Json(marks, JsonRequestBehavior.AllowGet);
        }

        //Get All Mail
        public JsonResult GetAllMailsByLoginIDandFolderName(String Folder, Int64 LoginID)
        {
            String[] dataResult = null; var ResModel = new ResponseModel();
            string message = "";
            var dateFormat = Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue("DateFormat");
            SqlConnectionStringBuilder _sBuilder = new SqlConnectionStringBuilder(ConfigurationExtensions.GetConnectionString("dbEduegateSchoolContext").ToString());
            using (IDbConnection conn = new SqlConnection(_sBuilder.ConnectionString))
            {

                try
                {
                    var reader = conn.QueryMultiple("[schools].[GET_MAIL_LIST]",
                                   param: new { LoginID = LoginID, FloderName = Folder },
                                 commandType: CommandType.StoredProcedure);
                    var mailList = reader.Read<MAIL_LIST>().ToList();
                    if (mailList.Count() > 0)
                    {
                        ResModel.Data = new
                        {
                            mailList = mailList,
                            isError = false
                        };
                        message = string.Format("");
                    }
                    else
                    {
                        ResModel.Data = new
                        {
                            isError = true,
                        };
                        message = string.Format("");
                    }
                }
                catch (Exception e)
                {
                    message = string.Format(e.Message + ", " + e.InnerException.Message);
                    ResModel.Message = message;
                    ResModel.Data = "";
                }
            }
            return Json(ResModel, JsonRequestBehavior.AllowGet);
        }
        //End Get All Mail

        [HttpGet]
        public JsonResult GetClassWiseExamGroup(int classID, int? sectionID, int academicYearID)
        {
            var response = ClientFactory.SchoolServiceClient(CallContext).GetClassWiseExamGroup(classID, sectionID, academicYearID);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetSkillSetByClassExam(int classID, int? sectionID, long? examID, int academicYearID, short? languageTypeID)
        {
            var response = ClientFactory.SchoolServiceClient(CallContext).GetSkillSetByClassExam(classID, sectionID, examID, academicYearID, languageTypeID);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetSubjectsBySkillset(long skillSetID)
        {
            var response = ClientFactory.SchoolServiceClient(CallContext).GetSubjectsBySkillset(
                new MarkEntrySearchArgsDTO { SkillSetID = skillSetID });
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetSkillGroupByClassExam(int classID, int? examGroupID, long skillSetID, int academicYearID)
        {
            var response = ClientFactory.SchoolServiceClient(CallContext).GetSkillGroupByClassExam(classID, examGroupID, skillSetID, academicYearID);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetSkillsByClassExam(int classID, int skillGroupID, long skillSetID, int academicYearID)
        {
            var response = ClientFactory.SchoolServiceClient(CallContext).GetSkillsByClassExam(classID, skillGroupID, skillSetID, academicYearID);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetSubjectsByClassID(int classID, int? sectionId, long examID, int academicYearID, short? languageTypeID)
        {
            var response = ClientFactory.SchoolServiceClient(CallContext).GetSubjectsByClassID(
            new MarkEntrySearchArgsDTO { ClassID = classID, SectionID = sectionId, ExamID = examID, AcademicYearID = academicYearID, LanguageTypeID = languageTypeID });
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetSubjectsBySubjectType(int classID, int? sectionId, long examID, int academicYearID, short? languageTypeID)
        {
            var response = ClientFactory.SchoolServiceClient(CallContext).GetSubjectsBySubjectType(
            new MarkEntrySearchArgsDTO { ClassID = classID, SectionID = sectionId, ExamID = examID, AcademicYearID = academicYearID, LanguageTypeID = languageTypeID });
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetExamsByTermID(int classID, int? sectionID, byte termID, int academicYearID, string tab)
        {
            var response = ClientFactory.SchoolServiceClient(CallContext).GetExamsByTermID(
                new MarkEntrySearchArgsDTO { ClassID = classID, SectionID = sectionID, TermID = termID, AcademicYearID = academicYearID, tabType = tab });
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCurrentAcademicYear()
        {
            var response = base.CallContext.AcademicYearID;
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAcademicYearDataByCalendarID(long calendarID)
        {
            var academicYearData = ClientFactory.SchoolServiceClient(CallContext).GetAcademicYearDataByCalendarID(calendarID);
            return Json(academicYearData, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCurrentUserData()
        {
            var response = new UserDTO
            {
                AcademicYearID = base.CallContext.AcademicYearID,
                SchoolID = (byte?)base.CallContext.SchoolID,
                LoginID = base.CallContext.LoginID.ToString(),
                EmployeeID = base.CallContext.EmployeeID
            };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetExamsByClassAndGroup(int classID, int? sectionID, int examGroupID, int academicYearID)
        {
            var response = ClientFactory.SchoolServiceClient(CallContext).GetExamsByClassAndGroup(classID, sectionID, examGroupID, academicYearID);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetSubjectByType(byte subjectTypeID)
        {
            var response = ClientFactory.SchoolServiceClient(CallContext).GetSubjectByType(subjectTypeID);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetSkillByExamAndClass(int classID, int? sectionID, int examID, int academicYearID, int termID)
        {
            var response = ClientFactory.SchoolServiceClient(CallContext).GetSkillByExamAndClass(classID, sectionID, examID, academicYearID, termID);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult MoveToApplication(long leadID, long? screenID)
        {
            var dateFormat = Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue("DateFormat");
            var data = ClientFactory.FrameworkServiceClient(CallContext).GetScreenData((int)Screens.Lead, leadID);
            var viewVm = new LeadViewModel();
            viewVm = viewVm.ToVM(viewVm.ToDTO(data)) as LeadViewModel;
            var studentApplication = new StudentApplicationDTO()
            {
                ProspectNumber = viewVm.LeadCode,
                EmailID = viewVm.ContactDetails.EmailAddress,
                FirstName = viewVm.ContactDetails.StudentName,
                MobileNumber = viewVm.ContactDetails.MobileNumber,
                FatherFirstName = viewVm.ContactDetails.ParentName,
                GenderID = viewVm.ContactDetails.GenderID,
                DateOfBirth = string.IsNullOrEmpty(viewVm.ContactDetails.DateOfBirthString) ? (DateTime?)null : DateTime.ParseExact(viewVm.ContactDetails.DateOfBirthString, dateFormat, CultureInfo.InvariantCulture),
                ClassID = viewVm.ContactDetails.ClassID,
                SchoolID = viewVm.SchoolID.HasValue ? viewVm.SchoolID : null,
                SchoolAcademicyearID = viewVm.ContactDetails.AcademicYearID,
                CurriculamID = viewVm.ContactDetails.CurriculamID,
            };

            var applicationSave = ClientFactory.SchoolServiceClient(CallContext).SaveStudentApplication(studentApplication);

            return Json(applicationSave, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SendTodayAttendancePushNotification(int classId, int sectionId)
        {
            var dataPass = ClientFactory.SchoolServiceClient(CallContext).SendTodayAttendancePushNotification(classId, sectionId);
            return Json(dataPass, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ResentLoginCredentials(long leadID, long? screenID)
        {
            var dateFormat = Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue("DateFormat");
            var data = ClientFactory.FrameworkServiceClient(CallContext).GetScreenData((int)Screens.Lead, leadID);
            var viewVm = new LeadViewModel();
            viewVm = viewVm.ToVM(viewVm.ToDTO(data)) as LeadViewModel;
            var studtApplication = new StudentApplicationDTO()
            {
                ProspectNumber = viewVm.LeadCode,
                EmailID = viewVm.ContactDetails.EmailAddress,
                FirstName = viewVm.ContactDetails.StudentName,
                MobileNumber = viewVm.ContactDetails.MobileNumber,
                FatherFirstName = viewVm.ContactDetails.ParentName,
                GenderID = viewVm.ContactDetails.GenderID,
                DateOfBirth = string.IsNullOrEmpty(viewVm.ContactDetails.DateOfBirthString) ? (DateTime?)null : DateTime.ParseExact(viewVm.ContactDetails.DateOfBirthString, dateFormat, CultureInfo.InvariantCulture),
                ClassID = viewVm.ContactDetails.ClassID,
                SchoolID = viewVm.SchoolID.HasValue ? viewVm.SchoolID : null,
                SchoolAcademicyearID = viewVm.ContactDetails.AcademicYearID,
                CurriculamID = viewVm.ContactDetails.CurriculamID,
            };

            var applicationSave = ClientFactory.SchoolServiceClient(CallContext).ResentFromLeadLoginCredentials(studtApplication);

            return Json(applicationSave, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult MoveToApplicationFromLead(LeadViewModel leadInfo)
        {
            var dateFormat = Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue("DateFormat");
            var studentApplication = new StudentApplicationDTO()
            {
                ProspectNumber = leadInfo.LeadCode,
                EmailID = leadInfo.ContactDetails.EmailAddress,
                FirstName = leadInfo.ContactDetails.StudentName,
                MobileNumber = leadInfo.ContactDetails.MobileNumber,
                FatherFirstName = leadInfo.ContactDetails.ParentName,
                GenderID = leadInfo.ContactDetails.GenderID,
                DateOfBirth = string.IsNullOrEmpty(leadInfo.ContactDetails.DateOfBirthString) ? (DateTime?)null : DateTime.ParseExact(leadInfo.ContactDetails.DateOfBirthString, dateFormat, CultureInfo.InvariantCulture),
                ClassID = leadInfo.ContactDetails.ClassID,
                SchoolID = leadInfo.SchoolID.HasValue ? leadInfo.SchoolID : null,
                SchoolAcademicyearID = leadInfo.ContactDetails.AcademicYearID,
                CurriculamID = leadInfo.ContactDetails.CurriculamID,
            };

            var applicationSave = ClientFactory.SchoolServiceClient(CallContext).SaveStudentApplication(studentApplication);

            return Json(applicationSave, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetClasswiseStudentDataForCampusTransfer(int classId, int sectionId)
        {

            var listStudents = ClientFactory.SchoolServiceClient(CallContext).GetClasswiseStudentDataForCampusTransfer(classId, sectionId);
            var studList = new List<CampusTransferStudentListViewModel>();

            foreach (var data in listStudents)
            {
                studList.Add(new CampusTransferStudentListViewModel()
                {
                    Student = new KeyValueViewModel() { Key = data.Student.Key, Value = data.Student.Value },

                });
            }

            return Json(studList, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult GetGeoSchoolSetting(int schoolID)
        {
            var response = ClientFactory.SchoolServiceClient(CallContext).GetGeoSchoolSetting(schoolID);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAdvancedAcademicYearBySchool(int schoolID)
        {
            var response = ClientFactory.SchoolServiceClient(CallContext).GetAdvancedAcademicYearBySchool(schoolID);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetBooKCategoryName(long bookCategoryCodeId)
        {
            var response = ClientFactory.SchoolServiceClient(CallContext).GetBooKCategoryName(bookCategoryCodeId);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetBookQuantityDetails(string CallAccNo, int? bookMapID)
        {
            var response = ClientFactory.SchoolServiceClient(CallContext).GetBookQuantityDetails(CallAccNo, bookMapID);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetIssuedBookDetails(string CallAccNo, int? bookMapID)
        {
            var response = ClientFactory.SchoolServiceClient(CallContext).GetIssuedBookDetails(CallAccNo, bookMapID);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetBookDetailsByCallNo(string CallAccNo)
        {
            var response = ClientFactory.SchoolServiceClient(CallContext).GetBookDetailsByCallNo(CallAccNo);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        //[HttpGet]
        //public JsonResult GetBookDetailsChange(long BookID)
        //{
        //    var response = ClientFactory.SchoolServiceClient(CallContext).GetBookDetailsChange(BookID);
        //    return Json(response, JsonRequestBehavior.AllowGet);
        //}


        [HttpPost]
        public JsonResult SaveGeoSchoolSetting(List<SchoolGeoLocationDTO> geoSettings)
        {
            ClientFactory.SchoolServiceClient(CallContext).SaveGeoSchoolSetting(geoSettings);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ClearGeoSchoolSetting(int schoolID)
        {
            ClientFactory.SchoolServiceClient(CallContext).ClearGeoSchoolSetting(schoolID);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetMonthAndYearByAcademicYearIDForAcademicClassMap(int? academicYearID)
        {
            var academicClassMapList = ClientFactory.SchoolServiceClient(CallContext).GetMonthAndYearByAcademicYearID(academicYearID);

            var classWorkingDaysList = new List<AcademicClassMapWorkingDaysViewModel>();

            foreach (var data in academicClassMapList)
            {
                classWorkingDaysList.Add(new AcademicClassMapWorkingDaysViewModel()
                {
                    MonthID = data.MonthID,
                    MonthName = data.MonthName,
                    YearID = data.YearID,
                    Description = data.MonthName + " - " + data.YearID
                });
            }

            return Json(classWorkingDaysList, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetMonthAndYearByAcademicYearIDForAcademicSchoolMap(int? academicYearID)
        {
            var academicSchoolMapList = ClientFactory.SchoolServiceClient(CallContext).GetMonthAndYearByAcademicYearID(academicYearID);

            var schoolWorkingDaysList = new List<AcademicSchoolMapWorkingDaysViewModel>();

            foreach (var data in academicSchoolMapList)
            {
                schoolWorkingDaysList.Add(new AcademicSchoolMapWorkingDaysViewModel()
                {
                    MonthID = data.MonthID,
                    MonthName = data.MonthName,
                    YearID = data.YearID,
                    Description = data.MonthName,
                });
            }

            return Json(schoolWorkingDaysList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SendPushNotification(PushNotificationViewModel pushViewModel)
        {
            try
            {
                var crudSave = ClientFactory.FrameworkServiceClient(CallContext).SaveCRUDData(new CRUDDataDTO() { ScreenID = (long)Screens.PushNotification, Data = pushViewModel.AsDTOString(pushViewModel.ToDTO(CallContext)) });
                if (crudSave.IsError)
                {
                    return Json(new { IsError = true, Response = "Failed to sent!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { IsError = false, Response = "Successfully sent" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new { IsError = true, Response = "Failed to sent!" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult CheckAndInsertCalendarEntries(long calendarID)
        {
            var academicYearData = ClientFactory.SchoolServiceClient(CallContext).CheckAndInsertCalendarEntries(calendarID);
            return Json(academicYearData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCalendarByTypeID(byte calendarTypeID)
        {
            var calendarList = ClientFactory.SchoolServiceClient(CallContext).GetCalendarByTypeID(calendarTypeID);
            return Json(calendarList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEmployeesByCalendarID(long calendarID)
        {
            var employeeList = ClientFactory.SchoolServiceClient(CallContext).GetEmployeesByCalendarID(calendarID);
            return Json(employeeList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCreditNoteNumber(long? headID, long studentID)
        {
            var data = ClientFactory.SchoolServiceClient(CallContext).GetCreditNoteNumber(headID, studentID);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAcademicYearDataByAcademicYearID(int academicYearID)
        {
            var AcademicYearData = ClientFactory.SchoolServiceClient(CallContext).GetAcademicYearDataByAcademicYearID(academicYearID);
            return Json(AcademicYearData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetToNotificationUsersByUser(int userID, int branchID, string user)
        {
            var castList = ClientFactory.SchoolServiceClient(CallContext).GetToNotificationUsersByUser(userID, branchID, user);
            return Json(castList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRoutesByRouteGroupID(int routeGroupID)
        {
            var routes = ClientFactory.SchoolServiceClient(CallContext).GetRoutesByRouteGroupID(routeGroupID);
            return Json(routes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAcademicYearDataByGroupID(int routeGroupID)
        {
            var academicYearData = ClientFactory.SchoolServiceClient(CallContext).GetAcademicYearDataByGroupID(routeGroupID);
            return Json(academicYearData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPickupStopMapsByRouteGroupID(int routeGroupID)
        {
            var pickupStops = ClientFactory.SchoolServiceClient(CallContext).GetPickupStopMapsByRouteGroupID(routeGroupID);
            return Json(pickupStops, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDropStopMapsByRouteGroupID(int routeGroupID)
        {
            var pickupStops = ClientFactory.SchoolServiceClient(CallContext).GetDropStopMapsByRouteGroupID(routeGroupID);
            return Json(pickupStops, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSubLedgerByAccount(long accountID)
        {
            var costCenterListList = ClientFactory.SchoolServiceClient(CallContext).GetSubLedgerByAccount(accountID);
            return Json(costCenterListList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetGuardianDetails(long studentID)
        {
            GuardianDTO data = ClientFactory.SchoolServiceClient(CallContext).GetGuardianDetails(studentID);

            var dateFormat = ConfigurationExtensions.GetAppConfigValue("DateFormat");

            var guardianDetails = new GaurdianViewModel()
            {
                FatherFirstName = data.FatherFirstName,
                FatherMiddleName = data.FatherMiddleName,
                FatherLastName = data.FatherLastName,
                MotherFirstName = data.MotherFirstName,
                MotherMiddleName = data.MotherMiddleName,
                MotherLastName = data.MotherLastName,
                GuardianFirstName = data.GuardianFirstName,
                GuardianMiddleName = data.GuardianMiddleName,
                GuardianLastName = data.GuardianLastName,
                GaurdianEmail = data.GaurdianEmail ?? "NA",
                MotherEmailID = data.MotherEmailID ?? "NA",
                MotherPhone = data.MotherPhone ?? "NA",
                GuardianPhone = data.GuardianPhone ?? "NA",
                FatherMobileNumberTwo = data.FatherMobileNumberTwo ?? "NA",
                FatherPassportNumber = data.FatherPassportNumber ?? "NA",
                MotherPassportNumber = data.MotherPassportNumber ?? "NA",
                MotherNationalID = data.MotherNationalID,
                FatherNationalID = data.FatherNationalID,
                FatherCompanyName = data.FatherCompanyName ?? "NA",
                MotherCompanyName = data.MotherCompanyName ?? "NA",
                MotherCountry = data.MotherCountry ?? "NA",
                FatherCountry = data.FatherCountry ?? "NA",
                FatherOccupation = data.FatherOccupation ?? "NA",
                MotherOccupation = data.MotherOccupation ?? "NA",
                FatherPassportNoExpiryString = data.FatherPassportNoExpiryDate.HasValue ? data.FatherPassportNoExpiryDate.Value.ToString(dateFormat) : "NA",
                FatherPassportNoIssueString = data.FatherPassportNoIssueDate.HasValue ? data.FatherPassportNoIssueDate.Value.ToString(dateFormat) : "NA",
                MotherPassportNoExpiryString = data.MotherPassportNoExpiryDate.HasValue ? data.MotherPassportNoExpiryDate.Value.ToString(dateFormat) : "NA",
                MotherPassportNoIssueString = data.MotherPassportNoIssueDate.HasValue ? data.MotherPassportNoIssueDate.Value.ToString(dateFormat) : "NA",
                MotherNationalDNoIssueDateString = data.MotherNationalDNoIssueDate.HasValue ? data.MotherNationalDNoIssueDate.Value.ToString(dateFormat) : "NA",
                MotherNationaIDNoExpiryDateString = data.MotherNationalDNoExpiryDate.HasValue ? data.MotherNationalDNoExpiryDate.Value.ToString(dateFormat) : "NA",
                FatherNationalDNoIssueDateString = data.FatherNationalDNoIssueDate.HasValue ? data.FatherNationalDNoIssueDate.Value.ToString(dateFormat) : "NA",
                FatherNationalDNoExpiryDateString = data.FatherNationalDNoExpiryDate.HasValue ? data.FatherNationalDNoExpiryDate.Value.ToString(dateFormat) : "NA",
                LocationNo = data.LocationNo ?? "NA",
                BuildingNo = data.BuildingNo ?? "NA",
                FlatNo = data.FlatNo ?? "NA",
                StreetName = data.StreetName ?? "NA",
                StreetNo = data.StreetNo ?? "NA",
                ZipNo = data.ZipNo ?? "NA",
                LocationName = data.LocationName ?? "NA",
                PostBoxNo = data.PostBoxNo ?? "NA",
                City = data.City ?? "NA",
                CountryID = data.CountryID,
                FatherCountryID = data.FatherCountryID,
                MotherCountryID = data.MotherCountryID,
                GuardianTypeID = data.GuardianTypeID,
                Country = data.Country ?? "NA",
            };

            if (guardianDetails == null)
            {
                return Json(new { IsError = true, Response = "There are some issues.Please try after sometime!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { IsError = false, Response = guardianDetails }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public JsonResult GetCountByDocumentTypeID(int docTypeID)
        {
            var result = ClientFactory.SchoolServiceClient(CallContext).GetCountByDocumentTypeID(docTypeID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SendAttendanceNotificationsToParents(int classId, int sectionId)
        {
            var dataPass = ClientFactory.SchoolServiceClient(CallContext).SendAttendanceNotificationsToParents(classId, sectionId);
            return Json(dataPass, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillClassSectionWiseCoordinators(int classID, int sectionID)
        {
            var listSubjects = ClientFactory.SchoolServiceClient(CallContext).FillClassSectionWiseCoordinators(classID, sectionID);
            return Json(listSubjects, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCountsForDashBoardMenuCards(int chartID)
        {
            var result = ClientFactory.SchoolServiceClient(CallContext).GetCountsForDashBoardMenuCards(chartID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetFeeDueDatasForReportMail(string asOnDateString, int classID, int? sectionID)
        {
            var dateFormat = ConfigurationExtensions.GetAppConfigValue("DateFormat");
            DateTime asOnDate = DateTime.ParseExact(asOnDateString, dateFormat, CultureInfo.InvariantCulture);

            var listStudents = ClientFactory.SchoolServiceClient(CallContext).GetFeeDueDatasForReportMail(asOnDate, classID, sectionID);
            return Json(listStudents, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetVehiclesByRoute(int routeID)
        {
            var vehicleList = ClientFactory.SchoolServiceClient(CallContext).GetVehiclesByRoute(routeID);
            return Json(vehicleList, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult LoadDataForDirectorsDashBoard()
        {
            var result = ClientFactory.SchoolServiceClient(CallContext).GetTeacherRelatedDataForDirectorsDashBoard();
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult LoadDataForClassTeacherDashBoard()
        {
            var result = ClientFactory.SchoolServiceClient(CallContext).GetTeacherRelatedDataForDirectorsDashBoard();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult UserProfileForDashBoard()
        {
            var result = ClientFactory.SchoolServiceClient(CallContext).UserProfileForDashBoard();
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetNotificationsForDashBoard()
        {
            var result = ClientFactory.SchoolServiceClient(CallContext).GetNotificationsForDashBoard();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SendFeeDueMailReportToParent(MailFeeDueStatementReportDTO gridData)
        {

            var dataPass = ClientFactory.SchoolServiceClient(CallContext).SendFeeDueMailReportToParent(gridData);

            ClientFactory.ReportGenerationServiceClient(CallContext).SendFeeDueMailReportToParent(dataPass);
            return Json(dataPass, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetWeeklyTimeTableForDashBoard(int weekDayID)
        {
            var result = ClientFactory.SchoolServiceClient(CallContext).GetWeeklyTimeTableForDashBoard(weekDayID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        #region STUDENT CONCESSION
        [HttpGet]
        public JsonResult GetStudentDetailsByStaff(long staffID)
        {
            var result = ClientFactory.SchoolServiceClient(CallContext).GetStudentDetailsByStaff(staffID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetStudentDetailsByParent(long parentID)
        {
            var result = ClientFactory.SchoolServiceClient(CallContext).GetStudentDetailsByParent(parentID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetFeeDueForConcession(long studentID, int academicYearID)
        {
            var fees = ClientFactory.SchoolServiceClient(CallContext).GetFeeDueForConcession(studentID, academicYearID);
            var studentConcession = new StudentConcessionViewModel();
            studentConcession.StudentConcessionDetail = (from x in fees
                                                         where (x.FeeMasterID ?? 0) > 0
                                                         select new StudentConcessionDetailViewModel()
                                                         {
                                                             StudentFeeConcessionID = x.StudentFeeConcessionID,
                                                             FeeDueFeeTypeMapID = x.FeeDueFeeTypeMapsID,
                                                             FeeInvoiceID = x.StudentFeeDueID,
                                                             FeeMaster = KeyValueViewModel.ToViewModel(x.FeeMaster),
                                                             FeePeriod = KeyValueViewModel.ToViewModel(x.FeePeriod),
                                                             InvoiceNo = KeyValueViewModel.ToViewModel(x.FeeInvoice),
                                                             ConcessionPercentage = x.PercentageAmount,
                                                             ConcessionAmount = x.ConcessionAmount,
                                                             Amount = x.DueAmount,
                                                             NetToPay = x.NetAmount
                                                         }).ToList();
            return Json(studentConcession.StudentConcessionDetail, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFeeDueForDueCancellation(long studentID, int academicYearID)
        {
            var fees = ClientFactory.SchoolServiceClient(CallContext).GetFeeDueForDueCancellation(studentID, academicYearID);
            var dueCancellation = new FeeDueCancellationViewModel();
            dueCancellation.FeeDueDetails = (from x in fees
                                             where (x.FeeMasterID ?? 0) > 0
                                             select new FeeDueCancellationDetailViewModel()
                                             {
                                                 FeeDueCancellationID = x.FeeDueCancellationID,
                                                 FeeDueFeeTypeMapID = x.FeeDueFeeTypeMapsID,
                                                 FeeInvoiceID = x.StudentFeeDueID,
                                                 FeeMaster = KeyValueViewModel.ToViewModel(x.FeeMaster),
                                                 FeePeriod = KeyValueViewModel.ToViewModel(x.FeePeriod),
                                                 InvoiceNo = KeyValueViewModel.ToViewModel(x.FeeInvoice),
                                                 FeeDueFeeTypeMapsID = x.FeeDueFeeTypeMapsID,
                                                 //ConcessionAmount = x.ConcessionAmount,
                                                 Amount = x.DueAmount,
                                                 //NetToPay = x.NetAmount
                                             }).ToList();
            return Json(dueCancellation.FeeDueDetails, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetFeeDueMonthlySplits(long studentFeeDueID, int? feeMasterID, int? feePeriodID)
        {
            var data = ClientFactory.SchoolServiceClient(CallContext).GetFeeDueMonthlyDetails(studentFeeDueID, feeMasterID, feePeriodID);
            var studentConcessionDetailViewModel = new StudentConcessionDetailViewModel();
            var months = new List<KeyValueViewModel>();
            var years = new List<KeyValueViewModel>();
            //studentConcessionDetailViewModel.MonthList = new List<KeyValueViewModel>();
            //studentConcessionDetailViewModel.YearList = new List<KeyValueViewModel>();
            //var feePeriod = new FeePeriodsViewModel();
            studentConcessionDetailViewModel.MonthSplitList = new List<FeeDueMonthlySplitViewModel>();
            foreach (var mnth in data)
            {
                if (mnth.MonthID != 0)
                {
                    months.Add(new KeyValueViewModel { Key = mnth.MonthID.ToString(), Value = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(mnth.MonthID).Substring(0, 3) });

                    if (!years.Any(x => x.Key == mnth.Year.ToString()))
                    {
                        years.Add(new KeyValueViewModel
                        {
                            Key = mnth.Year.ToString(),
                            Value = mnth.Year.ToString()
                        });
                    }
                }
                studentConcessionDetailViewModel.MonthSplitList.Add(
                        new FeeDueMonthlySplitViewModel
                        {
                            Year = mnth.Year,
                            MonthID = mnth.MonthID,
                            FeeDueMonthlySplitID = mnth.FeeDueMonthlySplitIID,
                            FeeDueFeeTypeMapsID = mnth.FeeDueFeeTypeMapsID,
                        });

            }
            if (studentConcessionDetailViewModel.MonthSplitList.Count() == 1)
            {
                studentConcessionDetailViewModel.FeeDueMonthlySplitID = studentConcessionDetailViewModel.MonthSplitList[0].FeeDueMonthlySplitID;
            }
            //studentConcessionDetailViewModel.MonthList.AddRange(months);
            //studentConcessionDetailViewModel.YearList.AddRange(years);
            return Json(studentConcessionDetailViewModel, JsonRequestBehavior.AllowGet);
        }

        #endregion STUDENT CONCESSION

        [HttpGet]
        public JsonResult GetEmployeeDetailsByEmployeeID(long employeeID)
        {
            var result = ClientFactory.SchoolServiceClient(CallContext).GetEmployeeDetailsByEmployeeID(employeeID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //for ClassTeachermap screen refresh button
        public JsonResult FillEditDatasAndSubjects(int IID, int classID, int sectionID)
        {
            var listDatas = ClientFactory.SchoolServiceClient(CallContext).FillEditDatasAndSubjects(IID, classID, sectionID);
            return Json(listDatas, JsonRequestBehavior.AllowGet);
        }

        #region ResponseModel
        public class ResponseModel
        {
            // [Bind(Prefix = "some_prefix")]
            public string Message { set; get; }
            public object Data { set; get; }
            public bool isError { get; set; }
        }
        #endregion Classess

        #region REFUND
        [HttpGet]
        public ActionResult FillCollectedFeesDetails(long studentId, int academicId)
        {
            var dateFormat = Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue("DateFormat");
            List<FeeCollectionDTO> data = ClientFactory.SchoolServiceClient(CallContext).FillCollectedFeesDetails(studentId, academicId);
            //DateTime settlementDate = new DateTime();
            var feeTypes = new List<RefundFeeTypeViewModel>();
            foreach (var dat in data)
            {
                var collectionFeeType = (from feeType in dat.FeeTypes
                                         select new RefundFeeTypeViewModel()
                                         {
                                             InvoiceNo = feeType.InvoiceNo,
                                             FeeDueAmount = feeType.Amount,
                                             FeePeriodID = feeType.FeePeriodID,
                                             StudentFeeDueID = feeType.StudentFeeDueID,
                                             FeeCollectionFeeTypeMapsIID = feeType.FeeCollectionFeeTypeMapsIID,
                                             CollectedAmount = feeType.CollectedAmount,
                                             CreditNote = feeType.CreditNoteAmount,
                                             FeeCollectionStatus = true,
                                             IsRefundable = feeType.IsRefundable,
                                             Refund = feeType.RefundAmount,
                                             Amount = feeType.ReceivableAmount - feeType.RefundAmount,
                                             FeeMaster = feeType.FeeMaster,
                                             FeePeriod = feeType.FeePeriodID.HasValue ? feeType.FeePeriod : null,
                                             FeeMasterID = feeType.FeeMasterID,
                                             InvoiceDateString = feeType.InvoiceDate == null ? "" : Convert.ToDateTime(feeType.InvoiceDate).ToString(dateFormat),
                                             InvoiceDate = feeType.InvoiceDate == null ? (DateTime?)null : Convert.ToDateTime(feeType.InvoiceDate),
                                             MonthlySplits = (from split in feeType.MontlySplitMaps
                                                              select new RefundSplitViewModel()
                                                              {
                                                                  Amount = split.Amount,
                                                                  MonthID = split.MonthID,
                                                                  Year = split.Year,
                                                                  PrvCollect = split.CollectedAmount,
                                                                  CreditNote = split.CreditNoteAmount,
                                                                  Balance = (split.ReceivableAmount ?? 0) - (split.RefundAmount ?? 0),
                                                                  Refund = split.RefundAmount,
                                                                  FeeCollectionStatus = split.CollectedAmount.HasValue ? true : false,
                                                                  MonthName = split.MonthID == 0 ? null : new DateTime(2010, split.MonthID, 1).ToString("MMM") + " " + split.Year
                                                              }).ToList(),
                                         }).ToList();



                feeTypes.AddRange(collectionFeeType);

            }


            return Json(feeTypes, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region CREDITNOTE
        [HttpGet]
        public ActionResult GetInvoiceForCreditNote(int classId, long studentId, int? feeMasterID, int? feePeriodID)
        {
            var data = ClientFactory.SchoolServiceClient(CallContext).GetInvoiceForCreditNote(classId, studentId, feeMasterID, feePeriodID);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetFeeDueMonthlyDetails(long studentFeeDueID, int? feeMasterID, int? feePeriodID)
        {
            var data = ClientFactory.SchoolServiceClient(CallContext).GetFeeDueMonthlyDetails(studentFeeDueID, feeMasterID, feePeriodID);
            var creditNoteFeeType = new CreditNoteFeeTypeViewModel();
            var months = new List<KeyValueViewModel>();
            var years = new List<KeyValueViewModel>();
            creditNoteFeeType.MonthList = new List<KeyValueViewModel>();
            creditNoteFeeType.YearList = new List<KeyValueViewModel>();
            //var feePeriod = new FeePeriodsViewModel();
            creditNoteFeeType.MonthSplitList = new List<FeeDueMonthlySplitViewModel>();
            foreach (var mnth in data)
            {
                if (mnth.MonthID != 0)
                {
                    months.Add(new KeyValueViewModel { Key = mnth.MonthID.ToString(), Value = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(mnth.MonthID).Substring(0, 3) });

                    if (!years.Any(x => x.Key == mnth.Year.ToString()))
                    {
                        years.Add(new KeyValueViewModel
                        {
                            Key = mnth.Year.ToString(),
                            Value = mnth.Year.ToString()
                        });
                    }
                }
                creditNoteFeeType.MonthSplitList.Add(
                        new FeeDueMonthlySplitViewModel
                        {
                            Year = mnth.Year,
                            MonthID = mnth.MonthID,
                            FeeDueMonthlySplitID = mnth.FeeDueMonthlySplitIID,
                            FeeDueFeeTypeMapsID = mnth.FeeDueFeeTypeMapsID,
                            Amount = mnth.Amount,
                        });

            }
            if (creditNoteFeeType.MonthSplitList.Count() == 1)
            {
                creditNoteFeeType.FeeDueFeeTypeMapsID = creditNoteFeeType.MonthSplitList[0].FeeDueFeeTypeMapsID;
                creditNoteFeeType.FeeDueMonthlySplitID = creditNoteFeeType.MonthSplitList[0].FeeDueMonthlySplitID;
            }
            creditNoteFeeType.MonthList.AddRange(months);
            creditNoteFeeType.YearList.AddRange(years);
            return Json(creditNoteFeeType, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetGridLookUpsForSchoolCreditNote(long studentId)
        {
            var data = ClientFactory.SchoolServiceClient(CallContext).GetGridLookUpsForSchoolCreditNote(studentId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region PROGRESS REPORT

        [HttpGet]
        public JsonResult GetProgressReportData(int classID, int? sectionID, int academicYearID)
        {
            var response = ClientFactory.SchoolServiceClient(CallContext).GetProgressReportData(classID, sectionID, academicYearID);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveProgressReportData(List<ProgressReportNewDTO> toDtoList)
        {
            var response = ClientFactory.SchoolServiceClient(CallContext).SaveProgressReportData(toDtoList);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdatePublishStatus(List<ProgressReportNewDTO> toDtoList)
        {
            var response = ClientFactory.SchoolServiceClient(CallContext).UpdatePublishStatus(toDtoList);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        #endregion
        public class Student_Co_Scholastic
        {
            public string AdmissionNumber { get; set; }
            public long StudentIID { get; set; }
            public string StudentName { get; set; }
            public int ClassID { get; set; }
            public string ClassDescription { get; set; }
            public int SectionID { get; set; }
            public string SectionName { get; set; }
        }

        public class SkillGroupMaster_Co_Scholastic
        {
            public int SkillGroupMasterID { get; set; }
            public string SkillGroup { get; set; }
        }

        public class Skill_Co_Scholastic
        {
            public long StudentIID { get; set; }
            public long ClassSubjectSkillGroupMapID { get; set; }
            public string SkillName { get; set; }
            public int MarkGradeID { get; set; }
            public string MarkGrade { get; set; }
            public int SkillMasterID { get; set; }
            public int ClassID { get; set; }
            public string ClassDescription { get; set; }
            public int SectionID { get; set; }
            public string SectionName { get; set; }
        }

        public class MAIL_LIST
        {
            public Int64 mailBoxID { get; set; }
            public Int64 fromID { get; set; }
            public Int64 toID { get; set; }
            public String mailSubject { get; set; }
            public String mailBody { get; set; }
            public String mailFolder { get; set; }
            public bool viewStatus { get; set; }
            public bool fromDelete { get; set; }
            public bool toDelete { get; set; }
            public DateTime onDate { get; set; }
            public String UserName { get; set; }

        }

        public ActionResult TCApproval(long studentTransferRequestID)
        {
            var vm = new StudentTransferRequestViewModel()
            {
                StudentTransferRequestIID = studentTransferRequestID,
            };

            ViewBag.StudentTransferRequestID = studentTransferRequestID;

            return View(vm);
        }

        public ActionResult GenerateTransferCertificate(TCApprovalViewModel tcApproval)
        {
            var operationResult = new OperationResultDTO();

            var tcDTO = new StudentTransferRequestDTO()
            {
                StudentTransferRequestIID = tcApproval.StudentTransferRequestIID,
                StudentID = tcApproval.StudentID,
            };

            if (tcDTO != null)
            {
                //Return TransferCertificate as Byte file
                //var contentData = ClientFactory.ReportGenerationServiceClient(CallContext).GenerateTransferRequestContentFile(tcDTO);
                ////Save TCPDF in Content Table 
                //var savedContentata = ClientFactory.ContentServicesClient(CallContext).SaveFile(contentData);

                var updateStatus = ClientFactory.SchoolServiceClient(CallContext).UpdateTCStatusToComplete(tcDTO.StudentTransferRequestIID);

                if (string.IsNullOrEmpty(updateStatus))
                {
                    return Json(new { IsError = true, Response = "Failed TC upload!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { IsError = false, Response = "TC uploaded successfully!" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { IsError = true, Response = "Failed TC upload!" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult FeePaymentValidationByTransaction(string transactionNumber, string mailID, int? collectionStatusID)
        {
            var paymentValidationResult = string.Empty;

            var feeCollectionDatas = ClientFactory.SchoolServiceClient(CallContext).GetFeeCollectionDetailsByTransactionNumber(transactionNumber, mailID);

            var totalAmountCollected = feeCollectionDatas != null && feeCollectionDatas.Count > 0 ? feeCollectionDatas.Sum(s => s.Amount) : 0;

            var draftFeeCollectionStatus = ClientFactory.SettingServiceClient(CallContext).GetSettingValueByKey("FEECOLLECTIONSTATUSID_DRAFT");
            var pendingCollectionStatusID = draftFeeCollectionStatus != null ? int.Parse(draftFeeCollectionStatus) : 1;

            if (collectionStatusID == pendingCollectionStatusID)
            {
                var paymentTransactionPrefix = ClientFactory.SettingServiceClient(CallContext).GetSettingValueByKey("ONLINE_PAYMENT_TRANSACTION_PREFIX");

                var transID = transactionNumber.Replace(paymentTransactionPrefix, "");

                paymentValidationResult = ClientFactory.PaymentGatewayServiceClient(CallContext).ValidatePaymentByTransaction(transID, totalAmountCollected);
            }

            ViewBag.FeeCollections = feeCollectionDatas;
            ViewBag.FeeCollectionStatusID = collectionStatusID;
            ViewBag.PendingFeeCollectionStatusID = pendingCollectionStatusID;
            ViewBag.TransactionNumber = transactionNumber;
            ViewBag.TotalAmount = totalAmountCollected;
            ViewBag.PaymentValidationStatus = paymentValidationResult;

            return View("OnlinePaymentValidation");
        }

        [HttpGet]
        public JsonResult CheckFeeCollectionExistingStatusByTransNo(string transactionNumber)
        {
            var feeCollectionStatus = ClientFactory.SchoolServiceClient(CallContext).CheckFeeCollectionStatusByTransactionNumber(transactionNumber);

            if (feeCollectionStatus != null)
            {
                return Json(new { IsError = true, Response = feeCollectionStatus }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { IsError = false, Response = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateFeeCollectionStatusByTransactionNo(string groupTransNumber)
        {
            var paymentUpdateDatas = new List<FeeCollectionDTO>();

            string status;
            bool isError;

            try
            {
                var paymentTransactionPrefix = ClientFactory.SettingServiceClient(CallContext).GetSettingValueByKey("ONLINE_PAYMENT_TRANSACTION_PREFIX");
                var transactionNo = groupTransNumber.Replace(paymentTransactionPrefix, "");

                paymentUpdateDatas = ClientFactory.SchoolServiceClient(CallContext).UpdateStudentsFeePaymentStatus(transactionNo);

                if (paymentUpdateDatas != null && paymentUpdateDatas.Count > 0)
                {
                    isError = false;
                    status = "Successfully updated";
                }
                else
                {
                    isError = true;
                    status = "Unable to update";
                }
            }
            catch (Exception ex)
            {
                isError = true;
                status = ex.Message;
            }

            if (isError == false)
            {
                try
                {
                    if (paymentUpdateDatas != null && paymentUpdateDatas.Count > 0)
                    {
                        //Send Mail
                        ClientFactory.ReportGenerationServiceClient(CallContext).GenerateFeeReceiptAndSendToMail(paymentUpdateDatas);
                    }
                }
                catch (Exception ex)
                {
                    var errorMessage = ex.Message;
                }

                return Json(new { IsError = false, Response = status }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { IsError = true, Response = status }, JsonRequestBehavior.AllowGet);
            }
        }

        #region Progress report pdf generation and saving
        public void GenerateAndSaveProgressReports(MarkRegisterDTO markRegisterDTO)
        {
            var fullSettings = ClientFactory.SettingServiceClient(CallContext).GetAllSettings();

            string reportName = GetProgressReportNameUsingClassName(markRegisterDTO.Class.Value, fullSettings);

            List<ProgressReportDTO> progressReportListDTOs = FillProgressReportDetails(markRegisterDTO, fullSettings, reportName);

            if (progressReportListDTOs.Count > 0)
            {
                var entryStatus = ClientFactory.SchoolServiceClient(CallContext).InsertProgressReportEntries(progressReportListDTOs, fullSettings);
            }
        }

        #region Get progress Report name from settings using class name
        public string GetProgressReportNameUsingClassName(string className, List<SettingDTO> settings)
        {
            var progressReportSettings = settings != null && settings.Count > 0 ? settings.Where(s => s.SettingCode.ToLower().Contains("progress_report_name")).ToList() : null;

            string reportName = null;

            if (className.ToLower().Contains("kg") && className.ToLower().Contains("1"))
            {
                reportName = progressReportSettings.FirstOrDefault(s => s.SettingCode == "PROGRESS_REPORT_NAME_CLASS_KG1")?.SettingValue;
            }

            else if (className.ToLower().Contains("kg") && className.ToLower().Contains("2"))
            {
                reportName = progressReportSettings.FirstOrDefault(s => s.SettingCode == "PROGRESS_REPORT_NAME_CLASS_KG2")?.SettingValue;
            }

            else if (className.ToLower().Contains("1") && !className.ToLower().Contains("11") && !className.ToLower().Contains("kg"))
            {
                reportName = progressReportSettings.FirstOrDefault(s => s.SettingCode == "PROGRESS_REPORT_NAME_CLASS_1")?.SettingValue;
            }

            else if (className.ToLower().Contains("2") && !className.ToLower().Contains("12") && !className.ToLower().Contains("kg"))
            {
                reportName = progressReportSettings.FirstOrDefault(s => s.SettingCode == "PROGRESS_REPORT_NAME_CLASS_2")?.SettingValue;
            }

            else if (className.ToLower().Contains("3"))
            {
                reportName = progressReportSettings.FirstOrDefault(s => s.SettingCode == "PROGRESS_REPORT_NAME_CLASS_3")?.SettingValue;
            }

            else if (className.ToLower().Contains("4"))
            {
                reportName = progressReportSettings.FirstOrDefault(s => s.SettingCode == "PROGRESS_REPORT_NAME_CLASS_4")?.SettingValue;
            }

            else if (className.ToLower().Contains("5"))
            {
                reportName = progressReportSettings.FirstOrDefault(s => s.SettingCode == "PROGRESS_REPORT_NAME_CLASS_5")?.SettingValue;
            }

            else if (className.ToLower().Contains("6"))
            {
                reportName = progressReportSettings.FirstOrDefault(s => s.SettingCode == "PROGRESS_REPORT_NAME_CLASS_6")?.SettingValue;
            }

            else if (className.ToLower().Contains("7"))
            {
                reportName = progressReportSettings.FirstOrDefault(s => s.SettingCode == "PROGRESS_REPORT_NAME_CLASS_7")?.SettingValue;
            }

            else if (className.ToLower().Contains("8"))
            {
                reportName = progressReportSettings.FirstOrDefault(s => s.SettingCode == "PROGRESS_REPORT_NAME_CLASS_8")?.SettingValue;
            }

            else if (className.ToLower().Contains("9"))
            {
                reportName = progressReportSettings.FirstOrDefault(s => s.SettingCode == "PROGRESS_REPORT_NAME_CLASS_9")?.SettingValue;
            }

            else if (className.ToLower().Contains("10"))
            {
                reportName = progressReportSettings.FirstOrDefault(s => s.SettingCode == "PROGRESS_REPORT_NAME_CLASS_10")?.SettingValue;
            }

            else if (className.ToLower().Contains("11"))
            {
                reportName = progressReportSettings.FirstOrDefault(s => s.SettingCode == "PROGRESS_REPORT_NAME_CLASS_11")?.SettingValue;
            }

            else if (className.ToLower().Contains("12"))
            {
                reportName = progressReportSettings.FirstOrDefault(s => s.SettingCode == "PROGRESS_REPORT_NAME_CLASS_12")?.SettingValue;
            }

            return reportName;
        }
        #endregion Get progress Report name

        public List<ProgressReportDTO> FillProgressReportDetails(MarkRegisterDTO markRegister, List<SettingDTO> settings, string reportName)
        {
            var progressReportListDTOs = new List<ProgressReportDTO>();

            var markEntryPublishedStatusSetting = settings != null && settings.Count > 0 ? settings.FirstOrDefault(s => s.SettingCode == "MARK_ENTRY_STATUS_PUBLISHED") : null;
            var markEntryPublishedStatusID = markEntryPublishedStatusSetting != null && !string.IsNullOrEmpty(markEntryPublishedStatusSetting.SettingValue) ? byte.Parse(markEntryPublishedStatusSetting.SettingValue) : (byte)3;

            var groupedPubllishedStudMarkRegDetails = markRegister.MarkRegistersDetails != null && markRegister.MarkRegistersDetails.Count > 0 ? markRegister.MarkRegistersDetails.Where(r => r.MarkEntryStatusID == markEntryPublishedStatusID).GroupBy(g => g.StudentID).ToList() : null;
            var groupedPubllishedStudSkillDetails = markRegister.MarkRegisterSkillGroupDTOs != null && markRegister.MarkRegisterSkillGroupDTOs.Count > 0 ? markRegister.MarkRegisterSkillGroupDTOs.Where(e => e.MarkEntryStatusID == markEntryPublishedStatusID).GroupBy(g => g.StudentID).ToList() : null;

            var progressReportDTO = new ProgressReportDTO();

            if (groupedPubllishedStudMarkRegDetails != null)
            {
                foreach (var publlishedStudMarkRegDetails in groupedPubllishedStudMarkRegDetails.ToList())
                {
                    var regDet = publlishedStudMarkRegDetails.ToList().Count > 0 ? publlishedStudMarkRegDetails.ToList().FirstOrDefault() : null;
                    if (regDet != null)
                    {
                        progressReportDTO = new ProgressReportDTO()
                        {
                            SchoolID = markRegister.SchoolID,
                            AcademicYearID = markRegister.AcademicYearID,
                            ClassID = markRegister.ClassID,
                            Class = markRegister.ClassID.HasValue ? new KeyValueDTO()
                            {
                                Key = markRegister.Class?.Key,
                                Value = markRegister.Class?.Value
                            } : new KeyValueDTO(),
                            SectionID = markRegister.SectionID,
                            ExamGroupID = markRegister.ExamGroupID,
                            ExamID = markRegister.ExamID,
                            StudentID = regDet.StudentID,
                            Student = regDet.StudentID.HasValue ? new KeyValueDTO()
                            {
                                Key = regDet.Student.Key,
                                Value = regDet.Student.Value
                            } : new KeyValueDTO(),
                        };

                        progressReportDTO.ReportContentID = GetProgressReportContentID(progressReportDTO, reportName);

                        progressReportListDTOs.Add(progressReportDTO);
                    }
                }
            }

            if (groupedPubllishedStudSkillDetails != null)
            {
                foreach (var publlishedStudSkillDetails in groupedPubllishedStudSkillDetails.ToList())
                {
                    var skillDet = publlishedStudSkillDetails.ToList().Count > 0 ? publlishedStudSkillDetails.ToList().FirstOrDefault() : null;
                    if (skillDet != null)
                    {
                        progressReportDTO = new ProgressReportDTO()
                        {
                            SchoolID = markRegister.SchoolID,
                            AcademicYearID = markRegister.AcademicYearID,
                            ClassID = markRegister.ClassID,
                            Class = markRegister.ClassID.HasValue ? new KeyValueDTO()
                            {
                                Key = markRegister.Class?.Key,
                                Value = markRegister.Class?.Value
                            } : new KeyValueDTO(),
                            SectionID = markRegister.SectionID,
                            ExamGroupID = markRegister.ExamGroupID,
                            ExamID = markRegister.ExamID,
                            StudentID = skillDet.StudentID,
                            Student = skillDet.StudentID.HasValue ? new KeyValueDTO()
                            {
                                Key = skillDet.StudentID.ToString(),
                                Value = skillDet.StudentName
                            } : new KeyValueDTO(),
                        };

                        progressReportDTO.ReportContentID = GetProgressReportContentID(progressReportDTO, reportName);

                        progressReportListDTOs.Add(progressReportDTO);
                    }
                }
            }

            return progressReportListDTOs;
        }

        public long? GetProgressReportContentID(ProgressReportDTO progressReportDTO, string reportName)
        {
            long? contentFileID = null;

            if (progressReportDTO != null)
            {
                //Return SalarySlipPDF as Byte file
                var contentData = ClientFactory.ReportGenerationServiceClient(CallContext).GenerateProgressReportContentFile(progressReportDTO, reportName);

                //Save SalarySlipPDF in Content Table 
                var savedContentData = ClientFactory.ContentServicesClient(CallContext).SaveFile(contentData);

                contentFileID = savedContentData != null ? savedContentData.ContentFileIID : (long?)null;
            }
            else
            {
                //response = new { IsFailed = 1, Message = salarydt.Message };
            }

            return contentFileID;
        }
        #endregion Progress report pdf generation and save End

    }
}