using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.CustomEntity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Security.Secured;

namespace Eduegate.Domain.Mappers.DynamicViews
{
    public partial class DynamicViewMapper
    {
        public Eduegate.Services.Contracts.Search.SearchResultDTO GetProductDetails(Product product)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (product != null)
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProductIID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProductName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProductCode" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "BrandID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Created" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Updated" });

                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                            product.ProductIID, product.ProductName, product.ProductCode,
                            product.BrandID,
                            product.Created, product.Updated
                        }
                });
            }

            return searchDTO;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO GetProductSettings(Product product)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (product != null && product.ProductInventoryProductConfigMaps.Count > 0)
            {
                var config = product.ProductInventoryProductConfigMaps.FirstOrDefault().ProductInventoryConfig;

                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProductInventoryConfigIID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DimensionalWeight" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "IsMarketPlace" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "IsQuntityUseDecimals" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "IsSerialNumber" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "IsSerialRequiredForPurchase" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "IsStockAvailabiltiyID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "MaximumQuantity" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "MaximumQuantityInCart" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "MinimumQuanityInCart" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "MinimumQuantity" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "NotifyQuantity" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProductHeight" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProductLength" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProductWarranty" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProductWeight" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProductWidth" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "UpdatedDate" });

                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                            config.ProductInventoryConfigIID,
                            config.DimensionalWeight, config.IsMarketPlace, config.IsQuntityUseDecimals,
                            config.IsSerialNumber,config.IsSerialRequiredForPurchase,config.IsStockAvailabiltiyID,
                            config.MaximumQuantity, config.MaximumQuantityInCart, config.MinimumQuanityInCart,
                            config.MinimumQuantity, config.NotifyQuantity, config.ProductHeight, config.ProductLength,
                            config.ProductWarranty, config.ProductWeight, config.ProductWidth,
                            config.CreatedDate, config.UpdatedDate,
                        }
                });
            }

            return searchDTO;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO GetProductSKUSettings(ProductInventoryConfig productConfig)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (productConfig != null)
            {
                var config = productConfig;

                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProductInventoryConfigIID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DimensionalWeight" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "IsMarketPlace" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "IsQuntityUseDecimals" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "IsSerialNumber" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "IsSerialRequiredForPurchase" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "IsStockAvailabiltiyID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "MaximumQuantity" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "MaximumQuantityInCart" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "MinimumQuanityInCart" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "MinimumQuantity" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "NotifyQuantity" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProductHeight" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProductLength" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProductWarranty" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProductWeight" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProductWidth" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "UpdatedDate" });

                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                            config.ProductInventoryConfigIID,
                            config.DimensionalWeight, config.IsMarketPlace, config.IsQuntityUseDecimals,
                            config.IsSerialNumber,config.IsSerialRequiredForPurchase,config.IsStockAvailabiltiyID,
                            config.MaximumQuantity, config.MaximumQuantityInCart, config.MinimumQuanityInCart,
                            config.MinimumQuantity, config.NotifyQuantity, config.ProductHeight, config.ProductLength,
                            config.ProductWarranty, config.ProductWeight, config.ProductWidth,
                            config.CreatedDate, config.UpdatedDate,
                        }
                });
            }

            return searchDTO;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO GetProductSKUMaps(Product product)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (product != null)
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProductSKUMapIID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "SKUName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "BarCode" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PartNo" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProductPrice" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProductSKUCode" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Sequence" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "VariantsMap" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "UpdatedDate" });

                foreach (var sku in product.ProductSKUMaps)
                {
                    searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                                {
                                    sku.ProductSKUMapIID, sku.SKUName, sku.BarCode,sku.PartNo, sku.ProductPrice, sku.ProductSKUCode, sku.Sequence, sku.VariantsMap,
                                    sku.CreatedDate, sku.UpdatedDate
                                }
                    });
                }
            }

            return searchDTO;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO GetProductInventory(List<ProductInventory> inventories, SecuredData claims)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();
            var repository = new ReferenceDataRepository();
            if (inventories != null)
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProductSKUMapID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Batch" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "BranchID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Branch" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Quantity" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ExpiryDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CostPrice" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "UpdatedDate" });

                foreach (var sku in inventories)
                {
                    if (claims.HasAccess(sku.BranchID.Value, Framework.Security.Enums.ClaimType.Branches))
                    {
                        if (repository.IsMarketPlaceBranch(sku.BranchID.Value))
                        {
                            searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                            {
                                DataCells = new Services.Contracts.Commons.DataCellListDTO()
                                {
                                    sku.ProductSKUMapID, sku.Batch, sku.BranchID, sku.Branch == null ? string.Empty : sku.Branch.BranchName, sku.Quantity, sku.ExpiryDate, sku.CostPrice,
                                    sku.CreatedDate, sku.UpdatedDate
                                }
                            });
                        }
                    }
                }

            }

            return searchDTO;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO GetBrand(Brand brand)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (brand != null)
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "BrandIID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "BrandName" });
                //searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "BrandCode" });
                //searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Description" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Status" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "LogoFile" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "UpdatedDate" });

                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                            brand.BrandIID, brand.BrandName,
                            //brand.BrandCode, 
                            brand.StatusID,brand.LogoFile,brand.CreatedDate, brand.UpdatedDate
                        }
                });
            }

            return searchDTO;
        }
        public Eduegate.Services.Contracts.Search.SearchResultDTO GetCategory(Category category, Category parentCategory)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (category != null)
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CategoryIID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CategoryName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ParentCategoryName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CategoryCode" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "IsActive" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ImageName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Created" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Updated" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Profit" });

                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                            category.CategoryIID,category.CategoryName,parentCategory.IsNotNull() ? parentCategory.CategoryName :"-",
                             category.CategoryCode,category.IsActive,
                            category.ImageName,category.Created,category.Updated, category.Profit
                        }
                });
            }

            return searchDTO;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO ProductProperties(Property property, PropertyType propertytype)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();
            if (property != null)
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PropertyIID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PropertyName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "DefaultValue" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PropertyTypeName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "UpdatedDate" });

                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          property.PropertyIID,property.PropertyName,property.DefaultValue,property.PropertyTypeID.IsNotNull() ?propertytype.PropertyTypeName :"-",property.CreatedDate,property.UpdatedDate
                        }
                });
            }
            return searchDTO;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO ProductFamilies(ProductFamily productfamily, List<Property> properties)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (productfamily != null)
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProductFamilyID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "FamilyName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PropertyName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "UpdatedDate" });

                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                     {
                          productfamily.ProductFamilyIID, productfamily.FamilyName, string.Join(",", properties.Select(x=>x.PropertyName)), productfamily.CreatedDate, productfamily.UpdatedDate
                     }
                });
            }
            return searchDTO;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO GetProductPriceListDetail(ProductPriceList productpricelist)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (productpricelist != null)
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProductPriceListIID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Price" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PricePercentage" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PriceDescription" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PriceListType" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Name" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "StartDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "EndDate" });

                searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                {
                    DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                          productpricelist.ProductPriceListIID,productpricelist.Price,
                          productpricelist.PricePercentage, productpricelist.PriceDescription,
                          productpricelist.ProductPriceListType.PriceListTypeName,productpricelist.ProductPriceListLevel.Name,
                          productpricelist.StartDate,productpricelist.EndDate
                        }
                });
            }

            return searchDTO;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO GetCategoryProductSKUMaps(List<Product> products, List<ProductSKUMap> productSKUMaps)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (productSKUMaps != null && productSKUMaps.Count > 0)
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProductSKUMapIID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "BarCode" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PartNo" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProductName" });


                foreach (var sku in productSKUMaps)
                {
                    searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                                {
                                    sku.ProductSKUMapIID, sku.BarCode,sku.PartNo, products.Where(x=>x.ProductIID.Equals(sku.ProductID)).FirstOrDefault().ProductName
                                }
                    });
                }

            }

            return searchDTO;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO GetCategoryImageMaps(List<CategoryImageMap> maps)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (maps != null && maps.Count > 0)
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CategoryImageMapIID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ImageFile" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ImageTitle" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ImageType" });


                foreach (var image in maps)
                {
                    searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                                {
                                    image.CategoryImageMapIID, image.ImageFile,image.ImageTitle, image.ImageType.TypeName,
                                }
                    });
                }

            }

            return searchDTO;
        }
        public Eduegate.Services.Contracts.Search.SearchResultDTO GetProductByBrand(List<Product> product, List<ProductSKUMap> productsku)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (productsku != null && productsku.Count > 0)
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProductSKUMapID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "BarCode" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PartNo" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProductName" });

                foreach (var sku in productsku)
                {
                    searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                            sku.ProductSKUMapIID,sku.BarCode,sku.PartNo,product.Where(x=>x.ProductIID.Equals(sku.ProductID)).FirstOrDefault().ProductName
                        }
                    });
                }

            }


            return searchDTO;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO GetProductPriceSKU(List<ProductPriceSKU> productpricesku)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (productpricesku != null && productpricesku.Count > 0)
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "SKU" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProductName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "PartNumber" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "BarCode" });


                foreach (var product in productpricesku)
                {
                    searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                           product.SKU,product.ProductName,product.PartNumber,product.Barcode

                        }
                    });
                }

            }

            return searchDTO;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO GetSKUTagDetail(ProductSKUTag tagsku)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO(); 
             
            if (tagsku != null)
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProductSKUTagIID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "TagName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CreatedDate" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "UpdatedDate" });

                    searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                         DataCells = new Services.Contracts.Commons.DataCellListDTO()
                          {
                              tagsku.ProductSKUTagIID,tagsku.TagName,tagsku.CreatedDate,tagsku.UpdatedDate
                          }
                    });
                }

            return searchDTO; 
        }
        public Eduegate.Services.Contracts.Search.SearchResultDTO GetSKUTagMapDetails(List<ProductSKUTagMap> tagskulist)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (tagskulist != null)
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProductSKUMapID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "SKUName" });

                foreach (var tagsku in tagskulist)
                {
                    var SKUName = new ProductDetailRepository().GetProductAndSKUNameByID((long)tagsku.ProductSKuMapID);
                    searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                          {
                              tagsku.ProductSKuMapID,SKUName
                          } 
                    });
                }
            }

            return searchDTO;
        }

        public Eduegate.Services.Contracts.Search.SearchResultDTO GetBundleDetails(List<ProductBundle> bundles)
        {
            var searchDTO = new Eduegate.Services.Contracts.Search.SearchResultDTO();

            if (bundles != null)
            {
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "BundleIID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "ProductIID" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "FromProductName" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "Quantity" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "CostPrice" });
                searchDTO.Columns.Add(new Services.Contracts.Search.ColumnDTO() { ColumnName = "SellingPrice" });

                foreach (var bndl in bundles)
                {
                    searchDTO.Rows.Add(new Services.Contracts.Commons.DataRowDTO()
                    {
                        DataCells = new Services.Contracts.Commons.DataCellListDTO()
                        {
                            bndl.BundleIID, bndl.Product1.ProductIID, bndl.Product.ProductName,
                            bndl.Quantity,
                            bndl.CostPrice, bndl.SellingPrice
                        }
                    });
                }
            }

            return searchDTO;
        }
    }
}
