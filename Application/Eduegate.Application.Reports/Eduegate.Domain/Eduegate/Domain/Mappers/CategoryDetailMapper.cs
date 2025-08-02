using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Domain;

namespace Eduegate.Domain.Mappers
{
    public class CategoryDetailMapper : IDTOEntityMapper<CategoryDetailDTO, Category>
    {
        private CallContext _context;

        public static CategoryDetailMapper Mapper(CallContext context)
        {
            var mapper = new CategoryDetailMapper();
            mapper._context = context;
            return mapper;
        }

        public List<CategoryImageMapDTO> ResolveDTO(List<CategoryImageMap> imageMaps)
        {
            return imageMaps == null ? null : imageMaps.Select(x => new CategoryImageMapDTO()
            {
                CategoryImageMapIID = x.CategoryImageMapIID,
                ImageFile = x.ImageFile,
                ImageTypeID = x.ImageTypeID,
                CategoryID = x.CategoryID,
                ImageTitle = x.ImageTitle,
            }).ToList();
        }

        public CategoryDetailDTO ToDTO(Category category)
        {
            var imageHostUrl = new SettingBL(null).GetSettingValue<string>("ImageHostUrl");
            var _categoryURL = string.Format("{0}/{1}/", imageHostUrl, Eduegate.Framework.Enums.EduegateImageTypes.Category.ToString());

            string thumbImage = null;
            var thumbnail = category
                    .CategoryImageMaps.Where(a => a.ImageTypeID == (byte)ImageTypes.Thumbnail)
                    .FirstOrDefault();
            if (thumbnail.IsNotNull())
            {
                thumbImage = string.Format("{0}/{1}/{2}", _categoryURL, "Thumbnail/", thumbnail.ImageFile);
            }
            else
            {
                thumbImage = "/images/noimage.jpg";
            }

            string imageName = null;

            var listingImage = category
                .CategoryImageMaps.Where(a => a.ImageTypeID == (byte)ImageTypes.Listing)
                .FirstOrDefault();
            var largeImage = category
               .CategoryImageMaps.Where(a => a.ImageTypeID == (byte)ImageTypes.CategoryBanner_Large)
               .FirstOrDefault();

            if (listingImage.IsNotNull())
            {
                imageName = string.Format("{0}/{1}/{2}", _categoryURL, "Listing/", listingImage.ImageFile);
            }
            else if (largeImage.IsNotNull())
            {
                imageName = string.Format("{0}/{1}/{2}", _categoryURL, "CategoryLarge/", largeImage.ImageFile);
            }
            else
            {
                imageName = "/images/noimage.jpg";
            }

            return category == null ? null : new CategoryDetailDTO
            {
                CategoryIID = category.CategoryIID,
                ParentCategoryID = category.ParentCategoryID,
                CategoryCode = category.CategoryCode,
                CategoryName = string.IsNullOrEmpty(category.CategoryName) ? category.CategoryCode : category.CategoryName,
                ImageName = imageName,
                ThumbnailImageName = thumbImage,
                IsActive = category.IsActive,
                //CategoryList = new List<CategoryDetailDTO>(),
                //CategoryList = FromCategoryListEntity(category.CategoryList),
                //CategoryCultureDatas = category.CategoryCultureDatas == null || category.CategoryCultureDatas.Count == 0 ? null : category.CategoryCultureDatas.Select(x => new CategoryCultureDataDTO()
                //{ CategoryName = x.CategoryName, CultureID = x.CultureID, CultureCode = x.Culture != null ? x.Culture.CultureCode : null }).ToList()
            };
        }

        public ProductCategoryDTO ToCategoryDTO(Category category)
        {
            return category == null ? null : new ProductCategoryDTO
            {
                CategoryID = category.CategoryIID,
                ParentCategoryID = category.ParentCategoryID,
                CategoryCode = category.CategoryCode,
                CategoryName = string.IsNullOrEmpty(category.CategoryName) ? category.CategoryCode : category.CategoryName,
                ImageName = category.ImageName,
                ThumbnailImageName = category.ThumbnailImageName,
                Active = category.IsActive.Value,
                //CategoryList = FromCategoryListEntity(category.CategoryList),
                CategoryCultureDatas = category.CategoryCultureDatas == null ? null : category.CategoryCultureDatas.Select(x => new CategoryCultureDataDTO()
                { CategoryName = x.CategoryName, CultureID = x.CultureID, CultureCode = x.Culture != null ? x.Culture.CultureCode : null }).ToList()
            };
        }

        public List<CategoryDetailDTO> ToDTOList(List<Category> category, bool resolveImages = false)
        {
            var categoryDTOList = new List<CategoryDetailDTO>();

            foreach (var item in category)
            {
                var dto = ToDTO(item);

                if (resolveImages)
                {
                    dto.CategoryImageMapList = ResolveDTO(item.CategoryImageMaps.ToList());
                }

                categoryDTOList.Add(dto);
            }

            return categoryDTOList;
        }

        public Category ToEntity(CategoryDetailDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
