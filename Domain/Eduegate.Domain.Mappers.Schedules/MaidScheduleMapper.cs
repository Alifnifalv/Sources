using Eduegate.Domain.Entity.Schedule;
using Eduegate.Domain.Entity.Schedule.Models;
using Eduegate.Domain.Repository.Accounts;
using Eduegate.Framework;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Schedulers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Domain.Mappers.Schedule
{
    public class MaidScheduleMapper : DTOEntityDynamicMapper
    {
        public static MaidScheduleMapper Mapper(CallContext context)
        {
            var mapper = new MaidScheduleMapper();
            mapper._context = context;
            return mapper;
        }

        public vMaidSchedule GetScheduleDetails(long despatchID)
        {
            using (var dbContext = new EduegatedERP_ScheduleContext())
            {
                return dbContext.vMaidSchedules
                    .Where(a => a.DailyDespatchId == despatchID)
                    .AsNoTracking()
                    .FirstOrDefault();
            }
        }

        public List<vMaidSchedule> GetScheduleDetails(string employeeCode, DateTime date)
        {
            using (var dbContext = new EduegatedERP_ScheduleContext())
            {
                return dbContext.vMaidSchedules
                    //.Where(
                    //a => a.EmployeeCode == employeeCode
                    //&& DbFunctions.TruncateTime(a.DespatchDate) == DbFunctions.TruncateTime(date)
                    //)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public EmployeeScheduleDetailsDTO ToDTO(List<vMaidSchedule> entities)
        {
            var detailDTO = new EmployeeScheduleDetailsDTO()
            {
                Schedules = new List<EmployeeScheduleDTO>()
            };

            foreach (var entity in entities)
            {
                var scheduleDto = ToDTO(entity);

                if (!string.IsNullOrEmpty(entity.CustomerCode))
                {
                    scheduleDto.AmountDue = new AccountingRepository().GetCustomerArrears(entity.CustomerCode);
                }

                detailDTO.Schedules.Add(scheduleDto);
            }

            return detailDTO;
        }
      

        public EmployeeScheduleDTO ToDTO(vMaidSchedule entity)
        {
            return new EmployeeScheduleDTO()
            {
                DespatchID = entity.DailyDespatchId,
                TimeFrom = entity.TimeFrom,
                TimeTo = entity.TimeTo,
                CustomerDetails = "Code :<b>" + entity.CustomerCode + "</b></br> Name:<b>" + entity.CustomerName + "</b></br>Address:<b>" + entity.CustomerDetails + "</b>",
                ItemsToCarry = entity.ItemsToCarry,
                Duration = entity.TotalHours,
                MaidCode = entity.EmployeeCode,
                MaidName = entity.EmployeeName,
                StatusID = entity.StatusId,
                Amount = !entity.Amount.HasValue ? 0 : decimal.Parse(entity.Amount.ToString()),
                TaxAmount = !entity.TaxAmount.HasValue ? 0 : decimal.Parse(entity.TaxAmount.ToString()),
                Rate = !entity.Rate.HasValue ? 0 : decimal.Parse(entity.Rate.ToString()),
                CustomerID = entity.CustomerId,
            };
        }
    }
}
