using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.CustomEntity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;
using Eduegate.Domain.Repository.Frameworks;
using Eduegate.Domain.Repository.Security;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;


namespace Eduegate.Domain.Mappers
{
    public class TransactionDetailsMapper : IDTOEntityMapper<TransactionDetailDTO, TransactionDetail>
    {
        private CallContext _context;

        public static TransactionDetailsMapper Mapper(CallContext context)
        {
            var mapper = new TransactionDetailsMapper();
            mapper._context = context;
            return mapper;
        }

        public TransactionDetail ToEntity(TransactionDetailDTO dto)
        {
            if (dto != null)
            {
                var settingDat = new SettingRepository().GetSettingDetail("DOCUMENTTYPE_FOCSALES").SettingValue;
                var docTypeID = int.Parse(settingDat);

                var entity = new TransactionDetail();
               
                    entity.DetailIID = dto.DetailIID;
                    entity.HeadID = dto.HeadID;
                    entity.ProductID = new ProductDetailBL(_context).GetProductBySKUID(Convert.ToInt64(dto.ProductSKUMapID)).ProductIID;
                    entity.ProductSKUMapID = dto.ProductSKUMapID;
                    entity.Quantity = dto.Quantity;
                    entity.UnitID = dto.UnitID;
                    entity.DiscountPercentage = dto.DiscountPercentage;
                    entity.UnitPrice = dto.DocumentTypeID == docTypeID ? 0 : dto.UnitPrice;
                    entity.Amount = dto.DocumentTypeID == docTypeID ? 0 : dto.UnitPrice * dto.Quantity;
                    entity.ExchangeRate = dto.ExchangeRate;
                    entity.WarrantyDate = dto.WarrantyDate.IsNotNull() ? Convert.ToDateTime(dto.WarrantyDate) : DateTime.Now;
                    entity.SerialNumber = dto.SerialNumber.IsNotNull() ? dto.SerialNumber : null;
                    entity.ParentDetailID = dto.ParentDetailID;
                    entity.Action = dto.Action;
                    entity.Remark = dto.Remark;

                    entity.TaxAmount1 = dto.TaxAmount1;
                    entity.TaxAmount2 = dto.TaxAmount2;
                    entity.TaxTemplateID = dto.TaxTemplateID;
                    entity.TaxPercentage = dto.TaxPercentage;
                    entity.HasTaxInclusive = dto.HasTaxInclusive;
                    entity.InclusiveTaxAmount = dto.InclusiveTaxAmount;
                    entity.ExclusiveTaxAmount = dto.ExclusiveTaxAmount;
                    entity.WarrantyStartDate = dto.WarrantyStartDate;
                    entity.WarrantyEndDate = dto.WarrantyEndDate;
                    entity.CostCenterID = dto.CostCenterID;
                    entity.LastCostPrice = dto.DocumentTypeID == docTypeID ? dto.CostPrice : dto.LastCostPrice;
                    entity.LandingCost = dto.LandingCost;
                    entity.Fraction = dto.Fraction;
                    entity.ForeignAmount = dto.ForeignAmount;
                    entity.ForeignRate = dto.ForeignRate;
                    entity.ExchangeRate = dto.ExchangeRate;
                    entity.UnitGroupID = dto.UnitGroupID;
                    if (dto.DetailIID <= 0)
                    {
                        entity.CreatedBy = _context == null ? (long?)null : _context.LoginID;
                        entity.CreatedDate = DateTime.Now;
                    }

                    entity.UpdatedBy = _context == null ? (long?)null : _context.LoginID;
                    entity.UpdatedDate = DateTime.Now;

                    if (dto.SKUDetails != null && dto.SKUDetails.Count > 0)
                    {
                        foreach (var item in dto.SKUDetails)
                        {
                            item.ProductSKUMapID = item.ProductSKUMapID > 0 ? item.ProductSKUMapID : (long)dto.ProductSKUMapID;
                        }
                        //entity.ProductSerialMaps = dto.SKUDetails.Select(x => ProductSerialMapMapper.Mapper(_context).ToEntity(x)).ToList();
                    }

                    if (dto.TransactionAllocations != null && dto.TransactionAllocations.Count > 0)
                    {
                        entity.TransactionAllocations = dto.TransactionAllocations.Select(x => TransactionAllocationMapper.Mapper().ToEntity(x)).ToList();
                    }
               
                return entity;
            }

            else return new TransactionDetail();
        }

        public TransactionDetailDTO ToDTO(TransactionDetail entity)
        {
            return ToDTO(entity, _context.IsNotNull() ? _context.CompanyID.Value : default(int));
        }

