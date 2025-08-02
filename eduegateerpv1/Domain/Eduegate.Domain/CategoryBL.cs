using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Domain.Mappers;

namespace Eduegate.Domain
{
    public class CategoryBL
    {
        private CallContext _callContext;

        public CategoryBL(CallContext context)
        {
            _callContext = context;
        }
        public List<CategoryDTO> GetAllCategoriesHomePage()
        {
            var repository = new CategoryRepository();
            var categoryDTOList = new List<CategoryDTO>();
            var fullCategoryList = repository.GetCategoryList(_callContext.LanguageCode, "");
            var categories = fullCategoryList.Where(a => a.ParentCategoryID == null).ToList<Category>();  //repository.GetCategoryByParentID(null);
            var mapper = Mappers.CategoryMapper.Mapper(_callContext);

            foreach (var category in categories)
            {
                var categoryDTO = mapper.ToDTO(category);
                var subCategories = fullCategoryList.Where(a => a.ParentCategoryID == category.CategoryIID).ToList<Category>(); //repository.GetCategoryByParentID(category.CategoryIID);
                if (subCategories.Count > 0)
                {
                    foreach (var subCategory in subCategories)
                    {
                        categoryDTO.CategoryList.Add(mapper.ToDTO(subCategory));
                    }
                }
                var categoryImageMapList = repository.GetCategoryImage(Services.Contracts.Enums.ImageTypes.HomePageBanner, category.CategoryIID);
                categoryDTO.CategoryImageMapList = new List<CategoryImageMapDTO>();
                if (categoryImageMapList.Count > 0)
                {
                    foreach (var categoryImageMap in categoryImageMapList)
                    {
                        categoryDTO.CategoryImageMapList.Add(FromCategoryImageMapEntity(categoryImageMap));
                    }
                }

                categoryDTOList.Add(categoryDTO);
            }
            return categoryDTOList;
        }

        public List<CategoryImageMap> GetCategoriesBannerbyCategoryID(Services.Contracts.Enums.ImageTypes imageType, long CategoryID)
        {
            return new CategoryRepository().GetCategoryImage(imageType, CategoryID, _callContext.IsNotNull() && _callContext.CompanyID.HasValue ? _callContext.CompanyID.Value : 0 );
        }

        public List<Brand> GetCategoryBrandsbyTag(string tagName)
        {
            return new BrandRepository().GetCategoryBrandsbyTag(tagName);
        }

        public List<CategoryDTO> GetAllCategoriesHomePageWithBrand()
        {
            var repository = new CategoryRepository();
            var categoryDTOList = new List<CategoryDTO>();
            var fullCategoryList = repository.GetCategoryList(_callContext.LanguageCode, "");
            var categories = fullCategoryList.Where(a => a.ParentCategoryID == null).ToList<Category>();  //repository.GetCategoryByParentID(null);
            var mapper = Mappers.CategoryMapper.Mapper(_callContext);
            foreach (var category in categories)
            {
                var categoryDTO = mapper.ToDTO(category);
                var subCategories = fullCategoryList.Where(a => a.ParentCategoryID == category.CategoryIID).ToList<Category>(); //repository.GetCategoryByParentID(category.CategoryIID);
                if (subCategories.Count > 0)
                {
                    foreach (var subCategory in subCategories)
                    {
                        categoryDTO.CategoryList.Add(mapper.ToDTO(subCategory));
                    }
                }
                var categoryImageMapList = repository.GetCategoryImage(Services.Contracts.Enums.ImageTypes.HomePageBanner, category.CategoryIID);
                categoryDTO.CategoryImageMapList = new List<CategoryImageMapDTO>();
                if (categoryImageMapList.Count > 0)
                {
                    foreach (var categoryImageMap in categoryImageMapList)
                    {
                        categoryDTO.CategoryImageMapList.Add(FromCategoryImageMapEntity(categoryImageMap));
                    }
                }
                categoryDTO.BrandList = new List<BrandDTO>();
                var brandList = new BrandRepository().GetAllBrands().Where(a => a.StatusID == 1).Take(10).ToList<Brand>();
                if (brandList.Count > 0)
                {
                    foreach (var brand in brandList)
                    {
                        categoryDTO.BrandList.Add(FromBrandEntity(brand));
                    }
                }
                categoryDTOList.Add(categoryDTO);
            }
            return categoryDTOList;
        }
        public BrandDTO FromBrandEntity(Brand brand)
        {
            return new BrandDTO
            {
                BrandIID = brand.BrandIID,
                BrandLogo = brand.LogoFile
            };
        }
        public CategoryImageMapDTO FromCategoryImageMapEntity(CategoryImageMap categoryImageMap)
        {
            return new CategoryImageMapDTO
            {
                CategoryImageMapIID = categoryImageMap.CategoryImageMapIID,
                CategoryID = categoryImageMap.CategoryID,
                ImageTypeID = categoryImageMap.ImageTypeID,
                ImageFile = categoryImageMap.ImageFile,
                ImageTitle = categoryImageMap.ImageTitle,
                CompanyID = _callContext.CompanyID,
                //SiteID = _callContext.SiteID,
            };
        }
      
