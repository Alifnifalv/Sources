using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Collaboration;
using Eduegate.Framework;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Collaboration
{
    public class QuestionnaireSetMapper : DTOEntityDynamicMapper
    {   
        public static  QuestionnaireSetMapper Mapper(CallContext context)
        {
            var mapper = new  QuestionnaireSetMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<QuestionnaireSetDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
             return ToDTOString(ToDTO(IID));
        }

        private QuestionnaireSetDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.QuestionnaireSets.Where(a => a.QuestionnaireSetID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new QuestionnaireSetDTO()
                {
                    QuestionnaireSetID = entity.QuestionnaireSetID,
                    QuestionnaireSetName = entity.QuestionnaireSetName,
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as QuestionnaireSetDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new QuestionnaireSet()
            {
                QuestionnaireSetID = toDto.QuestionnaireSetID,
                QuestionnaireSetName = toDto.QuestionnaireSetName,
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
               
                if (entity.QuestionnaireSetID == 0)
                {                  
                    var maxGroupID = dbContext.QuestionnaireSets.Max(a => (int?)a.QuestionnaireSetID);
                    entity.QuestionnaireSetID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;
                    dbContext.QuestionnaireSets.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.QuestionnaireSets.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.QuestionnaireSetID ));
        }       
    }
}