using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Jobs;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Domain.Mappers.HR.Employment
{
    public class JobOpeningMapper : DTOEntityDynamicMapper
    {
        public static JobOpeningMapper Mapper(CallContext context)
        {
            var mapper = new JobOpeningMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<JobsDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var dto = new JobsDTO();

                var entity = dbContext.AvailableJobs.Where(X => X.JobIID == IID)
                           .Include(x => x.Country)
                           .Include(x => x.AvailableJobSkillMaps)
                           .Include(x => x.AvailableJobCriteriaMaps)
                           .AsNoTracking()
                           .FirstOrDefault();

                var jdref = dbContext.JobDescriptions.FirstOrDefault(j => j.JDMasterIID == entity.JDReferenceID);

                dto.JobIID = entity.JobIID;
                dto.JobStatusID = entity.StatusID;
                dto.JobTypeID = entity.JobTypeID;
                dto.DepartmentID = entity.DepartmentId;
                dto.DesignationID = entity.DesignationID;
                dto.JobTitle = entity.JobTitle;
                dto.JobDescription = entity.JobDescription;
                dto.JobDetails = entity.JobDetails;
                dto.MonthlySalary = entity.MonthlySalary;
                dto.TotalYearOfExperience = entity.TotalYearOfExperience;
                dto.NoOfVacancies = entity.NoOfVacancies; 
                dto.CountryID = entity.CountryID;
                dto.Country = entity.CountryID.HasValue ? new KeyValueDTO { Key = entity.CountryID.ToString(), Value = entity.Country?.CountryName } : new KeyValueDTO();
                dto.JDReference = entity.JDReferenceID.HasValue ? new KeyValueDTO { Key = entity.JDReferenceID.ToString(), Value = jdref?.Title.ToString() } : new KeyValueDTO();
                dto.JDReferenceID = entity.JDReferenceID;
                dto.CreatedBy = entity.CreatedBy;
                dto.CreatedDate = entity.CreatedDate;
                dto.UpdatedBy = entity.UpdatedBy;
                dto.UpdatedDate = entity.UpdatedDate;
                dto.PublishDate = entity.PublishDate;
                dto.ClosingDate = entity.ClosingDate;
                dto.Location = entity.Location;
                dto.SkillList = new List<KeyValueDTO>();

                if(entity.AvailableJobSkillMaps.Count > 0)
                {
                    dto.SkillList = entity.AvailableJobSkillMaps.Select(x => new KeyValueDTO()
                    {
                        Key = x.SkillID.ToString(),
                        Value = x.Skill.ToString(),
                    }).ToList();
                }

                if (entity.AvailableJobCriteriaMaps.Count > 0)
                {
                    dto.AvailableJobCriteriaMapDTO = entity.AvailableJobCriteriaMaps.Select(y => new AvailableJobCriteriaMapDTO()
                    {
                        CriteriaID = y.CriteriaID,
                        JobID = y.JobID,
                        TypeID = y.TypeID,
                        QualificationID = y.QualificationID,
                        FieldOfStudy = y.FieldOfStudy,
                    }).ToList();
                }


                return ToDTOString(dto);
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as JobsDTO;
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if(toDto.JobIID != 0)
                {
                    #region remove map data issue when use this duplicate entry happens when modifies
                    //if (toDto.SkillList.Count > 0)
                    //{
                    //    var skillIDs = toDto.SkillList
                    //            .Where(a => long.TryParse(a.Key, out _)) // Filter out non-numeric strings  
                    //            .Select(a => long.Parse(a.Key))
                    //            .ToList();

                    //    //Remove not listed skills
                    //    var skillRemove = dbContext.AvailableJobSkills
                    //        .Where(x => x.JobID == toDto.JobIID && !skillIDs.Contains(x.SkillID))
                    //        .AsNoTracking()
                    //        .ToList();

                    //    if (skillRemove.Any())
                    //    {
                    //        dbContext.AvailableJobSkills.RemoveRange(skillRemove);
                    //        dbContext.SaveChanges();
                    //    }

                    //}

                    //if (toDto.AvailableJobCriteriaMapDTO.Count > 0)
                    //{
                    //    var criteriaIDs = toDto.AvailableJobCriteriaMapDTO
                    //    .Select(a => a.CriteriaID)
                    //    .ToList();

                    //    //Remove not listed criterias
                    //    var criteriaRemove = dbContext.AvailableJobCriterias
                    //        .Where(x => x.JobID == toDto.JobIID && !criteriaIDs.Contains(x.CriteriaID))
                    //        .AsNoTracking()
                    //        .ToList();

                    //    if (criteriaRemove.Any())
                    //    {
                    //        dbContext.AvailableJobCriterias.RemoveRange(criteriaRemove);
                    //        dbContext.SaveChanges();
                    //    }
                    //}
                    #endregion

                    #region remove list 
                    var getOldSkillList = dbContext.AvailableJobSkills.Where(x => x.JobID == toDto.JobIID).ToList();
                    if (getOldSkillList.Any())
                    {
                        dbContext.AvailableJobSkills.RemoveRange(getOldSkillList);
                        dbContext.SaveChanges();
                    } 
                    var getOldCriteriaList = dbContext.AvailableJobCriterias.Where(x => x.JobID == toDto.JobIID).ToList();
                    if (getOldCriteriaList.Any())
                    {
                        dbContext.AvailableJobCriterias.RemoveRange(getOldCriteriaList);
                        dbContext.SaveChanges();
                    }
                    #endregion
                }
                //convert the dto to entity and pass to the repository.
                var entity = new AvailableJob()
                {
                    JobIID = toDto.JobIID,
                    JobTitle = toDto.JobTitle,
                    JobDescription = toDto.JobDescription,
                    JobDetails = toDto.JobDetails,
                    DepartmentId = toDto.DepartmentID,
                    DesignationID = toDto.DesignationID,
                    JobTypeID = toDto.JobTypeID,
                    StatusID = toDto.JobStatusID,
                    SchoolID = (byte?)_context.SchoolID,
                    MonthlySalary = toDto.MonthlySalary,
                    TotalYearOfExperience = toDto.TotalYearOfExperience,
                    NoOfVacancies = toDto.NoOfVacancies,
                    CountryID = toDto.CountryID,
                    JDReferenceID = toDto.JDReferenceID,
                    PublishDate = toDto.PublishDate,
                    ClosingDate = toDto.ClosingDate,
                    Location = toDto.Location,
                    CreatedBy = toDto.JobIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.JobIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.JobIID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.JobIID > 0 ? DateTime.Now : dto.UpdatedDate,
                };

                if(toDto.SkillList.Count > 0)
                {
                    entity.AvailableJobSkillMaps = toDto.SkillList.Select(skill => new AvailableJobSkillMap()
                    {
                        Skill = skill.Value.ToString(),
                    }).ToList();
                }

                if(toDto.AvailableJobCriteriaMapDTO.Count > 0)
                {
                    entity.AvailableJobCriteriaMaps = toDto.AvailableJobCriteriaMapDTO.Select(cr => new AvailableJobCriteriaMap()
                    {
                        TypeID = cr.TypeID,
                        QualificationID = cr.QualificationID,
                        FieldOfStudy = cr.FieldOfStudy
                    }).Where(cr => cr.TypeID.HasValue).ToList();
                }

                if (entity.JobIID == 0)
                {
                    dbContext.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = EntityState.Modified;
                }

                dbContext.AddRange(entity.AvailableJobSkillMaps);
                dbContext.AddRange(entity.AvailableJobCriteriaMaps);
                dbContext.SaveChanges();

                return GetEntity(entity.JobIID);
            }
        }
    }
}