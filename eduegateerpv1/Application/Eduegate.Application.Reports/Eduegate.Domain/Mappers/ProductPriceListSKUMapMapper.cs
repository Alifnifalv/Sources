using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts;

namespace Eduegate.Domain.Mappers
{
    public class ProductPriceListSKUMapMapper : IDTOEntityMapper<ProductPriceListSKUMapDTO, ProductPriceListSKUMap>
    {
        public static ProductPriceListSKUMapMapper Mapper()
        {
            var mapper = new ProductPriceListSKUMapMapper();
            return mapper;
        }
        public ProductPriceListSKUMapDTO ToDTO(ProductPriceListSKUMap entity)
        {
            if (entity.IsNotNull())
            {
                return new ProductPriceListSKUMapDTO()
                {
                    ProductPriceListItemMapIID = entity.ProductPriceListItemMapIID,
                    ProductPriceListID = entity.ProductPriceListID,
                    ProductSKUID = entity.ProductSKUID,
                    UnitGroundID = entity.UnitGroundID,
                    CustomerGroupID = entity.CustomerGroupID,
                    SellingQuantityLimit = entity.SellingQuantityLimit,
                    Amount = entity.Amount,
                    SortOrder = entity.SortOrder,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    //TimeStamps = entity.TimeStamps,
                    PricePercentage = entity.PricePercentage,
                    Price = entity.Price,
                    Discount = entity.Discount,
                    DiscountPercentage = entity.DiscountPercentage,
                    Cost = entity.Cost,
                    IsActive = entity.IsActive
                };
            }
            else
            {
                return new ProductPriceListSKUMapDTO();
            }
           
        }

        public ProductPriceListSKUMap ToEntity(ProductPriceListSKUMapDTO dto)
        {
            if (dto.IsNotNull())
            {
                return new ProductPriceListSKUMap()
                {
                    ProductPriceListItemMapIID = dto.ProductPriceListItemMapIID,
                    ProductPriceListID = dto.ProductPriceListID,
                    ProductSKUID = dto.ProductSKUID,
                    UnitGroundID = dto.UnitGroundID,
                    CustomerGroupID = dto.CustomerGroupID,
                    SellingQuantityLimit = dto.SellingQuantityLimit,
                    Amount = dto.Amount,
                    SortOrder = dto.SortOrder,
                    CreatedBy = dto.CreatedBy,
                    UpdatedBy = dto.UpdatedBy,
                    CreatedDate = dto.CreatedDate,
                    UpdatedDate = dto.UpdatedDate,
                    //TimeStamps = dto.TimeStamps,
                    PricePercentage = dto.PricePercentage,
                    Price = dto.Price,
                    Discount = dto.Discount,
                    DiscountPercentage = dto.DiscountPercentage,
                    Cost = dto.Cost,
                    IsActive = dto.IsActive
                }; 
            }
            else
            {
                return new ProductPriceListSKUMap();
            }
        }
    }
}
