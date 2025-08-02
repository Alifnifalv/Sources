using Eduegate.Domain.Entity.Schedule;
using Eduegate.Domain.Entity.Schedule.Models;
using Eduegate.Framework;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Schedulers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Linq;

namespace Eduegate.Domain.Mappers.Schedule
{
    public class ScheduleDriverMapper : DTOEntityDynamicMapper
    {
        private CallContext _context;

        public static ScheduleDriverMapper Mapper(CallContext context)
        {
            var mapper = new ScheduleDriverMapper();
            mapper._context = context;
            return mapper;
        }

        public DriverSchedule ToEntity(EmployeeScheduleDTO dto)
        {
            return new DriverSchedule()
            {
                StatusId = dto.StatusID,
                ScheduleId = dto.ScheduleID,
                DriverId = dto.EmployeeID,
                ScheduleDate = dto.DesptachDate,
                DropOff = DateTime.ParseExact("01/01/1900 " + dto.DropOff, "dd/MM/yyyy h:mm tt", CultureInfo.InvariantCulture),
                PickUp = DateTime.ParseExact("01/01/1900 " + dto.PickUp, "dd/MM/yyyy h:mm tt", CultureInfo.InvariantCulture),
                DropOffLocation = dto.DropOffLocation,
                PickupLocation = dto.PickUpLocation,
            };
        }

        public DriverSchedule FromvSchedule(vDriverSchedule vSchedule)
        {
            return new DriverSchedule()
            {
                StatusId = vSchedule.StatusId,
                ScheduleId = vSchedule.ScheduleId,
                DriverId = vSchedule.DriverId,
                ScheduleDate = vSchedule.ScheduleDate,
                DropOff = vSchedule.DropOff,
                PickUp = vSchedule.PickUp,
                DropOffLocation = vSchedule.DropOff_Location,
                PickupLocation = vSchedule.PickUp_Location,
                AreaId = vSchedule.AreaId,
                MaidID = vSchedule.MaidId
            };
        }

        public DriverSchedule GetDriverScheduleBySchedule(long scheduleID)
        {
            using (var dbContext = new EduegatedERP_ScheduleContext())
            {
                return dbContext.DriverSchedules.Where(a => a.ScheduleId == scheduleID).AsNoTracking().FirstOrDefault();
            }
        }

        public DriverSchedule UpdateDriverSchedule(DriverSchedule schedule)
        {
            using (var dbContext = new EduegatedERP_ScheduleContext())
            {
                dbContext.DriverSchedules.Add(schedule);

                if (schedule.DriverScheduleIdty != 0)
                {
                    dbContext.Entry(schedule).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
                return schedule;
            }
        }

    }
}