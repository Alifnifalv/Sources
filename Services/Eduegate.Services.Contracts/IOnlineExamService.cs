using System;
using System.Collections.Generic;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.OnlineExam.Exam;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IOnlineExamService" in both code and config file together.
    public interface IOnlineExamService
    {
        string SaveCandidateFullAnswers(List<CandidateAnswerDTO> answerList);

        void AutoSaveMarksByCandidateAnswers(List<CandidateAnswerDTO> answerList);

        List<KeyValueDTO> GetOnlineExamsByCandidateAndAcademicYear(long candidateID, int academicYearID);

        OnlineExamResultDTO GetOnlineExamResults(long candidateID, long examID);

        OperationResultDTO UpdateOnlineExamResult(OnlineExamResultDTO resultDTO);

        List<KeyValueDTO> GetSubjectsByOnlineExam(long examID, int academicYearID, short? languageTypeID);

        List<OnlineExamResultDTO> GetOnlineExamEntryResults(long examID, int academicYearID, short? languageTypeID, int? subjectID);

        string SaveOnlineExamResultEntries(List<OnlineExamResultDTO> examResultDTOs);

        string UpdateCandidateExamMap(long? candidateOnlinExamMapID, double durationInMinutes, byte examStartStatusID);

        List<OnlineExamQuestionDTO> GetQuestionsByExamID(long onlineExamID, long? candidateExamMapID, long? candidateID);

        List<CandidateOnlineExamMapDTO> GetExamsDataByCandidateID(long candidateID);

        string SaveCandidateAnswer(CandidateAnswerDTO answer);

        OnlineExamsDTO GetOnlineExamDetailsByExamID(long onlineExamID);

        CandidateOnlineExamMapDTO GetCandidateOnlineExamMapDataByID(long candidateExamMapID);

        string UpdateStartTime(DateTime startDate, long candidateOnlineExamMapID);

        string UpdateCandidateExamMapStatus(CandidateOnlineExamMapDTO examMapData);

        List<CandidateAnswerDTO> GetCandidateAnswersByMapData(CandidateOnlineExamMapDTO examMapData);

        OnlineQuestionGroupsDTO GetQnGroupDetailsByID(int qnGroupID);

        List<KeyValueDTO> GetQuestionGroupByExamIDs(List<int> subjectIDs);

        string GetQuestionsByMarks(int marks, int subjectID, int classID, bool isPassage);

        List<string> GetQuestionDetails(int subjectID, int classID);

    }
}