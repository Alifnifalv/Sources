using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Library;
using Eduegate.Framework;
using System;
using Eduegate.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Library
{
    public class LibraryBookCategoryMapper : DTOEntityDynamicMapper
    {   
        public static  LibraryBookCategoryMapper Mapper(CallContext context)
        {
            var mapper = new  LibraryBookCategoryMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<LibraryBookCategoryDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
             return ToDTOString(ToDTO(IID));
        }

        private LibraryBookCategoryDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.LibraryBookCategories.Where(x => x.LibraryBookCategoryID == IID)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new LibraryBookCategoryDTO()
                {
                    LibraryBookCategoryID = entity.LibraryBookCategoryID,
                    CategoryCode = entity.CategoryCode,
                    BookCategoryName = entity.BookCategoryName,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    UpdatedBy = entity.UpdatedBy

                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            MutualRepository mutualRepository = new MutualRepository();
            Entity.Models.Settings.Sequence sequence = new Entity.Models.Settings.Sequence();
            var toDto = dto as LibraryBookCategoryDTO;

            //if (toDto.LibraryBookCategoryID == 0)
            //{
            //    try
            //    {
            //        sequence = mutualRepository.GetNextSequence("BookCategoryCode");
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new Exception("Please generate sequence with 'BookCategoryCode'");
            //    }
            //}

            //convert the dto to entity and pass to the repository.
            var entity = new LibraryBookCategory()
            {
                LibraryBookCategoryID = toDto.LibraryBookCategoryID,
                CategoryCode = toDto.CategoryCode,
                BookCategoryName = toDto.BookCategoryName,
                CreatedBy = toDto.LibraryBookCategoryID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.LibraryBookCategoryID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.LibraryBookCategoryID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.LibraryBookCategoryID > 0 ? DateTime.Now : dto.UpdatedDate,

            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
               
                if (entity.LibraryBookCategoryID == 0)
                {                  
                    var maxGroupID = dbContext.LibraryBookCategories.Max(a => (long?)a.LibraryBookCategoryID);
                    entity.LibraryBookCategoryID = maxGroupID == null ? 1 : long.Parse(maxGroupID.ToString()) + 1;
                    dbContext.LibraryBookCategories.Add(entity);
                    dbContext.Entry(entity).State = EntityState.Added;
                }
                else
                {
                    dbContext.LibraryBookCategories.Add(entity);
                    dbContext.Entry(entity).State = EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.LibraryBookCategoryID ));
        }       
    }
}