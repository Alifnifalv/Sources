using Newtonsoft.Json;
using Eduegate.Domain.Entity.HR;
using Eduegate.Domain.Entity.HR.Models;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.Employment
{
    public class JobDepartmentMapper : DTOEntityDynamicMapper
    {
        public static Eduegate.Domain.Mappers.Employment.JobDepartmentMapper Mapper(CallContext context)
        {
            var mapper = new Eduegate.Domain.Mappers.Employment.JobDepartmentMapper();
            mapper._context = context;
            return mapper;
        }

        public List<JobDepartmentDTO> ToDTO(List<DB_Department> entities)
        {
            var dtos = new List<JobDepartmentDTO>();

            foreach (var entity in entities)
            {
                dtos.Add(ToDTO(entity));
            }

            return dtos;
        }

        public JobDepartmentDTO ToDTO(DB_Department entity)
        {
            return new JobDepartmentDTO()
            {
                DepartmentID = entity.DepartmentID,
                DepartmentName = entity.DepartmentName
            };
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<JobDepartmentDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return JsonConvert.SerializeObject(dto as JobDepartmentDTO);
        }

        public override string GetEntity(long IID)
        {
            using (var dbContext = new dbEduegateHRContext())
            {
                var entity = dbContext.DB_Departments.Where(X => X.DepartmentID == IID)
                    .Include(i => i.DepartmentTags)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTOString(new JobDepartmentDTO()
                {
                    DepartmentName = entity.DepartmentName,
                    DepartmentID = entity.DepartmentID,
                    Logo = entity.Logo,
                    Tags = entity.DepartmentTags.Select(a => new KeyValueDTO() { Key = a.TagName.ToUpper(), Value = a.TagName }).ToList()
                });
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as JobDepartmentDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new DB_Department()
            {
                DepartmentName = toDto.DepartmentName,
                DepartmentID = toDto.DepartmentID,
                Logo = toDto.Logo,
                DepartmentTags = new List<DepartmentTag>()
            };

            using (var dbContext = new dbEduegateHRContext())
            {               
                foreach (var tag in toDto.Tags)
                {
                    entity.DepartmentTags.Add(new DepartmentTag()
                    {
                        DepartmentID = entity.DepartmentID,
                        TagName = tag.Value
                    });
                }

                if (entity.DepartmentID == 0)
                {
                    var maxGroupID =dbContext.DB_Departments.Max(a => (int?)a.DepartmentID);
                    entity.DepartmentID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;
                    dbContext.DB_Departments.Add(entity);
                }
                else
                {
                    dbContext.DB_Departments.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    //delete all tags                    
                    var tagsRepo = dbContext.DepartmentTags.Where(a => a.DepartmentID == entity.DepartmentID).ToList();                   
                   dbContext.DepartmentTags.RemoveRange(tagsRepo);                   
                }
                dbContext.SaveChanges();
            }

            return GetEntity(entity.DepartmentID);
        }

        public DB_Department ToEntity(JobDepartmentDTO dto)
        {
            throw new NotImplementedException();
        }

    }
}