using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Framework;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Academics
{
    public class TimeTableMapper : DTOEntityDynamicMapper
    {
        public static TimeTableMapper Mapper(CallContext context)
        {
            var mapper = new TimeTableMapper();
            mapper._context = context;
            return mapper;
        }
        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<TimeTableDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {               
                var entity = dbContext.TimeTables.Where(X => X.TimeTableID == IID)
                    .Include(x=>x.AcademicYear)
                    .AsNoTracking()
                    .FirstOrDefault();

                var timeTable = new TimeTableDTO()
                {
                    TimeTableID = entity.TimeTableID,
                    TimeTableDescription = entity.TimeTableDescription,
                    IsActivice = entity.IsActive,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    SchoolID = entity.SchoolID,
                    AcademicYearID = entity.AcademicYearID.HasValue? Convert.ToInt32(entity.AcademicYearID):0,
                    AcademicYear = new KeyValueDTO()
                    {
                        Key = entity.AcademicYearID.ToString(),
                        Value = entity.AcademicYear.Description
                        + " ( " + (Convert.ToDateTime(entity.AcademicYear.StartDate).ToString("dd-MM-yyyy")
                        + " - " + Convert.ToDateTime(entity.AcademicYear.EndDate).ToString("dd-MM-yyyy") + ")")
                    },
                };

                return ToDTOString(timeTable);
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as TimeTableDTO;
            if (toDto.AcademicYearID == 0)
            {
                throw new Exception("Please Select Academic Year");
            }
            //convert the dto to entity and pass to the repository.
            var entity = new TimeTable()
            {
                AcademicYearID = toDto.AcademicYearID,
                IsActive = toDto.IsActivice,
                TimeTableDescription = toDto.TimeTableDescription,
                SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                TimeTableID = toDto.TimeTableID,
                CreatedBy = toDto.TimeTableID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.TimeTableID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.TimeTableID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.TimeTableID > 0 ? DateTime.Now : dto.UpdatedDate,
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (entity.TimeTableID == 0)
                {
                    var maxGroupID = dbContext.TimeTables.Max(a => (int?)a.TimeTableID);
                    entity.TimeTableID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;
                    dbContext.TimeTables.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.TimeTables.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return GetEntity(entity.TimeTableID);
        }

    }
}