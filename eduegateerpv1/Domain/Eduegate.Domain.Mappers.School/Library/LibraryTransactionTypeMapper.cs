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
    public class LibraryTransactionTypeMapper : DTOEntityDynamicMapper
    {   
        public static  LibraryTransactionTypeMapper Mapper(CallContext context)
        {
            var mapper = new  LibraryTransactionTypeMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<LibraryTransactionTypeDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
             return ToDTOString(ToDTO(IID));
        }

        private LibraryTransactionTypeDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.LibraryTransactionTypes.Where(a => a.LibraryTransactionTypeID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new LibraryTransactionTypeDTO()
                {
                    LibraryTransactionTypeID = entity.LibraryTransactionTypeID,
                    TransactionTypeName = entity.TransactionTypeName,
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as LibraryTransactionTypeDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new LibraryTransactionType()
            {
                LibraryTransactionTypeID = toDto.LibraryTransactionTypeID,
                TransactionTypeName = toDto.TransactionTypeName,
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
              
                if (entity.LibraryTransactionTypeID == 0)
                {
                    var maxGroupID = dbContext.LibraryTransactionTypes.Max(a => (byte?)a.LibraryTransactionTypeID);
                    entity.LibraryTransactionTypeID = Convert.ToByte(maxGroupID == null ? 1 : byte.Parse(maxGroupID.ToString()) + 1);
                    dbContext.LibraryTransactionTypes.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.LibraryTransactionTypes.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.LibraryTransactionTypeID ));
        }       
    }
}