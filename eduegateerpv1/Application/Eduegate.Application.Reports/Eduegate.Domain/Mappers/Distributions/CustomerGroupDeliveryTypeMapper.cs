using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Distributions;

namespace Eduegate.Domain.Mappers.Distributions
{
    public class CustomerGroupDeliveryTypeMapper : IDTOEntityMapper<CustomerGroupDeliveryChargeDTO, CustomerGroupDeliveryTypeMap>
    {
        private CallContext _context;

        public static CustomerGroupDeliveryTypeMapper Mapper(CallContext context)
        {
            var mapper = new CustomerGroupDeliveryTypeMapper();
            mapper._context = context;
            return mapper;
        }

        public List<CustomerGroupDeliveryChargeDTO> ToDTO(List<CustomerGroupDeliveryTypeMap> entities)
        {
            var dtos = new List<CustomerGroupDeliveryChargeDTO>();

            foreach (var entity in entities)
            {
                dtos.Add(ToDTO(entity));
            }

            return dtos;
        }

        public CustomerGroupDeliveryChargeDTO ToDTO(CustomerGroupDeliveryTypeMap entity)
        {
            return new CustomerGroupDeliveryChargeDTO()
            {
                CustomerGroupDeliveryTypeMapIID = entity.CustomerGroupDeliveryTypeMapIID,
                DeliveryCharge = entity.DeliveryCharge,
                DeliveryChargePercentage = entity.DeliveryChargePercentage,
                DeliveryTypeID = entity.DeliveryTypeID.Value,
                CustomerGroupID = entity.CustomerGroupID,
                IsDeliveryAvailable = entity.IsSelected,
                CreatedBy = entity.CreatedBy,
                UpdatedBy = entity.UpdatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
                TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                CartTotalFrom = entity.CartTotalFrom,
                CartTotalTo = entity.CartTotalTo
            };
        }
        public CustomerGroupDeliveryTypeMap ToEntity(CustomerGroupDeliveryChargeDTO dto)
        {
            return new CustomerGroupDeliveryTypeMap()
            {
                CustomerGroupDeliveryTypeMapIID = dto.CustomerGroupDeliveryTypeMapIID,
                DeliveryCharge = dto.DeliveryCharge,
                DeliveryChargePercentage = dto.DeliveryChargePercentage,
                DeliveryTypeID = dto.DeliveryTypeID,
                CustomerGroupID = dto.CustomerGroupID,
                IsSelected = dto.IsDeliveryAvailable,
                CreatedBy = dto.CustomerGroupDeliveryTypeMapIID > 0 ? dto.CreatedBy : (int)_context.LoginID,
                UpdatedBy = dto.CustomerGroupDeliveryTypeMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = dto.CustomerGroupDeliveryTypeMapIID > 0 ? dto.CreatedDate : DateTime.Now,
                UpdatedDate = dto.CustomerGroupDeliveryTypeMapIID > 0 ? DateTime.Now : dto.UpdatedDate,
                CartTotalFrom = dto.CartTotalFrom,
                CartTotalTo = dto.CartTotalTo,
                TimeStamps = dto.TimeStamps == null ? null : Convert.FromBase64String(dto.TimeStamps),
            };
        }     
    }
}
