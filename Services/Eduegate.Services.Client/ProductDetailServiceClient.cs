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

namespace Eduegate.Service.Client
{
    public class ProductDetailServiceClient : BaseClient, IProductDetail
    {
        private static string ServiceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string Service = string.Concat(ServiceHost, Eduegate.Framework.Helper.Constants.PRODUCT_DETAIL_SERVICE_NAME);

        public ProductDetailServiceClient(CallContext callContext = null, Action<string> logger = null)
            : base(callContext, logger)
        {

        }

        public Services.Contracts.Eduegates.ProductFeatureDTO GetProductDetail(long ProductId, long skuID, string propertyTypeValue, Services.Contracts.Enums.ProductStatuses productStatus)
        {
            throw new NotImplementedException();
        }

        public List<Services.Contracts.Eduegates.QuantityDTO> GetQuantityListByProductIID(decimal productIID)
        {
            throw new NotImplementedException();
        }

        public List<Services.Contracts.Eduegates.ImageDTO> GetImagesByProductIID(decimal productIID)
        {
            throw new NotImplementedException();
        }

        public Services.Contracts.Catalog.ProductsDTO GetProducts(Services.Contracts.Eduegates.ProductSearchInfoDTO searchInfo)
        {
            var result = ServiceHelper.HttpPostRequest(Service + "GetProducts", searchInfo);
            return JsonConvert.DeserializeObject<Services.Contracts.Catalog.ProductsDTO>(result);
        }

        public Services.Contracts.Catalog.ProductViewDTO GetProductSummaryInfo()
        {
            return ServiceHelper.HttpGetRequest<Services.Contracts.Catalog.ProductViewDTO>(string.Format("{0}/GetProductSummaryInfo", Service, _callContext, _logger));
        }

        public List<Services.Contracts.Catalog.ProductItemDTO> GetProductViews(Services.Contracts.Catalog.ProductViewSearchInfoDTO searchInfo)
        {
            var result = ServiceHelper.HttpPostRequest(Service + "GetProductViews", searchInfo);
            return JsonConvert.DeserializeObject<List<Services.Contracts.Catalog.ProductItemDTO>>(result);
        }


        public List<Services.Contracts.Catalog.ProductItemDTO> GetProductList()
        {
            throw new NotImplementedException();
        }

        public Services.Contracts.Catalog.TransactionDTO SaveTransactions(Services.Contracts.Catalog.TransactionDTO transaction)
        {
            var uri = string.Format("{0}/SaveTransactions", Service);
            return ServiceHelper.HttpPostGetRequest<TransactionDTO>(uri, transaction, _callContext, _logger);
        }

        public List<Services.Contracts.Catalog.TransactionDetailDTO> GetTransactionByTransactionID(long transactionID)
        {
            throw new NotImplementedException();
        }

        public List<Services.Contracts.Catalog.TransactionDetailDTO> GetTransactionByTransactionDate(DateTime transactionDate)
        {
            throw new NotImplementedException();
        }

        public ProductPriceDTO CreatePriceInformationDetail(Services.Contracts.Catalog.ProductPriceDTO productPriceDTO)
        {
            throw new NotImplementedException();
        }

        public List<Services.Contracts.Catalog.ProductPriceSKUDTO> GetProductPriceSKUMaps()
        {
            throw new NotImplementedException();
        }

        public List<Services.Contracts.Catalog.ProductPriceSKUDTO> UpdateSKUProductPrice(List<Services.Contracts.Catalog.ProductPriceSKUDTO> productPriceSKUDTOList)
        {
            throw new NotImplementedException();
        }

        public long UpdateProduct(Services.Contracts.Catalog.AddProductDTO productDetails)
        {
            throw new NotImplementedException();
        }

        public List<CultureDataInfoDTO> GetCultureList()
        {
            return ServiceHelper.HttpGetRequest<List<CultureDataInfoDTO>>(Service + "GetCultureList");
        }

        public List<Services.Contracts.Catalog.BrandDTO> GetBrandList()
        {
            throw new NotImplementedException();
        }

