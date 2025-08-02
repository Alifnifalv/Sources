using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Domain;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        public List<CategoryDTO> GetAllCategoriesHomePage()
        {
            return new CategoryBL(CallContext).GetAllCategoriesHomePage();
        }
        public List<CategoryDTO> GetAllCategoriesHomePageWithBrand()
        {
            return new CategoryBL(CallContext).GetAllCategoriesHomePageWithBrand();
        }

        public List<CategoryDTO> GetTopLevelCategories()
        {
            return new CategoryBL(CallContext).GetTopLevelCategories();
        }

        public List<CategoryDTO> GetSubCategoriesDTO(long categoryID)
        {
            return new CategoryBL(CallContext).GetSubCategoriesDTO(categoryID);
        }

        public List<CategoryDTO> GetCategoryBlocks(long categoryID)
        {
            return new CategoryBL(CallContext).GetCategoryBlocks(categoryID);
        }

        public CategoryDTO GetCategoryByCode(string categoryCode)
        {
            return new CategoryBL(CallContext).GetCategoryByCode(categoryCode);
        }

        public List<CategoryDTO> SearchCategories(string searchText)
        {
            return new CategoryBL(CallContext).SearchCategories(searchText);
        }

        public List<KeyValueDTO> GetCategoryTags()
        {
            return new CategoryBL(CallContext).GetCategoryTags();
        }

        public CategoryDTO GetCategoryByID(long categoryID)
        {
            return new CategoryBL(CallContext).GetCategoryByID(categoryID);
        }

        public List<CategorySettingDTO> GetCategorySettings(long categoryID)
        {
            return new CategoryBL(CallContext).GetCategorySettings(categoryID);
        }
    }
}
