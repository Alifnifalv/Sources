using System.Collections.Generic;
using Eduegate.Services.Contracts.SearchData;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "BlinkLegacyService" in both code and config file together.
    public interface IBlinkLegacyService
    {
        long GetTotalKeywords();

        long GetTotalProducts(int companyID);

        List<SearchCatalogDTO> GetProductCatalogs(int pageNumber, int pageSize);

        SearchCatalogDTO GetProductCatalog(long productID);

        List<ProductDTO> GetProducts(int pageNumber, int pageSize, int country, int cultureID = 1, int companyID = 1,int siteID=1);

        List<KeywordsDTO> GetKeywords(int pageNumber, int pageSize,string lng,int country);

        ProductDTO GetProduct(long productID, int country, int cultureID = 1, int companyID = 1,int siteID=1);

        long GetTotalCategories();

        List<ProductCategoryDTO> GetCategories(int pageNumber, int pageSize);

        ProductCategoryDTO GetCategory(long categoryID);

        ProductCategoryDTO GetCategoryProductLevel(long categoryID, long productID,int country);

        bool UpdateEntityChangeTrackerLog();

        string CategoryAllList(long productID,int siteID);

        ProductCategoryDTO GetCategoryCulture(long categoryID, int cultureID);
    }
}