using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Extensions;

namespace Eduegate.Domain.Mappers.PriceSettings
{
    public class ProductPriceSKUQuantityMapper : IDTOEntityMapper<QuantityPriceDTO, ProductPriceListSKUQuantityMap>
    {
        private CallContext _context;

        public static ProductPriceSKUQuantityMapper Mapper(CallContext context)
        {
            var mapper = new ProductPriceSKUQuantityMapper();
            mapper._context = context;
            return mapper;
        }

        public QuantityPriceDTO ToDTO(ProductPriceListSKUQuantityMap entity)
        {
            if (entity.IsNotNull())
            {
                QuantityPriceDTO qpDTO = new QuantityPriceDTO();

                qpDTO.ProductPriceListSKUQuantityMapIID = entity.ProductPriceListSKUQuantityMapIID;
                qpDTO.ProductPriceListSKUMapID = entity.ProductPriceListSKUMapID;
                qpDTO.ProductSKUMapID = entity.ProductSKUMapID;
                qpDTO.Quantity = entity.Quantity;
                qpDTO.Discount = entity.DiscountPrice;
                qpDTO.DiscountPercentage = entity.DiscountPercentage;
                qpDTO.CreatedBy = entity.CreatedBy;
                qpDTO.UpdatedBy = entity.UpdatedBy;
                qpDTO.CreatedDate = entity.CreatedDate;
                qpDTO.UpdatedDate = entity.UpdatedDate;
                //qpDTO.TimeStamps = entity.TimeStamps.IsNotNull() ? Convert.ToBase64String(entity.TimeStamps) : null;

                return qpDTO;
            }
            else
            {
                return new QuantityPriceDTO();
            }
        }

        public ProductPriceListSKUQuantityMap ToEntity(QuantityPriceDTO dto)
        {
            if (dto.IsNotNull())
            {
                ProductPriceListSKUQuantityMap pplsqm = new ProductPriceListSKUQuantityMap();

                pplsqm.ProductPriceListSKUQuantityMapIID = dto.ProductPriceListSKUQuantityMapIID;
                pplsqm.ProductPriceListSKUMapID = dto.ProductPriceListSKUMapID.IsNotNull() ? dto.ProductPriceListSKUMapID : null;
                pplsqm.ProductSKUMapID = dto.ProductSKUMapID.IsNotNull() ? dto.ProductSKUMapID : null;
                pplsqm.Quantity = dto.Quantity;
                pplsqm.DiscountPrice = dto.Discount;
                pplsqm.DiscountPercentage = dto.DiscountPercentage;
                pplsqm.CreatedBy = dto.ProductPriceListSKUQuantityMapIID > 0 ? dto.CreatedBy : (int)_context.LoginID;
                pplsqm.UpdatedBy = dto.ProductPriceListSKUQuantityMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy;
                pplsqm.CreatedDate = dto.ProductPriceListSKUQuantityMapIID > 0 ? dto.CreatedDate : DateTime.Now;
                pplsqm.UpdatedDate = dto.ProductPriceListSKUQuantityMapIID > 0 ? DateTime.Now : dto.UpdatedDate;
                //pplsqm.TimeStamps = dto.TimeStamps.IsNotNull() ? Convert.FromBase64String(dto.TimeStamps) : null;

                return pplsqm;
            }
            else
            {
                return new ProductPriceListSKUQuantityMap();
            }
        }
    }
}
