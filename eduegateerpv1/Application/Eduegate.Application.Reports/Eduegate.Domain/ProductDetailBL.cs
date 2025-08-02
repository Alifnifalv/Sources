using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Eduegates;
using Catalog = Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework;
using Helper = Eduegate.Framework.Helper.Enums;
using Eduegate.Domain.Entity.CustomEntity;
using Eduegate.Services.Contracts.MenuLinks;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Domain.Mappers;
using Eduegate.Services.Contracts.Inventory;
using System.Data;
using Eduegate.Globalization;
using System.Text.RegularExpressions;
using System.IO;
using Eduegate.Framework.Ftp;
using Eduegate.Domain.Repository.Payroll;
using Eduegate.Services.Contracts.Commons;

namespace Eduegate.Domain
{
    public class ProductDetailBL
    {
        private CallContext _callContext;
        private string ImageRootUrl = Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue("ImageRootUrl");
        ProductDetailRepository productDetailRepository = new ProductDetailRepository();
        //private static ProductCatalogRepository productCatalogRepository = new ProductCatalogRepository();
        private static ShoppingCartRepository shoppingCartRepository = new ShoppingCartRepository();

        public ProductDetailBL(CallContext context)
        {
            _callContext = context;
        }
        public ProductDetailBL()
        { }

        public ProductFeatureDTO GetProductDetail(long ProductId, long skuID, string propertyTypeValue,
            Eduegate.Services.Contracts.Enums.ProductStatuses productStatus, CallContext context)
        {
            decimal? ConvertedPrice = UtilityRepository.GetCurrencyPrice(context);

            if (!string.IsNullOrEmpty(propertyTypeValue))
            {
                return productDetailRepository.GetProductDetail(ProductId, propertyTypeValue, ConvertedPrice, productStatus);
            }
            else { return productDetailRepository.GetProductDetail(ProductId, skuID, ConvertedPrice, productStatus); }
        }

        public List<QuantityDTO> GetQuantityListByProductIID(decimal productIID)
        {
            List<QuantityDTO> quantityDTOList = new List<QuantityDTO>();

            quantityDTOList = productDetailRepository.GetQuantityListByProductIID(productIID);

            return quantityDTOList;
        }

        public List<ImageDTO> GetImagesByProductIID(decimal productIID)
        {
            List<ImageDTO> imageDTOList = new List<ImageDTO>();
            imageDTOList = productDetailRepository.GetImagesByProductIID(productIID);
            return imageDTOList;
        }

        public Catalog.ProductsDTO GetProducts(ProductSearchInfoDTO searchInfo)
        {
            //we need to create a new repository for populating Eduegates products
            //List<ProductDTO> products = new List<ProductDTO>();
            //var productDetails = productCatalogRepository.GetProductDetails(searchInfo);
            //return productDetails;
            return null;
        }

        public Catalog.ProductViewDTO GetProductSummaryInfo()
        {
            Catalog.ProductViewDTO productViewDTO = productDetailRepository.GetProductSummaryInfo();
            return productViewDTO;
        }

        public List<Catalog.ProductItemDTO> GetProductViews(Catalog.ProductViewSearchInfoDTO searchInfo)
        {
            var productViews = productDetailRepository.GetProductViews(searchInfo);
            return productViews;
        }

        public List<Catalog.ProductItemDTO> GetProductList()
        {
            List<Catalog.ProductItemDTO> productDTOList = productDetailRepository.GetProductList();
            return productDTOList;
        }

        public Catalog.TransactionDTO SaveTransactions(Catalog.TransactionDTO transaction)
        {
            TransactionHead transactionHead = new TransactionHead();

            if (transaction.IsNotNull())
            {
                if (transaction.TransactionHead.IsNotNull())
                {
                    transactionHead = new TransactionHead()
                    {
                        HeadIID = transaction.TransactionHead.HeadIID,
                        DocumentTypeID = (int)transaction.TransactionHead.DocumentTypeID,
                        TransactionDate = transaction.TransactionHead.TransactionDate.IsNotNull() ? Convert.ToDateTime(transaction.TransactionHead.TransactionDate) : DateTime.Now,
                        CustomerID = transaction.TransactionHead.CustomerID,
                        StudentID = transaction.TransactionHead.StudentID,
                        Description = transaction.TransactionHead.Description,
                        Reference = transaction.TransactionHead.Reference,
                        TransactionNo = transaction.TransactionHead.TransactionNo,
                        SupplierID = transaction.TransactionHead.SupplierID,
                        TransactionStatusID = transaction.TransactionHead.TransactionStatusID,
                        DiscountAmount = transaction.TransactionHead.DiscountAmount,
                        DiscountPercentage = transaction.TransactionHead.DiscountPercentage,
                        BranchID = transaction.TransactionHead.BranchID,
                        ToBranchID = transaction.TransactionHead.ToBranchID,
                        DueDate = transaction.TransactionHead.DueDate,
                        DeliveryDate = transaction.TransactionHead.DeliveryDate,
                        CurrencyID = transaction.TransactionHead.CurrencyID,
                        DeliveryMethodID = transaction.TransactionHead.DeliveryMethodID,
                        IsShipment = transaction.TransactionHead.IsShipment,
                        EntitlementID = transaction.TransactionHead.EntitlementID,
                        CreatedBy = transaction.TransactionHead.CreatedBy,
                        UpdatedBy = transaction.TransactionHead.UpdatedBy,
                        ReferenceHeadID = transaction.TransactionHead.ReferenceHeadID,
                        JobEntryHeadID = transaction.TransactionHead.JobEntryHeadID,
                        CompanyID = transaction.TransactionHead.CompanyID,
                        DeliveryTypeID = transaction.TransactionHead.DeliveryTypeID,
                        DeliveryCharge = transaction.TransactionHead.DeliveryCharge,
                        DeliveryDays = transaction.TransactionHead.DeliveryDays,
                        TransactionRole = transaction.TransactionHead.TransactionRole
                        //CreatedDate = Convert.ToDateTime(transaction.TransactionHead.CreatedDate),
                        //UpdatedDate = Convert.ToDateTime(transaction.TransactionHead.UpdatedDate),
                    };
                }

                if (transaction.TransactionDetails.IsNotNull() && transaction.TransactionDetails.Count > 0)
                {
                    transactionHead.TransactionDetails = new List<TransactionDetail>();

                    foreach (TransactionDetailDTO transactionDetailDTO in transaction.TransactionDetails)
                    {
                        var transactionDetail = new TransactionDetail();
                        transactionDetail.ProductSerialMaps = new List<ProductSerialMap>();

                        transactionDetail.DetailIID = transactionDetailDTO.DetailIID;
                        transactionDetail.HeadID = transactionDetailDTO.HeadID;
                        transactionDetail.ProductID = GetProductBySKUID(Convert.ToInt64(transactionDetailDTO.ProductSKUMapID)).ProductIID;
                        transactionDetail.ProductSKUMapID = transactionDetailDTO.ProductSKUMapID;
                        transactionDetail.Quantity = transactionDetailDTO.Quantity;
                        transactionDetail.UnitID = transactionDetailDTO.UnitID;
                        transactionDetail.DiscountPercentage = transactionDetailDTO.DiscountPercentage;
                        transactionDetail.UnitPrice = transactionDetailDTO.UnitPrice;
                        transactionDetail.Amount = transactionDetailDTO.Amount;
                        transactionDetail.ExchangeRate = transactionDetailDTO.ExchangeRate;
                        transactionDetail.CreatedBy = transactionDetailDTO.CreatedBy;
                        transactionDetail.UpdatedBy = transactionDetailDTO.UpdatedBy;
                        //CreatedDate = Convert.ToDateTime(transactionDetail.CreatedDate);
                        //UpdatedDate = Convert.ToDateTime(transactionDetail.UpdatedDate);
                        if (transactionDetailDTO.SKUDetails != null && transactionDetailDTO.SKUDetails.Count > 0)
                        {
                            foreach (var skuDetail in transactionDetailDTO.SKUDetails)
                            {
                                var serialMap = new ProductSerialMap();
                                serialMap.SerialNo = skuDetail.SerialNo;
                                serialMap.DetailID = transactionDetail.DetailIID;
                                transactionDetail.ProductSerialMaps.Add(serialMap);
                            }
                        }
                        transactionHead.TransactionDetails.Add(transactionDetail);
                    }
                }

                if (transaction.ShipmentDetails.IsNotNull())
                {
                    transactionHead.TransactionShipments = new List<TransactionShipment>();

                    transactionHead.TransactionShipments.Add(new TransactionShipment()
                    {
                        TransactionShipmentIID = transaction.ShipmentDetails.TransactionShipmentIID,
                        TransactionHeadID = transaction.ShipmentDetails.TransactionHeadID,
                        SupplierIDFrom = transaction.ShipmentDetails.SupplierIDFrom,
                        SupplierIDTo = transaction.ShipmentDetails.SupplierIDTo,
                        ShipmentReference = transaction.ShipmentDetails.ShipmentReference,
                        FreightCarrier = transaction.ShipmentDetails.FreightCareer,
                        ClearanceTypeID = transaction.ShipmentDetails.ClearanceTypeID,
                        AirWayBillNo = transaction.ShipmentDetails.AirWayBillNo,
                        FreightCharges = transaction.ShipmentDetails.FrieghtCharges,
                        BrokerCharges = transaction.ShipmentDetails.BrokerCharges,
                        AdditionalCharges = transaction.ShipmentDetails.AdditionalCharges,
                        Weight = transaction.ShipmentDetails.Weight,
                        NoOfBoxes = transaction.ShipmentDetails.NoOfBoxes,
                        BrokerAccount = transaction.ShipmentDetails.BrokerAccount,
                        Description = transaction.ShipmentDetails.Remarks,
                        CreatedBy = transaction.ShipmentDetails.CreatedBy,
                        UpdatedBy = transaction.ShipmentDetails.UpdatedBy,
                        CreatedDate = transaction.ShipmentDetails.CreatedDate,
                        UpdatedDate = transaction.ShipmentDetails.UpdatedDate,
                        //TimeStamps = transaction.ShipmentDetails.TimeStamps,
                    });
                }

            }//passing entity model data to repository

            transactionHead = productDetailRepository.SaveTransactions(transactionHead);

            // create dto variable for OrderCOntactMap
            var dtoOrderContactMap = new OrderContactMapDTO();
            if (transactionHead.IsNotNull())
            {
                // update LastTransactionNo in [mutual].[DocumentTypes] table
                //DocumentType entity = new MutualBL(_callContext).UpdateLastTransactionNo(Convert.ToInt32(transactionHead.DocumentTypeID), transactionHead.TransactionNo);

                // Save OrderContact Map
                if (transaction.OrderContactMap.IsNotNull())
                {
                    transaction.OrderContactMap.OrderID = transactionHead.HeadIID;
                    dtoOrderContactMap = new OrderBL(_callContext).SaveOrderContactMap(transaction.OrderContactMap);
                }
            }




            // Updated entity data and moved to the dto / ?? Eswar> Is this required to populate again to DTO again Prabha? I think, DTO is already available. Please check and delete below code.
            TransactionDTO transactionDTO = new TransactionDTO();
            if (transactionHead.IsNotNull())
            {
                transactionDTO = new TransactionBL(_callContext).GetTransaction(transactionHead.HeadIID);
            }

            // assign updated OrderContactMap into dto
            if (dtoOrderContactMap.IsNotNull())
            {
                transactionDTO.OrderContactMap = new OrderContactMapDTO();
                transactionDTO.OrderContactMap = dtoOrderContactMap;
            }
            //}

            return transactionDTO;
        }


        public List<Catalog.TransactionDetailDTO> GetTransactionByTransactionID(long transactionID)
        {
            List<Catalog.TransactionDetailDTO> transactionDetails = productDetailRepository.GetTransactionByTransactionID(transactionID);
            return transactionDetails;
        }

        public List<Catalog.TransactionDetailDTO> GetTransactionByTransactionDate(DateTime transactionDate)
        {
            List<Catalog.TransactionDetailDTO> transactionDetails = productDetailRepository.GetTransactionByTransactionDate(transactionDate);
            return transactionDetails;
        }

        public Catalog.ProductPriceDTO CreatePriceInformationDetail(Catalog.ProductPriceDTO productPriceDTO)
        {
            ProductPriceList productPrice = new ProductPriceList();
            productPrice = ProductPriceMapper.Mapper(_callContext).ToEntity(productPriceDTO);
            productPrice = productDetailRepository.CreatePriceInformationDetail(productPrice);
            if (productPriceDTO.ProductPriceListIID == default(long))
            {
                productPriceDTO.BranchMaps.ForEach(x => x.ProductPriceListID = productPrice.ProductPriceListIID);
            }
            PriceSettingsBL priceSettingBL = new PriceSettingsBL(_callContext);
            List<BranchMapDTO> branchMaps = priceSettingBL.SaveBranchMaps(productPriceDTO.BranchMaps);

            productPriceDTO = ProductPriceMapper.Mapper(_callContext).ToDTO(productPrice);
            productPriceDTO.BranchMaps = branchMaps;

            return productPriceDTO;
        }

