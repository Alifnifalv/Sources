using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.SearchData;
using Eduegate.Services.Contracts.Synchronizer;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "BlinkLegacyService" in both code and config file together.
    [ServiceContract]
    public interface IBlinkLegacyService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetTotalKeywords")]
        long GetTotalKeywords();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetTotalProducts?companyID={companyID}")]
        long GetTotalProducts(int companyID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetProductCatalogs?pageNumber={pageNumber}&pageSize={pageSize}")]
        List<SearchCatalogDTO> GetProductCatalogs(int pageNumber, int pageSize);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetProductCatalog?productID={productID}")]
        SearchCatalogDTO GetProductCatalog(long productID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetProducts?pageNumber={pageNumber}&pageSize={pageSize}&country={country}&cultureID={cultureID}&companyID={companyID}&siteID={siteID}")]
        List<ProductDTO> GetProducts(int pageNumber, int pageSize, int country, int cultureID = 1, int companyID = 1,int siteID=1);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetKeywords?pageNumber={pageNumber}&pageSize={pageSize}&lng={lng}&country={country}")]
        List<KeywordsDTO> GetKeywords(int pageNumber, int pageSize,string lng,int country);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetProduct?productID={productID}&country={country}&cultureID={cultureID}&companyID={companyID}&siteID={siteID}")]
        ProductDTO GetProduct(long productID, int country, int cultureID = 1, int companyID = 1,int siteID=1);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetTotalCategories")]
        long GetTotalCategories();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetCategories?pageNumber={pageNumber}&pageSize={pageSize}")]
        List<ProductCategoryDTO> GetCategories(int pageNumber, int pageSize);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetCategory?categoryID={categoryID}")]
        ProductCategoryDTO GetCategory(long categoryID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetCategoryProductLevel?categoryID={categoryID}&productID={productID}&country={country}")]
        ProductCategoryDTO GetCategoryProductLevel(long categoryID, long productID,int country);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "UpdateEntityChangeTrackerLog")]
        bool UpdateEntityChangeTrackerLog();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "CategoryAllList?productID={productID}&siteID={siteID}")]
        string CategoryAllList(long productID,int siteID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetCategoryCulture?categoryID={categoryID}&cultureID={cultureID}")]
        ProductCategoryDTO GetCategoryCulture(long categoryID, int cultureID);
    }
}
