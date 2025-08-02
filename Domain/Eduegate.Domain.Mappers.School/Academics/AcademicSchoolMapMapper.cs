using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.School.Academics;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Academics
{
    public class AcademicSchoolMapMapper : DTOEntityDynamicMapper
    {

        public static AcademicSchoolMapMapper Mapper(CallContext context)
        {
            var mapper = new AcademicSchoolMapMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<SchoolDateSettingDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private SchoolDateSettingDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.SchoolDateSettings.Where(x => x.SchoolDateSettingIID == IID)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.SchoolDateSettingMaps)
                    .AsNoTracking()
                    .FirstOrDefault();

                var DTO = new SchoolDateSettingDTO()
                {
                    SchoolDateSettingIID = entity.SchoolDateSettingIID,
                    SchoolID = entity.SchoolID,
                    AcademicYearID = entity.AcademicYearID,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedDate = entity.UpdatedDate,
                    //AcademicYear = entity.AcademicYearID.HasValue ? new KeyValueDTO() { Key = entity.AcademicYearID.Value.ToString(), Value = entity.AcademicYear.Description + " " + "( " + entity.AcademicYear.AcademicYearCode + ")" } : new KeyValueDTO(),
                };

                DTO.SchoolDateSettingMaps = new List<SchoolDateSettingMapDTO>();

                if (entity.SchoolDateSettingMaps.Count > 0)
                {
                    foreach (var schoolSettings in entity.SchoolDateSettingMaps)
                    {
                        if (schoolSettings.MonthID.HasValue)
                        {
                            DTO.SchoolDateSettingMaps.Add(new SchoolDateSettingMapDTO()
                            {
                                SchoolDateSettingMapsIID = schoolSettings.SchoolDateSettingMapsIID,
                                SchoolDateSettingID = schoolSettings.SchoolDateSettingID.HasValue ? schoolSettings.SchoolDateSettingID : null,
                                MonthID = schoolSettings.MonthID,
                                YearID = schoolSettings.YearID,
                                //MonthName = schoolSettings.MonthName,
                                MonthName = new DateTime(Convert.ToInt32(schoolSettings.YearID), Convert.ToInt32(schoolSettings.MonthID), 1).ToString("MMMM", CultureInfo.InvariantCulture),
                                Description = schoolSettings.Description,
                                TotalWorkingDays = schoolSettings.TotalWorkingDays,
                                PayrollCutoffDate = schoolSettings.PayrollCutoffDate,
                                CreatedBy = entity.CreatedBy,
                                CreatedDate = entity.CreatedDate,
                                UpdatedBy = entity.UpdatedBy,
                                UpdatedDate = entity.UpdatedDate
                            });
                        }
                    }
                }

                return DTO;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as SchoolDateSettingDTO;

            if (toDto.AcademicYearID == null)
            {
                throw new Exception("Please select academic year!");
            }

            if (toDto.SchoolID == null)
            {
                throw new Exception("Please select atleast one school!");
            }

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = new SchoolDateSetting()
                {
                    SchoolDateSettingIID = toDto.SchoolDateSettingIID,
                    AcademicYearID = toDto.AcademicYearID,
                    SchoolID = toDto.SchoolID,
                    CreatedBy = toDto.SchoolDateSettingIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.SchoolDateSettingIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.SchoolDateSettingIID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.SchoolDateSettingIID > 0 ? DateTime.Now : dto.UpdatedDate,
                };

                var IIDs = toDto.SchoolDateSettingMaps
                    .Select(a => a.SchoolDateSettingMapsIID).ToList();

                //delete maps
                var entities = dbContext.SchoolDateSettingMaps.Where(x =>
                    x.SchoolDateSettingID == entity.SchoolDateSettingIID &&
                    !IIDs.Contains(x.SchoolDateSettingMapsIID)).AsNoTracking().ToList();

                if (entities.IsNotNull())
                    dbContext.SchoolDateSettingMaps.RemoveRange(entities);

                foreach (var dateSettings in toDto.SchoolDateSettingMaps)
                {
                    entity.SchoolDateSettingMaps.Add(new SchoolDateSettingMap()
                    {
                        SchoolDateSettingMapsIID = dateSettings.SchoolDateSettingMapsIID,
                        SchoolDateSettingID = entity.SchoolDateSettingIID,
                        MonthID = dateSettings.MonthID,
                        YearID = dateSettings.YearID,
                        Description = dateSettings.Description,
                        TotalWorkingDays = dateSettings.TotalWorkingDays,
                        PayrollCutoffDate = dateSettings.PayrollCutoffDate,
                        CreatedBy = toDto.SchoolDateSettingIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                        UpdatedBy = toDto.SchoolDateSettingIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                        CreatedDate = toDto.SchoolDateSettingIID == 0 ? DateTime.Now : dto.CreatedDate,
                        UpdatedDate = toDto.SchoolDateSettingIID > 0 ? DateTime.Now : dto.UpdatedDate,
                    });
                }

                dbContext.SchoolDateSettings.Add(entity);

                if (entity.SchoolDateSettingIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                foreach (var dateSettings in entity.SchoolDateSettingMaps)
                {
                    if (dateSettings.SchoolDateSettingMapsIID != 0)
                    {
                        dbContext.Entry(dateSettings).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                    else
                    {
                        dbContext.Entry(dateSettings).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }
                }

                dbContext.SaveChanges();

                return ToDTOString(ToDTO(entity.SchoolDateSettingIID));
            }
        }

    }
}