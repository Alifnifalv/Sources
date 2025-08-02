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
    public class DeliveryTypeAllowedZoneMapper : IDTOEntityMapper<ZoneDeliveryChargeDTO, DeliveryTypeAllowedZoneMap>
    {
        private CallContext _context;

        public static DeliveryTypeAllowedZoneMapper Mapper(CallContext context)
        {
            var mapper = new DeliveryTypeAllowedZoneMapper();
            mapper._context = context;
            return mapper;
        }

        public List<ZoneDeliveryChargeDTO> ToDTO(List<DeliveryTypeAllowedZoneMap> entities)
        {
            var zoneDtos = new List<ZoneDeliveryChargeDTO>();

            foreach (var entity in entities)
            {
                zoneDtos.Add(ToDTO(entity));
            }

            return zoneDtos;
        }

        public ZoneDeliveryChargeDTO ToDTO(DeliveryTypeAllowedZoneMap entity)
        {
            return new ZoneDeliveryChargeDTO()
            {
                ZoneDeliveryTypeMapIID = entity.ZoneDeliveryTypeMapIID,
                ZoneID = entity.ZoneID.IsNotNull() ? entity.ZoneID : (short?)null,
                CountryID = entity.CountryID,
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
                //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
            };
        }

        public DeliveryTypeAllowedZoneMap ToEntity(ZoneDeliveryChargeDTO dto)
        {
            return new DeliveryTypeAllowedZoneMap()
            {
                ZoneDeliveryTypeMapIID = dto.ZoneDeliveryTypeMapIID,
                ZoneID = dto.ZoneID,
                CountryID = dto.CountryID,
                CartTotalFrom = dto.CartTotalFrom,
                CartTotalTo = dto.CartTotalTo,
                DeliveryTypeID = dto.DeliveryTypeID,
                DeliveryCharge = dto.DeliveryCharge,
                DeliveryChargePercentage = dto.DeliveryChargePercentage,
                IsSelected = dto.IsDeliveryAvailable,
                CreatedBy = dto.ZoneDeliveryTypeMapIID > 0 ? dto.CreatedBy : (int)_context.LoginID,
                CreatedDate = dto.ZoneDeliveryTypeMapIID > 0 ? dto.CreatedDate : DateTime.Now,
                UpdatedBy = dto.ZoneDeliveryTypeMapIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                UpdatedDate = dto.ZoneDeliveryTypeMapIID > 0 ? DateTime.Now : dto.UpdatedDate,
                //TimeStamps = dto.TimeStamps == null? null:Convert.FromBase64String(dto.TimeStamps),
            };
        }
    }
}
