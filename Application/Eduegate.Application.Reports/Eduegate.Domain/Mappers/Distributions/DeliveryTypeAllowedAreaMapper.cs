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
    public class DeliveryTypeAllowedAreaMapper : IDTOEntityMapper<AreaDeliveryChargeDTO, DeliveryTypeAllowedAreaMap>
    {
        private CallContext _context;

        public static DeliveryTypeAllowedAreaMapper Mapper(CallContext context)
        {
            var mapper = new DeliveryTypeAllowedAreaMapper();
            mapper._context = context;
            return mapper;
        }

        public List<AreaDeliveryChargeDTO> ToDTO(List<DeliveryTypeAllowedAreaMap> entities)
        {
            var areaDtos = new List<AreaDeliveryChargeDTO>();

            foreach (var entity in entities)
            {
                areaDtos.Add(ToDTO(entity));
            }

            return areaDtos;
        }

        public AreaDeliveryChargeDTO ToDTO(DeliveryTypeAllowedAreaMap entity)
        {
            return new AreaDeliveryChargeDTO()
            {
                AreaDeliveryTypeMapIID = entity.AreaDeliveryTypeMapIID,
                AreaID = entity.AreaID.IsNotNull() ? entity.AreaID  : (int?)null,
                CartTotalFrom = entity.CartTotalFrom,
                CartTotalTo = entity.CartTotalTo,
                DeliveryTypeID = entity.DeliveryTypeID.IsNotNull() ? entity.DeliveryTypeID : (int?)null,
                DeliveryCharge = entity.DeliveryCharge,
                DeliveryChargePercentage = entity.DeliveryChargePercentage,
                IsDeliveryAvailable = entity.IsSelected,
                CreatedBy = entity.CreatedBy,
                UpdatedBy = entity.UpdatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
                TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
            };
        }

        public DeliveryTypeAllowedAreaMap ToEntity(AreaDeliveryChargeDTO dto)
        {
            return new DeliveryTypeAllowedAreaMap()
            {
                AreaDeliveryTypeMapIID = dto.AreaDeliveryTypeMapIID,
                AreaID = dto.AreaID,
                CartTotalFrom = dto.CartTotalFrom,
                CartTotalTo = dto.CartTotalTo,
                DeliveryTypeID = dto.DeliveryTypeID,
                DeliveryCharge = dto.DeliveryCharge,
                DeliveryChargePercentage = dto.DeliveryChargePercentage,
                IsSelected = dto.IsDeliveryAvailable,
                CreatedBy = dto.AreaDeliveryTypeMapIID > 0 ? dto.CreatedBy : (int)_context.LoginID,
                CreatedDate = dto.AreaDeliveryTypeMapIID > 0 ? dto.CreatedDate : DateTime.Now,
                UpdatedBy = dto.AreaDeliveryTypeMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                UpdatedDate = dto.AreaDeliveryTypeMapIID > 0 ? DateTime.Now : dto.UpdatedDate,
                TimeStamps = dto.TimeStamps == null ? null : Convert.FromBase64String(dto.TimeStamps),
            };
        }
    }
}
