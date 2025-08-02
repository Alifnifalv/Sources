using Eduegate.Services.Contracts.Logging;
using Eduegate.Domain.Logging;
using Eduegate.Framework.Services;

namespace Eduegate.Services.Logging
{
    public class LoggingServices : BaseService, ILoggingService
    {
        public void SyncLogger(CatalogLoggerDTO catalogLoggerDTO)
        {
            new LoggingBL(CallContext).SyncLogger(catalogLoggerDTO: catalogLoggerDTO);
        }

        public DataHistoryResultDTO GetDataHistory(int EntityID, int IID, string FieldName)
        {
            return new LoggingBL(CallContext).GetDataHistory(EntityID, IID, FieldName);
        }

        public List<ActivityDTO> GetActivities(int activityTypeID)
        {
            return new LoggingBL(CallContext).GetActivities(activityTypeID);
        }

        public void SaveActivities(List<ActivityDTO> activities)
        {
            new LoggingBL(CallContext).SaveActivities(activities);
        }

        public void SaveActivitiesAsynch(List<ActivityDTO> activities)
        {
            new LoggingBL(CallContext).SaveActivitiesAsynch(activities);
        }

        public List<ActivityDTO> GetActivitiesByLoginID(long loginID)
        {
            return new LoggingBL(CallContext).GetActivitiesByLoginID(loginID);
        }

        public ActivityDTO GetActivity(long activityID)
        {
            return new LoggingBL(CallContext).GetActivity(activityID);
        }
    }
}
