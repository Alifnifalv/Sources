using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using System;
using Eduegate.Services.Contracts.School.Academics;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Academics
{
    public class FunctionalPeriodsMapper : DTOEntityDynamicMapper
    {
        public static FunctionalPeriodsMapper Mapper(CallContext context)
        {
            var mapper = new FunctionalPeriodsMapper();
            mapper._context = context;
            return mapper;
        }
        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<FunctionalPeriodsDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private FunctionalPeriodsDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.FunctionalPeriods.Where(x => x.FunctionalPeriodID == IID)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.School)
                    .AsNoTracking()
                    .FirstOrDefault();

                var dto = new FunctionalPeriodsDTO()
                {
                    FunctionalPeriodID = entity.FunctionalPeriodID,
                    SchoolID = entity.SchoolID,
                    AcademicYearID = entity.AcademicYearID,
                    AcademicYearString = entity.AcademicYearID.HasValue ? entity.AcademicYear.Description + " (" + entity.AcademicYear.AcademicYearCode + ")" : null,
                    SchoolString = entity.SchoolID.HasValue ? entity.School.SchoolName : null,
                    FromDate = entity.FromDate,
                    ToDate = entity.ToDate,
                    CreatedBy = (int?)entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    UpdatedBy = (int?)entity.UpdatedBy
                };

                return dto;
            }
        }
        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as FunctionalPeriodsDTO;
            //checkings
            if (toDto.FromDate > toDto.ToDate)
            {
                throw new Exception("Select Dates Properlly !!");
            }

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dupDataChk = toDto.FunctionalPeriodID == 0 ? dbContext.FunctionalPeriods.AsNoTracking().FirstOrDefault(a => a.SchoolID == _context.SchoolID && a.AcademicYearID == _context.AcademicYearID) : null;
                if (dupDataChk != null && toDto.FunctionalPeriodID == 0)
                {
                    throw new Exception("For this Academic Year functional period is already exists");
                }

                var entity = new FunctionalPeriod()
                {
                    FunctionalPeriodID = toDto.FunctionalPeriodID,
                    SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                    AcademicYearID = toDto.AcademicYearID == null ? _context.AcademicYearID : toDto.AcademicYearID,
                    FromDate = toDto.FromDate,
                    ToDate = toDto.ToDate,
                    ////TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                    CreatedBy = toDto.FunctionalPeriodID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.FunctionalPeriodID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.FunctionalPeriodID == 0 ? DateTime.Now.Date : dto.CreatedDate,
                    UpdatedDate = toDto.FunctionalPeriodID > 0 ? DateTime.Now.Date : dto.UpdatedDate,
                };
                dbContext.FunctionalPeriods.Add(entity);

                if (entity.FunctionalPeriodID == 0)
                {                  
                    var maxGroupID = dbContext.FunctionalPeriods.Max(a => (int?)a.FunctionalPeriodID);
                    entity.FunctionalPeriodID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;
                    dbContext.FunctionalPeriods.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.FunctionalPeriods.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();              

                return GetEntity(entity.FunctionalPeriodID);
            }
        }

    }
}