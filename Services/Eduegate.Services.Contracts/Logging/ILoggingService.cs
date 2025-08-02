using System.Collections.Generic;

namespace Eduegate.Services.Contracts.Logging
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWBLoggingService" in both code and config file together.
    public interface ILoggingService
    {
        void SyncLogger(CatalogLoggerDTO catalogLoggerDTO);

        DataHistoryResultDTO GetDataHistory(int EntityID, int IID, string FieldName);

        List<ActivityDTO> GetActivities(int activityTypeID);

        List<ActivityDTO> GetActivitiesByLoginID(long loginID);

        void SaveActivities(List<ActivityDTO> activities);

        void SaveActivitiesAsynch(List<ActivityDTO> activities);

        ActivityDTO GetActivity(long activityID);
    }
}