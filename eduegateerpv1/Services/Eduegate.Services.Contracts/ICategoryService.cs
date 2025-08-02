using System.Collections.Generic;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICategoryService" in both code and config file together.
    public interface ICategoryService 
    {
        List<CategoryDTO> GetAllCategoriesHomePage();

        List<CategoryDTO> GetAllCategoriesHomePageWithBrand();

        List<CategoryDTO> GetTopLevelCategories();

        List<CategoryDTO> GetSubCategoriesDTO(long categoryID);

        List<CategoryDTO> GetCategoryBlocks(long categoryID);

        CategoryDTO GetCategoryByCode(string categoryCode);

        List<CategoryDTO> SearchCategories(string searchText);

        List<KeyValueDTO> GetCategoryTags();

        CategoryDTO GetCategoryByID(long categoryID);

        List<CategorySettingDTO> GetCategorySettings(long categoryID);
    }
}