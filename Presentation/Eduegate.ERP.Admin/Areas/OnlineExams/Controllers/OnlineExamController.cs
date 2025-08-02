using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.OnlineExam.Exam;
using Eduegate.Web.Library.OnlineExam.OnlineExam;
using Eduegate.Web.Library.ViewModels;

namespace Eduegate.ERP.Admin.Areas.OnlineExams.Controllers
{
    public class OnlineExamController : BaseSearchController
    {
        // GET: Payroll/Employee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OnlineExamResult()
        {
            //var vm = new EmployeeTimeSheetApprovalViewModel();

            //return View(vm);
            return View();
        }

        [HttpGet]
        public JsonResult GetOnlineExamsByCandidateAndAcademicYear(long candidateID, int academicYearID)
        {
            try
            {
                var examList = ClientFactory.OnlineExamServiceClient(CallContext).GetOnlineExamsByCandidateAndAcademicYear(candidateID, academicYearID);
                if (examList == null || examList.Count == 0)
                {
                    return Json(new { IsError = true, Response = examList }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { IsError = false, Response = examList }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new { IsError = true, Response = "Something went wrong" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetOnlineExamResults(long candidateID, long examID)
        {
            var result = ClientFactory.OnlineExamServiceClient(CallContext).GetOnlineExamResults(candidateID, examID);

            if (result != null)
            {
                var examResultQuestionMaps = new List<OnlineExamResultQuestionViewModel>();

                foreach (var map in result.OnlineExamResultQuestionMaps)
                {
                    var qnOptions = new List<OnlineQuestionOptionsViewModel>();
                    foreach (var option in map.QuestionOptionMaps)
                    {
                        qnOptions.Add(new OnlineQuestionOptionsViewModel()
                        {
                            QuestionOptionMapIID = option.QuestionOptionMapIID,
                            OptionText = option.OptionText,
                            IsSelected = option.IsSelected,
                        });
                    }

                    examResultQuestionMaps.Add(new OnlineExamResultQuestionViewModel()
                    {
                        OnlineExamResultQuestionMapIID = map.OnlineExamResultQuestionMapIID,
                        OnlineExamResultID = map.OnlineExamResultID,
                        QuestionID = map.QuestionID,
                        Question = map.Question,
                        Marks = map.Mark,
                        OldMarks = map.Mark,
                        Remarks = map.Remarks,
                        OldRemarks = map.Remarks,
                        EntryType = map.EntryType,
                        SelectedOptionID = map.SelectedOptionID,
                        SelectedOption = map.SelectedOption,
                        CreatedBy = map.CreatedBy,
                        CreatedDate = map.CreatedDate,
                        QuestionOptions = qnOptions,
                        IsExpand = false,
                        TotalMarksOfQuestion = map.TotalMarksOfQuestion,
                        CandidateTextAnswer = map.CandidateTextAnswer,
                    });
                }

                var obtainedPercentage = result.Marks / examResultQuestionMaps.Sum(s => s.TotalMarksOfQuestion) * 100;

                var examResult = new OnlineExamResultViewModel()
                {
                    IsExpand = false,
                    OnlineExamResultIID = result.OnlineExamResultIID,
                    MarksObtained = result.Marks,
                    CandidateID = result.CandidateID,
                    Candidate = result.CandidateID.HasValue ? new KeyValueViewModel()
                    {
                        Key = result.CandidateID.ToString(),
                        Value = result.CandidateName
                    } : new KeyValueViewModel(),
                    Remarks = result.Remarks,
                    OnlineExamID = result.OnlineExamID,
                    OnlineExam = result.OnlineExamID.HasValue ? new KeyValueViewModel()
                    {
                        Key = result.OnlineExamID.ToString(),
                        Value = result.OnlineExamName
                    } : new KeyValueViewModel(),
                    ResultStatusID = result.ResultStatusID,
                    ResultStatus = result.ResultStatusID.HasValue ? new KeyValueViewModel()
                    {
                        Key = result.ResultStatusID.ToString(),
                        Value = result.ResultStatus
                    } : new KeyValueViewModel(),
                    AcademicYearID = result.AcademicYearID,
                    AcademicYear = result.AcademicYear,
                    SchoolID = result.SchoolID,
                    SchoolName = result.SchoolName,
                    CreatedBy = result.CreatedBy,
                    CreatedDate = result.CreatedDate,
                    QuestionMapResults = examResultQuestionMaps,
                    TotalMarks = examResultQuestionMaps.Sum(s => s.TotalMarksOfQuestion),
                    ObtainedMarksPercentage = obtainedPercentage != 0 ? Math.Round(Convert.ToDecimal(obtainedPercentage), 2) : 0,
                };

                if (examResult != null)
                {
                    return Json(new { IsError = false, Response = examResult }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { IsError = true, Response = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { IsError = true, Response = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UpdateOnlineExamResult(OnlineExamResultViewModel examResult)
        {
            var resultQuestionMaps = new List<OnlineExamResultQuestionMapDTO>();

            foreach (var map in examResult.QuestionMapResults)
            {
                var entryType = map.EntryType;

                if (map.OldMarks != map.Marks || map.OldRemarks != map.Remarks)
                {
                    entryType = "Manual";
                }

                resultQuestionMaps.Add(new OnlineExamResultQuestionMapDTO()
                {
                    OnlineExamResultQuestionMapIID = map.OnlineExamResultQuestionMapIID,
                    OnlineExamResultID = map.OnlineExamResultID,
                    QuestionID = map.QuestionID,
                    Mark = map.Marks,
                    Remarks = map.Remarks,
                    EntryType = entryType,
                    CreatedBy = map.CreatedBy,
                    CreatedDate = map.CreatedDate
                });
            }

            var resultDTO = new OnlineExamResultDTO()
            {
                OnlineExamResultIID = examResult.OnlineExamResultIID,
                Marks = examResult.MarksObtained,
                CandidateID = examResult.CandidateID,
                Remarks = examResult.Remarks,
                OnlineExamID = examResult.OnlineExamID,
                ResultStatusID = examResult.ResultStatus == null || string.IsNullOrEmpty(examResult.ResultStatus.Key) ? (byte?)null : byte.Parse(examResult.ResultStatus.Key),
                AcademicYearID = examResult.AcademicYearID,
                SchoolID = examResult.SchoolID,
                CreatedBy = examResult.CreatedBy,
                CreatedDate = examResult.CreatedDate,
                OnlineExamResultQuestionMaps = resultQuestionMaps
            };

            var message = ClientFactory.OnlineExamServiceClient(CallContext).UpdateOnlineExamResult(resultDTO);

            if (message.operationResult == OperationResult.Error)
            {
                return Json(new { IsError = true, Response = message.Message }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { IsError = false, Response = message.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetSubjectsByOnlineExam(long examID, int academicYearID, short? languageTypeID)
        {
            var response = ClientFactory.OnlineExamServiceClient(CallContext).GetSubjectsByOnlineExam(examID, academicYearID, languageTypeID);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        #region Online Exam Result Entry
        public ActionResult OnlineExamResultEntry()
        {
            return View();
        }

        public ActionResult GetOnlineExamEntryResults(long examID, int academicYearID, short? languageTypeID, int? subjectID)
        {
            var entry = ClientFactory.OnlineExamServiceClient(CallContext).GetOnlineExamEntryResults(examID, academicYearID, languageTypeID, subjectID);

            var response = (from s in entry
                            select new CandidateResultEntryViewModel
                            {
                                CandidateID = s.CandidateID.Value,
                                CandidateName = s.CandidateName,
                                Remarks = s.Remarks,
                                MaxMark = s.MaxMark,
                                ResultSubjects = (from sg in s.OnlineExamResultSubjectMapDTOs
                                                  select new CandidateResultSubjectViewModel
                                                  {
                                                      Marks = sg.Marks,
                                                      SubjectID = sg.SubjectID,
                                                      SubjectName = sg.SubjectName
                                                  }).ToList()

                            }).ToList();

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveOnlineExamResultEntries(OnlineExamResultViewModel model)
        {
            string rtnMsg = "0#Something went wrong!";

            if (model.CandidateResultEntry.Count == 0)
            {
                rtnMsg = "0#Please enter mark details for at least a student!";
            }
            else
            {
                var resultEntries = (from x in model.CandidateResultEntry
                                   where x.ResultSubjects.Any(sk => (sk.Marks != null || x.Remarks != null))
                                   select new OnlineExamResultDTO
                                   {
                                       AcademicYearID = model.AcademicYearID,
                                       OnlineExamID = model.OnlineExamID,
                                       CandidateID = x.CandidateID,
                                       Remarks = x.Remarks,
                                       OnlineExamResultSubjectMapDTOs = (from y in x.ResultSubjects
                                                                         select new OnlineExamResultSubjectMapDTO
                                                                         {
                                                                             SubjectID = y.SubjectID,
                                                                             Marks = y.Marks

                                                                         }).ToList()
                                   }).ToList();

                if (resultEntries.Count > 0)
                {
                    rtnMsg = ClientFactory.OnlineExamServiceClient(CallContext).SaveOnlineExamResultEntries(resultEntries);
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
        #endregion

        [HttpGet]
        public JsonResult GetOnlineExamDetailsByExamID(long onlineExamID)
        {
            var result = ClientFactory.OnlineExamServiceClient(CallContext).GetOnlineExamDetailsByExamID(onlineExamID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetQnGroupDetailsByID(int qnGroupID)
        {
            var groupData = ClientFactory.OnlineExamServiceClient(CallContext).GetQnGroupDetailsByID(qnGroupID);

            if (groupData != null)
            {
                return Json(new { IsError = false, Response = groupData }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { IsError = true, Response = "No data found!" }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}