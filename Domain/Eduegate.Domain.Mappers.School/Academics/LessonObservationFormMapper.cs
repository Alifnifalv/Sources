using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Mutual;
using Eduegate.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

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
            return JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private AcademicYearDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var repository = new EntiyRepository<AcademicYear, dbEduegateSchoolContext>(dbContext);
                var entity = repository.GetById(IID);

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
                var repository = new EntiyRepository<AcademicYear, dbEduegateSchoolContext>(dbContext);
                var entity = repository.GetById(academicYearID);

                var data = entity == null ? null : new AcademicYearDTO()
                {
                    AcademicYearID = entity.AcademicYearID,
                    Description = entity.Description,
                };

                if (data != null)
                {
                    academicList.Add(data);
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
                CreatedBy = toDto.AcademicYearID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.AcademicYearID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.AcademicYearID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.AcademicYearID > 0 ? DateTime.Now : dto.UpdatedDate,

            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var repository = new EntiyRepository<AcademicYear, dbEduegateSchoolContext>(dbContext);

                if (entity.AcademicYearID == 0)
                {
                    var maxGroupID = repository.GetMaxID(a => a.AcademicYearID);
                    var maxGroup = repository.GetMaxID(b => b.ORDERNO);
                    entity.AcademicYearID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;
                    entity.ORDERNO = maxGroup == null ? 1 : int.Parse(maxGroup.ToString()) + 1;
                    entity = repository.Insert(entity);
                }
                else
                {
                    entity = repository.Update(entity);
                }
            }

            return ToDTOString(ToDTO(entity.AcademicYearID));
        }

        public List<AcademicYearDTO> GetAcademicYearListData()
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var academicyearList = new List<AcademicYearDTO>();

                var data = dbContext.AcademicYears.Where(x => x.AcademicYearStatusID == 2 || x.AcademicYearStatusID == 3).ToList();

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

        public AcademicYearDTO GetAcademicYearDataByAcademicYearID(int? academicYearID)
        {
            var academicYearDTO = new AcademicYearDTO();
            var dateFormat = Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue("DateFormat");

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var academicYearData = dbContext.AcademicYears.FirstOrDefault(x => x.AcademicYearID == academicYearID);

                if (academicYearData != null)
                {
                    academicYearDTO = new AcademicYearDTO()
                    {
                        AcademicYearID = academicYearData.AcademicYearID,
                        EndDateString = academicYearData.EndDate.HasValue ? academicYearData.EndDate.Value.ToString(dateFormat) : null,
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
                var currentAcademicYearStatus = dbContext.Settings.FirstOrDefault(s => s.SettingCode == "CURRENT_ACADEMIC_YEAR_STATUSID").SettingValue;

                byte? currentAcademicStatusID = byte.Parse(currentAcademicYearStatus);

                var academicYearDatas = dbContext.AcademicYears.Where(a => a.AcademicYearStatusID == currentAcademicStatusID && a.IsActive == true).ToList();

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

                var academicYearList = dbContext.AcademicYears.Where(a => a.SchoolID == schoolID).ToList();

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

        public List<AcademicYearDTO> GetActiveAcademicYearListData()
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var academicyearList = new List<AcademicYearDTO>();

                var data = dbContext.AcademicYears.Where(x => x.IsActive == true).ToList();

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