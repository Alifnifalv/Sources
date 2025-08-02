using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.CustomEntity;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Extensions;

namespace Eduegate.Domain.Mappers
{
    public class ProductPriceSKUMapper : IDTOEntityMapper<ProductPriceSKUDTO, ProductPriceSKU>
    {
        public static ProductPriceSKUMapper Mapper { get { return new ProductPriceSKUMapper(); } }
        public ProductPriceSKUDTO ToDTO(ProductPriceSKU entity)
        {
            if (entity != null)
            {
                return new ProductPriceSKUDTO()
                {
                    ProductPriceListItemMapIID = entity.ProductPriceListItemMapIID,
                    ProductPriceListID = entity.ProductPriceListID,
                    ProductSKUID = entity.ProductSKUID,
                    UnitGroundID = entity.UnitGroundID,
                    SellingQuantityLimit = entity.SellingQuantityLimit,
                    Amount = entity.Amount,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    TimeStamps = entity.TimeStamps.IsNotNull() ? Convert.ToBase64String(entity.TimeStamps) : null,
                    ProductPriceSKU = entity.SKU,
                    ProductName = entity.ProductName,
                    SortOrder = entity.SortOrder,
                    PartNumber = entity.PartNumber,
                    Barcode=entity.Barcode,
                    PricePercentage = entity.PricePercentage
                };
            }
            else return new ProductPriceSKUDTO();
        }

        public ProductPriceSKU ToEntity(ProductPriceSKUDTO dto)
        {
            if (dto != null)
            {
                return new ProductPriceSKU()
                {
                    ProductPriceListItemMapIID = dto.ProductPriceListItemMapIID,
                    ProductPriceListID = dto.ProductPriceListID,
                    ProductSKUID = dto.ProductSKUID,
                    UnitGroundID = dto.UnitGroundID,
                    SellingQuantityLimit = dto.SellingQuantityLimit,
                    Amount = dto.Amount,
                    CreatedBy = dto.CreatedBy,
                    UpdatedBy = dto.UpdatedBy,
                    CreatedDate = dto.CreatedDate,
                    UpdatedDate = dto.UpdatedDate,
                    TimeStamps = dto.TimeStamps.IsNotNull() ? Convert.FromBase64String(dto.TimeStamps) : null,
                    SKU= dto.ProductPriceSKU,
                    ProductName= dto.ProductName,
                    SortOrder = dto.SortOrder,
                    PartNumber = dto.PartNumber,
                    Barcode = dto.Barcode,
                    PricePercentage = dto.PricePercentage
                };
            }
            else return new ProductPriceSKU();
        }
    }
}
