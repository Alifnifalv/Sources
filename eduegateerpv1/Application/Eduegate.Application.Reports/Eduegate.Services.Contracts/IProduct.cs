using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Inventory;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IProduct" in both code and config file together.
    [ServiceContract]
    public interface IProduct
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetProducts?searchText={searchText}")]
        List<ProductDTO> GetProducts(string searchText);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetProductGroups?searchText={searchText}")]
        List<ProductDTO> GetProductGroups(string searchText);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetProduct?productID={productID}")]
        ProductDTO GetProduct(long productID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetProductGroupDetails?productGroup={productGroup}")]
        ProductDTO GetProductGroupDetails(string productGroup);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate =
            "GetProductByCategory?CategoryId={CategoryId}&MenuId={MenuId}&pageNumber={pageNumber}&pageSize={pageSize}&sortBy={sortBy}&productStatus={productStatus}")]
        List<ProductDTO> GetProductByCategory(long CategoryId, long MenuId, int pageNumber, int pageSize, string sortBy, ProductStatuses productStatus);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetProductListWithSKU?searchText={searchText}&dataSize={dataSize}")]
        List<POSProductDTO> GetProductListWithSKU(string searchText, int dataSize);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetProductsBySearchCriteria?searchText={searchText}&pageNumber={pageNumber}&pageSize={pageSize}&sortBy={sortBy}")]
        List<ProductDTO> GetProductsBySearchCriteria(string searchText, int pageNumber, int pageSize, string sortBy);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProductCategory/{categoryID}/")]
        ProductCategoryDTO GetProductCategory(string categoryID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveProductCategory")]
        ProductCategoryDTO SaveProductCategory(ProductCategoryDTO customerDTO);

        [OperationContract]
        [WebInvoke(Method ="GET", ResponseFormat =WebMessageFormat.Json,  RequestFormat =WebMessageFormat.Json,
            BodyStyle =WebMessageBodyStyle.Bare, UriTemplate = "GetProductAndSKUByID?productSKUMapID={productSKUMapID}")]
        POSProductDTO GetProductAndSKUByID(long productSKUMapID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "ProductSKUSearch?searchText={searchText}&dataSize={dataSize}&documentTypeID={documentTypeID}")]
        List<POSProductDTO> ProductSKUSearch(string searchText, int dataSize, string documentTypeID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "ProductSKUByCategoryID?categoryID={categoryID}&dataSize={dataSize}")]
        List<POSProductDTO> ProductSKUByCategoryID(long categoryID, int dataSize);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetProductSKUInventoryDetail?skuIID={skuIID}&referenceType={referenceType}&branchID={branchID}")]
        POSProductDTO GetProductSKUInventoryDetail(long skuIID, DocumentReferenceTypes referenceType, long branchID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetProductSkuIDInventoryBranchWise?SkuID={SkuID}&BranchID={BranchID}")]
        decimal GetProductSkuIDInventoryBranchWise(long SkuID, long BranchID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetProductInventoryOnline?SkuID={SkuID}")]
        List<ProductInventoryBranchDTO> GetProductInventoryOnline(long SkuID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "ProductSearch?searchText={searchText}&dataSize={dataSize}")]
        List<ProductMapDTO> ProductSearch(string searchText, int dataSize);

        [OperationContract(IsOneWay=true)]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate =
            "SetProductSKUSiteMap?productSKUMapID={productSKUMapID}&siteID={siteID}&isActive={isActive}")]
        void SetProductSKUSiteMap(long productSKUMapID, int siteID, bool isActive);
    }
}
