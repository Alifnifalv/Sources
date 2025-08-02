using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Distributions;
using Eduegate.Framework.Extensions;

namespace Eduegate.Domain.Mappers.Distributions
{
    public class ProductDeliveryTypeMapper : IDTOEntityMapper<ProductSKUDeliveryTypeChargeDTO, ProductDeliveryTypeMap>
    {
        private CallContext _context;

        public static ProductDeliveryTypeMapper Mapper(CallContext context)
        {
            var mapper = new ProductDeliveryTypeMapper();
            mapper._context = context;
            return mapper;
        }

        public List<ProductSKUDeliveryTypeChargeDTO> ToDTO(List<ProductDeliveryTypeMap> entities)
        {
            var dtos = new List<ProductSKUDeliveryTypeChargeDTO>();

            foreach (var entity in entities)
            {
                dtos.Add(ToDTO(entity));
            }

            return dtos;
        }

        public ProductSKUDeliveryTypeChargeDTO ToDTO(ProductDeliveryTypeMap entity)
        {
            return new ProductSKUDeliveryTypeChargeDTO()
            {
                ProductDeliveryTypeMapIID = entity.ProductDeliveryTypeMapIID,
                DeliveryCharge = entity.DeliveryCharge,
                DeliveryChargePercentage = entity.DeliveryChargePercentage,
                DeliveryTypeID = entity.DeliveryTypeID.IsNotNull() ? entity.DeliveryTypeID.Value : 0,
                ProductSKUMapID = entity.ProductSKUMapID.IsNotNull() ? entity.ProductSKUMapID.Value : 0,
                ProductID = entity.ProductID,
                CreatedBy = entity.CreatedBy,
                UpdatedBy = entity.UpdatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
                //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
            };
        }

        public ProductDeliveryTypeMap ToEntity(ProductSKUDeliveryTypeChargeDTO dto)
        {
            return new ProductDeliveryTypeMap() {
                ProductDeliveryTypeMapIID = dto.ProductDeliveryTypeMapIID,
                DeliveryCharge = dto.DeliveryCharge,
                DeliveryChargePercentage = dto.DeliveryChargePercentage,
                DeliveryTypeID = dto.DeliveryTypeID,
                ProductSKUMapID = dto.ProductSKUMapID,
                ProductID = dto.ProductID,
                CreatedBy = dto.CreatedBy,
                UpdatedBy = dto.UpdatedBy,
                CreatedDate = dto.CreatedDate,
                UpdatedDate = dto.UpdatedDate,
                //TimeStamps = dto.TimeStamps == null ? null : Convert.FromBase64String(dto.TimeStamps),
            };
        }
    }
}
