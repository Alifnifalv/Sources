using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts;
using Eduegate.Domain.Entity.Models.ValueObjects;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Domain.Entity.Models;

namespace Eduegate.Domain.Mappers
{
    public class DeliveryTypeCheckMappers : IDTOEntityMapper<DeliveryCheckDTO, DeliveryTypeCheck>
    {
        public static DeliveryTypeCheckMappers Mapper()
        {
            var mapper = new DeliveryTypeCheckMappers();
            return mapper;
        }

        public DeliveryCheckDTO ToDTO(DeliveryTypeCheck entity)
        {
            if (entity != null)
            {
                var dto = new DeliveryCheckDTO()
                {
                   SKUID = entity.SKUID,
                   DeliveryCount = entity.DeliveryCount
                };

                return dto;
            }
            else
                return null;
        }

        public DeliveryTypeCheck ToEntity(DeliveryCheckDTO dto)
        {
            return null;
        }

        public static List<DeliveryTypeTimeSlotMapCultureDTO> ToDtoConfigCulureList(DeliveryTypeTimeSlotMap entity, DateTime deliveryDate)
        {
            var deliveryConfigCultureDTOList = new List<DeliveryTypeTimeSlotMapCultureDTO>();
            var deliveryConfigCultureDTO = new DeliveryTypeTimeSlotMapCultureDTO()
            {
                CultureID = 1,
                DeliveryTypeTimeSlotMapID = entity.DeliveryTypeTimeSlotMapIID,
                CutOffDisplayText = string.IsNullOrEmpty(entity.CutOffDisplayText) ? null : string.Format(entity.CutOffDisplayText, deliveryDate.ToString("dd-MMM-yyyy"), deliveryDate.ToString("hh:mm tt"))
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