        public TransactionDetailDTO ToDTO(TransactionDetail entity, int companyID)
        {
            if (entity != null)
            {
                var transactionDetailDTO = new TransactionDetailDTO();
                ProductSKUDetail skuDetail = entity.ProductSKUMapID.IsNotNull() && (long)entity.ProductSKUMapID > 0 ?
                    new TransactionRepository().GetTransactionProductWithSKUName((long)entity.ProductSKUMapID) : new ProductSKUDetail();

                transactionDetailDTO.Barcode = skuDetail.Barcode;
                transactionDetailDTO.PartNo = skuDetail.PartNo;
                transactionDetailDTO.SKU = skuDetail.SKU;
                transactionDetailDTO.DetailIID = entity.DetailIID;
                transactionDetailDTO.ParentDetailID = entity.ParentDetailID;
                transactionDetailDTO.Action = entity.Action;
                transactionDetailDTO.Remark = entity.Remark;
                transactionDetailDTO.HeadID = entity.HeadID;
                transactionDetailDTO.ProductID = entity.ProductID;
                transactionDetailDTO.ProductSKUMapID = entity.ProductSKUMapID;
                transactionDetailDTO.UnitID = entity.UnitID;
                transactionDetailDTO.UnitPrice = entity.UnitPrice;
                transactionDetailDTO.Quantity = entity.Quantity;
                transactionDetailDTO.DiscountPercentage = entity.DiscountPercentage;
                transactionDetailDTO.Amount = entity.Amount;
                transactionDetailDTO.WarrantyDate = entity.WarrantyDate.IsNull() ? DateTime.Now : Convert.ToDateTime(entity.WarrantyDate);
                transactionDetailDTO.TaxAmount1 = entity.TaxAmount1;
                transactionDetailDTO.TaxAmount2 = entity.TaxAmount2;
                transactionDetailDTO.TaxPercentage = entity.TaxPercentage;
                transactionDetailDTO.TaxTemplateID = entity.TaxTemplateID;
                transactionDetailDTO.HasTaxInclusive = entity.HasTaxInclusive;
                transactionDetailDTO.InclusiveTaxAmount = entity.InclusiveTaxAmount;
                transactionDetailDTO.ExclusiveTaxAmount = entity.ExclusiveTaxAmount;
                transactionDetailDTO.WarrantyStartDate = entity.WarrantyStartDate;
                transactionDetailDTO.WarrantyEndDate = entity.WarrantyEndDate;
                transactionDetailDTO.CostCenterID = entity.CostCenterID;
                transactionDetailDTO.LastCostPrice = entity.LastCostPrice;
                transactionDetailDTO.CostPrice = entity.LastCostPrice;
                transactionDetailDTO.LandingCost = entity.LandingCost;
                transactionDetailDTO.Fraction = entity.Fraction;
                transactionDetailDTO.ForeignAmount = entity.ForeignAmount ?? 0;
                transactionDetailDTO.ForeignRate = entity.ForeignRate ?? 0;
                transactionDetailDTO.ExchangeRate = entity.ExchangeRate;
                transactionDetailDTO.ProductCode = GetProductCode(entity.ProductID);
                if (entity.UnitGroupID != null)
                {
                    transactionDetailDTO.UnitGroupID = entity.UnitGroupID;
                    transactionDetailDTO.UnitList = GetUnitDataByUnitGroup(transactionDetailDTO.UnitGroupID);
                    transactionDetailDTO.UnitDTO = GetUnitsByUnitGroup(transactionDetailDTO.UnitGroupID);
                    transactionDetailDTO.UnitID = entity.UnitID;
                    transactionDetailDTO.Unit = transactionDetailDTO.UnitDTO.Where(y => y.Key == entity.UnitID.Value.ToString()).Select(x => x.Value).FirstOrDefault();
                }

                if (entity.UnitID != null && entity.UnitGroupID == null)
                {
                    transactionDetailDTO.UnitID = entity.UnitID;
                    transactionDetailDTO.Unit = UnitDetail(transactionDetailDTO.UnitID);
                }

                if (entity.CostCenterID != null)
                {
                    transactionDetailDTO.CostCenterID = entity.CostCenterID;
                    transactionDetailDTO.CostCenter = CostCenterDetail(transactionDetailDTO.CostCenterID).CostCenter;
                }

                var skuSettingDetail = entity.ProductSKUMapID.IsNotNull() && (long)entity.ProductSKUMapID > 0 ?
                    new ProductCatalogRepository().GetProductSKUDetail(entity.ProductSKUMapID.Value) : new ProductSKUDetail();

                if (skuSettingDetail.IsNotNull())
                {
                    transactionDetailDTO.IsSerialNumber = skuSettingDetail.IsSerialNumber != null ? Convert.ToBoolean(skuSettingDetail.IsSerialNumber) : false;
                    transactionDetailDTO.IsSerialNumberOnPurchase = skuSettingDetail.IsSerialNumberOnPurchase != null ? Convert.ToBoolean(skuSettingDetail.IsSerialNumberOnPurchase) : false;

                    transactionDetailDTO.ProductLength = skuSettingDetail.ProductTypeName == Framework.Enums.ProductTypes.Digital.ToString() ? (skuSettingDetail.ProductLength != null ? Convert.ToInt32(skuSettingDetail.ProductLength) : 0) : 0;
                    transactionDetailDTO.ProductTypeName = skuSettingDetail.ProductTypeName;
                }

                var settingRepository = new SettingRepository();
                var securityRepository = new SecurityRepository();
                var hasFullReadClaim = false;
                try
                {
                    var settingValue = settingRepository.GetSettingDetail(Eduegate.Framework.Helper.Constants.READSERIALKEYCLAIM)?.SettingValue;

                    hasFullReadClaim = (_context.IsNotNull() && _context.LoginID.IsNotNull()) ? settingValue != null ? securityRepository.HasClaimAccess(long.Parse(settingValue), _context.LoginID.Value) : false : false;
                }
                catch (Exception) { }

                var hasPartialReadClaim = false;
                try
                {
                    var settingValue = settingRepository.GetSettingDetail(Eduegate.Framework.Helper.Constants.READSERIALKEYCLAIM)?.SettingValue;

                    hasPartialReadClaim = (_context.IsNotNull() && _context.LoginID.IsNotNull()) ? settingValue != null ? securityRepository.HasClaimAccess(long.Parse(settingValue), _context.LoginID.Value) : false : false;
                }
                catch (Exception) { }
                var visibleSerialKey = "";
                if (entity.SerialNumber.IsNotNull())
                {
                    if (hasFullReadClaim)
                        visibleSerialKey = entity.SerialNumber;
                    else if (hasPartialReadClaim)
                    {
                        var length = entity.SerialNumber.Length;

                        if (length <= 4)
                        {
                            visibleSerialKey = new String('x', length);
                        }
                        else
                        {
                            visibleSerialKey = new String('x', length - 4) + entity.SerialNumber.Substring(length - 4);
                        }
                        entity.SerialNumber = visibleSerialKey;
                    }
                    else
                        visibleSerialKey = "";
                }
                transactionDetailDTO.SerialNumber = entity.SerialNumber.IsNotNull() ? visibleSerialKey : null;

                if (entity.ProductSerialMaps != null && entity.ProductSerialMaps.Count > 0)
                {
                    transactionDetailDTO.SKUDetails = new List<ProductSerialMapDTO>();
                    transactionDetailDTO.SKUDetails = entity.ProductSerialMaps.Select(x => ProductSerialMapMapper.Mapper(_context).ToDTO(x)).ToList();
                }

                if (entity.TransactionAllocations != null && entity.TransactionAllocations.Count > 0)
                {
                    transactionDetailDTO.TransactionAllocations = entity.TransactionAllocations.Select(x => TransactionAllocationMapper.Mapper().ToDTO(x)).ToList();
                }

                transactionDetailDTO.CreatedBy = entity.CreatedBy;
                transactionDetailDTO.UpdatedBy = entity.UpdatedBy;
                transactionDetailDTO.CreatedDate = entity.CreatedDate.HasValue ? entity.CreatedDate.Value.ToLongDateString() : null;
                transactionDetailDTO.UpdatedDate = entity.UpdatedDate.HasValue ? entity.UpdatedDate.Value.ToLongDateString() : null;

                return transactionDetailDTO;
            }

            else return new TransactionDetailDTO();
        }

