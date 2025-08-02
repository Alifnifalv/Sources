using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using System;
using Eduegate.Services.Contracts.School.Academics;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Academics
{
    public class AcademicCalendarMasterMapper : DTOEntityDynamicMapper
    {
        public static AcademicCalendarMasterMapper Mapper(CallContext context)
        {
            var mapper = new AcademicCalendarMasterMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<AcademicCalendarMasterDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private AcademicCalendarMasterDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.AcadamicCalendars.Where(X => X.AcademicCalendarID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new AcademicCalendarMasterDTO()
                {
                    AcademicCalendarID = entity.AcademicCalendarID,
                    CalenderName = entity.CalenderName,
                    Description = entity.Description,
                    AcademicYearID = entity.AcademicYearID,
                    SchoolID = entity.SchoolID,
                    AcademicCalendarStatusID = entity.AcademicCalendarStatusID,
                    CalendarTypeID = entity.CalendarTypeID,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as AcademicCalendarMasterDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new AcadamicCalendar()
            {
                AcademicCalendarID = toDto.AcademicCalendarID,
                CalenderName = toDto.CalenderName,
                Description = toDto.Description,
                AcademicYearID = toDto.AcademicYearID,
                AcademicCalendarStatusID = toDto.AcademicCalendarStatusID,
                CalendarTypeID = toDto.CalendarTypeID,
                SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                CreatedBy = toDto.AcademicCalendarID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.AcademicCalendarID > 0 ? (int)_context.LoginID : Convert.ToInt32(dto.UpdatedBy),
                CreatedDate = toDto.AcademicCalendarID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.AcademicCalendarID > 0 ? DateTime.Now : dto.UpdatedDate,
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (entity.AcademicCalendarID == 0)
                {
                    //var maxGroupID = dbContext.AcadamicCalendars.Max(a => (long?)a.AcademicCalendarID);
                    //entity.AcademicCalendarID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;
                    dbContext.AcadamicCalendars.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.AcadamicCalendars.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.AcademicCalendarID));
        }

        public List<KeyValueDTO> GetCalendarByTypeID(byte calendarTypeID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var calendarList = new List<KeyValueDTO>();

                byte? activeStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("ACTIVE_CALENDAR_STATUS_ID");

                var entities = dbContext.AcadamicCalendars
                    .Where(c => c.CalendarTypeID == calendarTypeID && c.AcademicCalendarStatusID == activeStatusID && c.SchoolID == _context.SchoolID)
                    .AsNoTracking()
                    .ToList();

                foreach (var calendar in entities)
                {
                    calendarList.Add(new KeyValueDTO
                    {
                        Key = calendar.AcademicCalendarID.ToString(),
                        Value = calendar.CalenderName
                    });
                }

                return calendarList;
            }
        }

    }
}