        public List<ProductPriceSKUDTO> GetProductPriceSKUMaps()
        {
            List<ProductPriceSKUDTO> productPriceSKUMapDTOList = new List<ProductPriceSKUDTO>();

            List<ProductPriceListSKUMap> productPriceListSKUMaps = productDetailRepository.GetProductPriceSKUMaps();

            if (productPriceListSKUMaps != null && productPriceListSKUMaps.Count > 0)
            {
                foreach (ProductPriceListSKUMap productSKUMap in productPriceListSKUMaps)
                {
                    productPriceSKUMapDTOList.Add(new ProductPriceSKUDTO()
                    {
                        ProductPriceListItemMapIID = productSKUMap.ProductPriceListItemMapIID,
                        ProductPriceListID = productSKUMap.ProductPriceListID,
                        ProductSKUID = productSKUMap.ProductSKUID,
                        UnitGroundID = productSKUMap.UnitGroundID,
                        SellingQuantityLimit = productSKUMap.SellingQuantityLimit,
                        Amount = productSKUMap.Amount,
                        SortOrder = productSKUMap.SortOrder,
                    });
                }
            }

            return productPriceSKUMapDTOList;
        }

        public List<ProductPriceSKUDTO> UpdateSKUProductPrice(List<Catalog.ProductPriceSKUDTO> productPriceSKUDTOList)
        {
            List<ProductPriceListSKUMap> productPriceSKUMapList = new List<ProductPriceListSKUMap>();
            List<ProductPriceSKUDTO> productPriceSKUMapsDTO = new List<ProductPriceSKUDTO>();

            if (productPriceSKUDTOList != null && productPriceSKUDTOList.Count > 0)
            {
                foreach (Catalog.ProductPriceSKUDTO productPriceSKUDTO in productPriceSKUDTOList)
                {
                    productPriceSKUMapList.Add(new ProductPriceListSKUMap()
                    {
                        ProductPriceListItemMapIID = productPriceSKUDTO.ProductPriceListItemMapIID,
                        ProductPriceListID = productPriceSKUDTO.ProductPriceListID,
                        ProductSKUID = productPriceSKUDTO.ProductSKUID,
                        SellingQuantityLimit = productPriceSKUDTO.SellingQuantityLimit,
                        Amount = productPriceSKUDTO.Amount,
                        SortOrder = productPriceSKUDTO.SortOrder,
                        PricePercentage = productPriceSKUDTO.PricePercentage
                    });

                }
            }

            List<ProductPriceListSKUMap> saveproductPriceSKUMaps = productDetailRepository.UpdateSKUProductPrice(productPriceSKUMapList);

            if (saveproductPriceSKUMaps != null && saveproductPriceSKUMaps.Count > 0)
            {
                foreach (ProductPriceListSKUMap productPriceSKUMap in saveproductPriceSKUMaps)
                {
                    productPriceSKUMapsDTO.Add(new ProductPriceSKUDTO()
                    {
                        ProductPriceListItemMapIID = productPriceSKUMap.ProductPriceListItemMapIID,
                        ProductPriceListID = productPriceSKUMap.ProductPriceListID,
                        ProductSKUID = productPriceSKUMap.ProductSKUID,
                        SellingQuantityLimit = productPriceSKUMap.SellingQuantityLimit,
                        Amount = productPriceSKUMap.Amount,
                        SortOrder = productPriceSKUMap.SortOrder,
                        PricePercentage = productPriceSKUMap.PricePercentage
                    });

                }
            }

            return productPriceSKUMapsDTO;
        }

        private void AddProductToSupplierPriceList(long productID)
        {
            new ProductDetailRepository().AddProductToSupplierPriceList(productID, _callContext);
        }

        public long UpdateProduct(Catalog.AddProductDTO productDetails)
        {
            var product = new Product();
            product.ProductIID = Convert.ToInt64(productDetails.QuickCreate.ProductIID);
            product.ProductCode = string.Format("{0}{1}", "PC", DateTime.UtcNow.ToString("yyyyMMddHHmmssfff"));
            product.ProductFamilyID = productDetails.QuickCreate.ProductFamilyIID;
            //product.ProductOwnderID = productDetails.QuickCreate.ProductOwnderID;
            product.ProductName = productDetails.QuickCreate.ProductName;
            product.StatusID = Convert.ToByte(productDetails.QuickCreate.StatusIID);
            product.BrandID = productDetails.QuickCreate.BrandIID;
            product.IsOnline = productDetails.QuickCreate.IsOnline;
            product.ProductTypeID = productDetails.QuickCreate.ProductTypeID;
            product.TaxTemplateID = productDetails.QuickCreate.TaxTemplateID;
            product.ProductSKUMaps = new List<ProductSKUMap>();
            product.ProductCategoryMaps = new List<ProductCategoryMap>();
            product.ProductCultureDatas = new List<ProductCultureData>();
            product.UpdateBy = _callContext.LoginID != null ? int.Parse(_callContext.LoginID.Value.ToString()) : (int?)null;
            product.Updated = DateTime.Now;
            var companyID = _callContext.IsNotNull() ? _callContext.CompanyID.HasValue ? _callContext.CompanyID.Value : default(int) : default(int);

            if (product.ProductIID == 0) 
            {
                product.CreatedBy = _callContext.LoginID != null ? int.Parse(_callContext.LoginID.Value.ToString()) : (int?)null;
                product.Created = DateTime.Now;
            }

            if (productDetails.QuickCreate.ProductIID <= 0)
            {
                product.Created = DateTime.Now;
            }
            else
            {
                product.Updated = DateTime.Now;
            }

            if (productDetails.QuickCreate.CultureInfo != null && productDetails.QuickCreate.CultureInfo.Count > 0)
            {
                foreach (var cul in productDetails.QuickCreate.CultureInfo)
                {
                    var culData = new ProductCultureData();
                    culData.CultureID = cul.CultureID;
                    culData.ProductID = product.ProductIID;
                    culData.ProductName = cul.CultureValue;
                    culData.CreatedDate = product.Created;
                    if (product.ProductIID > 0)
                    {
                        culData.UpdatedDate = DateTime.Now;
                    }
                    product.ProductCultureDatas.Add(culData);
                }
            }

            if (productDetails.QuickCreate.DefaultProperties != null && productDetails.QuickCreate.DefaultProperties.Count > 0)
            {
                foreach (var prop in productDetails.QuickCreate.DefaultProperties)
                {
                    var productPropertyMaps = new ProductPropertyMap();
                    productPropertyMaps.ProductID = Convert.ToInt32(productDetails.QuickCreate.ProductIID);
                    productPropertyMaps.PropertyID = prop.PropertyIID;
                    productPropertyMaps.Value = prop.DefaultValue;
                    productPropertyMaps.CreatedDate = product.Created;
                    if (productDetails.QuickCreate.ProductIID > 0)
                    {
                        productPropertyMaps.UpdatedDate = DateTime.Now;
                    }
                    product.ProductPropertyMaps.Add(productPropertyMaps);
                }
            }

            // Manage ProductInventoryConfig for Product
            // NOTE: we don't have any direct relation between  ProductInventoryConfig and Product
            var productInventoryProductConfigMap = new ProductInventoryProductConfigMap();
            if (productDetails.QuickCreate.ProductInventoryConfigDTOs != null)
            {
                // ProductInventorySKUConfigMapDTO
                productInventoryProductConfigMap.ProductInventoryConfig = new ProductInventoryConfig();
                //productInventorySKUConfigMap.ProductInventoryConfigID = sku.ProductInventoryConfigDTOs.ProductInventoryConfigID;
                //productInventorySKUConfigMap.ProductSKUMapID = sku.ProductSKUMapID;

                // ProductInventoryConfigDTO
                productInventoryProductConfigMap.ProductInventoryConfig = ProductInventoryConfigMapper.ToEntity(productDetails.QuickCreate.ProductInventoryConfigDTOs);

                if (product.ProductIID > 0)
                {
                    productInventoryProductConfigMap.ProductInventoryConfig.UpdatedDate = DateTime.Now;
                    //productInventoryProductConfigMap.ProductInventoryConfig.UpdatedBy = (int)_callContext.LoginID;
                }
                else
                {
                    productInventoryProductConfigMap.ProductInventoryConfig.CreatedDate = DateTime.Now;
                    //productInventoryProductConfigMap.ProductInventoryConfig.CreatedBy = (int)_callContext.LoginID;
                }
                product.ProductInventoryProductConfigMaps.Add(productInventoryProductConfigMap);

                if (productDetails.QuickCreate.ProductInventoryConfigDTOs.AllowShippingfor.IsNotNull())
                {
                    foreach (var country in productDetails.QuickCreate.ProductInventoryConfigDTOs.AllowShippingfor)
                    {
                        var productDeliveryCountrySettings = ProductDeliveryCountrySettingMapper.ToEntity(country);
                        product.ProductDeliveryCountrySettings.Add(productDeliveryCountrySettings);
                    }
                }
            }

            if (productDetails.SelectedCategory != null && productDetails.SelectedCategory.Count > 0)
            {
                foreach (var category in productDetails.SelectedCategory)
                {
                    var categoryMap = new ProductCategoryMap();
                    categoryMap.CategoryID = category.CategoryID;
                    categoryMap.CreatedDate = product.Created;
                    categoryMap.IsPrimary = category.IsPrimary;
                    if (product.ProductIID > 0)
                    {
                        categoryMap.UpdatedDate = DateTime.Now;
                    }
                    product.ProductCategoryMaps.Add(categoryMap);
                }
            }

            if (productDetails.SelectedTags.IsNotNull() && productDetails.SelectedTags.Count > 0) //Product tags are adding to productproperty maps
            {
                foreach (var tag in productDetails.SelectedTags)
                {
                    var propertyMap = new ProductPropertyMap();

                    propertyMap.ProductID = Convert.ToInt64(productDetails.QuickCreate.ProductIID);
                    propertyMap.PropertyTypeID = (int)PropertyTypes.ProductTag;
                    propertyMap.PropertyID = Convert.ToInt64(tag.TagIID);

                    if (product.ProductIID > 0)
                        propertyMap.UpdatedDate = DateTime.Now;

                    product.ProductPropertyMaps.Add(propertyMap);
                }
            }

            if (productDetails.SKUMappings != null && productDetails.SKUMappings.Count > 0)
            {
                foreach (var sku in productDetails.SKUMappings)
                {
                   
                    var skuMap = new ProductSKUMap();
                    //skuMap.ProductSKUCultureDatas
                    string productSKuCode = sku.SkuName.Trim().Replace(" ", "-").Replace("_", "").Replace("»", "");
                    productSKuCode = Regex.Replace(productSKuCode, @"[^\d\w\s]", "-");
                    skuMap.ProductSKUCode = productSKuCode;
                    skuMap.SKUName = sku.SkuName;
                    skuMap.Sequence = sku.Sequence;
                    skuMap.ProductPrice = sku.ProductPrice;
                    skuMap.PartNo = sku.PartNumber;
                    skuMap.BarCode = sku.BarCode;
                    skuMap.ProductSKUMapIID = sku.ProductSKUMapID;
                    skuMap.StatusID = sku.StatusID;

                    skuMap.ProductID = product.ProductIID;
                    skuMap.CreatedDate = product.Created;

                    skuMap.ProductPropertyMaps = new List<ProductPropertyMap>();
                    skuMap.ProductImageMaps = new List<ProductImageMap>();
                    skuMap.ProductSKUCultureDatas = new List<ProductSKUCultureData>();

                    foreach (var productSKUCultureInfo in sku.ProductSKUCultureInfo)
                    {
                        var productSKUCultureData = new ProductSKUCultureData();
                        productSKUCultureData.CultureID = productSKUCultureInfo.CultureID;
                        productSKUCultureData.ProductSKUMapID = sku.ProductSKUMapID;
                        productSKUCultureData.ProductSKUName = productSKUCultureInfo.ProductSKUName;
                        skuMap.ProductSKUCultureDatas.Add(productSKUCultureData);
                    }
                    if (sku.ImageMaps != null && sku.ImageMaps.Count > 0)
                    {
                        foreach (var imageMapDTO in sku.ImageMaps)
                        {
                            var imageMap = new ProductImageMap();
                            imageMap.ProductImageTypeID = imageMapDTO.ImageMapID;
                            imageMap.ImageFile = imageMapDTO.ImagePath;
                            imageMap.ProductImageTypeID = imageMapDTO.ProductImageTypeID;
                            imageMap.Sequence = Convert.ToByte(imageMapDTO.Sequence);
                            imageMap.ProductSKUMapID = imageMapDTO.SKUMapID;
                            imageMap.ProductID = imageMapDTO.ProductID;
                            skuMap.ProductImageMaps.Add(imageMap);
                        }
                    }

                    // Product Video Map
                    if (sku.ProductVideoMaps != null && sku.ProductVideoMaps.Count > 0)
                    {
                        foreach (var videoMapDTO in sku.ProductVideoMaps)
                        {
                            var videoMap = new ProductVideoMap();
                            videoMap = ProductVideoMapMapper.ToEntity(videoMapDTO);
                            skuMap.ProductVideoMaps.Add(videoMap);
                        }
                    }

                    skuMap.IsHiddenFromList = sku.IsHiddenFromList;
                    skuMap.HideSKU = sku.HideSKU;

                    skuMap.UpdatedBy = _callContext.LoginID != null ? int.Parse(_callContext.LoginID.Value.ToString()) : (int?)null;
                    skuMap.UpdatedDate = DateTime.Now;

                    if (product.ProductIID == 0)
                    {
                        skuMap.CreatedBy = _callContext.LoginID != null ? int.Parse(_callContext.LoginID.Value.ToString()) : (int?)null;
                        skuMap.CreatedDate = DateTime.Now;
                    }

                    // ProductInventorySKUConfigMapDTO
                    var productInventorySKUConfigMap = new ProductInventorySKUConfigMap();
                    // ProductInventorySKUConfigMapDTO
                    var productDeliveryCountrySettings = new ProductDeliveryCountrySetting();
                    // manage Product SKU Config
                    if (sku.ProductInventoryConfigDTOs.IsNotNull())
                    {
                        productInventorySKUConfigMap.ProductInventoryConfig = new ProductInventoryConfig();
                        //productInventorySKUConfigMap.ProductInventoryConfigID = sku.ProductInventoryConfigDTOs.ProductInventoryConfigID;
                        //productInventorySKUConfigMap.ProductSKUMapID = sku.ProductSKUMapID;

                        // ProductInventoryConfigDTO
                        productInventorySKUConfigMap.ProductInventoryConfig = ProductInventoryConfigMapper.ToEntity(sku.ProductInventoryConfigDTOs);


                        skuMap.ProductInventorySKUConfigMaps.Add(productInventorySKUConfigMap);

                        if (sku.ProductInventoryConfigDTOs.AllowShippingfor.IsNotNull())
                        {
                            foreach (var country in sku.ProductInventoryConfigDTOs.AllowShippingfor)
                            {
                                productDeliveryCountrySettings = ProductDeliveryCountrySettingMapper.ToEntity(country);
                                skuMap.ProductDeliveryCountrySettings.Add(productDeliveryCountrySettings);
                            }
                        }
                    }

                    if (sku.Properties != null && sku.Properties.Count > 0)
                    {
                        foreach (var prop in sku.Properties)
                        {
                            var productPropertyMaps = new ProductPropertyMap();
                            productPropertyMaps.ProductID = Convert.ToInt32(productDetails.QuickCreate.ProductIID);
                            productPropertyMaps.ProductSKUMapID = prop.ProductSKUMapId;
                            productPropertyMaps.PropertyID = prop.PropertyIID;
                            productPropertyMaps.Value = prop.DefaultValue;
                            productPropertyMaps.CreatedDate = product.Created;
                            if (productDetails.QuickCreate.ProductIID > 0)
                            {
                                productPropertyMaps.UpdatedDate = DateTime.Now;
                            }

                            skuMap.ProductPropertyMaps.Add(productPropertyMaps);
                        }
                    }

                    // tags
                    if (sku.ProductInventoryConfigDTOs != null && sku.ProductInventoryConfigDTOs.Tags != null && sku.ProductInventoryConfigDTOs.Tags.Count > 0)
                    {
                        foreach (var tag in sku.ProductInventoryConfigDTOs.Tags)
                        {
                            long tagID;
                            long.TryParse(tag.Key, out tagID);

                            skuMap.ProductSKUTagMaps.Add(new ProductSKUTagMap()
                            {
                                ProductSKUTagID = tagID,
                                CompanyID = companyID,
                                ProductSKUTag = tagID == 0 ? new ProductSKUTag()
                                {
                                    ProductSKUTagIID = tagID,
                                    TagName = tag.Value
                                } : null,
                            });
                        }
                    }

                    if (sku.isDefaultSKU)
                    {
                        product.ProductSKUMaps.Add(skuMap);
                        break;
                    }

                    if (!string.IsNullOrEmpty(sku.SKU))
                    {
                        string[] propertiesMapped = sku.SKU.Split('»');

                        foreach (var propertyMap in propertiesMapped)
                        {
                            foreach (var propertyList in productDetails.QuickCreate.Properties)
                            {
                                var isproperyMapped = false;

                                foreach (var property in propertyList.SelectedProperties)
                                {
                                    if (property.PropertyName == propertyMap)
                                    {
                                        ProductPropertyMap skuDetail = new ProductPropertyMap();
                                        skuDetail.ProductID = skuMap.ProductID;
                                        skuDetail.PropertyID = property.PropertyIID;
                                        //ERRORERROR
                                        skuDetail.PropertyTypeID = (byte)propertyList.PropertyType.PropertyTypeID;
                                        skuMap.ProductPropertyMaps.Add(skuDetail);
                                        skuDetail.CreatedDate = skuMap.CreatedDate;
                                        if (product.ProductIID > 0)
                                        {
                                            skuDetail.UpdatedDate = DateTime.Now;
                                        }
                                        isproperyMapped = true;
                                        break;
                                    }
                                }

                                if (isproperyMapped)
                                {
                                    break;
                                }
                            }
                        }
                    }

                    product.ProductSKUMaps.Add(skuMap);
                }
                MapProductBundlesToEntity(productDetails, product);
            }

            product.SeoMetadata = ToSeoMetadataEntity(productDetails.SeoMetadata, _callContext);

            // Mapped with Multimedia DTO
            MultimediaDTO dtoMultimedia = new MultimediaDTO();
            dtoMultimedia.ImageSourcePath = productDetails.ImageSourceTempPath;
            dtoMultimedia.ImageDestinationPath = productDetails.ImageSourceDesignationPath;
            dtoMultimedia.VideoSourcePath = productDetails.VideoSourceTempPath; 
            dtoMultimedia.VideoDestinationPath = productDetails.VideoSourceDestinationPath;
            ImageCorrectionAndSynching(product);
            long productID = productDetailRepository.UpdateProduct(product, dtoMultimedia,companyID);

            //add the product map to the supplier price lists
            AddProductToSupplierPriceList(productID);
            if(ConfigurationExtensions.GetAppConfigValue<bool>("IsSyncImageWithFtp"))
            {
                SynchToFTPLocation(product);
            }
            return productID;
        }

