using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Framework;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Academics
{
    public class SubjectTopicMapper : DTOEntityDynamicMapper
    {
        public static SubjectTopicMapper Mapper(CallContext context)
        {
            var mapper = new SubjectTopicMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<SubjectTopicDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private SubjectTopicDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.SubjectTopics.Where(X => X.SubjectTopicIID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new SubjectTopicDTO()
                {
                    SubjectTopicIID = entity.SubjectTopicIID,
                    TopicName = entity.TopicName,
                    Duration = entity.Duration,
                    SubjectID = entity.SubjectID,
                    ClassID = entity.ClassID,
                    SectionID = entity.SectionID,
                    EmployeeID = entity.EmployeeID,
                    ParentTopicID = entity.ParentTopicID,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as SubjectTopicDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new SubjectTopic()
            {
                SubjectTopicIID = toDto.SubjectTopicIID,
                TopicName = toDto.TopicName,
                Duration = toDto.Duration,
                SubjectID = toDto.SubjectID,
                ClassID = toDto.ClassID,
                SectionID = toDto.SectionID,
                EmployeeID = toDto.EmployeeID,
                ParentTopicID = toDto.ParentTopicID,
                CreatedBy = toDto.SubjectTopicIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.SubjectTopicIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.SubjectTopicIID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.SubjectTopicIID > 0 ? DateTime.Now : dto.UpdatedDate,
                //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (entity.SubjectTopicIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.SubjectTopicIID));
        }
    }
}