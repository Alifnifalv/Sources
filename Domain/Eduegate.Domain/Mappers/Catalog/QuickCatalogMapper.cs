using Newtonsoft.Json;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Helper;
using Eduegate.Logger;
using Eduegate.Utilities.ImageScalar;
using System.IO;
using static Eduegate.Utilities.ImageScalar.Contracts;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Mappers.Catalog
{
    public class QuickCatalogMapper : DTOEntityDynamicMapper
    {
        Product _product;
        List<string> validationFields = new List<string>() { "PartNumber", "BarCode", "Product", "ProductDESC" };

        public static QuickCatalogMapper Mapper(CallContext context)
        {
            var mapper = new QuickCatalogMapper();
            mapper._context = context;
            mapper._product = null;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<SKUDTO>(entity);
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as SKUDTO;

            //if (toDto.PartNumber ==null || toDto.PartNumber=="")
            //{
            //    throw new Exception("Please fill Part number !");
            //}

            //if (toDto.BarCode == null || toDto.BarCode == "")
            //{
            //    throw new Exception("Please fill Bar code !");
            //}

            //if (toDto.UnitID == null || toDto.UnitID == 0)
            //{
            //    throw new Exception("Please fill Unit !");
            //}

            if (toDto.Categories.Count <= 0)
            {
                throw new Exception("Please select Categories !");
            }

            if (toDto.ProductTypeID == null || toDto.ProductTypeID == 0)
            {
                throw new Exception("Please fill Product Type !");
            }

            var errorMessage = string.Empty;

            //validate first
            foreach (var field in validationFields)
            {
                var isValid = ValidateField(toDto, field);

                if (isValid.Key.Equals("true"))
                {
                    errorMessage = string.Concat(errorMessage, "-", isValid.Value, "<br>");
                }
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                throw new Exception(errorMessage);
            }

            var entity = ToEntity(toDto);

            var existingProductCategoryMapList = new List<ProductCategoryMap>();

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                existingProductCategoryMapList = dbContext.ProductCategoryMaps
                    .Include(i => i.FeeDueInventoryMaps)
                    .Where(c => c.ProductID == entity.ProductIID).AsNoTracking().ToList();

                if (existingProductCategoryMapList.Any(x => x.FeeDueInventoryMaps.Count == 0))
                {
                    dbContext.ProductCategoryMaps.RemoveRange(existingProductCategoryMapList);
                }

                var existingProductSKURackMaps = dbContext.ProductSKURackMaps
                    .Where(c => c.ProductID == entity.ProductIID).AsNoTracking().ToList();

                if (existingProductSKURackMaps != null && existingProductSKURackMaps.Count > 0)
                {
                    dbContext.ProductSKURackMaps.RemoveRange(existingProductSKURackMaps);
                }

                dbContext.SaveChanges();
            }

            var productID = new ProductDetailRepository().UpdateProduct(entity, null, 0);
            _product = new ProductDetailRepository().GetProduct(productID);
            var productSKUMap = _product.ProductSKUMaps.FirstOrDefault();

            var priceListSKUMapList = new List<ProductPriceListSKUMap>();
            foreach (var price in toDto.ProductPrices)
            {
                if (price.ProductPriceListID.HasValue && price.ProductPriceListID.Value != 0)
                {
                    priceListSKUMapList.Add(new ProductPriceListSKUMap()
                    {
                        CompanyID = _context.CompanyID.Value,
                        DiscountPercentage = price.DiscountPercentage,
                        //DiscountPrice = price.Discount,
                        Price = price.Price,
                        //ProductID = toDto.ProductID,
                        ProductPriceListID = price.ProductPriceListID,
                        //ProductPriceListProductMapIID = price.ProductPriceListProductMapIID,
                        ProductSKUID = productSKUMap.ProductSKUMapIID,
                        Amount = price.Price,
                        Discount = price.Discount,
                        Cost = price.Price,
                        PricePercentage = price.DiscountPercentage,
                        ProductPriceListItemMapIID = price.ProductPriceListProductMapIID,
                    });
                }
            }

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var IIDs = toDto.ProductBundle
                        .Select(a => a.BundleIID).ToList();
                //delete maps
                var bundentities = dbContext.ProductBundles.Where(x =>
                    (x.FromProductSKUMapID != toDto.ProductSKUMapID) && (x.ToProductSKUMapID == toDto.ProductSKUMapID) &&
                    !IIDs.Contains(x.BundleIID)).AsNoTracking().ToList();

                if (bundentities.IsNotNull())
                    dbContext.ProductBundles.RemoveRange(bundentities);

                dbContext.SaveChanges();
            }

            var bundleEntities = new List<ProductBundle>();
            foreach (var bundle in toDto.ProductBundle)
            {
                if (bundle.Quantity.HasValue && bundle.Quantity.Value != 0)
                {
                    bundleEntities.Add(new ProductBundle()
                    {
                        BundleIID = bundle.BundleIID,
                        CostPrice = bundle.CostPrice,
                        ToProductID = productSKUMap.ProductID,
                        FromProductSKUMapID = bundle.FromProductSKUMapID,
                        Quantity = bundle.Quantity.HasValue ? bundle.Quantity : 0,
                        SellingPrice = bundle.SellingPrice,
                        FromProductID = bundle.FromProductID,
                        ToProductSKUMapID = productSKUMap.ProductSKUMapIID,
                    });
                }
            }

            var categoryEntities = new List<ProductCategoryMap>();

            if (toDto.Categories.Count > 0)
            {
                foreach (KeyValueDTO categ in toDto.Categories)
                {
                    categoryEntities.Add(new ProductCategoryMap()
                    {
                        ProductID = entity.ProductIID,
                        CategoryID = long.Parse(categ.Key),
                    });
                }
            }

            var allergyEntities = new List<ProductAllergyMap>();
            if (toDto.Allergies.Count > 0)
            {
                foreach (KeyValueDTO allegry in toDto.Allergies)
                {
                    allergyEntities.Add(new ProductAllergyMap()
                    {
                        ProductID = entity.ProductIID,
                        AllergyID = int.Parse(allegry.Key),
                    });
                }
            }

            var rackEntities = new List<ProductSKURackMap>();
            if (toDto.Rack.Count > 0)
            {
                foreach (KeyValueDTO rack in toDto.Rack)
                {
                    rackEntities.Add(new ProductSKURackMap()
                    {
                        ProductSKUMapID = productSKUMap.ProductSKUMapIID,
                        ProductID = entity.ProductIID,
                        RackID = long.Parse(rack.Key),
                    });
                }
            }

            new PriceSettingsRepository().SaveSKUPrice(priceListSKUMapList, toDto.ProductID, null);
            new PriceSettingsRepository().Saveproductbundlesettings(bundleEntities, toDto.ProductID, null);
            if (existingProductCategoryMapList.Any(x => x.FeeDueInventoryMaps.Count == 0))
            {
                new PriceSettingsRepository().Savecategoeries(categoryEntities, toDto.ProductID, null);
            }
            else
            {
                new PriceSettingsRepository().Savecategoeries(categoryEntities, toDto.ProductID, existingProductCategoryMapList);
            }

            var existingAllergies = new List<ProductAllergyMap>();

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                existingAllergies = dbContext.ProductAllergyMaps
                    .Include(i => i.Product)
                    .Where(c => c.ProductID == entity.ProductIID).AsNoTracking().ToList();

                new PriceSettingsRepository().SaveAllergies(allergyEntities, toDto.ProductID, existingAllergies);
            }

            new PriceSettingsRepository().Savecrackentities(rackEntities, toDto.ProductID, null);

            return GetEntity(entity.ProductIID);
        }

        public List<ProductImageMap> GetProductImages(SKUDTO dto)
        {
            var userID = _context.LoginID;

            var imageTypes = Enum.GetValues(typeof(Eduegate.Framework.Helper.Enums.ImageType))
            .Cast<Eduegate.Framework.Helper.Enums.ImageType>().ToList();

            foreach (var imageType in imageTypes)
            {
                ResizeProductImage(Convert.ToInt64(userID), imageType, dto);
            }

            var productImageMapList = new List<ProductImageMap>();

            byte sequence = 1;

            foreach (var imageType in imageTypes)
            {
                dto.ProductImageUrls.ForEach(fileName =>
                {
                    productImageMapList.Add(new ProductImageMap()
                    {
                        ProductSKUMapID = dto.ProductSKUMapID,
                        ProductID = dto.ProductID,
                        ProductImageTypeID = (int)imageType,
                        ImageFile = dto.ProductSKUMapID + "\\" + imageType.ToString() + "\\" + Path.GetFileName(fileName.FilePath),
                        Sequence = sequence
                    });
                });
            }
            sequence++;

            return productImageMapList;
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            dto = dto as SKUDTO;
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            //if (_product == null)
            _product = new ProductDetailRepository().GetProduct(IID);

            var productSKUMap = _product.ProductSKUMaps.FirstOrDefault();
            var priceRepository = new PriceSettingsRepository();
            var prices = priceRepository.GetProductPriceListForSKU(productSKUMap.ProductSKUMapIID, _context.CompanyID.Value);
            var dto = ToDTO(_product);
            dto.ProductPrices = new List<ProductPriceSettingDTO>();

            foreach (var price in prices)
            {
                dto.ProductPrices.Add(new ProductPriceSettingDTO()
                {
                    Discount = price.Discount,
                    DiscountPercentage = price.DiscountPercentage,
                    CompanyID = price.CompanyID,
                    Price = price.Price,
                    ProductID = price.ProductSKUID,
                    ProductPriceListID = price.ProductPriceListID,
                    ProductPriceListProductMapIID = price.ProductPriceListItemMapIID,
                });
            }
            var productbundleMap = _product.ProductBundleToProducts.ToList();
            var productbundleSKUMap = _product.ProductSKUMaps.FirstOrDefault();
            var productbundleRepository = new PriceSettingsRepository();
            var ProductBundle = productbundleRepository.GetProductbundles(productbundleSKUMap.ProductSKUMapIID, _context.CompanyID.Value);
            dto.ProductBundle = new List<ProductBundleDTO>();

            //foreach (var productbudle in ProductBundle)
            if (ProductBundle.Count > 0)
            {
                foreach (var pBundle in ProductBundle)
                {
                    dto.ProductBundle.Add(new ProductBundleDTO()
                    {
                        BundleIID = pBundle.BundleIID,
                        CostPrice = pBundle.CostPrice,
                        ToProductID = pBundle.ToProductID,
                        FromProductSKUMapID = pBundle.FromProductSKUMapID,
                        Quantity = pBundle.Quantity,
                        SellingPrice = pBundle.SellingPrice,
                        FromProductID = pBundle.FromProductID,
                        FromProduct = pBundle.FromProductID.HasValue ? new KeyValueDTO()
                        {
                            Key = pBundle.FromProductID.ToString(),
                            Value = pBundle.FromProduct.ProductName

                        } : new KeyValueDTO(),
                        ToProductSKUMapID = pBundle.ToProductSKUMapID,
                    });
                }
            }

            List<KeyValueDTO> mapDto = new List<KeyValueDTO>();
            foreach (var BookCategorie in _product.ProductCategoryMaps)
            {
                mapDto.Add(new KeyValueDTO()
                {
                    Key = BookCategorie.CategoryID.ToString(),
                    Value = BookCategorie.Category.CategoryName
                });
                dto.Categories = mapDto;
            }

            List<KeyValueDTO> allergyDto = new List<KeyValueDTO>();
            foreach (var productAllergy in _product.ProductAllergyMaps)
            {
                allergyDto.Add(new KeyValueDTO()
                {

                    Key = productAllergy.AllergyID.ToString(),
                    Value = productAllergy.Allergy.AllergyName

                });
                dto.Allergies = allergyDto;
            }

            List<KeyValueDTO> maprackDto = new List<KeyValueDTO>();
            foreach (var rackmap in _product.ProductSKURackMaps)
            {
                maprackDto.Add(new KeyValueDTO()
                {

                    Key = rackmap.RackID.ToString(),
                    Value = rackmap.Rack.RackName

                });
                dto.Rack = maprackDto;
            }

            var priceLists = priceRepository.GetProductPriceLists(_context.CompanyID);

            foreach (var list in priceLists)
            {
                var existingMap = dto.ProductPrices.FirstOrDefault(a => a.ProductPriceListID == list.ProductPriceListIID);

                if (existingMap == null)
                {
                    dto.ProductPrices.Add(new ProductPriceSettingDTO()
                    {
                        ProductPriceListID = list.ProductPriceListIID,
                        PriceDescription = list.PriceDescription
                    });
                }
                else
                {
                    existingMap.PriceDescription = list.PriceDescription;
                }
            }

            var imageHostUrl = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ImageHostUrl");

            if (productSKUMap.ProductImageMaps != null && productSKUMap.ProductImageMaps.Count > 0)
            {
                var largeImages = productSKUMap.ProductImageMaps
                          .Where(x => x.ProductImageTypeID == (int)ImageTypes.Large);
                var imagePath = string.Format("{0}\\{1}\\{2}",
                          imageHostUrl, "Products",
                          largeImages.FirstOrDefault().ImageFile);
                dto.ProductImageUrl = imagePath;
                dto.ProductImageUploadFile = imagePath;

                foreach (var image in largeImages)
                {
                    dto.ProductImageUrls.Add(
                        new Services.Contracts.DowloadFileDTO()
                        {
                            FileMapID = image.ProductImageMapIID,
                            FilePath = string.Format("{0}\\{1}\\{2}",
                          imageHostUrl, "Products",
                          image.ImageFile)
                        });
                }
            }

            return ToDTOString(dto);
        }

        public SKUDTO ToDTO(Product entity)
        {
            var sku = entity.ProductSKUMaps.FirstOrDefault();
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                //var getPurchaseGrpID = dbContext.Units.FirstOrDefault(x => x.UnitID == entity.PurchaseUnitID);
                //var getSellingGrpID = dbContext.Units.FirstOrDefault(x => x.UnitID == entity.SellingUnitID);

                //var purchaseGroup = getPurchaseGrpID != null ? dbContext.UnitGroups.FirstOrDefault(p => p.UnitGroupID == getPurchaseGrpID.UnitGroupID) : null;
                //var sellingGroup = getSellingGrpID != null ? dbContext.UnitGroups.FirstOrDefault(p => p.UnitGroupID == getSellingGrpID.UnitGroupID) : null;

                return new SKUDTO()
                {
                    BarCode = sku.BarCode,
                    PartNumber = sku.PartNo,
                    SkuName = sku == null ? entity.ProductName : sku.SKUName,
                    //ProductSKUCode = entity.ProductCode,
                    TaxTempleteID = entity.TaxTemplateID,
                    ProductID = entity.ProductIID,
                    //SKU = entity.ProductCode,
                    ProductSKUMapID = sku.ProductSKUMapIID,
                    UnitID = entity.UnitID,
                    BrandID = entity.BrandID,
                    ProductFamilyID = entity.ProductFamilyID,
                    SellingUnitID = entity.SellingUnitID,
                    PurchaseUnitID = entity.PurchaseUnitID,
                    IsActive = entity.IsActive,
                    ProductCode = entity.ProductCode,
                    Calorie = entity.Calorie,
                    Weight = entity.Weight,
                    //PurchaseUnitGroupID = entity.PurchaseUnitGroupID,
                    //SellingUnitGroupID = entity.SellingUnitGroupID,
                    Unit = entity.UnitID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.UnitID.ToString(),
                        Value = entity.Unit2.UnitName

                    } : new KeyValueDTO(),
                    Brand = entity.BrandID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.BrandID.ToString(),
                        Value = entity.Brand.BrandName

                    } : new KeyValueDTO(),
                    ProductFamily = entity.ProductFamilyID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.ProductFamilyID.ToString(),
                        Value = entity.ProductFamily.FamilyName

                    } : new KeyValueDTO(),

                    PurchasingUnit = entity.PurchaseUnitID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.PurchaseUnitID.ToString(),
                        Value = entity.Unit.UnitName

                    } : new KeyValueDTO(),

                    SellingUnit = entity.SellingUnitID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.SellingUnitID.ToString(),
                        Value = entity.Unit1.UnitName

                    } : new KeyValueDTO(),

                    GLAccount = entity.GLAccountID.HasValue ? new KeyValueDTO()
                    {
                        Key = entity.GLAccountID.ToString(),
                        Value = entity.GLAccount.AccountName

                    } : new KeyValueDTO(),

                    //PurchaseUnitGroup = entity.PurchaseUnitID.HasValue ? new KeyValueDTO()
                    //{
                    //    Key = entity.PurchaseUnitGroupID.ToString(),
                    //    Value = entity.UnitGroup.UnitGroupName

                    //} : new KeyValueDTO(),

                    //SellingUnitGroup = entity.SellingUnitGroupID.HasValue ? new KeyValueDTO()
                    //{
                    //    Key = entity.SellingUnitGroupID.ToString(),
                    //    Value = entity.UnitGroup1.UnitGroupName

                    //} : new KeyValueDTO(),

                    ProductTypeID = entity.ProductTypeID.HasValue ? entity.ProductTypeID : null,
                    ProductType = entity.ProductTypeID.HasValue ? entity.ProductTypeID.ToString() : null,

                    PurchaseUnitGroupID = entity.PurchaseUnitGroupID.HasValue ? entity.PurchaseUnitGroupID : null,
                    PurchaseUnitGroup = entity.PurchaseUnitGroupID.HasValue ? entity.PurchaseUnitGroupID.ToString() : null,

                    SellingUnitGroupID = entity.SellingUnitGroupID.HasValue ? entity.SellingUnitGroupID : null,
                    SellingUnitGroup = entity.SellingUnitGroupID.HasValue ? entity.SellingUnitGroupID.ToString() : null,

                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.Created,
                    UpdatedBy = entity.UpdateBy,
                    UpdatedDate = entity.Updated,

                };
            }

        }

        public Product ToEntity(SKUDTO dto)
        {
            var product = new Product();
            var repository = new ProductDetailRepository();

            //Remove product images from map table
            if (dto.ProductID != 0)
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    var imageSkumapDataList = dbContext.ProductImageMaps.Where(x => x.ProductID == dto.ProductID).AsNoTracking().ToList();
                    foreach (var listData in imageSkumapDataList)
                    {
                        dbContext.ProductImageMaps.Remove(listData);
                    }

                    dbContext.SaveChanges();
                }
            }

            if (dto.ProductID != 0)
            {
                product = repository.GetProduct(dto.ProductID);
            }
            //else
            //{
            //    product.ProductCode = dto.SkuName.ToLower().Replace(" ", "_");
            //}

            product.ProductIID = dto.ProductID;
            product.ProductName = dto.SkuName;
            product.TaxTemplateID = dto.TaxTempleteID;

            if (product.ProductSKUMaps == null)
                product.ProductSKUMaps = new List<ProductSKUMap>();

            if (product.ProductSKUMaps.Count == 0)
                product.ProductSKUMaps.Add(new ProductSKUMap());

            var sku = product.ProductSKUMaps.FirstOrDefault();
            sku.ProductSKUMapIID = dto.ProductSKUMapID;
            sku.SKUName = dto.SkuName;
            sku.BarCode = dto.BarCode;
            sku.PartNo = dto.PartNumber;
            sku.ProductID = dto.ProductID;
            //sku.ProductSKUCode = dto.SKU;
            sku.ProductSKUCode = dto.ProductCode;
            product.UnitID = dto.UnitID;
            product.PurchaseUnitID = dto.PurchaseUnitID;
            product.SellingUnitID = dto.SellingUnitID;
            product.BrandID = dto.BrandID;
            product.ProductFamilyID = dto.ProductFamilyID;
            product.ProductTypeID = dto.ProductTypeID;
            product.PurchaseUnitGroupID = dto.PurchaseUnitGroupID;
            product.SellingUnitGroupID = dto.SellingUnitGroupID;
            product.IsActive = dto.IsActive;
            product.ProductCode = dto.ProductCode;

            product.GLAccountID = dto.GLAccountID;
            sku.StatusID = 2;

            product.ProductImageMaps = new List<ProductImageMap>();
            product.ProductImageMaps = GetProductImages(dto);
            product.Calorie = dto.Calorie;
            product.Weight = dto.Weight;

            return product;
        }

        public override KeyValueDTO ValidateField(BaseMasterDTO dto, string field)
        {
            var toDto = dto as SKUDTO;
            var valueDTO = new KeyValueDTO();

            switch (field)
            {
                case "PartNumber":
                    if (!string.IsNullOrEmpty(toDto.PartNumber))
                    {
                        var hasDuplicated = new ProductDetailRepository().IsPartNumberDuplicated(toDto.PartNumber, toDto.ProductSKUMapID);
                        if (hasDuplicated)
                        {
                            valueDTO.Key = "true";
                            valueDTO.Value = "Part number already exists, Please try with different part number.";
                        }
                        else
                        {
                            valueDTO.Key = "false";
                        }
                    }
                    else
                    {
                        valueDTO.Key = "false";
                    }
                    break;
                case "BarCode":

                    if (!string.IsNullOrEmpty(toDto.BarCode))
                    {
                        var hasBarCode = new ProductDetailRepository().IsBarCodeDuplicated(toDto.BarCode, toDto.ProductSKUMapID);
                        if (hasBarCode)
                        {
                            valueDTO.Key = "true";
                            valueDTO.Value = "Bar code already exists, Please try with different bar code.";
                        }
                        else
                        {
                            valueDTO.Key = "false";
                        }
                    }
                    else
                    {
                        valueDTO.Key = "false";
                    }

                    break;
                case "ProductDESC":
                    if (!string.IsNullOrEmpty(toDto.SkuName))
                    {
                        var hasDuplicated = new ProductDetailRepository().IsProductSKUNameDuplicated(toDto.SkuName, toDto.ProductSKUMapID);
                        if (hasDuplicated)
                        {
                            valueDTO.Key = "true";
                            valueDTO.Value = "Product description already exists, Please try with different description.";
                        }
                        else
                        {
                            valueDTO.Key = "false";
                        }
                    }
                    else
                    {
                        valueDTO.Key = "false";
                    }
                    break;
                case "Product":
                    if (!string.IsNullOrEmpty(toDto.SKU))
                    {
                        var hasDuplicated = new ProductDetailRepository().IsProductCodeDuplicated(toDto.SKU, toDto.ProductSKUMapID);
                        if (hasDuplicated)
                        {
                            valueDTO.Key = "true";
                            valueDTO.Value = "Product Code/Name already exists, Please try with different Product Code/Name.";
                        }
                        else
                        {
                            valueDTO.Key = "false";
                        }
                    }
                    else
                    {
                        valueDTO.Key = "false";
                    }
                    break;
            }

            return valueDTO;
        }

        public ProductBundleDTO GetProductBundleData(long productskuId)
        {
            var prodDetail = new ProductBundleDTO();
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                decimal? totalQuantity = 0;

                var costPrice = (from prod in dbContext.Products.AsEnumerable()
                                 join prodsku in dbContext.ProductSKUMaps on prod.ProductIID equals prodsku.ProductID
                                 join prodprice in dbContext.ProductPriceListSKUMaps on prodsku.ProductSKUMapIID equals prodprice.ProductSKUID
                                 where (prodprice.ProductSKUID == productskuId && prodprice.ProductPriceListID == 2)
                                 orderby prod.ProductIID descending
                                 select prodprice.Amount
                             ).FirstOrDefault();

                var unit = dbContext.ProductSKUMaps.Where(a => a.ProductSKUMapIID == productskuId)
                    .Include(a => a.Product).ThenInclude(a => a.Unit1)
                    .AsNoTracking().FirstOrDefault();

                prodDetail.Unit = unit.Product?.Unit1?.UnitName;
                prodDetail.UnitID = unit.Product?.SellingUnitID;

                prodDetail.CostPrice = costPrice ?? 0 ;

                var quantityListData = (from prod in dbContext.Products.AsEnumerable()
                                        join prodsku in dbContext.ProductSKUMaps on prod.ProductIID equals prodsku.ProductID
                                        join prodqty in dbContext.ProductInventories on prodsku.ProductSKUMapIID equals prodqty.ProductSKUMapID
                                        where (prodsku.ProductSKUMapIID == productskuId && prodqty.BranchID == _context.SchoolID)
                                        orderby prod.ProductIID descending
                                        select prodqty
                              ).ToList();

                foreach (var data in quantityListData)
                {
                    if (data.Quantity == null)
                    {
                        data.Quantity = 0;
                    }

                    totalQuantity = totalQuantity + data.Quantity;
                }

                prodDetail.AvailableQuantity = totalQuantity;
            }

            return prodDetail;
        }


        public ProductBundleDTO GetProductDetails(long productId)
        {
            var prodDetail = new ProductBundleDTO();
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                decimal? totalQuantity = 0;

                prodDetail.SellingPrice = (from prod in dbContext.Products.AsEnumerable()
                                           join prodsku in dbContext.ProductSKUMaps on prod.ProductIID equals prodsku.ProductID
                                           join prodprice in dbContext.ProductPriceListSKUMaps on prodsku.ProductSKUMapIID equals prodprice.ProductSKUID
                                           where (prod.ProductIID == productId && prodprice.ProductPriceListID == 1)
                                           orderby prod.ProductIID descending
                                           select prodprice.Amount
                              ).FirstOrDefault();

                prodDetail.CostPrice = (from prod in dbContext.Products.AsEnumerable()
                                        join prodsku in dbContext.ProductSKUMaps on prod.ProductIID equals prodsku.ProductID
                                        join prodprice in dbContext.ProductPriceListSKUMaps on prodsku.ProductSKUMapIID equals prodprice.ProductSKUID
                                        where (prod.ProductIID == productId && prodprice.ProductPriceListID == 2)
                                        orderby prod.ProductIID descending
                                        select prodprice.Amount
                              ).FirstOrDefault();

                prodDetail.FromProductSKUMapID = (from prodsku in dbContext.ProductSKUMaps.AsEnumerable()
                                                  where (prodsku.ProductID == productId)
                                                  orderby prodsku.ProductID descending
                                                  select prodsku.ProductSKUMapIID
                              ).FirstOrDefault();

                var quantityListData = (from prod in dbContext.Products.AsEnumerable()
                                        join prodsku in dbContext.ProductSKUMaps on prod.ProductIID equals prodsku.ProductID
                                        join prodqty in dbContext.ProductInventories on prodsku.ProductSKUMapIID equals prodqty.ProductSKUMapID
                                        where (prod.ProductIID == productId && prodqty.BranchID == _context.SchoolID)
                                        orderby prod.ProductIID descending
                                        select prodqty
                              ).ToList();

                foreach (var data in quantityListData)
                {
                    if (data.Quantity == null)
                    {
                        data.Quantity = 0;
                    }

                    totalQuantity = totalQuantity + data.Quantity;
                }

                prodDetail.AvailableQuantity = totalQuantity;

            }
            return prodDetail;
        }

        private bool ResizeProductImage(long userID, Eduegate.Framework.Helper.Enums.ImageType imageType, SKUDTO dto)
        {
            bool isImageResized = false;
            var imgAttributeList = new List<ImageAttributes>();
            try
            {
                string tempFolderPath = string.Format("{0}\\{1}\\{2}\\{3}", new Domain.Setting.SettingBL().GetSettingValue<string>("ImagesPhysicalPath").ToString(), EduegateImageTypes.Products, Constants.TEMPFOLDER, userID);
                string destinationFolderPath = string.Format("{0}\\{1}\\{2}\\{3}", new Domain.Setting.SettingBL().GetSettingValue<string>("ImagesPhysicalPath").ToString(), EduegateImageTypes.Products, dto.ProductSKUMapID, imageType.ToString());

                if (Directory.Exists(tempFolderPath))
                {
                    string[] imageNames = Directory.GetFiles(tempFolderPath).Select(path => Path.GetFileName(path)).ToArray();
                    if (imageNames != null && imageNames.Count() > 0)
                    {
                        if (!Directory.Exists(destinationFolderPath))
                        {
                            Directory.CreateDirectory(destinationFolderPath);
                        }

                        foreach (var image in imageNames)
                        {
                            var imgAttribute = new ImageAttributes();
                            imgAttribute.SourceImageName = image;
                            imgAttribute.SourcePath = tempFolderPath;
                            imgAttribute.DestinationImageName = image;
                            imgAttribute.DestinationImageType = (Eduegate.Utilities.ImageScalar.ImageType)imageType;
                            imgAttribute.DestinationImagePath = destinationFolderPath;
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
                LogHelper<QuickCatalogMapper>.Fatal(ex.Message.ToString(), ex);
            }

            return isImageResized;
        }
    }
}