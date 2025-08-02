using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.PriceSettings;
using Eduegate.Framework.Extensions;

namespace Eduegate.Domain.Mappers.PriceSettings
{
    public class ProductPriceSettingQuantityMapper : IDTOEntityMapper<ProductPriceSettingQuantityDTO, ProductPriceListProductQuantityMap>
    {
        private CallContext _context;

        public static ProductPriceSettingQuantityMapper Mapper(CallContext context)
        {
            var mapper = new ProductPriceSettingQuantityMapper();
            mapper._context = context;
            return mapper;
        }

        public ProductPriceSettingQuantityDTO ToDTO(ProductPriceListProductQuantityMap entity)
        {
            if (entity.IsNotNull())
            {
                ProductPriceSettingQuantityDTO dto = new ProductPriceSettingQuantityDTO();

                dto.ProductPriceListProductQuantityMapIID = entity.ProductPriceListProductQuantityMapIID;
                dto.ProductPriceListProductMapID = entity.ProductPriceListProductMapID;
                dto.ProductID = entity.ProductID;
                dto.Quantity = entity.Quantity;
                dto.Discount = entity.DiscountPrice;
                dto.DiscountPercentage = entity.DiscountPercentage;
                dto.CreatedBy = entity.CreatedBy;
                dto.CreatedDate = entity.CreatedDate;
                dto.UpdatedBy = entity.UpdatedBy;
                dto.UpdatedDate = entity.UpdatedDate;
                dto.TimeStamps = entity.TimeStamps.IsNotNull() ? Convert.ToBase64String(entity.TimeStamps) : string.Empty;

                return dto;
            }
            else
            {
                return new ProductPriceSettingQuantityDTO();
            }
        }

        public ProductPriceListProductQuantityMap ToEntity(ProductPriceSettingQuantityDTO dto)
        {
            if (dto.IsNotNull())
            {
                ProductPriceListProductQuantityMap entity = new ProductPriceListProductQuantityMap();

                entity.ProductPriceListProductQuantityMapIID = dto.ProductPriceListProductQuantityMapIID;
                entity.ProductPriceListProductMapID = dto.ProductPriceListProductMapID;
                entity.ProductID = dto.ProductID;
                entity.Quantity = dto.Quantity;
                entity.DiscountPrice = dto.Discount;
                entity.DiscountPercentage = dto.DiscountPercentage;
                entity.CreatedBy = dto.ProductPriceListProductQuantityMapIID > 0 ? dto.CreatedBy : (int)_context.LoginID;
                entity.CreatedDate = dto.ProductPriceListProductQuantityMapIID > 0 ? dto.CreatedDate : DateTime.Now;
                entity.UpdatedBy = dto.ProductPriceListProductQuantityMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy;
                entity.UpdatedDate = dto.ProductPriceListProductQuantityMapIID > 0 ? DateTime.Now : dto.UpdatedDate;
                entity.TimeStamps = dto.TimeStamps.IsNotNull() ? Convert.FromBase64String(dto.TimeStamps) : null;

                return entity;
            }
            else
            {
                return new ProductPriceListProductQuantityMap();
            }
        }

    }
}