        public List<CategoryDTO> GetTopLevelCategories()
        {
            var repository = new CategoryRepository();
            var categoryDTOList = new List<CategoryDTO>();
            var fullCategoryList = repository.GetCategoryList(_callContext.LanguageCode, "");
            var categories = fullCategoryList.Where(a => a.ParentCategoryID == null && a.IsActive == true).ToList<Category>();
            foreach (var category in categories)
            {
                categoryDTOList.Add(Mappers.CategoryMapper.Mapper(_callContext).ToDTO(category));
            }
            return categoryDTOList;
        }

        public List<CategoryDTO> GetSubCategoriesDTO(long CategoryID)
        {
            var repository = new CategoryRepository();
            var categoryDTOList = new List<CategoryDTO>();
            var fullCategoryList = repository.GetCategoryList(_callContext.LanguageCode, "");
            var subCategories = fullCategoryList.Where(a => a.ParentCategoryID == CategoryID && a.IsActive == true).ToList<Category>(); //repository.GetCategoryByParentID(category.CategoryIID);
            foreach (var category in subCategories)
            {
                categoryDTOList.Add(Mappers.CategoryMapper.Mapper(_callContext).ToDTO(category));
            }
            return categoryDTOList;
        }

        public List<ProductTagDTO> GetCategoryTags(long categoryID)
        {
            var repository = new CategoryRepository();
            var categoryTagList = new List<ProductTagDTO>();
            categoryTagList = repository.GetCategoryTags(categoryID);
            return categoryTagList;
        }

        public List<CategoryDTO> GetCategoryBlocks(long CategoryID)
        {
            var repository = new CategoryRepository();
            var categoryDTOList = new List<CategoryDTO>();
            var fullCategoryList = repository.GetCategoryList(_callContext.LanguageCode, string.Empty);
            var subCategories = fullCategoryList.Where(a => a.ParentCategoryID == CategoryID && a.IsActive == true).ToList<Category>(); //repository.GetCategoryByParentID(category.CategoryIID);
            foreach (var category in subCategories)
            {
                categoryDTOList.Add(Mappers.CategoryMapper.Mapper(_callContext).ToDTO(category));
            }
            return categoryDTOList; 
        }

        public CategoryDTO GetCategoryBlockDetails(long blockID)
        {
            var repository = new CategoryRepository();
            var fullCategoryList = repository.GetCategoryList(_callContext.LanguageCode, string.Empty);
            var category = fullCategoryList.Where(a => a.CategoryIID == blockID).FirstOrDefault();
            return Mappers.CategoryMapper.Mapper(_callContext).ToDTO(category);
        }

        public CategoryDTO GetCategoryByCode(string categoryCode)
        {
            var repository = new CategoryRepository();
            var fullCategoryList = repository.GetCategoryList(_callContext.LanguageCode, string.Empty);
            var category = fullCategoryList.Where(a => a.CategoryCode == categoryCode).FirstOrDefault();
            return Mappers.CategoryMapper.Mapper(_callContext).ToDTO(category);
        }

