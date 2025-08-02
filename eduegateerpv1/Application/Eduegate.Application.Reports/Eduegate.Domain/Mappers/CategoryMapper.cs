using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Domain.Mappers
{
    public class CategoryMapper : IDTOEntityMapper<CategoryDTO, Category>
    {
        private CallContext _context;

        public static CategoryMapper Mapper(CallContext context)
        {
            var mapper = new CategoryMapper();
            mapper._context = context;
            return mapper;
        }

        public CategoryDTO ToDTO(Category category)
        {
            return category == null? null : new CategoryDTO
            {
                CategoryIID = category.CategoryIID,
                ParentCategoryID = category.ParentCategoryID,
                CategoryCode = category.CategoryCode,
                CategoryName = category.CategoryName,
                ImageName = category.ImageName,
                ThumbnailImageName = category.ThumbnailImageName,
                IsActive = category.IsActive,
                CategoryList = new List<CategoryDTO>()
                //CategoryList = FromCategoryListEntity(category.CategoryList)
            };
        }

        public List<CategoryDTO> ToDTOList(List<Category> category)
        {
            var categoryDTOList = new List<CategoryDTO>();
            foreach (var item in category)
            {
                categoryDTOList.Add(ToDTO(item));
            }
            return categoryDTOList;
        }

        public Category ToEntity(CategoryDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
