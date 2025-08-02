using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services;

namespace Eduegate.Service.Client.Direct
{
    public class ProductDetailServiceClient : IProductDetail
    {
        ProductDetail service = new ProductDetail();

        public ProductDetailServiceClient(CallContext callContext = null, Action<string> logger = null)
        {
            service.CallContext = callContext;
        }

        public Services.Contracts.Eduegates.ProductFeatureDTO GetProductDetail(long ProductId, long skuID, string propertyTypeValue, Services.Contracts.Enums.ProductStatuses productStatus)
        {
            return service.GetProductDetail(ProductId, skuID, propertyTypeValue, productStatus);
        }

        public List<Services.Contracts.Eduegates.QuantityDTO> GetQuantityListByProductIID(decimal productIID)
        {
            return service.GetQuantityListByProductIID(productIID);
        }

        public List<Services.Contracts.Eduegates.ImageDTO> GetImagesByProductIID(decimal productIID)
        {
            return service.GetImagesByProductIID(productIID);
        }

        public Services.Contracts.Catalog.ProductsDTO GetProducts(Services.Contracts.Eduegates.ProductSearchInfoDTO searchInfo)
        {
            return service.GetProducts(searchInfo);
        }

        public Services.Contracts.Catalog.ProductViewDTO GetProductSummaryInfo()
        {
            return service.GetProductSummaryInfo();
        }

        public List<Services.Contracts.Catalog.ProductItemDTO> GetProductViews(Services.Contracts.Catalog.ProductViewSearchInfoDTO searchInfo)
        {
            return service.GetProductViews(searchInfo);
        }

        public List<Services.Contracts.Catalog.ProductItemDTO> GetProductList()
        {
            return service.GetProductList();
        }

        public Services.Contracts.Catalog.TransactionDTO SaveTransactions(Services.Contracts.Catalog.TransactionDTO transaction)
        {
            return service.SaveTransactions(transaction);
        }

        public List<Services.Contracts.Catalog.TransactionDetailDTO> GetTransactionByTransactionID(long transactionID)
        {
            return service.GetTransactionByTransactionID(transactionID);
        }

        public List<Services.Contracts.Catalog.TransactionDetailDTO> GetTransactionByTransactionDate(DateTime transactionDate)
        {
            return service.GetTransactionByTransactionDate(transactionDate);
        }

        public ProductPriceDTO CreatePriceInformationDetail(Services.Contracts.Catalog.ProductPriceDTO productPriceDTO)
        {
            return service.CreatePriceInformationDetail(productPriceDTO);
        }

        public List<Services.Contracts.Catalog.ProductPriceSKUDTO> GetProductPriceSKUMaps()
        {
            return service.GetProductPriceSKUMaps();
        }

        public List<Services.Contracts.Catalog.ProductPriceSKUDTO> UpdateSKUProductPrice(List<Services.Contracts.Catalog.ProductPriceSKUDTO> productPriceSKUDTOList)
        {
            return service.UpdateSKUProductPrice(productPriceSKUDTOList);
        }

        public long UpdateProduct(Services.Contracts.Catalog.AddProductDTO UpdateProduct)
        {
            return service.UpdateProduct(UpdateProduct);
        }

        public List<CultureDataInfoDTO> GetCultureList()
        {
            return service.GetCultureList();
        }

        public List<Services.Contracts.Catalog.BrandDTO> GetBrandList()
        {
            return service.GetBrandList();
        }

        public List<Services.Contracts.Catalog.ProductFamilyDTO> GetProductFamilies()
        {
            return service.GetProductFamilies();
        }

        public Services.Contracts.Catalog.ProductFamilyDTO GetProductFamily(string familyID)
        {
            return service.GetProductFamily(familyID);
        }

        public Services.Contracts.Catalog.ProductFamilyDTO SaveProductFamily(ProductFamilyDTO familyDTO)
        {
            return service.SaveProductFamily(familyDTO);
        }

        public List<Services.Contracts.Catalog.ProductStatusDTO> GetProductStatus()
        {
            return service.GetProductStatus();
        }

        public List<Services.Contracts.Catalog.PropertyTypeDTO> GetProductPropertyTypes(decimal productFamilyIID)
        {
            return service.GetProductPropertyTypes(productFamilyIID);
        }

        public List<PropertyDTO> GetPropertiesByPropertyTypeID(decimal propertyTypeIID)
        {
            return service.GetPropertiesByPropertyTypeID(propertyTypeIID);
        }

        public List<ProductCategoryDTO> GetCategoryList()
        {
            return service.GetCategoryList();
        }

        public Services.Contracts.Catalog.AddProductDTO GetProduct(long productIID)
        {
            return service.GetProduct(productIID);
        }

        public List<PropertyDTO> GetProperitesByProductFamilyID(long productFamilyID)
        {
            return service.GetProperitesByProductFamilyID(productFamilyID);
        }

        public List<ProductDTO> GetSuggestedProduct(long productID)
        {
            return service.GetSuggestedProduct(productID);
        }

        public List<ProductDTO> GetProductListBySearchText(string searchtext, long excludeProductFamilyID = default(long))
        {
            return service.GetProductListBySearchText(searchtext, excludeProductFamilyID);
        }

