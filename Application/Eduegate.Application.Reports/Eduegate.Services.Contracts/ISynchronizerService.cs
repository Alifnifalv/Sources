using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Eduegate.Services.Contracts.Synchronizer;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISynchronizerService" in both code and config file together.
    [ServiceContract]
    public interface ISynchronizerService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetNextChangeFromQueue?entity={entity}&numberOfitems={numberOfChanges}")]
        List<EntityChangeTrackerDTO> GetNextChangeFromQueue(Eduegate.Services.Contracts.Enums.Synchronizer.Entities entity, int numberOfChanges);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "UpdateEntityChangeTrackerStatus?changeTrackerID={changeTrackerID}&statusID={statusID}")]
        bool UpdateEntityChangeTrackerStatus(long changeTrackerID, Eduegate.Services.Contracts.Enums.Synchronizer.TrackerStatuses statusID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
             RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, 
            UriTemplate = "SaveEntityChangeTracker")]
        EntityChangeTrackerDTO SaveEntityChangeTracker(EntityChangeTrackerDTO changeTracker);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetFieldMaps?mapType={mapType}")]
        List<FieldMapTypeDTO> GetFieldMaps(Eduegate.Services.Contracts.Enums.Synchronizer.FieldMapTypes mapType);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SyncGetQueueCount?SyncLastDatetime={SyncLastDatetime}&SyncCurrentDatetime={SyncCurrentDatetime}")]
        int SyncGetQueueCount(string SyncLastDatetime, string SyncCurrentDatetime);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SyncGetQueueList?SyncLastDatetime={SyncLastDatetime}&SyncCurrentDatetime={SyncCurrentDatetime}&PageSize={PageSize}&PageNo={PageNo}")]
        List<SynchronizerQueue> SyncGetQueueList(string SyncLastDatetime, string SyncCurrentDatetime, int PageSize, int PageNo);
    }
}
