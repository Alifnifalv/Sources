using Eduegate.Services.Contracts.Distributions;
using Eduegate.Framework.Interfaces;
using Eduegate.Framework;
using System.Collections.Generic;
using System;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Domain.Entity.Models;

namespace Eduegate.Domain.Mappers
{
    public class DeliveryTypeTimeSlotMapMapper : IDTOEntityMapper<DeliveryTypeTimeSlotDTO, DeliveryTypeTimeSlotMap>
    {
        private CallContext _context;

        public static DeliveryTypeTimeSlotMapMapper Mapper(CallContext context)
        {
            var mapper = new DeliveryTypeTimeSlotMapMapper();
            mapper._context = context;
            return mapper;
        }

        public List<DeliveryTypeTimeSlotDTO> ToDTO(List<DeliveryTypeTimeSlotMap> entities)
        {
            var dtos = new List<DeliveryTypeTimeSlotDTO>();
            foreach (var entity in entities)
            {
                dtos.Add(ToDTO(entity));
            }

            return dtos;
        }

        public DeliveryTypeTimeSlotDTO ToDTO(DeliveryTypeTimeSlotMap entity)
        {
            return new DeliveryTypeTimeSlotDTO()
            {
                CutOffDays = entity.CutOffDays,
                IsCutOff = entity.IsCutOff,
                TimeTo = entity.TimeTo,
                TimeFrom = entity.TimeFrom,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                CutOffDisplayText = entity.CutOffDisplayText,
                CutOffHour = entity.CutOffHour,
                CutOffTime = entity.CutOffTime,
                DeliveryTypeID = entity.DeliveryTypeID,
                DeliveryTypeTimeSlotMapIID = entity.DeliveryTypeTimeSlotMapIID,
                NoOfCutOffOrder = entity.NoOfCutOffOrder,
                SlotName = entity.SlotName,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate
            };
        }

        public DeliveryTypeTimeSlotMap ToEntity(DeliveryTypeTimeSlotDTO dto)
        {
            throw new System.NotImplementedException();
        }

        public static List<DeliveryTypeTimeSlotMapCultureDTO> ToDtoConfigCulureList(DeliveryTypeTimeSlotMap entity, DateTime deliveryDate)
        {
            var deliveryConfigCultureDTOList = new List<DeliveryTypeTimeSlotMapCultureDTO>();
            var deliveryConfigCultureDTO = new DeliveryTypeTimeSlotMapCultureDTO()
            {
                CultureID = 1,
                DeliveryTypeTimeSlotMapID = entity.DeliveryTypeTimeSlotMapIID,
                CutOffDisplayText = string.Format(entity.CutOffDisplayText, deliveryDate.ToString("dd-MMM-yyyy"), deliveryDate.ToString("hh:mm tt"))
            };
            deliveryConfigCultureDTOList.Add(deliveryConfigCultureDTO);
            foreach (var item in entity.DeliveryTypeTimeSlotMapsCultures)
            {
                var deliveryConfigCultureDTOItem = new DeliveryTypeTimeSlotMapCultureDTO()
                {
                    CultureID = item.CultureID,
                    DeliveryTypeTimeSlotMapID = item.DeliveryTypeTimeSlotMapID,
                    CutOffDisplayText = string.Format(item.CutOffDisplayText, deliveryDate.ToString("dd-MMM-yyyy"), deliveryDate.ToString("hh:mm tt"))
                };
                deliveryConfigCultureDTOList.Add(deliveryConfigCultureDTOItem);
            }
            return deliveryConfigCultureDTOList;
        }
    }
}
