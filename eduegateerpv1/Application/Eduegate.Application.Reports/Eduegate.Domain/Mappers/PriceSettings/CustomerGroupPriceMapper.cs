using System;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Extensions;
using Eduegate.Domain.Repository;

namespace Eduegate.Domain.Mappers
{
    public class CustomerGroupPriceMapper : IDTOEntityMapper<CustomerGroupPriceDTO, ProductPriceListCustomerGroupMap>
    {
        private CallContext _context;

        public static CustomerGroupPriceMapper Mapper(CallContext context)
        {
            var mapper = new CustomerGroupPriceMapper();
            mapper._context = context;
            return mapper;
        }

        public CustomerGroupPriceDTO ToDTO(ProductPriceListCustomerGroupMap entity)
        {
            if (entity.IsNotNull())
            {
                CustomerGroupPriceDTO dto = new CustomerGroupPriceDTO();

                dto.ProductPriceListCustomerGroupMapIID = entity.ProductPriceListCustomerGroupMapIID;
                dto.ProductPriceListID = entity.ProductPriceListID;
                dto.CustomerGroupID = entity.CustomerGroupID;
                dto.ProductID = entity.ProductID;
                dto.ProductSKUMapID = entity.ProductSKUMapID;
                dto.CategoryID = entity.CategoryID;
                dto.BrandID = entity.BrandID;
                dto.GroupName = new PriceSettingsRepository().GetCustomerGroupName(Convert.ToInt32(entity.CustomerGroupID));
                dto.PriceListName = new PriceSettingsRepository().GetPriceDescription(Convert.ToInt32(entity.ProductPriceListID));
                dto.Price = entity.Price;
                dto.Quantity = entity.Quantity;
                dto.Discount = entity.DiscountPrice;
                dto.DiscountPercentage = entity.DiscountPercentage;
                dto.CreatedBy = entity.CreatedBy;
                dto.CreatedDate = entity.CreatedDate;
                dto.UpdatedBy = entity.UpdatedBy;
                dto.UpdatedDate = entity.UpdatedDate;
                dto.TimeStamps = entity.TimeStamps.IsNotNull() ? Convert.ToBase64String(entity.TimeStamps) : null;

                return dto;
            }
            else
            {
                return new CustomerGroupPriceDTO();
            }
        }

        public ProductPriceListCustomerGroupMap ToEntity(CustomerGroupPriceDTO dto)
        {
            if (dto.IsNotNull())
            {
                ProductPriceListCustomerGroupMap entity = new ProductPriceListCustomerGroupMap();

                entity.ProductPriceListCustomerGroupMapIID = dto.ProductPriceListCustomerGroupMapIID;
                entity.ProductPriceListID = dto.ProductPriceListID;
                entity.CustomerGroupID = dto.CustomerGroupID;
                entity.ProductID = dto.ProductID;
                entity.ProductSKUMapID = dto.ProductSKUMapID;
                entity.CategoryID = dto.CategoryID;
                entity.BrandID = dto.BrandID;
                entity.Price = dto.Price;
                entity.Quantity = dto.Quantity;
                entity.DiscountPrice = dto.Discount;
                entity.DiscountPercentage = dto.DiscountPercentage;
                entity.CreatedBy = dto.ProductPriceListCustomerGroupMapIID > 0 ? dto.CreatedBy : (int)_context.LoginID;
                entity.CreatedDate = dto.ProductPriceListCustomerGroupMapIID > 0 ? dto.CreatedDate : DateTime.Now;
                entity.UpdatedBy = dto.ProductPriceListCustomerGroupMapIID > 0 ? (int)_context.LoginID :dto.UpdatedBy;
                entity.UpdatedDate = dto.ProductPriceListCustomerGroupMapIID > 0 ? DateTime.Now : dto.UpdatedDate;
                entity.TimeStamps = dto.TimeStamps.IsNotNull() ? Convert.FromBase64String(dto.TimeStamps) : null;

                return entity;
            }
            else
            {
                return new ProductPriceListCustomerGroupMap();
            }
        }
    }
}
