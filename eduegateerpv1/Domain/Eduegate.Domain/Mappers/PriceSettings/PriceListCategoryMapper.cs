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
    public class PriceListCategoryMapper : IDTOEntityMapper<ProductPriceCategoryDTO, ProductPriceListCategoryMap>
    {
        private CallContext _context;

        public static PriceListCategoryMapper Mapper(CallContext context)
        {
            var mapper = new PriceListCategoryMapper();
            mapper._context = context;
            return mapper;
        }

        public ProductPriceCategoryDTO ToDTO(ProductPriceListCategoryMap entity)
        {
            if (entity.IsNotNull())
            {
                ProductPriceCategoryDTO pcDTO = new ProductPriceCategoryDTO();

                pcDTO.ProductPriceListCategoryMapIID = entity.ProductPriceListCategoryMapIID;
                pcDTO.ProductPriceListID = entity.ProductPriceListID;
                pcDTO.CategoryID = entity.CategoryID;
                pcDTO.PriceDescription = new PriceSettingsRepository().GetPriceDescription(Convert.ToInt32(entity.ProductPriceListID));
                pcDTO.Price = entity.Price;
                pcDTO.DiscountPrice = entity.DiscountPrice;
                pcDTO.DiscountPercentage = entity.DiscountPercentage;
                pcDTO.CreatedBy = entity.CreatedBy;
                pcDTO.UpdatedBy = entity.UpdatedBy;
                pcDTO.CreatedDate = entity.CreatedDate;
                pcDTO.UpdatedDate = entity.UpdatedDate;
                //pcDTO.TimeStamps = entity.TimeStamps.IsNotNull() ? Convert.ToBase64String(entity.TimeStamps) : null;

                return pcDTO;
            }
            else
            {
                return new ProductPriceCategoryDTO();
            }
        }

        public ProductPriceListCategoryMap ToEntity(ProductPriceCategoryDTO dto)
        {
            if (dto.IsNotNull())
            {
                ProductPriceListCategoryMap pplcm = new ProductPriceListCategoryMap();

                pplcm.ProductPriceListCategoryMapIID = dto.ProductPriceListCategoryMapIID;
                pplcm.ProductPriceListID = dto.ProductPriceListID;
                pplcm.CategoryID = dto.CategoryID;
                pplcm.Price = dto.Price;
                pplcm.DiscountPrice = dto.DiscountPrice;
                pplcm.DiscountPercentage = dto.DiscountPercentage;
                pplcm.CreatedBy = dto.ProductPriceListCategoryMapIID > 0 ? dto.CreatedBy : (int)_context.LoginID;
                pplcm.UpdatedBy = dto.ProductPriceListCategoryMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy;
                pplcm.CreatedDate = dto.ProductPriceListCategoryMapIID > 0 ? dto.CreatedDate : DateTime.Now;
                pplcm.UpdatedDate = dto.ProductPriceListCategoryMapIID > 0 ? DateTime.Now : dto.UpdatedDate;
                //pplcm.TimeStamps = dto.TimeStamps.IsNotNull() ? Convert.FromBase64String(dto.TimeStamps) : null;

                return pplcm;
            }
            else
            {
                return new ProductPriceListCategoryMap();
            }
        }
    }
}
