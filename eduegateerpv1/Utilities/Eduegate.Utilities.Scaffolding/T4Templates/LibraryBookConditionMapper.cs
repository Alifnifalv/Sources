using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.School.Library;
using Eduegate.Framework;
using System;


namespace Eduegate.Domain.Mappers.School.Library
{
    public class LibraryBookConditionMapper : DTOEntityDynamicMapper
    {   
        public static  LibraryBookConditionMapper Mapper(CallContext context)
        {
            var mapper = new  LibraryBookConditionMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<LibraryBookConditionDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
             return ToDTOString(ToDTO(IID));
        }

        private LibraryBookConditionDTO ToDTO(long IID)
        {
            using (dbEduegateSchoolContext dbContext = new dbEduegateSchoolContext())
            {
                var repository = new EntiyRepository<LibraryBookCondition, dbEduegateSchoolContext>(dbContext);
                var entity = repository.GetById(IID);

                return new LibraryBookConditionDTO()
                {
                    BookConditionID = entity.BookConditionID,
                    BookConditionName = entity.BookConditionName,
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as LibraryBookConditionDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new LibraryBookCondition()
            {
                BookConditionID = toDto.BookConditionID,
                BookConditionName = toDto.BookConditionName,
            };

            using (dbEduegateSchoolContext dbContext = new dbEduegateSchoolContext())
            {
                var repository = new EntiyRepository<LibraryBookCondition, dbEduegateSchoolContext>(dbContext);

                if (entity.LibraryBookConditionID == 0)
                {
                    var maxGroupID = repository.GetMaxID(a => a.LibraryBookConditionID);
                    entity.LibraryBookConditionID = maxGroupID == null ? 1 : int.Parse(maxGroupID.ToString()) + 1;
                    entity = repository.Insert(entity);
                }
                else
                {
                    entity = repository.Update(entity);
                }
            }

            return ToDTOString(ToDTO(entity.LibraryBookConditionID ));
        }       
    }
}




