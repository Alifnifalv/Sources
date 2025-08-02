using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts.Schedulers;

namespace Eduegate.Service.Client.Schedulers
{
    public class SchedulerServiceClient : BaseClient, ISchedulerService
    {
        private static string serviceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string Service = string.Concat(serviceHost, Eduegate.Framework.Helper.Constants.SCHEDULER_SERVICE);

        public SchedulerServiceClient(CallContext context = null, Action<string> logger = null)
            : base(context, logger)
        {
        }

        public SchedulerDTO GetScheduler(Services.Contracts.Enums.Schedulers.SchedulerTypes schedulerType, Services.Contracts.Enums.Schedulers.SchedulerEntityTypes entityType, string entityID)
        {
            var url = string.Format("{0}/{1}?schedulerType={2}&entityType={3}&entityID={4}", Service, "GetScheduler", schedulerType, entityType, entityID);
            return ServiceHelper.HttpGetRequest<SchedulerDTO>(url, _callContext, _logger);
        }

        public List<SchedulerDTO> SaveSchedulers(List<SchedulerDTO> dtoList)
        {
            return ServiceHelper.HttpPostGetRequest<List<SchedulerDTO>>(Service + "SaveSchedulers", dtoList, _callContext);
        }

        public List<SchedulerDTO> GetSchedulerByType(Services.Contracts.Enums.Schedulers.SchedulerTypes schedulerType, string entityID)
        {
            var url = string.Format("{0}/{1}?schedulerType={2}&entityID={3}", Service, "GetSchedulerByType", schedulerType, entityID);
            return ServiceHelper.HttpGetRequest<List<SchedulerDTO>>(url, _callContext, _logger);
        }
    }
}
