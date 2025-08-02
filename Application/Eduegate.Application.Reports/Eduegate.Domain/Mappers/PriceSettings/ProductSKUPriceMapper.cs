using System;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Extensions;
using System.Collections.Generic;
using Eduegate.Domain.Repository;
using Eduegate.Framework.Security.Secured;
using Eduegate.Domain.Repository.Security;
using System.Linq;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Domain.Mappers
{
    public class ProductSKUPriceMapper : IDTOEntityMapper<ProductPriceSKUDTO, ProductPriceListSKUMap>
    {
        private CallContext _context;

        public static ProductSKUPriceMapper Mapper(CallContext context)
        {
            var mapper = new ProductSKUPriceMapper();
            mapper._context = context;
            return mapper;
        }

        public ProductPriceSKUDTO ToDTO(ProductPriceListSKUMap entity)
        {
            if (entity.IsNotNull() && _context.IsNotNull())
            {

                var secured = new SecuredData(new Eduegate.Domain.Repository.Security.SecurityRepository().GetUserClaimKey(_context.LoginID.Value, (int)Eduegate.Services.Contracts.Enums.ClaimType.ProductPrice));
                if (secured.HasAccess(entity.GetIID(), (Eduegate.Framework.Security.Enums.ClaimType)Enum.Parse(typeof(Eduegate.Services.Contracts.Enums.ClaimType), Eduegate.Services.Contracts.Enums.ClaimType.ProductPrice.ToString())))
                {
                    return DTOMapper(entity);
                }
                else
                {
                    return null;
                }
            }
            // case written to handle call from TE as _callcontext will be null
            else if (entity.IsNotNull() && _context.IsNull()) { return DTOMapper(entity); }
            else
            {
                return new ProductPriceSKUDTO();
            }
        }

        ProductPriceSKUDTO DTOMapper(ProductPriceListSKUMap entity)
        {
            // get login info
            var supplier =  _context.IsNotNull()?  new SupplierBL(_context).GetSupplierByLoginID(Convert.ToInt64(_context.LoginID)) : null;

            //var categories = new CategoryRepository().GetCategoryBySkuId(entity.ProductSKUID.Value);
            //var category = categories.Where(x => x.Profit > 0).FirstOrDefault();

            ProductPriceSKUDTO ppsDTO = new ProductPriceSKUDTO();
            ppsDTO.ProductSKUQuntityLevelPrices = new List<QuantityPriceDTO>();

            // if GetSupplierByLoginID returns null  we r getting from GetSuplierByProductPriceListId   ---- Please change if its not correct
            //ppsDTO.IsMarketPlace = supplier.IsNotNull() ? (supplier.IsMarketPlace.IsNotNull() ? (bool)supplier.IsMarketPlace : false ): Convert.ToBoolean(new SupplierRepository().GetSuplierByProductPriceListId(entity.ProductPriceListID.Value).IsMarketPlace);
            if (supplier.IsNotNull())
            {
                ppsDTO.IsMarketPlace = supplier.IsMarketPlace.HasValue ? supplier.IsMarketPlace.Value : false;
            }
            else
            {
                var supplierDetail = new SupplierRepository().GetSuplierByProductPriceListId(entity.ProductPriceListID.Value);
                if (supplierDetail.IsNotNull())
                {
                    ppsDTO.IsMarketPlace = supplierDetail.IsMarketPlace.HasValue ? supplierDetail.IsMarketPlace.Value : false;
                }
            }

            ppsDTO.ProductPriceListItemMapIID = entity.ProductPriceListItemMapIID;
            ppsDTO.CompanyID = entity.CompanyID;
            ppsDTO.ProductPriceListID = entity.ProductPriceListID;
            ppsDTO.ProductSKUID = entity.ProductSKUID;
            ppsDTO.PriceDescription = new PriceSettingsRepository().GetPriceDescription(Convert.ToInt32(entity.ProductPriceListID));
            ppsDTO.UnitGroundID = entity.UnitGroundID;
            ppsDTO.CustomerGroupID = entity.CustomerGroupID;
            ppsDTO.SellingQuantityLimit = entity.SellingQuantityLimit;
            ppsDTO.Amount = entity.Amount;
            ppsDTO.SortOrder = entity.SortOrder;
            ppsDTO.PricePercentage = entity.PricePercentage;

            //ppsDTO.Price = (category != null && category.Profit != null) ? entity.Cost + entity.Cost * (category.Profit / 100)
            //                : (supplier != null && supplier.Profit != null) ? entity.Cost + entity.Cost * (supplier.Profit / 100) : entity.Price.IsNotNull() ? entity.Price : entity.Cost;


            ppsDTO.Price = entity.Price;
            ppsDTO.Discount = entity.Discount;
            ppsDTO.DiscountPercentage = entity.DiscountPercentage;
            ppsDTO.Cost = entity.Cost;
            ppsDTO.CreatedBy = entity.CreatedBy;
            ppsDTO.UpdatedBy = entity.UpdatedBy;
            ppsDTO.CreatedDate = entity.CreatedDate;
            ppsDTO.UpdatedDate = entity.UpdatedDate;
            //ppsDTO.TimeStamps = entity.TimeStamps.IsNotNull() ? Convert.ToBase64String(entity.TimeStamps) : null;
            ppsDTO.PriceListStatus = new KeyValueDTO();
            ppsDTO.PriceListStatus.Key = Convert.ToString(Convert.ToInt32(entity.IsActive));
            ppsDTO.PriceListStatus.Value = entity.IsActive == true ? "Active" : "InActive";

            var ProductPriceListSKUQuantityMaps = new List<ProductPriceListSKUQuantityMap>();
            ProductPriceListSKUQuantityMaps = new PriceSettingsRepository().GetProductPriceListSkuQuantityMaps(entity.ProductPriceListItemMapIID);

            if (ProductPriceListSKUQuantityMaps.IsNotNull() && ProductPriceListSKUQuantityMaps.Count > 0)
            {
                foreach (var pplsqm in ProductPriceListSKUQuantityMaps)
                {
                    ppsDTO.ProductSKUQuntityLevelPrices.Add(PriceSettings.ProductPriceSKUQuantityMapper.Mapper(_context).ToDTO(pplsqm));
                }
            }
            if (ppsDTO.CompanyID == 0)
            {
                ppsDTO.CompanyID = _context.CompanyID;
            }
            return ppsDTO;
        }

        public ProductPriceListSKUMap ToEntity(ProductPriceSKUDTO dto)
        {
            if (dto.IsNotNull())
            {
                ProductPriceListSKUMap ppls = new ProductPriceListSKUMap();
                ppls.ProductPriceListSKUQuantityMaps = new List<ProductPriceListSKUQuantityMap>();
                ppls.CompanyID = dto.CompanyID.IsNotNull() ? dto.CompanyID : _context.CompanyID;
                ppls.ProductPriceListItemMapIID = dto.ProductPriceListItemMapIID;
                ppls.ProductPriceListID = dto.ProductPriceListID.IsNotNull() ? dto.ProductPriceListID : null;
                ppls.ProductSKUID = dto.ProductSKUID.IsNotNull() ? dto.ProductSKUID : null;
                ppls.UnitGroundID = dto.UnitGroundID.IsNotNull() ? dto.UnitGroundID : null;
                ppls.CustomerGroupID = dto.CustomerGroupID.IsNotNull() ? dto.CustomerGroupID : null;
                ppls.SellingQuantityLimit = dto.SellingQuantityLimit;
                ppls.Amount = dto.Amount;
                ppls.SortOrder = dto.SortOrder;
                ppls.PricePercentage = dto.PricePercentage;
                ppls.Price = dto.Price;
                ppls.Discount = dto.Discount;
                ppls.DiscountPercentage = dto.DiscountPercentage;
                ppls.Cost = dto.Cost;
                ppls.CreatedBy = dto.ProductPriceListItemMapIID > 0 ? dto.CreatedBy : (int)_context.LoginID;
                ppls.UpdatedBy = dto.ProductPriceListItemMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy;
                ppls.CreatedDate = dto.ProductPriceListItemMapIID > 0 ? dto.CreatedDate : DateTime.Now;
                ppls.UpdatedDate = dto.ProductPriceListItemMapIID > 0 ? DateTime.Now : dto.UpdatedDate;
                //ppls.TimeStamps = dto.TimeStamps.IsNotNull() ? Convert.FromBase64String(dto.TimeStamps) : null;
                ppls.IsActive = Convert.ToBoolean(Convert.ToInt32(dto.PriceListStatus.Key));

                if (dto.ProductSKUQuntityLevelPrices.IsNotNull() && dto.ProductSKUQuntityLevelPrices.Count > 0)
                {
                    foreach (var psqlp in dto.ProductSKUQuntityLevelPrices)
                    {
                        var pplsqm = PriceSettings.ProductPriceSKUQuantityMapper.Mapper(_context).ToEntity(psqlp);
                        //pplsqm.ProductPriceListSKUMapID = dto.ProductPriceListID;
                        ppls.ProductPriceListSKUQuantityMaps.Add(pplsqm);
                    }
                }
                if (ppls.CompanyID == 0)
                {
                    ppls.CompanyID = _context.CompanyID;
                }

                return ppls;
            }
            else
            {
                return new ProductPriceListSKUMap();
            }
        }
    }
}
