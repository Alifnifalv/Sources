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
    public class LibraryBookTypeMapper : DTOEntityDynamicMapper
    {   
        public static  LibraryBookTypeMapper Mapper(CallContext context)
        {
            var mapper = new  LibraryBookTypeMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<LibraryBookTypeDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
             return ToDTOString(ToDTO(IID));
        }

        private LibraryBookTypeDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.LibraryBookTypes.Where(a => a.LibraryBookTypeID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new LibraryBookTypeDTO()
                {
                    LibraryBookTypeID = entity.LibraryBookTypeID,
                    BookTypeName = entity.LibraryBookName,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    UpdatedBy = entity.UpdatedBy
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as LibraryBookTypeDTO;
            //convert the dto to entity and pass to the repository.
            var entity = new LibraryBookType()
            {
                LibraryBookTypeID = toDto.LibraryBookTypeID,
                LibraryBookName = toDto.BookTypeName,
                CreatedBy = toDto.LibraryBookTypeID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.LibraryBookTypeID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.LibraryBookTypeID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.LibraryBookTypeID > 0 ? DateTime.Now : dto.UpdatedDate,
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {               
                if (entity.LibraryBookTypeID == 0)
                {                   
                    var maxGroupID = dbContext.LibraryBookTypes.Max(a => (byte?)a.LibraryBookTypeID);
                    entity.LibraryBookTypeID = Convert.ToByte(maxGroupID == null ? 1 : Convert.ToByte(maxGroupID.ToString()) + 1);
                    dbContext.LibraryBookTypes.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.LibraryBookTypes.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.LibraryBookTypeID ));
        }       
    }
}