        public List<Services.Contracts.Catalog.ProductFamilyDTO> GetProductFamilies()
        {
            var uri = string.Format("{0}/{1}", Service, "GetProductFamilies");
            return ServiceHelper.HttpGetRequest<List<ProductFamilyDTO>>(uri, _callContext, _logger);
        }

        public Services.Contracts.Catalog.ProductFamilyDTO GetProductFamily(string familyID)
        {
            var uri = string.Format("{0}/GetProductFamily?familyID={1}", Service, familyID);
            return ServiceHelper.HttpGetRequest<ProductFamilyDTO>(uri, _callContext, _logger);
        }

        public Services.Contracts.Catalog.ProductFamilyDTO SaveProductFamily(ProductFamilyDTO familyDTO)
        {
            var uri = string.Format("{0}/SaveProductFamily", Service);
            return ServiceHelper.HttpPostGetRequest<ProductFamilyDTO>(uri, familyDTO, _callContext, _logger);
        }

        public List<Services.Contracts.Catalog.ProductStatusDTO> GetProductStatus()
        {
            var uri = string.Format("{0}/GetProductStatus", Service);
            return ServiceHelper.HttpGetRequest<List<ProductStatusDTO>>(uri, _callContext, _logger);
        }

        public List<Services.Contracts.Catalog.PropertyTypeDTO> GetProductPropertyTypes(decimal productFamilyIID)
        {
            throw new NotImplementedException();
        }

        public List<PropertyDTO> GetPropertiesByPropertyTypeID(decimal propertyTypeIID)
        {
            throw new NotImplementedException();
        }

        public List<ProductCategoryDTO> GetCategoryList()
        {
            var uri = string.Format("{0}/{1}", Service, "GetCategoryList");
            return ServiceHelper.HttpGetRequest<List<ProductCategoryDTO>>(uri, _callContext, _logger);
        }

        public Services.Contracts.Catalog.AddProductDTO GetProduct(long productIID)
        {
            return ServiceHelper.HttpGetRequest<Services.Contracts.Catalog.AddProductDTO>
                (Service + "GetProduct?productIID=" + productIID, _callContext, _logger);
        }

        public List<PropertyDTO> GetProperitesByProductFamilyID(long productFamilyID)
        {
            throw new NotImplementedException();
        }

        public List<ProductDTO> GetSuggestedProduct(long productID)
        {
            throw new NotImplementedException();
        }

        public List<ProductDTO> GetProductListBySearchText(string searchtext, long excludeProductFamilyID = default(long))
        {
            throw new NotImplementedException();
        }

        public List<ProductDTO> GetProductListByCategoryID(long categoryID)
        {
            throw new NotImplementedException();
        }

        public bool SaveProductMaps(Services.Contracts.Catalog.ProductToProductMapDTO productMaps)
        {
            throw new NotImplementedException();
        }

        public Services.Contracts.Catalog.ProductToProductMapDTO GetProductMaps(long productID)
        {
            throw new NotImplementedException();
        }

        public Services.Contracts.Catalog.TransactionDTO GetTransaction(long headIID)
        {
            var uri = string.Format("{0}/GetTransaction?headIID={1}", Service, headIID);
            return ServiceHelper.HttpGetRequest<TransactionDTO>(uri, _callContext, _logger);
        }

        public bool RemoveProductPriceListSKUMaps(long id)
        {
            throw new NotImplementedException();
        }

        public Services.Contracts.Catalog.ProductPriceDTO GetProductPriceListDetail(long id)
        {
            var uri = string.Format("{0}/GetProductPriceListDetail?id={1}", Service, id);
            return ServiceHelper.HttpGetRequest<ProductPriceDTO>(uri, _callContext, _logger);
        }

        public List<Services.Contracts.Catalog.ProductPriceSKUDTO> GetProductPriceSKU(long id)
        {
            throw new NotImplementedException();
        }

        public PropertyDTO GetProperty(string propertyID)
        {
            var uri = string.Format("{0}/GetProperty?propertyID={1}", Service, propertyID);
            return ServiceHelper.HttpGetRequest<PropertyDTO>(uri, _callContext, _logger);
        }

