using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Domain.Mappers
{
    public class ProductPriceMapper : IDTOEntityMapper<ProductPriceDTO, ProductPriceList>
    {
        private CallContext _context;

        public static ProductPriceMapper Mapper(CallContext context)
        {
            var mapper = new ProductPriceMapper();
            mapper._context = context;
            return mapper;
        }

        public ProductPriceDTO ToDTO(ProductPriceList entity)
        {
            if (entity != null)
            {
                return new ProductPriceDTO()
                {
                    ProductPriceListIID = entity.ProductPriceListIID,
                    PriceDescription = entity.PriceDescription,
                    Price = entity.Price,
                    PricePercentage = entity.PricePercentage,
                    StartDate = entity.StartDate,
                    EndDate = entity.EndDate,
                    ProductPriceListTypeID = entity.ProductPriceListTypeID,
                    ProductPriceListLevelID = entity.ProductPriceListLevelID,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedDate = entity.UpdatedDate,
                    //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                    CompanyID = entity.CompanyID.IsNotNull() ? entity.CompanyID : _context.CompanyID,
                };
            }
            else return new ProductPriceDTO();
        }


        public ProductPriceList ToEntity(ProductPriceDTO dto)
        {
            if (dto != null)
            {
                return new ProductPriceList()
                {
                    ProductPriceListIID = dto.ProductPriceListIID,
                    PriceDescription = dto.PriceDescription,
                    Price = dto.Price,
                    PricePercentage = dto.PricePercentage,
                    StartDate = dto.StartDate,
                    EndDate = dto.EndDate,
                    ProductPriceListTypeID = dto.ProductPriceListTypeID,
                    ProductPriceListLevelID = dto.ProductPriceListLevelID,
                    CreatedDate = dto.ProductPriceListIID == default(long) ? DateTime.Now : dto.CreatedDate,
                    CreatedBy = dto.ProductPriceListIID == default(long) ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedDate = dto.ProductPriceListIID == default(long) ? DateTime.Now : dto.UpdatedDate,
                    UpdatedBy = dto.ProductPriceListIID == default(long) ? (int)_context.LoginID : dto.UpdatedBy,
                    //TimeStamps = dto.TimeStamps.IsNotNull() ? Convert.FromBase64String(dto.TimeStamps) : null,
                    CompanyID = dto.CompanyID.IsNotNull() ? dto.CompanyID : _context.CompanyID,
                };
            }
            else
            {
                return new ProductPriceList();
            }
        }
    }
}
