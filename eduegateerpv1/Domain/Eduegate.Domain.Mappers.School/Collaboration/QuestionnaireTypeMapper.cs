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
    public class QuestionnaireTypeMapper : DTOEntityDynamicMapper
    {   
        public static  QuestionnaireTypeMapper Mapper(CallContext context)
        {
            var mapper = new  QuestionnaireTypeMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<QuestionnaireTypeDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
             return ToDTOString(ToDTO(IID));
        }

        private QuestionnaireTypeDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.QuestionnaireTypes.Where(a => a.QuestionnaireTypeID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new QuestionnaireTypeDTO()
                {
                    QuestionnaireTypeID = entity.QuestionnaireTypeID,
                    TypeName = entity.TypeName,
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as QuestionnaireTypeDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new QuestionnaireType()
            {
                QuestionnaireTypeID = toDto.QuestionnaireTypeID,
                TypeName = toDto.TypeName,
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
               
                if (entity.QuestionnaireTypeID == 0)
                {                    
                    var maxGroupID = dbContext.QuestionnaireTypes.Max(a => (int?)a.QuestionnaireTypeID);
                    entity.QuestionnaireTypeID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;
                    dbContext.QuestionnaireTypes.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.QuestionnaireTypes.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.QuestionnaireTypeID ));
        }       
    }
}