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
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Domain.Repository;

namespace Eduegate.Domain.Mappers.PriceSettings
{
    public class PriceListBrandMapper : IDTOEntityMapper<ProductPriceBrandDTO, ProductPriceListBrandMap>
    {
        private CallContext _context;

        public static PriceListBrandMapper Mapper(CallContext context)
        {
            var mapper = new PriceListBrandMapper();
            mapper._context = context;
            return mapper;
        }

        public ProductPriceBrandDTO ToDTO(ProductPriceListBrandMap entity)
        {
            if (entity.IsNotNull())
            {
                var pbDTO = new ProductPriceBrandDTO();

                pbDTO.ProductPriceListBrandMapIID = entity.ProductPriceListBrandMapIID;
                pbDTO.ProductPriceListID = entity.ProductPriceListID;
                pbDTO.BrandID = entity.BrandID;
                pbDTO.PriceDescription = new PriceSettingsRepository().GetPriceDescription(Convert.ToInt32(entity.ProductPriceListID));
                pbDTO.Price = entity.Price;
                pbDTO.DiscountPrice = entity.DiscountPrice;
                pbDTO.DiscountPercentage = entity.DiscountPercentage;
                pbDTO.CreatedBy = entity.CreatedBy;
                pbDTO.UpdatedBy = entity.UpdatedBy;
                pbDTO.CreatedDate = entity.CreatedDate;
                pbDTO.UpdatedDate = entity.UpdatedDate;
                //pbDTO.TimeStamps = entity.TimeStamps.IsNotNull() ? Convert.ToBase64String(entity.TimeStamps) : null;

                return pbDTO;
            }
            else
            {
                return new ProductPriceBrandDTO();
            }
        }

        public ProductPriceListBrandMap ToEntity(ProductPriceBrandDTO dto)
        {
            if (dto.IsNotNull())
            {
                ProductPriceListBrandMap pplbm = new ProductPriceListBrandMap();

                pplbm.ProductPriceListBrandMapIID = dto.ProductPriceListBrandMapIID;
                pplbm.ProductPriceListID = dto.ProductPriceListID;
                pplbm.BrandID = dto.BrandID;
                pplbm.Price = dto.Price;
                pplbm.DiscountPrice = dto.DiscountPrice;
                pplbm.DiscountPercentage = dto.DiscountPercentage;
                pplbm.CreatedBy = dto.ProductPriceListBrandMapIID > 0 ? dto.CreatedBy : (int)_context.LoginID;
                pplbm.UpdatedBy = dto.ProductPriceListBrandMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy;
                pplbm.CreatedDate = dto.ProductPriceListBrandMapIID > 0 ? dto.CreatedDate : DateTime.Now;
                pplbm.UpdatedDate = dto.ProductPriceListBrandMapIID > 0 ? DateTime.Now : dto.UpdatedDate;
                //pplbm.TimeStamps = dto.TimeStamps.IsNotNull() ? Convert.FromBase64String(dto.TimeStamps) : null;

                return pplbm;
            }
            else
            {
                return new ProductPriceListBrandMap();
            }
        }
    }
}
