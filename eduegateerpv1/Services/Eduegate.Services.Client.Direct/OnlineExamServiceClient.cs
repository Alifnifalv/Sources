using System;
using System.Collections.Generic;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.OnlineExam.Exam;

namespace Eduegate.Services.Client.Direct
{
    public class OnlineExamServiceClient : IOnlineExamService
    {
        OnlineExamService service = new OnlineExamService();

        public OnlineExamServiceClient(CallContext context = null, Action<string> logger = null)
        {
            service.CallContext = context;
        }

        public string SaveCandidateFullAnswers(List<CandidateAnswerDTO> answerList)
        {
            return service.SaveCandidateFullAnswers(answerList);
        }

        public void AutoSaveMarksByCandidateAnswers(List<CandidateAnswerDTO> answerList)
        {
            service.AutoSaveMarksByCandidateAnswers(answerList);
        }

        public List<KeyValueDTO> GetOnlineExamsByCandidateAndAcademicYear(long candidateID, int academicYearID)
        {
            return service.GetOnlineExamsByCandidateAndAcademicYear(candidateID, academicYearID);
        }

        public OnlineExamResultDTO GetOnlineExamResults(long candidateID, long examID)
        {
            return service.GetOnlineExamResults(candidateID, examID);
        }

        public OperationResultDTO UpdateOnlineExamResult(OnlineExamResultDTO resultDTO)
        {
            return service.UpdateOnlineExamResult(resultDTO);
        }

        public List<KeyValueDTO> GetSubjectsByOnlineExam(long examID, int academicYearID, short? languageTypeID)
        {
            return service.GetSubjectsByOnlineExam(examID, academicYearID, languageTypeID);
        }

        public List<OnlineExamResultDTO> GetOnlineExamEntryResults(long examID, int academicYearID, short? languageTypeID, int? subjectID)
        {
            return service.GetOnlineExamEntryResults(examID, academicYearID, languageTypeID, subjectID);
        }

        public string SaveOnlineExamResultEntries(List<OnlineExamResultDTO> examResultDTOs)
        {
            return service.SaveOnlineExamResultEntries(examResultDTOs);
        }

        public string UpdateCandidateExamMap(long? candidateOnlinExamMapID, double durationInMinutes, byte examStartStatusID)
        {
            return service.UpdateCandidateExamMap(candidateOnlinExamMapID, durationInMinutes, examStartStatusID);
        }

        public List<OnlineExamQuestionDTO> GetQuestionsByExamID(long onlineExamID, long? candidateExamMapID, long? candidateID)
        {
            return service.GetQuestionsByExamID(onlineExamID, candidateExamMapID, candidateID);
        }

        public List<CandidateOnlineExamMapDTO> GetExamsDataByCandidateID(long candidateID)
        {
            return service.GetExamsDataByCandidateID(candidateID);
        }

        public string SaveCandidateAnswer(CandidateAnswerDTO answer)
        {
            return service.SaveCandidateAnswer(answer);
        }

        public OnlineExamsDTO GetOnlineExamDetailsByExamID(long onlineExamID)
        {
            return service.GetOnlineExamDetailsByExamID(onlineExamID);
        }

        public CandidateOnlineExamMapDTO GetCandidateOnlineExamMapDataByID(long candidateExamMapID)
        {
            return service.GetCandidateOnlineExamMapDataByID(candidateExamMapID);
        }

        public string UpdateStartTime(DateTime startDate, long candidateOnlineExamMapID)
        {
            return service.UpdateStartTime(startDate, candidateOnlineExamMapID);
        }

        public string UpdateCandidateExamMapStatus(CandidateOnlineExamMapDTO examMapData)
        {
            return service.UpdateCandidateExamMapStatus(examMapData);
        }

        public List<CandidateAnswerDTO> GetCandidateAnswersByMapData(CandidateOnlineExamMapDTO examMapData)
        {
            return service.GetCandidateAnswersByMapData(examMapData);
        }

        public OnlineQuestionGroupsDTO GetQnGroupDetailsByID(int qnGroupID)
        {
            return service.GetQnGroupDetailsByID(qnGroupID);
        }

    }
}