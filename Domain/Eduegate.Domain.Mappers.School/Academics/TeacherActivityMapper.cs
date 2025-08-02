using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Framework;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.School.Academics
{
    public class TeacherActivityMapper : DTOEntityDynamicMapper
    {   
        public static  TeacherActivityMapper Mapper(CallContext context)
        {
            var mapper = new  TeacherActivityMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<TeacherActivityDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
             return ToDTOString(ToDTO(IID));
        }

        private TeacherActivityDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.TeacherActivities.Where(X => X.TeacherActivityIID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new TeacherActivityDTO()
                {
                    TeacherActivityIID = entity.TeacherActivityIID,
                    EmployeeID = entity.EmployeeID,
                    ActivityDate = entity.ActivityDate,
                    TimeFrom = entity.TimeFrom,
                    TimeTo = entity.TimeTo,
                    SubjectID = entity.SubjectID,
                    ClassID = entity.ClassID,
                    SectionID = entity.SectionID,
                    ShiftID = entity.ShiftID,
                    TopicID = entity.TopicID,
                    SubTopicID = entity.SubTopicID,
                    PeriodID = entity.PeriodID,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as TeacherActivityDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new TeacherActivity()
            {
                TeacherActivityIID = toDto.TeacherActivityIID,
                EmployeeID = toDto.EmployeeID,
                ActivityDate = toDto.ActivityDate,
                TimeFrom = toDto.TimeFrom,
                TimeTo = toDto.TimeTo,
                SubjectID = toDto.SubjectID,
                ClassID = toDto.ClassID,
                SectionID = toDto.SectionID,
                ShiftID = toDto.ShiftID,
                TopicID = toDto.TopicID,
                SubTopicID = toDto.SubTopicID,
                PeriodID = toDto.PeriodID,
                CreatedBy = toDto.TeacherActivityIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.TeacherActivityIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.TeacherActivityIID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.TeacherActivityIID > 0 ? DateTime.Now : dto.UpdatedDate,
                //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (entity.TeacherActivityIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.TeacherActivityIID));
        }

    }
}