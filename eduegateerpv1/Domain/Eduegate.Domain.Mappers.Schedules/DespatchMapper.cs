using Eduegate.Domain.Entity.Schedule;
using Eduegate.Domain.Entity.Schedule.Models;
using Eduegate.Framework;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Schedulers;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Eduegate.Domain.Mappers.Schedule
{
    public class DespatchMapper : DTOEntityDynamicMapper
    {
        public static DespatchMapper Mapper(CallContext context)
        {
            var mapper = new DespatchMapper();
            mapper._context = context;
            return mapper;
        }

        public Despatch ToEntity(EmployeeScheduleDTO dto)
        {
            return new Despatch()
            {
                Amount = dto.Amount,
                CustomerID = dto.CustomerID,
                DespatchDate = dto.DesptachDate,
                EmployeeID = dto.EmployeeID,
                ReceivedAmount = dto.ReceivedAmount,
                TimeFrom = dto.TimeFrom,
                TimeTo = dto.TimeTo,
                ScheduleID = dto.ScheduleID,
                StatusID = dto.StatusID,
                ExternalReferenceID1 = dto.DespatchID
            };
        }

        public Despatch GetDespatchBySchedule(long scheduleID)
        {
            using (var dbContext = new EduegatedERP_ScheduleContext())
            {
                return dbContext.Despatches.Where(a=> a.ScheduleID == scheduleID).AsNoTracking().FirstOrDefault();
            }
        }

        public Despatch GetDespatchByDesptach(long despatchID)
        {
            using (var dbContext = new EduegatedERP_ScheduleContext())
            {
                return dbContext.Despatches.Where(a => a.ExternalReferenceID1 == despatchID).AsNoTracking().FirstOrDefault();
            }
        }

        public Despatch UpdateDespatch(Despatch despatch)
        {
            using (var dbContext = new EduegatedERP_ScheduleContext())
            {
                dbContext.Despatches.Add(despatch);

                if (despatch.DespatchIdty != 0)
                {
                    dbContext.Entry(despatch).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
                return despatch;
            }
        }
    }
}