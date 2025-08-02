using Eduegate.Domain.Entity.HR;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.HR.Designation;
using Newtonsoft.Json;
using System.Collections.Generic;
using Eduegate.Domain.Entity;
using Eduegate.Framework;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.HR.Settings
{
    public class JobTypeMapper : DTOEntityDynamicMapper
    {
        public static JobTypeMapper Mapper(CallContext context)
        {
            var mapper = new JobTypeMapper();
            mapper._context = context;
            return mapper;
        }

        public List<JobTypeDTO> ToDTO(List<JobType> entities)
        {
            var dtos = new List<JobTypeDTO>();

            foreach (var entity in entities)
            {
                dtos.Add(ToDTO(entity));
            }

            return dtos;
        }

        public JobTypeDTO ToDTO(JobType entity)
        {
            return new JobTypeDTO() { JobTypeID = entity.JobTypeID, JobTypeName = entity.JobTypeName };
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<JobTypeDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return JsonConvert.SerializeObject(dto as JobTypeDTO);
        }

        public override string GetEntity(long IID)
        {
            using (var dbContext = new dbEduegateHRContext())
            {              
                var entity = dbContext.JobTypes.Where(X => X.JobTypeID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTOString(new JobTypeDTO()
                {
                    JobTypeName = entity.JobTypeName,
                    JobTypeID = entity.JobTypeID,
                });
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {


            var toDto = dto as JobTypeDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new JobType()
            {
                JobTypeID = toDto.JobTypeID,
                JobTypeName = toDto.JobTypeName,
            };

            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                if (entity.JobTypeID == 0)
                {                   
                    var maxGroupID = dbContext.JobTypes.Max(a => (int?)a.JobTypeID);                   
                    entity.JobTypeID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;
                    dbContext.JobTypes.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.JobTypes.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(new JobTypeDTO()
            {
                JobTypeID = entity.JobTypeID,
                JobTypeName = entity.JobTypeName,
            });
        }
    }
}