using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Library;
using Eduegate.Framework;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
             return ToDTOString(ToDTO(IID));
        }

        private LibraryBookConditionDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.LibraryBookConditions.Where(x => x.BookConditionID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new LibraryBookConditionDTO()
                {
                    BookConditionID = entity.BookConditionID,
                    BookConditionName = entity.BookConditionName,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    UpdatedBy = entity.UpdatedBy
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
                CreatedBy = toDto.BookConditionID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.BookConditionID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.BookConditionID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.BookConditionID > 0 ? DateTime.Now : dto.UpdatedDate,
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {              
                if (entity.BookConditionID == 0)
                {                   
                    var maxGroupID = dbContext.LibraryBookConditions.Max(a => (byte?)a.BookConditionID);
                    entity.BookConditionID = Convert.ToByte(maxGroupID == null ? 1 : Convert.ToByte(maxGroupID.ToString()) + 1);
                    dbContext.LibraryBookConditions.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.LibraryBookConditions.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.BookConditionID));
        }       
    }
}