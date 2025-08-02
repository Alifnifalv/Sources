using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Eduegate.Services.Contracts.Logging
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWBLoggingService" in both code and config file together.
    [ServiceContract]
    public interface ILoggingService
    {
        [OperationContract(IsOneWay = true)]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SyncLogger")]
        void SyncLogger(CatalogLoggerDTO catalogLoggerDTO);

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetDataHistory?EntityID={EntityID}&IID={IID}&FieldName={FieldName}")]
        DataHistoryResultDTO GetDataHistory(int EntityID, int IID, string FieldName);

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetActivities?activityTypeID={activityTypeID}")]
        List<ActivityDTO> GetActivities(int activityTypeID);

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetActivitiesByLoginID?loginID={loginID}")]
        List<ActivityDTO> GetActivitiesByLoginID(long loginID);

        [OperationContract(IsOneWay = true)]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveActivities")]
        void SaveActivities(List<ActivityDTO> activities);

        [OperationContract(IsOneWay = true)]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveActivitiesAsynch")]
        void SaveActivitiesAsynch(List<ActivityDTO> activities);

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetActivity?activityID={activityID}")]
        ActivityDTO GetActivity(long activityID);
    }
}
