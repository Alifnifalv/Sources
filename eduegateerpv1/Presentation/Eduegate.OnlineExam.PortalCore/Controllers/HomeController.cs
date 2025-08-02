using Eduegate.Services.Contracts.OnlineExam.Exam;
using Eduegate.Web.Library.OnlineExam.Common;
using Eduegate.Web.Library.OnlineExam.OnlineExam;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Eduegate.Application.Mvc;
using Microsoft.AspNetCore.Http;
using Eduegate.Services.Client.Factory;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.OnlineExam.Models;

namespace Eduegate.OnlineExam.Portal.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public IActionResult Index()
        {
            if (GetCookieLoginId() == true)
            {
                GetQuestions();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        public ActionResult Test()
        {
            if (GetCookieLoginId() == true)
            {

            }
            else
            {
                return RedirectToAction("login", "account");
            }
            return View();
        }

        public IActionResult Exam()
        {
            List<Eduegate.Web.Library.OnlineExam.OnlineExam.ExamsListViewModel> exams = new List<Eduegate.Web.Library.OnlineExam.OnlineExam.ExamsListViewModel>();

            if (GetCookieLoginId())
            {
                long candidateID = Convert.ToInt64(HttpContext.Session.GetString("ExamCandidate"));

                var dataList = ClientFactory.OnlineExamServiceClient(CallContext).GetExamsDataByCandidateID(candidateID);

                var onlineExams = dataList.Select(data => new ExamsListViewModel
                {
                    CandidateID = data.CandidateID,
                    OnlineExamID = data.OnlineExamID,
                    OnlineExamName = data.OnlineExamName,
                    OnlineExamDescription = data.OnlineExamDescription,
                    Duration = data.Duration,
                    AdditionalTime = data.AdditionalTime,
                    TotalExamDuration = (data.Duration ?? 0) + (data.AdditionalTime ?? 0),
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
                    CandidateExamQuestionsTotalMarks = data.CandidateExamQuestionsMarks
                }).ToList();

                if (onlineExams.Any())
                {
                    exams = onlineExams;
                }

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

            return View(exams);
        }


        public ActionResult Result()
        {
            if (GetCookieLoginId() == true)
            {
                long candidateID = Convert.ToInt64(HttpContext.Session.GetString("ExamCandidate"));

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
            if (GetCookieLoginId() == true)
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
            if (GetCookieLoginId() == true)
            {
                long candidateID = Convert.ToInt64(HttpContext.Session.GetString("ExamCandidate"));

                long onlineExamID = examID.HasValue ? Convert.ToInt64(examID) : 0;

                long candidateExamMapID = candidateOnlinExamMapID.HasValue ? Convert.ToInt64(candidateOnlinExamMapID) : 0;

                var allOnlineExamQuestions = ClientFactory.OnlineExamServiceClient(CallContext).GetQuestionsByExamID(onlineExamID, candidateExamMapID, candidateID);

                var xmMapData = ClientFactory.OnlineExamServiceClient(CallContext).GetCandidateOnlineExamMapDataByID(candidateExamMapID);

                var examQuestions = new List<ExamQuestionViewModel>();

                var onlineExamQuestions = allOnlineExamQuestions.Where(a => a.IsPassageQn == false).ToList();

                var passageOnlineExamQuestions = allOnlineExamQuestions.Where(a => a.IsPassageQn == true).ToList();

                foreach (var question in allOnlineExamQuestions.Where(a => !a.IsPassageQn))
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
                        IsPassageQn = question.IsPassageQn,
                        DocFile = question.DocFile,
                    });
                }

                var groupedQuestions = allOnlineExamQuestions
                    .Where(q => q.IsPassageQn)
                    .GroupBy(q => q.PassageQuestion).ToList();

                foreach (var group in groupedQuestions)
                {
                    // You can access the key of the group (the PassageQuestion)
                    var passageQuestion = group.Key;

                    // Create a list for the grouped questions
                    var groupedExamQuestions = new List<ExamQuestionViewModel>();
                    var passageQuestions = new List<ExamQuestionViewModel>();

                    foreach (var question in group.ToList())
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

                        passageQuestions.Add(new ExamQuestionViewModel()
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
                            IsPassageQn = question.IsPassageQn,
                            DocFile = question.DocFile,
                        });
                    }

                    //foreach (var question in passageQuestions)
                    //{
                    //    var passageqn = question.PassageQuestion;
                    groupedExamQuestions.Add(new ExamQuestionViewModel()
                    {
                        Question = group.Key,
                        //QuestionOptions = question.QuestionOptions,
                        PassageQuestions = passageQuestions,
                        IsPassageQn = true,
                    });
                    //}


                    examQuestions.AddRange(groupedExamQuestions);
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
            if (GetCookieLoginId() == true)
            {

            }
            return Json(ResModel);
        }

        public JsonResult GetQuestions()
        {
            var ResModel = new ResponseModel();
            if (GetCookieLoginId() == true)
            {
                long candidateID = Convert.ToInt64(HttpContext.Session.GetString("ExamCandidate"));

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

            return Json(ResModel);
        }

        private bool GetCookieLoginId()
        {
            try
            {
                string sessionID = HttpContext.Session.GetString("ExamCandidate");

                if (string.IsNullOrEmpty(sessionID))
                {
                    string cookieLoginID = Request.Cookies["ExamCandidateCookie"];

                    if (!string.IsNullOrEmpty(cookieLoginID))
                    {
                        HttpContext.Session.SetString("ExamCandidate", cookieLoginID);
                        return true;
                    }
                    else
                    {
                        return false;

                    }
                }
                else
                {
                    if (int.TryParse(sessionID, out int result) && result == 0)
                    {
                        HttpContext.Session.Remove("ExamCandidate");
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception ex)
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

            return Json(message);
        }
        #endregion

        public ActionResult CheckExamQuestionAvailability(long? examID, long? candidateOnlinExamMapID)
        {
            try
            {
                long candidateID = Convert.ToInt64(HttpContext.Session.GetString("ExamCandidate"));

                long onlineExamID = examID.HasValue ? Convert.ToInt64(examID) : 0;

                var onlineExamQuestions = ClientFactory.OnlineExamServiceClient(CallContext).GetQuestionsByExamID(onlineExamID, candidateOnlinExamMapID, candidateID);

                if (onlineExamQuestions.Count == 0)
                {
                    return Json(new { IsError = true, Response = "We apologize, but there are no questions available for this exam at the moment. Please check back later or contact the exam administrator for further assistance!" });
                }
                else
                {
                    return Json(new { IsError = false, Response = "Questions available" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { IsError = true, Response = ex.Message });
            }
        }

        public ActionResult InsertExamMapStartEndTime(long? candidateOnlinExamMapID, double durationInMinutes)
        {
            var startStatus = ClientFactory.SettingServiceClient(CallContext).GetSettingValueByKey("ONLINE_EXAM_STATUSID_START");
            byte examStartStatusID = startStatus != null ? byte.Parse(startStatus) : (byte)3;

            var result = ClientFactory.OnlineExamServiceClient(CallContext).UpdateCandidateExamMap(candidateOnlinExamMapID, durationInMinutes, examStartStatusID);

            if (result == null)
            {
                return Json(new { IsError = true, Response = "Something went wrong, try again later!" });
            }
            else
            {
                return Json(new { IsError = false, Response = "" });
            }
        }

        [HttpPost]
        public ActionResult SaveCandidateAnswer([FromBody] CandidateAnswerDTO answer)
        {
            var message = "";

            message = ClientFactory.OnlineExamServiceClient(CallContext).SaveCandidateAnswer(answer);

            if (message == null)
            {
                return new JsonResult(new { IsError = true, Response = "Answer not saved!" });
            }
            else
            {
                return new JsonResult(new { IsError = false, Response = message });
            }
        }


        public IActionResult SyncServerTime()
        {
            DateTime localTime = DateTime.Now;

            return new JsonResult(localTime);
        }


        [HttpPost]
        public IActionResult UpdateCandidateExamMapStatus([FromBody] CandidateOnlineExamMapDTO examMapData)
        {
            var message = ClientFactory.OnlineExamServiceClient(CallContext).UpdateCandidateExamMapStatus(examMapData);

            List<CandidateAnswerDTO> answerDtoList = ClientFactory.OnlineExamServiceClient(CallContext).GetCandidateAnswersByMapData(examMapData);

            ClientFactory.OnlineExamServiceClient(CallContext).AutoSaveMarksByCandidateAnswers(answerDtoList);

            return new JsonResult(message);
        }

        public ActionResult Home()
        {
            return View();
        }

        public CandidateDTO GetCandidateDetails()
        {
            var listData = new CandidateDTO();

            if (GetCookieLoginId() == true)
            {
                long candidateID = Convert.ToInt64(HttpContext.Session.GetString("ExamCandidate"));

                listData = ClientFactory.SchoolServiceClient(CallContext).GetCandidateDetailsByCandidateID(candidateID);
            }

            return listData;
        }
    }
}