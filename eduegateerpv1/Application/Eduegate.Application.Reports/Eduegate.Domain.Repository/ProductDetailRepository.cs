using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Helper;
using Eduegate.Framework.Logs;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts;
using Eduegate.Framework;
using Helper = Eduegate.Framework.Helper.Enums;
using System.IO;
using Eduegate.Domain.Entity.CustomEntity;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Domain.Entity.Models.ValueObjects;
using System.Data.Entity;
using Eduegate.Services.Contracts.Inventory;
using System.Data;
using System.Data.SqlClient;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Students;

namespace Eduegate.Domain.Repository
{
    public class ProductDetailRepository
    {
        //Get List of country master details
        public ProductFeatureDTO GetProductDetail(long ProductId, long skuID, decimal? ConvertedPrice,
            Eduegate.Services.Contracts.Enums.ProductStatuses productStatus)
        {
            dbEduegateERPContext db = new dbEduegateERPContext();
            ProductFeatureDTO _dto = new ProductFeatureDTO();

            try
            {
                // get the products details 
                var qryProd = (from p in db.Products
                               join b in db.Brands on p.BrandID equals b.BrandIID
                               join psm in db.ProductSKUMaps on p.ProductIID equals psm.ProductID into two
                               from psm in two.DefaultIfEmpty()
                               join ppm in db.ProductPropertyMaps on psm.ProductID equals ppm.ProductID into ppmLJ
                               from ppm in ppmLJ.DefaultIfEmpty()
                               where (psm.ProductSKUMapIID == ppm.ProductSKUMapID || ppm.ProductSKUMapID == null)

                               join prt in db.PropertyTypes on ppm.PropertyTypeID equals prt.PropertyTypeID into one
                               from prt in one.DefaultIfEmpty()
                               join pr in db.Properties on ppm.PropertyID equals pr.PropertyIID into prLJ
                               from pr in prLJ.DefaultIfEmpty()
                               join pplsm in db.ProductPriceListSKUMaps on psm.ProductSKUMapIID equals pplsm.ProductSKUID into three
                               from pplsm in three.DefaultIfEmpty()
                               join ppl in db.ProductPriceLists on pplsm.ProductPriceListID equals ppl.ProductPriceListIID into four
                               from ppl in four.DefaultIfEmpty()
                               join pis in db.ProductInventories on psm.ProductSKUMapIID equals pis.ProductSKUMapID into five
                               from pis in five.DefaultIfEmpty()

                               join pisc in db.ProductInventorySKUConfigMaps on psm.ProductSKUMapIID equals pisc.ProductSKUMapID into six
                               from pisc in six.DefaultIfEmpty()

                               join pic in db.ProductInventoryConfigs on pisc.ProductInventoryConfigID equals pic.ProductInventoryConfigIID into seven
                               from pic in seven.DefaultIfEmpty()

                               where p.ProductIID == ProductId && ppm.ProductID == p.ProductIID

                               select new
                               {
                                   p.ProductIID,
                                   p.ProductName,
                                   b.BrandIID,
                                   b.BrandName,
                                   ProductSKUMapIID = (long?)psm.ProductSKUMapIID,
                                   p.StatusID,
                                   psm.ProductSKUCode,
                                   psm.ProductPrice,
                                   DiscountedPrice = (ppl.Price != null && ((ppl.StartDate != null && ppl.EndDate == null && DateTime.Now >= ppl.StartDate) || (DateTime.Now >= ppl.StartDate && DateTime.Now <= ppl.EndDate) || (ppl.StartDate == null && ppl.EndDate == null))) ? ppl.Price :
                                                       (ppl.PricePercentage != null && ((ppl.StartDate != null && ppl.EndDate == null && DateTime.Now >= ppl.StartDate) || (DateTime.Now >= ppl.StartDate && DateTime.Now <= ppl.EndDate) || (ppl.StartDate == null && ppl.EndDate == null))) ? (decimal?)((decimal?)psm.ProductPrice * (100 - (decimal?)ppl.PricePercentage)) / 100 :
                                                       psm.ProductPrice,

                                   PricePercentage = ppl.Price != null ? (1 - (ppl.Price / psm.ProductPrice)) * 100 : ppl.PricePercentage,
                                   prt.PropertyTypeID,
                                   prt.PropertyTypeName,
                                   PropertyIID = (long?)pr.PropertyIID,
                                   pr.PropertyName,
                                   ppm.Value,
                                   psm.Sequence,
                                   pic.MinimumQuanityInCart,
                                   pic.MaximumQuantityInCart,
                                   pis.Quantity
                               }).ToList();

                //Check product status
                if (productStatus != Services.Contracts.Enums.ProductStatuses.All)
                {
                    sbyte statusID = Convert.ToSByte(productStatus);
                    qryProd = qryProd.Where(x => x.StatusID == statusID).ToList();
                }
                // check if query is null then return here
                if (qryProd == null || qryProd.Count() < 1)
                    return null;

                // get SKU detail based on sequence
                var SKU = (from qry in qryProd
                           where qry.ProductSKUMapIID == skuID
                           //where qry.Sequence == 1
                           orderby qry.PropertyTypeID descending
                           select new
                           {
                               qry.ProductSKUMapIID,
                               qry.ProductSKUCode,
                               qry.ProductPrice,
                               qry.DiscountedPrice,
                               //PricePercentage = (qry.ProductPrice != qry.DiscountedPrice ? 
                               //(qry.PricePercentage != null ? qry.PricePercentage : decimal.ToInt32((decimal)(1 - (qry.DiscountedPrice / qry.ProductPrice)) * 100)) : 0),

                               qry.PricePercentage,
                               qry.MinimumQuanityInCart,
                               qry.MaximumQuantityInCart,
                               qry.Quantity,
                           }).FirstOrDefault();

                if (SKU == null)
                    return null;
                // distinct PropertyTypeID for filter the data 
                var distinctPropertyTypeID = qryProd.GroupBy(x => x.PropertyTypeID)
                                            .Select(g => g.First()).ToList();

                var listPropertyIID = (from qry in qryProd
                                       where qry.PropertyIID != null && qry.ProductSKUMapIID == skuID
                                       select qry.PropertyIID).ToList();

                long? skuMapID = SKU == null ? null : (long?)SKU.ProductSKUMapIID;
                // get the images of Products
                var qryProdImage = (from p in db.Products
                                    join pim in db.ProductImageMaps on p.ProductIID equals pim.ProductID
                                    where pim.ProductSKUMapID == skuMapID
                                    select new
                                    {
                                        p.ProductIID,
                                        pim.ProductImageMapIID,
                                        pim.ProductImageTypeID,
                                        pim.ImageFile,
                                        pim.Sequence
                                    }).OrderBy(x => x.ProductImageMapIID).ThenBy(x => x.Sequence).ToList();


                _dto.ImageList = new List<ImageDTO>();
                _dto.ProductSKUDTOList = new List<ProductSKUDTO>();

                _dto.ProductID = qryProd[0].ProductIID;
                _dto.ProductName = qryProd[0].ProductName;

                _dto.BrandID = qryProd[0].BrandIID;
                _dto.BrandName = qryProd[0].BrandName;

                _dto.ProductSKUMapId = (long)(SKU.ProductSKUMapIID);
                _dto.ProductSKUCode = Convert.ToString(SKU.ProductSKUCode);

                decimal ProductPrice = Convert.ToDecimal(SKU.ProductPrice * ConvertedPrice);
                _dto.ProductPrice = Utility.FormatDecimal(ProductPrice, 3);
                decimal DiscountedPrice = Convert.ToDecimal(SKU.DiscountedPrice * ConvertedPrice);
                _dto.DiscountedPrice = Utility.FormatDecimal(DiscountedPrice, 3);

                _dto.PricePercentage = SKU.PricePercentage;

                _dto.MinimumQuanityInCart = SKU.MinimumQuanityInCart;
                _dto.MaximumQuantityInCart = SKU.MaximumQuantityInCart;

                _dto.Quantity = SKU.Quantity;

                _dto.PropertyTypeList = new List<PropertyTypeDTO>();
                _dto.PropertyList = new List<PropertyDTO>();

                // for each for PropertyTypeID 
                foreach (var PropertyTypeID in distinctPropertyTypeID)
                {
                    // get the data based on PropertyTypeID 
                    var temp = (from q in qryProd where q.PropertyTypeID == PropertyTypeID.PropertyTypeID select q)
                        .GroupBy(p => new { p.PropertyIID, p.PropertyName })
                        .Select(g => g.First()).ToList();

                    // when PropertyTypeID is null  then we have to add the type & value in PropertyDTO else type in PropertyTypeDTO and value in PropertyDTO
                    if (string.IsNullOrEmpty(Convert.ToString(PropertyTypeID.PropertyTypeID)))
                    {
                        foreach (var item in temp)
                        {
                            if (item.PropertyIID.HasValue)
                            {
                                PropertyDTO _PropertyDTO = new PropertyDTO();
                                _PropertyDTO.PropertyIID = (long)item.PropertyIID;
                                _PropertyDTO.PropertyName = item.PropertyName;
                                _PropertyDTO.DefaultValue = item.Value;
                                //_PropertyDTO.ProductSKUMapId = item.ProductSKUMapIID;
                                //_PropertyDTO.ProductSKUCode = item.ProductSKUCode;
                                _dto.PropertyList.Add(_PropertyDTO);
                            }
                        }
                    }
                    else
                    {
                        PropertyTypeDTO _PropertyTypeDTO = new PropertyTypeDTO();
                        _PropertyTypeDTO.PropertyList = new List<PropertyDTO>();

                        _PropertyTypeDTO.PropertyTypeID = temp[0].PropertyTypeID;
                        _PropertyTypeDTO.PropertyTypeName = temp[0].PropertyTypeName;
                        _PropertyTypeDTO.ProductSKUMapId = temp[0].ProductSKUMapIID;
                        foreach (var item in temp)
                        {
                            PropertyDTO _PropertyDTO = new PropertyDTO();
                            if (listPropertyIID.Contains(item.PropertyIID))
                                _PropertyDTO.IsSelected = true;
                            else
                                _PropertyDTO.IsSelected = false;

                            _PropertyDTO.PropertyIID = (long)item.PropertyIID;
                            _PropertyDTO.PropertyName = item.PropertyName;
                            //_PropertyDTO.ProductSKUMapId = item.ProductSKUMapIID;
                            //_PropertyDTO.ProductSKUCode = item.ProductSKUCode;

                            _PropertyTypeDTO.PropertyList.Add(_PropertyDTO);
                        }
                        _dto.PropertyTypeList.Add(_PropertyTypeDTO);
                    }
                }

                // add images in ImageDTO
                foreach (var item in qryProdImage)
                {
                    ImageDTO _ImageDTO = new ImageDTO();
                    _ImageDTO.ImageId = item.ProductImageMapIID;
                    _ImageDTO.ImageTypeId = item.ProductImageTypeID;
                    _ImageDTO.ImageSrc = item.ImageFile;
                    _ImageDTO.ImageSequence = item.Sequence;
                    _dto.ImageList.Add(_ImageDTO);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return _dto;
        }

        public ProductFeatureDTO GetProductDetail(long ProductId, string propertyTypeValue, decimal? ConvertedPrice,
            Eduegate.Services.Contracts.Enums.ProductStatuses productStatus)
        {
            dbEduegateERPContext db = new dbEduegateERPContext();
            ProductFeatureDTO _dto = new ProductFeatureDTO();

            // get the products details 
            var qryProd = (from p in db.Products
                           join b in db.Brands on p.BrandID equals b.BrandIID

                           join ppm in db.ProductPropertyMaps on p.ProductIID equals ppm.ProductID into ppmLJ
                           from ppm in ppmLJ.DefaultIfEmpty()

                           join prt in db.PropertyTypes on ppm.PropertyTypeID equals prt.PropertyTypeID into one
                           from prt in one.DefaultIfEmpty()

                           join pr in db.Properties on ppm.PropertyID equals pr.PropertyIID into prLJ
                           from pr in prLJ.DefaultIfEmpty()

                           join psm in db.ProductSKUMaps on ppm.ProductSKUMapID equals psm.ProductSKUMapIID into two
                           from psm in two.DefaultIfEmpty()

                           join pplsm in db.ProductPriceListSKUMaps on psm.ProductSKUMapIID equals pplsm.ProductSKUID into three
                           from pplsm in three.DefaultIfEmpty()

                           join ppl in db.ProductPriceLists on pplsm.ProductPriceListID equals ppl.ProductPriceListIID into four
                           from ppl in four.DefaultIfEmpty()

                           join pis in db.ProductInventories on ppm.ProductSKUMapID equals pis.ProductSKUMapID into five
                           from pis in five.DefaultIfEmpty()

                           join pisc in db.ProductInventorySKUConfigMaps on psm.ProductSKUMapIID equals pisc.ProductSKUMapID into six
                           from pisc in six.DefaultIfEmpty()

                           join pic in db.ProductInventoryConfigs on pisc.ProductInventoryConfigID equals pic.ProductInventoryConfigIID into seven
                           from pic in seven.DefaultIfEmpty()

                               // price list should be left join and start and end date --join pi in db.ProductInventories on psm.ProductSKUMapIID equals pi.ProductSKUMapID
                           where p.ProductIID == ProductId //&& prt.PropertyTypeID == PropertyTypeId && pr.PropertyIID == PropertyId
                           //orderby p.ProductIID, psm.Sequence
                           select new
                           {
                               p.ProductIID,
                               p.ProductName,
                               b.BrandIID,
                               b.BrandName,
                               p.StatusID,
                               ProductSKUMapIID = (long?)psm.ProductSKUMapIID,
                               psm.ProductSKUCode,
                               psm.ProductPrice,
                               DiscountedPrice = (ppl.Price != null && ((ppl.StartDate != null && ppl.EndDate == null && DateTime.Now >= ppl.StartDate) || (DateTime.Now >= ppl.StartDate && DateTime.Now <= ppl.EndDate) || (ppl.StartDate == null && ppl.EndDate == null))) ? ppl.Price :
                                                       (ppl.PricePercentage != null && ((ppl.StartDate != null && ppl.EndDate == null && DateTime.Now >= ppl.StartDate) || (DateTime.Now >= ppl.StartDate && DateTime.Now <= ppl.EndDate) || (ppl.StartDate == null && ppl.EndDate == null))) ? (decimal?)((decimal?)psm.ProductPrice * (100 - (decimal?)ppl.PricePercentage)) / 100 :
                                                       psm.ProductPrice,
                               PricePercentage = ppl.Price != null ? (1 - (ppl.Price / psm.ProductPrice)) * 100 : ppl.PricePercentage,
                               prt.PropertyTypeID,
                               prt.PropertyTypeName,
                               PropertyIID = (long?)pr.PropertyIID,
                               pr.PropertyName,
                               ppm.Value,
                               psm.Sequence,
                               pic.MinimumQuanityInCart,
                               pic.MaximumQuantityInCart,
                               pis.Quantity
                           }).ToList();


            //Check product status
            if (productStatus != Services.Contracts.Enums.ProductStatuses.All)
            {
                sbyte statusID = Convert.ToSByte(productStatus);
                qryProd = qryProd.Where(x => x.StatusID == statusID).ToList();
            }

            // check if query is null then return here
            if (qryProd == null)
                return _dto;

            var propTypeValue = (from qry in db.GetSKUByProperties
                                 where qry.ProductIID == ProductId && qry.SKUPropertyID == propertyTypeValue
                                 select qry).FirstOrDefault();

            // get SKU detail based on sequence
            var SKU = (from qry in qryProd
                       where //qry.ProductIID == ProductId && qry.PropertyTypeID == PropertyTypeId && qry.PropertyIID == PropertyId
                       qry.ProductSKUMapIID == propTypeValue.ProductSKUMapIID
                       select new
                       {
                           qry.ProductSKUMapIID,
                           qry.ProductSKUCode,

                           qry.ProductPrice,
                           qry.DiscountedPrice,
                           //PricePercentage = (qry.ProductPrice != qry.DiscountedPrice ? (qry.PricePercentage != null ? qry.PricePercentage : decimal.ToInt32((decimal)(1 - (qry.DiscountedPrice / qry.ProductPrice)) * 100)) : 0),
                           qry.PricePercentage,
                           qry.MinimumQuanityInCart,
                           qry.MaximumQuantityInCart,
                           qry.Quantity
                       }).FirstOrDefault();

            // distinct PropertyTypeID for filter the data 
            var distinctPropertyTypeID = qryProd.GroupBy(x => x.PropertyTypeID)
                                        .Select(g => g.First()).ToList();

            // get the images of Products
            var qryProdImage = (from p in db.Products
                                join pim in db.ProductImageMaps on p.ProductIID equals pim.ProductID
                                where pim.ProductSKUMapID == (long)(SKU.ProductSKUMapIID)
                                select new
                                {
                                    p.ProductIID,
                                    pim.ProductImageMapIID,
                                    pim.ProductImageTypeID,
                                    pim.ImageFile,
                                    pim.Sequence
                                }).OrderBy(x => x.ProductImageMapIID).ThenBy(x => x.Sequence).ToList();


            _dto.ImageList = new List<ImageDTO>();
            _dto.ProductSKUDTOList = new List<ProductSKUDTO>();

            _dto.ProductID = qryProd[0].ProductIID;
            _dto.ProductName = qryProd[0].ProductName;

            _dto.BrandID = qryProd[0].BrandIID;
            _dto.BrandName = qryProd[0].BrandName;

            _dto.ProductSKUMapId = (long)(SKU.ProductSKUMapIID);
            _dto.ProductSKUCode = Convert.ToString(SKU.ProductSKUCode);

            decimal ProductPrice = Convert.ToDecimal(SKU.ProductPrice * ConvertedPrice);
            _dto.ProductPrice = Utility.FormatDecimal(ProductPrice, 3);
            decimal DiscountedPrice = Convert.ToDecimal(SKU.DiscountedPrice * ConvertedPrice);
            _dto.DiscountedPrice = Utility.FormatDecimal(DiscountedPrice, 3);


            _dto.PricePercentage = SKU.PricePercentage;
            _dto.MinimumQuanityInCart = SKU.MinimumQuanityInCart;
            _dto.MaximumQuantityInCart = SKU.MaximumQuantityInCart;
            _dto.Quantity = SKU.Quantity;

            _dto.PropertyTypeList = new List<PropertyTypeDTO>();
            _dto.PropertyList = new List<PropertyDTO>();

            var listPropertyIID = new List<string>(propertyTypeValue.Replace(", ", ",").Split(','));

            // for each for PropertyTypeID 
            foreach (var PropertyTypeID in distinctPropertyTypeID)
            {
                // get the data based on PropertyTypeID 
                var temp = (from q in qryProd where q.PropertyTypeID == PropertyTypeID.PropertyTypeID select q)
                    .GroupBy(p => new { p.PropertyIID, p.PropertyName })
                    .Select(g => g.First())
                    .ToList();

                // when PropertyTypeID is null  then we have to add the type & value in PropertyDTO else type in PropertyTypeDTO and value in PropertyDTO
                if (string.IsNullOrEmpty(Convert.ToString(PropertyTypeID.PropertyTypeID)))
                {
                    foreach (var item in temp)
                    {
                        PropertyDTO _PropertyDTO = new PropertyDTO();
                        _PropertyDTO.PropertyIID = (long)item.PropertyIID;
                        _PropertyDTO.PropertyName = item.PropertyName;
                        _PropertyDTO.DefaultValue = item.Value;
                        _dto.PropertyList.Add(_PropertyDTO);
                    }
                }
                else
                {
                    PropertyTypeDTO _PropertyTypeDTO = new PropertyTypeDTO();
                    _PropertyTypeDTO.PropertyList = new List<PropertyDTO>();

                    _PropertyTypeDTO.PropertyTypeID = temp[0].PropertyTypeID;
                    _PropertyTypeDTO.PropertyTypeName = temp[0].PropertyTypeName;
                    _PropertyTypeDTO.ProductSKUMapId = temp[0].ProductSKUMapIID;
                    foreach (var item in temp)
                    {
                        PropertyDTO _PropertyDTO = new PropertyDTO();
                        if (listPropertyIID.Contains(Convert.ToString(item.PropertyIID)))
                            _PropertyDTO.IsSelected = true;
                        else
                            _PropertyDTO.IsSelected = false;

                        _PropertyDTO.PropertyIID = (long)item.PropertyIID;
                        _PropertyDTO.PropertyName = item.PropertyName;
                        _PropertyTypeDTO.PropertyList.Add(_PropertyDTO);
                    }
                    //_PropertyTypeDTO.PropertyList = (from l in _PropertyTypeDTO.PropertyList
                    //                                 select l).OrderByDescending(x => x.IsSelected).Distinct().ToList();

                    _dto.PropertyTypeList.Add(_PropertyTypeDTO);
                }
            }

            // add images in ImageDTO
            foreach (var item in qryProdImage)
            {
                ImageDTO _ImageDTO = new ImageDTO();
                _ImageDTO.ImageId = item.ProductImageMapIID;
                _ImageDTO.ImageTypeId = item.ProductImageTypeID;
                _ImageDTO.ImageSrc = item.ImageFile;
                _ImageDTO.ImageSequence = item.Sequence;
                _dto.ImageList.Add(_ImageDTO);
            }

            return _dto;
        }

        public List<QuantityDTO> GetQuantityListByProductIID(decimal productIID)
        {
            List<QuantityDTO> quantityDTOList = new List<QuantityDTO>();

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                //var quantity = (from quantity in dbContext.ProductQuantityDiscounts
                //                    select quantity).ToList();
            }

            return quantityDTOList;
        }

        public List<ImageDTO> GetImagesByProductIID(decimal productIID)
        {
            List<ImageDTO> imageDTOList = new List<ImageDTO>();

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                //var quantity = (from quantity in dbContext.ProductQuantityDiscounts
                //                    select quantity).ToList();
            }

            return imageDTOList;
        }

