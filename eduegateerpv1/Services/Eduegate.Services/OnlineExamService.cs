using Eduegate.Domain.Mappers.OnlineExam.Exam;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.OnlineExam.Exam;

namespace Eduegate.Services
{
    public class OnlineExamService : BaseService, IOnlineExamService
    {
        public string SaveCandidateFullAnswers(List<CandidateAnswerDTO> answerList)
        {
            return CandidateAnswerMapper.Mapper(CallContext).SaveCandidateFullAnswers(answerList);
        }

        public void AutoSaveMarksByCandidateAnswers(List<CandidateAnswerDTO> answerList)
        {
            OnlineExamResultsMapper.Mapper(CallContext).AutoSaveMarksByCandidateAnswers(answerList);
        }

        public List<KeyValueDTO> GetOnlineExamsByCandidateAndAcademicYear(long candidateID, int academicYearID)
        {
            return OnlineExamsMapper.Mapper(CallContext).GetOnlineExamsByCandidateAndAcademicYear(candidateID, academicYearID);
        }

        public OnlineExamResultDTO GetOnlineExamResults(long candidateID, long examID)
        {
            return OnlineExamResultsMapper.Mapper(CallContext).GetOnlineExamResults(candidateID, examID);
        }

        public OperationResultDTO UpdateOnlineExamResult(OnlineExamResultDTO resultDTO)
        {
            return OnlineExamResultsMapper.Mapper(CallContext).UpdateOnlineExamResult(resultDTO);
        }

        public List<KeyValueDTO> GetSubjectsByOnlineExam(long examID, int academicYearID, short? languageTypeID)
        {
            return OnlineExamResultsMapper.Mapper(CallContext).GetSubjectsByOnlineExam(examID, academicYearID, languageTypeID);
        }

        public List<OnlineExamResultDTO> GetOnlineExamEntryResults(long examID, int academicYearID, short? languageTypeID, int? subjectID)
        {
            return OnlineExamResultsMapper.Mapper(CallContext).GetOnlineExamEntryResults(examID, academicYearID, languageTypeID, subjectID);
        }

        public string SaveOnlineExamResultEntries(List<OnlineExamResultDTO> examResultDTOs)
        {
            return OnlineExamResultsMapper.Mapper(CallContext).SaveOnlineExamResultEntries(examResultDTOs);
        }

        public string UpdateCandidateExamMap(long? candidateOnlinExamMapID, double durationInMinutes, byte examStartStatusID)
        {
            return CandidateMapper.Mapper(CallContext).UpdateCandidateExamMap(candidateOnlinExamMapID, durationInMinutes, examStartStatusID);
        }

        public List<OnlineExamQuestionDTO> GetQuestionsByExamID(long onlineExamID, long? candidateExamMapID, long? candidateID)
        {
            return OnlineQuestionsMapper.Mapper(CallContext).GetQuestionsByExamID(onlineExamID, candidateExamMapID, candidateID);
        }

        public List<CandidateOnlineExamMapDTO> GetExamsDataByCandidateID(long candidateID)
        {
            return OnlineExamsMapper.Mapper(CallContext).GetExamsDataByCandidateID(candidateID);
        }

        public string SaveCandidateAnswer(CandidateAnswerDTO answer)
        {
            return CandidateAnswerMapper.Mapper(CallContext).SaveCandidateAnswer(answer);
        }

        public OnlineExamsDTO GetOnlineExamDetailsByExamID(long onlineExamID)
        {
            return OnlineExamsMapper.Mapper(CallContext).GetOnlineExamDetailsByExamID(onlineExamID);
        }

        public CandidateOnlineExamMapDTO GetCandidateOnlineExamMapDataByID(long candidateExamMapID)
        {
            return CandidateOnlineExamMapMapper.Mapper(CallContext).GetCandidateOnlineExamMapDataByID(candidateExamMapID);
        }

        public string UpdateStartTime(DateTime startDate, long candidateOnlineExamMapID)
        {
            return CandidateOnlineExamMapMapper.Mapper(CallContext).UpdateStartTime(startDate, candidateOnlineExamMapID);
        }

        public string UpdateCandidateExamMapStatus(CandidateOnlineExamMapDTO examMapData)
        {
            return CandidateOnlineExamMapMapper.Mapper(CallContext).UpdateCandidateExamMapStatus(examMapData);
        }

        public List<CandidateAnswerDTO> GetCandidateAnswersByMapData(CandidateOnlineExamMapDTO examMapData)
        {
            return CandidateAnswerMapper.Mapper(CallContext).GetCandidateAnswersByMapData(examMapData);
        }

        public OnlineQuestionGroupsDTO GetQnGroupDetailsByID(int qnGroupID)
        {
            return OnlineQuestionGroupsMapper.Mapper(CallContext).GetQnGroupDetailsByID(qnGroupID);
        }

    }
}