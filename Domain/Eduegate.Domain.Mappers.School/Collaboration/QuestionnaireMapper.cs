using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Collaboration;
using Eduegate.Framework;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Collaboration
{
    public class QuestionnaireMapper : DTOEntityDynamicMapper
    {
        public static QuestionnaireMapper Mapper(CallContext context)
        {
            var mapper = new QuestionnaireMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<QuestionnaireDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private QuestionnaireDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.Questionnaires.Where(a => a.QuestionnaireIID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new QuestionnaireDTO()
                {
                    QuestionnaireIID = entity.QuestionnaireIID,
                    Description = entity.Description,
                    QuestionnaireAnswerTypeID = entity.QuestionnaireAnswerTypeID,
                    MoreInfo = entity.MoreInfo,
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as QuestionnaireDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new Questionnaire()
            {
                QuestionnaireIID = toDto.QuestionnaireIID,
                Description = toDto.Description,
                QuestionnaireAnswerTypeID = toDto.QuestionnaireAnswerTypeID,
                MoreInfo = toDto.MoreInfo,
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                dbContext.Questionnaires.Add(entity);
                if (entity.QuestionnaireIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.QuestionnaireIID));
        }

    }
}