        public ProductViewDTO GetProductSummaryInfo()
        {
            ProductViewDTO productViewDTO = new ProductViewDTO();

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                productViewDTO.TotalProduct = 12;
                productViewDTO.RecentlyAdded = "Apple iPhone 6S";
                productViewDTO.MostSellingProduct = "Galaxy S4";
                productViewDTO.OutOfStocks = 4;
                productViewDTO.PendingCreate = 2;
            }

            return productViewDTO;
        }

        //Get product view detail for admin
        public List<ProductItemDTO> GetProductViews(ProductViewSearchInfoDTO searchInfo)
        {
            if (searchInfo != null)
            {
                var productViews = new List<ProductItemDTO>();

                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {

                    //productViews = (from pro in (dbContext.Products.Select(x => new { x.ProductIID, x.ProductName, x.Brand, x.Created, x.ProductCode, x.Unit }).OrderBy(x => x.ProductIID).Skip((searchInfo.PageDetails.PageNumber - 1) * searchInfo.PageDetails.PageSize).Take(searchInfo.PageDetails.PageSize))
                    //                               select new ProductItemDTO { IID = pro.ProductIID, Name = pro.ProductName, Brand = pro.Brand.BrandName, CreatedOn = pro.Created.Value, BarCode = pro.ProductCode }).ToList();

                    var product = new ProductItemDTO();

                    product.IID = 100001;
                    product.Name = "Apple I5";
                    product.CreatedOn = "Nov 23 2015";
                    product.Quantity = 50;
                    product.PartNo = 1234456;
                    product.DeliveryDays = 02;
                    product.BarCode = "A1238152";
                    product.Location = "Market Place";
                    product.Category = "Electronics";
                    product.Brand = "Apple";
                    product.Price = 50;
                    product.Status = "En Ar";

                    productViews.Add(product);


                    product = new ProductItemDTO();

                    product.IID = 100002;
                    product.Name = "Apple I6";
                    product.CreatedOn = "Nov 23 2015";
                    product.Quantity = 50;
                    product.PartNo = 1234456;
                    product.DeliveryDays = 02;
                    product.BarCode = "A1238152";
                    product.Location = "Market Place";
                    product.Category = "Electronics";
                    product.Brand = "Apple";
                    product.Price = 50;
                    product.Status = "En Ar";

                    productViews.Add(product);

                    return productViews;
                }
            }

            return new List<ProductItemDTO>();
        }


        public List<ProductItemDTO> GetProductList()
        {
            List<ProductItemDTO> productsList = new List<ProductItemDTO>();

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var product = new ProductItemDTO();

                product.IID = 100001;
                product.Name = "Apple I5";
                product.Quantity = 50;
                product.Price = 150;
                product.UnitPrice = 150;
                product.DiscountPercentage = 20;

                productsList.Add(product);

                product = new ProductItemDTO();

                product.IID = 100002;
                product.Name = "Apple I6";
                product.Quantity = 30;
                product.Price = 180;
                product.UnitPrice = 180;
                product.DiscountPercentage = 25;

                productsList.Add(product);

                product = new ProductItemDTO();

                product.IID = 100003;
                product.Name = "Asus Zenfone";
                product.Quantity = 20;
                product.Price = 70;
                product.UnitPrice = 70;
                product.DiscountPercentage = 10;

                productsList.Add(product);
            }

