using System;
using System.Collections.Generic;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Inventory;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IProductDetail" in both code and config file together.
    public interface IProductDetail
    {
        ProductFeatureDTO GetProductDetail(long ProductId, long skuID, string propertyTypeValue, Enums.ProductStatuses productStatus);

        List<QuantityDTO> GetQuantityListByProductIID(decimal productIID);

        List<ImageDTO> GetImagesByProductIID(decimal productIID);

        Catalog.ProductsDTO GetProducts(ProductSearchInfoDTO searchInfo);

        ProductViewDTO GetProductSummaryInfo();

        List<ProductItemDTO> GetProductViews(ProductViewSearchInfoDTO searchInfo);

        List<ProductItemDTO> GetProductList();

        TransactionDTO SaveTransactions(TransactionDTO transaction);

        List<TransactionDetailDTO> GetTransactionByTransactionID(long transactionID);

        List<TransactionDetailDTO> GetTransactionByTransactionDate(DateTime transactionDate);

        Catalog.ProductPriceDTO CreatePriceInformationDetail(ProductPriceDTO productPriceDTO);

        List<ProductPriceSKUDTO> GetProductPriceSKUMaps();

        List<ProductPriceSKUDTO> UpdateSKUProductPrice(List<ProductPriceSKUDTO> productPriceSKUDTOList);
        
        long UpdateProduct(AddProductDTO productDetails);

        List<CultureDataInfoDTO> GetCultureList();

        List<Eduegate.Services.Contracts.Catalog.BrandDTO> GetBrandList();

        List<ProductFamilyDTO> GetProductFamilies();

        ProductFamilyDTO GetProductFamily(string familyID);

        ProductFamilyDTO SaveProductFamily(ProductFamilyDTO familyDTO);

        List<ProductStatusDTO> GetProductStatus();

        List<PropertyTypeDTO> GetProductPropertyTypes(decimal productFamilyIID);

        PropertyDTO GetProperty(string propertyID);

        PropertyDTO SaveProperty(PropertyDTO propertyDTO);
        
        List<PropertyDTO> GetPropertiesByPropertyTypeID(decimal propertyTypeIID);

        List<ProductCategoryDTO> GetCategoryList();

        AddProductDTO GetProduct(long productIID);

        List<PropertyDTO> GetProperitesByProductFamilyID(long productFamilyID);

        List<ProductDTO> GetSuggestedProduct(long productID);

        List<ProductDTO> GetProductListBySearchText(string searchtext, long excludeProductFamilyID = default(long));

        List<ProductDTO> GetProductListByCategoryID(long categoryID);

        bool SaveProductMaps(ProductToProductMapDTO productMaps);

        ProductToProductMapDTO GetProductMaps(long productID);

        TransactionDTO GetTransaction(long headIID);

        bool RemoveProductPriceListSKUMaps(long id);

        ProductPriceDTO GetProductPriceListDetail(long id);

        List<ProductPriceSKUDTO> GetProductPriceSKU(long id);

        List<SalesRelationshipTypeDTO> GetSalesRelationshipType(Eduegate.Services.Contracts.Enums.SalesRelationShipTypes salesRelationShipTypes);

        List<SalesRelationshipTypeDTO> GetSalesRelationshipTypeList(Eduegate.Services.Contracts.Enums.SalesRelationShipTypes salesRelationShipTypes, long productID);

        ProductDTO GetProductDetailByProductId(long productID);

        List<PropertyDTO> CreateProperties(List<PropertyDTO> dtoPropertyList);

        List<ProductPriceListTypeDTO> GetProductPriceListTypes();

        List<ProductPriceListLevelDTO> GetProductPriceListLevels();

        List<KeyValueDTO> SearchBrand(string searchText, int pageSize);

        List<KeyValueDTO> SearchProductFamilies(string searchText, int pageSize);

        PropertyTypeDTO SavePropertyType(PropertyTypeDTO propertyTypeDTO);
        
        PropertyTypeDTO GetPropertyType(byte propertyTypeID);

        List<ProductTypeDTO> GetProductTypes();

        List<KeyValueDTO> GetProdctSKUTags();

        string SaveProductSkuBarCode(long skuId, string barCode);

        string SaveProductSkuPartNo(long skuId, string partNo);

        ProductInventoryBranchDTO GetInventoryDetailsSKUID(long skuId, long customerID, bool type);

        bool SaveProductSKUTags(ProductSKUTagDTO productSKUTags);

        List<KeyValueDTO> GetProductSKUTagMaps(long skuID);

        SKUDTO GetProductSKUDetails(long skuID);

        SKUDTO SaveProductSKUDetails(SKUDTO sku);

        string SaveSKUProductMap(long productID, long skuID);

        bool SaveSKUManagers(AddProductDTO skuManagers);

        SKUDTO GetSKUEmployeeDetails(long skuID);

        string GetProductSkuCode(long skuID);

        List<ProductCategoryDTO> GetReportingCategoryList();         
    }
}