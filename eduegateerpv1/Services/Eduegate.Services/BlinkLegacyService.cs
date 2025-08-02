using Eduegate.Framework.Services;

namespace Eduegate.Services
{
    //TODO: Not used
    //public class BlinkLegacyService : BaseService, IBlinkLegacyService
    //{
    //    public List<SearchCatalogDTO> GetProductCatalogs(int pageNumber, int pageSize)
    //    {
    //        try
    //        {
    //            var products = (new BlinkLegacyBL(CallContext)).GetProductCatalogs(pageNumber, pageSize);
    //            Eduegate.Logger.LogHelper<SearchCatalogDTO>.Info("Service Result : " + products.ToString());
    //            return products;
    //        }
    //        catch (Exception exception)
    //        {
    //            Eduegate.Logger.LogHelper<SearchCatalogDTO>.Fatal(exception.Message, exception);
    //            throw new FaultException("Internal server, please check with your administrator");
    //        }
    //    }

    //    public SearchCatalogDTO GetProductCatalog(long productID)
    //    {
    //        try
    //        {
    //            var products = (new BlinkLegacyBL(CallContext)).GetProductCatalog(productID);
    //            Eduegate.Logger.LogHelper<SearchCatalogDTO>.Info("Service Result : " + products.ToString());
    //            return products;
    //        }
    //        catch (Exception exception)
    //        {
    //            Eduegate.Logger.LogHelper<SearchCatalogDTO>.Fatal(exception.Message, exception);
    //            throw new FaultException("Internal server, please check with your administrator");
    //        }
    //    }

    //    public List<Contracts.ProductDTO> GetProducts(int pageNumber, int pageSize, int country, int cultureID = 1, int companyID = 1, int siteID = 1)
    //    {
    //        try
    //        {
    //            var products = (new BlinkLegacyBL(CallContext)).GetProducts(pageNumber, pageSize, country, cultureID, companyID, siteID);
    //            Eduegate.Logger.LogHelper<ProductDTO>.Info("Service Result : " + products.ToString());
    //            return products;
    //        }
    //        catch (Exception exception)
    //        {
    //            Eduegate.Logger.LogHelper<ProductDTO>.Fatal(exception.Message, exception);
    //            throw new FaultException("Internal server, please check with your administrator");
    //        }
    //    }

    //    public Contracts.ProductDTO GetProduct(long productID, int country, int cultureID = 1, int companyID = 1,int siteID=1)
    //    {
    //        try
    //        {
    //            var products = (new BlinkLegacyBL(CallContext)).GetProducts(productID, country, cultureID, companyID, siteID);
    //            Eduegate.Logger.LogHelper<ProductDTO>.Info("Service Result : " + products.ToString());
    //            return products;
    //        }
    //        catch (Exception exception)
    //        {
    //            Eduegate.Logger.LogHelper<ProductDTO>.Fatal(exception.Message, exception);
    //            throw new FaultException("Internal server, please check with your administrator");
    //        }
    //    }

    //    public List<Contracts.ProductCategoryDTO> GetCategories(int pageNumber, int pageSize)
    //    {
    //        try
    //        {
    //            var categories = (new BlinkLegacyBL(CallContext)).GetCategory(pageNumber, pageSize);
    //            Eduegate.Logger.LogHelper<ProductCategoryDTO>.Info("Service Result : " + categories.ToString());
    //            return categories;
    //        }
    //        catch (Exception exception)
    //        {
    //            Eduegate.Logger.LogHelper<ProductCategoryDTO>.Fatal(exception.Message, exception);
    //            throw new FaultException("Internal server, please check with your administrator");
    //        }
    //    }

    //    public Contracts.ProductCategoryDTO GetCategory(long categoryID)
    //    {
    //        try
    //        {
    //            var categories = (new BlinkLegacyBL(CallContext)).GetCategory(categoryID);
    //            Eduegate.Logger.LogHelper<ProductCategoryDTO>.Info("Service Result : " + (categories == null? "null" : categories.ToString()));
    //            return categories;
    //        }
    //        catch (Exception exception)
    //        {
    //            Eduegate.Logger.LogHelper<ProductCategoryDTO>.Fatal(exception.Message, exception);
    //            throw new FaultException("Internal server, please check with your administrator");
    //        }
    //    }

