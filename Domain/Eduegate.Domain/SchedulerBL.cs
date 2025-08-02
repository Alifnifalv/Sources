using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Services.Contracts.Enums.Schedulers;
using Eduegate.Services.Contracts.Schedulers;

namespace Eduegate.Domain
{
    public class SchedulerBL
    {
        private CallContext _callContext { get; set; }

        public SchedulerBL(CallContext context)
        {
            _callContext = context;
        }

        public List<SchedulerDTO> SaveSchedulers(List<SchedulerDTO> dtos)
        {
            var schedulerMapper = Mappers.Schedulers.EntitySchedulerMapper.Mapper(_callContext);
            return schedulerMapper.ToDTO(new SchedulerRepository().SaveEntityScheduler(schedulerMapper.ToEntity(dtos)));
        }

        public SchedulerDTO GetScheduler(SchedulerTypes schedulerType, SchedulerEntityTypes schedulerEntityType, string entityID)
        {
            return Mappers.Schedulers.EntitySchedulerMapper.Mapper(_callContext).ToDTO(new SchedulerRepository().GetEntityScheduler((int)schedulerType, (int)schedulerEntityType, entityID));
        }

        public List<SchedulerDTO> GetSchedulerByType(SchedulerTypes schedulerType, string entityID)
        {
            return Mappers.Schedulers.EntitySchedulerMapper.Mapper(_callContext).ToDTO(new SchedulerRepository().GetEntityScheduler((int)schedulerType, entityID));
        }
    }
}
