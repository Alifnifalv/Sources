using Eduegate.Frameworks.Mvc.Controllers;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.OnlineExam.Exam;
using Eduegate.Web.Library.OnlineExam.Common;
using Eduegate.Web.Library.OnlineExam.OnlineExam;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eduegate.OnlineExam.Portal.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            if (getcookieloginid() == true)
            {
                GetQuestions();
            }
            else
            {
                return RedirectToAction("login", "account");
            }
            return View();
        }

        public ActionResult Test()
        {
            if (getcookieloginid() == true)
            {

            }
            else
            {
                return RedirectToAction("login", "account");
            }
            return View();
        }

        public ActionResult Exam()
        {
            if (getcookieloginid() == true)
            {
                long candidateID = Convert.ToInt64(Session["ExamCandidate"].ToString());

                var dataList = ClientFactory.OnlineExamServiceClient(CallContext).GetExamsDataByCandidateID(candidateID);

                var onlineExams = new List<ExamsListViewModel>();

                foreach (var data in dataList)
                {
                    onlineExams.Add(new ExamsListViewModel()
                    {
                        CandidateID = data.CandidateID,
                        OnlineExamID = data.OnlineExamID,
                        OnlineExamName = data.OnlineExamName,
                        OnlineExamDescription = data.OnlineExamDescription,
                        Duration = data.Duration,
                        AdditionalTime = data.AdditionalTime,
                        TotalExamDuration = data.Duration.HasValue ? (data.Duration + (data.AdditionalTime.HasValue ? data.AdditionalTime : 0)) : 0,
                        OnlineExamStatusID = data.OnlineExamStatusID,
                        OnlineExamStatusName = data.OnlineExamStatusName,
                        OnlineExamOperationStatusID = data.OnlineExamOperationStatusID,
                        OnlineExamOperationStatusName = data.OnlineExamOperationStatusName,
                        QuestionSelectionID = data.OnlineExamDTO.QuestionSelectionID,
                        QuestionSelectionName = data.OnlineExamDTO.QuestionSelectionName,
                        MinimumDuration = data.OnlineExamDTO.MinimumDuration,
                        MaximumDuration = data.OnlineExamDTO.MaximumDuration,
                        PassPercentage = data.OnlineExamDTO.PassPercentage,
                        PassNos = data.OnlineExamDTO.PassNos,
                        MinimumMarks = data.OnlineExamDTO.MinimumMarks,
                        MaximumMarks = data.OnlineExamDTO.MaximumMarks,
                        ClassID = data.OnlineExamDTO.ClassID,
                        CandidateOnlinExamMapID = data.CandidateOnlinExamMapIID,
                        IsCandidateConductedExam = data.IsCandidateConductedExam,
                        ExamStartTime = data.ExamStartTime,
                        ExamEndTime = data.ExamEndTime,
                        CandidateExamQuestionsTotalMarks = data.CandidateExamQuestionsMarks,
                    });
                }

                if (onlineExams.Count() > 0)
                {
                    ViewBag.ExamsList = onlineExams;
                }

            }
            else
            {
                return RedirectToAction("login", "account");
            }

            return View();
        }

        public ActionResult Result()
        {
            if (getcookieloginid() == true)
            {
                long candidateID = Convert.ToInt64(Session["ExamCandidate"].ToString());

                var dataList = ClientFactory.SchoolServiceClient(CallContext).GetOnlineExamsResultByCandidateID(candidateID);

                var onlineExamResults = new List<OnlineExamResultViewModel>();

                foreach (var data in dataList)
                {
                    onlineExamResults.Add(new OnlineExamResultViewModel()
                    {
                        OnlineExamResultIID = data.OnlineExamResultIID,
                        MarksObtained = data.Marks,
                        CandidateID = data.CandidateID,
                        Remarks = data.Remarks,
                        AcademicYearID = data.AcademicYearID,
                        AcademicYear = data.AcademicYear,
                        SchoolID = data.SchoolID,
                        SchoolName = data.SchoolName,
                        OnlineExamID = data.OnlineExamID,
                        OnlineExam = data.OnlineExamID.HasValue ? new KeyValueViewModel() { Key = data.OnlineExamID.ToString(), Value = data.OnlineExamName } : new KeyValueViewModel(),
                    });
                }

                if (onlineExamResults.Count() > 0)
                {
                    ViewBag.ExamResults = onlineExamResults;
                }
            }
            else
            {
                return RedirectToAction("login", "account");
            }
            return View();
        }

        public ActionResult Index2()
        {
            if (getcookieloginid() == true)
            {

            }
            else
            {
                return RedirectToAction("login", "account");
            }
            return View();
        }

        public ActionResult Questions(long? examID, long? candidateOnlinExamMapID)
        {
            if (getcookieloginid() == true)
            {
                long candidateID = Convert.ToInt64(Session["ExamCandidate"].ToString());

                long onlineExamID = examID.HasValue ? Convert.ToInt64(examID) : 0;

                long candidateExamMapID = candidateOnlinExamMapID.HasValue ? Convert.ToInt64(candidateOnlinExamMapID) : 0;

                var onlineExamQuestions = ClientFactory.OnlineExamServiceClient(CallContext).GetQuestionsByExamID(onlineExamID, candidateExamMapID, candidateID);

                var xmMapData = ClientFactory.OnlineExamServiceClient(CallContext).GetCandidateOnlineExamMapDataByID(candidateExamMapID);

                var examQuestions = new List<ExamQuestionViewModel>();

                foreach (var question in onlineExamQuestions)
                {
                    var examQuestionsOptions = new List<OnlineQuestionOptionsViewModel>();

                    if (question.QuestionOptionMaps != null)
                    {
                        foreach (var option in question.QuestionOptionMaps)
                        {
                            examQuestionsOptions.Add(new OnlineQuestionOptionsViewModel()
                            {
                                QuestionOptionMapIID = option.QuestionOptionMapIID,
                                OptionText = option.OptionText,
                                QuestionID = option.QuestionID,
                                ImageName = option.ImageName,
                                ContentID = option.ContentID,
                                IsSelected = option.IsSelected,
                            });
                        }
                    }
                    examQuestions.Add(new ExamQuestionViewModel()
                    {
                        OnlineExamQuestionIID = question.OnlineExamQuestionIID,
                        CandidateID = question.CandidateID.HasValue ? question.CandidateID : candidateID,
                        OnlineExamIID = question.OnlineExamID,
                        ExamName = question.ExamName,
                        ExamDescription = question.ExamDescription,
                        GroupName = question.GroupName,
                        QuestionIID = question.QuestionID,
                        Question = question.Question,
                        AnswerType = question.AnswerType,
                        QuestionOptionCount = question.QuestionOptionCount,
                        ExamMaximumDuration = question.ExamMaximumDuration,
                        QuestionOptions = examQuestionsOptions,
                        QuestionAnswer = question.QuestionAnswer,
                        CandidateOnlinExamMapID = candidateOnlinExamMapID,
                    });
                }

                if (examQuestions.Count() > 0)
                {
                    ViewBag.ExamQuestions = examQuestions;
                    ViewBag.ExamMapData = xmMapData;
                }
            }
            else
            {
                return RedirectToAction("login", "account");
            }
            return View();
        }

        public JsonResult GetExams()
        {
            var ResModel = new ResponseModel();
            if (getcookieloginid() == true)
            {
                //var emailDetails = ClientFactory.SchoolServiceClient(CallContext).GetTeacherEmailByParentLoginID(loginID);
                //using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbEduegateERPContext"].ConnectionString))
                //{
                //    try
                //    {
                //        var reader = conn.QueryMultiple("[exam].[SP_GET_EXAMS_BY_Candidate]",
                //                       param: new { CandidateID = Convert.ToInt64(Session["ExamCandidate"].ToString()) },
                //                     commandType: CommandType.StoredProcedure);
                //        var examsLists = reader.Read<GetExamsList>().ToList();
                //        if (examsLists.Count() > 0)
                //        {
                //            ResModel.Data = new
                //            {
                //                examsLists = examsLists
                //            };
                //            string message = string.Format("");
                //        }
                //    }
                //    catch (Exception e)
                //    {
                //        string message = string.Format(e.Message + ", " + e.InnerException.Message);
                //        ResModel.Message = message;
                //        ResModel.Data = "";
                //    }
                //}
            }
            return Json(ResModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetQuestions()
        {
            var ResModel = new ResponseModel();
            if (getcookieloginid() == true)
            {
                long candidateID = Convert.ToInt64(Session["ExamCandidate"].ToString());

                var listData = ClientFactory.SchoolServiceClient(CallContext).GetQuestionsByCandidateID(candidateID);

                var examQuestions = new List<ExamQuestionViewModel>();

                foreach (var data in listData)
                {
                    examQuestions.Add(new ExamQuestionViewModel()
                    {
                        OnlineExamQuestionIID = data.OnlineExamQuestionIID,
                        CandidateID = Convert.ToInt64(data.CandidateID),
                        OnlineExamIID = Convert.ToInt64(data.OnlineExamID),
                        ExamName = data.ExamName,
                        ExamDescription = data.ExamDescription,
                        GroupName = data.GroupName,
                        QuestionIID = Convert.ToInt64(data.QuestionID),
                        Question = data.Question,
                        AnswerType = data.AnswerType,
                        QuestionOptionCount = Convert.ToInt64(data.QuestionOptionCount),
                    });
                }

                if (examQuestions.Count() > 0)
                {
                    ResModel.Data = new
                    {
                        examQuestions = examQuestions
                    };
                    string message = string.Format("");
                }
            }

            return Json(ResModel, JsonRequestBehavior.AllowGet);
        }

        private bool getcookieloginid()
        {
            String SessionID = "";
            try
            {
                SessionID = Session["ExamCandidate"] != null ? Session["ExamCandidate"].ToString() : null;

                if (SessionID != null || SessionID != "")
                {
                    if (int.Parse(SessionID) == 0)
                    {
                        SessionID = null;
                    }
                }
            }
            catch { }
            String getcookieID = SessionID;
            if (SessionID == null || SessionID == "")
            {
                try
                {
                    HttpCookie SessionLoginID = (HttpCookie)Request.Cookies["ExamCandidateCookie"];

                    if (SessionLoginID != null)
                    {
                        for (int i = 0; i < SessionLoginID.Values.Count; i++)
                        {
                            getcookieID = SessionLoginID.Values[0].ToString();
                            break;
                        }
                    }
                }
                catch { }
                Session["ExamCandidate"] = getcookieID;
            }
            if (getcookieID != null && getcookieID != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region candidate answer saving old code
        [HttpPost]
        public ActionResult SaveCandidateFullAnswers(List<CandidateAnswerViewModel> candidateAnswers)
        {
            var message = "";
            var answerDtoList = new List<CandidateAnswerDTO>();

            foreach (var answer in candidateAnswers)
            {
                answerDtoList.Add(new CandidateAnswerDTO()
                {
                    CandidateAnswerIID = answer.CandidateAnswerIID,
                    CandidateID = answer.CandidateID,
                    CandidateOnlineExamMapID = answer.CandidateOnlineExamMapID,
                    DateOfAnswer = answer.DateOfAnswer,
                    Comments = answer.Comments,
                    QuestionOptionMapID = answer.QuestionOptionMapID,
                    OtherDetails = answer.OtherDetails,
                    OtherAnswers = answer.OtherAnswers,
                    OnlineExamID = answer.OnlineExamID,
                    OnlineExamQuestionID = answer.OnlineExamQuestionID
                });
            }

            if (answerDtoList.Count > 0)
            {
                message = ClientFactory.OnlineExamServiceClient(CallContext).SaveCandidateFullAnswers(answerDtoList);

                ClientFactory.OnlineExamServiceClient(CallContext).AutoSaveMarksByCandidateAnswers(answerDtoList);
            }

            return Json(message, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public ActionResult CheckExamQuestionAvailability(long? examID, long? candidateOnlinExamMapID)
        {
            try
            {
                long candidateID = Convert.ToInt64(Session["ExamCandidate"].ToString());

                long onlineExamID = examID.HasValue ? Convert.ToInt64(examID) : 0;

                var onlineExamQuestions = ClientFactory.OnlineExamServiceClient(CallContext).GetQuestionsByExamID(onlineExamID, candidateOnlinExamMapID, candidateID);

                if (onlineExamQuestions.Count == 0)
                {
                    return Json(new { IsError = true, Response = "We apologize, but there are no questions available for this exam at the moment. Please check back later or contact the exam administrator for further assistance!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { IsError = false, Response = "Questions available" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { IsError = true, Response = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult InsertExamMapStartEndTime(long? candidateOnlinExamMapID, double durationInMinutes)
        {
            var startStatus = ClientFactory.SettingServiceClient(CallContext).GetSettingValueByKey("ONLINE_EXAM_STATUSID_START");
            byte examStartStatusID = startStatus != null ? byte.Parse(startStatus) : (byte)3;

            var result = ClientFactory.OnlineExamServiceClient(CallContext).UpdateCandidateExamMap(candidateOnlinExamMapID, durationInMinutes, examStartStatusID);

            if (result == null)
            {
                return Json(new { IsError = true, Response = "Something went wrong, try again later!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { IsError = false, Response = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult SaveCandidateAnswer(CandidateAnswerViewModel answer)
        {
            var message = "";
            var answerDto = new CandidateAnswerDTO()
            {
                CandidateAnswerIID = answer.CandidateAnswerIID,
                CandidateID = answer.CandidateID,
                CandidateOnlineExamMapID = answer.CandidateOnlineExamMapID,
                DateOfAnswer = answer.DateOfAnswer,
                Comments = answer.Comments,
                QuestionOptionMapID = answer.QuestionOptionMapID,
                OtherDetails = answer.OtherDetails,
                OtherAnswers = answer.OtherAnswers,
                OnlineExamID = answer.OnlineExamID,
                OnlineExamQuestionID = answer.OnlineExamQuestionID,
                QuestionOptionMapIDs = answer.QuestionOptionMapIDs
            };
            
            message = ClientFactory.OnlineExamServiceClient(CallContext).SaveCandidateAnswer(answerDto);

            if (message == null)
            {
                return Json(new { IsError = true, Response = "Answer not saved!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { IsError = false, Response = message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SyncServerTime()
        {
            DateTime localTime = DateTime.UtcNow;

            return Json(localTime, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateCandidateExamMapStatus(CandidateOnlineExamMapDTO examMapData)
        {
            var message = ClientFactory.OnlineExamServiceClient(CallContext).UpdateCandidateExamMapStatus(examMapData);

            List<CandidateAnswerDTO> answerDtoList = ClientFactory.OnlineExamServiceClient(CallContext).GetCandidateAnswersByMapData(examMapData);

            ClientFactory.OnlineExamServiceClient(CallContext).AutoSaveMarksByCandidateAnswers(answerDtoList);

            return Json(message, JsonRequestBehavior.AllowGet);
        }

    }
}