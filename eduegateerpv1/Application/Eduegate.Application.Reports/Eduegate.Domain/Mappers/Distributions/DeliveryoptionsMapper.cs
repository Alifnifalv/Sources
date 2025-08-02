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
    public class DeliveryOptionsMapper : IDTOEntityMapper<DeliverySettingDTO, DeliveryTypes1>
    { 
        private CallContext _context;

        public static DeliveryOptionsMapper Mapper(CallContext context)
        {
            var mapper = new DeliveryOptionsMapper();
            mapper._context = context;
            return mapper;
        }
        public System.Collections.Generic.List<DeliverySettingDTO> ToDTO(List<DeliveryTypes1> entities)
        {
            var deliveryOptions = new List<DeliverySettingDTO>();

            foreach (var entity in entities)
            {
                deliveryOptions.Add(ToDTO(entity));
            }

            return deliveryOptions; 
        }

        public DeliverySettingDTO ToDTO(DeliveryTypes1 entity)
        {
            var dto = new DeliverySettingDTO()
            {
                DeliveryTypeID = entity.DeliveryTypeID,
                DeliveryTypeName = entity.DeliveryTypeName,
                Description = entity.Description,
                StatusID = entity.StatusID,
                Days = entity.Days,
                Priority = entity.Priority,
                CreatedBy = entity.CreatedBy,
                UpdatedBy = entity.UpdatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
                TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
            };

            return dto;
        }

        public DeliveryTypes1 ToEntity(DeliverySettingDTO dto)
        {
            return new DeliveryTypes1()
            {

            };
        }    
    }
}