        public bool SaveProductMaps(Services.Contracts.Catalog.ProductToProductMapDTO productMaps)
        {
            return service.SaveProductMaps(productMaps);
        }

        public Services.Contracts.Catalog.ProductToProductMapDTO GetProductMaps(long productID)
        {
            return service.GetProductMaps(productID);
        }

        public Services.Contracts.Catalog.TransactionDTO GetTransaction(long headIID)
        {
            return service.GetTransaction(headIID);
        }

        public bool RemoveProductPriceListSKUMaps(long id)
        {
            return service.RemoveProductPriceListSKUMaps(id);
        }

        public Services.Contracts.Catalog.ProductPriceDTO GetProductPriceListDetail(long id)
        {
            return service.GetProductPriceListDetail(id);
        }

        public List<Services.Contracts.Catalog.ProductPriceSKUDTO> GetProductPriceSKU(long id)
        {
            return service.GetProductPriceSKU(id);
        }

        public PropertyDTO GetProperty(string propertyID)
        {
            return service.GetProperty(propertyID);
        }

        public PropertyDTO SaveProperty(PropertyDTO propertyDTO)
        {
            return service.SaveProperty(propertyDTO);
        }

        public PropertyTypeDTO SavePropertyType(PropertyTypeDTO propertyTypeDTO)
        {
            return service.SavePropertyType(propertyTypeDTO);
        }

        public List<SalesRelationshipTypeDTO> GetSalesRelationshipType(Eduegate.Services.Contracts.Enums.SalesRelationShipTypes salesRelationShipTypes)
        {
            return service.GetSalesRelationshipType(salesRelationShipTypes);
        }

        public List<SalesRelationshipTypeDTO> GetSalesRelationshipTypeList(Eduegate.Services.Contracts.Enums.SalesRelationShipTypes salesRelationShipTypes, long productID)
        {
            return service.GetSalesRelationshipTypeList(salesRelationShipTypes, productID);
        }

        public ProductDTO GetProductDetailByProductId(long productID)
        {
            return service.GetProductDetailByProductId(productID);
        }

        public List<PropertyDTO> CreateProperties(List<PropertyDTO> dtoPropertyList)
        {
            return service.CreateProperties(dtoPropertyList);
        }

        public List<ProductPriceListTypeDTO> GetProductPriceListTypes()
        {
            return service.GetProductPriceListTypes();
        }

        public List<ProductPriceListLevelDTO> GetProductPriceListLevels()
        {
            return service.GetProductPriceListLevels();
        }

        public List<KeyValueDTO> SearchBrand(string searchText, int pageSize)
        {
            return service.SearchBrand(searchText, pageSize);
        }

        public List<KeyValueDTO> SearchProductFamilies(string searchText, int pageSize)
        {
            return service.SearchProductFamilies(searchText, pageSize);
        }

        public Services.Contracts.Catalog.PropertyTypeDTO GetPropertyType(byte propertyTypeID)
        {
            return service.GetPropertyType(propertyTypeID);
        }

        public List<ProductTypeDTO> GetProductTypes()
        {
            return service.GetProductTypes();
        }

        public List<KeyValueDTO> GetProdctSKUTags()
        {
            return service.GetProdctSKUTags();
        }

        public string SaveProductSkuBarCode(long skuId, string barCode)
        {
            return service.SaveProductSkuBarCode(skuId, barCode);
        }

        public string SaveProductSkuPartNo(long skuId, string partNo)
        {
            return service.SaveProductSkuPartNo(skuId, partNo);
        }

        public Services.Contracts.Inventory.ProductInventoryBranchDTO GetInventoryDetailsSKUID(long skuId, long customerID, bool type)
        {
            return service.GetInventoryDetailsSKUID(skuId, customerID, type);
        }

        public bool SaveProductSKUTags(ProductSKUTagDTO productSKUTags)
        {
            return service.SaveProductSKUTags(productSKUTags);
        }

        public List<KeyValueDTO> GetProductSKUTagMaps(long skuID)
        {
            return service.GetProductSKUTagMaps(skuID);
        }

        public SKUDTO GetProductSKUDetails(long skuID)
        {
            return service.GetProductSKUDetails(skuID);
        }

        public SKUDTO SaveProductSKUDetails(SKUDTO sku)
        {
            return service.SaveProductSKUDetails(sku);
        }

        public string SaveSKUProductMap(long productID,long skuID) 
        {
            return service.SaveSKUProductMap(productID, skuID);
        }

        public bool SaveSKUManagers(AddProductDTO skuManagers)
        {
            return service.SaveSKUManagers(skuManagers);
        }

        public SKUDTO GetSKUEmployeeDetails(long skuID)
        {
            return service.GetSKUEmployeeDetails(skuID);
        }

        public string GetProductSkuCode(long skuID)
        {
            return service.GetProductSkuCode(skuID);
        }

        public List<ProductCategoryDTO> GetReportingCategoryList()
        {
            return service.GetReportingCategoryList();
        }

        public List<ProductDTO> GetProductListByCategoryID(long categoryID)
        {
            return service.GetProductListByCategoryID(categoryID);
        }

        //public List<ProductBundleDTO> GetProductBundleItemDetail(long productSKUMapID)
        //{
        //    return service.GetProductBundleItemDetail(productSKUMapID);
        //}

    }
}


