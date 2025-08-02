using Eduegate.Domain.Entity;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Distributions;
using System.Collections.Generic;

namespace Eduegate.Domain.Mappers
{
    public class DeliveryTypeCutOffSlotMapper : IDTOEntityMapper<DeliveryTypeCutOffSlotDTO, DeliveryTypeCutOffSlot>
    {
        private CallContext _context;

        public static DeliveryTypeCutOffSlotMapper Mapper(CallContext context)
        {
            var mapper = new DeliveryTypeCutOffSlotMapper();
            mapper._context = context;
            return mapper;
        }

        public List<DeliveryTypeCutOffSlotDTO> ToDTO(List<DeliveryTypeCutOffSlot> entities)
        {
            var dtos = new List<DeliveryTypeCutOffSlotDTO>();
            foreach (var entity in entities)
            {
                dtos.Add(ToDTO(entity));
            }

            return dtos;
        }

        public DeliveryTypeCutOffSlotDTO ToDTO(DeliveryTypeCutOffSlot x)
        {
            return new DeliveryTypeCutOffSlotDTO()
            {
                DeliveryTypeID = x.DeliveryTypeID,
                DeliveryTypeCutOffSlotID = x.DeliveryTypeCutOffSlotID,
                OccuranceDate = x.OccuranceDate,
                OccuranceDayID = x.OccuranceDayID,
                OccurrenceTypeID = x.OccurrenceTypeID,
                TimeSlotID = x.TimeSlotID,
                TimeFromValue = x.TimeFrom,
                TimeToValue = x.TimeTo,
                TimeFrom = x.TimeFrom.HasValue ? x.TimeFrom.Value.ToLongTimeString() : null,
                TimeTo = x.TimeTo.HasValue ? x.TimeTo.Value.ToLongTimeString() : null,
                WarningMessage = x.WarningMessage,
                TooltipMessage = x.TooltipMessage
            };
        }

        public DeliveryTypeCutOffSlot ToEntity(DeliveryTypeCutOffSlotDTO dto)
        {
            throw new System.NotImplementedException();
        }
    }
}
