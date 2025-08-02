using System.ServiceModel;
using Eduegate.Domain;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Synchronizer;

namespace Eduegate.Services
{
    public class SynchronizerService : BaseService,ISynchronizerService
    {
        public List<EntityChangeTrackerDTO> GetNextChangeFromQueue(Contracts.Enums.Synchronizer.Entities entity, int numberOfChanges)
        {
            try
            {
                var dto = (new SynchronizerBL(CallContext)).GetNextChangeFromQueue(entity, numberOfChanges);
                Eduegate.Logger.LogHelper<EntityChangeTrackerDTO>.Info("Service Result : " + dto.ToString());
                return dto;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<EntityChangeTrackerDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool UpdateEntityChangeTrackerStatus(long changeTrackerID, Eduegate.Services.Contracts.Enums.Synchronizer.TrackerStatuses statusID)
        {
            try
            {
                var dto = (new SynchronizerBL(CallContext)).UpdateEntityChangeTrackerStatus(changeTrackerID, statusID);
                Eduegate.Logger.LogHelper<bool>.Info("Service Result : " + dto.ToString());
                return dto;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<bool>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public EntityChangeTrackerDTO SaveEntityChangeTracker(EntityChangeTrackerDTO tracker)
        {
            try
            {
                var dto = (new SynchronizerBL(CallContext)).SaveEntityChangeTracker(tracker);
                Eduegate.Logger.LogHelper<EntityChangeTrackerDTO>.Info("Service Result : " + dto.ToString());
                return dto;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<EntityChangeTrackerDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<FieldMapTypeDTO> GetFieldMaps(Contracts.Enums.Synchronizer.FieldMapTypes mapType)
        {
            try
            {
                var dto = (new SynchronizerBL(CallContext)).GetFieldMaps(mapType);
                Eduegate.Logger.LogHelper<FieldMapTypeDTO>.Info("Service Result : " + dto.ToString());
                return dto;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<FieldMapTypeDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public int SyncGetQueueCount(string SyncLastDatetime, string SyncCurrentDatetime)
        {
            try
            {
                var dto = (new SynchronizerBL(CallContext)).SyncGetQueueCount(DateTime.Parse(SyncLastDatetime), DateTime.Parse(SyncCurrentDatetime));
                Eduegate.Logger.LogHelper<FieldMapTypeDTO>.Info("Service Result : " + dto.ToString());
                return dto;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<FieldMapTypeDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }


        public List<SynchronizerQueue> SyncGetQueueList(string SyncLastDatetime, string SyncCurrentDatetime, int PageSize, int PageNo)
        {
            try
            {
                var dto = (new SynchronizerBL(CallContext)).SyncGetQueueList(DateTime.Parse(SyncLastDatetime), DateTime.Parse(SyncCurrentDatetime), PageSize, PageNo);
                Eduegate.Logger.LogHelper<FieldMapTypeDTO>.Info("Service Result : " + dto.ToString());
                return dto;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<FieldMapTypeDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

    }
}