    //    public Contracts.ProductCategoryDTO GetCategoryCulture(long categoryID,int cultureID)
    //    {
    //        try
    //        {
    //            var categories = (new BlinkLegacyBL(CallContext)).GetCategoryCulture(categoryID, cultureID);
    //            Eduegate.Logger.LogHelper<ProductCategoryDTO>.Info("Service Result : " + (categories == null ? "null" : categories.ToString()));
    //            return categories;
    //        }
    //        catch (Exception exception)
    //        {
    //            Eduegate.Logger.LogHelper<ProductCategoryDTO>.Fatal(exception.Message, exception);
    //            throw new FaultException("Internal server, please check with your administrator");
    //        }
    //    }

    //    public long GetTotalProducts(int companyID)
    //    {
    //        try
    //        {
    //            var count = (new BlinkLegacyBL(CallContext)).GetTotalProducts(companyID);
    //            Eduegate.Logger.LogHelper<string>.Info("Service Result : " + count.ToString());
    //            return count;
    //        }
    //        catch (Exception exception)
    //        {
    //            Eduegate.Logger.LogHelper<string>.Fatal(exception.Message, exception);
    //            throw new FaultException("Internal server, please check with your administrator");
    //        }
    //    }

    //    public long GetTotalCategories()
    //    {
    //        try
    //        {
    //            var count = (new BlinkLegacyBL(CallContext)).GetTotalCategories();
    //            Eduegate.Logger.LogHelper<string>.Info("Service Result : " + count.ToString());
    //            return count;
    //        }
    //        catch (Exception exception)
    //        {
    //            Eduegate.Logger.LogHelper<string>.Fatal(exception.Message, exception);
    //            throw new FaultException("Internal server, please check with your administrator");
    //        }
    //    }

    //    public List<KeywordsDTO> GetKeywords(int pageNumber, int pageSize, string lng,int country)
    //    {
    //        try
    //        {
    //            var keywords = (new BlinkLegacyBL(CallContext)).GetKeywords(pageNumber, pageSize, lng, country);
    //            Eduegate.Logger.LogHelper<KeywordsDTO>.Info("Service Result : " + keywords);
    //            return keywords;
    //        }
    //        catch (Exception exception)
    //        {
    //            Eduegate.Logger.LogHelper<ProductCategoryDTO>.Fatal(exception.Message, exception);
    //            throw new FaultException("Internal server, please check with your administrator");
    //        }
    //    }

    //    public long GetTotalKeywords()
    //    {
    //        try
    //        {
    //            var count = (new BlinkLegacyBL(CallContext)).GetTotalKeywords();
    //            Eduegate.Logger.LogHelper<long>.Info("Service Result : " + count.ToString());
    //            return count;
    //        }
    //        catch (Exception exception)
    //        {
    //            Eduegate.Logger.LogHelper<long>.Fatal(exception.Message, exception);
    //            throw new FaultException("Internal server, please check with your administrator");
    //        }
    //    }

    //    public Contracts.ProductCategoryDTO GetCategoryProductLevel(long categoryID, long productID,int country)
    //    {
    //        try
    //        {
    //            var categories = (new BlinkLegacyBL(CallContext)).GetCategoryProductLevel(categoryID, productID, country);
    //            Eduegate.Logger.LogHelper<ProductCategoryDTO>.Info("Service Result : " + (categories == null ? "null" : categories.ToString()));
    //            return categories;
    //        }
    //        catch (Exception exception)
    //        {
    //            Eduegate.Logger.LogHelper<ProductCategoryDTO>.Fatal(exception.Message, exception);
    //            throw new FaultException("Internal server, please check with your administrator");
    //        }
    //    }

    //    public bool UpdateEntityChangeTrackerLog()
    //    {
    //        try
    //        {
    //            var dto = (new BlinkLegacyBL(CallContext)).UpdateEntityChangeTrackerLog();
    //            Eduegate.Logger.LogHelper<bool>.Info("Service Result : " + dto.ToString());
    //            return dto;
    //        }
    //        catch (Exception exception)
    //        {
    //            Eduegate.Logger.LogHelper<bool>.Fatal(exception.Message, exception);
    //            throw new FaultException("Internal server, please check with your administrator");
    //        }
    //    }

    //    public string CategoryAllList(long productID,int siteID)
    //    {
    //        try
    //        {
    //            var dto = (new BlinkLegacyBL(CallContext)).CategoryAllList(productID, siteID);
    //            Eduegate.Logger.LogHelper<bool>.Info("Service Result : " + dto.ToString());
    //            return dto;
    //        }
    //        catch (Exception exception)
    //        {
    //            Eduegate.Logger.LogHelper<bool>.Fatal(exception.Message, exception);
    //            throw new FaultException("Internal server, please check with your administrator");
    //        }
    //    }
    //}

}