using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper;
using Eduegate.Framework.Helper.Enums;
//using Eduegate.Framework.Web.Library.Common;
using Eduegate.Logger;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Utilities.ImageScalar;
using Eduegate.Web.Library;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Eduegate.Utilities.ImageScalar.Contracts;

namespace Eduegate.ERP.Admin.Areas.Catalogs.Controllers
{
    public class ProductController : BaseSearchController
    {
        private string utilityServiceUrl { get { return ServiceHost + Constants.UTILITY_SERVICE_NAME; } }
        private static string dataSize { get { return ConfigurationExtensions.GetAppConfigValue("Select2DataSize"); } }
        IProductDetail client;

        public ProductController()
        {
            client = ClientFactory.ProductDetailServiceClient(CallContext);
        }

        // GET: /Product/
        public ActionResult ProductView()
        {
            ProductViewModel productViewModel = new ProductViewModel();
            productViewModel.ProductItems = new List<ProductItemViewModel>();
            string getServiceReq = string.Empty;

            try
            {
                ProductViewDTO productViewDTO = client.GetProductSummaryInfo();

                if (productViewDTO != null)
                {
                    productViewModel.TotalProduct = productViewDTO.TotalProduct;
                    productViewModel.RecentlyAdded = productViewDTO.RecentlyAdded;
                    productViewModel.MostSellingProduct = productViewDTO.MostSellingProduct;
                    productViewModel.OutOfStocks = productViewDTO.OutOfStocks;
                    productViewModel.PendingCreate = productViewDTO.PendingCreate;
                }
            }
            catch (Exception exception)
            {
                LogHelper<AccountController>.Fatal(exception.Message.ToString(), exception);
            }

            return View("ProductView", productViewModel);
        }

