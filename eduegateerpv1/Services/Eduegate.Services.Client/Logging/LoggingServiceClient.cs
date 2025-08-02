using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts.Logging;

namespace Eduegate.Service.Client.Logging
{
    public class LoggingServiceClient : BaseClient, ILoggingService
    {
        private static string serviceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string loggingService = string.Concat(serviceHost, Eduegate.Framework.Helper.Constants.WB_LOGGING_SERVICE);

        public LoggingServiceClient(CallContext callContext = null, Action<string> logger = null)
            : base(callContext, logger)
        {
        }

        public void SyncLogger(CatalogLoggerDTO catalogLoggerDTO)
        {
            ServiceHelper.HttpPostRequestAsync(loggingService + "SyncLogger", catalogLoggerDTO);
        }

        public DataHistoryResultDTO GetDataHistory(int EntityID, int IID, string FieldName)
        {
            var serviceUrl = string.Format("{0}{1}?EntityID={2}&IID={3}&FieldName={4}", loggingService, "GetDataHistory", EntityID, IID, FieldName);
            return ServiceHelper.HttpGetRequest<DataHistoryResultDTO>(serviceUrl, _callContext);
        }

        public List<ActivityDTO> GetActivities(int activityTypeID)
        {
            var serviceUrl = string.Format("{0}{1}?activityTypeID={2}", loggingService, "GetActivities", activityTypeID);
            return ServiceHelper.HttpGetRequest<List<ActivityDTO>>(serviceUrl, _callContext);
        }

        public void SaveActivities(List<ActivityDTO> activities)
        {
            ServiceHelper.HttpPostGetRequest(loggingService + "SaveActivities", activities);
        }

        public List<ActivityDTO> GetActivitiesByLoginID(long loginID)
        {
            var serviceUrl = string.Format("{0}{1}?loginID={2}", loggingService, "GetActivitiesByLoginID", loginID);
            return ServiceHelper.HttpGetRequest<List<ActivityDTO>>(serviceUrl, _callContext);
        }

        public ActivityDTO GetActivity(long activityID)
        {
            var serviceUrl = string.Format("{0}{1}?activityID={2}", loggingService, "GetActivity", activityID);
            return ServiceHelper.HttpGetRequest<ActivityDTO>(serviceUrl, _callContext);
        }

        public void SaveActivitiesAsynch(List<ActivityDTO> activities)
        {
            ServiceHelper.HttpPostGetRequest(loggingService + "SaveActivitiesAsynch", activities);
        }
    }
}
