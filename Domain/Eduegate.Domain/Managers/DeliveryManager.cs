
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Handlers;
using Eduegate.Domain.Helpers;
using Eduegate.Domain.Mappers.Distributions;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Services.Contracts.Distributions;
using Eduegate.Services.Contracts.Eduegates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Domain.Managers
{
    public class DeliveryManager
    {
        CallContext _callContext;
        ShoppingCartHelper _cartHelpers;
        DeliveryHandler _deliveryHandler;
        CacheDataHandler _cacheHelper;


        public static DeliveryManager Manager(CallContext callContext)
        {
            var manager = new DeliveryManager();
            manager._callContext = callContext;
            manager._cartHelpers = ShoppingCartHelper.Helper(callContext);
            manager._deliveryHandler = DeliveryHandler.Handler(callContext);
            manager._cacheHelper = CacheDataHandler.Handler(callContext);
            return manager;
        }

        public List<DeliverySettingDTO> GetAllDeliveryTypes(CartDTO cartDTO = null)
        {
            var activeDeliveryTypes = _deliveryHandler.GetAllDeliveryTypes();

            var cutOffSlots = _deliveryHandler.GetCutOffDeliveryTimeSlots();

            var defaultSlots = _deliveryHandler.GetDefaultDeliverySlots();

            var isTimeslotEnabled = _cartHelpers.GetSettingValue<bool>("ENABLETIMESLOTS", (long)this._callContext.CompanyID, false);

            if (isTimeslotEnabled)
            {
                var slotCapacityEnabled = _cartHelpers.GetSettingValue<bool>("ENABLESLOTCAPACITY", (long)this._callContext.CompanyID, true);

                foreach (var type in activeDeliveryTypes)
                {
                    //if (!type.HasTimeSlot.HasValue || type.HasTimeSlot.Value)
                    {
                        var occupliedSlots = slotCapacityEnabled ? new ReferenceDataRepository()
                           .GetOccupiedTimeSlots(type.DeliveryTypeID == 10 ? DateTime.Now
                           : type.DeliveryTypeID == 3 ? DateTime.Now.AddDays(1) : (DateTime?)null)
                            : new List<DeliveryTypeTimeSlotMap>();

                        var cutOffSlotsByDelivery = cutOffSlots.Where(x => !x.DeliveryTypeID.HasValue ||
                            x.DeliveryTypeID == type.DeliveryTypeID);
                        type.TimeSlots = new List<DeliveryTimeSlotDTO>();

                        foreach (var slot in defaultSlots)
                        {
                            if (type.DeliveryTypeID == 10)
                            {
                                if (slot.TimeFrom.HasValue && slot.TimeFrom < DateTime.Now.TimeOfDay)
                                {
                                    continue;
                                }
                            }

                            var occupiedSlot = occupliedSlots
                                .Where(x => x.DeliveryTypeID == type.DeliveryTypeID &&
                                 x.DeliveryTypeTimeSlotMapIID == slot.DeliveryTypeTimeSlotMapIID)
                                        .FirstOrDefault();

                            if (occupiedSlot != null)
                            {
                                if (slot.NoOfCutOffOrder.HasValue
                                    && slot.NoOfCutOffOrder >= occupiedSlot.NoOfCutOffOrder)
                                {
                                    continue;
                                }
                            }

                            //if(cutOffSlotsByDelivery != null && cutOffSlotsByDelivery.Count() > 0)
                            //{
                            //    if (cutOffSlotsByDelivery.Any(x => 
                            //        x.OccurrenceTypeID == 1  &&
                            //        x.TimeSlotID == slot.DeliveryTypeTimeSlotMapIID
                            //         && (x.DeliveryTypeID == slot.DeliveryTypeID 
                            //         || !slot.DeliveryTypeID.HasValue)))
                            //    {
                            //        continue;
                            //    }
                            //}

                            type.TimeSlots.Add(DeliveryTypeTimeSlotMapper.Mapper(_callContext).ToDTO(slot));
                        }
                    }
                }

                activeDeliveryTypes = activeDeliveryTypes
                    .Where(x => !x.HasTimeSlot.HasValue || !x.HasTimeSlot.Value
                || x.TimeSlots.Count() > 0).ToList();
            }

            //activeDeliveryTypes = cartDTO.DeliveryDistance.HasValue ? activeDeliveryTypes.Where(x => !x.DistanceStartRange.HasValue ||
            //    (x.DistanceStartRange.HasValue && cartDTO.DeliveryDistance.Value >= x.DistanceStartRange.Value
            //    && cartDTO.DeliveryDistance.Value <= x.DistanceEndRange.Value)).ToList() : activeDeliveryTypes;

            // check the current time vs cutoff time
            foreach (var slot in cutOffSlots
                .Where(x => x.TimeFromValue.HasValue && x.TimeToValue.HasValue))
            {
                var startDateTime = DateTime.Parse(DateTime.Now.ToLongDateString() + " " + slot.TimeFromValue.Value.ToLongTimeString());
                var endDateTime = DateTime.Parse(DateTime.Now.ToLongDateString() + " " + slot.TimeToValue.Value.ToLongTimeString());

                //day
                if (slot.OccurrenceTypeID == 1 && (byte)DateTime.Now.DayOfWeek != slot.OccuranceDayID)
                {
                    continue;
                }

                //date
                if (slot.OccurrenceTypeID == 2 && DateTime.Now.Date != slot.OccuranceDate.Value)
                {
                    continue;
                }

                if (_deliveryHandler.TimeBetween(DateTime.Now, slot.TimeFromValue.Value.TimeOfDay, slot.TimeToValue.Value.TimeOfDay))
                {
                    var deliveryType = activeDeliveryTypes.FirstOrDefault(x => x.DeliveryTypeID == slot.DeliveryTypeID);
                    deliveryType.NotAvailable = true;
                    deliveryType.WarningMessage = slot.WarningMessage;
                    deliveryType.TooltipMessage = slot.TooltipMessage;
                }
            }

            return activeDeliveryTypes;
        }

        public List<DeliveryTypeCutOffSlotDTO> GetCutOffDeliveryTimeSlots()
        {
            return _cacheHelper.GetCutOffDeliveryTimeSlots();
        }

        public List<DeliveryTimeSlotDTO> GetDefaultDeliverySlots()
        {
            return _cacheHelper.GetDefaultDeliverySlots();
        }
    }
}