        private TransactionDetailDTO CostCenterDetail(long? costCenterID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var dtos = new TransactionDetailDTO();
                var repository = new EntiyRepository<CostCenter, dbEduegateERPContext>(dbContext);
                var costCenterDetail = repository.Get(a => a.CostCenterID == costCenterID).LastOrDefault();

                dtos.CostCenter = new KeyValueDTO() { Key = costCenterDetail.CostCenterID.ToString(), Value = costCenterDetail.CostCenterName };

                return dtos;
            }
        }
        private string UnitDetail(long? unitID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var dtos = new TransactionDetailDTO();
                var repository = new EntiyRepository<Unit, dbEduegateERPContext>(dbContext);
                var unitDetail = repository.Get(a => a.UnitID == unitID).LastOrDefault();

                if (unitDetail != null)
                    return unitDetail.UnitCode;
                else
                    return null;
            }
        }
        private List<UnitsDTO> GetUnitDataByUnitGroup(long? groupID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {

                var units = from unts in dbContext.Units
                            where unts.UnitGroupID == groupID
                            select new UnitsDTO
                            {
                                UnitID = unts.UnitID,
                                Fraction = unts.Fraction,
                                UnitCode = unts.UnitCode,
                                UnitGroupID = unts.UnitGroupID
                            };

                return units.ToList();
            }

        }

        private string GetProductCode(long? productID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var prdct = dbContext.Products.Where(x=>x.ProductIID== productID).Select(x => x.ProductCode).FirstOrDefault();
                return prdct;

            }

        }
        private List<KeyValueDTO> GetUnitsByUnitGroup(long? groupID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {

                var units = from unts in dbContext.Units
                            where unts.UnitGroupID == groupID
                            select new KeyValueDTO
                            {
                                Key = unts.UnitID.ToString(),
                                Value = unts.UnitCode

                            };

                return units.ToList();
            }

        }

    }

}