        void SynchToFTPLocation(Product productDetails)
        {
            try
            {
                FtpClient ftpClient = null;

                var imagePath = Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue("ImagesPhysicalPath") + "Products\\";
                var ftpPath = "Products\\";

                if (productDetails.ProductIID > 0)
                {
                    foreach (var skuMaps in productDetails.ProductSKUMaps)
                    {
                        foreach (var item in skuMaps.ProductImageMaps)
                        {
                            try
                            {
                                if (ftpClient == null)
                                    ftpClient = new FtpClient(ConfigurationExtensions.GetAppConfigValue<string>("ImageSynchFtpServer"),
                                     ConfigurationExtensions.GetAppConfigValue<string>("ImageSynchFtpServerUserID"), ConfigurationExtensions.GetAppConfigValue<string>("ImageSynchFtpServerPassword"));

                                //if not exists create directory
                                var directoryName = Path.GetDirectoryName(Path.Combine(ftpPath, item.ImageFile));
                                if (!ftpClient.DirectoryExists(directoryName))
                                    ftpClient.CreateDirectory(directoryName);

                                //upload file
                                ftpClient.UploadFile(imagePath + item.ImageFile, Path.Combine(ftpPath, item.ImageFile));   
                            }
                            catch (Exception ex) //continue with the next file
                            {
                                //Eduegate.Logger.LogHelper<ProductDetailBL>.Fatal(ex.Message, ex);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        void ImageCorrectionAndSynching(Product productDetails)
        {
            try
            {
                var imagePath = Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValue("ImagesPhysicalPath") + "Products\\";

                if (productDetails.ProductIID > 0)
                {
                    var product = productDetailRepository.GetProductImagesByProductID(productDetails.ProductIID);
                    foreach (var skuMaps in productDetails.ProductSKUMaps)
                    {
                        foreach (var item in skuMaps.ProductImageMaps)
                        {
                            var correctedFileName = Path.Combine(imagePath, item.ImageFile);
                            var actualFileName = "";
                            try
                            {
                                if (!File.Exists(correctedFileName))
                                {
                                    var currentProductImage = product.Where(x => x.ProductSKUMapID == item.ProductSKUMapID && x.ProductImageTypeID == item.ProductImageTypeID && x.Sequence == item.Sequence).FirstOrDefault();
                                    actualFileName = imagePath + currentProductImage.ImageFile;
                                    System.IO.File.Move(actualFileName, correctedFileName);
                                }
                            }
                            catch (Exception) //continue with the next file
                            {
                                //Eduegate.Logger.LogHelper<ProductDetailBL>.Fatal(ex.Message, ex);
                            }
                            
                        }

                    }
                }
            }
            catch (Exception)
            {
            }
        }

        string ImageTypePath(long productImageTypeID,long productID)
        {
            var strImageType = productID.ToString() ;
            switch (productImageTypeID)
            {
                case 8:
                    return strImageType + "\\ListingImage";
                case 3:
                    return strImageType + "\\LargeImage";
                case 1:
                    return strImageType + "\\ThumbnailImage";
                case 2:
                    return strImageType + "\\SmallImage";
                default:
                    return "";
            }
        }
        
        public List<ProductTypeDTO> GetProductTypes()
        {
            var entities = new ProductDetailRepository().GetProductTypes();
            var dtos = new List<ProductTypeDTO>();
            if (entities != null && entities.Count > 0)
            {
                foreach (var entity in entities)
                {
                    dtos.Add(new ProductTypeDTO()
                    {
                        ProductTypeID = entity.ProductTypeID,
                        ProductTypeName = entity.ProductTypeName,
                    });
                }
            }
            return dtos;
        }

        public List<KeyValueDTO> GetProdctSKUTags()
        {
            var dtos = new List<KeyValueDTO>();

            foreach (var entity in new ProductDetailRepository().GetProdctSKUTags())
            {
                dtos.Add(new KeyValueDTO()
                {
                    Key = entity.ProductSKUTagIID.ToString(),
                    Value = entity.TagName,
                });
            }

            return dtos;
        }

        public PropertyTypeDTO SavePropertyType(PropertyTypeDTO propertyTypeDTO)
        {
            // Save PropertyType
            return PropertyTypeMapper.Mapper(_callContext).ToDTO(new ProductDetailRepository().SavePropertyType(PropertyTypeMapper.Mapper(_callContext).ToEntity(propertyTypeDTO)));
        }

        public PropertyTypeDTO GetPropertyType(byte propertyTypeID)
        {
            // Save PropertyType
            return PropertyTypeMapper.Mapper(_callContext).ToDTO(new ProductDetailRepository().GetPropertyType(propertyTypeID));
        }

        private static void MapProductBundlesToEntity(AddProductDTO productDetails, Product product)
        {

            //Product bundles
            if (productDetails.ProductBundles.IsNotNull() && productDetails.ProductBundles.ToProduct.IsNotNull())
            {
                product.ProductBundles = new List<ProductBundle>();

                foreach (var sku in productDetails.SKUMappings)
                {
                    string[] bundleProductMapped = sku.SKU.Split('»');
                    foreach (var productMap in bundleProductMapped)
                    {
                        var toProductID = productDetails.ProductBundles.ToProduct.Where(p => p.ProductName.Trim() == productMap.Trim()).Select(x => x.ProductID).FirstOrDefault().ToString();
                        product.ProductBundles.Add(new ProductBundle()
                        {

                            FromProductID = product.ProductIID != default(long) ? (long)(product.ProductIID) : (long?)null,
                            ToProductID = toProductID != null ? Convert.ToInt32(toProductID) : (long?)null,
                            FromProductSKUMapID = sku.ProductSKUMapID //For product level bundles, we get ProductID 
                        });
                    }
                }
            }

            //SKU bundles
            if (productDetails.SKUBundles.IsNotNull() && productDetails.SKUBundles.ToProduct.IsNotNull())
            {
                product.ProductBundles = new List<ProductBundle>();

                foreach (var sku in productDetails.SKUMappings)
                {
                    string[] bundleProductSKUMapped = sku.SKU.Split('»');

                    foreach (var productSKUMap in bundleProductSKUMapped)
                    {
                        var productMap = productDetails.SKUBundles.ToProduct.Where(p => p.ProductName.Trim() == productSKUMap.Trim()).Select(x => new { x.ProductID, x.ProductSKUMapID }).FirstOrDefault();

                        product.ProductBundles.Add(new ProductBundle()
                        {
                            FromProductID = product.ProductIID != default(long) ? (long)(product.ProductIID) : (long?)null,
                            ToProductID = productMap.ProductID,
                            ToProductSKUMapID = Convert.ToInt32(productMap.ProductSKUMapID),
                            FromProductSKUMapID = sku.ProductSKUMapID
                        });
                    }
                }
            }
        }

        public static SeoMetadata ToSeoMetadataEntity(SeoMetadataDTO dto, CallContext callContext)
        {
            if (dto.IsNotNull())
            {
                var entity = new SeoMetadata()
                {
                    MetaDescription = dto.MetaDescription,
                    MetaKeywords = dto.MetaKeywords,
                    PageTitle = dto.PageTitle,
                    UrlKey = dto.UrlKey,
                    SEOMetadataIID = dto.SEOMetadataIID,
                    //TimeStamps = string.IsNullOrEmpty(dto.TimeStamps) ? null : Convert.FromBase64String(dto.TimeStamps),
                };

                if (dto.SEOMetadataIID == 0)
                {
                    entity.CreatedBy = int.Parse(callContext.LoginID.ToString());
                    entity.CreatedDate = DateTime.Now;
                }

                entity.UpdatedBy = int.Parse(callContext.LoginID.ToString());
                entity.UpdatedDate = DateTime.Now;
                entity.SeoMetadataCultureDatas = new List<SeoMetadataCultureData>();

                foreach (var cultureData in dto.CultureDatas)
                {
                    var cultureInfo = new SeoMetadataCultureData()
                    {
                        CultureID = cultureData.CultureID,
                        MetaDescription = cultureData.MetaDescription,
                        MetaKeywords = cultureData.MetaKeywords,
                        PageTitle = cultureData.PageTitle,
                        SEOMetadataID = cultureData.SEOMetadataID,
                        UrlKey = cultureData.UrlKey,
                        //TimeStamps = string.IsNullOrEmpty(cultureData.TimeStamps) ? null : Convert.FromBase64String(cultureData.TimeStamps),
                    };

                    if (cultureData.SEOMetadataID == 0)
                    {
                        cultureInfo.CreatedBy = int.Parse(callContext.LoginID.ToString());
                        cultureInfo.CreatedDate = DateTime.Now;
                    }

                    cultureInfo.UpdatedBy = int.Parse(callContext.LoginID.ToString());
                    cultureInfo.UpdatedDate = DateTime.Now;
                    entity.SeoMetadataCultureDatas.Add(cultureInfo);
                }

                return entity;
            }
            else return new SeoMetadata();
        }

        public List<CultureDataInfoDTO> GetCultureList()
        {
            return new ReferenceDataBL(_callContext).GetCultureList();
        }

        public List<BrandDTO1> GetBrandList1()
        {
            var brandsList = new List<BrandDTO1>();
            var brands = new List<Brand>();
            brands = productDetailRepository.GetBrandList();
            if (brands != null && brands.Count > 0)
            {
                foreach (var brand in brands)
                {
                    var brandDTO = new BrandDTO1();
                    brandDTO.BrandIID = brand.BrandIID;
                    brandDTO.BrandName = brand.BrandName;
                    brandsList.Add(brandDTO);
                }
            }
            return brandsList;
        }

        public List<BrandDTO> GetBrandList()
        {
            var brandsList = new List<BrandDTO>();
            var brands = new List<Brand>();
            brands = productDetailRepository.GetBrandList();
            if (brands != null && brands.Count > 0)
            {
                foreach (var brand in brands)
                {
                    var brandDTO = new BrandDTO();
                    brandDTO.BrandIID = brand.BrandIID;
                    brandDTO.BrandName = brand.BrandName;
                    brandsList.Add(brandDTO);
                }
            }
            return brandsList;
        }

        public List<KeyValueDTO> SearchBrand(string searchText, int pageSize)
        {
            var brandsList = new List<KeyValueDTO>();
            var brands = new ProductDetailRepository().GetBrandList(searchText, pageSize);

            if (brands.IsNotNull() && brands.Count > 0)
            {
                foreach (var brand in brands)
                {
                    brandsList.Add(new KeyValueDTO()
                    {
                        Key = brand.BrandIID.ToString(),
                        Value = brand.BrandName
                    });
                }
            }
            return brandsList;
        }

        public List<Catalog.ProductFamilyDTO> GetProductFamilies()
        {
            var productFamilyDTOList = new List<Catalog.ProductFamilyDTO>();
            var productFamilies = new List<ProductFamily>();
            productFamilies = productDetailRepository.GetProductFamilies();
            if (productFamilies != null && productFamilies.Count > 0)
            {
                foreach (var productFamily in productFamilies)
                {
                    var productFamilyDTO = new Catalog.ProductFamilyDTO();
                    productFamilyDTO.ProductFamilyIID = productFamily.ProductFamilyIID;
                    productFamilyDTO.FamilyName = productFamily.FamilyName;
                    productFamilyDTOList.Add(productFamilyDTO);
                }
            }
            return productFamilyDTOList;
        }

        public List<KeyValueDTO> SearchProductFamilies(string searchText, int pageSize)
        {
            var productFamilyList = new List<KeyValueDTO>();
            var productFamilies = productDetailRepository.GetProductFamilies(searchText, pageSize);

            if (productFamilies != null && productFamilies.Count > 0)
            {
                foreach (var productFamily in productFamilies)
                {
                    productFamilyList.Add(new KeyValueDTO()
                    {
                        Key = productFamily.ProductFamilyIID.ToString(),
                        Value = productFamily.FamilyName
                    });
                }
            }
            return productFamilyList;
        }

        public Catalog.ProductFamilyDTO GetProductFamily(long familyID)
        {
            return FromFamilyEntity(productDetailRepository.GetProductFamily(familyID)); ;
        }

        public Catalog.ProductFamilyDTO SaveProductFamily(Catalog.ProductFamilyDTO familyDTO)
        {
            var updatedEntity = productDetailRepository.SaveProductFamily(ToFamilyEntity(familyDTO, _callContext));
            return FromFamilyEntity(updatedEntity);
        }

        public static ProductFamilyDTO FromFamilyEntity(ProductFamily entity)
        {
            var dto = new ProductFamilyDTO()
            {
                FamilyName = entity.FamilyName,
                ProductFamilyIID = entity.ProductFamilyIID,
                ProductFamilyTypeID = entity.ProductFamilyTypeID,
                UpdatedBy = entity.UpdatedBy,
                CreatedBy = entity.CreatedBy,
                TimeStamps = Convert.ToBase64String(entity.TimeStamps),
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate
            };
            dto.PropertyTypes = new List<PropertyTypeDTO>();
            foreach (var proptype in entity.ProductFamilyPropertyTypeMaps)
            {
                dto.PropertyTypes.Add(new PropertyTypeDTO()
                {
                    PropertyTypeID = (byte)proptype.PropertyTypeID,
                    PropertyTypeName = proptype.PropertyType.IsNotNull() ? proptype.PropertyType.PropertyTypeName : null,
                });
            }
            dto.Properties = new List<PropertyDTO>();
            foreach (var prop in entity.ProductFamilyPropertyMaps)
            {
                dto.Properties.Add(new PropertyDTO()
                {
                    PropertyIID = prop.Property.PropertyIID,
                    PropertyName = prop.Property.PropertyName,
                });
            }
            return dto;
        }

        public static ProductFamily ToFamilyEntity(ProductFamilyDTO dto, CallContext callContext)
        {
            var entity = new ProductFamily()
            {
                UpdatedDate = DateTime.Now,
                TimeStamps = dto.TimeStamps == null ? null : Convert.FromBase64String(dto.TimeStamps),
                UpdatedBy = int.Parse(callContext.LoginID.ToString()),
                FamilyName = dto.FamilyName,
                ProductFamilyTypeID = dto.ProductFamilyTypeID,
                ProductFamilyIID = dto.ProductFamilyIID
            };

            if (entity.ProductFamilyIID == 0)
            {
                entity.CreatedBy = int.Parse(callContext.LoginID.ToString());
                entity.CreatedDate = DateTime.Now;
            }
            entity.ProductFamilyPropertyTypeMaps = new List<ProductFamilyPropertyTypeMap>();
            foreach (var proptype in dto.PropertyTypes)
            {
                entity.ProductFamilyPropertyTypeMaps.Add(new ProductFamilyPropertyTypeMap()
                {
                    ProductFamilyID = dto.ProductFamilyIID,
                    PropertyTypeID = (byte)proptype.PropertyTypeID,
                });
            }

            entity.ProductFamilyPropertyMaps = new List<ProductFamilyPropertyMap>();

            foreach (var prop in dto.Properties)
            {
                entity.ProductFamilyPropertyMaps.Add(new ProductFamilyPropertyMap()
                {
                    ProductFamilyID = dto.ProductFamilyIID,
                    ProductPropertyID = prop.PropertyIID,
                });
            }

            return entity;
        }

        public PropertyDTO GetProperty(long propertyID)
        {
            return FromPropertyEntity(productDetailRepository.GetProperty(propertyID));
        }

        public PropertyDTO SaveProperty(PropertyDTO dto)
        {
            var updatedEntity = productDetailRepository.SaveProperty(ToPropertyEntity(dto, _callContext));
            return FromPropertyEntity(updatedEntity);
        }

        public static PropertyDTO FromPropertyEntity(Property entity)
        {
            return new PropertyDTO()
            {
                PropertyIID = entity.PropertyIID,
                PropertyName = entity.PropertyName,
                DefaultValue = entity.DefaultValue,
                PropertyTypeID = entity.PropertyTypeID,
                IsUnqiue = entity.IsUnqiue,
                UpdatedBy = entity.UpdatedBy,
                CreatedBy = entity.CreatedBy,
                TimeStamps = Convert.ToBase64String(entity.TimeStamps),
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate
            };
        }

        public static Property ToPropertyEntity(PropertyDTO dto, CallContext callContext)
        {
            var entity = new Property()
            {
                UpdatedDate = DateTime.Now,
                TimeStamps = dto.TimeStamps == null ? null : Convert.FromBase64String(dto.TimeStamps),
                UpdatedBy = int.Parse(callContext.LoginID.ToString()),
                PropertyName = dto.PropertyName,
                PropertyTypeID = dto.PropertyTypeID,
                PropertyIID = dto.PropertyIID,
                DefaultValue = dto.DefaultValue,
                IsUnqiue = dto.IsUnqiue,
            };

            if (entity.PropertyIID == 0)
            {
                entity.CreatedBy = int.Parse(callContext.LoginID.ToString());
                entity.CreatedDate = DateTime.Now;
            }

            return entity;
        }

        public List<Catalog.ProductStatusDTO> GetProductStatus()
        {
            return Mappers.Catalog.ProductStatusMapper.Mapper(_callContext).ToDTO(productDetailRepository.GetProductStatus());
        }

        public List<Catalog.PropertyTypeDTO> GetProductPropertyTypes(decimal productFamilyIID)
        {
            var propertyTypeDTOList = new List<Catalog.PropertyTypeDTO>();
            var propertyType = new List<PropertyType>();
            propertyType = productDetailRepository.GetProductPropertyTypes(productFamilyIID);
            if (propertyType != null && propertyType.Count > 0)
            {
                foreach (var prop in propertyType)
                {
                    var propDTO = new Catalog.PropertyTypeDTO();
                    //propDTO.CultureID = prop.CultureID;
                    propDTO.PropertyTypeID = prop.PropertyTypeID;
                    propDTO.PropertyTypeName = prop.PropertyTypeName;
                    propertyTypeDTOList.Add(propDTO);
                }
            }
            return propertyTypeDTOList;
        }

        public List<PropertyDTO> GetPropertiesByPropertyTypeID(decimal propertyTypeIID)
        {
            var propertyDTOList = new List<PropertyDTO>();
            var property = new List<Property>();
            property = productDetailRepository.GetPropertiesByPropertyTypeID(propertyTypeIID);
            if (property != null && property.Count > 0)
            {
                foreach (var prop in property)
                {
                    var propDTO = new PropertyDTO();
                    propDTO.PropertyIID = prop.PropertyIID;
                    propDTO.PropertyName = prop.PropertyName;
                    propertyDTOList.Add(propDTO);
                }
            }
            return propertyDTOList;
        }

        public List<ProductCategoryDTO> GetCategoryList(bool isReporting=false, string searchText = "")
        {
            var categoriesDTOList = new List<ProductCategoryDTO>();
            var categories = new List<Category>();
            categories = isReporting == false ? new CategoryRepository().GetCategoryList(_callContext.LanguageCode, searchText) : new CategoryRepository().GetCategoryList(_callContext.LanguageCode, searchText, isReporting);
            if (categories != null && categories.Count > 0)
            {
                foreach (var category in categories)
                {
                    var categoryDTO = new ProductCategoryDTO();
                    categoryDTO.CategoryName = category.CategoryName;
                    categoryDTO.CategoryID = category.CategoryIID;
                    categoriesDTOList.Add(categoryDTO);
                }
            }
            return categoriesDTOList;
        }

        public AddProductDTO GetProduct(long productIID)
        {
            var productDetails = new AddProductDTO();
            var product = new Product();

            product = productDetailRepository.GetProduct(productIID);
            if (product != null)
            {
                var availablePropertyTypes = new List<PropertyType>();
                productDetails.QuickCreate = new QuickCreateDTO();
                productDetails.QuickCreate.CultureInfo = new List<CultureDataInfoDTO>();
                productDetails.QuickCreate.DefaultProperties = new List<PropertyDTO>();
                productDetails.SelectedCategory = new List<ProductCategoryDTO>();
                productDetails.SKUMappings = new List<SKUDTO>();
                productDetails.QuickCreate.Properties = new List<ProductPropertiesTypeValuesDTO>();
                productDetails.SelectedTags = new List<ProductTagDTO>();
                //productDetails.QuickCreate.Brand = new BrandDTO();
                //productDetails.QuickCreate.ProductFamily = new List<ProductFamilyDTO>();

                productDetails.QuickCreate.ProductIID = product.ProductIID;
                productDetails.QuickCreate.ProductCode = product.ProductCode;
                productDetails.QuickCreate.ProductFamilyIID = product.ProductFamilyID;
                //productDetails.QuickCreate.ProductOwnderID = product.ProductOwnderID;
                productDetails.QuickCreate.ProductName = product.ProductName;
                productDetails.QuickCreate.StatusIID = product.StatusID;
                productDetails.QuickCreate.StatusName = product.ProductStatu.IsNotNull() ? product.ProductStatu.StatusName : null;
                productDetails.QuickCreate.BrandIID = product.BrandID;
                productDetails.QuickCreate.ProductTypeID = product.ProductTypeID;
                productDetails.QuickCreate.TaxTemplateID = product.TaxTemplateID;

                if (productDetails.QuickCreate.BrandIID.IsNotNull())
                    productDetails.QuickCreate.BrandName = product.Brand != null ? product.Brand.BrandName : null;
                if (productDetails.QuickCreate.ProductFamilyIID.IsNotNull())
                    productDetails.QuickCreate.ProductFamilyName = product.ProductFamily != null ? product.ProductFamily.FamilyName : null;

                productDetails.QuickCreate.IsOnline = product.IsOnline;
                var availablePropertyMaps = GetProperitesByProductFamilyID(Convert.ToInt32(product.ProductFamilyID));
                // Product Inventory Product Config
                productDetails.QuickCreate.ProductInventoryConfigDTOs = new ProductInventoryConfigDTO();
                if (product.ProductInventoryProductConfigMaps.IsNotNull())
                {
                    // it would be always a single row 
                    foreach (var item in product.ProductInventoryProductConfigMaps)
                    {
                        if (item.ProductInventoryConfig.IsNotNull())
                        {
                            productDetails.QuickCreate.ProductInventoryConfigDTOs = ProductInventoryConfigMapper.ToDto(item.ProductInventoryConfig);
                            productDetails.QuickCreate.ProductInventoryConfigDTOs.AllowShippingfor = new List<ProductDeliveryCountrySettingDTO>();

                            if (product.ProductDeliveryCountrySettings.IsNotNull() && product.ProductDeliveryCountrySettings.Count > 0)
                            {
                                productDetails.QuickCreate.ProductInventoryConfigDTOs.AllowShippingfor =
                                    product.ProductDeliveryCountrySettings.Where(x => x.ProductSKUMapID == null)
                                    .Select(x => ProductDeliveryCountrySettingMapper.ToDTO(x)).ToList();
                            }
                        }
                    }
                }

                if (product.ProductFamilyID != null)
                {
                    availablePropertyTypes = productDetailRepository.GetProductPropertyTypes(Convert.ToInt32(product.ProductFamilyID));
                }
                if (product.ProductCultureDatas != null && product.ProductCultureDatas.Count > 0)
                {
                    var cultureInfo = new ReferenceDataBL(_callContext).GetCultureList();
                    foreach (var cul in product.ProductCultureDatas)
                    {
                        var culData = new CultureDataInfoDTO();
                        culData.CultureID = cul.CultureID;
                        culData.CultureValue = cul.ProductName;
                        culData.CultureName = (from culName in cultureInfo
                                               where culName.CultureID == culData.CultureID
                                               select culName.CultureName).FirstOrDefault();
                        productDetails.QuickCreate.CultureInfo.Add(culData);
                    }
                }

                if (product.ProductCategoryMaps != null && product.ProductCategoryMaps.Count > 0)
                {
                    var categoryRep = new CategoryRepository();
                    foreach (var category in product.ProductCategoryMaps)
                    {
                        var languageCode = _callContext.IsNotNull() ? _callContext.LanguageCode : string.Empty;
                        var categoryMap = new ProductCategoryDTO();
                        categoryMap.CategoryID = category.CategoryID != null ? Convert.ToInt32(category.CategoryID) : 0;

                        var categoryDetail = categoryRep.GetCategoryByID(category.CategoryID.Value, languageCode);


                        var categoryName = category.CategoryID.HasValue ? categoryRep.GetCategoryByID(category.CategoryID.Value, languageCode).CategoryName : string.Empty;
                        categoryMap.CategoryName = categoryName;

                        productDetails.SelectedCategory.Add(categoryMap);
                    }
                }

                if (product.ProductPropertyMaps != null && product.ProductPropertyMaps.Count > 0)
                {
                    string propertyTypeID = ((byte)PropertyTypes.ProductTag).ToString();
                    var availableTags = new UtilityRepository().GetProperties(propertyTypeID);

                    foreach (var propertyMap in product.ProductPropertyMaps.Where(x => x.PropertyTypeID == (int)PropertyTypes.ProductTag).ToList())
                    {
                        var productTagDTO = new ProductTagDTO();
                        productTagDTO.TagIID = propertyMap.PropertyID != null ? Convert.ToInt32(propertyMap.PropertyID) : 0;

                        var tagObject = (from tag in availableTags
                                         where tag.PropertyIID == propertyMap.PropertyID
                                         select tag).FirstOrDefault();

                        if (tagObject.IsNotNull())
                            productTagDTO.TagName = tagObject.PropertyName;

                        productDetails.SelectedTags.Add(productTagDTO);
                    }
                }


                if (product.ProductSKUMaps != null && product.ProductSKUMaps.Count > 0)
                {
                    var propertyMapList = new List<ProductPropertyMap>();

                    foreach (var sku in product.ProductSKUMaps)
                    {
                        var skuDTO = new SKUDTO();

                        skuDTO.ProductSKUCode = sku.ProductSKUCode;
                        skuDTO.Sequence = sku.Sequence;
                        skuDTO.ProductPrice = sku.ProductPrice;
                        skuDTO.PartNumber = sku.PartNo;
                        skuDTO.BarCode = sku.BarCode;
                        skuDTO.ProductID = product.ProductIID;
                        skuDTO.ProductSKUMapID = sku.ProductSKUMapIID;
                        skuDTO.SKU = sku.SKUName;
                        skuDTO.SkuName = sku.SKUName;
                        skuDTO.Properties = new List<PropertyDTO>();
                        skuDTO.StatusID = sku.StatusID;
                        skuDTO.ProductSKUCultureInfo = new List<ProductSKUCultureDataDTO>();

                        var productSKUCultureDataDTO = new ProductSKUCultureDataDTO()
                        {
                            CultureID = 1,
                            ProductSKUMapID = sku.ProductSKUMapIID,
                            ProductSKUName = sku.SKUName
                        };
                        skuDTO.ProductSKUCultureInfo.Add(productSKUCultureDataDTO);
                       
                        foreach (var skuCultureInfo in sku.ProductSKUCultureDatas)
                        {
                            var productSKUCultureDataDTOList = new ProductSKUCultureDataDTO()
                            {
                                CultureID = skuCultureInfo.CultureID,
                                ProductSKUMapID = skuCultureInfo.ProductSKUMapID,
                                ProductSKUName = skuCultureInfo.ProductSKUName
                            };
                            skuDTO.ProductSKUCultureInfo.Add(productSKUCultureDataDTOList);
                        };

                        foreach (var propertymap in sku.ProductPropertyMaps)
                        {

                            if (propertymap.Value == null)
                            {
                                skuDTO.SKU += "»" + propertymap.PropertyID;

                                propertyMapList.Add(propertymap);
                            }
                        }

                        foreach (var productPropertyMap in availablePropertyMaps)
                        {
                            var defaultPropertyMap = sku.ProductPropertyMaps.Where(x => x.PropertyID == productPropertyMap.PropertyIID).FirstOrDefault();

                            var propertyDTO = new PropertyDTO();
                            propertyDTO.DefaultValue = defaultPropertyMap != null ? defaultPropertyMap.Value : productPropertyMap.DefaultValue;
                            propertyDTO.PropertyIID = Convert.ToInt32(productPropertyMap.PropertyIID);
                            propertyDTO.PropertyName = productPropertyMap.PropertyName;
                            propertyDTO.ProductSKUMapId = productPropertyMap.ProductSKUMapId;
                            skuDTO.Properties.Add(propertyDTO);
                        }

                        skuDTO.ImageMaps = new List<SKUImageMapDTO>();

                        if (sku.ProductImageMaps != null && sku.ProductImageMaps.Count > 0)
                        {
                            var largeTypeMaps = sku.ProductImageMaps.Where(x => x.ProductImageTypeID == (int)Helper.ImageType.LargeImage).ToList();
                            foreach (var imageMap in largeTypeMaps)
                            {
                                var imageMapDTO = new Catalog.SKUImageMapDTO();
                                imageMapDTO.ImageMapID = imageMap.ProductImageTypeID;
                                imageMapDTO.ImagePath = imageMap.ImageFile;
                                imageMapDTO.ProductImageTypeID = imageMap.ProductImageTypeID;
                                imageMapDTO.Sequence = Convert.ToInt32(imageMap.Sequence);
                                imageMapDTO.ImageName = System.IO.Path.GetFileName(imageMap.ImageFile);
                                imageMapDTO.SKUMapID = imageMap.ProductSKUMapID;
                                imageMapDTO.ProductID = imageMap.ProductID;
                                skuDTO.ImageMaps.Add(imageMapDTO);
                            }
                        }

                        // Product Video Maps
                        skuDTO.ProductVideoMaps = new List<ProductVideoMapDTO>();
                        if (sku.ProductVideoMaps != null && sku.ProductVideoMaps.Count > 0)
                        {
                            foreach (var videoMap in sku.ProductVideoMaps)
                            {
                                var videoMapDTO = new Catalog.ProductVideoMapDTO();
                                videoMapDTO = ProductVideoMapMapper.ToDto(videoMap);
                                skuDTO.ProductVideoMaps.Add(videoMapDTO);
                            }
                        }

                        // Product Inventory SKU Config
                        if (sku.ProductInventorySKUConfigMaps.IsNotNull())
                        {
                            // always would be single row
                            foreach (var item in sku.ProductInventorySKUConfigMaps)
                            {
                                skuDTO.ProductInventoryConfigDTOs = new ProductInventoryConfigDTO();
                                skuDTO.ProductInventoryConfigDTOs = ProductInventoryConfigMapper.ToDto(item.ProductInventoryConfig);
                                skuDTO.ProductInventoryConfigDTOs.EmployeeName = new EmployeeRepository().GetEmployeeName(item.ProductInventoryConfig.EmployeeID > 0 ? item.ProductInventoryConfig.EmployeeID.Value : default(long));
                                skuDTO.ProductInventoryConfigDTOs.AllowShippingfor = new List<ProductDeliveryCountrySettingDTO>();

                                if (sku.ProductDeliveryCountrySettings.IsNotNull())
                                {
                                    foreach (var country in sku.ProductDeliveryCountrySettings)
                                    {
                                        skuDTO.ProductInventoryConfigDTOs.AllowShippingfor.Add(ProductDeliveryCountrySettingMapper.ToDTO(country));
                                    }
                                }
                                skuDTO.ProductInventoryConfigDTOs.IsHiddenFromList = sku.IsHiddenFromList.IsNotNull() ? Convert.ToBoolean(sku.IsHiddenFromList) : false;
                                skuDTO.ProductInventoryConfigDTOs.HideSKU = sku.HideSKU.IsNotNull() ? Convert.ToBoolean(sku.HideSKU) : false;
                            }
                        }

                        if (skuDTO.ProductInventoryConfigDTOs == null)
                        {
                            skuDTO.ProductInventoryConfigDTOs = new ProductInventoryConfigDTO();
                        }

                        skuDTO.ProductInventoryConfigDTOs.Tags = new List<KeyValueDTO>();
                        var companyID = _callContext.IsNotNull() ? _callContext.CompanyID.HasValue ? _callContext.CompanyID.Value : default(int) : default(int);

                        foreach (var tag in sku.ProductSKUTagMaps.Where(x=>x.CompanyID == companyID))
                        {
                            skuDTO.ProductInventoryConfigDTOs.Tags.Add(new KeyValueDTO() { Key = tag.ProductSKUTagID.ToString(), Value = tag.ProductSKUTag.TagName });
                        }

                        productDetails.SKUMappings.Add(skuDTO);
                    }

                    foreach (var property in availablePropertyTypes)
                    {
                        var productPropertiesTypeValuesDTO = new ProductPropertiesTypeValuesDTO();

                        productPropertiesTypeValuesDTO.PropertyType = new PropertyTypeDTO();
                        var availableProperties = productDetailRepository.GetPropertiesByPropertyTypeID(Convert.ToDecimal(property.PropertyTypeID));

                        productPropertiesTypeValuesDTO.PropertyType.PropertyTypeID = property.PropertyTypeID;
                        productPropertiesTypeValuesDTO.PropertyType.PropertyTypeName = property.PropertyTypeName;

                        productPropertiesTypeValuesDTO.SelectedProperties = new List<PropertyDTO>();

                        var selectedProperties = (from propertyDistinctList in propertyMapList.GroupBy(test => test.PropertyID).Select(grp => grp.FirstOrDefault())
                                                  where propertyDistinctList.PropertyTypeID == property.PropertyTypeID
                                                  select propertyDistinctList).ToList();

                        if (selectedProperties != null && selectedProperties.Count > 0)
                        {
                            foreach (var selectedProperty in selectedProperties)
                            {
                                var propertyDTO = new PropertyDTO();
                                propertyDTO.PropertyIID = Convert.ToInt32(selectedProperty.PropertyID);
                                propertyDTO.PropertyTypeID = Convert.ToByte(selectedProperty.PropertyTypeID);

                                if (availableProperties != null)
                                {
                                    var propName = (from prop in availableProperties
                                                    where prop.PropertyIID == propertyDTO.PropertyIID
                                                    select prop).FirstOrDefault();
                                    propertyDTO.PropertyName = propName == null ? string.Empty : propName.PropertyName;
                                }

                                productPropertiesTypeValuesDTO.SelectedProperties.Add(propertyDTO);
                            }
                        }
                        productDetails.QuickCreate.Properties.Add(productPropertiesTypeValuesDTO);
                    }
                }

                var defaultPropertyMaps = product.ProductPropertyMaps.Where(x => x.ProductID == product.ProductIID && x.ProductSKUMapID == null).ToList();
                foreach (var productPropertyMap in availablePropertyMaps)
                {
                    var propertyDTO = new PropertyDTO();
                    var isDefaultValueAvailable = defaultPropertyMaps != null && defaultPropertyMaps.Count > 0 ? defaultPropertyMaps.Where(x => x.PropertyID == productPropertyMap.PropertyIID).FirstOrDefault() : null;
                    propertyDTO.DefaultValue = isDefaultValueAvailable != null ? isDefaultValueAvailable.Value : productPropertyMap.DefaultValue;
                    propertyDTO.PropertyIID = Convert.ToInt32(productPropertyMap.PropertyIID);
                    propertyDTO.PropertyName = productPropertyMap.PropertyName;
                    productDetails.QuickCreate.DefaultProperties.Add(propertyDTO);
                }

                if (product.ProductToProductMaps != null && product.ProductToProductMaps.Count > 0)
                {
                    productDetails.ProductMaps = new ProductToProductMapDTO();
                    productDetails.ProductMaps.ToProduct = new List<ProductMapDTO>();
                    foreach (var productMaps in product.ProductToProductMaps)
                    {
                        var productMapsDTO = new ProductMapDTO();
                        productMapsDTO.ProductToProductMapID = productMaps.ProductToProductMapIID;
                        productMapsDTO.ProductID = Convert.ToInt32(productMaps.ProductIDTo);
                        productMapsDTO.ProductName = productDetailRepository.GetProductNameByID(productMapsDTO.ProductID);
                        productMapsDTO.SalesRelationShipType = productMaps.SalesRelationTypeID != null ? Convert.ToInt32(productMaps.SalesRelationTypeID) : 0;
                        productDetails.ProductMaps.ToProduct.Add(productMapsDTO);
                    }
                }

                productDetails.SeoMetadata = ToMetadataDTO(product.SeoMetadata);
                MapProductBundlesToDTO(productDetails, product);
                MapSKUNamesFromProductBundle(productDetails, product);

            }

            return productDetails;
        }

        public static SeoMetadataDTO ToMetadataDTO(SeoMetadata entity)
        {
            if (entity == null) return null;

            var metadata = new SeoMetadataDTO()
            {
                SEOMetadataIID = entity.SEOMetadataIID,
                MetaDescription = entity.MetaDescription,
                UrlKey = entity.UrlKey,
                MetaKeywords = entity.MetaKeywords,
                CreatedBy = entity.CreatedBy,
                UpdatedBy = entity.UpdatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
                PageTitle = entity.PageTitle,
                //TimeStamps = Convert.ToBase64String(entity.TimeStamps),
            };

            foreach (var data in entity.SeoMetadataCultureDatas)
            {
                metadata.CultureDatas.Add(new SeoMetadataCultureDataDTO()
                {
                    CultureID = data.CultureID,
                    MetaDescription = data.MetaDescription,
                    MetaKeywords = data.MetaKeywords,
                    PageTitle = data.PageTitle,
                    SEOMetadataID = data.SEOMetadataID,
                    UrlKey = data.UrlKey,
                    ///TimeStamps = Convert.ToBase64String(data.TimeStamps),
                });
            }

            return metadata;
        }

        private void MapProductBundlesToDTO(AddProductDTO productDetails, Product product)
        {
            //Bundles
            if (product.ProductBundles != null)
            {
                //Product Bundles
                if (product.ProductBundles.Where(x => x.ToProductID != null).Any() && product.ProductBundles.Where(x => x.ToProductSKUMapID == null).Any())
                {
                    productDetails.ProductBundles = new ProductToProductMapDTO();
                    productDetails.ProductBundles.ToProduct = new List<ProductMapDTO>();

                    foreach (ProductBundle bundle in product.ProductBundles.Where(x => x.ToProductID != null))
                    {
                        var productName = productDetailRepository.GetProductNameByID((long)bundle.ToProductID);

                        productDetails.ProductBundles.ToProduct.Add(new ProductMapDTO()
                        {
                            ProductID = (long)bundle.ToProductID,
                            ProductName = productName
                        });

                    }
                }
                //SKU Bundles
                if (product.ProductBundles.Where(x => x.ToProductSKUMapID != null).Any())
                {
                    productDetails.SKUBundles = new ProductToProductMapDTO();
                    productDetails.SKUBundles.ToProduct = new List<ProductMapDTO>();
                    var distinctBundleList = product.ProductBundles.Where(x => x.ToProductSKUMapID != null).GroupBy(y => y.ToProductSKUMapID).Select(grp => grp.FirstOrDefault()).ToList();
                    foreach (ProductBundle bundle in distinctBundleList)
                    {
                        productDetails.SKUBundles.ToProduct.Add(new ProductMapDTO()
                        {
                            ProductID = bundle.ToProductID.IsNotNull() ? (long)bundle.ToProductID : default(long),
                            ProductName = productDetailRepository.GetProductAndSKUNameByID((long)bundle.ToProductSKUMapID),
                            ProductSKUMapID = (long)bundle.ToProductSKUMapID
                        });
                    }
                }
            }
        }

        private void MapSKUNamesFromProductBundle(AddProductDTO productDetails, Product product)
        {
            if (productDetails.ProductBundles != null && productDetails.ProductBundles.ToProduct != null && productDetails.ProductBundles.ToProduct.Count > 0)
            {
                var SKUName = "";
                foreach (var bundle in productDetails.ProductBundles.ToProduct)
                {

                    if (SKUName == "")
                    {
                        SKUName = bundle.ProductName;
                    }
                    else
                    {
                        SKUName = SKUName + " » " + bundle.ProductName;
                    }
                }
                productDetails.SKUMappings[0].SKU = SKUName;
            }

            if (product.ProductBundles.Where(x => x.ToProductSKUMapID != null).Any())
            {
                foreach (var productSKU in productDetails.SKUMappings)
                {
                    var SKUName = "";

                    var mappedSKUBundleList = product.ProductBundles.Where(x => x.FromProductSKUMapID == productSKU.ProductSKUMapID).ToList();

                    foreach (var skuBundle in mappedSKUBundleList)
                    {
                        var productSKUName = productDetails.SKUBundles.ToProduct.Where(x => x.ProductSKUMapID == skuBundle.ToProductSKUMapID).Select(y => y.ProductName).FirstOrDefault().ToString();
                        if (SKUName == "")
                        {
                            SKUName = productSKUName;
                        }
                        else
                        {
                            SKUName = SKUName + " » " + productSKUName;
                        }
                    }

                    productSKU.SKU = SKUName;
                }
            }
        }

        public List<PropertyDTO> GetProperitesByProductFamilyID(long productFamilyID)
        {
            var properties = new List<Property>();
            var propertyDTOList = new List<PropertyDTO>();
            properties = productDetailRepository.GetProperitesByProductFamilyID(productFamilyID);
            if (properties != null && properties.Count > 0)
            {
                foreach (var property in properties)
                {
                    var propertyDTO = new PropertyDTO();
                    propertyDTO.PropertyIID = property.PropertyIID;
                    propertyDTO.PropertyName = property.PropertyName;

                    propertyDTOList.Add(propertyDTO);
                }
            }
            return propertyDTOList;
        }

        public List<ProductDTO> GetSuggestedProduct(long productID, CallContext context)
        {
            //List<SearchResult> searchResults = productDetailRepository.GetSuggestedProduct(productID);
            //List<SearchResultDTO> searchResultsDTO = searchResults.Select(x => SearchResultMapper.ToSearchResultDTOMap(x)).ToList();
            ////List<SearchResultDTO> searchResultsDT = searchResults.ConvertToDTO().ToList();
            //return searchResultsDTO;
            // get the converted price 
            decimal ConvertedPrice = UtilityRepository.GetCurrencyPrice(context);

            var lists = productDetailRepository.GetSuggestedProduct(productID);
            List<ProductDTO> list = new List<ProductDTO>();

            lists.ForEach(x =>
            {
                list.Add(new ProductDTO
                {
                    ProductID = x.ProductIID,
                    SKUID = x.SKUID,
                    ProductCode = x.ProductCode,
                    ProductName = x.ProductName,
                    ProductPrice = Eduegate.Framework.Helper.Utility.FormatDecimal(Convert.ToDecimal(x.ProductPrice) * ConvertedPrice, 3),
                    ImageFile = ImageRootUrl + (x.ImageFile.IsNull() ? string.Empty : x.ImageFile.Replace("\\", "/")),
                    DiscountedPrice = x.DiscountedPrice.IsNull() ? "0" : Eduegate.Framework.Helper.Utility.FormatDecimal(Convert.ToDecimal(x.DiscountedPrice) * ConvertedPrice, 3),
                    HasStock = x.HasStock,
                    BrandIID = x.BrandIID,
                    BrandName = x.BrandName
                });
            });

            return list;
        }

        public List<ProductDTO> GetProductListBySearchText(string searchtext, long excludeProductFamilyID)
        {
            var productList = productDetailRepository.GetProductListBySearchText(searchtext, excludeProductFamilyID);
            var productListDTO = new List<ProductDTO>();

            foreach (var product in productList)
            {
                var productDTO = new ProductDTO();
                productDTO.ProductID = product.ProductIID;
                productDTO.ProductName = product.ProductName;

                productListDTO.Add(productDTO);
            }
            return productListDTO;
        }

        public List<ProductDTO> GetProductListByCategoryID(long categoryID)
        {
            var productList = productDetailRepository.GetProductListByCategoryID(categoryID);
            var productListDTO = new List<ProductDTO>();
            

            foreach (var product in productList)
            {
                var productDTO = new ProductDTO();
                productDTO.ProductID = product.ProductIID;
                productDTO.ProductName = product.ProductName;
                productListDTO.Add(productDTO);
            }
            return productListDTO;
        }

        public bool SaveProductMaps(ProductToProductMapDTO productMaps, CallContext context)
        {
            bool isUpdated = false;

            var productMapsList = new List<ProductToProductMap>();

            if (productMaps != null)
            {
                if (productMaps.SalesRelationshipTypes != null && productMaps.SalesRelationshipTypes.Count > 0)
                {
                    foreach (var salesRelationship in productMaps.SalesRelationshipTypes)
                    {
                        if (salesRelationship.ToProducts.IsNotNull() && salesRelationship.ToProducts.Count > 0)
                        {
                            foreach (var item in salesRelationship.ToProducts)
                            {
                                var productMap = new ProductToProductMap();

                                if (item.IsNotNull())
                                {
                                    productMap.ProductToProductMapIID = item.ProductToProductMapID;
                                    productMap.ProductID = Convert.ToInt32(productMaps.FromProduct.ProductID);
                                    productMap.ProductIDTo = item.ProductID;
                                    productMap.SalesRelationTypeID = Convert.ToByte(salesRelationship.SalesRelationTypeID);
                                    productMap.CreatedDate = DateTime.Now;
                                    productMap.CreatedBy = Convert.ToInt32(context.LoginID);
                                    productMapsList.Add(productMap);
                                }
                            }
                        }
                        else
                        {
                            if (productMaps.FromProduct.IsNotNull())
                            {
                                var productMap = new ProductToProductMap();
                                productMap.ProductID = Convert.ToInt32(productMaps.FromProduct.ProductID);
                                productMapsList.Add(productMap);
                            }
                        }
                    }
                }

            }

            isUpdated = productDetailRepository.SaveProductMaps(productMapsList, context);
            return isUpdated;
        }

        public ProductToProductMapDTO GetProductMaps(long productID)
        {
            var productToProductMapDTO = new ProductToProductMapDTO();
            var productMaps = productDetailRepository.GetProductMaps(productID);

            if (productMaps != null && productMaps.Count > 0)
            {
                productToProductMapDTO.ToProduct = new List<ProductMapDTO>();
                productToProductMapDTO.FromProduct = new ProductMapDTO();
                productToProductMapDTO.FromProduct.ProductID = Convert.ToInt32(productMaps[0].ProductID);
                productToProductMapDTO.FromProduct.ProductName = productDetailRepository.GetProductNameByID(productID);

                foreach (var maps in productMaps)
                {
                    var productMapsDTO = new ProductMapDTO();
                    productMapsDTO.ProductToProductMapID = maps.ProductToProductMapIID;
                    productMapsDTO.ProductID = Convert.ToInt32(maps.ProductIDTo);
                    productMapsDTO.ProductName = productDetailRepository.GetProductNameByID(productMapsDTO.ProductID);
                    productMapsDTO.SalesRelationShipType = maps.SalesRelationTypeID != null ? Convert.ToInt32(maps.SalesRelationTypeID) : 0;
                    productToProductMapDTO.ToProduct.Add(productMapsDTO);
                }
            }

            return productToProductMapDTO;
        }
        
        public bool RemoveProductPriceListSKUMaps(long id)
        {
            return productDetailRepository.RemoveProductPriceListSKUMaps(id);
        }

        public ProductPriceDTO GetProductPriceListDetail(long id)
        {
            ProductPriceList obj = productDetailRepository.GetProductPriceListDetail(id, _callContext.CompanyID.HasValue ? (int)_callContext.CompanyID.Value : default(int));
            ProductPriceDTO dto = ProductPriceMapper.Mapper(_callContext).ToDTO(obj);
            dto.BranchMaps = new List<BranchMapDTO>();
            if (obj.ProductPriceListBranchMaps != null)
            {
                foreach (ProductPriceListBranchMap branchMap in obj.ProductPriceListBranchMaps)
                    dto.BranchMaps.Add(BranchMapMapper.Mapper(_callContext).ToDTO(branchMap));
            }
            return dto;
        }

        public List<ProductPriceSKUDTO> GetProductPriceSKU(long id)
        {
            List<ProductPriceSKU> list = productDetailRepository.GetProductPriceSKU(id);
            List<ProductPriceSKUDTO> dtoList = list.Select(x => ProductPriceSKUMapper.Mapper.ToDTO(x)).ToList();
            return dtoList;
        }
        
        public TransactionDTO GetTransaction(long headIID)
        {
            TransactionDTO transactionDTO = productDetailRepository.GetTransaction(headIID);

            transactionDTO.OrderContactMap = new OrderContactMapDTO();
            var orderContactMap = new OrderRepository().GetOrderContacts(headIID).FirstOrDefault();
            transactionDTO.OrderContactMap = OrderContactMapMapper.Mapper(_callContext).ToDTO(orderContactMap);
            return transactionDTO;
        }

        public List<SalesRelationshipTypeDTO> GetSalesRelationshipType(Eduegate.Services.Contracts.Enums.SalesRelationShipTypes salesRelationShipTypes)
        {
            List<SalesRelationshipType> list = productDetailRepository.GetSalesRelationshipType((Eduegate.Framework.Enums.SalesRelationshipType)salesRelationShipTypes);
            List<SalesRelationshipTypeDTO> dtoList = list.Select(x => SalesRelationshipTypeMapper.ToSalesRelationshipTypeDTOMap(x)).ToList();
            return dtoList;
        }
        
        public List<SalesRelationshipTypeDTO> GetSalesRelationshipTypeList(Eduegate.Services.Contracts.Enums.SalesRelationShipTypes salesRelationShipTypes, long productID)
        {
            // get SalesRelationshipType
            List<SalesRelationshipType> listSalesRelationshipType = productDetailRepository.GetSalesRelationshipType((Eduegate.Framework.Enums.SalesRelationshipType)salesRelationShipTypes);

            // get ProductToProductMap
            List<ProductToProductMap> listProductToProductMap = productDetailRepository.GetProductMaps(productID);

            List<SalesRelationshipTypeDTO> dtoListSalesRelationshipTypeDTO = new List<SalesRelationshipTypeDTO>();

            foreach (var item in listSalesRelationshipType)
            {
                SalesRelationshipTypeDTO dtoSalesRelationshipTypeDTO = new SalesRelationshipTypeDTO();
                dtoSalesRelationshipTypeDTO.SalesRelationTypeID = item.SalesRelationTypeID;
                dtoSalesRelationshipTypeDTO.RelationName = item.RelationName;

                dtoSalesRelationshipTypeDTO.ToProducts = new List<ProductMapDTO>();
                // filter based on SalesRelationTypeID
                List<ProductToProductMap> lists = listProductToProductMap.Where(x => x.SalesRelationTypeID == item.SalesRelationTypeID).ToList();
                foreach (var list in lists)
                {
                    ProductMapDTO dtoProductMapDTO = new ProductMapDTO();
                    dtoProductMapDTO.ProductToProductMapID = list.ProductToProductMapIID;
                    dtoProductMapDTO.ProductID = (long)list.ProductIDTo;
                    dtoProductMapDTO.ProductName = productDetailRepository.GetProductNameByID((long)list.ProductIDTo);
                    dtoProductMapDTO.SalesRelationShipType = item.SalesRelationTypeID;
                    // add ProductMapDTO
                    dtoSalesRelationshipTypeDTO.ToProducts.Add(dtoProductMapDTO);
                }
                // add dtoSalesRelationshipTypeDTO
                dtoListSalesRelationshipTypeDTO.Add(dtoSalesRelationshipTypeDTO);
            }
            return dtoListSalesRelationshipTypeDTO;
        }

        public ProductToProductMapDTO GetProductToProductMap(string searchText, long productID)
        {
            ProductToProductMapDTO dto = new ProductToProductMapDTO();

            if (productID.IsNull() || productID <= 0)
            {
                dto.SalesRelationshipTypes = GetSalesRelationshipType(Eduegate.Services.Contracts.Enums.SalesRelationShipTypes.All);
            }
            else
            {
                dto.FromProduct.ProductID = productID;
                dto.FromProduct.ProductName = productDetailRepository.GetProductNameByID(productID);

                dto.SalesRelationshipTypes = GetSalesRelationshipTypeList(Eduegate.Services.Contracts.Enums.SalesRelationShipTypes.All, productID);
            }
            return dto;
        }

        public ProductDTO GetProductDetailByProductId(long productID)
        {
            Product product = productDetailRepository.GetProductDetailByProductId(productID);
            return ProductMapper.ToProductDTOMap(product);
        }

        public List<PropertyDTO> CreateProperties(List<PropertyDTO> dtoPropertyList)
        {
            List<Property> propertyEntityList = new List<Property>();
            List<PropertyDTO> propertyDTOList = new List<PropertyDTO>();
            Property propertyEntity = null;
            PropertyDTO propertyDTO = null;

            if (dtoPropertyList.IsNotNull() && dtoPropertyList.Count > 0)
            {
                foreach (PropertyDTO dtoProperty in dtoPropertyList)
                {
                    propertyEntity = new Property();
                    propertyEntity = ToPropertyEntity(dtoProperty, _callContext);
                    propertyEntityList.Add(propertyEntity);
                }

                var entityList = productDetailRepository.CreateProperties(propertyEntityList); //pass data to repository and get entitylist from properties

                if (entityList.IsNotNull() && entityList.Count > 0)
                {
                    foreach (var entity in entityList)
                    {
                        propertyDTO = new PropertyDTO();
                        propertyDTO = FromPropertyEntity(entity);
                        propertyDTOList.Add(propertyDTO);
                    }
                }
            }

            return propertyDTOList;
        }

        public List<ProductPriceListTypeDTO> GetProductPriceListTypes()
        {
            List<ProductPriceListType> productPriceListTypes = new ProductCatalogRepository().GetProductPriceListTypes();
            return productPriceListTypes.Select(x => ProductPriceListTypeMapper.Mapper.ToDTO(x)).ToList();
        }

        public List<ProductPriceListLevelDTO> GetProductPriceListLevels()
        {
            List<ProductPriceListLevel> productPriceListLevels = new ProductCatalogRepository().GetProductPriceListLevels();
            return productPriceListLevels.Select(x => ProductPriceListLevelMapper.Mapper.ToDTO(x)).ToList();
        }

        public List<ProductDTO> GetProductWidgets(CallContext context)
        {
            decimal ConvertedPrice = UtilityRepository.GetCurrencyPrice(context);

            var lists = productDetailRepository.GetProductWidgets();
            List<ProductDTO> list = new List<ProductDTO>();

            lists.ForEach(x =>
            {
                list.Add(new ProductDTO
                {
                    ProductID = x.ProductIID,
                    SKUID = x.SKUID,
                    ProductCode = x.ProductCode,
                    ProductName = x.ProductName,
                    ProductPrice = Eduegate.Framework.Helper.Utility.FormatDecimal(Convert.ToDecimal(x.ProductPrice) * ConvertedPrice, 3),
                    ImageFile = (x.ImageFile.IsNull() ? string.Empty : x.ImageFile.Replace("\\", "/")),
                    DiscountedPrice = x.DiscountedPrice.IsNull() ? "0" : Eduegate.Framework.Helper.Utility.FormatDecimal(Convert.ToDecimal(x.DiscountedPrice) * ConvertedPrice, 3),
                    HasStock = x.HasStock,
                    BrandIID = x.BrandIID,
                    BrandName = x.BrandName
                });
            });

            return list;
        }

        public decimal GetProductInventoryByBranch(long skuID, long BranchID)
        {
            return productDetailRepository.GetProductInventoryByBranch(skuID, BranchID);
        }

        public List<ProductInventoryBranchDTO> GetProductInventoryOnline(string skuID, long? customerID =null)
        {
            return GetProductInventoryOnlineBranch(skuID, customerID);
        }

        public List<ProductInventoryBranchDTO> GetProductInventoryOnlineBranch(string skuID, long? customerID = null)
        {
            var userCart = default(ShoppingCartDTO);
            if (_callContext != null)
            {
                userCart = new ShoppingCartBL(_callContext).IsUserCartExist(_callContext, customerID);
            }
            var cartIID = "";
            if (userCart != null && userCart.CartID != null)
            {
                cartIID = userCart.CartID;
            }

            var productInventoryDetails = productDetailRepository.GetInventoryDetails(skuID, customerID, cartIID, _callContext);
            //var deliveryCheckDTO = GetDeliveryTypeCheckDTO(skuID);
            DataTable DeliveryCheckList = productDetailRepository.GetProductDeliveryDetailsData(skuID);
            var ProductInventoryBranchList = new List<ProductInventoryBranchDTO>();

            if (productInventoryDetails != null && productInventoryDetails.Rows.Count > 0)
            {
                foreach (DataRow item in productInventoryDetails.Rows)
                {
                    long deliveryCount = 0;

                    var category = shoppingCartRepository.GetAddedCartDetailsWithItems(Convert.ToInt32(item["ProductSKUMapID"]));
                    var canteenCategoryID = new SettingRepository().GetSettingDetail("CANTEEN_CATEGORY_ID").SettingValue;
                    bool isCanteen = category.CategoryIID == long.Parse(canteenCategoryID);

                    var ProductInventoryBranch = new ProductInventoryBranchDTO();
                    ProductInventoryBranch.ProductDiscountPrice = Convert.ToDecimal(item["ProductDiscountPrice"]);
                    ProductInventoryBranch.ProductPricePrice = Convert.ToDecimal(item["ProductPrice"]);
                    if (DeliveryCheckList != null && DeliveryCheckList.Rows.Count > 0)
                    {
                        DataRow[] resultDataRow = DeliveryCheckList.Select("ProductSKUMapIID= '" + Convert.ToString(item["ProductSKUMapID"]) + "'");
                        foreach (DataRow drItem in resultDataRow)
                        {
                            deliveryCount = Convert.ToInt32(drItem["DeliveryCount"]);
                        }
                    }
                    ProductInventoryBranch.ProductSKUMapID = (long)item["ProductSKUMapID"];
                    long longValue;
                    long.TryParse(item["InventoryBranch"].ToString(), out longValue);
                    ProductInventoryBranch.BranchID = longValue;
                   // ProductInventoryBranch.BranchID = item.Table.Columns.Contains("InventoryBranch") ? Convert.ToInt64(item["InventoryBranch"].ToString()) : 0;
                    //condition added to handle if any item is not having price

                    if (ProductInventoryBranch.ProductDiscountPrice == 0 && ProductInventoryBranch.ProductPricePrice > 0)
                    {
                        ProductInventoryBranch.ProductDiscountPrice = ProductInventoryBranch.ProductPricePrice;
                    }

                    if (item["Quantity"] != System.DBNull.Value)
                    {
                        ProductInventoryBranch.Quantity = isCanteen ? 10 :(long)item["Quantity"];
                    }
                    else
                    {
                        ProductInventoryBranch.Quantity = 0;
                    }

                    //if (ProductInventoryBranch.ProductDiscountPrice > 0 && deliveryCount > 0)
                    //{
                    //ProductInventoryBranch.Quantity = (long)item["Quantity"];
                    //}
                    //else
                    //{
                    //    ProductInventoryBranch.Quantity = 0;
                    //}

                    ProductInventoryBranch.ProductCostPrice = Convert.ToDecimal(item["CostPrice"]);
                    ProductInventoryBranchList.Add(ProductInventoryBranch);
                }
            }
            return ProductInventoryBranchList;
        }

        public ProductInventoryBranchDTO GetInventoryDetailsSKUID(long skuID, long customerID = 0, bool type = false,int companyID=1)
        {
            return productDetailRepository.GetInventoryDetailsSKUID(skuID, customerID, "", type, _callContext.IsNotNull() && _callContext.CompanyID.HasValue ? (int)_callContext.CompanyID : companyID);
        }

        public ProductInventoryBranchDTO GetPriceDetailsSKUID(long InventoryBranch, long skuID, long customerID = 0, string CartID = "")
        {
            return productDetailRepository.GetPriceDetailsSKUID(InventoryBranch, skuID, customerID, CartID);
        }

        public Product GetProductBySKUID(long productSKUMapID)
        {
            return new ProductDetailRepository().GetProductBySKUID(productSKUMapID);
        }

        public Product GetProductSKUIDBranchID(long productSKUMapID)
        {
            return new ProductDetailRepository().GetProductBySKUID(productSKUMapID);
        }

        #region Product SeriralMap Methods

        public bool IsSerialNumberUnique(string serialNumber, long productSerialIID = 0)
        {
            var serials = new ProductDetailRepository().GetProductSerialMaps(serialNumber, productSerialIID);
            if (serials.Count > 0)
                return false;
            else
                return true;
        }

        public List<ProductSerialMap> GetProductSerialMaps(long transactionDetailID, long productSKUMapID, int recordLimit)
        {
            return new ProductDetailRepository().GetProductSerialMaps(transactionDetailID, productSKUMapID, recordLimit);
        }
        public List<ProductSerialMap> GetProductSerialMaps(string serialNumber, long productSerialIID = 0)
        {
            return new ProductDetailRepository().GetProductSerialMaps(serialNumber, productSerialIID);
        }

        public List<ProductInventorySerialMap> GetProductInventorySerialMaps(long productSKUMapID, int recordLimit, bool serialKeyUsed = false)
        {
            return new TransactionRepository().GetProductInventorySerialMaps(productSKUMapID, "", recordLimit, serialKeyUsed);
        }

        public bool UpdateProductInventorySerialMaps(List<ProductInventorySerialMap> entities, bool isDelete = false)
        {
            return new ProductDetailRepository().UpdateProductInventorySerialMaps(entities, isDelete);
        }
        #endregion

        public List<ProductImageMap> GetProductImages(long skuID)
        {
            return new ProductDetailRepository().GetProductImages(skuID);
        }
        
        public List<ProductMultiPriceDTO> GetProductMultiPriceDetails(long InventoryBranch, long skuID, long customerID = 0)
        {
            decimal ConversionRate = (decimal)UtilityRepository.GetExchangeRate( (int)_callContext.CompanyID, _callContext.CurrencyCode);
            var productMultiPriceList = productDetailRepository.GetProductMultiPriceDetails(InventoryBranch, skuID, customerID);
            var productMultiPriceDTOList = new List<ProductMultiPriceDTO>();
            foreach (var multiprice in productMultiPriceList)
            {
                switch (multiprice.GroupID)
                {
                    case "1":
                    default:
                        multiprice.GroupName = ResourceHelper.GetValue("BlueMember", _callContext.IsNotNull() ? _callContext.LanguageCode : "en");
                        break;
                    case "2":
                        multiprice.GroupName = ResourceHelper.GetValue("SilverMember", _callContext.IsNotNull() ? _callContext.LanguageCode : "en");
                        break;
                    case "3":
                        multiprice.GroupName = ResourceHelper.GetValue("GoldMember", _callContext.IsNotNull() ? _callContext.LanguageCode : "en");
                        break;
                    case "4":
                        multiprice.GroupName = ResourceHelper.GetValue("PlatinumMember", _callContext.IsNotNull() ? _callContext.LanguageCode : "en");
                        break;
                    case "5":
                        multiprice.GroupName = ResourceHelper.GetValue("DiamondMember", _callContext.IsNotNull() ? _callContext.LanguageCode : "en");
                        break;
                    case "6":
                        multiprice.GroupName = ResourceHelper.GetValue("DiamondPlusMember", _callContext.IsNotNull() ? _callContext.LanguageCode : "en");
                        break;
                }
                multiprice.MultipriceValue = (Convert.ToDecimal(multiprice.MultipriceValue) * ConversionRate).ToString();
                productMultiPriceDTOList.Add(ProductMultiPriceMapper.Mapper(this._callContext).ToDTO(multiprice));
            }
            return productMultiPriceDTOList;
        }

        public List<ProductQuantityDiscountDTO> GetProductQtyDiscountDetails(long InventoryBranch, long skuID, long customerID = 0)
        {
            decimal ConversionRate = (decimal)UtilityRepository.GetExchangeRate(_callContext == null || !_callContext.CompanyID.HasValue ? 1 : (int)_callContext.CompanyID, _callContext == null ? null : _callContext.CurrencyCode);
            var productQtyDiscountList = productDetailRepository.GetProductQtyDiscountDetails(InventoryBranch, skuID, customerID);
            var productQtyDiscountDTOList = new List<ProductQuantityDiscountDTO>();
            foreach (var qtydiscount in productQtyDiscountList)
            {
                qtydiscount.QtyPrice = (Convert.ToDecimal(qtydiscount.QtyPrice) * ConversionRate).ToString();
                productQtyDiscountDTOList.Add(ProductQuantityDiscountMapper.Mapper(this._callContext).ToDTO(qtydiscount));
            }
            return productQtyDiscountDTOList;
        }

        public List<DeliveryCheckDTO> GetDeliveryTypeCheckDTO(string SKUID)
        {
            var DeliveryCheckDTOList = new List<DeliveryCheckDTO>();
            var DeliveryCheckList = productDetailRepository.GetProductDeliveryDetails(SKUID);
            if (DeliveryCheckList != null && DeliveryCheckList.Any())
            {
                var mapper = DeliveryTypeCheckMappers.Mapper();
                foreach (var item in DeliveryCheckList)
                {
                    DeliveryCheckDTOList.Add(mapper.ToDTO(item));
                }
            }
            return DeliveryCheckDTOList;
        }

        public string SaveProductSkuBarCode(long skuId, string barCode)
        {
            if (skuId > 0 && !string.IsNullOrEmpty(barCode))
            {
                barCode = new ProductDetailRepository().SaveProductSkuBarCode(skuId, barCode);
            }
            else { barCode = null; }
            return barCode;
        }

        public List<string> GetProductSKUTags(long skuId,int companyID)
        {
            return new ProductDetailRepository().GetProductSKUTags(skuId, companyID);
        }

        public List<string> GetCategoryProductSKUTags(long skuId)
        {
            return new ProductDetailRepository().GetCategoryProductSKUTags(skuId);
        }

        public string SaveProductSkuPartNo(long skuId, string partNo)
        {
            if (skuId > 0 && !string.IsNullOrEmpty(partNo))
            {
                partNo = new ProductDetailRepository().SaveProductSkuPartNo(skuId, partNo);
            }
            else { partNo = null; }
            return partNo;
        }

        public bool SaveProductSKUTags(ProductSKUTagDTO productSKUTags)
        {
            long loginID = _callContext.IsNotNull() ? _callContext.LoginID.HasValue ? _callContext.LoginID.Value : default(long) : default(long);
            int companyID = _callContext.IsNotNull() ? _callContext.CompanyID.HasValue ? _callContext.CompanyID.Value : default(int) : default(int);
            return new ProductDetailRepository().SaveProductSKUTags(productSKUTags.SelectedTagIds, productSKUTags.IDs, productSKUTags.SelectedTagNames, loginID, companyID);
        } 
         
        public List<KeyValueDTO> GetProductSKUTagMaps(long productSKUMapIID)
        {
            int companyID = _callContext.IsNotNull() ? _callContext.CompanyID.HasValue ? _callContext.CompanyID.Value : default(int) : default(int);
            return new ProductDetailRepository().GetProductSKUTagMaps(productSKUMapIID,companyID);
        }
         
        public SKUDTO GetProductSKUDetails(long productSKUID)
        {
            var skuMap = new ProductDetailRepository().GetProductSkuDetails(productSKUID);
            if (skuMap.IsNotNull())
            {
                var skuDTO = new SKUDTO();
                skuDTO = Mappers.Catalog.SKUMapper.Mapper(_callContext).ToDTO(skuMap);
                
                return skuDTO;
            }
            else
            {
                return null;
            }
        }

        public SKUDTO SaveProductSKUDetails(SKUDTO dto)
        {

            //var skuInventoryConfigDetails = new ProductDetailRepository().GetProductInventorySKUConfig(productSKUID);

            if (dto.IsNotNull())
            {
                var sku = new ProductSKUMap();

                sku = Mappers.Catalog.SKUMapper.Mapper(_callContext).ToEntity(dto);


                if (dto.ProductInventorySKUConfigMaps.IsNotNull() && dto.ProductInventorySKUConfigMaps.Count > 0)
                {
                    sku.ProductInventorySKUConfigMaps = new List<ProductInventorySKUConfigMap>();
                    foreach (var skuConfigMap in dto.ProductInventorySKUConfigMaps)
                    {
                        // Add foreach here if multiple is possible
                        var skuMap = new ProductInventorySKUConfigMap()
                        {
                            ProductInventoryConfigID = skuConfigMap.ProductInventoryConfigID,
                            ProductSKUMapID = skuConfigMap.ProductSKUMapID,
                            UpdatedBy = Convert.ToInt32(_callContext.LoginID),
                            CreatedBy = skuConfigMap.CreatedBy,
                            CreatedDate = skuConfigMap.CreatedDate,
                            UpdatedDate = DateTime.Now,
                            TimeStamps = Convert.FromBase64String(skuConfigMap.TimeStamps),
                        };

                        skuMap.ProductInventoryConfig = new ProductInventoryConfig();
                        skuMap.ProductInventoryConfig = ProductInventoryConfigMapper.ToEntity(skuConfigMap.ProductInventoryConfig);
                        sku.ProductInventorySKUConfigMaps.Add(skuMap);
                    } 
                }
                var skuInventoryConfigDetails = new ProductDetailRepository().SaveProductSkuDetails(sku);
            }
            return null;
        }

        public string SaveSKUProductMap(long productID,long skuID)
        {
               var sku = Mappers.Catalog.SKUMapper.Mapper(_callContext).FromDTOToEntity(productID,skuID);
             
                var skuInventoryConfigDetails = new ProductDetailRepository().SaveSKUProductMap(sku);
            return null; 
        }

        public bool SaveSKUManagers(AddProductDTO skuManagers)
        {
            return new ProductDetailRepository().SaveSKUManagers(skuManagers.SelectedKeyValueOwnerId, skuManagers.ID, _callContext);
        }

        public SKUDTO GetSKUEmployeeDetails(long productSKUID)
        {
            var skuMap = new ProductDetailRepository().GetProductSkuDetails(productSKUID);
            if (skuMap.IsNotNull())
            { 
                var skuDTO = new SKUDTO();
                skuDTO = Mappers.Catalog.SKUMapper.Mapper(_callContext).ToDTO(skuMap);
                return skuDTO;
            }
            else
            {
                return null;
            }
        }

        public string GetProductSkuCode(long skuID)
        {
           return new ProductDetailRepository().GetProductSkuCode(skuID);
        }



        //public List<ProductBundleDTO> GetProductBundleItemDetail(long productSKUMapID)
        //{
        //    var ProductBundle = new List<ProductBundle>();
        //    var productBundleDTO = new List<ProductBundleDTO>();
        //    // get the Product Bundle Item Details
        //    ProductBundle = new ProductDetailRepository().GetProductBundleItemDetail(productSKUMapID);

        //    // convert from TransactionDetail to TransactionDetailDTO
        //    foreach (ProductBundle  detail in ProductBundle)
        //    {
        //        productBundleDTO.Add(new ProductBundleDTO()
        //        {
        //            FromProductID = detail.FromProductID,
        //            Quantity = detail.Quantity,
        //            FromProductSKUMapID = detail.FromProductSKUMapID,
        //            CostPrice = detail.CostPrice,
                   
        //        });
        //    }

        //    return productBundleDTO;
        //}


    }
}
