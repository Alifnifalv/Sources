using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.School.Academics;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Academics
{
    public class LessonObjectiveMapper : DTOEntityDynamicMapper
    {
        public static LessonObjectiveMapper Mapper(CallContext context)
        {
            var mapper = new LessonObjectiveMapper();
            mapper._context = context;
            return mapper;
        }
        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<LessonObjectiveDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private LessonObjectiveDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.LessonLearningObjectives.Where(x => x.LessonLearningObjectiveID == IID)
            
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTO(entity);
            }
        }

        private LessonObjectiveDTO ToDTO(LessonLearningObjective entity)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
            var lessonObjectiveDTO = new LessonObjectiveDTO()
            {
                IsActive = entity.IsActive,
                LessonLearningObjectiveID = entity.LessonLearningObjectiveID,
                LessonLearningObjectiveName = entity.LessonLearningObjectiveName,
                CreatedBy = entity.CreatedBy,
                UpdatedBy = entity.UpdatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
            };

            return lessonObjectiveDTO;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as LessonObjectiveDTO;
            using (var dbContext = new dbEduegateSchoolContext())
            {

                if (string.IsNullOrEmpty(toDto.LessonLearningObjectiveName))
                {
                    throw new Exception("Please enter the chapter Lesson Objective Name!");
                }

                var entity = new LessonLearningObjective()
                {
                    LessonLearningObjectiveID = toDto.LessonLearningObjectiveID,
                    IsActive = toDto.IsActive,
                    CreatedBy = toDto.LessonLearningObjectiveID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.LessonLearningObjectiveID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.LessonLearningObjectiveID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.LessonLearningObjectiveID > 0 ? DateTime.Now : dto.UpdatedDate,
                    LessonLearningObjectiveName = toDto.LessonLearningObjectiveName,
                };

                // If it's a new Chapter
                if (toDto.LessonLearningObjectiveID == 0)
                {
                    //entity.CreatedBy = Convert.ToInt32(_context.LoginID);
                    entity.CreatedDate = DateTime.Now;
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else // Updating an existing Chapter
                {

                    //entity.UpdatedBy = Convert.ToInt32(_context.LoginID);
                    //entity.UpdatedDate = DateTime.Now;
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
                return ToDTOString(ToDTO(entity.LessonLearningObjectiveID));
            }
        }

    


    }
}