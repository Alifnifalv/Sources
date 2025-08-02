using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services;

namespace Eduegate.Service.Client.Direct
{
    public class CategoryServiceClient : ICategoryService
    {
        CategoryService service = new CategoryService();

        public CategoryServiceClient(CallContext callContext = null, Action<string> logger = null)
        {
        }

        public List<CategoryDTO> GetAllCategoriesHomePage()
        {
            return service.GetAllCategoriesHomePage();
        }

        public List<CategoryDTO> GetAllCategoriesHomePageWithBrand()
        {
            return service.GetAllCategoriesHomePageWithBrand();
        }

        public List<CategoryDTO> GetTopLevelCategories()
        {
            return service.GetTopLevelCategories();
        }

        public List<CategoryDTO> GetSubCategoriesDTO(long categoryID)
        {
            return service.GetSubCategoriesDTO(categoryID);
        }
        public List<CategoryDTO> GetCategoryBlocks(long categoryID)
        {
            return service.GetCategoryBlocks(categoryID);
        }

        public CategoryDTO GetCategoryByCode(string categoryCode)
        {
            return service.GetCategoryByCode(categoryCode);
        }

        public List<CategoryDTO> SearchCategories(string searchText)
        {
            return service.SearchCategories(searchText);
        }

        public List<KeyValueDTO> GetCategoryTags()
        {
            return service.GetCategoryTags();
        }

        public CategoryDTO GetCategoryByID(long categoryID)
        {
            return service.GetCategoryByID(categoryID);
        }

        public List<CategorySettingDTO> GetCategorySettings(long categoryID)
        {
            return service.GetCategorySettings(categoryID);
        }
    }
}
