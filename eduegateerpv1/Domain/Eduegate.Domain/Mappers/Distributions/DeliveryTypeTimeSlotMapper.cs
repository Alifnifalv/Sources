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
    public class DeliveryTypeTimeSlotMapper : IDTOEntityMapper<DeliveryTimeSlotDTO, DeliveryTypeTimeSlotMap>
    {
        private CallContext _context;

        public static DeliveryTypeTimeSlotMapper Mapper(CallContext context)
        {
            var mapper = new DeliveryTypeTimeSlotMapper();
            mapper._context = context;
            return mapper;
        }

        public List<DeliveryTimeSlotDTO> ToDTO(List<DeliveryTypeTimeSlotMap> entities)
        {
            var dtos = new List<DeliveryTimeSlotDTO>();
            foreach (var entity in entities)
            {
                dtos.Add(ToDTO(entity));
            }

            return dtos;
        }

        public DeliveryTimeSlotDTO ToDTO(DeliveryTypeTimeSlotMap entity)
        {
            var timeSlot = new DeliveryTimeSlotDTO()
            {
                DeliveryTypeTimeSlotMapIID = entity.DeliveryTypeTimeSlotMapIID,
                TimeFrom = entity.TimeFrom == null ? null : TimeSpan.Parse(entity.TimeFrom.ToString()) == null ? null : entity.TimeFrom.Value.ToString(),
                TimeTo = entity.TimeTo == null ? null : TimeSpan.Parse(entity.TimeTo.ToString()) == null ? null : entity.TimeTo.Value.ToString(),
                CreatedBy = entity.CreatedBy,
                UpdatedBy = entity.UpdatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
                //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                IsCutOff = entity.IsCutOff,
                CutOffDays = entity.CutOffDays,
                CutOffTime = entity.CutOffTime.ToString(),
                CutOffHour = entity.CutOffHour.ToString(),
                CutOffDisplayText = entity.CutOffDisplayText,
                SlotName = entity.SlotName,
            };

            if (string.IsNullOrEmpty(timeSlot.TimeFrom) && string.IsNullOrEmpty(timeSlot.TimeTo) && !string.IsNullOrEmpty(entity.SlotName))
            {
                var slotLanguage =
                    Eduegate.Globalization.ResourceHelper.GetValue(entity.SlotName, this._context.LanguageCode);
                timeSlot.TimeFrom = !string.IsNullOrEmpty(slotLanguage) ? slotLanguage : entity.SlotName;
            }

            if (string.IsNullOrEmpty(timeSlot.SlotName))
            {
                timeSlot.SlotName = timeSlot.TimeFrom + "-" + timeSlot.TimeTo;
            }

            return timeSlot;
        }

        public List<DeliveryTimeSlotDTO> ToDTO(List<DeliveryTypeTimeSlotDTO> entities)
        {
            var dtos = new List<DeliveryTimeSlotDTO>();
            foreach (var entity in entities)
            {
                dtos.Add(ToDTO(entity));
            }

            return dtos;
        }

        public DeliveryTimeSlotDTO ToDTO(DeliveryTypeTimeSlotDTO entity)
        {
            var timeSlot = new DeliveryTimeSlotDTO()
            {
                DeliveryTypeTimeSlotMapIID = entity.DeliveryTypeTimeSlotMapIID,
                TimeFrom = entity.TimeFrom == null ? null : TimeSpan.Parse(entity.TimeFrom.ToString()) == null ? null : entity.TimeFrom.Value.ToString(),
                TimeTo = entity.TimeTo == null ? null : TimeSpan.Parse(entity.TimeTo.ToString()) == null ? null : entity.TimeTo.Value.ToString(),
                CreatedBy = entity.CreatedBy,
                UpdatedBy = entity.UpdatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
                IsCutOff = entity.IsCutOff,
                CutOffDays = entity.CutOffDays,
                CutOffTime = entity.CutOffTime.ToString(),
                CutOffHour = entity.CutOffHour.ToString(),
                CutOffDisplayText = entity.CutOffDisplayText,
                SlotName = entity.SlotName,
            };

            if (string.IsNullOrEmpty(timeSlot.TimeFrom) && string.IsNullOrEmpty(timeSlot.TimeTo) && !string.IsNullOrEmpty(entity.SlotName))
            {
                var slotLanguage =
                    Eduegate.Globalization.ResourceHelper.GetValue(entity.SlotName, this._context.LanguageCode);
                timeSlot.TimeFrom = !string.IsNullOrEmpty(slotLanguage) ? slotLanguage : entity.SlotName;
            }

            if (string.IsNullOrEmpty(timeSlot.SlotName))
            {
                timeSlot.SlotName = timeSlot.TimeFrom + "-" + timeSlot.TimeTo;
            }

            return timeSlot;
        }

        public DeliveryTypeTimeSlotMap ToEntity(DeliveryTimeSlotDTO dto)
        {
            return new DeliveryTypeTimeSlotMap()
            {
                DeliveryTypeTimeSlotMapIID = dto.DeliveryTypeTimeSlotMapIID,
                TimeFrom = dto.TimeFrom == null ? (TimeSpan?)null : TimeSpan.Parse(dto.TimeFrom),
                TimeTo = dto.TimeTo == null ? (TimeSpan?)null : TimeSpan.Parse(dto.TimeTo),
                CreatedBy = dto.CreatedBy,
                UpdatedBy = dto.UpdatedBy,
                CreatedDate = dto.CreatedDate,
                UpdatedDate = dto.UpdatedDate,
                //TimeStamps = dto.TimeStamps == null ? null : Convert.FromBase64String(dto.TimeStamps),
            };
        }
    }
}