        public PropertyDTO SaveProperty(PropertyDTO propertyDTO)
        {
            var uri = string.Format("{0}/SaveProperty", Service);
            return ServiceHelper.HttpPostGetRequest<PropertyDTO>(uri, propertyDTO, _callContext, _logger);
        }
        public PropertyTypeDTO SavePropertyType(PropertyTypeDTO propertyTypeDTO)
        {
            var uri = string.Format("{0}/SavePropertyType", Service);
            return ServiceHelper.HttpPostGetRequest<PropertyTypeDTO>(uri, propertyTypeDTO, _callContext, _logger);
        }


        public List<SalesRelationshipTypeDTO> GetSalesRelationshipType(Eduegate.Services.Contracts.Enums.SalesRelationShipTypes salesRelationShipTypes)
        {
            var uri = string.Format("{0}/GetSalesRelationshipType?salesRelationShipTypes={1}", Service, salesRelationShipTypes);
            return ServiceHelper.HttpGetRequest<List<SalesRelationshipTypeDTO>>(uri, _callContext, _logger);
        }


        public List<SalesRelationshipTypeDTO> GetSalesRelationshipTypeList(Eduegate.Services.Contracts.Enums.SalesRelationShipTypes salesRelationShipTypes, long productID)
        {
            var uri = string.Format("{0}/GetSalesRelationshipTypeList?salesRelationShipTypes={1}&productID={2}", Service, salesRelationShipTypes, productID);
            return ServiceHelper.HttpGetRequest<List<SalesRelationshipTypeDTO>>(uri, _callContext, _logger);
        }

        public ProductDTO GetProductDetailByProductId(long productID)
        {
            var uri = string.Format("{0}/GetProductDetailByProductId?productID={1}", Service, productID);
            return ServiceHelper.HttpGetRequest<ProductDTO>(uri, _callContext, _logger);
        }

        public List<PropertyDTO> CreateProperties(List<PropertyDTO> dtoPropertyList)
        {
            var uri = string.Format("{0}/CreateProperties", Service);
            return ServiceHelper.HttpPostGetRequest<List<PropertyDTO>>(uri, dtoPropertyList, _callContext, _logger);
        }

        public List<ProductPriceListTypeDTO> GetProductPriceListTypes()
        {
            var uri = string.Format("{0}/GetProductPriceListTypes", Service);
            return ServiceHelper.HttpGetRequest<List<ProductPriceListTypeDTO>>(uri, _callContext, _logger);
        }

        public List<ProductPriceListLevelDTO> GetProductPriceListLevels()
        {
            var uri = string.Format("{0}/GetProductPriceListLevels", Service);
            return ServiceHelper.HttpGetRequest<List<ProductPriceListLevelDTO>>(uri, _callContext, _logger);
        }

        public List<KeyValueDTO> SearchBrand(string searchText, int pageSize)
        {
            var uri = string.Format("{0}/SearchBrand?searchText={1}&pageSize={2}", Service, searchText, pageSize);
            return ServiceHelper.HttpGetRequest<List<KeyValueDTO>>(uri, _callContext, _logger);
        }

        public List<KeyValueDTO> SearchProductFamilies(string searchText, int pageSize)
        {
            var uri = string.Format("{0}/SearchProductFamilies?searchText={1}&pageSize={2}", Service, searchText, pageSize);
            return ServiceHelper.HttpGetRequest<List<KeyValueDTO>>(uri, _callContext, _logger);
        }

        public Services.Contracts.Catalog.PropertyTypeDTO GetPropertyType(byte propertyTypeID)
        {
            var uri = string.Format("{0}/GetPropertyType?propertyTypeID={1}", Service, propertyTypeID);
            return ServiceHelper.HttpGetRequest<PropertyTypeDTO>(uri, _callContext, _logger);
        }

        public List<ProductTypeDTO> GetProductTypes()
        {
            var uri = string.Format("{0}/GetProductTypes", Service);
            return ServiceHelper.HttpGetRequest<List<ProductTypeDTO>>(uri, _callContext, _logger);
        }

