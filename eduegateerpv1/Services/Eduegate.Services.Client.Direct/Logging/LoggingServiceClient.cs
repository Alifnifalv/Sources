using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts.Logging;
using Eduegate.Services.Logging;

namespace Eduegate.Service.Client.Direct.Logging
{
    public class LoggingServiceClient : BaseClient, ILoggingService
    {
        LoggingServices service = new LoggingServices();

        public LoggingServiceClient(CallContext callContext = null, Action<string> logger = null)
            : base(callContext, logger)
        {
        }

        public void SyncLogger(CatalogLoggerDTO catalogLoggerDTO)
        {
            service.SyncLogger(catalogLoggerDTO);
        }

        public DataHistoryResultDTO GetDataHistory(int EntityID, int IID, string FieldName)
        {
            return service.GetDataHistory(EntityID, IID, FieldName);
        }

        public List<ActivityDTO> GetActivities(int activityTypeID)
        {
            return service.GetActivities(activityTypeID);
        }

        public void SaveActivities(List<ActivityDTO> activities)
        {
            service.SaveActivities(activities);
        }

        public void SaveActivitiesAsynch(List<ActivityDTO> activities)
        {
            service.SaveActivitiesAsynch(activities);
        }

        public List<ActivityDTO> GetActivitiesByLoginID(long loginID)
        {
            return service.GetActivitiesByLoginID(loginID);
        }

        public ActivityDTO GetActivity(long activityID)
        {
            return service.GetActivity(activityID);
        }
    }
}
