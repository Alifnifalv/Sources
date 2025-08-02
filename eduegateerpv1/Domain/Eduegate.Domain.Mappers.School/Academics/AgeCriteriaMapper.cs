using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using Eduegate.Services.Contracts.School.Academics;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Academics
{
    public class AgeCriteriaMapper : DTOEntityDynamicMapper
    {
        public static AgeCriteriaMapper Mapper(CallContext context)
        {
            var mapper = new AgeCriteriaMapper();
            mapper._context = context;
            return mapper;
        }
        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<AgeCriteriaDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private AgeCriteriaDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var ageCriteriaData = dbContext.AgeCriterias.Where(X => X.AgeCriteriaIID == IID).Include(i => i.Class).AsNoTracking().FirstOrDefault();
                var criteriaList = ageCriteriaData != null ? dbContext.AgeCriterias.Where(X => X.AcademicYearID == ageCriteriaData.AcademicYearID && X.SchoolID == ageCriteriaData.SchoolID)
                    .Include(i => i.Class)
                    .AsNoTracking()
                    .ToList() : null;

                var ageclasslist = new List<AgeCriteriaMapDTO>();
                AgeCriteria ageMap = null;
                foreach (var ageMapDet in criteriaList)
                {
                    ageclasslist.Add(new AgeCriteriaMapDTO()
                    {
                        ClassID = ageMapDet.ClassID,
                        Class = new KeyValueDTO()
                        {
                            Key = ageMapDet.ClassID.ToString(),
                            Value = ageMapDet.Class.ClassDescription
                        },
                        BirthFrom = ageMapDet.BirthFrom,
                        BirthTo = ageMapDet.BirthTo,
                        MaxAge = ageMapDet.MaxAge,
                        MinAge = ageMapDet.MinAge,
                        IsActive = ageMapDet.IsActive,
                        CreatedBy = ageMapDet.CreatedBy,
                        UpdatedBy = ageMapDet.UpdatedBy,
                        CreatedDate = ageMapDet.CreatedDate,
                        UpdatedDate = ageMapDet.UpdatedDate,
                    });
                    ageMap = ageMapDet;
                }

                return new AgeCriteriaDTO()
                {
                    CurriculumID = ageMap.CurriculumID,
                    AgeCriteriaIID = ageMap.AgeCriteriaIID,
                    SchoolID = ageMap.SchoolID,
                    AcademicYearID = ageMap.AcademicYearID,
                    CreatedBy = ageMap.CreatedBy,
                    UpdatedBy = ageMap.UpdatedBy,
                    CreatedDate = ageMap.CreatedDate,
                    UpdatedDate = ageMap.UpdatedDate,
                    AgeCriteriaMap = ageclasslist,
                };
            }
        }
        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as AgeCriteriaDTO;
            //checkings
            foreach (var check in toDto.AgeCriteriaMap)
            {
                if (check.BirthFrom >= check.BirthTo)
                {
                    throw new Exception("Select Bith From/To Date Properlly!!");
                }
                if (check.MinAge >= check.MaxAge)
                {
                    throw new Exception("Please Check Min/Max age Properlly!!");
                }
            }
            //convert the dto to entity and pass to the repository.
            using (var dbContext = new dbEduegateSchoolContext())
            {
                //delete all mapping and recreate
                foreach (var classIDs in toDto.AgeCriteriaMap)
                {
                    var getDataEntity = dbContext.AgeCriterias.Where(X => X.AcademicYearID == toDto.AcademicYearID && X.SchoolID == _context.SchoolID && X.CurriculumID == toDto.CurriculumID && X.ClassID == classIDs.ClassID).AsNoTracking().ToList();
                    if (getDataEntity.Count != 0 || getDataEntity.Count > 0)
                    {
                        dbContext.AgeCriterias.RemoveRange(getDataEntity);
                    }
                }

                AgeCriteria classAgeMap = null;
                foreach (var markgr in toDto.AgeCriteriaMap)
                {
                    var entity = new AgeCriteria();
                    entity.AgeCriteriaIID = toDto.AgeCriteriaIID;
                    entity.ClassID = markgr.ClassID;
                    entity.BirthFrom = markgr.BirthFrom;
                    entity.BirthTo = markgr.BirthTo;
                    entity.MinAge = markgr.MinAge;
                    entity.MaxAge = markgr.MaxAge;
                    entity.IsActive = markgr.IsActive;
                    entity.AcademicYearID = toDto.AcademicYearID;
                    entity.CurriculumID = toDto.CurriculumID;
                    entity.SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID;
                    entity.CreatedBy = markgr.AgeCriteriaIID == 0 ? (int)_context.LoginID : dto.CreatedBy;
                    entity.UpdatedBy = markgr.AgeCriteriaIID > 0 ? (int)_context.LoginID : dto.UpdatedBy;
                    entity.CreatedDate = markgr.AgeCriteriaIID == 0 ? DateTime.Now.Date : dto.CreatedDate;
                    entity.UpdatedDate = markgr.AgeCriteriaIID > 0 ? DateTime.Now.Date : dto.UpdatedDate;
                    //entity.TimeStamps = markgr.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps);


                    if (entity.AgeCriteriaIID == 0)
                    {
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }
                    else
                    {
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                    dbContext.SaveChanges();

                    classAgeMap = entity;
                };

                return GetEntity(classAgeMap.AgeCriteriaIID);
            }
        }

        public List<AgeCriteriaMapDTO> GetAgeCriteriaByClassID(int classId, int academicYearID)
        {
            var AgeCriteriaMapDTO = new List<AgeCriteriaMapDTO>();
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
            using (var dbContext = new dbEduegateSchoolContext())
            {
                AgeCriteriaMapDTO = (from s in dbContext.AgeCriterias
                                     where s.IsActive == true && s.AcademicYearID == academicYearID
                                     && s.ClassID == classId
                                     select new AgeCriteriaMapDTO()
                                     {
                                         AgeCriteriaIID = s.AgeCriteriaIID,
                                         ClassID = s.ClassID,
                                         //Class = s.ClassID.HasValue ? new KeyValueDTO() { Key = s.ClassID.ToString(), Value = s.Class.ClassDescription} : new KeyValueDTO(),
                                         BirthFrom = s.BirthFrom,
                                         BirthTo = s.BirthTo,
                                         BirthFromString = s.BirthFrom.Value.ToString(),
                                         BirthToString = s.BirthTo.Value.ToString(),
                                         IsActive = s.IsActive,
                                         MinAge = s.MinAge,
                                         MaxAge = s.MaxAge,
                                     }).AsNoTracking().ToList();
            }
            return AgeCriteriaMapDTO;
        }

    }
}