        public List<KeyValueDTO> GetProdctSKUTags()
        {
            var uri = string.Format("{0}/GetProdctSKUTags", Service);
            return ServiceHelper.HttpGetRequest<List<KeyValueDTO>>(uri, _callContext, _logger);
        }

        public string SaveProductSkuBarCode(long skuId, string barCode)
        {
            var uri = string.Concat(Service, "SaveProductSkuBarCode?skuId=" + skuId + "&barCode=" + barCode + "");
            return ServiceHelper.HttpGetRequest<string>(uri, _callContext);
        }

        public string SaveProductSkuPartNo(long skuId, string partNo)
        {
            var uri = string.Concat(Service, "SaveProductSkuPartNo?skuId=" + skuId + "&partNo=" + partNo + "");
            return ServiceHelper.HttpGetRequest<string>(uri, _callContext);
        }

        public Services.Contracts.Inventory.ProductInventoryBranchDTO GetInventoryDetailsSKUID(long skuId, long customerID, bool type)
        {
            var uri = string.Concat(Service, "GetInventoryDetailsSKUID?skuId=" + skuId + "&customerID=" + customerID + "&type=" + type.ToString());
            return ServiceHelper.HttpGetRequest<Services.Contracts.Inventory.ProductInventoryBranchDTO>(uri, _callContext);
        }

        public bool SaveProductSKUTags(ProductSKUTagDTO productSKUTags)
        {
            var uri = string.Format("{0}/SaveProductSKUTags", Service);
            return bool.Parse(ServiceHelper.HttpPostRequest(uri, productSKUTags, _callContext));
        }

        public List<KeyValueDTO> GetProductSKUTagMaps(long skuID)
        {
            var uri = string.Concat(Service, "GetProductSKUTagMaps?skuId=" + skuID);
            return ServiceHelper.HttpGetRequest<List<KeyValueDTO>>(uri, _callContext);
        }

        public SKUDTO GetProductSKUDetails(long skuID)
        {
            var uri = string.Concat(Service, "GetProductSKUDetails?skuId=" + skuID);
            return ServiceHelper.HttpGetRequest<SKUDTO>(uri, _callContext);
        }

        public SKUDTO SaveProductSKUDetails(SKUDTO sku)
        {
            var result = ServiceHelper.HttpPostRequest(Service + "SaveProductSKUDetails", sku, _callContext);
            return JsonConvert.DeserializeObject<SKUDTO>(result);
        }

        public string SaveSKUProductMap(long productID,long skuID) 
        {
            var uri = string.Concat(Service, "SaveSKUProductMap?productID="+ productID + "&skuID=" + skuID + "");
            return ServiceHelper.HttpGetRequest<string>(uri,_callContext);
        }

        public bool SaveSKUManagers(AddProductDTO skuManagers)
        {
            var uri = string.Format("{0}/SaveSKUManagers", Service);
            return bool.Parse(ServiceHelper.HttpPostRequest(uri, skuManagers, _callContext));
        }

        public SKUDTO GetSKUEmployeeDetails(long skuID)
        {
            var uri = string.Concat(Service, "GetSKUEmployeeDetails?skuId=" + skuID);
            return ServiceHelper.HttpGetRequest<SKUDTO>(uri, _callContext);
        }

        public string GetProductSkuCode(long skuID)
        {
            var uri = string.Concat(Service, "GetProductSkuCode?skuID=" + skuID);
            return ServiceHelper.HttpGetRequest<string>(uri, _callContext);
        }
        public List<ProductCategoryDTO> GetReportingCategoryList()
        {
            var uri = string.Format("{0}/{1}", Service, "GetReportingCategoryList");
            return ServiceHelper.HttpGetRequest<List<ProductCategoryDTO>>(uri, _callContext, _logger);
        }
        //public List<ProductBundleDTO> GetProductBundleItemDetail(long productSKUMapID)
        //{
        //    var uri = string.Concat(Service, "GetProductBundleItemDetail?productSKUMapID=" + productSKUMapID);
        //    return ServiceHelper.HttpGetRequest<List<ProductBundleDTO>>(uri, _callContext, _logger);
        //}
    }
}


