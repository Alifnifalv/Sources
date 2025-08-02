using Eduegate.Domain.Entity.Schedule;
using Eduegate.Domain.Entity.Schedule.Models;
using Eduegate.Framework;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Schedulers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Domain.Mappers.Schedule
{
    public class DriverScheduleMapper : DTOEntityDynamicMapper
    {
        public static DriverScheduleMapper Mapper(CallContext context)
        {
            var mapper = new DriverScheduleMapper();
            mapper._context = context;
            return mapper;
        }

        public vDriverSchedule GetScheduleDetails(long scheduleID)
        {
            using (var dbContext = new EduegatedERP_ScheduleContext())
            {
                return dbContext.vDriverSchedules
                    .Where(a => a.DriverScheduleIdty == scheduleID)
                    .AsNoTracking()
                    .FirstOrDefault();
            }
        }

        public List<vDriverSchedule> GetScheduleDetails(string employeeCode, DateTime date)
        {
            using (var dbContext = new EduegatedERP_ScheduleContext())
            {
                return dbContext.vDriverSchedules.Where(a=> a.DriverCode == employeeCode 
                && a.ScheduleDate == date).AsNoTracking().ToList();
            }
        }

        public List<vDriverSchedule> GetScheduleDetails(string employeeCode, DateTime date, DateTime time)
        {
            using (var dbContext = new EduegatedERP_ScheduleContext())
            {
                return dbContext.vDriverSchedules.Where(a => a.DriverCode == employeeCode &&
                  a.ScheduleDate == date &&
                 (a.DropOff == time || a.PickUp == time)).AsNoTracking().ToList();
            }
        }

        public vDriverSchedule GetNextPickupDetailsByMaid(string maidCode, DateTime date)
        {
            using (var dbContext = new EduegatedERP_ScheduleContext())
            {
                return dbContext.vDriverSchedules.Where(a => a.MaidCode == maidCode && a.ScheduleDate == date)
                    .AsNoTracking()
                    .FirstOrDefault();
            }
        }

        public EmployeeScheduleDetailsDTO ToDTO(List<vDriverSchedule> entities)
        {
            var detailDTO = new EmployeeScheduleDetailsDTO()
            {
                Schedules = new List<EmployeeScheduleDTO>()
            };

            foreach (var entity in entities)
            {
                var dto = ToDTO(entity);
                dto.ScheduleType = ScheduleTypes.Pick;
                dto.DropOffLocation = null;
                dto.Time = dto.PickUp;
                detailDTO.Schedules.Add(dto);
                dto = ToDTO(entity);
                dto.ScheduleType = ScheduleTypes.Drop;
                dto.PickUpLocation = null;
                dto.Time = dto.DropOff;
                dto.StatusID = entity.StatusId;
                detailDTO.Schedules.Add(dto);
            }

            return detailDTO;
        }

        public EmployeeScheduleDTO ToDTO(vDriverSchedule entity)
        {
            return new EmployeeScheduleDTO()
            {
                PickUpLocation = entity.PickUp_Location,
                PickUp = entity.PickUp.ToString("hh:mm tt"),
                MaidName = entity.MaidName,
                MaidCode = entity.MaidCode,
                DropOff = entity.DropOff.ToString("hh:mm tt"),
                DropOffLocation = entity.DropOff_Location,
                DriverName = entity.DriverName,
                //StatusID = entity.StatusId
                ScheduleID = entity.DriverScheduleIdty,
                StatusID = entity.StatusId,
                AreaID = entity.AreaId,
                //CustomerID = entity.CustomerID
            };
        }

        public EmployeeScheduleSummaryInfoDTO ToScheduleDTO(List<vDriverSchedule> entities)
        {
            var detailDTO = new EmployeeScheduleSummaryInfoDTO()
            {
                SchedulesInfos = new List<ScheduleSummaryInfoDTO>()
            };

            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    detailDTO.SchedulesInfos.AddRange(ToScheduleDTO(entity));
                }
            }

            var firstSchedule = entities.FirstOrDefault();

            var gropedInfo = new EmployeeScheduleSummaryInfoDTO()
            {
                EmployeeID = firstSchedule == null ? 0 : firstSchedule.DriverId.Value,
                EmployeeName = firstSchedule == null ? string.Empty : firstSchedule.DriverName,
                SchedulesInfos = new List<ScheduleSummaryInfoDTO>()
            };

            foreach (var group in detailDTO.SchedulesInfos.GroupBy(a => a.ActualTime))
            {
                var summaryInfo = new ScheduleSummaryInfoDTO() {
                     SchedulerSummaryDetails = new List<SchedulerSummaryDetailDTO>()
                };

                summaryInfo.ActualTime = group.Key;
                summaryInfo.Time = summaryInfo.ActualTime.ToShortTimeString();

                foreach (var item in group)
                {
                    summaryInfo.SchedulerSummaryDetails.Add(new SchedulerSummaryDetailDTO()
                    {
                        ScheduleID = item.SchedulerSummaryDetails[0].ScheduleID,
                        ScheduleType = item.SchedulerSummaryDetails[0].ScheduleType,
                        ScheduleStatus = item.SchedulerSummaryDetails[0].ScheduleStatus,
                    });
                }

                summaryInfo.NumberOfDrops = summaryInfo.SchedulerSummaryDetails.Where(a => a.ScheduleType == ScheduleTypes.Drop && a.ScheduleStatus == ScheduleStatuses.Started).Count();
                summaryInfo.NumberOfPickups = summaryInfo.SchedulerSummaryDetails.Where(a => a.ScheduleType == ScheduleTypes.Pick && a.ScheduleStatus == ScheduleStatuses.Started).Count();
                summaryInfo.NumberOfDropsCompleted = summaryInfo.SchedulerSummaryDetails.Where(a => a.ScheduleType == ScheduleTypes.Drop && a.ScheduleStatus == ScheduleStatuses.Completed).Count();
                summaryInfo.NumberOfPickupsCompleted = summaryInfo.SchedulerSummaryDetails.Where(a => a.ScheduleType == ScheduleTypes.Pick && a.ScheduleStatus == ScheduleStatuses.Completed).Count();
                gropedInfo.SchedulesInfos.Add(summaryInfo);
            }

            gropedInfo.SchedulesInfos = gropedInfo.SchedulesInfos.OrderBy(a => a.ActualTime).ToList();
            return gropedInfo;
        }

        public List<ScheduleSummaryInfoDTO> ToScheduleDTO(vDriverSchedule entity)
        {
            var infos = new List<ScheduleSummaryInfoDTO>();

            infos.Add(new ScheduleSummaryInfoDTO()
            {
                ActualTime = entity.PickUp,
                SchedulerSummaryDetails = new List<SchedulerSummaryDetailDTO>() {
                    new SchedulerSummaryDetailDTO() {
                         ScheduleID = entity.ScheduleId.Value,
                         ScheduleType = ScheduleTypes.Pick,
                         ScheduleStatus = (ScheduleStatuses)entity.StatusId
                    }
                }
            });

            infos.Add(new ScheduleSummaryInfoDTO()
            {
                ActualTime = entity.DropOff,
                SchedulerSummaryDetails = new List<SchedulerSummaryDetailDTO>() {
                    new SchedulerSummaryDetailDTO() {
                         ScheduleID = entity.ScheduleId.Value,
                         ScheduleType = ScheduleTypes.Drop
                    }
                }
            });

            return infos;
        }
    }
}