            return productsList;
        }

        public TransactionHead SaveTransactions(TransactionHead transaction)
        {
            try
            {
                if (transaction.IsNotNull())
                {
                    using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                    {
                        if (transaction.HeadIID <= 0)
                        {
                            dbContext.TransactionHeads.Add(transaction);
                        }
                        else
                        {
                            TransactionHead dbTransactionHead = dbContext.TransactionHeads.Where(x => x.HeadIID == transaction.HeadIID).FirstOrDefault();

                            if (dbTransactionHead.IsNotNull())
                            {
                                dbTransactionHead.HeadIID = transaction.HeadIID;
                                dbTransactionHead.DocumentTypeID = transaction.DocumentTypeID;
                                dbTransactionHead.TransactionDate = transaction.TransactionDate;
                                dbTransactionHead.CustomerID = transaction.CustomerID;
                                dbTransactionHead.StudentID = transaction.StudentID;
                                dbTransactionHead.Description = transaction.Description;
                                dbTransactionHead.TransactionNo = transaction.TransactionNo;
                                dbTransactionHead.SupplierID = transaction.SupplierID;
                                dbTransactionHead.TransactionStatusID = transaction.TransactionStatusID;
                                dbTransactionHead.DiscountAmount = transaction.DiscountAmount;
                                dbTransactionHead.DiscountPercentage = transaction.DiscountPercentage;
                                dbTransactionHead.CreatedBy = transaction.CreatedBy;
                                dbTransactionHead.UpdatedBy = transaction.UpdatedBy;
                                dbTransactionHead.CreatedDate = transaction.CreatedDate;
                                dbTransactionHead.UpdatedDate = transaction.UpdatedDate;
                                dbTransactionHead.BranchID = transaction.BranchID;
                                dbTransactionHead.ToBranchID = transaction.ToBranchID;
                                dbTransactionHead.CurrencyID = transaction.CurrencyID;
                                dbTransactionHead.DeliveryDate = transaction.DeliveryDate;
                                dbTransactionHead.DeliveryMethodID = transaction.DeliveryMethodID;
                                dbTransactionHead.DueDate = transaction.DueDate;
                                dbTransactionHead.EntitlementID = transaction.EntitlementID;
                                dbTransactionHead.IsShipment = transaction.IsShipment;
                                dbTransactionHead.ReferenceHeadID = transaction.ReferenceHeadID;
                                dbTransactionHead.JobEntryHeadID = transaction.JobEntryHeadID;
                            }

                            if (transaction.TransactionShipments.IsNotNull() && transaction.TransactionShipments.Count > 0)
                            {
                                foreach (TransactionShipment ts in transaction.TransactionShipments)
                                {
                                    if (ts.TransactionShipmentIID <= 0)
                                        dbContext.Entry(ts).State = System.Data.Entity.EntityState.Added;
                                    else
                                        dbContext.Entry(ts).State = System.Data.Entity.EntityState.Modified;
                                }
                            }

                            //TransactionDetails won't be null if it is null we should not update DB atleast one record must be exist in DB.
                            if (transaction.TransactionDetails.IsNotNull() && transaction.TransactionDetails.Count > 0)
                            {
                                List<TransactionDetail> transactionToInsert = transaction.TransactionDetails.Where(x => x.DetailIID == default(long)).ToList();
                                List<TransactionDetail> transactionToUpdate = (from transDetails in transaction.TransactionDetails
                                                                               join dbTransaction in dbTransactionHead.TransactionDetails on transDetails.DetailIID equals dbTransaction.DetailIID
                                                                               select transDetails).ToList();

                                var transDetailIIDs = new HashSet<long>(transaction.TransactionDetails.Select(x => x.DetailIID));
                                List<TransactionDetail> transactionDetailsToDelete = dbTransactionHead.TransactionDetails.Where(x => !transDetailIIDs.Contains(x.DetailIID)).ToList();

                                if (transactionToUpdate != null)
                                {
                                    foreach (TransactionDetail tDetail in transactionToUpdate)
                                    {
                                        TransactionDetail transactionDetail = dbContext.TransactionDetails.Where(x => x.DetailIID == tDetail.DetailIID).FirstOrDefault();

                                        transactionDetail.DetailIID = tDetail.DetailIID;
                                        transactionDetail.HeadID = tDetail.HeadID;
                                        transactionDetail.ProductID = tDetail.ProductID;
                                        transactionDetail.ProductSKUMapID = tDetail.ProductSKUMapID;
                                        transactionDetail.Quantity = tDetail.Quantity;
                                        transactionDetail.Amount = tDetail.Amount;
                                        transactionDetail.UnitPrice = tDetail.UnitPrice;
                                        transactionDetail.UnitID = tDetail.UnitID;
                                        transactionDetail.DiscountPercentage = tDetail.DiscountPercentage;
                                        transactionDetail.ExchangeRate = tDetail.ExchangeRate;
                                        transactionDetail.UpdatedBy = tDetail.UpdatedBy;
                                        transactionDetail.UpdatedDate = tDetail.UpdatedDate;

                                        if (transactionDetail.ProductSerialMaps != null && transactionDetail.ProductSerialMaps.Count > 0)
                                        {
                                            dbContext.ProductSerialMaps.RemoveRange(transactionDetail.ProductSerialMaps);
                                        }

                                        transactionDetail.ProductSerialMaps.Clear();
                                        transactionDetail.ProductSerialMaps = tDetail.ProductSerialMaps;
                                    }
                                }

                                if (transactionToInsert != null)
                                {
                                    foreach (TransactionDetail tDetail in transactionToInsert)
                                    {
                                        TransactionDetail transactionDetail = tDetail;
                                        transactionDetail.HeadID = dbTransactionHead.HeadIID;
                                        dbContext.TransactionDetails.Add(transactionDetail);
                                    }
                                }

                                if (transactionDetailsToDelete != null)
                                {
                                    foreach (TransactionDetail tDetail in transactionDetailsToDelete)
                                    {
                                        dbTransactionHead.TransactionDetails.Remove(tDetail);
                                    }
                                }
                            }
                        }

                        dbContext.SaveChanges();
                    }
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetailRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }

            return transaction;
        }

        public List<ProductType> GetProductTypes()
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.ProductTypes.OrderBy(a => a.ProductTypeName).ToList();
            }
        }

        public List<ProductSKUTag> GetProdctSKUTags()
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.ProductSKUTags.ToList();
            }
        }

        public List<TransactionDetailDTO> GetTransactionByTransactionID(long transactionID)
        {
            List<TransactionDetailDTO> transactionDetails = new List<TransactionDetailDTO>();
            TransactionDetailDTO transactionDetail = null;

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var transactionDtls = (from transactionDet in dbContext.TransactionDetails
                                       where transactionDet.DetailIID == transactionID
                                       select transactionDet).ToList();

                if (transactionDtls != null && transactionDtls.Count > 0)
                {
                    foreach (var transactionDtl in transactionDtls)
                    {
                        transactionDetail = new TransactionDetailDTO();

                        transactionDetail.DetailIID = transactionDtl.DetailIID;
                        transactionDetail.HeadID = transactionDtl.HeadID;
                        transactionDetail.ProductID = transactionDtl.ProductID;
                        transactionDetail.Quantity = transactionDtl.Quantity;
                        transactionDetail.UnitID = transactionDtl.UnitID;
                        transactionDetail.Amount = transactionDtl.Amount;
                        transactionDetail.ExchangeRate = transactionDtl.ExchangeRate;
                        transactionDetail.CreatedBy = transactionDtl.CreatedBy;
                        transactionDetail.UpdatedBy = transactionDtl.UpdatedBy;
                        transactionDetail.UpdatedBy = transactionDtl.UpdatedBy;

                        transactionDetails.Add(transactionDetail);
                    }
                }
            }

            return transactionDetails;
        }

        public List<TransactionDetailDTO> GetTransactionByTransactionDate(DateTime transactionDate)
        {
            List<TransactionDetailDTO> transactionDetails = new List<TransactionDetailDTO>();
            TransactionDetailDTO transactionDetail = null;

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var transaction = (from transactionDet in dbContext.TransactionHeads
                                   where transactionDet.TransactionDate == transactionDate
                                   select transactionDet).FirstOrDefault();

                if (transaction != null)
                {
                    foreach (var transactionDtl in transaction.TransactionDetails)
                    {
                        transactionDetail = new TransactionDetailDTO();

                        transactionDetail.DetailIID = transactionDtl.DetailIID;
                        transactionDetail.HeadID = transactionDtl.HeadID;
                        transactionDetail.ProductID = transactionDtl.ProductID;
                        transactionDetail.Quantity = transactionDtl.Quantity;
                        transactionDetail.UnitID = transactionDtl.UnitID;
                        transactionDetail.Amount = transactionDtl.Amount;
                        transactionDetail.ExchangeRate = transactionDtl.ExchangeRate;
                        transactionDetail.CreatedBy = transactionDtl.CreatedBy;
                        transactionDetail.UpdatedBy = transactionDtl.UpdatedBy;
                        transactionDetail.UpdatedBy = transactionDtl.UpdatedBy;

                        transactionDetails.Add(transactionDetail);
                    }
                }
            }

            return transactionDetails;
        }

        public ProductPriceList CreatePriceInformationDetail(ProductPriceList productPrice)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                dbContext.ProductPriceLists.Add(productPrice);
                if (productPrice.ProductPriceListIID == default(long))
                    dbContext.Entry(productPrice).State = System.Data.Entity.EntityState.Added;
                else
                {
                    dbContext.Entry(productPrice).State = System.Data.Entity.EntityState.Modified;
                }

                dbContext.SaveChanges();
                return productPrice;
            }


        }

        public List<ProductPriceListSKUMap> GetProductPriceSKUMaps()
        {
            List<ProductPriceListSKUMap> productPriceListSKUMaps = new List<ProductPriceListSKUMap>();

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    productPriceListSKUMaps = (from productPriceSKUMap in dbContext.ProductPriceListSKUMaps
                                               select productPriceSKUMap).ToList();
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetailRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }

            return productPriceListSKUMaps;
        }

        public List<ProductPriceListSKUMap> UpdateSKUProductPrice(List<ProductPriceListSKUMap> productPriceSKUMapList)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    // remove the data 
                    List<ProductPriceListSKUMap> productPriceListSKUMaps = dbContext.ProductPriceListSKUMaps.ToList().Where(x => x.ProductPriceListID == productPriceSKUMapList[0].ProductPriceListID).ToList();
                    if (productPriceListSKUMaps != null)
                    {
                        dbContext.ProductPriceListSKUMaps.RemoveRange(productPriceListSKUMaps);
                    }

                    if (productPriceSKUMapList != null && productPriceSKUMapList.Count > 0)
                    {
                        // add the data 
                        foreach (var item in productPriceSKUMapList)
                        {
                            if (item.ProductSKUID != null && item.ProductSKUID > 0)
                            {
                                dbContext.ProductPriceListSKUMaps.Add(item);
                                //dbContext.SaveChanges();
                            }
                        }
                    }
                    dbContext.SaveChanges();
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetailRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }

            return productPriceSKUMapList;
        }


        public void UpdateSKUPrice(List<ProductPriceListSKUMap> productPriceSKUMapList)
        {

            dbEduegateERPContext dbContext = new dbEduegateERPContext();
            // get all from [catalog].[ProductPriceListSKUMaps] based on ProductPriceListID
            var productPriceListSKUMaps = dbContext.ProductPriceListSKUMaps.Where(x => x.ProductPriceListID == productPriceSKUMapList[0].ProductPriceListID).ToList();
            foreach (var productPriceListSKUMap in productPriceListSKUMaps)
            {
                if (productPriceSKUMapList.Contains(productPriceListSKUMap))
                {
                    // update
                    if (productPriceSKUMapList != null && productPriceSKUMapList.Count > 0)
                    {
                        foreach (ProductPriceListSKUMap productPriceSKUMap in productPriceSKUMapList)
                        {
                            if (productPriceSKUMap.ProductPriceListItemMapIID > 0)
                            {
                                ProductPriceListSKUMap skuMap = dbContext.ProductPriceListSKUMaps.Where(x => x.ProductPriceListItemMapIID == productPriceSKUMap.ProductPriceListItemMapIID).FirstOrDefault();

                                if (skuMap.IsNotNull())
                                {
                                    skuMap.ProductPriceListItemMapIID = productPriceSKUMap.ProductPriceListItemMapIID;
                                    skuMap.ProductPriceListID = productPriceSKUMap.ProductPriceListID;
                                    skuMap.ProductSKUID = productPriceSKUMap.ProductSKUID;
                                    skuMap.SellingQuantityLimit = productPriceSKUMap.SellingQuantityLimit;
                                    skuMap.Amount = productPriceSKUMap.Amount;
                                    skuMap.SortOrder = productPriceSKUMap.SortOrder;
                                    skuMap.PricePercentage = productPriceSKUMap.PricePercentage;

                                    dbContext.SaveChanges();
                                }

                            }
                            else
                            {
                                dbContext.ProductPriceListSKUMaps.Add(productPriceSKUMap);

                                dbContext.SaveChanges();
                            }
                        }
                    }
                }
                else
                {
                    // delete
                }


            }
        }

        public long UpdateProduct(Product productDetails, MultimediaDTO dtoMultimedia,int companyID)
        {
            long productID = 0;
            bool UpdateforeignKeyReference = false;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    if (productDetails.ProductIID <= 0)
                    {
                        dbContext.Products.Add(productDetails);
                        // Expect ProductInventoryConfig will add : Check it
                        UpdateforeignKeyReference = true;

                        foreach (var productskuMap in productDetails.ProductSKUMaps)
                        {
                            foreach (var skuTag in productskuMap.ProductSKUTagMaps)
                            {
                                if (skuTag.ProductSKUTagID != 0)
                                {
                                    dbContext.Entry(skuTag.ProductSKUTag).State = System.Data.Entity.EntityState.Modified;
                                }
                            }
                        }
                    }
                    else
                    {
                        var dbproduct = dbContext.Products.Where(x => x.ProductIID == productDetails.ProductIID).FirstOrDefault();
                        dbproduct.ProductName = productDetails.ProductName;
                        dbproduct.StatusID = productDetails.StatusID;
                        dbproduct.UnitID = productDetails.UnitID;
                        dbproduct.BrandID = productDetails.BrandID;
                        dbproduct.ProductTypeID = productDetails.ProductTypeID;
                        dbproduct.ProductOwnderID = productDetails.ProductOwnderID;
                        dbproduct.IsOnline = productDetails.IsOnline;
                        dbproduct.Updated = DateTime.Now;
                        dbproduct.UpdateBy = productDetails.UpdateBy;
                        dbproduct.TaxTemplateID = productDetails.TaxTemplateID;
                        dbproduct.SellingUnitID = productDetails.SellingUnitID;
                        dbproduct.PurchaseUnitID = productDetails.PurchaseUnitID;
                        dbproduct.PurchaseUnitGroupID = productDetails.PurchaseUnitGroupID;
                        dbproduct.SellingUnitGroupID = productDetails.SellingUnitGroupID;
                        dbproduct.GLAccountID = productDetails.GLAccountID;
                        dbproduct.IsActive = productDetails.IsActive;
                        dbproduct.ProductCode = productDetails.ProductCode;
                        dbproduct.Calorie = productDetails.Calorie;
                        dbproduct.Weight = productDetails.Weight;

                        //var imagemap = new ProductImageMap();
                        //if (productDetails.ProductImageMaps != null && productDetails.ProductImageMaps.Count > 0)
                        //{
                        //    foreach (var images in productDetails.ProductImageMaps)
                        //    {
                        //        //imagemap = new ProductImageMap()
                        //        //{
                        //        //    ImageFile = images.ImageFile,
                        //        //    ProductImageMapIID = images.ProductImageMapIID,
                        //        //    ProductSKUMapID = images.ProductSKUMapID,
                        //        //    Sequence = images.Sequence,
                        //        //    ProductImageTypeID = images.ProductImageTypeID
                        //        //};
                        //        dbContext.Entry(images).State = System.Data.Entity.EntityState.Added;
                        //        dbContext.SaveChanges();
                        //    }
                        //}


                        if (dbproduct.ProductPropertyMaps != null && dbproduct.ProductPropertyMaps.Count > 0)
                        {
                            dbContext.ProductPropertyMaps.RemoveRange(dbproduct.ProductPropertyMaps);
                        }
                        dbContext.SaveChanges();
                        if (productDetails.ProductPropertyMaps.IsNotNull() && productDetails.ProductPropertyMaps.Count > 0)
                        {
                            dbproduct.ProductPropertyMaps = productDetails.ProductPropertyMaps; //Product tags are adding to productproperty maps
                        }
                        dbContext.SaveChanges();
                        if (dbproduct.ProductCategoryMaps != null && dbproduct.ProductCategoryMaps.Count > 0)
                        {
                            if (dbproduct.ProductCategoryMaps.Any(x => x.FeeDueInventoryMaps.Count == 0))
                            {
                                dbContext.ProductCategoryMaps.RemoveRange(dbproduct.ProductCategoryMaps);
                            }
                        }
                        dbContext.SaveChanges();


                        if (dbproduct.ProductAllergyMaps != null && dbproduct.ProductAllergyMaps.Count > 0)
                        {
                           dbContext.ProductAllergyMaps.RemoveRange(dbproduct.ProductAllergyMaps);

                        }
                        dbContext.SaveChanges();

                        if (productDetails.ProductCategoryMaps != null && productDetails.ProductCategoryMaps.Count > 0)
                        {
                            if (productDetails.ProductCategoryMaps.Any(x => x.FeeDueInventoryMaps.Count == 0))
                            {
                                dbproduct.ProductCategoryMaps = productDetails.ProductCategoryMaps;
                            }
                            
                        }
                        dbContext.SaveChanges();
                        if (productDetails.ProductTagMaps != null && productDetails.ProductTagMaps.Count > 0)
                        {
                            dbproduct.ProductTagMaps = productDetails.ProductTagMaps;
                        }
                        dbContext.SaveChanges();
                        if (productDetails.ProductInventoryProductConfigMaps.IsNotNull())
                        {
                            // First Check in ProductInventoryProductConfigMaps based on ProductID if exist then Update else Insert
                            var dbProductInventoryProductConfigMap = dbContext.ProductInventoryProductConfigMaps.Where(x => x.ProductID == productDetails.ProductIID)
                                .FirstOrDefault();
                            foreach (var item in productDetails.ProductInventoryProductConfigMaps)
                            {
                                if (item.IsNotNull())
                                {
                                    foreach (var confiItem in item.ProductInventoryConfig.ProductInventoryConfigCultureDatas.ToList())
                                    {
                                        //item.ProductInventoryConfig.ProductInventoryConfigCultureDatas.Remove(confiItem);
                                        confiItem.ProductInventoryConfigID = dbProductInventoryProductConfigMap.ProductInventoryConfigID;
                                    }

                                    if (dbProductInventoryProductConfigMap.IsNotNull())
                                    {
                                        item.ProductInventoryConfig.ProductInventoryConfigIID = dbProductInventoryProductConfigMap.ProductInventoryConfigID;
                                        SaveProductInventoryConfig(item.ProductInventoryConfig);
                                    }
                                    else
                                    {
                                        SaveProductInventoryConfig(item.ProductInventoryConfig);
                                    }
                                }
                            }
                        }
                        dbContext.SaveChanges();
                        if (dbproduct.ProductDeliveryCountrySettings.IsNotNull() && dbproduct.ProductDeliveryCountrySettings.Count > 0)
                        {
                            dbContext.ProductDeliveryCountrySettings.RemoveRange(dbproduct.ProductDeliveryCountrySettings.Where(x => x.ProductSKUMapID == null).ToList());
                        }
                        dbContext.SaveChanges();
                        dbContext.ProductDeliveryCountrySettings.AddRange(productDetails.ProductDeliveryCountrySettings.Where(x => x.ProductSKUMapID == null).ToList());
                        dbContext.SaveChanges();

                        if (productDetails.ProductCultureDatas != null && productDetails.ProductCultureDatas.Count > 0)
                        {
                            foreach (var item in productDetails.ProductCultureDatas)
                            {
                                var productCultureData = dbproduct.ProductCultureDatas.Where(x => x.CultureID == item.CultureID && x.ProductID == item.ProductID).FirstOrDefault();

                                if (productCultureData != null)
                                {
                                    productCultureData.CultureID = item.CultureID;
                                    productCultureData.ProductName = item.ProductName;
                                }
                            }
                        }
                        dbContext.SaveChanges();
                        if (dbproduct.ProductFamilyID != productDetails.ProductFamilyID)
                        {
                            //Removing all the propertymaps and sku config maps related to skus
                            foreach (var sku in dbproduct.ProductSKUMaps)
                            {
                                dbContext.ProductPropertyMaps.RemoveRange(sku.ProductPropertyMaps);
                                dbContext.ProductInventorySKUConfigMaps.RemoveRange(sku.ProductInventorySKUConfigMaps);
                                dbContext.ProductImageMaps.RemoveRange(sku.ProductImageMaps);
                            }
                            dbContext.SaveChanges();
                            //Removing all the Image maps to skus
                            //dbContext.ProductImageMaps.RemoveRange(dbproduct.ProductImageMaps);
                            //dbContext.SaveChanges();
                            //Removing all the skus

                            
                            //dbContext.ProductSKUMaps.RemoveRange(dbproduct.ProductSKUMaps);

                            //dbproduct.ProductSKUMaps = productDetails.ProductSKUMaps;
                            //dbContext.SaveChanges();
                            //Update Product SKU Config
                            dbproduct.ProductPropertyMaps = productDetails.ProductPropertyMaps;
                            dbContext.SaveChanges();
                            // Expecting ProductInventoryConfig should work : Check it
                            if (dbproduct.ProductSKUMaps != null && dbproduct.ProductSKUMaps.Count() > 0)
                            {
                                foreach (var sku in dbproduct.ProductSKUMaps)
                                {
                                    sku.ProductImageMaps = productDetails.ProductImageMaps;
                                    // ProductInventorySKUConfig
                                    // Check into Product Inventory SKU Config
                                    var dbProductInventorySKUConfigMap = dbContext.ProductInventorySKUConfigMaps.Where(x => x.ProductSKUMapID == sku.ProductSKUMapIID)
                                        .FirstOrDefault();

                                    foreach (var item in sku.ProductInventorySKUConfigMaps)
                                    {
                                        if (item.IsNotNull())
                                        {
                                            if (dbProductInventorySKUConfigMap.IsNotNull())
                                            {
                                                item.ProductInventoryConfig.ProductInventoryConfigIID = dbProductInventorySKUConfigMap.ProductInventoryConfigID;
                                                SaveProductInventoryConfig(item.ProductInventoryConfig);
                                            }
                                            else
                                            {
                                                SaveProductInventoryConfig(item.ProductInventoryConfig);
                                            }
                                        }
                                    }
                                }
                                dbContext.SaveChanges();

                            }
                        }
                        else
                        {
                            if (productDetails.ProductSKUMaps != null && productDetails.ProductSKUMaps.Count > 0)
                            {
                                var skusToRemove = new List<ProductSKUMap>();

                                if (dbproduct.ProductSKUMaps != null && dbproduct.ProductSKUMaps.Count > 0)
                                {
                                    foreach (var sku in dbproduct.ProductSKUMaps)
                                    {
                                        //Updating  the existing lines and mapping the line to be removed.

                                        var isSKUExist = productDetails.ProductSKUMaps.Where(x => x.ProductSKUMapIID == sku.ProductSKUMapIID);

                                        if (isSKUExist.Count() > 0)
                                        {
                                            var skuLineToUpdate = isSKUExist.First();
                                            ProductSKUMap productskuMap = sku;
                                            productskuMap.BarCode = skuLineToUpdate.BarCode;
                                            productskuMap.ProductPrice = skuLineToUpdate.ProductPrice;
                                            productskuMap.PartNo = skuLineToUpdate.PartNo;
                                            productskuMap.IsHiddenFromList = skuLineToUpdate.IsHiddenFromList;
                                            productskuMap.Sequence = skuLineToUpdate.Sequence;
                                            //productskuMap.ProductSKUCode = skuLineToUpdate.ProductSKUCode;
                                            productskuMap.VariantsMap = skuLineToUpdate.VariantsMap;
                                            productskuMap.HideSKU = skuLineToUpdate.HideSKU;
                                            productskuMap.SKUName = skuLineToUpdate.SKUName;
                                            productskuMap.StatusID = skuLineToUpdate.StatusID;
                                            productskuMap.ProductSKUCode = skuLineToUpdate.ProductSKUCode;

                                            if (skuLineToUpdate.ProductImageMaps != null && skuLineToUpdate.ProductImageMaps.Count > 0)
                                            {
                                                // First remove the existing ProductImageMaps data for that SKU
                                                var imageMapToBeRemoved = dbproduct.ProductImageMaps.Where(x => x.ProductSKUMapID == skuLineToUpdate.ProductSKUMapIID).ToList();
                                                dbContext.ProductImageMaps.RemoveRange(imageMapToBeRemoved);

                                                // First remove the existing ProductImageMaps data using sku
                                                var imageSkumapDataList = dbContext.ProductImageMaps.Where(x => x.ProductSKUMapID == sku.ProductSKUMapIID).ToList();
                                                foreach (var listData in imageSkumapDataList)
                                                {
                                                    dbContext.ProductImageMaps.Remove(listData);
                                                }
                                            }
                                            dbContext.SaveChanges();

                                            if (productDetails.ProductImageMaps != null && productDetails.ProductImageMaps.Count > 0)
                                            {
                                                productskuMap.ProductImageMaps = new List<ProductImageMap>();
                                                foreach (var imageMap in productDetails.ProductImageMaps)
                                                {
                                                    productskuMap.ProductImageMaps.Add(new ProductImageMap()
                                                    {
                                                        ProductID = productDetails.ProductIID,
                                                        ProductSKUMapID = imageMap.ProductSKUMapID,
                                                        ProductImageTypeID = imageMap.ProductImageTypeID,
                                                        ImageFile = imageMap.ImageFile,
                                                        Sequence = imageMap.Sequence,
                                                        ImageFileReference = imageMap.ImageFileReference,
                                                        CreatedBy = imageMap.CreatedBy,
                                                        CreatedDate = imageMap.CreatedDate,
                                                        UpdatedBy = imageMap.UpdatedBy.HasValue ? imageMap.UpdatedBy : dbproduct.UpdateBy,
                                                        UpdatedDate = DateTime.Now,
                                                    });
                                                }

                                            }
                                            // Remove Product Video Map
                                            var listProductVideoMap = dbContext.ProductVideoMaps.Where(x => x.ProductID == productDetails.ProductIID).ToList();
                                            dbContext.ProductVideoMaps.RemoveRange(listProductVideoMap);
                                            dbContext.SaveChanges();
                                            // Product Video Map
                                            if (skuLineToUpdate.ProductVideoMaps != null && skuLineToUpdate.ProductVideoMaps.Count > 0)
                                            {
                                                foreach (var videoMap in skuLineToUpdate.ProductVideoMaps)
                                                {
                                                    ProductVideoMap productskuVideoMap = videoMap;
                                                    dbproduct.ProductVideoMaps.Add(videoMap);
                                                }
                                            }

                                            if (skuLineToUpdate.ProductPropertyMaps != null && skuLineToUpdate.ProductPropertyMaps.Count > 0)
                                            {
                                                var propertiesToBeRemoved = dbproduct.ProductSKUMaps.Where(x => x.ProductSKUMapIID == skuLineToUpdate.ProductSKUMapIID).FirstOrDefault();
                                                dbContext.ProductPropertyMaps.RemoveRange(propertiesToBeRemoved.ProductPropertyMaps);
                                                productskuMap.ProductPropertyMaps = skuLineToUpdate.ProductPropertyMaps;
                                            }
                                            dbContext.SaveChanges();
                                            //Update the tags at the SKU level
                                            dbContext.ProductSKUTagMaps.RemoveRange(productskuMap.ProductSKUTagMaps.Where(x=>x.CompanyID == companyID));
                                            dbContext.SaveChanges();    
                                            foreach (var skuTag in skuLineToUpdate.ProductSKUTagMaps)
                                            {
                                                productskuMap.ProductSKUTagMaps.Add(skuTag);

                                                // As we are already removing skuTagMaps commented below. Please change if wrong
                                                //if (skuTag.ProductSKUTagID != 0)
                                                //{
                                                //    dbContext.Entry(skuTag.ProductSKUTag).State = EntityState.Modified;
                                                //}
                                            }

                                            var productSkuCultureDataList = dbContext.ProductSKUCultureDatas.Where(x => x.ProductSKUMapID == sku.ProductSKUMapIID).ToList();
                                            foreach (var listData in productSkuCultureDataList)
                                            {
                                                dbContext.ProductSKUCultureDatas.Remove(listData);
                                            }

                                            productskuMap.ProductSKUCultureDatas = new List<ProductSKUCultureData>();

                                            foreach (var skuCulture in skuLineToUpdate.ProductSKUCultureDatas)
                                            {
                                                productskuMap.ProductSKUCultureDatas.Add(new ProductSKUCultureData()
                                                {
                                                    CultureID = skuCulture.CultureID,
                                                    ProductSKUMapID = skuCulture.ProductSKUMapID,
                                                    ProductSKUName = skuCulture.ProductSKUName,
                                                    ProductSKUDescription = skuCulture.ProductSKUDescription,
                                                    ProductSKUDetails = skuCulture.ProductSKUDetails,
                                                    CreatedBy = skuCulture.CreatedBy,
                                                    CreatedDate = skuCulture.CreatedDate,
                                                    UpdatedBy = skuCulture.UpdatedBy.HasValue ? skuCulture.UpdatedBy : dbproduct.UpdateBy,
                                                    UpdatedDate = DateTime.Now,
                                                });
                                            }

                                            //dbContext.ProductSKUCultureDatas.RemoveRange(productskuMap.ProductSKUCultureDatas);
                                            //foreach (var skuCulture in skuLineToUpdate.ProductSKUCultureDatas)
                                            //{
                                            //    productskuMap.ProductSKUCultureDatas.Add(skuCulture);
                                            //}
                                            dbContext.SaveChanges();

                                            var productInventorySKUConfigMapDataList = dbContext.ProductInventorySKUConfigMaps.Where(x => x.ProductSKUMapID == sku.ProductSKUMapIID).ToList();
                                            foreach (var listData in productInventorySKUConfigMapDataList)
                                            {
                                                dbContext.ProductInventorySKUConfigMaps.Remove(listData);
                                            }

                                            productskuMap.ProductInventorySKUConfigMaps = new List<ProductInventorySKUConfigMap>();

                                            foreach (var skuConfigMap in skuLineToUpdate.ProductInventorySKUConfigMaps)
                                            {
                                                productskuMap.ProductInventorySKUConfigMaps.Add(new ProductInventorySKUConfigMap()
                                                {
                                                    ProductInventoryConfigID = skuConfigMap.ProductInventoryConfigID,
                                                    ProductSKUMapID = skuConfigMap.ProductSKUMapID,
                                                    //Price = skuConfigMap.Price,
                                                    CreatedBy = skuConfigMap.CreatedBy,
                                                    CreatedDate = skuConfigMap.CreatedDate,
                                                    UpdatedBy = skuLineToUpdate.UpdatedBy.HasValue ? skuLineToUpdate.UpdatedBy : dbproduct.UpdateBy,
                                                    UpdatedDate = DateTime.Now,
                                                });
                                            }

                                            //productskuMap.ProductInventorySKUConfigMaps = skuLineToUpdate.ProductInventorySKUConfigMaps;

                                            //foreach (var item in productskuMap.ProductInventorySKUConfigMaps)
                                            //{
                                            //    if (item.ProductSKUMapID.IsNotNull())
                                            //    {
                                            //        dbContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
                                            //    }
                                            //    else
                                            //    {
                                            //        dbContext.Entry(item).State = System.Data.Entity.EntityState.Added;
                                            //    }

                                            //}
                                        }
                                        else
                                        {
                                            skusToRemove.Add(sku);
                                        }
                                    }

                                    if (skusToRemove.Count > 0)
                                    {
                                        foreach (var skuToRemove in skusToRemove)
                                        {
                                            var propertymapsToRemove = dbproduct.ProductPropertyMaps.Where(x => x.ProductSKUMapID == skuToRemove.ProductSKUMapIID).ToList();
                                            var imageMapsToRemove = dbproduct.ProductImageMaps.Where(x => x.ProductSKUMapID == skuToRemove.ProductSKUMapIID).ToList();
                                            var ProductDeliveryCountrySettingsRemove = dbContext.ProductDeliveryCountrySettings.Where(x => x.ProductSKUMapID == skuToRemove.ProductSKUMapIID).ToList();
                                            dbContext.ProductPropertyMaps.RemoveRange(skuToRemove.ProductPropertyMaps);

                                            if (ProductDeliveryCountrySettingsRemove.IsNotNull() && ProductDeliveryCountrySettingsRemove.Count > 0)
                                            {
                                                foreach (var countrySetting in ProductDeliveryCountrySettingsRemove)
                                                {
                                                    RemoveProductDeliveryCountrySetting(countrySetting);
                                                }
                                            }
                                            dbContext.SaveChanges();
                                            foreach (var propertyMap in propertymapsToRemove)
                                            {
                                                dbproduct.ProductPropertyMaps.Remove(propertyMap);
                                            }
                                            dbContext.SaveChanges();
                                            foreach (var imageMapToRemove in imageMapsToRemove)
                                            {
                                                dbproduct.ProductImageMaps.Remove(imageMapToRemove);
                                            }
                                            dbContext.SaveChanges();
                                            // Remove Product Video Map
                                            var videoMapsToRemoves = dbproduct.ProductVideoMaps.Where(x => x.ProductSKUMapID == skuToRemove.ProductSKUMapIID).ToList();
                                            dbContext.ProductVideoMaps.RemoveRange(videoMapsToRemoves);
                                            dbContext.SaveChanges();

                                            // ProductInventorySKUConfig
                                            var removeProductInventorySKUConfig = dbContext.ProductInventorySKUConfigMaps
                                                .Where(x => x.ProductSKUMapID == skuToRemove.ProductSKUMapIID).FirstOrDefault();
                                            dbContext.SaveChanges();
                                            // ProductInvemtoryConfig
                                            if (removeProductInventorySKUConfig.IsNotNull())
                                            {
                                                var removeProductInventoryConfig = dbContext.ProductInventoryConfigs
                                                    .Where(x => x.ProductInventoryConfigIID == removeProductInventorySKUConfig.ProductInventoryConfigID).FirstOrDefault();

                                                dbContext.ProductInventoryConfigs.Remove(removeProductInventoryConfig);
                                                dbContext.ProductInventorySKUConfigMaps.Remove(removeProductInventorySKUConfig);
                                            }

                                            dbContext.SaveChanges();
                                            dbproduct.ProductSKUMaps.Remove(skuToRemove);
                                            dbContext.SaveChanges();
                                        }
                                    }

                                    //adding the new lines
                                    var addNewSKUs = productDetails.ProductSKUMaps.Where(x => x.ProductSKUMapIID <= 0).ToList();

                                    foreach (var newsku in addNewSKUs)
                                    {
                                        //inserting new lines
                                        dbContext.ProductSKUMaps.Add(newsku);
                                        foreach (var newImageMap in newsku.ProductImageMaps)
                                        {
                                            dbContext.ProductImageMaps.Add(newImageMap);
                                        }

                                        // Product Video Maps
                                        foreach (var newVideoMap in newsku.ProductVideoMaps)
                                        {
                                            dbContext.ProductVideoMaps.Add(newVideoMap);
                                        }
                                    }
                                    dbContext.SaveChanges();
                                    //ProductDeliveryCountrySetting
                                    foreach (var dsSKUMap in productDetails.ProductSKUMaps)
                                    {
                                        var existdbProductDeliveryCountrySettings = dbContext.ProductDeliveryCountrySettings.Where(x => x.ProductSKUMapID == dsSKUMap.ProductSKUMapIID).ToList();

                                        if (existdbProductDeliveryCountrySettings.IsNotNull() && existdbProductDeliveryCountrySettings.Count > 0)
                                        {
                                            dbContext.ProductDeliveryCountrySettings.RemoveRange(existdbProductDeliveryCountrySettings);
                                        }

                                        dbContext.ProductDeliveryCountrySettings.AddRange(dsSKUMap.ProductDeliveryCountrySettings);
                                    }
                                    dbContext.SaveChanges();
                                    //foreach (var dsSKUMap in productDetails.ProductSKUMaps)
                                    //{
                                    //    var existdbProductInventorySKUConfigMaps = dbContext.ProductInventorySKUConfigMaps.Where(x => x.ProductSKUMapID == dsSKUMap.ProductSKUMapIID).ToList();

                                    //    if (existdbProductInventorySKUConfigMaps.IsNotNull() && existdbProductInventorySKUConfigMaps.Count > 0)
                                    //    {
                                    //        dbContext.ProductInventorySKUConfigMaps.RemoveRange(existdbProductInventorySKUConfigMaps);
                                    //    }

                                    //    dbContext.ProductInventorySKUConfigMaps.AddRange(dsSKUMap.ProductInventorySKUConfigMaps);
                                    //}
                                    //dbContext.SaveChanges();
                                }
                                else
                                {
                                    dbproduct.ProductSKUMaps = productDetails.ProductSKUMaps;
                                    dbproduct.ProductImageMaps = productDetails.ProductImageMaps;
                                    dbproduct.ProductVideoMaps = productDetails.ProductVideoMaps;

                                }
                            }
                            else
                            {
                                //dbproduct.ProductSKUMaps.Clear();
                                var propertymapsToRemove = dbproduct.ProductPropertyMaps.Where(x => x.ProductSKUMapID != null).ToList();
                                var imageMapsToRemove = dbproduct.ProductImageMaps.Where(x => x.ProductSKUMapID != null).ToList();
                                foreach (var propertyMap in propertymapsToRemove)
                                {
                                    dbproduct.ProductPropertyMaps.Remove(propertyMap);
                                }
                                dbContext.SaveChanges();
                                foreach (var imageMapToRemove in imageMapsToRemove)
                                {
                                    dbproduct.ProductImageMaps.Remove(imageMapToRemove);
                                }
                                dbContext.SaveChanges();
                                // Product Video Maps
                                var videoMapsToRemoves = dbproduct.ProductVideoMaps.Where(x => x.ProductSKUMapID != null).ToList();
                                foreach (var videoMapToRemove in videoMapsToRemoves)
                                {
                                    dbproduct.ProductVideoMaps.Remove(videoMapToRemove);
                                }
                                dbContext.SaveChanges();
                            }

                            var defaultPropertiesToDelete = new List<ProductPropertyMap>();
                            var defaultPropertiesToUpdate = new List<ProductPropertyMap>();
                            var defaultPropertiesToInsert = new List<ProductPropertyMap>();

                            if (productDetails.ProductPropertyMaps.Where(x => x.ProductSKUMapID == null) != null)
                            {
                                if (dbproduct.ProductPropertyMaps.Where(x => x.ProductSKUMapID == null) != null)
                                {
                                    defaultPropertiesToUpdate = (from property in productDetails.ProductPropertyMaps
                                                                 join dbProp in dbproduct.ProductPropertyMaps on property.PropertyID equals dbProp.PropertyID
                                                                 where property.ProductSKUMapID == null
                                                                 select property).ToList();

                                    //var defaultPropertiesToInsert = from property in productDetails.ProductPropertyMaps
                                    //                                join dbProp in product.ProductPropertyMaps on property.PropertyID not in dbProp.PropertyID
                                    //                                where property.ProductSKUMapID == null 
                                    //                                select property;


                                    defaultPropertiesToInsert = productDetails.ProductPropertyMaps.Except(dbproduct.ProductPropertyMaps).Where(x => x.ProductSKUMapID == null).ToList();

                                    defaultPropertiesToDelete = dbproduct.ProductPropertyMaps.Except(productDetails.ProductPropertyMaps).Where(x => x.ProductSKUMapID == null).ToList();
                                }
                                else
                                {
                                    defaultPropertiesToInsert = productDetails.ProductPropertyMaps.Where(x => x.ProductSKUMapID == null).ToList();
                                }
                            }
                            else
                            {
                                defaultPropertiesToDelete = dbproduct.ProductPropertyMaps.Where(x => x.ProductSKUMapID == null).ToList();
                            }

                            if (defaultPropertiesToDelete != null && defaultPropertiesToDelete.Count > 0)
                            {
                                foreach (var defaultProperty in defaultPropertiesToDelete)
                                {
                                    dbproduct.ProductPropertyMaps.Remove(defaultProperty);
                                }
                                dbContext.SaveChanges();
                            }

                            if (defaultPropertiesToUpdate != null && defaultPropertiesToUpdate.Count > 0)
                            {
                                foreach (var defaultProperty in defaultPropertiesToUpdate)
                                {
                                    var dbPropertyToUpdate = dbproduct.ProductPropertyMaps.Where(x => x.PropertyID == defaultProperty.PropertyID).FirstOrDefault();

                                    if (dbPropertyToUpdate != null)
                                    {
                                        dbPropertyToUpdate.Value = defaultProperty.Value;
                                        dbPropertyToUpdate.UpdatedDate = defaultProperty.UpdatedDate;
                                    }
                                }
                            }

                            if (defaultPropertiesToInsert != null && defaultPropertiesToInsert.Count > 0)
                            {
                                foreach (var defaultProperty in defaultPropertiesToInsert)
                                {
                                    dbproduct.ProductPropertyMaps.Add(defaultProperty);
                                }
                            }
                        }

                        dbproduct.ProductFamilyID = productDetails.ProductFamilyID;

                        if (dbproduct.SeoMetadata != null && dbproduct.SeoMetadata.SEOMetadataIID != 0)
                        {
                            productDetails.SeoMetadata.SEOMetadataIID = dbproduct.SeoMetadata.SEOMetadataIID;
                        }


                        if (!dbproduct.SeoMetadataIID.HasValue)
                        {
                            if (productDetails.SeoMetadata != null)
                            {
                                dbproduct.SeoMetadata = productDetails.SeoMetadata;
                                dbContext.Entry(dbproduct.SeoMetadata).State = System.Data.Entity.EntityState.Added;
                            }
                        }
                        else
                        {
                            //dbContext.Entry(dbproduct.SeoMetadata).State = System.Data.Entity.EntityState.Modified;
                            dbproduct.SeoMetadata.SEOMetadataIID = productDetails.SeoMetadata.SEOMetadataIID;
                            dbproduct.SeoMetadata.PageTitle = productDetails.SeoMetadata.PageTitle;
                            dbproduct.SeoMetadata.MetaKeywords = productDetails.SeoMetadata.MetaKeywords;
                            dbproduct.SeoMetadata.MetaDescription = productDetails.SeoMetadata.MetaDescription;
                            dbproduct.SeoMetadata.UrlKey = productDetails.SeoMetadata.UrlKey;
                            dbproduct.SeoMetadata.UpdatedBy = productDetails.SeoMetadata.UpdatedBy;
                            dbproduct.SeoMetadata.UpdatedDate = productDetails.SeoMetadata.UpdatedDate;
                        }

                        if (dbproduct.SeoMetadata != null)
                        {
                            foreach (var cultureData in dbproduct.SeoMetadata.SeoMetadataCultureDatas)
                            {
                                if (cultureData.SEOMetadataID == 0)
                                    dbContext.Entry(cultureData).State = System.Data.Entity.EntityState.Added;
                                else
                                    dbContext.Entry(cultureData).State = System.Data.Entity.EntityState.Modified;
                            }
                        }

                        UpdateProductBundles(productDetails, dbproduct, dbContext, dbproduct.ProductSKUMaps);
                    }

                    try
                    {
                        dbContext.SaveChanges();

                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }


                    if (UpdateforeignKeyReference)
                    {
                        long productIID = productDetails.ProductIID;

                        foreach (var sku in productDetails.ProductSKUMaps)
                        {
                            foreach (var property in sku.ProductPropertyMaps)
                            {
                                property.ProductID = productIID;
                                ProductPropertyMap product = dbContext.ProductPropertyMaps.Where(x => x.ProductSKUMapID == property.ProductPropertyMapIID).FirstOrDefault();
                            }
                            foreach (var SKUImageMap in sku.ProductImageMaps)
                            {
                                SKUImageMap.ProductID = productIID;
                                SKUImageMap.ImageFile = SKUImageMap.ImageFile.Replace("0", productIID.ToString());
                            }

                            // Product Video Map
                            foreach (var SKUVideoMap in sku.ProductVideoMaps)
                            {
                                SKUVideoMap.ProductID = productIID;
                                SKUVideoMap.VideoFile = SKUVideoMap.VideoFile.Replace("0", productIID.ToString());
                            }

                            dbContext.SaveChanges();
                        }
                    }



                    // Save ProductInventoryProductConfigMap
                    foreach (var item in productDetails.ProductInventoryProductConfigMaps)
                    {
                        item.ProductID = productDetails.ProductIID;
                        item.ProductInventoryConfigID = item.ProductInventoryConfig.ProductInventoryConfigIID;
                        SaveProductInventoryProductConfigMap(item);
                    }

                    // Save ProductInventorySKUConfigMap
                    foreach (var sku in productDetails.ProductSKUMaps)
                    {
                        foreach (var item in sku.ProductInventorySKUConfigMaps)
                        {
                            item.ProductSKUMapID = sku.ProductSKUMapIID;
                            item.ProductInventoryConfigID = item.ProductInventoryConfig.ProductInventoryConfigIID;
                            SaveProductInventorySKUConfigMap(item);
                        }
                    }

                    GetProduct(productDetails.ProductIID);

                    if (dtoMultimedia != null)
                    {
                        if (Directory.Exists(dtoMultimedia.ImageSourcePath))
                        {
                            ReLocateUploadImages(dtoMultimedia.ImageSourcePath, dtoMultimedia.ImageDestinationPath, Convert.ToInt64(productDetails.ProductIID), "Image");
                        }
                    }

                    if (dtoMultimedia != null)
                    {
                        if (Directory.Exists(dtoMultimedia.VideoSourcePath))
                        {
                            ReLocateUploadImages(dtoMultimedia.VideoSourcePath, dtoMultimedia.VideoDestinationPath, Convert.ToInt64(productDetails.ProductIID), "Video");
                        }
                    }

                    productID = productDetails.ProductIID;
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to Add/Update the product. TrackId:0", 0);
                throw ex;
            }

            return productID;
        }       

        public List<Brand> GetBrandList()
        {
            var brands = new List<Brand>();

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                brands = (from brand in dbContext.Brands
                          select brand).OrderBy(a => a.BrandName).ToList();
            }

            return brands;
        }

        public List<Brand> GetBrandList(string searchText, int pageSize)
        {
            var brands = new List<Brand>();

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                brands = dbContext.Brands.Where(x => x.BrandName != null && x.BrandName.Contains(searchText)).Take(pageSize).ToList();
            }

            return brands;
        }

        public List<ProductFamily> GetProductFamilies()
        {
            var productFamilies = new List<ProductFamily>();

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var code = (from productfamily in dbContext.ProductFamilys
                            select productfamily);

                productFamilies = (from productfamily in dbContext.ProductFamilys
                                   select productfamily).ToList();
            }
            return productFamilies;
        }

        public List<ProductFamily> GetProductFamilies(string searchText, int pageSize)
        {
            var productFamilies = new List<ProductFamily>();

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                productFamilies = dbContext.ProductFamilys.Where(c => c.FamilyName != null && c.FamilyName.Contains(searchText)).Take(pageSize).ToList();
            }
            return productFamilies;
        }

        public ProductFamily GetProductFamily(long familyID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var family = dbContext.ProductFamilys.Where(x => x.ProductFamilyIID == familyID)
                            .Include("ProductFamilyPropertyMaps")
                            .Include("ProductFamilyPropertyMaps.Property")
                            .Include("ProductFamilyPropertyTypeMaps.PropertyType")
                            .FirstOrDefault();

                if (family.IsNotNull() && family.ProductFamilyPropertyTypeMaps.IsNotNull())
                {
                    foreach (var familyProperty in family.ProductFamilyPropertyTypeMaps)
                    {
                        if (familyProperty.PropertyType.IsNull())
                        {
                            dbContext.Entry(familyProperty).Reference(x => x.PropertyType).Load();
                        }
                    }
                }

                return family;
            }
        }

        public ProductFamily SaveProductFamily(ProductFamily entity)
        {
            ProductFamily updatedEntity = null;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    dbContext.ProductFamilys.Add(entity);

                    if (entity.ProductFamilyIID == 0)
                        dbContext.Entry(entity).State = System.Data.Entity.EntityState.Added;
                    else
                        dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;

                    var propertiesToRemove = dbContext.ProductFamilyPropertyMaps.Where(a => a.ProductFamilyID == entity.ProductFamilyIID);
                    dbContext.ProductFamilyPropertyMaps.RemoveRange(propertiesToRemove);

                    var propertyTypesToRemove = dbContext.ProductFamilyPropertyTypeMaps.Where(a => a.ProductFamilyID == entity.ProductFamilyIID);
                    dbContext.ProductFamilyPropertyTypeMaps.RemoveRange(propertyTypesToRemove);

                    dbContext.SaveChanges();

                    updatedEntity = dbContext.ProductFamilys
                         .Include("ProductFamilyPropertyMaps")
                         .Include("ProductFamilyPropertyMaps.Property").Where(x => x.ProductFamilyIID == entity.ProductFamilyIID)
                         .Include("ProductFamilyPropertyTypeMaps.PropertyType").Where(x => x.ProductFamilyIID == entity.ProductFamilyIID)
                         .FirstOrDefault();
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetailRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }

            return updatedEntity;
        }

        public List<PropertyType> GetPropertyTypes()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.PropertyTypes.OrderBy(a => a.PropertyTypeName).ToList();
            }
        }

        public List<Property> GetProperties()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Properties.ToList();
            }
        }

        public List<Property> GetProperties(string searchText, int pageSize)
        {
            var properties = new List<Property>();

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (searchText.IsNotNullOrEmpty())
                    properties = dbContext.Properties.Where(p => p.PropertyName != null && p.PropertyName.Contains(searchText)).OrderBy(a => a.PropertyName).Take(pageSize).ToList();
                else
                    properties = dbContext.Properties.Where(p => p.PropertyName != null).OrderBy(p => p.PropertyName).Take(pageSize).ToList();
            }

            return properties;
        }

        public Property GetProperty(long propertyID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Properties.Where(a => a.PropertyIID == propertyID).FirstOrDefault();
            }
        }

        public Property SaveProperty(Property entity)
        {
            Property updatedEntity = null;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    dbContext.Properties.Add(entity);

                    if (entity.PropertyIID == 0)
                        dbContext.Entry(entity).State = System.Data.Entity.EntityState.Added;
                    else
                        dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;

                    dbContext.SaveChanges();
                    updatedEntity = dbContext.Properties.Where(x => x.PropertyIID == entity.PropertyIID).FirstOrDefault();
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetailRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }

            return updatedEntity;
        }

        public List<ProductFamilyType> GetProductFamilyTypes()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.ProductFamilyTypes.OrderBy(a => a.FamilyTypeName).ToList();
            }
        }

        public List<ProductStatu> GetProductStatus()
        {
            var prductStatusList = new List<ProductStatu>();

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                prductStatusList = (from status in dbContext.ProductStatus
                                    select status).ToList();
            }
            return prductStatusList;
        }

        public List<PropertyType> GetProductPropertyTypes(decimal productFamilyIID)
        {
            var propertyTypes = new List<PropertyType>();

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                propertyTypes = (from pf in dbContext.ProductFamilyPropertyTypeMaps
                                 join pt in dbContext.PropertyTypes on pf.PropertyTypeID equals pt.PropertyTypeID
                                 where pf.ProductFamilyID == productFamilyIID
                                 select pt).ToList();

            }
            return propertyTypes;
        }

        public List<Property> GetPropertiesByPropertyTypeID(decimal propertyTypeIID)
        {
            var properties = new List<Property>();

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                properties = (from ptm in dbContext.PropertyTypePropertyMaps
                              join pt in dbContext.Properties on ptm.PropertyID equals pt.PropertyIID
                              where ptm.PropertyTypeID == propertyTypeIID
                              select pt).ToList();
            }

            return properties;
        }

        public PropertyType GetPropertyTypeByPropertyTypeID(decimal propertyTypeID)
        {
            var propertytypes = new PropertyType();
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                propertytypes = (from pt in dbContext.PropertyTypes
                                 where pt.PropertyTypeID == propertyTypeID
                                 select pt).FirstOrDefault();
            }
            return propertytypes;
        }

        public Product GetProduct(long productIID)
        {
            var productDetails = new Product();
            dbEduegateERPContext dbContext = new dbEduegateERPContext();
            try
            {
                productDetails = (from product in dbContext.Products
                                  where product.ProductIID == productIID
                                  select product).FirstOrDefault();
            }
            catch (Exception ex)
            {
                dbContext.Dispose();
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to Fetch the product. TrackId:" + productIID, 0);
            }
            dbContext = null;
            return productDetails;
        }

        public List<Property> GetProperitesByProductFamilyID(long productFamilyID)
        {
            var properties = new List<Property>();

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                properties = (from property in dbContext.Properties
                              let propertymap = from familyProperty in dbContext.ProductFamilyPropertyMaps
                                                where familyProperty.ProductFamilyID == productFamilyID && familyProperty.ProductPropertyID != null
                                                select familyProperty.ProductPropertyID
                              where propertymap.Contains(property.PropertyIID)
                              select property).ToList();
            }
            return properties;
        }

        public bool ReLocateUploadImages(string sourcePath, string designationPath, long productID, string mode)
        {
            string[] imageNames = Directory.GetFiles(sourcePath).Select(path => Path.GetFileName(path)).ToArray();
            string[] directories = Directory.GetDirectories(sourcePath);

            // For images
            if (imageNames != null && imageNames.Count() > 0)
            {
                var largeImagePath = string.Empty;

                if (mode.ToLower() == "image")
                {
                    largeImagePath = string.Format("{0}\\{1}\\{2}", designationPath, productID, Helper.ImageType.LargeImage.ToString());
                }
                else // For Video
                {
                    largeImagePath = string.Format("{0}\\{1}", designationPath, productID);
                }

                if (!Directory.Exists(largeImagePath))
                {
                    Directory.CreateDirectory(largeImagePath);
                }

                foreach (var image in imageNames)
                {
                    if (System.IO.File.Exists(largeImagePath + "//" + image))
                    {
                        System.IO.File.Delete(largeImagePath + "//" + image);
                    }

                    using (var stream = File.Open(sourcePath + "//" + image, FileMode.Open))
                    {
                        // Use stream
                        // Use stream
                        using (FileStream fileStream = File.Create(largeImagePath + "//" + image, (int)stream.Length))
                        {
                            // Initialize the bytes array with the stream length and then fill it with data
                            byte[] bytesInStream = new byte[stream.Length];
                            stream.Read(bytesInStream, 0, bytesInStream.Length);
                            // Use write method to write to the file specified above
                            fileStream.Write(bytesInStream, 0, bytesInStream.Length);
                        }
                    }
                    //System.IO.File.Copy(sourcePath + "//" + image, largeImagePath + "//" + image);
                    //System.IO.File.Move(sourcePath + "//" + image, largeImagePath);
                }
            }

            // For directories
            if (directories != null && directories.Count() > 0)
            {
                foreach (var dir in directories)
                {
                    var dirName = Path.GetFileName(dir);
                    var imagePath = string.Format("{0}\\{1}\\{2}", designationPath, productID, dirName.ToString());
                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }
                    var imageSourcePath = sourcePath + "\\" + dirName;
                    string[] images = Directory.GetFiles(imageSourcePath).Select(path => Path.GetFileName(path)).ToArray();

                    if (images != null && images.Count() > 0)
                    {
                        foreach (var image in images)
                        {



                            using (var stream = File.Open(imageSourcePath + "//" + image, FileMode.Open))
                            {
                                if (System.IO.File.Exists(imagePath + "//" + image))
                                {
                                    System.IO.File.Delete(imagePath + "//" + image);
                                }

                                // Use stream
                                using (FileStream fileStream = File.Create(imagePath + "//" + image, (int)stream.Length))
                                {
                                    // Initialize the bytes array with the stream length and then fill it with data
                                    byte[] bytesInStream = new byte[stream.Length];
                                    stream.Read(bytesInStream, 0, bytesInStream.Length);
                                    // Use write method to write to the file specified above
                                    fileStream.Write(bytesInStream, 0, bytesInStream.Length);
                                }



                            }

                            //System.IO.File.Copy(imageSourcePath + "//" + image, imagePath + "//" + image);
                            //System.IO.File.Move(imageSourcePath + "//" + image, imagePath);
                        }
                    }
                }
            }

            return true;
        }

        public Product GetProductBySKUID(long productSKUMapID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return (from p in db.Products
                        join sku in db.ProductSKUMaps on p.ProductIID equals sku.ProductID
                        where sku.ProductSKUMapIID == productSKUMapID
                        select p).SingleOrDefault();
            }
        }

        public List<SearchResult> GetSuggestedProduct(long productID)
        {
            dbEduegateERPContext db = new dbEduegateERPContext();
            List<SearchResult> lists;
            try
            {
                lists = (from p in db.Products
                         join b in db.Brands on p.BrandID equals b.BrandIID
                         join psm in db.ProductSKUMaps on p.ProductIID equals psm.ProductID

                         join pim in db.ProductImageMaps on psm.ProductSKUMapIID equals pim.ProductSKUMapID into psmLJ
                         from pim in psmLJ.DefaultIfEmpty()

                         join pplsm in db.ProductPriceListSKUMaps on psm.ProductSKUMapIID equals pplsm.ProductSKUID into one
                         from pplsm in one.DefaultIfEmpty()

                         join ppl in db.ProductPriceLists on pplsm.ProductPriceListID equals ppl.ProductPriceListIID into two
                         from ppl in two.DefaultIfEmpty()

                         join pi in db.ProductInventories on psm.ProductSKUMapIID equals pi.ProductSKUMapID into productStock
                         from pi in productStock.DefaultIfEmpty()

                         join pp in db.ProductToProductMaps on p.ProductIID equals pp.ProductIDTo

                         where pp.ProductID == productID
                         && (pp.SalesRelationTypeID == (int)Eduegate.Framework.Enums.SalesRelationshipType.UpSell || pp.SalesRelationTypeID == (int)Eduegate.Framework.Enums.SalesRelationshipType.CrossSell)
                        && (pim.Sequence == null || pim.Sequence == 1) && (pim.ProductImageTypeID == null || pim.ProductImageTypeID == 39)

                         select
                         new SearchResult()
                         {
                             ProductIID = p.ProductIID,
                             ProductCode = p.ProductCode,
                             ProductName = p.ProductName,
                             BrandIID = b.BrandIID,
                             BrandName = b.BrandName,
                             ProductPrice = psm.ProductPrice,
                             ImageFile = pim.ImageFile,
                             ProductSKUCode = psm.ProductSKUCode,
                             SKUID = psm.ProductSKUMapIID,
                             DiscountedPrice = ((ppl.Price != null && ((ppl.StartDate != null && ppl.EndDate == null && DateTime.Now >= ppl.StartDate) ||
                                                 (DateTime.Now >= ppl.StartDate && DateTime.Now <= ppl.EndDate) || (ppl.StartDate == null && ppl.EndDate == null))) ?
                                                 ppl.Price :
                                                 (ppl.PricePercentage != null && ((ppl.StartDate != null && ppl.EndDate == null && DateTime.Now >= ppl.StartDate) ||
                                                 (DateTime.Now >= ppl.StartDate && DateTime.Now <= ppl.EndDate) || (ppl.StartDate == null && ppl.EndDate == null))) ?
                                                 ((decimal?)((decimal?)psm.ProductPrice * (100 - (decimal?)ppl.PricePercentage)) / 100) :
                                                 psm.ProductPrice),
                             HasStock = (pi.Quantity != null && pi.Quantity > 0) ? true : false,
                         }).Distinct().ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return lists;
        }

        public List<Product> GetProductListBySearchText(string searchText, long excludeProductFamilyID)
        {
            var productList = new List<Product>();

            using (Eduegate.Domain.Entity.Models.dbEduegateERPContext dbContext = new Eduegate.Domain.Entity.Models.dbEduegateERPContext())
            {
                if (excludeProductFamilyID != default(long))
                {
                    productList = (from product in (dbContext.Products.Select(x => new { x.ProductIID, x.ProductName, x.ProductFamilyID }).Where(a => a.ProductName.Contains(searchText) && a.ProductFamilyID != excludeProductFamilyID).Take(50).ToList())
                                   select new Product { ProductIID = product.ProductIID, ProductName = product.ProductName }).ToList();
                }

                else
                {
                    productList = (from product in (dbContext.Products.Select(x => new { x.ProductIID, x.ProductName }).Where(a => a.ProductName.Contains(searchText)).Take(50).ToList())
                                   select new Product { ProductIID = product.ProductIID, ProductName = product.ProductName }).ToList();
                }

                return productList;
            }
        }

        public List<Product> GetProductListByCategoryID(long categoryID)
        {
            var productList = new List<Product>();

            using (Eduegate.Domain.Entity.Models.dbEduegateERPContext dbContext = new Eduegate.Domain.Entity.Models.dbEduegateERPContext())
            {
                productList = (from p in dbContext.Products
                               join b in dbContext.ProductCategoryMaps on p.ProductIID equals b.ProductID
                               where b.CategoryID == categoryID
                               select p).ToList();
             
                return productList;
            }
        }

        public List<ProductBundle> GetBundleDetails(long bundleId)
        {
            var bundleList = new List<ProductBundle>();

            using (Eduegate.Domain.Entity.Models.dbEduegateERPContext dbContext = new Eduegate.Domain.Entity.Models.dbEduegateERPContext())
            {
                bundleList = (from bndl in dbContext.ProductBundles.AsEnumerable()
                              join prdct in dbContext.Products on bndl.ToProductID equals prdct.ProductIID
                              where (bndl.ToProductSKUMapID == bundleId)
                              orderby bndl.BundleIID descending
                              select bndl).ToList();

                return bundleList;
            }
        }

        public bool SaveProductMaps(List<ProductToProductMap> productMaps, CallContext context)
        {
            var isUpdated = false;
            bool isExist = false;
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                if (productMaps.IsNotNull() && productMaps.Count > 0)
                {
                    // ------ get the ProductToProductMap list based on ProductID
                    List<ProductToProductMap> dbProductToProductMap = new List<ProductToProductMap>();
                    dbProductToProductMap = db.ProductToProductMaps.AsEnumerable().Where(x => x.ProductID == productMaps[0].ProductID).ToList();

                    if (dbProductToProductMap.IsNotNull())
                    {
                        // First handle delete
                        foreach (var dbProductMap in dbProductToProductMap)
                        {
                            // check dbObject is exist or not in userObject
                            isExist = productMaps.Any(x => x.ProductID == dbProductMap.ProductID
                            && x.ProductIDTo == dbProductMap.ProductIDTo && x.SalesRelationTypeID == dbProductMap.SalesRelationTypeID);

                            if (!isExist)
                            {
                                // delete from the DB
                                db.ProductToProductMaps.Remove(dbProductMap);
                                //db.SaveChanges();
                            }
                        }
                    }




                    // ------- handle Insert and Update
                    foreach (var productMap in productMaps)
                    {
                        if (productMap.ProductIDTo.IsNotNull())
                        {
                            // check if exist then Update else Insert
                            isExist = db.ProductToProductMaps.Any(x => x.ProductID == productMap.ProductID
                                && x.ProductIDTo == productMap.ProductIDTo && x.SalesRelationTypeID == productMap.SalesRelationTypeID);

                            if (!isExist)
                            {
                                db.ProductToProductMaps.Add(productMap);
                            }
                            else
                            {
                                // Update
                                ProductToProductMap map = db.ProductToProductMaps.Where(x => x.ProductID == productMap.ProductID
                                && x.ProductIDTo == productMap.ProductIDTo && x.SalesRelationTypeID == productMap.SalesRelationTypeID).FirstOrDefault();

                                map.UpdatedDate = DateTime.Now;
                                map.UpdatedBy = Convert.ToInt32(context.LoginID);
                            }
                        }
                    }
                    db.SaveChanges();
                }
                isUpdated = true;
            }


            return isUpdated;
        }

        public string GetProductNameByID(long productID)
        {
            using (Eduegate.Domain.Entity.Models.dbEduegateERPContext dbContext = new Eduegate.Domain.Entity.Models.dbEduegateERPContext())
            {
                string productName = dbContext.Products.Where(x => x.ProductIID == productID).Select(y => y.ProductName).FirstOrDefault();
                return productName;
            }
        }

        public string GetProductAndSKUNameByID(long productSKUMapIID)
        {
            string productSKU = string.Empty;

            if (productSKUMapIID > 0)
            {
                using (Eduegate.Domain.Entity.Models.dbEduegateERPContext dbContext = new Eduegate.Domain.Entity.Models.dbEduegateERPContext())
                {
                    string searchQuery = "select SKU from catalog.ProductSkuSearchview where ProductSKUMapIID=" + productSKUMapIID;
                    ProductSKUDetail skuDetail = dbContext.Database.SqlQuery<ProductSKUDetail>(searchQuery).FirstOrDefault();

                    if (skuDetail.IsNotNull())
                        productSKU = skuDetail.SKU;
                }
            }

            return productSKU;
        }

        public List<ProductToProductMap> GetProductMaps(long productID)
        {
            using (Eduegate.Domain.Entity.Models.dbEduegateERPContext dbContext = new Eduegate.Domain.Entity.Models.dbEduegateERPContext())
            {
                List<ProductToProductMap> dbProductMaps = dbContext.ProductToProductMaps.Where(x => x.ProductID == productID).ToList();
                return dbProductMaps;
            }
        }

        public TransactionDTO GetTransaction(long headIID)
        {
            TransactionDTO transactionDTO = new TransactionDTO();
            transactionDTO.TransactionHead = new TransactionHeadDTO();
            transactionDTO.TransactionDetails = new List<TransactionDetailDTO>();
            TransactionDetailDTO transactionDetailDTO = null;

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                TransactionHead transactionModel = (from transaction in dbContext.TransactionHeads
                                                    where transaction.HeadIID == headIID
                                                    select transaction).FirstOrDefault();

                if (transactionModel != null)
                {
                    transactionDTO.TransactionHead.HeadIID = transactionModel.HeadIID;
                    transactionDTO.TransactionHead.Description = transactionModel.Description;
                    transactionDTO.TransactionHead.Reference = transactionModel.Reference;
                    transactionDTO.TransactionHead.CustomerID = transactionModel.CustomerID;
                    transactionDTO.TransactionHead.StudentID = transactionModel.StudentID;
                    transactionDTO.TransactionHead.SupplierID = transactionModel.SupplierID;
                    transactionDTO.TransactionHead.TransactionDate = transactionModel.TransactionDate.IsNull() ? (DateTime?)null : Convert.ToDateTime(transactionModel.TransactionDate);
                    transactionDTO.TransactionHead.TransactionNo = transactionModel.TransactionNo;
                    transactionDTO.TransactionHead.TransactionStatusID = transactionModel.TransactionStatusID;
                    transactionDTO.TransactionHead.DocumentStatusID = transactionModel.DocumentStatusID;
                    transactionDTO.TransactionHead.DocumentTypeID = transactionModel.DocumentTypeID;
                    transactionDTO.TransactionHead.DiscountAmount = transactionModel.DiscountAmount;
                    transactionDTO.TransactionHead.DiscountPercentage = transactionModel.DiscountPercentage;
                    transactionDTO.TransactionHead.BranchID = transactionModel.BranchID != null ? (long)transactionModel.BranchID : default(long);
                    transactionDTO.TransactionHead.ToBranchID = transactionModel.ToBranchID != null ? (long)transactionModel.ToBranchID : default(long);
                    transactionDTO.TransactionHead.CurrencyID = transactionModel.CurrencyID != null ? (int)transactionModel.CurrencyID : default(int);
                    transactionDTO.TransactionHead.DeliveryDate = transactionModel.DeliveryDate != null ? (DateTime)transactionModel.DeliveryDate : (DateTime?)null;
                    transactionDTO.TransactionHead.DeliveryTypeID = transactionModel.DeliveryMethodID != null ? (short)transactionModel.DeliveryMethodID : default(short);
                    transactionDTO.TransactionHead.DueDate = transactionModel.DueDate != null ? (DateTime)transactionModel.DueDate : (DateTime?)null;
                    transactionDTO.TransactionHead.EntitlementID = transactionModel.EntitlementID;
                    transactionDTO.TransactionHead.IsShipment = transactionModel.IsShipment != null ? (bool)transactionModel.IsShipment : default(bool);
                    transactionDTO.TransactionHead.ReferenceHeadID = transactionModel.ReferenceHeadID;

                    var supplier = dbContext.Suppliers.Where(x => x.SupplierIID == transactionModel.SupplierID).FirstOrDefault();

                    if (supplier != null)
                        transactionDTO.TransactionHead.SupplierName = string.Concat(supplier.FirstName + supplier.MiddleName + " " + supplier.LastName);

                    var currency = dbContext.Currencies.Where(x => x.CurrencyID == transactionModel.CurrencyID).FirstOrDefault();

                    if (currency != null)
                        transactionDTO.TransactionHead.CurrencyName = currency.Name;

                    var customer = dbContext.Customers.Where(x => x.CustomerIID == transactionModel.CustomerID).FirstOrDefault();

                    if (customer != null)
                        transactionDTO.TransactionHead.CustomerName = string.Concat(customer.FirstName + customer.MiddleName + " " + customer.LastName);

                    var student = dbContext.Students.Where(x => x.StudentIID == transactionModel.StudentID).FirstOrDefault();

                    if (student != null)
                        transactionDTO.TransactionHead.StudentName = string.Concat(student.FirstName + student.MiddleName + " " + student.LastName);

                    if (transactionModel.TransactionDetails != null && transactionModel.TransactionDetails.Count > 0)
                    {
                        foreach (var transactionDetail in transactionModel.TransactionDetails)
                        {
                            transactionDetailDTO = new TransactionDetailDTO();
                            transactionDetailDTO.SKUDetails = new List<ProductSerialMapDTO>();

                            transactionDetailDTO.DetailIID = transactionDetail.DetailIID;
                            transactionDetailDTO.HeadID = transactionDetail.HeadID;
                            transactionDetailDTO.ProductID = transactionDetail.ProductID;

                            var searchQuery = string.Concat("select * from [catalog].[ProductListBySKU] where productskumapiid = '" + transactionDetail.ProductSKUMapID + "'");
                            ProductSKUDetail product = dbContext.Database.SqlQuery<ProductSKUDetail>(searchQuery).FirstOrDefault();

                            if (product != null)
                            {
                                transactionDetailDTO.ProductName = product.SKU;
                                transactionDetailDTO.ImageFile = product.ImageFile;
                                transactionDetailDTO.SellingQuantityLimit = product.Quantity;

                                if (product.Quantity != null && product.SellingQuantityLimit != null)
                                {
                                    if (product.SellingQuantityLimit > product.Quantity)
                                        transactionDetailDTO.SellingQuantityLimit = product.SellingQuantityLimit;
                                }
                            }

                            transactionDetailDTO.ProductSKUMapID = transactionDetail.ProductSKUMapID;
                            transactionDetailDTO.UnitID = transactionDetail.UnitID;
                            transactionDetailDTO.UnitPrice = transactionDetail.UnitPrice;
                            transactionDetailDTO.Quantity = transactionDetail.Quantity;
                            transactionDetailDTO.DiscountPercentage = transactionDetail.DiscountPercentage;
                            transactionDetailDTO.Amount = transactionDetail.Amount;

                            if (transactionDetail.ProductSerialMaps != null && transactionDetail.ProductSerialMaps.Count > 0)
                            {
                                foreach (var serailMap in transactionDetail.ProductSerialMaps)
                                {
                                    var skuDetail = new ProductSerialMapDTO();
                                    skuDetail.SerialNo = serailMap.SerialNo;
                                    transactionDetailDTO.SKUDetails.Add(skuDetail);
                                }
                            }
                            transactionDTO.TransactionDetails.Add(transactionDetailDTO);
                        }
                    }

                    if (transactionModel.TransactionShipments.IsNotNull() && transactionModel.TransactionShipments.Count > 0)
                    {
                        foreach (var ts in transactionModel.TransactionShipments)
                        {
                            transactionDTO.ShipmentDetails = new ShipmentDetailDTO()
                            {
                                TransactionShipmentIID = ts.TransactionShipmentIID,
                                TransactionHeadID = ts.TransactionHeadID,
                                SupplierIDFrom = ts.SupplierIDFrom,
                                SupplierIDTo = ts.SupplierIDTo,
                                ShipmentReference = ts.ShipmentReference,
                                FreightCareer = ts.FreightCarrier,
                                ClearanceTypeID = ts.ClearanceTypeID,
                                AirWayBillNo = ts.AirWayBillNo,
                                FrieghtCharges = ts.FreightCharges,
                                BrokerCharges = ts.BrokerCharges,
                                AdditionalCharges = ts.AdditionalCharges,
                                Weight = ts.Weight,
                                NoOfBoxes = ts.NoOfBoxes,
                                BrokerAccount = ts.BrokerAccount,
                                Remarks = ts.Description,
                                CreatedBy = ts.CreatedBy,
                                UpdatedBy = ts.UpdatedBy,
                                CreatedDate = ts.CreatedDate,
                                UpdatedDate = ts.UpdatedDate,
                                //TimeStamps = ts.TimeStamps,
                            };
                        }
                    }
                }
            }

            return transactionDTO;
        }

        public bool RemoveProductPriceListSKUMaps(long id)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                ProductPriceListSKUMap item = db.ProductPriceListSKUMaps.Where(x => x.ProductPriceListItemMapIID == id).FirstOrDefault();
                db.ProductPriceListSKUMaps.Remove(item);
                db.SaveChanges();
                return true;
            }

        }

        public ProductPriceList GetProductPriceListDetail(long id, int companyID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                ProductPriceList list = db.ProductPriceLists.Where(x => x.ProductPriceListIID == id && x.CompanyID == companyID)
                    .Include(x => x.ProductPriceListLevel)
                    .Include(x => x.ProductPriceListType)
                    .Include(x => x.ProductPriceListBranchMaps)
                    .Include(x => x.ProductPriceListBranchMaps.Select(y => y.Branch))
                    .FirstOrDefault();
                return list;
            }
        }

        public List<ProductPriceSKU> GetProductPriceSKU(long id)
        {
            List<ProductPriceSKU> entityList = new List<ProductPriceSKU>();
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {

                var searchQuery = string.Concat(@"SELECT DISTINCT 
                                    ppls.ProductPriceListItemMapIID
                                    , ppls.ProductPriceListID
                                    , ppls.ProductSKUID
                                    , ppls.UnitGroundID
                                    , ppls.SellingQuantityLimit
                                    , ppls.Amount
                                    , ppls.PricePercentage
                                    , pls.ProductName
                                    , pls.SKU
                                    , pls.PartNo AS PartNumber
                                    , pls.Barcode
                                    FROM
                                    [catalog].[ProductListBySKU]  pls
                                    JOIN [catalog].[ProductPriceListSKUMaps] ppls ON pls.ProductSKUMapIID = ppls.ProductSKUID
                                    WHERE ppls.ProductPriceListID = " + id);

                entityList = db.Database.SqlQuery<ProductPriceSKU>(searchQuery).ToList();
            }
            return entityList;
        }

        public List<SalesRelationshipType> GetSalesRelationshipType(Eduegate.Framework.Enums.SalesRelationshipType salesRelationshipType)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                if (salesRelationshipType == Framework.Enums.SalesRelationshipType.All)
                {
                    return db.SalesRelationshipTypes.ToList();
                }
                else
                {
                    return db.SalesRelationshipTypes.Where(x => x.SalesRelationTypeID == (int)salesRelationshipType).ToList();
                }
            }
        }

        public Product GetProductDetailByProductId(long productID)
        {
            Product entity = new Product();
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                entity = db.Products.Where(x => x.ProductIID == productID)
                    .FirstOrDefault();
            }
            return entity;
        }

        #region ProductInventoryConfig

        public ProductInventoryConfig SaveProductInventoryConfig(ProductInventoryConfig entity)
        {
            ProductInventoryConfig entityDB = new ProductInventoryConfig();
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var test = db.ProductInventoryConfigs.Where(x => x.ProductInventoryConfigIID == entity.ProductInventoryConfigIID);

                entityDB = db.ProductInventoryConfigs.Where(x => x.ProductInventoryConfigIID == entity.ProductInventoryConfigIID).FirstOrDefault();
                if (entityDB.IsNotNull())
                {
                    // Update
                    entityDB.NotifyQuantity = entity.NotifyQuantity;
                    entityDB.MinimumQuantity = entity.MinimumQuantity;
                    entityDB.MaximumQuantity = entity.MaximumQuantity;
                    entityDB.MinimumQuanityInCart = entity.MinimumQuanityInCart;
                    entityDB.MaximumQuantityInCart = entity.MaximumQuantityInCart;
                    entityDB.ProductWarranty = entity.ProductWarranty;
                    entityDB.IsSerialNumber = entity.IsSerialNumber;
                    entityDB.IsSerialRequiredForPurchase = entity.IsSerialRequiredForPurchase;
                    entityDB.HSCode = entity.HSCode;
                    entityDB.DeliveryMethod = entity.DeliveryMethod;
                    entityDB.ProductWeight = entity.ProductWeight;
                    entityDB.ProductLength = entity.ProductLength;
                    entityDB.ProductWidth = entity.ProductWidth;
                    entityDB.ProductHeight = entity.ProductHeight;
                    entityDB.PackingTypeID = entity.PackingTypeID;
                    entityDB.DimensionalWeight = entity.DimensionalWeight;
                    entityDB.IsMarketPlace = entity.IsMarketPlace;
                    entityDB.Description = entity.Description;
                    entityDB.Details = entity.Details;
                    entityDB.IsNonRefundable = entity.IsNonRefundable;
                    entityDB.UpdatedDate = DateTime.Now;
                    entityDB.GroupingKey = entity.GroupingKey;
                    entityDB.IsSerailNumberAutoGenerated = entity.IsSerailNumberAutoGenerated;
                    entityDB.MaxQuantityInCartForVerifiedCustomer = entity.MaxQuantityInCartForVerifiedCustomer;
                    entityDB.MaxQuantityInCartForNonVerifiedCustomer = entity.MaxQuantityInCartForNonVerifiedCustomer;
                    entityDB.MaxQuantityDuration = entity.MaxQuantityDuration;
                    
                    foreach (var cultureData in entity.ProductInventoryConfigCultureDatas)
                    {                        
                        var configUpdated = entityDB.ProductInventoryConfigCultureDatas.Where(c => c.ProductInventoryConfigID == cultureData.ProductInventoryConfigID 
                            && c.CultureID == cultureData.CultureID).First();
                        configUpdated.Description = cultureData.Description;
                        configUpdated.Details = cultureData.Details;
                        db.Entry(configUpdated).State = System.Data.Entity.EntityState.Modified;
                    }                    
                    
                    db.SaveChanges();
                }
                else
                {
                    // Insert
                    entity.CreatedDate = DateTime.Now;
                    db.ProductInventoryConfigs.Add(entity);
                }
                return entity;

            }
        }
        public ProductInventoryProductConfigMap SaveProductInventoryProductConfigMap(ProductInventoryProductConfigMap entity)
        {
            ProductInventoryProductConfigMap entityDB = new ProductInventoryProductConfigMap();
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {

                entityDB = db.ProductInventoryProductConfigMaps.Where(x => x.ProductInventoryConfigID == entity.ProductInventoryConfigID
                && x.ProductID == entity.ProductID).FirstOrDefault();
                if (entityDB.IsNotNull())
                {
                    // Update
                    entityDB.ProductID = entity.ProductID;
                    entityDB.ProductInventoryConfigID = entity.ProductInventoryConfigID;
                    entityDB.UpdatedDate = DateTime.Now;
                }
                else
                {
                    // Insert
                    entity.CreatedDate = DateTime.Now;
                    db.ProductInventoryProductConfigMaps.Add(entity);
                }

                db.SaveChanges();
                return entity;
            }
        }



        public ProductInventorySKUConfigMap SaveProductInventorySKUConfigMap(ProductInventorySKUConfigMap entity)
        {
            ProductInventorySKUConfigMap entityDB = new ProductInventorySKUConfigMap();
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {

                entityDB = db.ProductInventorySKUConfigMaps.Where(x => x.ProductInventoryConfigID == entity.ProductInventoryConfigID
                && x.ProductSKUMapID == entity.ProductSKUMapID).FirstOrDefault();
                if (entityDB.IsNotNull())
                {
                    // Update
                    entityDB.ProductSKUMapID = entity.ProductSKUMapID;
                    entityDB.ProductInventoryConfigID = entity.ProductInventoryConfigID;
                    entityDB.UpdatedDate = DateTime.Now;

                }
                else
                {
                    // Insert
                    entity.CreatedDate = DateTime.Now;
                    db.ProductInventorySKUConfigMaps.Add(entity);
                }

                db.SaveChanges();
                return entity;
            }
        }


        #endregion

        private static void UpdateProductBundles(Product productDetails, Product product, dbEduegateERPContext dbContext, ICollection<ProductSKUMap> ProductSKUMaps)
        {
            //Remove existing bundles
            foreach (ProductBundle existingBundle in dbContext.ProductBundles.Where(x => x.FromProductID == product.ProductIID))
            {
                dbContext.ProductBundles.Remove(existingBundle);
            }

            //Add new bundles
            if (productDetails.ProductBundles != null)
            {
                if (productDetails.ProductBundles.Count > 0)
                {
                    productDetails.ProductBundles = product.ProductBundles;
                    foreach (ProductBundle bundle in productDetails.ProductBundles)
                    {
                        product.ProductBundles.Add(bundle);
                        dbContext.SaveChanges();
                    }
                }
            }
        }

        public List<Property> CreateProperties(List<Property> entityList)
        {
            List<Property> dbEntityList = new List<Property>();
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    if (entityList.IsNotNull() && entityList.Count > 0)
                    {
                        foreach (Property entity in entityList)
                        {
                            dbContext.Properties.Add(entity);
                        }

                        dbContext.SaveChanges();
                        dbEntityList = dbContext.Properties.Where(x => x.PropertyTypeID == (byte)PropertyTypes.ProductTag).ToList();
                    }
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetailRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }
            return dbEntityList;
        }

        public ProductHierarchy GetProductTree(long? categoryID)
        {
            var productCat = new ProductHierarchy();

            using (var db = new dbEduegateERPContext())
            {
                foreach (var category in db.Categories
                    .Select(a => new { a.CategoryIID, a.CategoryName, a.CategoryCode, a.ParentCategoryID })
                    .Where(a => a.ParentCategoryID == categoryID))
                {
                    var categoryNode = new CategoryTree() { CategoryID = category.CategoryIID, CategoryName = category.CategoryName, CategoryCode = category.CategoryCode };
                    productCat.CategoryTree.Add(categoryNode);
                }
            }

            return productCat;
        }

        private void BuildProductTree(IEnumerable<dynamic> categories, CategoryTree hierarchy, dbEduegateERPContext db)
        {
            foreach (var category in categories)
            {
                long categoryID = category.CategoryIID;
                var categoryHeirarcy = new CategoryTree() { CategoryID = category.CategoryIID, CategoryName = category.CategoryName, CategoryCode = category.CategoryCode };

                BuildProductTree(db.Categories.Select(a => new { a.CategoryIID, a.CategoryName, a.CategoryCode, a.ParentCategoryID })
                    .Where(x => x.ParentCategoryID == categoryID).ToList(), categoryHeirarcy, db);

                hierarchy.Categories.Add(categoryHeirarcy);

                //foreach (var productMap in db.ProductCategoryMaps.Select(a=> new { a.ProductID, a.Product.ProductCode,  a.Product.ProductName, a.CategoryID })
                //    .Where(a=> a.CategoryID == categoryID).ToList())
                //{
                //    if (productMap.ProductID.HasValue)
                //    {
                //        hierarchy.Products.Add(new ProductTree()
                //        {
                //            ProductCode = productMap.ProductCode,
                //            ProductID = productMap.ProductID.Value,
                //            ProductName = productMap.ProductName
                //        });
                //    }
                //}
            }
        }

        public List<SearchResult> GetProductWidgets()
        {
            dbEduegateERPContext db = new dbEduegateERPContext();
            List<SearchResult> lists;
            try
            {
                lists = (from p in db.Products
                         join b in db.Brands on p.BrandID equals b.BrandIID
                         join psm in db.ProductSKUMaps on p.ProductIID equals psm.ProductID

                         join pim in db.ProductImageMaps on psm.ProductSKUMapIID equals pim.ProductSKUMapID into psmLJ
                         from pim in psmLJ.DefaultIfEmpty()

                         join pplsm in db.ProductPriceListSKUMaps on psm.ProductSKUMapIID equals pplsm.ProductSKUID into one
                         from pplsm in one.DefaultIfEmpty()

                         join ppl in db.ProductPriceLists on pplsm.ProductPriceListID equals ppl.ProductPriceListIID into two
                         from ppl in two.DefaultIfEmpty()

                         join pi in db.ProductInventories on psm.ProductSKUMapIID equals pi.ProductSKUMapID into productStock
                         from pi in productStock.DefaultIfEmpty()

                         join pp in db.ProductToProductMaps on p.ProductIID equals pp.ProductIDTo
                         where
                         (pp.SalesRelationTypeID == (int)Eduegate.Framework.Enums.SalesRelationshipType.UpSell || pp.SalesRelationTypeID == (int)Eduegate.Framework.Enums.SalesRelationshipType.CrossSell)
                        && (pim.Sequence == null || pim.Sequence == 1) && (pim.ProductImageTypeID == null || pim.ProductImageTypeID == 39)

                         select
                         new SearchResult()
                         {
                             ProductIID = p.ProductIID,
                             ProductCode = p.ProductCode,
                             ProductName = p.ProductName,
                             BrandIID = b.BrandIID,
                             BrandName = b.BrandName,
                             ProductPrice = psm.ProductPrice,
                             ImageFile = pim.ImageFile,
                             ProductSKUCode = psm.ProductSKUCode,
                             SKUID = psm.ProductSKUMapIID,
                             DiscountedPrice = ((ppl.Price != null && ((ppl.StartDate != null && ppl.EndDate == null && DateTime.Now >= ppl.StartDate) ||
                                                 (DateTime.Now >= ppl.StartDate && DateTime.Now <= ppl.EndDate) || (ppl.StartDate == null && ppl.EndDate == null))) ?
                                                 ppl.Price :
                                                 (ppl.PricePercentage != null && ((ppl.StartDate != null && ppl.EndDate == null && DateTime.Now >= ppl.StartDate) ||
                                                 (DateTime.Now >= ppl.StartDate && DateTime.Now <= ppl.EndDate) || (ppl.StartDate == null && ppl.EndDate == null))) ?
                                                 ((decimal?)((decimal?)psm.ProductPrice * (100 - (decimal?)ppl.PricePercentage)) / 100) :
                                                 psm.ProductPrice),
                             HasStock = (pi.Quantity != null && pi.Quantity > 0) ? true : false,
                         }).Distinct().ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return lists;
        }

        public ProductSKUMap GetProductSKUMap(long skuID)
        {
            using (var db = new dbEduegateERPContext())
            {
                return db.ProductSKUMaps.Where(a => a.ProductSKUMapIID == skuID).FirstOrDefault();

                //var entity = db.ProductSKUMaps.Where(a => a.ProductSKUMapIID == skuID)
                //           .Include("ProductInventorySKUConfigMaps")
                //           .Include("ProductInventorySKUConfigMaps.ProductInventoryConfig")
                //           .FirstOrDefault();
                //return entity;

            }
        }

        public List<ProductSKUMap> GetProductSKUByCategory(long category, int takeCount, string tag)
        {
            using (var db = new dbEduegateERPContext())
            {
                return (from sku in db.ProductSKUMaps
                        join cat in db.ProductCategoryMaps on sku.ProductID equals cat.ProductID
                        join skuTagMap in db.ProductSKUTagMaps on sku.ProductSKUMapIID equals skuTagMap.ProductSKuMapID
                        join skuTag in db.ProductSKUTags on skuTagMap.ProductSKUTagID equals skuTag.ProductSKUTagIID
                        where cat.CategoryID == category && sku.StatusID == 2 //active from ProductStatus
                        && skuTag.TagName.Equals(tag, StringComparison.InvariantCulture)
                        select sku).Take(takeCount).ToList();
            }
        }

        public List<ProductSKUMap> GetProductSKUByTag(string tag = null, int takeCount = 20, string languageCode = null)
        {
            using (var db = new dbEduegateERPContext())
            {
                if (tag == null)
                {
                    return (from sku in db.ProductSKUMaps                               
                            join skuConfig in db.ProductInventorySKUConfigMaps on sku.ProductSKUMapIID equals skuConfig.ProductSKUMapID into invConfig
                            from invConfigLj in invConfig.DefaultIfEmpty()
                            join config in db.ProductInventoryConfigs on invConfigLj.ProductInventoryConfigID equals config.ProductInventoryConfigIID into invProductConfig
                            from invLj in invProductConfig.DefaultIfEmpty()
                            where sku.StatusID == null || sku.StatusID == 2 //active from ProductStatus                           
                            select sku).Take(takeCount).ToList();
                }
                else
                {
                    var skus = (from sku in db.ProductSKUMaps                                
                            join skuTagMap in db.ProductSKUTagMaps on sku.ProductSKUMapIID equals skuTagMap.ProductSKuMapID into tagMap
                            from tagMapLJ in tagMap.DefaultIfEmpty()
                            join skuTag in db.ProductSKUTags on tagMapLJ.ProductSKUTagID equals skuTag.ProductSKUTagIID into skuTag
                            from skuTagLJ in skuTag.DefaultIfEmpty()
                            join skuConfig in db.ProductInventorySKUConfigMaps on sku.ProductSKUMapIID equals skuConfig.ProductSKUMapID into skuConfig
                            from skuConfigLJ in skuConfig.DefaultIfEmpty()
                            join config in db.ProductInventoryConfigs on skuConfigLJ.ProductInventoryConfigID equals config.ProductInventoryConfigIID into invConfig
                            from invConfigLJ in invConfig.DefaultIfEmpty()
                            //join skuCulture in db.ProductSKUCultureDatas on sku.ProductSKUMapIID equals skuCulture.ProductSKUMapID into skuCulture
                            //from skuCultureLJ in skuCulture.DefaultIfEmpty()
                            where sku.StatusID == 2 //active from ProductStatus
                            && skuTagLJ.TagName.Equals(tag, StringComparison.InvariantCulture)
                            select sku)
                            .Include(a=> a.ProductSKUCultureDatas)
                            .Take(takeCount)
                            .ToList();

                    if (languageCode != null)
                    {
                        foreach (var sku in skus)
                        {
                            var cultureData = sku.ProductSKUCultureDatas.Where(a => a.Culture.CultureCode == languageCode).FirstOrDefault();

                            if(cultureData != null)
                            {
                                sku.SKUName = string.IsNullOrEmpty(cultureData.ProductSKUName) ? sku.SKUName : cultureData.ProductSKUName;
                            }
                        }
                    }

                    return skus;
                }
            }
        }

        public ProductInventoryConfig GetProductInventorySKUConfig(long skuID)
        {
            using (var db = new dbEduegateERPContext())
            {

                var productInventoryConfig = (from picsm in db.ProductInventorySKUConfigMaps
                                              join pic in db.ProductInventoryConfigs on picsm.ProductInventoryConfigID equals pic.ProductInventoryConfigIID
                                              where picsm.ProductSKUMapID == skuID
                                              select pic).FirstOrDefault();

                db.Entry(productInventoryConfig).Collection(a => a.ProductInventorySKUConfigMaps).Load();
                return productInventoryConfig;

            }
        }

        public ProductDeliveryCountrySetting RemoveProductDeliveryCountrySetting(ProductDeliveryCountrySetting productDeliveryCountrySetting)
        {
            if (productDeliveryCountrySetting.IsNotNull())
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    ProductDeliveryCountrySetting dbEntity = dbContext.ProductDeliveryCountrySettings.Where(x => x.ProductDeliveryCountrySettingsIID == productDeliveryCountrySetting.ProductDeliveryCountrySettingsIID).FirstOrDefault();
                    dbContext.ProductDeliveryCountrySettings.Remove(dbEntity);
                    dbContext.SaveChanges();
                }
            }

            return productDeliveryCountrySetting;
        }

        public ProductDeliveryCountrySetting SaveProductDeliveryCountrySetting(ProductDeliveryCountrySetting productDeliveryCountrySetting)
        {
            if (productDeliveryCountrySetting.IsNotNull())
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    ProductDeliveryCountrySetting dbEntity = dbContext.ProductDeliveryCountrySettings.Where(x => x.ProductDeliveryCountrySettingsIID == productDeliveryCountrySetting.ProductDeliveryCountrySettingsIID).FirstOrDefault();

                    if (dbEntity.IsNotNull())
                    {
                        //Update
                        dbEntity.ProductDeliveryCountrySettingsIID = productDeliveryCountrySetting.ProductDeliveryCountrySettingsIID;
                        dbEntity.ProductID = productDeliveryCountrySetting.ProductID;
                        dbEntity.ProductSKUMapID = productDeliveryCountrySetting.ProductSKUMapID;
                        dbEntity.UpdateDate = DateTime.Now;
                    }
                    else
                    {
                        productDeliveryCountrySetting.CreatedDate = DateTime.Now;
                        dbContext.ProductDeliveryCountrySettings.Add(productDeliveryCountrySetting);
                    }

                    dbContext.SaveChanges();
                }
            }

            return productDeliveryCountrySetting;
        }

        public PropertyType SavePropertyType(PropertyType entity)
        {
            PropertyType updatedEntity = null;
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (entity.PropertyTypeID == 0)
                {
                    var maxIdValue = dbContext.PropertyTypes.Max(x => (byte?)x.PropertyTypeID);
                    // increment max value by 1
                    entity.PropertyTypeID = Convert.ToByte(maxIdValue + 1);
                }
                if (dbContext.PropertyTypes.Any(x => x.PropertyTypeID == entity.PropertyTypeID))
                {
                    var propertyTypePropertyMaps = dbContext.PropertyTypePropertyMaps.Where(p => p.PropertyTypeID == entity.PropertyTypeID);
                    dbContext.PropertyTypePropertyMaps.RemoveRange(propertyTypePropertyMaps);
                    var dbEntity = dbContext.PropertyTypes.Where(p => p.PropertyTypeID == entity.PropertyTypeID).Single();
                    dbEntity.PropertyTypeName = entity.PropertyTypeName;
                }
                else
                {
                    dbContext.Entry(entity).State = System.Data.Entity.EntityState.Added;
                }
                foreach (PropertyTypePropertyMap ptpm in entity.PropertyTypePropertyMaps)
                    dbContext.Entry(ptpm).State = System.Data.Entity.EntityState.Added;


                dbContext.SaveChanges();
                updatedEntity = dbContext.PropertyTypes.Where(x => x.PropertyTypeID == entity.PropertyTypeID)
                                .Include("PropertyTypePropertyMaps")
                                .FirstOrDefault();
                updatedEntity.Properties = (from prop in dbContext.Properties
                                            join ptpm in dbContext.PropertyTypePropertyMaps on prop.PropertyIID equals ptpm.PropertyID
                                            where ptpm.PropertyTypeID.Equals(updatedEntity.PropertyTypeID)
                                            select prop).ToList();
                return updatedEntity;
            }
        }

        public PropertyType GetPropertyType(byte propertyTypeID)
        {
            PropertyType entity = null;

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                entity = dbContext.PropertyTypes.Where(x => x.PropertyTypeID == propertyTypeID).FirstOrDefault();
                dbContext.Entry(entity).Collection(a => a.PropertyTypePropertyMaps).Load();
                return entity;
            }
        }

        public List<Property> GetPropertiesByPropertyTypeID(byte PropertyTypeID)
        {
            var Properties = new List<Property>();

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                Properties = (from ptpm in dbContext.PropertyTypePropertyMaps
                              join pt in dbContext.Properties on ptpm.PropertyID equals pt.PropertyIID
                              where ptpm.PropertyTypeID == PropertyTypeID
                              select pt).ToList();
            }
            return Properties;
        }

        public decimal GetProductInventoryByBranch(long skuID, long BranchID)
        {
            using (var db = new dbEduegateERPContext())
            {
                var quantity = db.ProductInventories.Where(a => a.ProductSKUMapID == skuID && a.BranchID == BranchID).Sum(a => a.Quantity);
                if (quantity != null)
                { return (decimal)quantity; }
                else { return 0; }
            }
        }

        public List<ProductInventory> GetProductInventory(long skuID)
        {
            using (var db = new dbEduegateERPContext())
            {
                var inventories = db.ProductInventories.Where(a => a.ProductSKUMapID == skuID);

                foreach (var inventory in inventories)
                {
                    db.Entry(inventory).Reference(a => a.Branch).Load();
                }

                return inventories.ToList();
            }
        }

        public List<ProductImageMap> GetProductImages(long skuID)
        {
            using (var db = new dbEduegateERPContext())
            {
                var productImages = db.ProductImageMaps.Where(a => a.ProductSKUMapID == skuID && a.Sequence == 1 && (a.ProductImageTypeID == 1 || a.ProductImageTypeID == 8)).ToList();
                return productImages;
            }
        }

        public ProductImageMap GetProductImage(long skuID)
        {
            using (var db = new dbEduegateERPContext())
            {
                var productImages = db.ProductImageMaps.Where(a => a.ProductSKUMapID == skuID && a.Sequence == 1 && (a.ProductImageTypeID == 1)).FirstOrDefault();
                return productImages;
            }
        }

        public List<ProductImageMap> GetAllProductImages(long skuID)
        {
            using (var db = new dbEduegateERPContext())
            {
                var productImages = db.ProductImageMaps.Where(a => a.ProductSKUMapID == skuID && (a.ProductImageTypeID == 1 || a.ProductImageTypeID == 8)).OrderBy(x => x.Sequence).ToList();
                return productImages;
            }
        }

        #region Product SerialMap Methods

        public List<ProductSerialMap> GetProductSerialMaps(long transactionDetailID, long productSKUMapID, int recordLimit)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.ProductSerialMaps.Where(p => p.ProductSKUMapID == productSKUMapID && p.DetailID == transactionDetailID).Take(recordLimit).ToList();
            }
        }

        // Overload for serialnumber
        public List<ProductSerialMap> GetProductSerialMaps(string serialNumber, long productSerialIID = 0)
        {
            var serials = new List<ProductSerialMap>();
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                if (productSerialIID > 0)
                    serials = db.ProductSerialMaps.Where(p => p.SerialNo == serialNumber && p.ProductSerialIID == productSerialIID).ToList();
                else
                    serials = db.ProductSerialMaps.Where(p => p.SerialNo == serialNumber).ToList();

                return serials;
            }
        }

        public bool UpdateProductInventorySerialMaps(List<ProductInventorySerialMap> entities, bool isUpdate = false)
        {
            bool exit = false;
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (isUpdate)
                {
                    entities.ForEach(p => dbContext.Entry(p).State = System.Data.Entity.EntityState.Modified);
                }
                else
                {
                    dbContext.ProductInventorySerialMaps.AddRange(entities);
                    //dbContext.Entry(entities).State = EntityState.Added;
                }

                dbContext.SaveChanges();
                exit = true;
            }
            return exit;
        }
        #endregion

        public List<ProductInventoryBranchDTO> GetProductSKUIDBranchID(long productSKUMapID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var query = from p in db.ProductInventories
                            join sku in db.ProductSKUMaps on p.ProductSKUMapID equals sku.ProductSKUMapIID
                            where sku.ProductSKUMapIID == productSKUMapID
                            group new { p, sku }
                             by new { p.BranchID } into g
                            select new ProductInventoryBranchDTO
                            {
                                BranchID = (long)g.Key.BranchID,
                                IsMarketPlace = g.FirstOrDefault().p.IsMarketPlaceBranch != null ? (bool)g.FirstOrDefault().p.IsMarketPlaceBranch : false,
                                Quantity = (decimal)g.Sum(x => x.p.Quantity)
                            };

                return query.ToList();
            }
        }

        public DataTable GetInventoryDetails(string ProductSKUMapIDString, long? CustomerID = null, string CartID = "", CallContext callContext = null)
        {
            DataTable CartItemList = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationExtensions.GetConnectionString("dbEduegateERPContext").ToString()))
                using (SqlCommand cmd = new SqlCommand("[inventory].[spcGetProductInventoryMultipleSkus]", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@ProductSKUMapIDString", SqlDbType.VarChar, 2000));
                    adapter.SelectCommand.Parameters["@ProductSKUMapIDString"].Value = ProductSKUMapIDString;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.BigInt));
                    adapter.SelectCommand.Parameters["@CustomerID"].Value = CustomerID.HasValue ? CustomerID.Value : 0;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@CartID", SqlDbType.NVarChar, 200));
                    adapter.SelectCommand.Parameters["@CartID"].Value = CartID;

                    if (callContext.IsNotNull() && callContext.CompanyID.IsNotNull() && callContext.CurrencyCode.IsNotNull())
                    {
                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@CompanyID", SqlDbType.Int));
                        adapter.SelectCommand.Parameters["@CompanyID"].Value = callContext.CompanyID;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@CurrencyCode", SqlDbType.NVarChar));
                        adapter.SelectCommand.Parameters["@CurrencyCode"].Value = callContext.CurrencyCode;
                    }

                    DataSet dt = new DataSet();
                    adapter.Fill(dt);
                    CartItemList = dt.Tables[0];
                }

            }
            catch (Exception ex)
            {
                CartItemList = null;
            }

            return CartItemList;
        }

        public ProductInventoryBranchDTO GetInventoryDetailsSKUID(long ProductSKUMapID, long CustomerID = 0, string CartID = "", bool type = true, Int32 CompanyID = 2)
        {
            var ProductInventoryBranch = new ProductInventoryBranchDTO();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationExtensions.GetConnectionString("dbEduegateERPContext").ToString()))
                using (SqlCommand cmd = new SqlCommand("[inventory].[spcProductInventoryDetails]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ProductSKUMapID", SqlDbType.BigInt, 8).Value = ProductSKUMapID;
                    cmd.Parameters.Add("@CustomerID", SqlDbType.BigInt, 8).Value = CustomerID;
                    cmd.Parameters.Add("@Type", SqlDbType.Bit).Value = type;
                    cmd.Parameters.Add("@CartID", SqlDbType.NVarChar, 200).Value = CartID;
                    cmd.Parameters.Add("@CompanyID", SqlDbType.Int, 4).Value = CompanyID;

                    cmd.Parameters.Add("@InventoryBranchID", SqlDbType.BigInt).Value = 0;
                    cmd.Parameters["@InventoryBranchID"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@QuantityBranch", SqlDbType.BigInt).Value = 0;
                    cmd.Parameters["@QuantityBranch"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("@CostPrice", SqlDbType.Decimal).Value = 0;
                    cmd.Parameters["@CostPrice"].Direction = ParameterDirection.Output;
                    cmd.Parameters["@CostPrice"].Precision = 18;
                    cmd.Parameters["@CostPrice"].Scale = 3;

                    cmd.Parameters.Add("@ProductPrice", SqlDbType.Decimal).Value = 0;
                    cmd.Parameters["@ProductPrice"].Direction = ParameterDirection.Output;
                    cmd.Parameters["@ProductPrice"].Precision = 18;
                    cmd.Parameters["@ProductPrice"].Scale = 3;

                    cmd.Parameters.Add("@ProductDiscountPrice", SqlDbType.Decimal).Value = 0;
                    cmd.Parameters["@ProductDiscountPrice"].Direction = ParameterDirection.Output;
                    cmd.Parameters["@ProductDiscountPrice"].Precision = 18;
                    cmd.Parameters["@ProductDiscountPrice"].Scale = 3;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();

                    ProductInventoryBranch.ProductSKUMapID = ProductSKUMapID;
                    ProductInventoryBranch.BranchID = Convert.ToInt64(cmd.Parameters["@InventoryBranchID"].Value);
                    ProductInventoryBranch.Quantity = Convert.ToInt64(cmd.Parameters["@QuantityBranch"].Value);
                    ProductInventoryBranch.ProductCostPrice = Convert.ToDecimal(cmd.Parameters["@CostPrice"].Value);
                    ProductInventoryBranch.ProductPricePrice = Convert.ToDecimal(cmd.Parameters["@ProductPrice"].Value);
                    ProductInventoryBranch.ProductDiscountPrice = Convert.ToDecimal(cmd.Parameters["@ProductDiscountPrice"].Value);
                    cmd.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                return ProductInventoryBranch;
            }
            return ProductInventoryBranch;
        }

        public ProductInventoryBranchDTO GetPriceDetailsSKUID(long InventoryBranch, long ProductSKUMapID, long CustomerID = 0, string CartID = "")
        {
            var ProductInventoryBranch = new ProductInventoryBranchDTO();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationExtensions.GetConnectionString("dbEduegateERPContext").ToString()))
                using (SqlCommand cmd = new SqlCommand("[catalog].[spcProductPriceDetails]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@InventoryBranch", SqlDbType.BigInt).Value = InventoryBranch;
                    cmd.Parameters.Add("@ProductSKUMapID", SqlDbType.BigInt).Value = ProductSKUMapID;
                    cmd.Parameters.Add("@CustomerID", SqlDbType.BigInt).Value = CustomerID;
                    cmd.Parameters.Add("@CartID", SqlDbType.NVarChar, 200).Value = CartID;

                    cmd.Parameters.Add("@PLCostPrice", SqlDbType.Decimal).Value = 0;
                    cmd.Parameters["@PLCostPrice"].Direction = ParameterDirection.Output;
                    cmd.Parameters["@PLCostPrice"].Precision = 18;
                    cmd.Parameters["@PLCostPrice"].Scale = 3;

                    cmd.Parameters.Add("@PLProductPrice", SqlDbType.Decimal).Value = 0;
                    cmd.Parameters["@PLProductPrice"].Direction = ParameterDirection.Output;
                    cmd.Parameters["@PLProductPrice"].Precision = 18;
                    cmd.Parameters["@PLProductPrice"].Scale = 3;

                    cmd.Parameters.Add("@PLProductDiscountPrice", SqlDbType.Decimal).Value = 0;
                    cmd.Parameters["@PLProductDiscountPrice"].Direction = ParameterDirection.Output;
                    cmd.Parameters["@PLProductDiscountPrice"].Precision = 18;
                    cmd.Parameters["@PLProductDiscountPrice"].Scale = 3;

                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();

                    ProductInventoryBranch.ProductSKUMapID = ProductSKUMapID;
                    ProductInventoryBranch.BranchID = InventoryBranch;
                    ProductInventoryBranch.ProductCostPrice = Convert.ToDecimal(cmd.Parameters["@PLCostPrice"].Value);
                    ProductInventoryBranch.ProductPricePrice = Convert.ToDecimal(cmd.Parameters["@PLProductPrice"].Value);
                    ProductInventoryBranch.ProductDiscountPrice = Convert.ToDecimal(cmd.Parameters["@PLProductDiscountPrice"].Value);
                    cmd.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                return ProductInventoryBranch;
            }
            return ProductInventoryBranch;
        }

        public List<ProductMultiPrice> GetProductMultiPriceDetails(long InventoryBranch, long ProductSKUMapID, long CustomerID = 0)
        {

            var productMultiPriceList = new List<ProductMultiPrice>();

            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                if (!isProductinDeal(InventoryBranch, ProductSKUMapID))
                {
                    var priceListID = db.ProductPriceListBranchMaps.Where(a => a.BranchID == InventoryBranch).Select(a => a.ProductPriceListID).FirstOrDefault();
                    if (priceListID.IsNotDefault() && priceListID.IsNotNull())
                    {
                        decimal? productDiscountPrice = 0;
                        var skuDiscountPriceList = db.ProductPriceListSKUMaps.Where(a => a.ProductSKUID == ProductSKUMapID && a.ProductPriceListID == priceListID).Select(a => new { a.Discount, a.Price }).FirstOrDefault();
                        if (skuDiscountPriceList != null)
                        {
                            productDiscountPrice = skuDiscountPriceList.Discount.HasValue ? skuDiscountPriceList.Discount : skuDiscountPriceList.Price;
                        }
                        if (productDiscountPrice.IsNotDefault() && productDiscountPrice.IsNotNull())
                        {
                            if (db.ProductPriceListCustomerGroupMaps.Where(a => a.ProductSKUMapID == ProductSKUMapID && a.ProductPriceListID == priceListID && a.DiscountPercentage != null && a.DiscountPercentage > 0).Any())
                            {
                                var groupID = db.Customers.Where(a => a.CustomerIID == CustomerID).Select(a => a.GroupID).FirstOrDefault();

                                var list = from a in db.ProductPriceListCustomerGroupMaps
                                           join b in db.CustomerGroups on a.CustomerGroupID equals b.CustomerGroupIID
                                           where a.ProductSKUMapID == ProductSKUMapID && a.DiscountPercentage != null && a.DiscountPercentage > 0 && a.ProductPriceListID == priceListID
                                           orderby b.PointLimit descending
                                           select new
                                           {
                                               GroupID = b.CustomerGroupIID,
                                               GroupName = b.GroupName,
                                               //MultiPrice = productDiscountPrice - (a.DiscountPercentage * productDiscountPrice / 100),
                                               MultiPrice = Math.Round((decimal)((productDiscountPrice - (a.DiscountPercentage * productDiscountPrice / 100))) / 5, 3) * 5,
                                               isSelected = groupID == b.CustomerGroupIID ? true : false
                                           };

                                foreach (var row in list)
                                {
                                    var productMultiPrice = new ProductMultiPrice();
                                    productMultiPrice.GroupID = row.GroupID.ToString();
                                    productMultiPrice.GroupName = row.GroupName;
                                    productMultiPrice.MultipriceValue = row.MultiPrice.ToString();
                                    productMultiPrice.isSelected = row.isSelected;

                                    productMultiPriceList.Add(productMultiPrice);
                                }
                                if (groupID.IsNotDefault() && groupID.IsNotNull())
                                {
                                    productDiscountPrice = db.ProductPriceListCustomerGroupMaps.Where(a => a.ProductSKUMapID == ProductSKUMapID && a.ProductPriceListID == priceListID && a.CustomerGroupID == groupID).Select(a => productDiscountPrice - (a.DiscountPercentage * productDiscountPrice / 100)).FirstOrDefault();
                                }

                            }
                        }
                    }
                }
            }
            return productMultiPriceList;
        }
        private decimal ReturnPrice(decimal productDiscountPrice, decimal DiscountPercentage)
        {
            return productDiscountPrice - (DiscountPercentage * productDiscountPrice / 100);
        }
        public List<Eduegate.Domain.Entity.Models.ValueObjects.ProductQuantityDiscount> GetProductQtyDiscountDetails(long InventoryBranch, long ProductSKUMapID, long CustomerID = 0)
        {

            var productQtyDiscountList = new List<Eduegate.Domain.Entity.Models.ValueObjects.ProductQuantityDiscount>();
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                if (!isProductinDeal(InventoryBranch, ProductSKUMapID))
                {
                    var priceListID = db.ProductPriceListBranchMaps.Where(a => a.BranchID == InventoryBranch).Select(a => a.ProductPriceListID).FirstOrDefault();
                    if (priceListID.IsNotDefault() && priceListID.IsNotNull())
                    {
                        decimal? productDiscountPrice = 0;
                        var skuDiscountPriceList = db.ProductPriceListSKUMaps.Where(a => a.ProductSKUID == ProductSKUMapID && a.ProductPriceListID == priceListID).Select(a => new { a.Discount, a.Price }).FirstOrDefault();
                        if (skuDiscountPriceList != null)
                        {
                            productDiscountPrice = skuDiscountPriceList.Discount.HasValue ? skuDiscountPriceList.Discount : skuDiscountPriceList.Price;
                        }
                        if (productDiscountPrice.IsNotDefault() && productDiscountPrice.IsNotNull())
                        {
                            if (db.ProductPriceListCustomerGroupMaps.Where(a => a.ProductSKUMapID == ProductSKUMapID && a.ProductPriceListID == priceListID).Any())
                            {
                                var groupID = db.Customers.Where(a => a.CustomerIID == CustomerID).Select(a => a.GroupID).FirstOrDefault();
                                if (groupID.IsNotDefault() && groupID.IsNotNull())
                                {
                                    var customerGrpproductDiscountPrice = db.ProductPriceListCustomerGroupMaps.Where(a => a.ProductSKUMapID == ProductSKUMapID && a.ProductPriceListID == priceListID && a.CustomerGroupID == groupID).Select(a => productDiscountPrice - (a.DiscountPercentage * productDiscountPrice / 100)).FirstOrDefault();
                                    productDiscountPrice = customerGrpproductDiscountPrice.HasValue && customerGrpproductDiscountPrice.Value > 0 ? customerGrpproductDiscountPrice.Value : productDiscountPrice;
                                }


                                var qtyList = from a in db.ProductPriceListSKUQuantityMaps
                                              join b in db.ProductPriceListSKUMaps on a.ProductPriceListSKUMapID equals b.ProductPriceListItemMapIID
                                              where b.ProductSKUID == ProductSKUMapID && b.ProductPriceListID == priceListID
                                              orderby a.Quantity
                                              select new
                                              {
                                                  DiscountPercentage = Math.Round((decimal)((productDiscountPrice - (productDiscountPrice - a.DiscountPrice)) / productDiscountPrice) * 100, 0),
                                                  //DiscountPercentage = Math.Round((decimal)(((productDiscountPrice - (productDiscountPrice - a.DiscountPrice)) / productDiscountPrice) * 100) / 5, 3) * 5,
                                                  Quantity = a.Quantity,
                                                  //QtyPrice= productDiscountPrice - (a.DiscountPrice),
                                                  QtyPrice = Math.Round((decimal)((productDiscountPrice - (a.DiscountPrice))) / 5, 3) * 5,
                                              };


                                foreach (var row in qtyList)
                                {
                                    var productQuantityDiscount = new Eduegate.Domain.Entity.Models.ValueObjects.ProductQuantityDiscount();

                                    productQuantityDiscount.DiscountPercentage = row.DiscountPercentage.ToString();
                                    productQuantityDiscount.Quantity = row.Quantity.ToString();
                                    productQuantityDiscount.QtyPrice = row.QtyPrice.ToString();
                                    productQtyDiscountList.Add(productQuantityDiscount);
                                }

                            }
                        }
                    }
                }
            }
            return productQtyDiscountList;
        }

        private bool isProductinDeal(long inventoryBranch, long ProductSkuMapID)
        {
            bool isDeal = false;
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                if ((from a in db.ProductPriceListSKUMaps
                     join b in db.ProductPriceLists on a.ProductPriceListID equals b.ProductPriceListIID
                     join c in db.ProductPriceListBranchMaps on b.ProductPriceListIID equals c.ProductPriceListID
                     where c.BranchID == inventoryBranch && a.ProductSKUID == ProductSkuMapID && b.ProductPriceListTypeID == 3 && b.StartDate <= DateTime.Now && b.EndDate > DateTime.Now
                     select a).Any())
                {
                    isDeal = true;
                }
            }
            return isDeal;
        }

        public void AddProductToSupplierPriceList(long productID, CallContext context)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {

                var priceListId = (from s in dbContext.Suppliers
                                   join p in dbContext.ProductPriceListBranchMaps on s.BranchID equals p.BranchID
                                   join ppl in dbContext.ProductPriceLists on p.ProductPriceListID equals ppl.ProductPriceListIID
                                   where s.LoginID == context.LoginID
                                   //2	Base	Price details needed at product level
                                   && ppl.ProductPriceListTypeID == 2
                                   orderby ppl.ProductPriceListIID
                                   select p.ProductPriceListID).FirstOrDefault();

                if (priceListId.IsNull())
                {
                    return;
                }

                if (!dbContext.ProductPriceListProductMaps.Any(a => a.ProductPriceListID == priceListId && a.ProductID == productID))
                {
                    dbContext.ProductPriceListProductMaps.Add(new ProductPriceListProductMap()
                    {
                        ProductID = productID,
                        ProductPriceListID = priceListId,
                        UpdatedBy = int.Parse(context.LoginID.Value.ToString()),
                        UpdatedDate = DateTime.Now,
                        CreatedBy = int.Parse(context.LoginID.Value.ToString()),
                        CreatedDate = DateTime.Now,
                        CompanyID = context.IsNotNull() ? (int)context.CompanyID : 0,
                    });
                }

                foreach (var sku in dbContext.ProductSKUMaps.Where(a => a.ProductID == productID))
                {
                    if (!dbContext.ProductPriceListSKUMaps.Any(a => a.ProductPriceListID == priceListId && a.ProductSKUID == sku.ProductSKUMapIID))
                    {
                        dbContext.ProductPriceListSKUMaps.Add(new ProductPriceListSKUMap()
                        {
                            ProductSKUID = sku.ProductSKUMapIID,
                            ProductPriceListID = priceListId,
                            UpdatedBy = int.Parse(context.LoginID.Value.ToString()),
                            UpdatedDate = DateTime.Now,
                            CreatedBy = int.Parse(context.LoginID.Value.ToString()),
                            CreatedDate = DateTime.Now,
                            CompanyID = context.IsNotNull() ? context.CompanyID : null,
                        });
                    }
                }

                dbContext.SaveChanges();
            }
        }


        public List<Eduegate.Domain.Entity.Models.ValueObjects.DeliveryTypeCheck> GetProductDeliveryDetails(string ProductSKUMapIDString)
        {
            DataTable CartItemList = null;
            var deliveryTypeCheckList = new List<Eduegate.Domain.Entity.Models.ValueObjects.DeliveryTypeCheck>();
            try
            {

                using (SqlConnection conn = new SqlConnection(ConfigurationExtensions.GetConnectionString("dbEduegateERPContext").ToString()))
                using (SqlCommand cmd = new SqlCommand("[inventory].[spcGetProductDeliveryCheckMultipleSkus]", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@ProductSKUMapIDString", SqlDbType.VarChar, 2000));
                    adapter.SelectCommand.Parameters["@ProductSKUMapIDString"].Value = ProductSKUMapIDString;

                    DataSet dt = new DataSet();
                    adapter.Fill(dt);
                    CartItemList = dt.Tables[0];
                    if (CartItemList != null && CartItemList.Rows.Count > 0)
                    {
                        foreach (DataRow item in CartItemList.Rows)
                        {
                            var deliveryTypeCheck = new Eduegate.Domain.Entity.Models.ValueObjects.DeliveryTypeCheck();
                            deliveryTypeCheck.SKUID = Convert.ToInt64(item["ProductSKUMapIID"]);
                            deliveryTypeCheck.DeliveryCount = Convert.ToInt64(item["DeliveryCount"]);
                            deliveryTypeCheckList.Add(deliveryTypeCheck);
                        }
                    }
                }


            }
            catch (Exception ex)
            {
            }
            return deliveryTypeCheckList;


        }

        public DataTable GetProductDeliveryDetailsData(string ProductSKUMapIDString)
        {
            DataTable CartItemList = null;
            try
            {

                using (SqlConnection conn = new SqlConnection(ConfigurationExtensions.GetConnectionString("dbEduegateERPContext").ToString()))
                using (SqlCommand cmd = new SqlCommand("[inventory].[spcGetProductDeliveryCheckMultipleSkus]", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@ProductSKUMapIDString", SqlDbType.VarChar, 2000));
                    adapter.SelectCommand.Parameters["@ProductSKUMapIDString"].Value = ProductSKUMapIDString;

                    DataSet dt = new DataSet();
                    adapter.Fill(dt);
                    CartItemList = dt.Tables[0];
                }


            }
            catch (Exception)
            {
                CartItemList = null;
            }
            return CartItemList;
        }


        public string SaveProductSkuBarCode(long skuId, string barCode)
        {
            using (var db = new dbEduegateERPContext())
            {
                var dbEntity = db.ProductSKUMaps.Where(x => x.ProductSKUMapIID == skuId).FirstOrDefault();
                if (dbEntity != null)
                {
                    dbEntity.BarCode = barCode;
                    var isSave = db.SaveChanges();
                }
                return dbEntity.BarCode;
            }
        }

        public List<string> GetProductSKUTags(long skuId,int companyID)
        {
            var tagList = new List<string>();
            using (var db = new dbEduegateERPContext())
            {
                var skuTagsList = (from m in db.ProductSKUTagMaps
                                   join t in db.ProductSKUTags on m.ProductSKUTagID equals t.ProductSKUTagIID
                                   where m.ProductSKuMapID == skuId && m.CompanyID==companyID
                                   select t.TagName).ToList();

                if (skuTagsList != null && skuTagsList.Any())
                {
                    foreach (var skuTags in skuTagsList)
                    {
                        tagList.Add(skuTags);
                    }
                }
            }
            return tagList;
        }

        public List<string> GetCategoryProductSKUTags(long skuId)
        {
            var tagList = new List<string>();
            using (var db = new dbEduegateERPContext())
            {
                var categorySkuTagsList = (from c in db.CategoryTagMaps
                                           join t in db.CategoryTags on c.CategoryTagID equals t.CategoryTagIID
                                           join p in db.ProductCategoryMaps on c.CategoryID equals p.CategoryID
                                           join cc in db.Categories on c.CategoryID equals cc.CategoryIID
                                           join s in db.ProductSKUMaps on p.ProductID equals s.ProductID
                                           where s.ProductSKUMapIID == skuId && cc.IsActive == true
                                           select t.TagName).ToList();

                if (categorySkuTagsList != null && categorySkuTagsList.Any())
                {
                    foreach (var skuTags in categorySkuTagsList)
                    {
                        tagList.Add(skuTags);
                    }
                }
            }
            return tagList;
        }

        public string SaveProductSkuPartNo(long skuId, string partNo)
        {
            using (var db = new dbEduegateERPContext())
            {
                var dbEntity = db.ProductSKUMaps.Where(x => x.ProductSKUMapIID == skuId).FirstOrDefault();
                if (dbEntity != null)
                {
                    dbEntity.PartNo = partNo;
                    var isSave = db.SaveChanges();
                }
                return dbEntity.PartNo;
            }
        }
        public bool SaveProductSKUTags(List<long> selectedTagIds, List<long> skuIDs, List<string> selectedTagNames, long loginID = 0,int companyID = 0)
        {
            using (var db = new dbEduegateERPContext()) 
            {
                if(selectedTagIds.IsNull())
                {
                    var allProductSkuTags = db.ProductSKUTagMaps.Where(x => x.ProductSKuMapID == skuIDs.FirstOrDefault()).ToList();
                    var removeProductSkuTags = allProductSkuTags.Where(x => x.ProductSKuMapID == allProductSkuTags.FirstOrDefault().ProductSKuMapID).ToList();
                    // update productSKUMaps for solr
                    var SkuMapsToUpdate = db.ProductSKUMaps.Where(x => x.ProductSKUMapIID == skuIDs.FirstOrDefault()).FirstOrDefault();
                    SkuMapsToUpdate.UpdatedDate = DateTime.Now;  
                    db.Entry(SkuMapsToUpdate).State = System.Data.Entity.EntityState.Modified;
                    db.ProductSKUTagMaps.RemoveRange(removeProductSkuTags);
                    db.SaveChanges();
                    return true;
                }

                var existingDBTagNames = from dbtag in db.ProductSKUTags
                                         join clientTag in selectedTagNames on dbtag.TagName equals clientTag
                                         select dbtag.TagName;

                var newTagNames = selectedTagNames.Except(existingDBTagNames.Select(x => x));

                if (newTagNames.Any())
                {
                    foreach (var tag in newTagNames)
                    {
                        ProductSKUTag skuTag = new ProductSKUTag() { TagName = tag };
                        db.ProductSKUTags.Add(skuTag);
                    }

                    db.SaveChanges(); //Create new tags
                }
                var newTagIDs = db.ProductSKUTags.Where(x => selectedTagNames.Contains(x.TagName)).Select( x=>x.ProductSKUTagIID).ToList();
                 
                foreach (var skuId in skuIDs)
                {
                    // get Product Sku Tag based on SkuId
                    var allProductSkuTags = db.ProductSKUTagMaps.Where(x => x.ProductSKuMapID == skuId && x.CompanyID == companyID).ToList();

                    // get those are not in selectedTagIds remove them
                    var removeProductSkuTags = allProductSkuTags.Where(x => !newTagIDs.Contains((long)x.ProductSKUTagID)).ToList();
                    db.ProductSKUTagMaps.RemoveRange(removeProductSkuTags);

                    foreach (var tagId in newTagIDs)
                    {
                        var productSkuTag = allProductSkuTags.Where(x => x.ProductSKUTagID == tagId).FirstOrDefault();

                        if (productSkuTag.IsNotNull())
                        {
                            // Update
                        }
                        else
                        {
                            // Insert
                            db.ProductSKUTagMaps.Add(new ProductSKUTagMap() { ProductSKUTagID = tagId, ProductSKuMapID = skuId, CreatedBy = (int)loginID, CreatedDate = DateTime.Now,CompanyID = companyID });
                        }
                    }
                }

                db.SaveChanges();
                return true;
            }
        }

        public List<KeyValueDTO> GetProductSKUTagMaps(long productSKUMapIID,int companyID)
        {
            using (var db = new dbEduegateERPContext())
            {
                var result = from pstm in db.ProductSKUTagMaps
                             join pst in db.ProductSKUTags on pstm.ProductSKUTagID equals pst.ProductSKUTagIID
                             where pstm.ProductSKuMapID == productSKUMapIID && pstm.CompanyID == companyID
                             select new KeyValueDTO() { Key = pst.ProductSKUTagIID.ToString(), Value = pst.TagName };

                return result.ToList();
            }
        }

        public ProductSKUMap GetProductSkuDetails(long skuID)
        {
            using (var db = new dbEduegateERPContext())
            {
                var entity = db.ProductSKUMaps.Where(a => a.ProductSKUMapIID == skuID)
                           .Include("ProductInventorySKUConfigMaps")
                           .Include("ProductInventorySKUConfigMaps.ProductInventoryConfig")
                           .Include("ProductInventorySKUConfigMaps.ProductInventoryConfig.ProductInventoryConfigCultureDatas")
                           .FirstOrDefault();
                return entity;
            }
        }

        public string GetProductSkuCode(long skuID)
        {
            using (var db = new dbEduegateERPContext())
            {
                return db.ProductSKUMaps.Where(a => a.ProductSKUMapIID == skuID).Select(x=>x.ProductSKUCode).FirstOrDefault();
            }
        }


        public ProductSKUMap SaveProductSkuDetails(ProductSKUMap skuMap)
        {
            try
            {
                using (var db = new dbEduegateERPContext())
                {
                    db.ProductSKUMaps.Add(skuMap);

                    foreach (var skuConfig in skuMap.ProductInventorySKUConfigMaps)
                    {
                        db.Entry(skuConfig.ProductInventoryConfig).State = System.Data.Entity.EntityState.Modified;
                        db.Entry(skuConfig).State = System.Data.Entity.EntityState.Modified;
                    }
                    db.Entry(skuMap).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return skuMap;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool UpdateProductSKUStatus(byte statusID, long brandID, CallContext context)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                bool result = false;
                var pdt = dbContext.Products.Where(x => x.BrandID == brandID).ToList();
                if (pdt.IsNotNull())
                {
                    foreach (var pd in pdt)
                    {
                        pd.StatusID = statusID;
                        pd.Updated = DateTime.Now;
                        pd.UpdateBy = (int)context.LoginID;
                        dbContext.SaveChanges();
                        result = true;

                        var sku = (from sk in dbContext.ProductSKUMaps
                                   join p in dbContext.Products
                                   on sk.ProductID equals p.ProductIID
                                   select sk).Where(x => x.ProductID == pd.ProductIID).ToList();
                        if (sku.IsNotNull())
                        {
                            foreach (var sk in sku)
                            {
                                sk.StatusID = statusID;
                                sk.UpdatedDate = DateTime.Now;
                                sk.UpdatedBy = (int)context.LoginID;
                                dbContext.SaveChanges();
                                result = true;
                            }
                        }
                    }
                }
                return result;
            }
        }

        public ProductSKUMap SaveSKUProductMap(ProductSKUMap skudetails)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                bool result = false;
                ProductSKUMap SKU = dbContext.ProductSKUMaps.Where(x => x.ProductSKUMapIID == skudetails.ProductSKUMapIID).FirstOrDefault();
                if (SKU.IsNotNull())
                {
                    SKU.ProductID = skudetails.ProductID;
                    SKU.UpdatedDate = DateTime.Now;
                    SKU.UpdatedBy = skudetails.UpdatedBy;
                    dbContext.SaveChanges();
                    result = true;
                }
                if (result)
                    return dbContext.ProductSKUMaps.Where(x => x.ProductSKUMapIID == skudetails.ProductSKUMapIID).FirstOrDefault();
                else return null;
            }
        }

        public List<ProductImageMap> GetProductImagesByProductID(long productID)
        {
            using (var db = new dbEduegateERPContext())
            {
                return db.ProductImageMaps.Where(a => a.ProductID == productID).ToList();
            }
        }

        public bool SaveSKUManagers(long SelectedKeyValueOwnerId, long ID, CallContext _callContext)
        {
            using (var db = new dbEduegateERPContext())
              {
                var loginID = _callContext.IsNotNull() ? _callContext.LoginID.HasValue ? (int)_callContext.LoginID.Value : default(int) : default(int);

                var allProductSkuTags = db.ProductInventorySKUConfigMaps.Where(x => x.ProductSKUMapID == ID).FirstOrDefault();
                var skuTag = db.ProductInventoryConfigs.Where(x => x.ProductInventoryConfigIID == allProductSkuTags.ProductInventoryConfigID).FirstOrDefault();

                //update sku Settings with  product Manager Id
                skuTag.EmployeeID = SelectedKeyValueOwnerId;
                skuTag.CompanyID = _callContext.CompanyID;
                skuTag.UpdatedBy = loginID;
                skuTag.UpdatedDate = DateTime.Now;

                db.SaveChanges(); 
                return true;
            }
        }
         
        public ProductSKUTag GetSKUTag(long tagId)
        {
            using (var db = new dbEduegateERPContext())
            {
                return db.ProductSKUTags.Where(x=>x.ProductSKUTagIID == tagId).FirstOrDefault();            
            } 
        }

        public List<ProductSKUTagMap> GetSKUTagMaps(long tagId)
        { 
            var tagList = new List<ProductSKUTagMap>();
            using (var db = new dbEduegateERPContext())
            {
                var tagsList = (from m in db.ProductSKUTagMaps
                               join s in db.ProductSKUTags
                               on m.ProductSKUTagID equals s.ProductSKUTagIID
                               where  m.ProductSKUTagID == tagId
                               select m).Include(x=>x.ProductSKUTag).ToList();

                if (tagsList != null)
                {
                    foreach (var skuTags in tagsList)
                    {
                        tagList.Add(skuTags);
                    }
                }
                return tagList;
            }
        }

        public bool IsPartNumberDuplicated(string partNumber, long productSKUMapID)
        {
            using (var db = new dbEduegateERPContext())
            {
                List<ProductSKUMap> product;

                if (productSKUMapID == 0)
                {
                    product = db.ProductSKUMaps.Where(x => x.PartNo == partNumber).ToList();
                }
                else
                {
                    product = db.ProductSKUMaps.Where(x => x.ProductSKUMapIID != productSKUMapID && x.PartNo == partNumber).ToList();
                }

                if(product.Count() >= 1)
                {
                    return true;
                }

                return false;
            }
        }

        public bool IsBarCodeDuplicated(string barCode, long productSKUMapID)
        {
            using (var db = new dbEduegateERPContext())
            {
                List<ProductSKUMap> product;

                if (productSKUMapID == 0)
                {
                    product = db.ProductSKUMaps.Where(x => x.BarCode == barCode).ToList();
                }
                else
                {
                    product = db.ProductSKUMaps.Where(x => x.ProductSKUMapIID != productSKUMapID && x.BarCode == barCode).ToList();
                }

                if (product.Count() >= 1)
                {
                    return true;
                }

                return false;
            }
        }

        public bool IsProductSKUNameDuplicated(string SKUName, long productSKUMapID)
        {
            using (var db = new dbEduegateERPContext())
            {
                List<ProductSKUMap> product;

                if (productSKUMapID == 0)
                {
                    product = db.ProductSKUMaps.Where(x => x.SKUName == SKUName).ToList();
                }
                else
                {
                    product = db.ProductSKUMaps.Where(x => x.ProductSKUMapIID != productSKUMapID && x.SKUName == SKUName).ToList();
                }

                if (product.Count() >= 1)
                {
                    return true;
                }

                return false;
            }
        }
      
        public bool IsProductCodeDuplicated(string SKU, long productSKUMapID)
        {
            using (var db = new dbEduegateERPContext())
            {
                List<ProductSKUMap> product;

                if (productSKUMapID == 0)
                {
                    product = db.ProductSKUMaps.Where(x => x.ProductSKUCode == SKU).ToList();
                }
                else
                {
                    product = db.ProductSKUMaps.Where(x => x.ProductSKUMapIID != productSKUMapID && x.ProductSKUCode == SKU).ToList();
                }

                if (product.Count() >= 1)
                {
                    return true;
                }

                return false;
            }
        }
        ///// <summary>
        ///// GetProductBundleItemDetail
        ///// </summary>
        ///// <param name="headId"></param>
        ///// <returns></returns>
        //public List<ProductBundle> GetProductBundleItemDetail(long productSKUMapID)
        //{
        //    var bundleList = new List<ProductBundle>();

        //    dbEduegateERPContext db = new dbEduegateERPContext();
        //    try
        //    {
        //        //bundleList = (from pb in db.ProductBundles.AsEnumerable()
        //        //               join p in db.Products on pb.ToProductID equals p.ProductIID
        //        //               join ps in db.ProductSKUMaps on pb.ToProductSKUMapID equals ps.ProductSKUMapIID                              
        //        //               where (pb.ToProductSKUMapID == productSKUMapID )
        //        //               group pb by new { pb.FromProductID, pb.FromProductSKUMapID, pb.Quantity, pb.CostPrice } into productGroup
        //        //               select new ProductBundle()
        //        //               {
        //        //                   FromProductID = productGroup.Key.FromProductID,
        //        //                   FromProductSKUMapID = productGroup.Key.FromProductSKUMapID,
        //        //                   Quantity = productGroup.Key.Quantity,
        //        //                   CostPrice = productGroup.Key.CostPrice,


        //        //               }).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        bundleList = null;
        //        SystemLog.Log("Application", EventLogEntryType.Error, ex, "No Items found to . ProductSKUMapID:" + productSKUMapID.ToString(), TrackingCode.TransactionEngine);
        //        throw;
        //    }

        //    return bundleList;
        //}

        public List<Category> GetCategories()
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var entity = dbContext.Categories
                    .Include(a => a.CategoryImageMaps)
                    //.Include(a => a.CategoryTagMaps)
                    .Include(a => a.CategoryCultureDatas)
                    //.Include(a => a.CategorySettings)
                    .Include(x => x.Culture)
                    .Where(x => x.IsActive == true)
                    .OrderBy(x => x.CategoryName)
                    .ToList();
                return entity;
            }
        }

        public List<ProductImageMap> OnlineStoreGetProductImages(List<long> skuIDs)
        {
            using (var db = new dbEduegateERPContext())
            {
                var productImages = db.ProductImageMaps
                    .Where(a => skuIDs.Contains(a.ProductSKUMapID.Value)
                            && a.Sequence == 1 && (a.ProductImageTypeID == 1 || a.ProductImageTypeID == 8 || a.ProductImageTypeID == 3))
                    .ToList();
                return productImages;
            }
        }

        public List<Student> GetStudentsSiblings(long loginID)
        {
            var studentDTO = new List<Student>();
            using (var dbContext = new dbEduegateERPContext())
            {
                var dateFormat = ConfigurationExtensions.GetAppConfigValue("DateFormat");
                var students = dbContext.Students.Where(s => s.Parent.LoginID == loginID && s.IsActive == true).ToList();

                foreach (var stud in students)
                {
                    studentDTO.Add(new Student()
                    {
                        StudentIID = stud.StudentIID,
                        AdmissionNumber = stud.AdmissionNumber,
                        FirstName = stud.FirstName,
                        MiddleName = stud.MiddleName,
                        LastName = stud.LastName,
                        ClassID = stud.ClassID,
                        SectionID = stud.SectionID,
                        SchoolID = stud.SchoolID,
                        AcademicYearID = stud.AcademicYearID,
                        //ClassName = stud.ClassID.HasValue ? stud.Class?.ClassDescription : null,
                        //SectionName = stud.SectionID.HasValue ? stud.Section?.SectionName : null,
                        //AcademicYear = stud.AcademicYearID.HasValue ? stud.AcademicYear?.Description + "(" + stud.AcademicYear?.AcademicYearCode + ")" : null,
                        //IsSelected = false,
                        //StudentProfile = stud.StudentProfile,
                        //ParentEmailID = stud?.Parent?.GaurdianEmail,
                    });
                }
            }

            return studentDTO;
        }
    }
}