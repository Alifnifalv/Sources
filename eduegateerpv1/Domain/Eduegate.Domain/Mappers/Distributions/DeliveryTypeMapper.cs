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
    public class DeliveryTypeMapper : IDTOEntityMapper<DeliverySettingDTO, DeliveryTypes1>
    {
        private CallContext _context;

        public static DeliveryTypeMapper Mapper(CallContext context)
        {
            var mapper = new DeliveryTypeMapper();
            mapper._context = context;
            return mapper;
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
                HasTimeSlot = entity.HasTimeSlot,
                CreatedBy = entity.CreatedBy,
                UpdatedBy = entity.UpdatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
                //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
            };

            dto.TimeSlots = DeliveryTypeTimeSlotMapper.Mapper(_context).ToDTO(entity.DeliveryTypeTimeSlotMaps.ToList());
            dto.ProductSKUCharges = ProductDeliveryTypeMapper.Mapper(_context).ToDTO(entity.ProductDeliveryTypeMaps.ToList());
            dto.CustomerGroupDeliveryCharges = CustomerGroupDeliveryTypeMapper.Mapper(_context).ToDTO(entity.CustomerGroupDeliveryTypeMaps.ToList());
            dto.AreaCharges = DeliveryTypeAllowedAreaMapper.Mapper(_context).ToDTO(entity.DeliveryTypeAllowedAreaMaps.ToList());
            return dto;
        }

        public DeliveryTypes1 ToEntity(DeliverySettingDTO dto)
        {
            var deliveryType = new DeliveryTypes1()
            {
                DeliveryTypeID = dto.DeliveryTypeID,
                DeliveryTypeName = dto.DeliveryTypeName,
                Description = dto.Description,
                StatusID = dto.StatusID,
                Priority = dto.Priority,
                Days = dto.Days,
                CreatedBy = dto.DeliveryTypeID > 0 ? dto.CreatedBy : (int)_context.LoginID,
                UpdatedBy = dto.DeliveryTypeID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = dto.DeliveryTypeID > 0 ? dto.CreatedDate : DateTime.Now,
                UpdatedDate = dto.DeliveryTypeID > 0 ? DateTime.Now : dto.UpdatedDate,
                //TimeStamps = dto.TimeStamps == null ? null : Convert.FromBase64String(dto.TimeStamps),
            };
            foreach (var item in dto.TimeSlots)
            {
                var deliveryTimeSlotMaps = new DeliveryTypeTimeSlotMap();
                deliveryTimeSlotMaps.DeliveryTypeID = dto.DeliveryTypeID;
                deliveryTimeSlotMaps.DeliveryTypeTimeSlotMapIID = item.DeliveryTypeTimeSlotMapIID;
                deliveryTimeSlotMaps.TimeFrom = DateTime.Parse(item.TimeFrom).TimeOfDay;
                deliveryTimeSlotMaps.TimeTo = DateTime.Parse(item.TimeTo).TimeOfDay;
                deliveryTimeSlotMaps.IsCutOff = item.IsCutOff;
                deliveryTimeSlotMaps.CutOffDays = item.CutOffDays.HasValue ? (byte)item.CutOffDays.Value : default(byte?);
                deliveryTimeSlotMaps.CutOffTime = !string.IsNullOrEmpty(item.CutOffTime) ? TimeSpan.Parse(item.CutOffTime) : default(TimeSpan?);
                deliveryTimeSlotMaps.CutOffHour = !string.IsNullOrEmpty(item.CutOffHour) ? byte.Parse(item.CutOffHour) : default(byte?);
                deliveryTimeSlotMaps.CutOffDisplayText = item.CutOffDisplayText;
                deliveryType.DeliveryTypeTimeSlotMaps.Add(deliveryTimeSlotMaps);
            }
            return deliveryType;
        }
    }
}
