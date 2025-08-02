using System.Collections.Generic;
using Eduegate.Services.Contracts.Synchronizer;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISynchronizerService" in both code and config file together.
    public interface ISynchronizerService
    {
        List<EntityChangeTrackerDTO> GetNextChangeFromQueue(Eduegate.Services.Contracts.Enums.Synchronizer.Entities entity, int numberOfChanges);

        bool UpdateEntityChangeTrackerStatus(long changeTrackerID, Eduegate.Services.Contracts.Enums.Synchronizer.TrackerStatuses statusID);

        EntityChangeTrackerDTO SaveEntityChangeTracker(EntityChangeTrackerDTO changeTracker);

        List<FieldMapTypeDTO> GetFieldMaps(Eduegate.Services.Contracts.Enums.Synchronizer.FieldMapTypes mapType);

        int SyncGetQueueCount(string SyncLastDatetime, string SyncCurrentDatetime);

        List<SynchronizerQueue> SyncGetQueueList(string SyncLastDatetime, string SyncCurrentDatetime, int PageSize, int PageNo);
    }
}