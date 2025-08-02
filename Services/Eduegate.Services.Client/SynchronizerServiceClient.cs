using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Synchronizer;

namespace Eduegate.Service.Client
{
    public class SynchronizerServiceClient : BaseClient, ISynchronizerService
    {
        private static string ServiceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string service = string.Concat(ServiceHost, Eduegate.Framework.Helper.Constants.SYNCHRONIZER_SERVICE_NAME);

        public SynchronizerServiceClient(CallContext context = null, Action<string> logger = null)
            :base(context, logger)
        {
        }

        public List<EntityChangeTrackerDTO> GetQueue(Eduegate.Services.Contracts.Enums.Synchronizer.Entities entity, int numberOfChanges)
        {
            var uri = string.Format("{0}/{1}?entity={2}&numberOfitems={3}", service, "GetNextChangeFromQueue", entity.ToString(), numberOfChanges);
            return ServiceHelper.HttpGetRequest< List<EntityChangeTrackerDTO>>(uri);
        }

        public bool UpdateEntityChangeTrackerStatus(long changeTrackerID, Eduegate.Services.Contracts.Enums.Synchronizer.TrackerStatuses statusID)
        {
            var uri = string.Format("{0}/{1}?changeTrackerID={2}&statusID={3}", service, "UpdateEntityChangeTrackerStatus", changeTrackerID, statusID);
            return ServiceHelper.HttpGetRequest<bool>(uri, null, WriteLog);
        }

        public List<FieldMapTypeDTO> GetFieldMaps(Services.Contracts.Enums.Synchronizer.FieldMapTypes mapType)
        {
            var uri = string.Format("{0}/{1}?mapType={2}", service, "GetFieldMaps", mapType.ToString());
            return ServiceHelper.HttpGetRequest<List<FieldMapTypeDTO>>(uri, null, WriteLog);
        }

        public List<EntityChangeTrackerDTO> GetNextChangeFromQueue(Services.Contracts.Enums.Synchronizer.Entities entity, int numberOfChanges)
        {
            var uri = string.Format("{0}/{1}?entity={2}&numberOfitems={3}", service, "GetNextChangeFromQueue", entity.ToString(), numberOfChanges);
            return ServiceHelper.HttpGetRequest<List<EntityChangeTrackerDTO>>(uri, null, WriteLog);
        }

        public EntityChangeTrackerDTO SaveEntityChangeTracker(EntityChangeTrackerDTO changeTracker)
        {
            var uri = string.Format("{0}/{1}", service, "SaveEntityChangeTracker");
            return ServiceHelper.HttpPostGetRequest<EntityChangeTrackerDTO>(uri, changeTracker, null, WriteLog);
        }

        public int SyncGetQueueCount(string SyncLastDatetime, string SyncCurrentDatetime)
        {
            var uri = string.Format("{0}/{1}?SyncLastDatetime={2}&SyncCurrentDatetime={3}", service, "SyncGetQueueCount", SyncLastDatetime, SyncCurrentDatetime);
            return ServiceHelper.HttpGetRequest<int>(uri, null, WriteLog);
        }

        public List<SynchronizerQueue> SyncGetQueueList(string SyncLastDatetime, string SyncCurrentDatetime, int PageSize, int PageNo)
        {
            var uri = string.Format("{0}/{1}?SyncLastDatetime={2}&SyncCurrentDatetime={3}&PageSize={4}&PageNo={5}", service, "SyncGetQueueList", SyncLastDatetime, SyncCurrentDatetime, PageSize, PageNo);
            return ServiceHelper.HttpGetRequest<List<SynchronizerQueue>>(uri, null, WriteLog);
        }
    }
}
