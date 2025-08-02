using System;
using System.Collections.Generic;
using Eduegate.Framework;
using Eduegate.Services.Contracts;
using Eduegate.Service.Client;
using Eduegate.Services.Contracts.OnlineExam.Exam;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Commons;

namespace Eduegate.Services.Client
{
    public class OnlineExamServiceClient : BaseClient, IOnlineExamService
    {
        public OnlineExamServiceClient(CallContext context = null, Action<string> logger = null)
            : base(context, logger)
        {
        }

        public string SaveCandidateFullAnswers(List<CandidateAnswerDTO> answerList)
        {
            throw new NotImplementedException();
        }

        public void AutoSaveMarksByCandidateAnswers(List<CandidateAnswerDTO> answerList)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetOnlineExamsByCandidateAndAcademicYear(long candidateID, int academicYearID)
        {
            throw new NotImplementedException();
        }

        public OnlineExamResultDTO GetOnlineExamResults(long candidateID, long examID)
        {
            throw new NotImplementedException();
        }

        public OperationResultDTO UpdateOnlineExamResult(OnlineExamResultDTO resultDTO)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetSubjectsByOnlineExam(long examID, int academicYearID, short? languageTypeID)
        {
            throw new NotImplementedException();
        }

        public List<OnlineExamResultDTO> GetOnlineExamEntryResults(long examID, int academicYearID, short? languageTypeID, int? subjectID)
        {
            throw new NotImplementedException();
        }

        public string SaveOnlineExamResultEntries(List<OnlineExamResultDTO> examResultDTOs)
        {
            throw new NotImplementedException();
        }

        public string UpdateCandidateExamMap(long? candidateOnlinExamMapID, double durationInMinutes, byte examStartStatusID)
        {
            throw new NotImplementedException();
        }

        public List<OnlineExamQuestionDTO> GetQuestionsByExamID(long onlineExamID, long? candidateExamMapID, long? candidateID)
        {
            throw new NotImplementedException();
        }

        public List<CandidateOnlineExamMapDTO> GetExamsDataByCandidateID(long candidateID)
        {
            throw new NotImplementedException();
        }

        public string SaveCandidateAnswer(CandidateAnswerDTO answer)
        {
            throw new NotImplementedException();
        }

        public OnlineExamsDTO GetOnlineExamDetailsByExamID(long onlineExamID)
        {
            throw new NotImplementedException();
        }

        public CandidateOnlineExamMapDTO GetCandidateOnlineExamMapDataByID(long candidateExamMapID)
        {
            throw new NotImplementedException();
        }

        public string UpdateStartTime(DateTime startDate, long candidateOnlineExamMapID)
        {
            throw new NotImplementedException();
        }

        public string UpdateCandidateExamMapStatus(CandidateOnlineExamMapDTO examMapData)
        {
            throw new NotImplementedException();
        }

        public List<CandidateAnswerDTO> GetCandidateAnswersByMapData(CandidateOnlineExamMapDTO examMapData)
        {
            throw new NotImplementedException();
        }

        public OnlineQuestionGroupsDTO GetQnGroupDetailsByID(int qnGroupID)
        {
            throw new NotImplementedException();
        }

    }
}