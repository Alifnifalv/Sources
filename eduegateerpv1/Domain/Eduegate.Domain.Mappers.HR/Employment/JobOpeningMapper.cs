using System;
using System.Collections.Generic;
using Eduegate.Domain.Entity.HR.Models;
using Eduegate.Framework;
using Eduegate.Services.Contracts.HR;
using System.Linq;
using Eduegate.Domain.Repository.HR;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Newtonsoft.Json;
using Eduegate.Domain.Entity.HR;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Domain.Mappers.Employment
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
            return JsonConvert.DeserializeObject<JobOpeningDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return JsonConvert.SerializeObject(dto as JobOpeningDTO);
        }

        public List<JobOpeningDTO> ToDTO(List<AvailableJob> entities)
        {
            var dtos = new List<JobOpeningDTO>();

            foreach(var entity in entities)
            {
                dtos.Add(ToDTO(entity));
            }

            return dtos;
        }

        public JobOpeningDTO ToDTO(AvailableJob entity, List<AvailableJobCultureData> cultureDatas)
        {
            var dto = ToDTO(entity);
            dto.CultureDatas = new List<JobOpeningCultureDataDTO>();

            if (cultureDatas != null)
            {
                foreach(var cultureData in cultureDatas)
                {
                    dto.CultureDatas.Add(new JobOpeningCultureDataDTO()
                    {
                        CulturID = cultureData.CultureID,
                        JobIID = cultureData.JobIID,
                        JobDescription = cultureData.JobDescription,
                        JobDetail = cultureData.JobDetails,
                        JobTitle = cultureData.JobTitle,
                        CreatedBy = cultureData.CultureID == 0 ? (int)_context.LoginID : cultureData.CreatedBy,
                        UpdatedBy = cultureData.CultureID > 0 ? (int)_context.LoginID : cultureData.UpdatedBy,
                        CreatedDate = cultureData.CultureID == 0 ? DateTime.Now : cultureData.CreatedDate,
                        UpdatedDate = cultureData.CultureID > 0 ? DateTime.Now : cultureData.UpdatedDate,
                    });
                }
            }

            return dto;
        }

        public JobOpeningDTO ToDTO(AvailableJob entity)
        {
            var department = entity.DepartmentId > 0 ? new EmploymentServiceRepository().GetDepartment((int)entity.DepartmentId) : null;

            return new JobOpeningDTO()
            {
                Id = entity.Id.ToString(),
                PageId = entity.PageId.ToString(),
                DepartmentId = entity.DepartmentId,
                TypeOfJob = entity.TypeOfJob,
                JobDescription = entity.JobDescription,
                JobDetail = entity.JobDetails,
                JobTitle = entity.JobTitle,
                DepartmentName = department != null ? department.DepartmentName : null,
                CreatedDate = entity.JobIID == 0 ? DateTime.Now : entity.CreatedDate,
                JobStatus = entity.Status,
                JobIID = entity.JobIID,
                CreateDateAsString = entity.CreatedDate.HasValue ? entity.CreatedDate.Value.ToLongDateString() : string.Empty,
                Tags = entity.AvailableJobTags != null ? entity.AvailableJobTags.Select(a=> new KeyValueDTO()
                 {
                      Key = a.TagName.ToString(),
                      Value = a.TagName
                 }).ToList() : new List<KeyValueDTO>(),
                CultureDatas = entity.AvailableJobCultureDatas != null ? entity.AvailableJobCultureDatas.Select(a=> new JobOpeningCultureDataDTO()
                {
                    CulturID = a.CultureID,
                    JobIID = a.JobIID,
                    JobDescription = a.JobDescription,
                    JobTitle = a.JobTitle,
                    JobDetail = a.JobDetails,
                    CreatedBy = a.CultureID == 0 ? (int)_context.LoginID : a.CreatedBy,
                    UpdatedBy = a.CultureID > 0 ? (int)_context.LoginID : a.UpdatedBy,
                    CreatedDate = a.CultureID == 0 ? DateTime.Now : a.CreatedDate,
                    UpdatedDate = a.CultureID > 0 ? DateTime.Now : a.UpdatedDate,
                }).ToList() : null
            };
        }

        public AvailableJob ToEntity(JobOpeningDTO dto)
        {
            var entity = new AvailableJob()
            {
                PageId = dto.PageId == null ? Guid.NewGuid() : Guid.Parse(dto.PageId),
                DepartmentId = dto.DepartmentId,
                TypeOfJob = dto.TypeOfJob,
                JobDescription = dto.JobDescription,
                JobDetails = dto.JobDetail,
                JobTitle = dto.JobTitle,
                CreatedDate = dto.JobIID == 0 ? DateTime.Now : dto.CreatedDate,
                Status = dto.JobStatus,
                JobIID = dto.JobIID
            };

            if (dto.Id != null)
            {
                entity.Id = Guid.Parse(dto.Id);
            }
            else
            {
                entity.CreatedDate = dto.JobIID == 0 ? DateTime.Now : dto.CreatedDate;
                entity.Id = Guid.NewGuid();
            }

            foreach(var tag in dto.Tags)
            {
                entity.AvailableJobTags.Add(new AvailableJobTag() {
                     JobID = entity.JobIID,
                     TagName = tag.Value,
                });
            }

            foreach (var cultureData in dto.CultureDatas)
            {
                entity.AvailableJobCultureDatas.Add(new AvailableJobCultureData()
                {
                    JobIID = cultureData.JobIID,
                    CultureID = cultureData.CulturID,
                    JobDescription = cultureData.JobDescription,
                    JobDetails = cultureData.JobDetail,
                    JobTitle = cultureData.JobTitle,
                    CreatedBy = cultureData.JobIID == 0 ? (int)_context.LoginID : cultureData.CreatedBy,
                    UpdatedBy = cultureData.JobIID > 0 ? (int)_context.LoginID : cultureData.UpdatedBy,
                    CreatedDate = cultureData.JobIID == 0 ? DateTime.Now : cultureData.CreatedDate,
                    UpdatedDate = cultureData.JobIID > 0 ? DateTime.Now : cultureData.UpdatedDate
                });
            }

            return entity;
        }

        public List<AvailableJobCultureData> ToEntity(List<JobOpeningCultureDataDTO> dtos)
        {
            var cultureDatas = new List<AvailableJobCultureData>();

            foreach (var dto in dtos)
            {
                cultureDatas.Add(ToEntity(dto));
            }

            return cultureDatas;
        }

        public AvailableJobCultureData ToEntity(JobOpeningCultureDataDTO dto)
        {
            var entity = new AvailableJobCultureData()
            {
                CultureID = dto.CulturID,
                JobDescription = dto.JobDescription,
                JobDetails = dto.JobDetail,
                JobTitle = dto.JobTitle,
                CreatedBy = dto.JobIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = dto.JobIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = dto.JobIID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = dto.JobIID > 0 ? DateTime.Now : dto.UpdatedDate,
                JobIID = dto.JobIID
            };

            return entity;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as JobOpeningDTO;
            //convert the dto to entity and pass to the repository.
            var entity = ToEntity(toDto);


            using (var dbContext = new dbEduegateHRContext())
            {
                dbContext.DB_Availablejobs.Add(entity);

                if (entity.JobIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    //delete all tags
                    var oldJobTags = dbContext.AvailableJobTags.Where(a => a.JobID == entity.JobIID).ToList();
                    if (oldJobTags.Count > 0)
                    {
                        dbContext.AvailableJobTags.RemoveRange(oldJobTags);
                    }

                    //delete all culture data
                    var oldCultureDatas = dbContext.AvailableJobCultureDatas.Where(a => a.JobIID == entity.JobIID).ToList();

                    if (oldCultureDatas.Count > 0)
                    {
                        dbContext.AvailableJobCultureDatas.RemoveRange(oldCultureDatas);
                    }

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return GetEntity(entity.JobIID);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(new EmploymentServiceRepository().GetJobOpening(IID)));
        }
    }
}