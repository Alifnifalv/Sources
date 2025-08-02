using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.OnlineExam.Exam;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IOnlineExamService" in both code and config file together.
    [ServiceContract]
    public interface IOnlineExamService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "SaveCandidateFullAnswers")]
        string SaveCandidateFullAnswers(List<CandidateAnswerDTO> answerList);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "AutoSaveMarksByCandidateAnswers")]
        void AutoSaveMarksByCandidateAnswers(List<CandidateAnswerDTO> answerList);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetOnlineExamsByCandidateAndAcademicYear?candiateID={candidateID}&academicYearID={academicYearID}")]
        List<KeyValueDTO> GetOnlineExamsByCandidateAndAcademicYear(long candidateID, int academicYearID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetOnlineExamResults?candiateID={candidateID}&examID={examID}")]
        OnlineExamResultDTO GetOnlineExamResults(long candidateID, long examID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "UpdateOnlineExamResult")]
        OperationResultDTO UpdateOnlineExamResult(OnlineExamResultDTO resultDTO);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetSubjectsByOnlineExam")]
        List<KeyValueDTO> GetSubjectsByOnlineExam(long examID, int academicYearID, short? languageTypeID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetOnlineExamEntryResults")]
        List<OnlineExamResultDTO> GetOnlineExamEntryResults(long examID, int academicYearID, short? languageTypeID, int? subjectID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "SaveOnlineExamResultEntries")]
        string SaveOnlineExamResultEntries(List<OnlineExamResultDTO> examResultDTOs);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "UpdateCandidateExamMap")]
        string UpdateCandidateExamMap(long? candidateOnlinExamMapID, double durationInMinutes, byte examStartStatusID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetQuestionsByExamID?onlineExamID={onlineExamID}&candidateExamMapID={candidateExamMapID}&candidateID={candidateID}")]
        List<OnlineExamQuestionDTO> GetQuestionsByExamID(long onlineExamID, long? candidateExamMapID, long? candidateID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetExamsDataByCandidateID?candidateID={candidateID}")]
        List<CandidateOnlineExamMapDTO> GetExamsDataByCandidateID(long candidateID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "SaveCandidateAnswer")]
        string SaveCandidateAnswer(CandidateAnswerDTO answer);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetOnlineExamDetailsByExamID?onlineExamID={onlineExamID}")]
        OnlineExamsDTO GetOnlineExamDetailsByExamID(long onlineExamID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetCandidateOnlineExamMapDataByID?candidateExamMapID={candidateExamMapID}")]
        CandidateOnlineExamMapDTO GetCandidateOnlineExamMapDataByID(long candidateExamMapID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "UpdateStartTime")]
        string UpdateStartTime(DateTime startDate, long candidateOnlineExamMapID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "UpdateCandidateExamMapStatus")]
        string UpdateCandidateExamMapStatus(CandidateOnlineExamMapDTO examMapData);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetCandidateAnswersByMapData")]
        List<CandidateAnswerDTO> GetCandidateAnswersByMapData(CandidateOnlineExamMapDTO examMapData);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetQnGroupDetailsByID?qnGroupID={qnGroupID}")]
        OnlineQuestionGroupsDTO GetQnGroupDetailsByID(int qnGroupID);

    }
}