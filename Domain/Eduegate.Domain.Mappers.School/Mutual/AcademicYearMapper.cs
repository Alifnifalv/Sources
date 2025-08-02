using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Mutual;
using Eduegate.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.VisualBasic;

namespace Eduegate.Domain.Mappers.School.Mutual
{
    public class AcademicYearMapper : DTOEntityDynamicMapper
    {
        public static AcademicYearMapper Mapper(CallContext context)
        {
            var mapper = new AcademicYearMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<AcademicYearDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private AcademicYearDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.AcademicYears.Where(a => a.AcademicYearID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new AcademicYearDTO()
                {
                    AcademicYearID = entity.AcademicYearID,
                    ORDERNO = entity.ORDERNO,
                    AcademicYearCode = entity.AcademicYearCode,
                    Description = entity.Description,
                    StartDate = entity.StartDate,
                    EndDate = entity.EndDate,
                    SchoolID = entity.SchoolID,
                    AcademicYearStatusID = entity.AcademicYearStatusID,
                    IsActive = entity.IsActive,
                    IsUsedForApplication = entity.IsUsedForApplication,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    UpdatedBy = entity.UpdatedBy
                };
            }
        }

        public List<AcademicYearDTO> GetAcademicYear(int academicYearID)
        {
            var academicList = new List<AcademicYearDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.AcademicYears.Where(a => a.AcademicYearID == academicYearID)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (entity != null)
                {
                    academicList.Add(new AcademicYearDTO()
                    {
                        AcademicYearID = entity.AcademicYearID,
                        Description = entity.Description,
                    });
                }
            }

            return academicList;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as AcademicYearDTO;
            // Date Different Check
            if (toDto.StartDate >= toDto.EndDate)
            {
                throw new Exception("Select Date Properlly!!");
            }
            //convert the dto to entity and pass to the repository.
            var entity = new AcademicYear()
            {
                AcademicYearID = toDto.AcademicYearID,
                ORDERNO = toDto.ORDERNO,
                AcademicYearCode = toDto.AcademicYearCode,
                Description = toDto.Description,
                StartDate = toDto.StartDate,
                EndDate = toDto.EndDate,
                SchoolID = toDto.SchoolID,
                AcademicYearStatusID = toDto.AcademicYearStatusID,
                IsActive = toDto.IsActive,
                IsUsedForApplication = toDto.IsUsedForApplication,
                CreatedBy = toDto.AcademicYearID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.AcademicYearID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.AcademicYearID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.AcademicYearID > 0 ? DateTime.Now : dto.UpdatedDate,

            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (entity.AcademicYearID == 0)
                {
                    var maxGroupID = dbContext.AcademicYears.Max(a => (int?)a.AcademicYearID);
                    var maxGroup = dbContext.AcademicYears.Max(b => b.ORDERNO);
                    entity.AcademicYearID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;
                    entity.ORDERNO = maxGroup == null ? 1 : int.Parse(maxGroup.ToString()) + 1;
                    dbContext.AcademicYears.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.AcademicYears.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.AcademicYearID));
        }

        public List<AcademicYearDTO> GetAcademicYearListData()
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var academicyearList = new List<AcademicYearDTO>();

                var data = dbContext.AcademicYears.Where(x => x.AcademicYearStatusID == 2 || x.AcademicYearStatusID == 3).AsNoTracking().ToList();

                foreach (var ay in data)
                {
                    academicyearList.Add(new AcademicYearDTO()
                    {
                        AcademicYearID = ay.AcademicYearID,
                        Description = ay.Description,
                        AcademicYearCode = ay.AcademicYearCode,
                        SchoolID = ay.SchoolID
                    });
                }

                return academicyearList;
            }
        }
        public byte? GetSchoolByAcademicYearID(int? academicYearID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var academicYearData = dbContext.AcademicYears.Where(x => x.AcademicYearID == academicYearID).AsNoTracking().FirstOrDefault();

                if (academicYearData != null)
                {
                    return academicYearData.SchoolID;
                }

                return null;
            }
        }
        public AcademicYearDTO GetAcademicYearDataByAcademicYearID(int? academicYearID)
        {
            var academicYearDTO = new AcademicYearDTO();
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var academicYearData = dbContext.AcademicYears.Where(x => x.AcademicYearID == academicYearID).AsNoTracking().FirstOrDefault();

                if (academicYearData != null)
                {
                    academicYearDTO = new AcademicYearDTO()
                    {
                        AcademicYearID = academicYearData.AcademicYearID,
                        EndDateString = academicYearData.EndDate.HasValue ? academicYearData.EndDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                    };
                }

                return academicYearDTO;
            }
        }

        public List<AcademicYearDTO> GetCurrentAcademicYearsData()
        {
            var academicYearDTOs = new List<AcademicYearDTO>();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var currentAcademicStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<byte?>("CURRENT_ACADEMIC_YEAR_STATUSID");

                var academicYearDatas = dbContext.AcademicYears.Where(a => a.AcademicYearStatusID == currentAcademicStatusID && a.IsActive == true).AsNoTracking().ToList();

                foreach (var academic in academicYearDatas)
                {
                    academicYearDTOs.Add(new AcademicYearDTO()
                    {
                        AcademicYearID = academic.AcademicYearID,
                        AcademicYearCode = academic.AcademicYearCode,
                        Description = academic.Description,
                        StartDate = academic.StartDate,
                        EndDate = academic.EndDate,
                        SchoolID = academic.SchoolID,
                        IsActive = academic.IsActive,
                        AcademicYearStatusID = academic.AcademicYearStatusID,
                        ORDERNO = academic.ORDERNO,
                    });
                }

                return academicYearDTOs;
            }
        }

        public List<KeyValueDTO> GetAllAcademicYearBySchoolID(int schoolID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var academicKeyValueList = new List<KeyValueDTO>();

                var academicYearList = dbContext.AcademicYears.Where(a => a.SchoolID == schoolID).AsNoTracking().ToList();

                foreach (var academic in academicYearList)
                {
                    academicKeyValueList.Add(new KeyValueDTO()
                    {
                        Key = academic.AcademicYearID.ToString(),
                        Value = academic.Description + " " + "(" + academic.AcademicYearCode + ")"
                    });
                }

                return academicKeyValueList;
            }
        }

        public List<AcademicYearDTO> GetActiveAcademicYearListDataFromCache()
        {
            var cacheKey = string.Concat($"ActiveAcademicYear_{_context.SchoolID}");
            var dtos = Framework.CacheManager.MemCacheManager<List<AcademicYearDTO>>.Get(cacheKey);
            if (dtos == null)
            {
                dtos = GetActiveAcademicYearListData();
                Framework.CacheManager.MemCacheManager<List<AcademicYearDTO>>.Add(dtos, cacheKey);
            }
            return dtos;
        }

        public List<AcademicYearDTO> GetActiveAcademicYearListData()
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var academicyearList = new List<AcademicYearDTO>();

                var data = dbContext.AcademicYears.Where(x => x.IsActive == true).AsNoTracking().ToList();

                foreach (var ay in data)
                {
                    academicyearList.Add(new AcademicYearDTO()
                    {
                        AcademicYearID = ay.AcademicYearID,
                        Description = ay.Description,
                        AcademicYearCode = ay.AcademicYearCode,
                        SchoolID = ay.SchoolID
                    });
                }

                return academicyearList;
            }
        }

    }
}