using System.ServiceModel;
using Eduegate.Domain;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Eduegates;
using Catalog = Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Services;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services
{
    public class ProductDetail : BaseService, IProductDetail
    {
        public ProductDetail()
        {
            
        }

        public ProductFeatureDTO GetProductDetail(long ProductId, long skuID, string propertyTypeValue, Eduegate.Services.Contracts.Enums.ProductStatuses productStatus)
        {
            try
            {
                var productDetailBL = new ProductDetailBL(CallContext);
                return productDetailBL.GetProductDetail(ProductId, skuID, propertyTypeValue, productStatus, CallContext);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductFeatureDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<QuantityDTO> GetQuantityListByProductIID(decimal productIID)
        {
            List<QuantityDTO> quantityDToList = new List<QuantityDTO>();

            try
            {
                var productDetailBL = new ProductDetailBL(CallContext);
                quantityDToList = productDetailBL.GetQuantityListByProductIID(productIID);
                return quantityDToList;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Product>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<ImageDTO> GetImagesByProductIID(decimal productIID)
        {
            List<ImageDTO> imageDTOList = new List<ImageDTO>();

            try
            {
                var productDetailBL = new ProductDetailBL(CallContext);
                imageDTOList = productDetailBL.GetImagesByProductIID(productIID);
                return imageDTOList;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Product>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public Catalog.ProductsDTO GetProducts(ProductSearchInfoDTO searchInfo)
        {
            try
            {
                var productDetailBL = new ProductDetailBL(CallContext);
                var products = productDetailBL.GetProducts(searchInfo);
                Eduegate.Logger.LogHelper<ProductDetail>.Info("Service Result : " + products.ToString());
                return products;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetail>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public Catalog.ProductViewDTO GetProductSummaryInfo()
        {
            try
            {
                var productDetailBL = new ProductDetailBL(CallContext);
                Catalog.ProductViewDTO productViewDTO = productDetailBL.GetProductSummaryInfo();
                Eduegate.Logger.LogHelper<ProductDetail>.Info("Service Result : " + productViewDTO.ToString());
                return productViewDTO;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetail>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<Catalog.ProductItemDTO> GetProductViews(Catalog.ProductViewSearchInfoDTO searchInfo)
        {
            try
            {
                var productDetailBL = new ProductDetailBL(CallContext);
                var productViews = productDetailBL.GetProductViews(searchInfo);
                Eduegate.Logger.LogHelper<ProductDetail>.Info("Service Result : " + productViews.ToString());
                return productViews;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetail>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<Catalog.ProductItemDTO> GetProductList()
        {
            try
            {
                var productDetailBL = new ProductDetailBL(CallContext);
                List<Catalog.ProductItemDTO> productDTOList = productDetailBL.GetProductList();
                Eduegate.Logger.LogHelper<ProductDetail>.Info("Service Result : " + productDTOList.ToString());
                return productDTOList;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetail>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public Catalog.TransactionDTO SaveTransactions(Catalog.TransactionDTO transaction)
        {
            try
            {
                var productDetailBL = new ProductDetailBL(CallContext);
                Eduegate.Logger.LogHelper<ProductDetail>.Info("Service Result : " + transaction.ToString());
                return productDetailBL.SaveTransactions(transaction);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetail>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<Catalog.TransactionDetailDTO> GetTransactionByTransactionID(long transactionID)
        {
            try
            {
                var productDetailBL = new ProductDetailBL(CallContext);
                List<Catalog.TransactionDetailDTO> transactionDetails = productDetailBL.GetTransactionByTransactionID(transactionID);
                Eduegate.Logger.LogHelper<ProductDetail>.Info("Service Result : " + transactionDetails.ToString());
                return transactionDetails;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetail>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<Catalog.TransactionDetailDTO> GetTransactionByTransactionDate(DateTime transactionDate)
        {
            try
            {
                var productDetailBL = new ProductDetailBL(CallContext);
                List<Catalog.TransactionDetailDTO> transactionDetails = productDetailBL.GetTransactionByTransactionDate(transactionDate);
                Eduegate.Logger.LogHelper<ProductDetail>.Info("Service Result : " + transactionDetails.ToString());
                return transactionDetails;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetail>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public Catalog.ProductPriceDTO CreatePriceInformationDetail(Catalog.ProductPriceDTO productPriceDTO)
        {
            try
            {
                var productDetailBL = new ProductDetailBL(CallContext);
                return productDetailBL.CreatePriceInformationDetail(productPriceDTO);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetail>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<Catalog.ProductPriceSKUDTO> GetProductPriceSKUMaps()
        {
            try
            {
                var productDetailBL = new ProductDetailBL(CallContext);
                List<Catalog.ProductPriceSKUDTO> productPriceSKUDTOList = productDetailBL.GetProductPriceSKUMaps();
                Eduegate.Logger.LogHelper<ProductDetail>.Info("Service Result : " + productPriceSKUDTOList.ToString());
                return productPriceSKUDTOList;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetail>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<ProductPriceSKUDTO> UpdateSKUProductPrice(List<Catalog.ProductPriceSKUDTO> productPriceSKUDTOList)
        {
            try
            {
                var productDetailBL = new ProductDetailBL(CallContext);
                Eduegate.Logger.LogHelper<ProductDetail>.Info("Service Result : " + productPriceSKUDTOList.ToString());
                return productDetailBL.UpdateSKUProductPrice(productPriceSKUDTOList);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetail>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public long UpdateProduct(Catalog.AddProductDTO productDetails)
        {
            try
            {
                var productDetailBL = new ProductDetailBL(CallContext);
                long productID = productDetailBL.UpdateProduct(productDetails);
                Eduegate.Logger.LogHelper<ProductDetail>.Info("Service Result : " + productID.ToString());
                return productID;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetail>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<CultureDataInfoDTO> GetCultureList()
        {
            try
            {
                var productDetailBL = new ProductDetailBL(CallContext);
                var culture = productDetailBL.GetCultureList();
                Eduegate.Logger.LogHelper<ProductDetail>.Info("Service Result : " + culture.Count.ToString());
                return culture;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetail>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<BrandDTO> GetBrandList()
        {
            try
            {
                var productDetailBL = new ProductDetailBL(CallContext);
                var brandList = productDetailBL.GetBrandList();
                Eduegate.Logger.LogHelper<ProductDetail>.Info("Service Result : " + brandList.Count.ToString());
                return brandList;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetail>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<Catalog.ProductFamilyDTO> GetProductFamilies()
        {
            try
            {
                var productDetailBL = new ProductDetailBL(CallContext);
                var productFamilies = productDetailBL.GetProductFamilies();
                Eduegate.Logger.LogHelper<ProductDetail>.Info("Service Result : " + productFamilies.Count.ToString());
                return productFamilies;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetail>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public Catalog.ProductFamilyDTO GetProductFamily(string familyID)
        {
            try
            {
                var productDetailBL = new ProductDetailBL(CallContext);
                var productFamilies = productDetailBL.GetProductFamily(long.Parse(familyID));
                Eduegate.Logger.LogHelper<ProductFamilyDTO>.Info("Service Result : " + productFamilies.ToString());
                return productFamilies;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetail>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public Catalog.ProductFamilyDTO SaveProductFamily(Catalog.ProductFamilyDTO dto)
        {
            try
            {
                var productDetailBL = new ProductDetailBL(CallContext);
                var productFamilies = productDetailBL.SaveProductFamily(dto);
                Eduegate.Logger.LogHelper<ProductFamilyDTO>.Info("Service Result : " + productFamilies.ToString());
                return productFamilies;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetail>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<Catalog.ProductStatusDTO> GetProductStatus()
        {
            try
            {
                var productDetailBL = new ProductDetailBL(CallContext);
                var productStatus = productDetailBL.GetProductStatus();
                Eduegate.Logger.LogHelper<ProductDetail>.Info("Service Result : " + productStatus.Count.ToString());
                return productStatus;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetail>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<Catalog.PropertyTypeDTO> GetProductPropertyTypes(decimal productFamilyIID)
        {
            try
            {
                var productDetailBL = new ProductDetailBL(CallContext);
                var propertyType = productDetailBL.GetProductPropertyTypes(productFamilyIID);
                Eduegate.Logger.LogHelper<ProductDetail>.Info("Service Result : " + propertyType.Count.ToString());
                return propertyType;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetail>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<PropertyDTO> GetPropertiesByPropertyTypeID(decimal propertyTypeIID)
        {
            try
            {
                var productDetailBL = new ProductDetailBL(CallContext);
                var properties = productDetailBL.GetPropertiesByPropertyTypeID(propertyTypeIID);
                Eduegate.Logger.LogHelper<ProductDetail>.Info("Service Result : " + properties.Count.ToString());
                return properties;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetail>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }
        public List<ProductCategoryDTO> GetCategoryList()
        {
            try
            {
                var productDetailBL = new ProductDetailBL(CallContext);
                var categories = productDetailBL.GetCategoryList();
                Eduegate.Logger.LogHelper<ProductDetail>.Info("Service Result : " + categories.Count.ToString());
                return categories;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetail>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }
        public List<ProductCategoryDTO> GetReportingCategoryList()
        {
            try
            {
                var productDetailBL = new ProductDetailBL(CallContext);
                var categories = productDetailBL.GetCategoryList(true);
                Eduegate.Logger.LogHelper<ProductDetail>.Info("Service Result : " + categories.Count.ToString());
                return categories;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetail>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }
        public AddProductDTO GetProduct(long productIID)
        {
            try
            {
                var productDetailBL = new ProductDetailBL(CallContext);
                var product = productDetailBL.GetProduct(productIID);
                Eduegate.Logger.LogHelper<ProductDetail>.Info("Service Result : " + product.QuickCreate.ProductIID.ToString());
                return product;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetail>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        List<Catalog.BrandDTO> IProductDetail.GetBrandList()
        {
            try
            {
                var productDetailBL = new ProductDetailBL(CallContext);
                var brandList = productDetailBL.GetBrandList();
                Eduegate.Logger.LogHelper<Catalog.BrandDTO>.Info("Service Result : " + brandList.Count.ToString());
                return brandList;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetail>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<PropertyDTO> GetProperitesByProductFamilyID(long productFamilyID)
        {
            try
            {
                var productDetailBL = new ProductDetailBL(CallContext);
                var properties = productDetailBL.GetProperitesByProductFamilyID(productFamilyID);
                Eduegate.Logger.LogHelper<ProductDetail>.Info("Service Result : " + properties.Count.ToString());
                return properties;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetail>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<ProductDTO> GetSuggestedProduct(long productID)
        {
            var productDetailBL = new ProductDetailBL(CallContext);
            return productDetailBL.GetSuggestedProduct(productID, CallContext);
        }

        public List<ProductDTO> GetProductListBySearchText(string searchtext, long excludeProductFamily = default(long))
        {
            try
            {
                var productDetailBL = new ProductDetailBL(CallContext);
                return productDetailBL.GetProductListBySearchText(searchtext, excludeProductFamily);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetail>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<ProductDTO> GetProductListByCategoryID(long categoryID)
        {
            try
            {
                var productDetailBL = new ProductDetailBL(CallContext);
                return productDetailBL.GetProductListByCategoryID(categoryID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetail>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool SaveProductMaps(ProductToProductMapDTO productMaps)
        {
            try
            {
                var productDetailBL = new ProductDetailBL(CallContext);
                return productDetailBL.SaveProductMaps(productMaps, CallContext);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetail>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public ProductToProductMapDTO GetProductMaps(long productID)
        {
            try
            {
                var productDetailBL = new ProductDetailBL(CallContext);
                return productDetailBL.GetProductMaps(productID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetail>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }


        public TransactionDTO GetTransaction(long headIID)
        {
            try
            {
                var productDetailBL = new ProductDetailBL(CallContext);
                return productDetailBL.GetTransaction(headIID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetail>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool RemoveProductPriceListSKUMaps(long id)
        {
            var productDetailBL = new ProductDetailBL(CallContext);
            return productDetailBL.RemoveProductPriceListSKUMaps(id);
        }


        public ProductPriceDTO GetProductPriceListDetail(long id)
        {
            var productDetailBL = new ProductDetailBL(CallContext);
            return productDetailBL.GetProductPriceListDetail(id);
        }


        public List<ProductPriceSKUDTO> GetProductPriceSKU(long id)
        {
            var productDetailBL = new ProductDetailBL(CallContext);
            return productDetailBL.GetProductPriceSKU(id);
        }


        public PropertyDTO GetProperty(string propertyID)
        {
            try
            {
                var productDetailBL = new ProductDetailBL(CallContext);
                var productProperties = productDetailBL.GetProperty(long.Parse(propertyID));
                Eduegate.Logger.LogHelper<PropertyDTO>.Info("Service Result : " + productProperties.ToString());
                return productProperties;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<PropertyDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }


        public PropertyDTO SaveProperty(PropertyDTO propertyDTO)
        {
            try
            {
                var productDetailBL = new ProductDetailBL(CallContext);
                var productFamilies = productDetailBL.SaveProperty(propertyDTO);
                Eduegate.Logger.LogHelper<PropertyDTO>.Info("Service Result : " + productFamilies.ToString());
                return productFamilies;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<PropertyDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<SalesRelationshipTypeDTO> GetSalesRelationshipType(Eduegate.Services.Contracts.Enums.SalesRelationShipTypes salesRelationShipTypes)
        {
            var productDetailBL = new ProductDetailBL(CallContext);
            return productDetailBL.GetSalesRelationshipType(salesRelationShipTypes);
        }

        public List<SalesRelationshipTypeDTO> GetSalesRelationshipTypeList(Eduegate.Services.Contracts.Enums.SalesRelationShipTypes salesRelationShipTypes, long productID)
        {
            var productDetailBL = new ProductDetailBL(CallContext);
            return productDetailBL.GetSalesRelationshipTypeList(salesRelationShipTypes, productID);
        }

        public ProductDTO GetProductDetailByProductId(long productID)
        {
            var productDetailBL = new ProductDetailBL(CallContext);
            return productDetailBL.GetProductDetailByProductId(productID);
        }

        public List<PropertyDTO> CreateProperties(List<PropertyDTO> dtoPropertyList)
        {
            try
            {
                var productDetailBL = new ProductDetailBL(CallContext);
                var properties = productDetailBL.CreateProperties(dtoPropertyList);
                Eduegate.Logger.LogHelper<PropertyDTO>.Info("Service Result : " + properties.ToString());
                return properties;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<PropertyDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<ProductPriceListTypeDTO> GetProductPriceListTypes()
        {
            return new ProductDetailBL(CallContext).GetProductPriceListTypes();
        }

        public List<ProductPriceListLevelDTO> GetProductPriceListLevels()
        {
            return new ProductDetailBL(CallContext).GetProductPriceListLevels();
        }

        public List<KeyValueDTO> SearchBrand(string searchText, int pageSize)
        {
            try
            {
                var brandList = new ProductDetailBL(CallContext).SearchBrand(searchText, pageSize);
                Eduegate.Logger.LogHelper<ProductDetail>.Info("Service Result : " + brandList.Count.ToString());
                return brandList;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetail>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<KeyValueDTO> SearchProductFamilies(string searchText, int pageSize)
        {
            try
            {
                var brandList = new ProductDetailBL(CallContext).SearchProductFamilies(searchText, pageSize);
                Eduegate.Logger.LogHelper<ProductDetail>.Info("Service Result : " + brandList.Count.ToString());
                return brandList;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetail>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public PropertyTypeDTO SavePropertyType(PropertyTypeDTO propertyTypeDTO)
        {
            try
            {
                var productDetailBL = new ProductDetailBL(CallContext);
                var propertyType = productDetailBL.SavePropertyType(propertyTypeDTO);
                Eduegate.Logger.LogHelper<PropertyTypeDTO>.Info("Service Result : " + propertyType.ToString());
                return propertyType;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<PropertyTypeDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public PropertyTypeDTO GetPropertyType(byte propertyTypeID)
        {
            var productDetailBL = new ProductDetailBL(CallContext);
            return productDetailBL.GetPropertyType(propertyTypeID);
        }

        public List<ProductTypeDTO> GetProductTypes()
        {
            try
            {
                var productTypes = new ProductDetailBL(CallContext).GetProductTypes();
                Eduegate.Logger.LogHelper<ProductDetail>.Info("Service Result : " + productTypes.Count.ToString());
                return productTypes;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetail>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<KeyValueDTO> GetProdctSKUTags()
        {
            try
            {
                var tags = new ProductDetailBL(CallContext).GetProdctSKUTags();
                Eduegate.Logger.LogHelper<KeyValueDTO>.Info("Service Result : " + tags.Count.ToString());
                return tags;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<KeyValueDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public string SaveProductSkuBarCode(long skuId, string barCode)
        {
            return new ProductDetailBL().SaveProductSkuBarCode(skuId, barCode);
        }

        public string SaveProductSkuPartNo(long skuId, string partNo)
        {
            return new ProductDetailBL().SaveProductSkuPartNo(skuId, partNo);
        }

        public Contracts.Inventory.ProductInventoryBranchDTO GetInventoryDetailsSKUID(long skuId, long customerID, bool type)
        {
            var productDetailBL = new ProductDetailBL(CallContext);
            return productDetailBL.GetInventoryDetailsSKUID(skuId, customerID, type);
        }

        public bool SaveProductSKUTags(ProductSKUTagDTO productSKUTags)
        {
            return new ProductDetailBL(CallContext).SaveProductSKUTags(productSKUTags);
        }

        public List<KeyValueDTO> GetProductSKUTagMaps(long skuId)
        {
            return new ProductDetailBL(CallContext).GetProductSKUTagMaps(skuId);
        }

        public SKUDTO GetProductSKUDetails(long skuID)
        {
            try
            {
                var productSKUDetail = new ProductDetailBL(CallContext).GetProductSKUDetails(skuID);
                Eduegate.Logger.LogHelper<KeyValueDTO>.Info("Service Result: " + productSKUDetail);
                return productSKUDetail;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<KeyValueDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
            
        }

        public SKUDTO SaveProductSKUDetails(SKUDTO sku)
        {
            try
            {
                SKUDTO productSKUDetail = new ProductDetailBL(CallContext).SaveProductSKUDetails(sku);
                Eduegate.Logger.LogHelper<KeyValueDTO>.Info("Service Result: " + productSKUDetail);
                return productSKUDetail;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<KeyValueDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public string SaveSKUProductMap(long productID,long skuID)
        { 
            try
            {
                var productSKU = new ProductDetailBL(CallContext).SaveSKUProductMap(productID, skuID);
                Eduegate.Logger.LogHelper<KeyValueDTO>.Info("Service Result: " + productSKU);
                return productSKU;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<KeyValueDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool SaveSKUManagers(AddProductDTO skuManagers)
        {
            return new ProductDetailBL(CallContext).SaveSKUManagers(skuManagers);
        }

        public SKUDTO GetSKUEmployeeDetails(long skuID)
        { 
            try
            {
                var productSKUDetail = new ProductDetailBL(CallContext).GetProductSKUDetails(skuID);
                Eduegate.Logger.LogHelper<KeyValueDTO>.Info("Service Result: " + productSKUDetail);
                return productSKUDetail;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<KeyValueDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }

        }

        public string GetProductSkuCode(long skuID)
        {
            try
            {
                return new ProductDetailBL(CallContext).GetProductSkuCode(skuID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<KeyValueDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        //public List<ProductBundleDTO> GetProductBundleItemDetail(long productSKUMapID)
        //{
        //    try
        //    {
               
        //        var productDetailBL = new ProductDetailBL(CallContext);
        //        var bundleItems = productDetailBL.GetProductBundleItemDetail(productSKUMapID);
        //        Eduegate.Logger.LogHelper<ProductDetail>.Info("Service Result : " + bundleItems.Count.ToString());
        //        return bundleItems;
        //    }
        //    catch (Exception exception)
        //    {
        //        Eduegate.Logger.LogHelper<ProductBundleDTO>.Fatal(exception.Message, exception);
        //        throw new FaultException("Internal server, please check with your administrator");
        //    }
        //}
    }
}
