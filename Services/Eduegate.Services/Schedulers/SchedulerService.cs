using Eduegate.Domain;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts.Schedulers;

namespace Eduegate.Services.Schedulers
{
    public class SchedulerService : BaseService, ISchedulerService
    {

        public SchedulerDTO GetScheduler(Contracts.Enums.Schedulers.SchedulerTypes schedulerType, Contracts.Enums.Schedulers.SchedulerEntityTypes entityType, string entityID)
        {
            return new SchedulerBL(base.CallContext).GetScheduler(schedulerType, entityType, entityID);
        }

        public List<SchedulerDTO> SaveSchedulers(List<SchedulerDTO> schedulers)
        {
            return new SchedulerBL(base.CallContext).SaveSchedulers(schedulers);
        }

        public List<SchedulerDTO> GetSchedulerByType(Contracts.Enums.Schedulers.SchedulerTypes schedulerType, string entityID)
        {
            return new SchedulerBL(base.CallContext).GetSchedulerByType(schedulerType, entityID);
        }
    }
}