        [HttpPost]
        public JsonResult GetProducts(ProductViewSearchInfoViewModel searchInfo = null)
        {
            try
            {
                string productServiceUrl = ServiceHost + Constants.PRODUCT_DETAIL_SERVICE_NAME;
                var productViewDetails = ClientFactory.ProductDetailServiceClient(CallContext).GetProductViews(ProductViewSearchInfoViewModel.ToDTO(searchInfo));
                return Json(productViewDetails, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                LogHelper<ProductController>.Fatal(ex.Message.ToString(), ex);
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Product(long productIID = 0)
        {
            var addproductDTO = new AddProductDTO();
            var addProduct = new AddProductViewModel();
            addProduct.QuickCreate = new QuickCreateViewModel();
            addProduct.QuickCreate.Properties = new List<ProductPropertiesTypeValueViewModel>();
            addProduct.QuickCreate.DefaultProperties = new List<PropertyViewModel>();
            addProduct.SKUMappings = new List<SKUViewModel>();
            addProduct.ProductFeature = new ProductFeatureViewModel();
            addProduct.SelectedCategory = new List<ProductCategoryViewModel>();
            addProduct.SelectedTags = new List<ProductTagViewModel>();
            addProduct.BundleMaps = new ProductBundlesViewModel();
            addProduct.BundleMaps.ProductMaps = new List<ProductBundleMapViewModel>();
            addProduct.BundleMaps.SKUMaps = new List<ProductBundleMapViewModel>();

            // keyvalue for selected product owners
            addProduct.KeyValueOwners = new List<KeyValueViewModel>();

            if (productIID > 0)
            {
                string product = string.Empty;
                var productDTO = new AddProductDTO();
                string productServiceUrl = ServiceHost + Constants.PRODUCT_DETAIL_SERVICE_NAME;

                try
                {
                    addproductDTO = ClientFactory.ProductDetailServiceClient(CallContext).GetProduct(productIID);
                    var cultureDto = ClientFactory.ProductDetailServiceClient(CallContext).GetCultureList();

                    foreach (var culture in cultureDto)
                    {
                        var cultureInfo = addproductDTO.QuickCreate.CultureInfo.FirstOrDefault(a => a.CultureID == culture.CultureID);

                        if (cultureInfo == null)
                        {
                            addproductDTO.QuickCreate.CultureInfo.Add(new CultureDataInfoDTO()
                            {
                                CultureCode = culture.CultureCode,
                                CultureID = culture.CultureID,
                                CultureName = culture.CultureName,
                            });
                        }
                    }

                    addProduct.QuickCreate.BrandIID = addproductDTO.QuickCreate.BrandIID;
                    addProduct.QuickCreate.ProductTypeID = addproductDTO.QuickCreate.ProductTypeID;
                    addProduct.QuickCreate.Brand = new KeyValueViewModel
                    {
                        Key = addproductDTO.QuickCreate.BrandIID.ToString(),
                        Value = addproductDTO.QuickCreate.BrandName,
                    };
                    addProduct.QuickCreate.ProductFamily = new KeyValueViewModel
                    {
                        Key = addproductDTO.QuickCreate.ProductFamilyIID.IsNotNull() ? Convert.ToString(addproductDTO.QuickCreate.ProductFamilyIID) : "0",
                        Value = addproductDTO.QuickCreate.ProductFamilyName,
                    };

                    if (addproductDTO.QuickCreate.CultureInfo != null && addproductDTO.QuickCreate.CultureInfo.Count > 0)
                    {
                        addProduct.QuickCreate.CultureInfo = CultureDataInfoViewModel.FromDTO(addproductDTO.QuickCreate.CultureInfo);
                    }

                    addProduct.QuickCreate.ProductFamilyIID = addproductDTO.QuickCreate.ProductFamilyIID != null ? Convert.ToInt32(addproductDTO.QuickCreate.ProductFamilyIID) : 0;
                    //addProduct.QuickCreate.ProductOwnderID = addproductDTO.QuickCreate.ProductOwnderID != null ? Convert.ToInt32(addproductDTO.QuickCreate.ProductOwnderID) : 0;
                    addProduct.QuickCreate.ProductIID = addproductDTO.QuickCreate.ProductIID;
                    addProduct.QuickCreate.StatusIID = addproductDTO.QuickCreate.StatusIID != null ? Convert.ToInt32(addproductDTO.QuickCreate.StatusIID) : 0;
                    addProduct.QuickCreate.StatusName = addproductDTO.QuickCreate.StatusName;
                    addProduct.QuickCreate.ProductName = addproductDTO.QuickCreate.ProductName;
                    addProduct.QuickCreate.IsOnline = addproductDTO.QuickCreate.IsOnline;
                    addProduct.QuickCreate.TaxTemplate = addproductDTO.QuickCreate.TaxTemplateID.ToString();

                    // Product Inventory Product Config
                    addProduct.QuickCreate.ProductInventoryConfigViewModels = new ProductInventoryConfigViewModel();
                    if (addproductDTO.QuickCreate.ProductInventoryConfigDTOs.IsNotNull())
                    {
                        addProduct.QuickCreate.ProductInventoryConfigViewModels = ProductInventoryConfigViewModel.ToViewModel(addproductDTO.QuickCreate.ProductInventoryConfigDTOs, (addproductDTO.QuickCreate == null ? null : addproductDTO.QuickCreate.CultureInfo));
                    }

                    addProduct.QuickCreate.Properties = new List<ProductPropertiesTypeValueViewModel>();
                    foreach (var property in addproductDTO.QuickCreate.Properties)
                    {
                        var propertyTypeValueViewModel = new ProductPropertiesTypeValueViewModel();
                        propertyTypeValueViewModel.SelectedProperties = new List<PropertyViewModel>();
                        if (property.SelectedProperties != null && property.SelectedProperties.Count() > 0)
                        {
                            foreach (var propertyDTO in property.SelectedProperties)
                            {
                                var propertyViewModel = new PropertyViewModel();
                                propertyViewModel.PropertyIID = propertyDTO.PropertyIID;
                                propertyViewModel.PropertyName = propertyDTO.PropertyName;
                                if (propertyDTO.PropertyTypeID != null)
                                {
                                    propertyViewModel.PropertyTypeID = Convert.ToByte(propertyDTO.PropertyTypeID);
                                }
                                propertyTypeValueViewModel.SelectedProperties.Add(propertyViewModel);
                            }
                        }
                        if (property.PropertyType != null)
                        {
                            propertyTypeValueViewModel.PropertyType = new PropertyTypeViewModel();
                            propertyTypeValueViewModel.PropertyType.PropertyTypeID = property.PropertyType.PropertyTypeID;
                            propertyTypeValueViewModel.PropertyType.PropertyTypeName = property.PropertyType.PropertyTypeName;
                        }

                        addProduct.QuickCreate.Properties.Add(propertyTypeValueViewModel);
                    }

                    if (addproductDTO.SKUMappings != null && addproductDTO.SKUMappings.Count > 0)
                    {
                        foreach (SKUDTO skuLines in addproductDTO.SKUMappings)
                        {
                            addProduct.SKUMappings.Add(SKUViewModel.FromDTO(skuLines, (addproductDTO.QuickCreate == null ? null : addproductDTO.QuickCreate.CultureInfo)));
                        }
                    }

                    if (addproductDTO.SelectedCategory != null && addproductDTO.SelectedCategory.Count > 0)
                    {
                        foreach (var category in addproductDTO.SelectedCategory)
                        {
                            var categoryViewModel = new ProductCategoryViewModel();
                            categoryViewModel.CategoryID = category.CategoryID;
                            categoryViewModel.CategoryName = category.CategoryName;

                            addProduct.SelectedCategory.Add(categoryViewModel);
                        }
                    }

                    if (addproductDTO.SelectedTags != null && addproductDTO.SelectedTags.Count > 0) //pushing dto to viewmodel list property to show the tag list in select2 control on edit
                    {
                        foreach (var dtoTag in addproductDTO.SelectedTags)
                        {
                            var vmTag = new ProductTagViewModel();

                            vmTag.TagIID = dtoTag.TagIID;
                            vmTag.TagName = dtoTag.TagName;

                            addProduct.SelectedTags.Add(vmTag);
                        }
                    }

                    if (addproductDTO.QuickCreate.DefaultProperties != null && addproductDTO.QuickCreate.DefaultProperties.Count > 0)
                    {
                        foreach (var productPropertyMap in addproductDTO.QuickCreate.DefaultProperties)
                        {
                            var propertyViewModel = new PropertyViewModel();
                            propertyViewModel.DefaultValue = productPropertyMap.DefaultValue;
                            propertyViewModel.PropertyIID = Convert.ToInt32(productPropertyMap.PropertyIID);
                            propertyViewModel.PropertyName = productPropertyMap.PropertyName;
                            addProduct.QuickCreate.DefaultProperties.Add(propertyViewModel);
                        }
                    }

                    addProduct.ProductMaps = new ProductToProductMapViewModel();
                    addProduct.ProductMaps.FromProduct = new ProductMapViewModel();
                    addProduct.ProductMaps.FromProduct.ProductID = Convert.ToInt32(addproductDTO.QuickCreate.ProductIID);
                    addProduct.ProductMaps.FromProduct.ProductName = addproductDTO.QuickCreate.ProductName;
                    addProduct.ProductMaps.IsFromProductDisabled = true;
                    //addProduct.ProductMaps.ToProduct = new List<ProductMapViewModel>();

                    if (addproductDTO.ProductMaps != null && addproductDTO.ProductMaps.ToProduct != null && addproductDTO.ProductMaps.ToProduct.Count > 0)
                    {
                        foreach (var productMaps in addproductDTO.ProductMaps.ToProduct)
                        {
                            var productMapViewModel = new ProductMapViewModel();
                            productMapViewModel.ProductToProductMapID = productMaps.ProductToProductMapID;
                            productMapViewModel.ProductID = Convert.ToInt32(productMaps.ProductID);
                            productMapViewModel.ProductName = productMaps.ProductName;
                            productMapViewModel.SalesRelationShipType = productMaps.SalesRelationShipType;
                            //addProduct.ProductMaps.ToProduct.Add(productMapViewModel);
                        }
                    }


                    addProduct.UploadedFiles = new List<UploadedFileDetailsViewModel>();
                    var tempFolder = string.Format("{0}\\{1}\\{2}\\{3}", ConfigurationExtensions.GetAppConfigValue("ImagesPhysicalPath").ToString(), EduegateImageTypes.Products, productIID, ImageType.LargeImage.ToString());
                    if (Directory.Exists(tempFolder))
                    {
                        string[] imageNames = Directory.GetFiles(tempFolder).Select(path => Path.GetFileName(path)).ToArray();
                        if (imageNames != null && imageNames.Count() > 0)
                        {
                            foreach (var image in imageNames)
                            {
                                var file = new UploadedFileDetailsViewModel();
                                file.FileName = image;
                                file.FilePath = string.Format("{0}/{1}/{2}/{3}/{4}", ConfigurationExtensions.GetAppConfigValue("ImageHostUrl").ToString(), EduegateImageTypes.Products, productIID, ImageType.LargeImage.ToString(), image);
                                addProduct.UploadedFiles.Add(file);
                            }
                        }
                    }

                    // get UploadVideoFiles
                    addProduct.UploadVideoFiles = new List<UploadedFileDetailsViewModel>();
                    var tempVideoFolder = string.Format("{0}\\{1}", ConfigurationExtensions.GetAppConfigValue("ImagesPhysicalPath").ToString(), EduegateImageTypes.ProductVideos, productIID);

                    if (Directory.Exists(tempVideoFolder))
                    {
                        string[] videoNames = Directory.GetFiles(tempVideoFolder).Select(path => Path.GetFileName(path)).ToArray();
                        if (videoNames != null && videoNames.Count() > 0)
                        {
                            foreach (var video in videoNames)
                            {
                                var file = new UploadedFileDetailsViewModel();
                                file.FileName = video;
                                file.FilePath = string.Format("{0}/{1}/{2}/{3}", ConfigurationExtensions.GetAppConfigValue("ImageHostUrl").ToString(), EduegateImageTypes.ProductVideos, productIID, video);
                                addProduct.UploadVideoFiles.Add(file);
                            }
                        }
                    }

                    // Get Slected Product Owner
                    addProduct.KeyValueOwners = GetEmployeeIdNameEntityTypeRelation(productIID);

                    MapProductBundlesToViewModel(addproductDTO, addProduct);

                }
                catch (Exception ex)
                {
                    LogHelper<ProductController>.Fatal(ex.Message.ToString(), ex);
                    return Json(ex.Message, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                // set default values 
                addProduct.QuickCreate.IsOnline = false;
                // for status when user create new Product
                addProduct.QuickCreate.StatusIID = (long)ProductStatuses.Draft;
                addProduct.QuickCreate.ProductInventoryConfigViewModels = new ProductInventoryConfigViewModel();
                var cultures = ClientFactory.ProductDetailServiceClient(CallContext).GetCultureList();
                addProduct.QuickCreate.CultureInfo = CultureDataInfoViewModel.FromDTO(cultures);
            }

            addProduct.DefaultLanguage = ConfigurationExtensions.GetAppConfigValue("DefaultLang").ToString();
            // get ProductToProductMap
            addProduct.ProductMaps = GetProductToProductMapDetail(productIID);
            addProduct.SeoMetadata = SeoMetadataViewModel.FromDTO(addproductDTO.SeoMetadata, (addproductDTO.QuickCreate == null ? null : addproductDTO.QuickCreate.CultureInfo));

            if (addProduct.SeoMetadata == null)
            {
                addProduct.SeoMetadata = new SeoMetadataViewModel();
                addProduct.SeoMetadata.InitializeCultureData(addProduct.QuickCreate.CultureInfo);
            }
            var skuviewmodel = new SKUViewModel() { SkuName = new MultiLanguageText() { Text = "", CultureDatas = addProduct.QuickCreate.CultureInfo } };
            var productInventoryViewmodel = new ProductInventoryConfigViewModel()
            {
                Description = new MultiLanguageText() { Text = "", CultureDatas = addProduct.QuickCreate.CultureInfo },
                Details = new MultiLanguageText() { Text = "", CultureDatas = addProduct.QuickCreate.CultureInfo }
            };

            if (skuviewmodel.IsNotNull())
            {
                skuviewmodel.ProductInventoryConfigViewModels = productInventoryViewmodel;
            }
            if (productIID == 0) { addProduct.SKUMappings.Add(skuviewmodel); }


            // Delivery Master Type
            addProduct.QuickCreate.DeliveryTypeViewModels = new List<DeliveryTypeViewModel>();
            addProduct.QuickCreate.DeliveryTypeViewModels = GetDeliveryTypeMaster();

            //Packing Type
            addProduct.QuickCreate.PackingTypes = new List<PackingTypeViewModel>();
            addProduct.QuickCreate.PackingTypes = GetPackingTypeMaster();

            //Countries
            addProduct.QuickCreate.Countries = new List<ProductDeliveryCountrySettingViewModel>();
            addProduct.QuickCreate.Countries = GetCountries();

            if (addProduct.QuickCreate.ProductInventoryConfigViewModels == null)
            {
                addProduct.QuickCreate.ProductInventoryConfigViewModels = new ProductInventoryConfigViewModel()
                {
                    Description = new MultiLanguageText() { Text = "", CultureDatas = addProduct.QuickCreate.CultureInfo },
                    Details = new MultiLanguageText() { Text = "", CultureDatas = addProduct.QuickCreate.CultureInfo }
                };
            }

            return View(addProduct);
        }

        private static void MapProductBundlesToViewModel(AddProductDTO addproductDTO, AddProductViewModel addProduct)
        {
            //Product Bundles
            if (addproductDTO.ProductBundles.IsNotNull())
            {
                addProduct.BundleMaps = new ProductBundlesViewModel();
                addProduct.BundleMaps.ProductMaps = new List<ProductBundleMapViewModel>();
                foreach (ProductMapDTO bundle in addproductDTO.ProductBundles.ToProduct)
                {
                    addProduct.BundleMaps.ProductMaps.Add(new ProductBundleMapViewModel()
                    {
                        ProductID = bundle.ProductID,
                        ProductName = bundle.ProductName,
                    });
                }
            }

            //SKU Bundles
            if (addproductDTO.SKUBundles.IsNotNull())
            {
                addProduct.BundleMaps = new ProductBundlesViewModel();
                addProduct.BundleMaps.SKUMaps = new List<ProductBundleMapViewModel>();
                foreach (ProductMapDTO bundle in addproductDTO.SKUBundles.ToProduct)
                {
                    addProduct.BundleMaps.SKUMaps.Add(new ProductBundleMapViewModel()
                    {
                        ProductID = bundle.ProductID,
                        ProductSKUMapID = bundle.ProductSKUMapID,
                        ProductSKUName = bundle.ProductName,
                    });
                }
            }
        }

        [HttpGet]
        public ActionResult ProductQuickCreate()
        {
            return PartialView(new Eduegate.Web.Library.ViewModels.AddProductViewModel() { QuickCreate = new QuickCreateViewModel() });
        }

        [HttpGet]
        public ActionResult _QuickCreate()
        {
            return PartialView();

        }

        [HttpGet]
        public ActionResult _CreateSKUMapping()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult _CreateProductFeature()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult _CreateProductType()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult _SearchCategory()
        {
            return PartialView();
        }

        [HttpPost]
        public JsonResult UpdateProduct(AddProductViewModel productDetails)
        {
            long productID = 0;
            string productServiceUrl = ServiceHost + Constants.PRODUCT_DETAIL_SERVICE_NAME;
            var userID = base.CallContext.LoginID;

            string tempFolderPath = string.Format(@"{0}\{1}\{2}\{3}", ConfigurationExtensions.GetAppConfigValue("ImagesPhysicalPath").ToString(), EduegateImageTypes.Products, Constants.TEMPFOLDER, userID);
            string DesignationFolderPath = string.Format("{0}\\{1}", ConfigurationExtensions.GetAppConfigValue("ImagesPhysicalPath").ToString(), EduegateImageTypes.Products);

            //Video
            string tempVideoFolderPath = string.Format("{0}\\{1}\\{2}\\{3}", ConfigurationExtensions.GetAppConfigValue("ImagesPhysicalPath").ToString(), EduegateImageTypes.ProductVideos, userID, Constants.TEMPFOLDER);
            string destonationVideoFolderPath = string.Format("{0}\\{1}", ConfigurationExtensions.GetAppConfigValue("ImagesPhysicalPath").ToString(), EduegateImageTypes.ProductVideos);

            var addProduct = new AddProductDTO();
            addProduct.QuickCreate = new QuickCreateDTO();
            addProduct.QuickCreate.ProductInventoryConfigDTOs = new ProductInventoryConfigDTO();
            addProduct.QuickCreate.DefaultProperties = new List<PropertyDTO>();
            addProduct.SKUMappings = new List<SKUDTO>();

            addProduct.SelectedCategory = new List<ProductCategoryDTO>();
            addProduct.SelectedTags = new List<ProductTagDTO>();

            try
            {
                List<ImageType> imageTypes = Enum.GetValues(typeof(ImageType)).Cast<ImageType>().ToList();

                addProduct.QuickCreate.BrandIID = Convert.ToInt32(productDetails.QuickCreate.BrandIID);
                addProduct.QuickCreate.CultureInfo = new List<CultureDataInfoDTO>();
                addProduct.ImageSourceTempPath = tempFolderPath;
                addProduct.ImageSourceDesignationPath = DesignationFolderPath;

                addProduct.VideoSourceTempPath = tempVideoFolderPath;
                addProduct.VideoSourceDestinationPath = destonationVideoFolderPath;
                // Start Manage ProductInventoryConfig for Product
                if (productDetails.QuickCreate.ProductInventoryConfigViewModels.IsNotNull())
                {
                    var skuViewModel = new SKUViewModel { ProductID = productDetails.QuickCreate.ProductIID };
                    addProduct.QuickCreate.ProductInventoryConfigDTOs = ProductInventoryConfigViewModel.ToDto(productDetails.QuickCreate.ProductInventoryConfigViewModels, skuViewModel, productDetails.QuickCreate.CultureInfo);

                }
                // End Manage ProductInventoryConfig for Product

                if (productDetails.QuickCreate.CultureInfo != null && productDetails.QuickCreate.CultureInfo.Count > 0)
                {
                    foreach (var culDetails in productDetails.QuickCreate.CultureInfo)
                    {
                        var culInfo = new CultureDataInfoDTO();
                        culInfo.CultureID = culDetails.CultureID;
                        culInfo.CultureName = culDetails.CultureName;
                        culInfo.CultureValue = culDetails.CultureValue;

                        addProduct.QuickCreate.CultureInfo.Add(culInfo);
                    }
                }

                addProduct.QuickCreate.ProductFamilyIID = productDetails.QuickCreate.ProductFamilyIID != default(long) ? Convert.ToInt32(productDetails.QuickCreate.ProductFamilyIID) : 0;

                addProduct.QuickCreate.ProductOwnderID = productDetails.QuickCreate.ProductOwnderID != default(long) ? Convert.ToInt32(productDetails.QuickCreate.ProductOwnderID) : 0;
                addProduct.QuickCreate.ProductIID = productDetails.QuickCreate.ProductIID;
                addProduct.QuickCreate.StatusIID = productDetails.QuickCreate.StatusIID != default(long) ? Convert.ToInt32(productDetails.QuickCreate.StatusIID) : 0;
                addProduct.QuickCreate.ProductName = productDetails.QuickCreate.ProductName;
                addProduct.QuickCreate.IsOnline = productDetails.QuickCreate.IsOnline;
                addProduct.QuickCreate.ProductTypeID = productDetails.QuickCreate.ProductTypeID != default(long) ? Convert.ToInt32(productDetails.QuickCreate.ProductTypeID) : 0;
                addProduct.QuickCreate.Properties = new List<ProductPropertiesTypeValuesDTO>();
                addProduct.QuickCreate.TaxTemplateID = productDetails.QuickCreate.TaxTemplate.IsNotNullOrEmpty() ? int.Parse(productDetails.QuickCreate.TaxTemplate) : (int?)null;
                if (productDetails.QuickCreate.Properties != null && productDetails.QuickCreate.Properties.Count > 0)
                {
                    foreach (var property in productDetails.QuickCreate.Properties)
                    {
                        var propertyTypeValueDTO = new ProductPropertiesTypeValuesDTO();
                        propertyTypeValueDTO.SelectedProperties = new List<PropertyDTO>();

                        if (property.SelectedProperties != null && property.SelectedProperties.Count > 0)
                        {
                            foreach (var propertyViewModel in property.SelectedProperties)
                            {
                                var propertyDTO = new PropertyDTO();
                                propertyDTO.PropertyIID = propertyViewModel.PropertyIID;
                                propertyDTO.PropertyName = propertyViewModel.PropertyName;
                                if (propertyViewModel.PropertyTypeID != default(byte))
                                {
                                    propertyDTO.PropertyTypeID = Convert.ToByte(propertyViewModel.PropertyTypeID);
                                }
                                propertyTypeValueDTO.SelectedProperties.Add(propertyDTO);
                            }
                        }

                        if (property.PropertyType != null)
                        {
                            propertyTypeValueDTO.PropertyType = new PropertyTypeDTO();
                            propertyTypeValueDTO.PropertyType.PropertyTypeID = property.PropertyType.PropertyTypeID;
                            propertyTypeValueDTO.PropertyType.PropertyTypeName = property.PropertyType.PropertyTypeName;
                        }

                        addProduct.QuickCreate.Properties.Add(propertyTypeValueDTO);
                    }
                }

                if (productDetails.SKUMappings != null && productDetails.SKUMappings.Count > 0)
                {
                    foreach (var skuLines in productDetails.SKUMappings)
                    {
                        var skuDTO = new SKUDTO();
                        skuDTO.BarCode = skuLines.BarCode;
                        skuDTO.PartNumber = skuLines.PartNumber;
                        skuDTO.ProductID = skuLines.ProductID;
                        skuDTO.ProductPrice = skuLines.ProductPrice != default(decimal) ? Convert.ToDecimal(skuLines.ProductPrice) : 0;
                        skuDTO.ProductSKUCode = skuLines.ProductSKUCode;
                        skuDTO.Sequence = skuLines.Sequence != default(int) ? Convert.ToInt32(skuLines.Sequence) : 0;
                        skuDTO.SKU = skuLines.SKU;
                        skuDTO.SkuName = skuLines.SkuName.Text;
                        skuDTO.ProductSKUMapID = skuLines.ProductSKUMapID;
                        skuDTO.isDefaultSKU = skuLines.isDefaultSKU;
                        skuDTO.StatusID = skuLines.StatusID;
                        skuDTO.ImageMaps = new List<SKUImageMapDTO>();

                        if (skuLines.ImageMaps != null && skuLines.ImageMaps.Count > 0)
                        {
                            foreach (var imageMapViewModel in skuLines.ImageMaps)
                            {
                                foreach (var imageproperty in imageTypes)
                                {
                                    var imageMapDTO = new SKUImageMapDTO();
                                    imageMapDTO.ImageMapID = imageMapViewModel.ImageMapID;
                                    imageMapDTO.ImageName = imageMapViewModel.ImageName;
                                    var imagePath = string.Format("{0}\\{1}\\{2}", skuLines.ProductID, imageproperty.ToString(), imageMapViewModel.ImageName);
                                    imageMapDTO.ImagePath = imagePath;
                                    imageMapDTO.ProductImageTypeID = (int)imageproperty;
                                    imageMapDTO.Sequence = imageMapViewModel.Sequence;
                                    imageMapDTO.SKUMapID = imageMapViewModel.SKUMapID;
                                    imageMapDTO.ProductID = Convert.ToInt64(skuDTO.ProductID);
                                    skuDTO.ImageMaps.Add(imageMapDTO);
                                }
                            }
                        }

                        skuDTO.ProductVideoMaps = new List<ProductVideoMapDTO>();
                        if (skuLines.VideoMaps != null && skuLines.VideoMaps.Count > 0)
                        {
                            foreach (var videoMapViewModel in skuLines.VideoMaps)
                            {
                                var videoMapDTO = new ProductVideoMapDTO();
                                videoMapDTO.ProductVideoMapID = videoMapViewModel.ProductVideoMapID;
                                videoMapDTO.VideoName = videoMapViewModel.VideoName;
                                videoMapViewModel.VideoPath = string.Format("{0}\\{1}", skuLines.ProductID, videoMapViewModel.VideoName);
                                videoMapDTO.VideoFile = videoMapViewModel.VideoPath;
                                videoMapDTO.Sequence = videoMapViewModel.Sequence;
                                videoMapDTO.ProductSKUMapID = skuDTO.ProductSKUMapID;
                                videoMapDTO.ProductID = Convert.ToInt64(skuDTO.ProductID);
                                skuDTO.ProductVideoMaps.Add(videoMapDTO);
                            }
                        }

                        // manage Product SKU Config
                        if (skuLines.ProductInventoryConfigViewModels.IsNotNull())
                        {
                            skuDTO.ProductInventoryConfigDTOs = new ProductInventoryConfigDTO();
                            // ProductInventoryConfigDTO
                            skuLines.ProductInventoryConfigViewModels.EmployeeID = skuLines.ProductInventoryConfigViewModels.SKUOwner.IsNotNull() ? Convert.ToInt64(skuLines.ProductInventoryConfigViewModels.SKUOwner.Key) : default(long);
                            skuDTO.ProductInventoryConfigDTOs = ProductInventoryConfigViewModel.ToDto(skuLines.ProductInventoryConfigViewModels, skuLines, productDetails.QuickCreate.CultureInfo);
                            skuDTO.IsHiddenFromList = skuLines.ProductInventoryConfigViewModels.IsHiddenFromList;
                            skuDTO.HideSKU = skuLines.ProductInventoryConfigViewModels.HideSKU;
                        }

                        //SKU Properties
                        skuDTO.Properties = new List<PropertyDTO>();
                        if (skuLines.Properties != null && skuLines.Properties.Count > 0)
                        {
                            foreach (var productPropertyMap in skuLines.Properties)
                            {
                                if (productPropertyMap.DefaultValue != null && productPropertyMap.DefaultValue.Trim() != "")
                                {
                                    var propertyDTO = new PropertyDTO();
                                    propertyDTO.DefaultValue = productPropertyMap.DefaultValue;
                                    propertyDTO.PropertyIID = Convert.ToInt32(productPropertyMap.PropertyIID);
                                    propertyDTO.PropertyName = productPropertyMap.PropertyName;
                                    propertyDTO.ProductSKUMapId = skuLines.ProductSKUMapID;
                                    skuDTO.Properties.Add(propertyDTO);
                                }
                            }
                        }

                        skuDTO.ProductSKUCultureInfo = SKUViewModel.ToCultureDTO(skuLines, (skuLines.SkuName == null ? null : skuLines.SkuName.CultureDatas));

                        addProduct.SKUMappings.Add(skuDTO);
                    }
                }

                if (productDetails.SelectedCategory != null && productDetails.SelectedCategory.Count > 0)
                {
                    foreach (var category in productDetails.SelectedCategory)
                    {
                        var categoryDTO = new ProductCategoryDTO();
                        categoryDTO.CategoryID = category.CategoryID;
                        categoryDTO.CategoryName = category.CategoryName;

                        addProduct.SelectedCategory.Add(categoryDTO);
                    }
                    if (addProduct.SelectedCategory.Count() > 0)
                        addProduct.SelectedCategory.FirstOrDefault().IsPrimary = true;
                }

                if (productDetails.SelectedTags != null && productDetails.SelectedTags.Count > 0)
                {
                    foreach (var vmTag in productDetails.SelectedTags)
                    {
                        var dtoTag = new ProductTagDTO();

                        dtoTag.TagIID = vmTag.TagIID;
                        dtoTag.TagName = vmTag.TagName;

                        addProduct.SelectedTags.Add(dtoTag);
                    }
                }

                if (productDetails.QuickCreate.DefaultProperties != null && productDetails.QuickCreate.DefaultProperties.Count > 0)
                {
                    foreach (var productPropertyMap in productDetails.QuickCreate.DefaultProperties)
                    {
                        if (productPropertyMap.DefaultValue != null && productPropertyMap.DefaultValue.Trim() != "")
                        {
                            var propertyDTO = new PropertyDTO();
                            propertyDTO.DefaultValue = productPropertyMap.DefaultValue;
                            propertyDTO.PropertyIID = Convert.ToInt32(productPropertyMap.PropertyIID);
                            propertyDTO.PropertyName = productPropertyMap.PropertyName;
                            addProduct.QuickCreate.DefaultProperties.Add(propertyDTO);
                        }
                    }
                }

                foreach (var imageType in imageTypes)
                {
                    if ((int)imageType != (int)ImageType.LargeImage)
                    {
                        ResizeProductImage(Convert.ToInt64(userID), imageType);
                    }
                }

                addProduct.SeoMetadata = SeoMetadataViewModel.ToDTO(productDetails.SeoMetadata, productDetails.QuickCreate.CultureInfo);
                MapProductBundleViewModelToDTO(productDetails, addProduct);
                productID = ClientFactory.ProductDetailServiceClient(CallContext).UpdateProduct(addProduct);


                // Save [catalog].[EmployeeCatalogRelations] 
                if (productID > 0)
                    SaveEntityTypeRelationMaps(productID, productDetails.KeyValueOwners);
            }
            catch (Exception ex)
            {
                LogHelper<ProductController>.Fatal(ex.Message.ToString(), ex);
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

            return Json(productID, JsonRequestBehavior.AllowGet);
        }

        private static void MapProductBundleViewModelToDTO(AddProductViewModel productDetails, AddProductDTO addProduct)
        {
            if (productDetails.BundleMaps != null)
            {
                //Product Bundles
                if (productDetails.BundleMaps.ProductMaps.IsNotNull() && productDetails.BundleMaps.ProductMaps.Count > 0)
                {
                    addProduct.ProductBundles = new ProductToProductMapDTO();
                    addProduct.ProductBundles.ToProduct = new List<ProductMapDTO>();
                    foreach (var bundleItem in productDetails.BundleMaps.ProductMaps)
                    {
                        addProduct.ProductBundles.ToProduct.Add(new ProductMapDTO()
                        {
                            ProductID = bundleItem.ProductID,
                            ProductName = bundleItem.ProductName
                        });

                    }
                }

                //SKU Bundles
                if (productDetails.BundleMaps.SKUMaps.IsNotNull() && productDetails.BundleMaps.SKUMaps.Count > 0)
                {
                    addProduct.SKUBundles = new ProductToProductMapDTO();
                    addProduct.SKUBundles.ToProduct = new List<ProductMapDTO>();
                    foreach (var bundleItem in productDetails.BundleMaps.SKUMaps)
                    {
                        addProduct.SKUBundles.ToProduct.Add(new ProductMapDTO()
                        {
                            ProductSKUMapID = bundleItem.ProductSKUMapID,
                            ProductID = bundleItem.ProductID,
                            ProductName = bundleItem.ProductSKUName
                        });
                    }
                }
            }
        }

        [HttpGet]
        public JsonResult GetCultureList()
        {
            try
            {
                string productServiceUrl = ServiceHost + Constants.PRODUCT_DETAIL_SERVICE_NAME;
                return Json(CultureDataInfoViewModel.FromDTO(ClientFactory.ProductDetailServiceClient(CallContext).GetCultureList()), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogHelper<ProductController>.Fatal(ex.Message.ToString(), ex);
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetBrandList(string searchText)
        {
            var brandKeyValueViewModel = new List<KeyValueViewModel>();

            try
            {
                var brandList = ClientFactory.ProductDetailServiceClient(CallContext).SearchBrand(searchText, Convert.ToInt32(ConfigurationExtensions.GetAppConfigValue("Select2DataSize")));
                if (brandList != null && brandList.Count > 0)
                {
                    brandKeyValueViewModel = brandList.Select(x => KeyValueViewModel.ToViewModel(x)).ToList();
                }
            }
            catch (Exception ex)
            {
                LogHelper<ProductController>.Fatal(ex.Message.ToString(), ex);
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

            return Json(brandKeyValueViewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProductFamilies(string searchText)
        {
            var familyKeyValueViewModel = new List<KeyValueViewModel>();

            try
            {
                var familyList = ClientFactory.ProductDetailServiceClient(CallContext).SearchProductFamilies(searchText, Convert.ToInt32(ConfigurationExtensions.GetAppConfigValue("Select2DataSize")));
                if (familyList != null && familyList.Count > 0)
                {
                    familyKeyValueViewModel = familyList.Select(x => KeyValueViewModel.ToViewModel(x)).ToList();
                }
            }
            catch (Exception ex)
            {
                LogHelper<ProductController>.Fatal(ex.Message.ToString(), ex);
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

            return Json(familyKeyValueViewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProductStatus()
        {
            try
            {
                return Json(ProductStatusViewModel.ToViewModel(ClientFactory.ProductDetailServiceClient(CallContext).GetProductStatus()), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                LogHelper<ProductController>.Fatal(ex.Message.ToString(), ex);
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetProductTypes()
        {
            string productTypes = string.Empty;
            var productTypeDTOList = new List<ProductTypeDTO>();
            var productTypesViewModelList = new List<ProductTypeViewModel>();

            try
            {
                productTypeDTOList = ClientFactory.ProductDetailServiceClient(CallContext).GetProductTypes();

                if (productTypeDTOList != null && productTypeDTOList.Count > 0)
                {
                    foreach (var ptDTO in productTypeDTOList)
                    {
                        productTypesViewModelList.Add(new ProductTypeViewModel()
                        {
                            ProductTypeID = ptDTO.ProductTypeID,
                            ProductTypeName = ptDTO.ProductTypeName,
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                LogHelper<ProductController>.Fatal(ex.Message.ToString(), ex);
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

            return Json(productTypesViewModelList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult _SKUProperties(decimal productFamilyIID)
        {
            return PartialView();
        }

        public ActionResult GetProductPropertyTypes(decimal productFamilyIID)
        {
            var propertyTypeDTOList = new List<PropertyTypeDTO>();
            var propertyTypeViewModelList = new List<PropertyTypeViewModel>();
            string productServiceUrl = ServiceHost + Constants.PRODUCT_DETAIL_SERVICE_NAME;

            try
            {
                propertyTypeDTOList = ClientFactory.ProductDetailServiceClient(CallContext).GetProductPropertyTypes(productFamilyIID);

                if (propertyTypeDTOList != null && propertyTypeDTOList.Count > 0)
                {
                    foreach (var property in propertyTypeDTOList)
                    {
                        var propertyViewModel = new PropertyTypeViewModel();
                        propertyViewModel.CultureID = property.CultureID;
                        propertyViewModel.PropertyTypeID = property.PropertyTypeID;
                        propertyViewModel.PropertyTypeName = property.PropertyTypeName;
                        propertyTypeViewModelList.Add(propertyViewModel);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper<ProductController>.Fatal(ex.Message.ToString(), ex);
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

            return Json(propertyTypeViewModelList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPropertiesByPropertyTypeID(decimal propertyTypeIID)
        {
            string properties = string.Empty;
            var propertyDTOList = new List<PropertyDTO>();
            var propertyViewModelList = new List<PropertyViewModel>();
            string productServiceUrl = ServiceHost + Constants.PRODUCT_DETAIL_SERVICE_NAME;

            try
            {
                propertyDTOList = ClientFactory.ProductDetailServiceClient(CallContext).GetPropertiesByPropertyTypeID(propertyTypeIID);

                if (propertyDTOList != null && propertyDTOList.Count > 0)
                {
                    foreach (var propertyDTO in propertyDTOList)
                    {
                        var propertyViewModel = new PropertyViewModel();
                        propertyViewModel.PropertyIID = propertyDTO.PropertyIID;
                        propertyViewModel.PropertyName = propertyDTO.PropertyName;
                        propertyViewModelList.Add(propertyViewModel);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper<ProductController>.Fatal(ex.Message.ToString(), ex);
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            return Json(propertyViewModelList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult List()
        {
            // call the service and get the meta data
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList(Services.Contracts.Enums.SearchView.Product);

            return View(new SearchListViewModel
            {
                ControllerName = Infrastructure.Enums.SearchView.Product.ToString(),
                ViewName = Infrastructure.Enums.SearchView.Product,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                FilterColumns = metadata.QuickFilterColumns,
                InfoBar = @"<li class='status-label-mobile'>
                            <div class='right status-label'>
                                <div class='status-label-color'><label class='status-color-label green'></label>Publish</div>
                                <div class='status-label-color'><label class='status-color-label orange'></label>Review</div>
                                <div class='status-label-color'><label class='status-color-label red'></label>Cancelled</div>
                            </div>
                        </li>",
                IsChild = false,
                HasChild = true,
                HasFilters = metadata.HasFilters,
            });
        }

        [HttpGet]
        public ActionResult ChildList(long ID)
        {
            // call the service and get the meta data
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList(Services.Contracts.Enums.SearchView.ProductSKU);

            return PartialView("_ChildList", new SearchListViewModel
            {
                ViewName = Infrastructure.Enums.SearchView.ProductSKU,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                FilterColumns = metadata.QuickFilterColumns,
                RuntimeFilter = Convert.ToString("ProductIID=" + ID.ToString()).Trim(), // child view must have this column
                IsChild = true,
                ParentViewName = Infrastructure.Enums.SearchView.Product,
                IsEditableLink = false
            });
        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View("DetailedView");
        }

        [HttpGet]
        public JsonResult GetCategoryList(string searchText)
        {
            string categoryList = string.Empty;
            var categoryDTOList = new List<CategoryDTO>();
            var categoryModelList = new List<ProductCategoryViewModel>();

            try
            {
                categoryDTOList = ClientFactory.CategoryServiceClient(CallContext).SearchCategories(searchText);

                if (categoryDTOList != null && categoryDTOList.Count > 0)
                {
                    foreach (var categoryDTO in categoryDTOList)
                    {
                        var category = new ProductCategoryViewModel();
                        category.CategoryID = categoryDTO.CategoryIID;
                        category.CategoryName = categoryDTO.CategoryName;
                        categoryModelList.Add(category);
                    }
                }

            }
            catch (Exception ex)
            {
                LogHelper<ProductController>.Fatal(ex.Message.ToString(), ex);
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

            return Json(categoryModelList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProperitesByProductFamilyID(long productFamilyID)
        {
            var propertyViewModelList = new List<PropertyViewModel>();
            var propertyDTOList = new List<PropertyDTO>();
            string productServiceUrl = ServiceHost + Constants.PRODUCT_DETAIL_SERVICE_NAME;

            try
            {
                propertyDTOList = ClientFactory.ProductDetailServiceClient(CallContext).GetProperitesByProductFamilyID(productFamilyID);

                if (propertyDTOList != null && propertyDTOList.Count > 0)
                {
                    foreach (var property in propertyDTOList)
                    {
                        var propertyViewModel = new PropertyViewModel();
                        propertyViewModel.PropertyIID = property.PropertyIID;
                        propertyViewModel.PropertyName = property.PropertyName;

                        propertyViewModelList.Add(propertyViewModel);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper<ProductController>.Fatal(ex.Message.ToString(), ex);
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

            return Json(propertyViewModelList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "", string runtimeFilter2 = "")
        {
            var view = Infrastructure.Enums.SearchView.Product;

            if (runtimeFilter.Trim().IsNotNullOrEmpty())
            {
                view = Infrastructure.Enums.SearchView.ProductSKU;
            }
            //summary view filter
            else if (runtimeFilter2.Trim().IsNotNullOrEmpty())
            {
                view = Infrastructure.Enums.SearchView.Product;
                runtimeFilter = runtimeFilter2;
            }

            return base.SearchData(view, currentPage, orderBy, runtimeFilter);
        }

        [HttpGet]
        public JsonResult SearchSummaryData()
        {
            return base.SearchSummaryData(Infrastructure.Enums.SearchView.ProductSummary);
        }

        public ActionResult Edit(long ID)
        {
            return RedirectToAction("Product", new { productIID = ID });
        }

        public ActionResult Create()
        {
            return RedirectToAction("Product");
        }

        public ActionResult _UploadProductImages()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult UploadProductImages(long existingFileNameIndex = 0)
        {
            bool isSavedSuccessfully = false;
            List<string> fileNames = new List<string>();
            var imageInfoList = new List<UploadedFileDetailsViewModel>();

            try
            {
                var userID = base.CallContext.LoginID;//Needs to fetch from call context
                string tempFolderPath = string.Format("{0}\\{1}\\{2}\\{3}", ConfigurationExtensions.GetAppConfigValue("ImagesPhysicalPath").ToString(), EduegateImageTypes.Products, Constants.TEMPFOLDER, userID);

                var fileNameIndex = existingFileNameIndex > 0 ? existingFileNameIndex + 1 : GetLastUploadedImageName(tempFolderPath);


                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    if (file != null && file.ContentLength > 0)
                    {
                        var imageInfo = new UploadedFileDetailsViewModel();
                        var imageExtension = System.IO.Path.GetExtension(file.FileName).ToLower();
                        var imageFileName = fileNameIndex + imageExtension;

                        if (!Directory.Exists(tempFolderPath))
                        {
                            Directory.CreateDirectory(tempFolderPath);
                        }

                        using (var fileStream = System.IO.File.Create(tempFolderPath + "/" + imageFileName))
                        {
                            file.InputStream.Seek(0, SeekOrigin.Begin);
                            file.InputStream.CopyTo(fileStream);
                            file.InputStream.Close();
                            file.InputStream.Dispose();
                        }


                        //file.SaveAs(tempFolderPath + "/" + imageFileName);

                        imageInfo.FileName = imageFileName;
                        imageInfo.FilePath = Path.Combine(ConfigurationExtensions.GetAppConfigValue("ImageHostUrl").ToString(), EduegateImageTypes.Products.ToString(), Constants.TEMPFOLDER, userID.Value.ToString(), imageFileName);
                        imageInfoList.Add(imageInfo);
                        fileNameIndex++;
                    }
                }

                isSavedSuccessfully = true;
            }
            catch (Exception ex)
            {
                LogHelper<ProductController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { Success = isSavedSuccessfully, Message = (ex.Message), JsonRequestBehavior.AllowGet });
            }

            return Json(new { Success = isSavedSuccessfully, FileInfo = imageInfoList }, JsonRequestBehavior.AllowGet);
        }

        private long GetLastUploadedImageName(string path)
        {
            var lastUploadedImageName = 1;

            if (Directory.Exists(path))
            {
                var existingfileNames = (from s in (Directory.GetFiles(path, "*.*").Select(Path.GetFileName)) select Convert.ToInt32(s.Split('.')[0])).ToArray();
                lastUploadedImageName = existingfileNames.Count() > 0 ? existingfileNames.Max() + 1 : 1;
            }
            return lastUploadedImageName;
        }

        [HttpPost]
        public ActionResult DeleteUploadProductImages(string fileName, long productID = 0)
        {
            bool isSuccessFullyDeleted = false;

            try
            {
                var userID = base.CallContext.LoginID;//Needs to fech from call context
                if (!string.IsNullOrEmpty(fileName))
                {
                    string tempFolderPath = string.Format("{0}\\{1}\\{2}\\{3}\\{4}", ConfigurationExtensions.GetAppConfigValue("ImagesPhysicalPath").ToString(), EduegateImageTypes.Products, Constants.TEMPFOLDER, userID, fileName);

                    if (System.IO.File.Exists(tempFolderPath))
                    {
                        System.IO.File.Delete(tempFolderPath);
                    }
                    isSuccessFullyDeleted = true;

                    if (productID != 0)
                    {
                        string productPath = string.Format("{0}\\{1}\\{2}\\{3}\\{4}", ConfigurationExtensions.GetAppConfigValue("ImagesPhysicalPath").ToString(), EduegateImageTypes.Products, productID, ImageType.LargeImage.ToString(), fileName);

                        if (System.IO.File.Exists(productPath))
                        {
                            System.IO.File.Delete(productPath);
                        }

                        productPath = string.Format("{0}\\{1}\\{2}\\{3}\\{4}", ConfigurationExtensions.GetAppConfigValue("ImagesPhysicalPath").ToString(), EduegateImageTypes.Products, productID, ImageType.ListingImage.ToString(), fileName);

                        if (System.IO.File.Exists(productPath))
                        {
                            System.IO.File.Delete(productPath);
                        }

                        productPath = string.Format("{0}\\{1}\\{2}\\{3}\\{4}", ConfigurationExtensions.GetAppConfigValue("ImagesPhysicalPath").ToString(), EduegateImageTypes.Products, productID, ImageType.SmallImage.ToString(), fileName);

                        if (System.IO.File.Exists(productPath))
                        {
                            System.IO.File.Delete(productPath);
                        }

                        productPath = string.Format("{0}\\{1}\\{2}\\{3}\\{4}", ConfigurationExtensions.GetAppConfigValue("ImagesPhysicalPath").ToString(), EduegateImageTypes.Products, productID, ImageType.ThumbnailImage.ToString(), fileName);

                        if (System.IO.File.Exists(productPath))
                        {
                            System.IO.File.Delete(productPath);
                        }

                        isSuccessFullyDeleted = true;
                    }

                }
                else
                {
                    var file = string.Format("{0}\\{1}\\{2}\\{3}", ConfigurationExtensions.GetAppConfigValue("ImagesPhysicalPath").ToString(), EduegateImageTypes.Products, Constants.TEMPFOLDER, userID);
                    if (System.IO.Directory.Exists(file))
                        System.IO.Directory.Delete(file, true);
                }
            }
            catch (Exception ex)
            {
                LogHelper<ProductController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { Success = isSuccessFullyDeleted, Message = (ex.Message), JsonRequestBehavior.AllowGet });
            }

            return Json(new { Success = isSuccessFullyDeleted });
        }

        public bool ResizeProductImage(long userID, ImageType imageType)
        {
            bool isImageResized = false;
            var imgAttributeList = new List<ImageAttributes>();
            try
            {
                string tempFolderPath = string.Format("{0}\\{1}\\{2}\\{3}", ConfigurationExtensions.GetAppConfigValue("ImagesPhysicalPath").ToString(), EduegateImageTypes.Products, Constants.TEMPFOLDER, userID);
                string DesignationTempFolderPath = string.Format("{0}\\{1}\\{2}\\{3}\\{4}", ConfigurationExtensions.GetAppConfigValue("ImagesPhysicalPath").ToString(), EduegateImageTypes.Products, Constants.TEMPFOLDER, userID, imageType.ToString());

                if (Directory.Exists(tempFolderPath))
                {
                    string[] imageNames = Directory.GetFiles(tempFolderPath).Select(path => Path.GetFileName(path)).ToArray();
                    if (imageNames != null && imageNames.Count() > 0)
                    {
                        if (!Directory.Exists(DesignationTempFolderPath))
                        {
                            Directory.CreateDirectory(DesignationTempFolderPath);
                        }

                        foreach (var image in imageNames)
                        {
                            var imgAttribute = new ImageAttributes();
                            imgAttribute.SourceImageName = image;
                            imgAttribute.SourcePath = tempFolderPath;
                            imgAttribute.DestinationImageName = image;
                            imgAttribute.DestinationImageType = imageType;
                            imgAttribute.DestinationImagePath = DesignationTempFolderPath;
                            imgAttributeList.Add(imgAttribute);
                        }

                        ImageResize imageResize = new ImageResize();
                        var isSuc = imageResize.Resize(imgAttributeList);
                        isImageResized = true;
                    }
                }

            }
            catch (Exception ex)
            {
                LogHelper<ProductController>.Fatal(ex.Message.ToString(), ex);
            }

            return isImageResized;
        }

        [HttpGet]

        public ActionResult ProductToProductMaps()
        {
            return View("_ProductToProductMaps");
        }

        [HttpGet]
        public JsonResult GetProductListBySearch(string searchText = "", long excludeProductFamilyID = default(long))
        {
            bool hasError = false;
            var productList = new List<ProductMapViewModel>();
            try
            {
                productList = GetProductListBySearchText(searchText, excludeProductFamilyID);
                hasError = false;
            }
            catch (Exception ex)
            {
                hasError = true;
                LogHelper<ProductController>.Fatal(ex.Message.ToString(), ex);
            }
            return Json(new { IsError = hasError, ProductList = productList }, JsonRequestBehavior.AllowGet);
        }


        public List<ProductMapViewModel> GetProductListBySearchText(string searchText = "", long excludeProductFamilyID = default(long))
        {
            var productList = new List<ProductMapViewModel>();
            try
            {
                var productDTOList = ClientFactory.ProductDetailServiceClient(CallContext).GetProductListBySearchText(searchText, excludeProductFamilyID);

                foreach (var productDTO in productDTOList)
                {
                    var product = new ProductMapViewModel();
                    product.ProductID = productDTO.ProductID;
                    product.ProductName = productDTO.ProductName;
                    productList.Add(product);
                }
            }
            catch (Exception ex)
            {
                LogHelper<ProductController>.Fatal(ex.Message.ToString(), ex);
            }

            return productList;
        }

        public JsonResult GetProductListByCategoryID(string categoryID)
        {
            var productList = new List<ProductMapViewModel>();
            try
            {
                var productDTOList = ClientFactory.ProductDetailServiceClient(CallContext).GetProductListByCategoryID(long.Parse(categoryID));

                foreach (var productDTO in productDTOList)
                {
                    var product = new ProductMapViewModel();
                    product.ProductID = productDTO.ProductID;
                    product.ProductName = productDTO.ProductName;
                    productList.Add(product);
                }
            }
            catch (Exception ex)
            {
                LogHelper<ProductController>.Fatal(ex.Message.ToString(), ex);
            }

            return Json(productList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveProductMaps(ProductToProductMapViewModel productMaps)
        {
            var productMapDTO = new ProductToProductMapDTO();
            productMapDTO.ToProduct = new List<ProductMapDTO>();
            bool isUpdated = false;
            try
            {
                if (productMaps != null)
                {
                    productMapDTO.FromProduct = new ProductMapDTO();
                    if (productMapDTO.FromProduct != null)
                    {
                        productMapDTO.FromProduct.ProductID = productMaps.FromProduct.ProductID;
                        productMapDTO.FromProduct.ProductName = productMaps.FromProduct.ProductName;
                    }
                    if (productMaps.SalesRelationshipTypes != null && productMaps.SalesRelationshipTypes.Count > 0)
                    {
                        productMapDTO.SalesRelationshipTypes = productMaps.SalesRelationshipTypes.Select(x => x.IsNull() ? null : SalesRelationshipTypeViewModel.ToDTO(x)).ToList();
                        //foreach (var relation in productMaps.SalesRelationshipTypes)
                        //{
                        //    var productMap = new ProductMapDTO();
                        //    foreach (var item in relation.ToProduct)
                        //    {
                        //        productMap.ProductToProductMapID = item.ProductToProductMapID;
                        //        productMap.ProductID = map.ProductID;
                        //        productMap.SalesRelationShipType = (int)SalesRelationShipTypes.UpSell;
                        //        productMapDTO.ToProduct.Add(productMap);
                        //    }
                        //}
                    }
                }
                isUpdated = ClientFactory.ProductDetailServiceClient(CallContext).SaveProductMaps(productMapDTO);
            }
            catch (Exception ex)
            {
                LogHelper<ProductController>.Fatal(ex.Message.ToString(), ex);
            }

            return Json(isUpdated, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProductMaps(long productID)
        {
            var productMapViewModel = new ProductToProductMapViewModel();
            productMapViewModel.FromProduct = new ProductMapViewModel();
            bool hasError = true;
            try
            {
                var productMapDTO = ClientFactory.ProductDetailServiceClient(CallContext).GetProductMaps(productID);

                if (productMapDTO != null)
                {
                    if (productMapDTO.FromProduct != null)
                    {
                        productMapViewModel.FromProduct.ProductID = productMapDTO.FromProduct.ProductID;
                        productMapViewModel.FromProduct.ProductName = productMapDTO.FromProduct.ProductName;
                    }
                    //if (productMapDTO.ToProduct != null && productMapDTO.ToProduct.Count>0)
                    //{
                    //    productMapViewModel.ToProduct = new List<ProductMapViewModel>();
                    //    foreach(var toProduct in productMapDTO.ToProduct)
                    //    {
                    //        var toProductViewModel = new ProductMapViewModel();
                    //        toProductViewModel.ProductID = toProduct.ProductID;
                    //        toProductViewModel.ProductName = toProduct.ProductName;
                    //        toProductViewModel.ProductToProductMapID = toProduct.ProductToProductMapID;
                    //        toProductViewModel.SalesRelationShipType = toProduct.SalesRelationShipType;
                    //        productMapViewModel.ToProduct.Add(toProductViewModel);
                    //    }
                    //}
                }
            }

            catch (Exception ex)
            {
                LogHelper<ProductController>.Fatal(ex.Message.ToString(), ex);
            }

            return Json(new { IsError = hasError, ProductMap = productMapViewModel }, JsonRequestBehavior.AllowGet);
        }

        public List<SalesRelationshipTypeViewModel> GetSalesRelationshipTypeList(SalesRelationShipTypes salesRelationShipTypes, long productID)
        {
            List<SalesRelationshipTypeViewModel> vmListSalesRelationshipType = new List<SalesRelationshipTypeViewModel>();

            var salesRelationshipTypeDTO = ClientFactory.ProductDetailServiceClient(CallContext).GetSalesRelationshipTypeList(salesRelationShipTypes, productID);

            if (salesRelationshipTypeDTO != null)
            {
                vmListSalesRelationshipType = salesRelationshipTypeDTO.Select(x => SalesRelationshipTypeViewModel.FromDTO(x)).ToList();
            }

            return vmListSalesRelationshipType;
        }

        /// <summary>
        /// get Relationship type list based on enum param
        /// </summary>
        /// <param name="salesRelationShipTypes">enum</param>
        /// <returns>list of relationship </returns>
        public List<SalesRelationshipTypeViewModel> GetSalesRelationshipTypeList(SalesRelationShipTypes salesRelationShipTypes)
        {
            var listSalesRelationshipType = new List<SalesRelationshipTypeViewModel>();
            var listSalesRelationshipTypeDTO = ClientFactory.ProductDetailServiceClient(CallContext).GetSalesRelationshipType(salesRelationShipTypes);
            listSalesRelationshipType = listSalesRelationshipTypeDTO.Select(x => SalesRelationshipTypeViewModel.FromDTO(x)).ToList();
            return listSalesRelationshipType;
        }

        public ProductMapViewModel GetProductDetailByProductId(long productID)
        {
            var productMapViewModel = new ProductMapViewModel();
            var dto = ClientFactory.ProductDetailServiceClient(CallContext).GetProductDetailByProductId(productID);

            var productMapDTO = new ProductMapDTO();
            if (dto.IsNotNull())
            {
                productMapDTO.ProductID = dto.ProductID;
                productMapDTO.ProductName = dto.ProductName;
            }

            productMapViewModel = ProductMapViewModel.FromDTO(productMapDTO);
            return productMapViewModel;
        }

        public ProductToProductMapViewModel GetProductToProductMapDetail(long productID)
        {
            ProductToProductMapViewModel productToProductMapViewModel = new ProductToProductMapViewModel();

            if (productID.IsNull() || productID <= 0)
            {
                productToProductMapViewModel.FromProduct = new ProductMapViewModel();
                productToProductMapViewModel.ProductList = GetProductListBySearchText();
                productToProductMapViewModel.SalesRelationshipTypes = GetSalesRelationshipTypeList(SalesRelationShipTypes.All);
                productToProductMapViewModel.IsFromProductDisabled = true;
            }
            else
            {
                productToProductMapViewModel.ProductList = GetProductListBySearchText();
                productToProductMapViewModel.SalesRelationshipTypes = GetSalesRelationshipTypeList(SalesRelationShipTypes.All, productID);
                productToProductMapViewModel.FromProduct = GetProductDetailByProductId(productID);
                productToProductMapViewModel.IsFromProductDisabled = true;
            }
            return productToProductMapViewModel;
        }

        [HttpGet]
        public JsonResult GetProductToProductMap(long productID)
        {
            bool hasError = false;
            ProductToProductMapViewModel productToProductMapViewModel = new ProductToProductMapViewModel();
            productToProductMapViewModel = GetProductToProductMapDetail(productID);

            if (productToProductMapViewModel.IsNotNull())
            {
                hasError = true;
            }
            return Json(new { IsError = hasError, ProductMap = productToProductMapViewModel }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProductTags()
        {
            List<ProductTagViewModel> vmTags = new List<ProductTagViewModel>();
            ProductTagViewModel vmTag = null;

            try
            {
                List<PropertyDTO> propertyDTOList = ClientFactory.UtilitiyServiceClient(CallContext).GetProperties(((byte)PropertyTypes.ProductTag).ToString());

                propertyDTOList = (from property in propertyDTOList
                                   where Convert.ToInt32(property.PropertyTypeID) == (int)PropertyType.ProductTag
                                   select property).ToList();

                if (propertyDTOList != null && propertyDTOList.Count > 0)
                {
                    foreach (PropertyDTO propertyDTO in propertyDTOList)
                    {
                        vmTag = new ProductTagViewModel();

                        vmTag.TagIID = propertyDTO.PropertyIID;
                        vmTag.TagName = propertyDTO.PropertyName;

                        vmTags.Add(vmTag);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper<ProductController>.Info("Exception : " + ex.Message.ToString());
                return Json(new { IsError = true, ErrorMessage = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { IsError = false, ProductTags = vmTags }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetSKUTags()
        {
            try
            {
                return Json(new { IsError = false, SKUTags = KeyValueViewModel.FromDTO(ClientFactory.ProductDetailServiceClient(CallContext).GetProdctSKUTags()) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogHelper<ProductController>.Info("Exception : " + ex.Message.ToString());
                return Json(new { IsError = true, ErrorMessage = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult ProductBundleMaps()
        {
            return PartialView("_ProductBundleMaps");
        }

        [HttpGet]
        public ActionResult ProductConfig(bool isRoot = false)
        {
            return PartialView("_ProductConfig", isRoot);
        }

        [HttpGet]
        public ActionResult SeoMetadata()
        {
            return PartialView("_SeoMetadata", new SeoMetadataViewModel());
        }

        [HttpGet]
        public ActionResult PriceSettings(string skuIds)
        {
            return PartialView("_PriceSettings", skuIds.Split(',').ToList());
        }

        [HttpPost]
        public JsonResult CreateProperties(List<ProductTagViewModel> productTags)
        {
            List<PropertyDTO> dtoPropertyList = new List<PropertyDTO>();
            List<ProductTagViewModel> dbProductTags = new List<ProductTagViewModel>();
            PropertyDTO dtoProperty = null;
            ProductTagViewModel productTag = null;

            if (productTags.IsNotNull() && productTags.Count > 0)
            {
                foreach (ProductTagViewModel vmProductTag in productTags)
                {
                    dtoProperty = new PropertyDTO();
                    dtoProperty.PropertyIID = Convert.ToInt64(vmProductTag.TagIID);
                    dtoProperty.PropertyName = vmProductTag.TagName;
                    dtoProperty.PropertyTypeID = (byte)PropertyTypes.ProductTag;

                    dtoPropertyList.Add(dtoProperty);
                }

                dtoPropertyList = ClientFactory.ProductDetailServiceClient(CallContext).CreateProperties(dtoPropertyList);

                if (dtoPropertyList.IsNotNull() && dtoPropertyList.Count > 0)
                {
                    foreach (PropertyDTO propertyDTO in dtoPropertyList)
                    {
                        productTag = new ProductTagViewModel();
                        productTag.TagIID = propertyDTO.PropertyIID;
                        productTag.TagName = propertyDTO.PropertyName;

                        dbProductTags.Add(productTag);
                    }
                }
            }

            return Json(new { IsError = false, ProductTags = dbProductTags }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult _UploadProductVideos()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult UploadProductVideos(long existingFileNameIndex = 0)
        {
            bool isSavedSuccessfully = false;
            List<string> fileNames = new List<string>();
            var videoInfoList = new List<UploadedFileDetailsViewModel>();

            try
            {
                var userID = base.CallContext.LoginID;
                string tempFolderPath = string.Format("{0}\\{1}\\{2}\\{3}", ConfigurationExtensions.GetAppConfigValue("ImagesPhysicalPath").ToString(), EduegateImageTypes.ProductVideos, userID, Constants.TEMPFOLDER);
                var fileNameIndex = existingFileNameIndex > 0 ? existingFileNameIndex + 1 : GetLastUploadedVideoName(tempFolderPath);

                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    if (file != null && file.ContentLength > 0)
                    {
                        var videoInfo = new UploadedFileDetailsViewModel();
                        var videoExtension = System.IO.Path.GetExtension(file.FileName).ToLower();
                        var videoFileName = fileNameIndex + videoExtension;

                        if (!Directory.Exists(tempFolderPath))
                        {
                            Directory.CreateDirectory(tempFolderPath);
                        }

                        file.SaveAs(tempFolderPath + "/" + videoFileName);

                        videoInfo.FileName = videoFileName;
                        videoInfo.FilePath = Path.Combine(ConfigurationExtensions.GetAppConfigValue("ImageHostUrl").ToString(),
                            EduegateImageTypes.ProductVideos.ToString(), userID.ToString(), Constants.TEMPFOLDER, videoFileName);
                        videoInfoList.Add(videoInfo);
                        fileNameIndex++;
                    }
                }

                isSavedSuccessfully = true;
            }
            catch (Exception ex)
            {
                LogHelper<ProductController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { Success = isSavedSuccessfully, Message = (ex.Message), JsonRequestBehavior.AllowGet });
            }

            return Json(new { Success = isSavedSuccessfully, FileInfo = videoInfoList }, JsonRequestBehavior.AllowGet);
        }

        private long GetLastUploadedVideoName(string path)
        {
            var lastUploadedVideoName = 1;

            if (Directory.Exists(path))
            {
                var existingfileNames = (from s in (Directory.GetFiles(path, "*.*").Select(Path.GetFileName)) select Convert.ToInt32(s.Split('.')[0])).ToArray();
                lastUploadedVideoName = existingfileNames.Count() > 0 ? existingfileNames.Max() + 1 : 1;
            }
            return lastUploadedVideoName;
        }

        [HttpPost]
        public ActionResult DeleteUploadProductVideos(string fileName)
        {
            bool isSuccessFullyDeleted = false;

            try
            {
                var userID = base.CallContext.LoginID;

                if (!string.IsNullOrEmpty(fileName))
                {
                    string tempFolderPath = string.Format("{0}\\{1}\\{2}\\{3}\\{4}", ConfigurationExtensions.GetAppConfigValue("ImagesPhysicalPath").ToString(), EduegateImageTypes.ProductVideos, userID, Constants.TEMPFOLDER, fileName);

                    if (System.IO.File.Exists(tempFolderPath))
                    {
                        System.IO.File.Delete(tempFolderPath);
                    }

                    isSuccessFullyDeleted = true;

                }
                else
                {
                    var file = string.Format("{0}\\{1}\\{2}", ConfigurationExtensions.GetAppConfigValue("ImagesPhysicalPath").ToString(), EduegateImageTypes.ProductVideos, userID);
                    if (System.IO.Directory.Exists(file))
                        System.IO.Directory.Delete(file, true);
                }
            }
            catch (Exception ex)
            {
                LogHelper<ProductController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { Success = isSuccessFullyDeleted, Message = (ex.Message), JsonRequestBehavior.AllowGet });
            }

            return Json(new { Success = isSuccessFullyDeleted });
        }

        /// <summary>
        /// get list of Delivery Type Master 
        /// </summary>
        /// <returns> list of delivery master </returns>
        public List<DeliveryTypeViewModel> GetDeliveryTypeMaster()
        {
            string deliveryTypeMaster = string.Empty;
            // service call
            var dtos = ClientFactory.ReferenceDataServiceClient(CallContext).GetDeliveryTypeMaster();
            // string json convert to Dtos
            // dto to view model
            var viewmodels = dtos.Select(x => DeliveryTypeViewModel.ToViewModel(x)).ToList();
            return viewmodels;
        }

        public List<PackingTypeViewModel> GetPackingTypeMaster()
        {
            string packingTypeMaster = string.Empty;
            var dtos = new List<PackingTypeDTO>();
            var viewModel = new List<PackingTypeViewModel>();
            var referenceServiceClient = ClientFactory.ReferenceDataServiceClient(CallContext);
            //Service call
            dtos = referenceServiceClient.GetPackingTypeMaster();
            // dto to view model
            viewModel = dtos.Select(x => PackingTypeViewModel.ToViewModel(x)).ToList();
            return viewModel;
        }

        protected void SaveEntityTypeRelationMaps(long productID, List<KeyValueViewModel> lists)
        {
            if (lists.IsNull())
                return;

            EntityTypeRelationDTO dto = new EntityTypeRelationDTO();
            dto.FromEntityTypes = Eduegate.Framework.Contracts.Common.Enums.EntityTypes.Product;
            dto.ToEntityTypes = Eduegate.Framework.Contracts.Common.Enums.EntityTypes.Employee;
            dto.FromRelaionID = productID;
            dto.ToRelaionIDs = new List<long>();
            foreach (var list in lists)
            {
                dto.ToRelaionIDs.Add(Convert.ToInt64(list.Key));
            }

            // Service Call.
            ClientFactory.MutualServiceClient(CallContext).SaveEntityTypeRelationMaps(dto);
        }

        public List<KeyValueViewModel> GetEmployeeIdNameEntityTypeRelation(long productID)
        {
            EntityTypeRelationDTO dto = new EntityTypeRelationDTO();
            dto.FromEntityTypes = Eduegate.Framework.Contracts.Common.Enums.EntityTypes.Product;
            dto.ToEntityTypes = Eduegate.Framework.Contracts.Common.Enums.EntityTypes.Employee;
            dto.FromRelaionID = productID;

            // Service Call.
            List<KeyValueDTO> dtoKeyValueOwners = ClientFactory.MutualServiceClient(CallContext).GetEmployeeIdNameEntityTypeRelation(dto);
            List<KeyValueViewModel> vmKeyValueOwners = dtoKeyValueOwners.Select(x => KeyValueViewModel.ToViewModel(x)).ToList();

            return vmKeyValueOwners;
        }

        /// <summary>
        /// get the List<KeyValueViewModel> using GetProductListWithSKU service
        /// </summary>
        /// <param name="searchText"> for search keyword </param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetProducts(string searchText)
        {
            List<KeyValueViewModel> vmKeyValues = new List<KeyValueViewModel>();
            int dataSize = Convert.ToInt32(ConfigurationExtensions.GetAppConfigValue("Select2DataSize"));

            List<POSProductDTO> posProductItems = ClientFactory.ProductCatalogServiceClient(CallContext).GetProductListWithSKU(searchText, dataSize);

            // Add into List<KeyValueViewModel> foreach
            posProductItems.ToList().ForEach(x =>
            {
                vmKeyValues.Add(new KeyValueViewModel { Key = x.ProductSKUMapIID.ToString(), Value = x.SKU });
            });
            return Json(vmKeyValues, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProductAndSKUByID(long productSKUMapID)
        {
            return Json(ExternalProductSettingsViewModel.POSProductDTOtoExternalProductSettingsViewModel(ClientFactory.ProductCatalogServiceClient(CallContext).GetProductAndSKUByID(productSKUMapID)), JsonRequestBehavior.AllowGet);
        }

        public List<ProductDeliveryCountrySettingViewModel> GetCountries()
        {
            var dtos = new List<CountryDTO>();
            var viewModel = new List<ProductDeliveryCountrySettingViewModel>();

            //Service call
            dtos = ClientFactory.ReferenceDataServiceClient(CallContext).GetCountries(false);
            // dto to view model
            viewModel = dtos.Select(x => ProductDeliveryCountrySettingViewModel.ToSelect2DeliverySetting(x)).ToList();
            return viewModel;
        }
    }
}