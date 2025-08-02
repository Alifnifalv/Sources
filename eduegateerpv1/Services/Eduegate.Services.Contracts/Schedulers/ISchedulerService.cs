using System.Collections.Generic;
using Eduegate.Services.Contracts.Enums.Schedulers;

namespace Eduegate.Services.Contracts.Schedulers
{
    public interface ISchedulerService
    {
         SchedulerDTO GetScheduler(SchedulerTypes schedulerType, SchedulerEntityTypes entityType, string entityID);

         List<SchedulerDTO> GetSchedulerByType(SchedulerTypes schedulerType, string entityID);

         List<SchedulerDTO> SaveSchedulers(List<SchedulerDTO> user);
    }
}