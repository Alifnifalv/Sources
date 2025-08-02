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
    public class LessonOutcomeMapper : DTOEntityDynamicMapper
    {
        public static LessonOutcomeMapper Mapper(CallContext context)
        {
            var mapper = new LessonOutcomeMapper();
            mapper._context = context;
            return mapper;
        }
        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<LessonOutcomeDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private LessonOutcomeDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.LessonLearningOutcomes.Where(x => x.LessonLearningOutcomeID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTO(entity);
            }
        }

        private LessonOutcomeDTO ToDTO(LessonLearningOutcome entity)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
            var LessonOutcomeDTO = new LessonOutcomeDTO()
            {
                IsActive = entity.IsActive,
                LessonLearningOutcomeID = entity.LessonLearningOutcomeID,
                LessonLearningOutcomeName = entity.LessonLearningOutcomeName,
                CreatedBy = entity.CreatedBy,
                UpdatedBy = entity.UpdatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
            };

            return LessonOutcomeDTO;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as LessonOutcomeDTO;
            using (var dbContext = new dbEduegateSchoolContext())
            {
               
                var entity = new LessonLearningOutcome()
                {
                    LessonLearningOutcomeID = toDto.LessonLearningOutcomeID,
                    IsActive = toDto.IsActive,
                    CreatedBy = toDto.LessonLearningOutcomeID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.LessonLearningOutcomeID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.LessonLearningOutcomeID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.LessonLearningOutcomeID > 0 ? DateTime.Now : dto.UpdatedDate,
                    LessonLearningOutcomeName = toDto.LessonLearningOutcomeName,
                };

                // If it's a new Chapter
                if (toDto.LessonLearningOutcomeID == 0)
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
                return ToDTOString(ToDTO(entity.LessonLearningOutcomeID));
            }
        }

    


    }
}