        public List<CategoryDTO> SearchCategories(string searchText = null)
        {
            var catagoryList = new List<CategoryDTO>();
            var categories = new CategoryRepository().GetCategoryList(_callContext.IsNull() ? null : _callContext.LanguageCode, searchText);

            if (categories.IsNotNull() && categories.Count > 0)
            {
                foreach (var category in categories)
                {
                    catagoryList.Add(Mappers.CategoryMapper.Mapper(_callContext).ToDTO(category));
                }
            }

            return catagoryList;
        }


        public List<CategoryDTO> GetCategoryByParentIDWithImage(long? parentID)
        {
            var categoryList = new List<CategoryDTO>();
            var categories = new CategoryRepository().GetCategoryByParentIDWithImage(parentID, _callContext.LanguageCode);
            if (categories.IsNotNull() && categories.Count > 0)
            {
                foreach (var category in categories)
                {
                    var categoryDTO = Mappers.CategoryMapper.Mapper(_callContext).ToDTO(category);
                    categoryDTO.CategoryImageMapList = new List<CategoryImageMapDTO>();
                    foreach (var image in category.CategoryImageMaps)
                    {
                        try
                        {
                            categoryDTO.CategoryImageMapList.Add(FromCategoryImageMapEntity(image));
                        }
                        catch (Exception) { }
                    }

                    categoryList.Add(categoryDTO);
                }
            }

            return categoryList;
        }

        public List<KeyValueDTO> GetCategoryTags()
        {
            return Mappers.Catalog.CategoryTagMapper.Mapper(_callContext).ToDTO(new CategoryRepository().GetCategoryTags());
        }

        public CategoryDTO GetCategoryByID(long categoryID)
        {
            var category = new CategoryRepository().GetCategoryByID(categoryID, _callContext.LanguageCode);
            return Mappers.CategoryMapper.Mapper(_callContext).ToDTO(category);
        }

        public static List<CategoryDTO> GetSiteMapCategoryListByCompanyID(int companyID)
        {
            var repository = new CategoryRepository();
            var categoryDTOList = new List<CategoryDTO>();
            var fullCategoryList = repository.GetSiteMapCategoryListByCompanyID(companyID);
            categoryDTOList = Mappers.CategoryMapper.Mapper(null).ToDTOList(fullCategoryList);
            return categoryDTOList;
        }

        public List<CategorySettingDTO> GetCategorySettings(long categoryID)
        {
            return new CategorySettingMapper().ToDTOList(new CategoryRepository().GetCategorySettings(categoryID));
        }

        public List<CategoryDetailDTO> GetCategoryDetailByParentIDWithImage(long? parentID)
        {
            var categoryList = Framework.CacheManager.MemCacheManager<List<CategoryDetailDTO>>
                .Get(parentID.HasValue ? "CATEGORIESBYPARENTIDWITHIMAGE_" + parentID.Value.ToString() + _callContext.LanguageCode
                        : "CATEGORIESBYPARENTIDWITHIMAGE" + _callContext.LanguageCode);

            if (categoryList == null)
            {
                categoryList = new List<CategoryDetailDTO>();
                var categories = new CategoryRepository()
                    .GetCategoryByParentIDWithImage(parentID, _callContext.LanguageCode);
                if (categories.IsNotNull() && categories.Count > 0)
                {
                    foreach (var category in categories.OrderBy(x => x.SortOrder))
                    {
                        var categoryDTO = CategoryDetailMapper.Mapper(_callContext).ToDTO(category);
                        categoryDTO.CategoryImageMapList = new List<CategoryImageMapDTO>();
                        foreach (var image in category.CategoryImageMaps)
                        {
                            categoryDTO.CategoryImageMapList.Add(FromCategoryImageMapEntity(image));
                        }

                        categoryList.Add(categoryDTO);
                    }

                    Framework.CacheManager.MemCacheManager<List<CategoryDetailDTO>>.Add(categoryList,
                       parentID.HasValue ? "CATEGORIESBYPARENTIDWITHIMAGE_" + parentID.Value.ToString() + _callContext.LanguageCode
                           : "CATEGORIESBYPARENTIDWITHIMAGE" + _callContext.LanguageCode);
                }
            }

            return categoryList;
        }
    }
}
