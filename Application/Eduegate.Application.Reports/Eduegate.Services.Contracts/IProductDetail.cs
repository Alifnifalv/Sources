using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Inventory;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IProductDetail" in both code and config file together.
    [ServiceContract]
    public interface IProductDetail
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetProductDetail?ProductId={ProductId}&skuID={skuID}&propertyTypeValue={propertyTypeValue}&productStatus={productStatus}")]
        ProductFeatureDTO GetProductDetail(long ProductId, long skuID, string propertyTypeValue, Enums.ProductStatuses productStatus);

        [OperationContract]
        [WebInvoke(Method = "Get", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetQuantityByProduct?productIID={productIID}")]
        List<QuantityDTO> GetQuantityListByProductIID(decimal productIID);

        [OperationContract]
        [WebInvoke(Method = "Get", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetImagesByProduct?productIID={productIID}")]
        List<ImageDTO> GetImagesByProductIID(decimal productIID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetProducts")]
        Catalog.ProductsDTO GetProducts(ProductSearchInfoDTO searchInfo);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProductSummaryInfo")]
        ProductViewDTO GetProductSummaryInfo();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetProductViews")]
        List<ProductItemDTO> GetProductViews(ProductViewSearchInfoDTO searchInfo);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProductList")]
        List<ProductItemDTO> GetProductList();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveTransactions")]
        TransactionDTO SaveTransactions(TransactionDTO transaction);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetTransactionByTransactionID?transactionID={transactionID}")]
        List<TransactionDetailDTO> GetTransactionByTransactionID(long transactionID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetTransactionByTransactionDate?transactionDate={transactionDate}")]
        List<TransactionDetailDTO> GetTransactionByTransactionDate(DateTime transactionDate);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "CreatePriceInformationDetail")]
        Catalog.ProductPriceDTO CreatePriceInformationDetail(ProductPriceDTO productPriceDTO);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProductPriceSKUMaps")]
        List<ProductPriceSKUDTO> GetProductPriceSKUMaps();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "UpdateSKUProductPrice")]
        List<ProductPriceSKUDTO> UpdateSKUProductPrice(List<ProductPriceSKUDTO> productPriceSKUDTOList);
        
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "UpdateProduct")]
        long UpdateProduct(AddProductDTO productDetails);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCultureList")]
        List<CultureDataInfoDTO> GetCultureList();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetBrandList")]
        List<Eduegate.Services.Contracts.Catalog.BrandDTO> GetBrandList();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProductFamilies")]
        List<ProductFamilyDTO> GetProductFamilies();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProductFamily?familyID={familyID}")]
        ProductFamilyDTO GetProductFamily(string familyID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveProductFamily")]
        ProductFamilyDTO SaveProductFamily(ProductFamilyDTO familyDTO);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProductStatus")]
        List<ProductStatusDTO> GetProductStatus();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProductPropertyTypes?productFamilyIID={productFamilyIID}")]
        List<PropertyTypeDTO> GetProductPropertyTypes(decimal productFamilyIID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProperty?propertyID={propertyID}")]
        PropertyDTO GetProperty(string propertyID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveProperty")]
        PropertyDTO SaveProperty(PropertyDTO propertyDTO);
        
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetPropertiesByPropertyTypeID?propertyTypeIID={propertyTypeIID}")]
        List<PropertyDTO> GetPropertiesByPropertyTypeID(decimal propertyTypeIID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCategoryList")]
        List<ProductCategoryDTO> GetCategoryList();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProduct?productIID={productIID}")]
        AddProductDTO GetProduct(long productIID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProperitesByProductFamilyID?productFamilyID={productFamilyID}")]
        List<PropertyDTO> GetProperitesByProductFamilyID(long productFamilyID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetSuggestedProduct?productID={productID}")]
        List<ProductDTO> GetSuggestedProduct(long productID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProductListBySearchText?searchtext={searchtext}&excludeProductFamilyID={excludeProductFamilyID}")]
        List<ProductDTO> GetProductListBySearchText(string searchtext, long excludeProductFamilyID = default(long));

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProductListByCategoryID?categoryID={categoryID}&excludeProductFamilyID={excludeProductFamilyID}")]
        List<ProductDTO> GetProductListByCategoryID(long categoryID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveProductMaps")]
        bool SaveProductMaps(ProductToProductMapDTO productMaps);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProductMaps?productID={productID}")]
        ProductToProductMapDTO GetProductMaps(long productID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetTransaction?headIID={headIID}")]
        TransactionDTO GetTransaction(long headIID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "RemoveProductPriceListSKUMaps?id={id}")]
        bool RemoveProductPriceListSKUMaps(long id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProductPriceListDetail?id={id}")]
        ProductPriceDTO GetProductPriceListDetail(long id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProductPriceSKU?id={id}")]
        List<ProductPriceSKUDTO> GetProductPriceSKU(long id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetSalesRelationshipType?salesRelationShipTypes={salesRelationShipTypes}")]
        List<SalesRelationshipTypeDTO> GetSalesRelationshipType(Eduegate.Services.Contracts.Enums.SalesRelationShipTypes salesRelationShipTypes);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetSalesRelationshipTypeList?salesRelationShipTypes={salesRelationShipTypes}&productID={productID}")]
        List<SalesRelationshipTypeDTO> GetSalesRelationshipTypeList(Eduegate.Services.Contracts.Enums.SalesRelationShipTypes salesRelationShipTypes, long productID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProductDetailByProductId?productID={productID}")]
        ProductDTO GetProductDetailByProductId(long productID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "CreateProperties")]
        List<PropertyDTO> CreateProperties(List<PropertyDTO> dtoPropertyList);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, 
            UriTemplate = "GetProductPriceListTypes")]
        List<ProductPriceListTypeDTO> GetProductPriceListTypes();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, 
            UriTemplate = "GetProductPriceListLevels")]
        List<ProductPriceListLevelDTO> GetProductPriceListLevels();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SearchBrand?searchText={searchText}&pageSize={pageSize}")]
        List<KeyValueDTO> SearchBrand(string searchText, int pageSize);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SearchProductFamilies?searchText={searchText}&pageSize={pageSize}")]
        List<KeyValueDTO> SearchProductFamilies(string searchText, int pageSize);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SavePropertyType")]
        PropertyTypeDTO SavePropertyType(PropertyTypeDTO propertyTypeDTO);
        
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetPropertyType?propertyTypeID={propertyTypeID}")]
        PropertyTypeDTO GetPropertyType(byte propertyTypeID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProductTypes")]
        List<ProductTypeDTO> GetProductTypes();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProdctSKUTags")]
        List<KeyValueDTO> GetProdctSKUTags();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveProductSkuBarCode?skuId={skuId}&barCode={barCode}")]
        string SaveProductSkuBarCode(long skuId, string barCode);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveProductSkuPartNo?skuId={skuId}&partNo={partNo}")]
        string SaveProductSkuPartNo(long skuId, string partNo);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetInventoryDetailsSKUID?skuId={skuId}&customerID={customerID}&type={type}")]
        ProductInventoryBranchDTO GetInventoryDetailsSKUID(long skuId, long customerID, bool type);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveProductSKUTags")]
        bool SaveProductSKUTags(ProductSKUTagDTO productSKUTags);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProductSKUTagMaps?skuID={skuID}")]
        List<KeyValueDTO> GetProductSKUTagMaps(long skuID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProductSKUDetails?skuID={skuID}")]
        SKUDTO GetProductSKUDetails(long skuID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveProductSKUDetails")]
        SKUDTO SaveProductSKUDetails(SKUDTO sku);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveSKUProductMap?productID={productID}&skuID={skuID}")]
        string SaveSKUProductMap(long productID,long skuID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveSKUManagers")]
        bool SaveSKUManagers(AddProductDTO skuManagers);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetSKUEmployeeDetails?skuID={skuID}")]
        SKUDTO GetSKUEmployeeDetails(long skuID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProductSkuCode?skuID={skuID}")]
        string GetProductSkuCode(long skuID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetReportingCategoryList")]
        List<ProductCategoryDTO> GetReportingCategoryList();         
    }
}
