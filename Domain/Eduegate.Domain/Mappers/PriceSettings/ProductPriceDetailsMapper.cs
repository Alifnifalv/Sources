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
using Eduegate.Domain.Mappers.PriceSettings;
using Eduegate.Domain.Repository;

namespace Eduegate.Domain.Mappers
{
    public class ProductPriceDetailsMapper : IDTOEntityMapper<ProductPriceSettingDTO, ProductPriceListProductMap>
    {
        private CallContext _context;

        public static ProductPriceDetailsMapper Mapper(CallContext context)
        {
            var mapper = new ProductPriceDetailsMapper();
            mapper._context = context;
            return mapper;
        }
        public ProductPriceSettingDTO ToDTO(ProductPriceListProductMap entity)
        {
            if (entity.IsNotNull())
            {
                ProductPriceSettingDTO dto = new ProductPriceSettingDTO();
                dto.ProductQuntityLevelPrices = new List<Services.Contracts.PriceSettings.ProductPriceSettingQuantityDTO>();

                dto.ProductPriceListProductMapIID = entity.ProductPriceListProductMapIID;
                dto.ProductPriceListID = entity.ProductPriceListID;
                dto.ProductID = entity.ProductID;
                dto.PriceDescription = new PriceSettingsRepository().GetPriceDescription(Convert.ToInt32(entity.ProductPriceListID));
                dto.Price = entity.Price;
                dto.Discount = entity.DiscountPrice;
                dto.DiscountPercentage = entity.DiscountPercentage;
                dto.CreatedBy = entity.CreatedBy;
                dto.CreatedDate = entity.CreatedDate;
                dto.UpdatedBy = entity.UpdatedBy;
                dto.UpdatedDate = entity.UpdatedDate;
                //dto.TimeStamps = entity.TimeStamps.IsNotNull() ? Convert.ToBase64String(entity.TimeStamps) : string.Empty;

                if (entity.ProductPriceListProductQuantityMaps.IsNotNull() && entity.ProductPriceListProductQuantityMaps.Count > 0)
                {
                    foreach(var quantityEntity in entity.ProductPriceListProductQuantityMaps)
                    {
                        dto.ProductQuntityLevelPrices.Add(ProductPriceSettingQuantityMapper.Mapper(_context).ToDTO(quantityEntity));
                    }
                }

                return dto;
            }
            else
            {
                return new ProductPriceSettingDTO();
            }
        }

        public ProductPriceListProductMap ToEntity(ProductPriceSettingDTO dto)
        {
            if (dto.IsNotNull())
            {
                ProductPriceListProductMap entity = new ProductPriceListProductMap();
                entity.ProductPriceListProductQuantityMaps = new List<ProductPriceListProductQuantityMap>();

                entity.ProductPriceListProductMapIID = dto.ProductPriceListProductMapIID;
                entity.ProductPriceListID = dto.ProductPriceListID;
                entity.ProductID = dto.ProductID;
                entity.Price = dto.Price;
                entity.DiscountPrice = dto.Discount;
                entity.DiscountPercentage = dto.DiscountPercentage;
                entity.CreatedBy = dto.ProductPriceListProductMapIID > 0 ? dto.CreatedBy : (int)_context.LoginID;
                entity.CreatedDate = dto.ProductPriceListProductMapIID > 0 ? dto.CreatedDate : DateTime.Now;
                entity.UpdatedBy = dto.ProductPriceListProductMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy;
                entity.UpdatedDate = dto.ProductPriceListProductMapIID > 0 ? DateTime.Now : dto.UpdatedDate;
                //entity.TimeStamps = dto.TimeStamps.IsNotNull() ? Convert.FromBase64String(dto.TimeStamps) : null;

                if (dto.ProductQuntityLevelPrices.IsNotNull() && dto.ProductQuntityLevelPrices.Count > 0)
                {
                    foreach(var quantityDTO in dto.ProductQuntityLevelPrices)
                    {
                        entity.ProductPriceListProductQuantityMaps.Add(ProductPriceSettingQuantityMapper.Mapper(_context).ToEntity(quantityDTO));
                    }
                }

                return entity;
            }
            else
            {
                return new ProductPriceListProductMap();
            }
        }

    }
}
