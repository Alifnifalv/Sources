using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts;
using Eduegate.Domain.Entity.Models.ValueObjects;

namespace Eduegate.Domain.Mappers.CategoryCulture
{
    public class CategoryCultureMapper : IDTOEntityMapper<ProductCategoryDTO,CategoryCultureDatas>
    {
        public static CategoryCultureMapper Mapper()
        {
            var mapper = new CategoryCultureMapper();
            return mapper;
        }


        public ProductCategoryDTO ToDTO(CategoryCultureDatas entity)
        {
            if (entity != null)
            {
                var dto = new ProductCategoryDTO()
                {
                    CategoryID = entity.CategoryID,
                    CategoryCode = entity.CategoryCode,
                    CategoryName = entity.CategoryName,
                    SeoKeyWords = entity.CategoryKeyWords,
                    Active = entity.IsActive
                };

                return dto;
            }
            else
                return null;
        }

        public CategoryCultureDatas ToEntity(ProductCategoryDTO dto)
        {
            return null;
        }
    }
}
