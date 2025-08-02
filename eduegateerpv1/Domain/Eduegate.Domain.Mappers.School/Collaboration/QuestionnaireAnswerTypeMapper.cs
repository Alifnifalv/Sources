using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Collaboration;
using Eduegate.Framework;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Collaboration
{
    public class QuestionnaireAnswerTypeMapper : DTOEntityDynamicMapper
    {   
        public static  QuestionnaireAnswerTypeMapper Mapper(CallContext context)
        {
            var mapper = new  QuestionnaireAnswerTypeMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<QuestionnaireAnswerTypeDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
             return ToDTOString(ToDTO(IID));
        }

        private QuestionnaireAnswerTypeDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.QuestionnaireAnswerTypes.Where(a => a.QuestionnaireAnswerTypeID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new QuestionnaireAnswerTypeDTO()
                {
                    QuestionnaireAnswerTypeID = entity.QuestionnaireAnswerTypeID,
                    TypeName = entity.TypeName,
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as QuestionnaireAnswerTypeDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new QuestionnaireAnswerType()
            {
                QuestionnaireAnswerTypeID = toDto.QuestionnaireAnswerTypeID,
                TypeName = toDto.TypeName,
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
              
                if (entity.QuestionnaireAnswerTypeID == 0)
                {
                    var maxGroupID = dbContext.QuestionnaireAnswerTypes.Max(a => (int?)a.QuestionnaireAnswerTypeID);
                    entity.QuestionnaireAnswerTypeID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;
                    dbContext.QuestionnaireAnswerTypes.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.QuestionnaireAnswerTypes.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.QuestionnaireAnswerTypeID ));
        }       
    }
}