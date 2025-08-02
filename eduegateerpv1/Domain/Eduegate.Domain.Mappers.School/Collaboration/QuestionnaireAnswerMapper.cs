using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Collaboration;
using Eduegate.Framework;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Collaboration
{
    public class QuestionnaireAnswerMapper : DTOEntityDynamicMapper
    {
        public static QuestionnaireAnswerMapper Mapper(CallContext context)
        {
            var mapper = new QuestionnaireAnswerMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<QuestionnaireAnswerDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private QuestionnaireAnswerDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.QuestionnaireAnswers.Where(a => a.QuestionnaireAnswerIID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new QuestionnaireAnswerDTO()
                {
                    QuestionnaireAnswerIID = entity.QuestionnaireAnswerIID,
                    QuestionnaireAnswerTypeID = entity.QuestionnaireAnswerTypeID,
                    QuestionnaireID = entity.QuestionnaireID,
                    Answer = entity.Answer,
                    MoreInfo = entity.MoreInfo,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    //TimeStamps = Convert.ToBase64String(entity.TimeStamps),
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as QuestionnaireAnswerDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new QuestionnaireAnswer()
            {
                QuestionnaireAnswerIID = toDto.QuestionnaireAnswerIID,
                QuestionnaireAnswerTypeID = toDto.QuestionnaireAnswerTypeID,
                QuestionnaireID = toDto.QuestionnaireID,
                Answer = toDto.Answer,
                MoreInfo = toDto.MoreInfo,
                CreatedBy = toDto.QuestionnaireAnswerIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.QuestionnaireAnswerIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.QuestionnaireAnswerIID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.QuestionnaireAnswerIID > 0 ? DateTime.Now : dto.UpdatedDate,
                //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                dbContext.QuestionnaireAnswers.Add(entity);
                if (entity.QuestionnaireAnswerIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.QuestionnaireAnswerIID));
        }

    }
}