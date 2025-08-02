using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Mappers;
using Eduegate.Domain.Mappers.Distributions;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Services.Contracts.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Domain.Handlers
{
    public class DeliveryHandler
    {
        CallContext _callContext;
        private static ShoppingCartRepository shoppingCartRepository = new ShoppingCartRepository();


        private DeliveryHandler()
        {

        }

        public static DeliveryHandler Handler(CallContext _callContext)
        {
            var handler = new DeliveryHandler();
            handler._callContext = _callContext;
            return handler;
        }

        public OrderContactMap SaveOrderContactMap(OrderContactMap dtoOrderContactMap)
        {
            return new Repository.OrderRepository().SaveOrderContactMap(dtoOrderContactMap);
        }

        public List<DeliverySettingDTO> GetAllDeliveryTypes()
        {
            var activeDeliveryTypes = Framework.CacheManager.MemCacheManager<List<DeliverySettingDTO>>
                .Get("GETACTIVEDELIVERYSETTINGS");

            var cartItemsList = shoppingCartRepository.GetAllCategoriesDetailsWithItems(
                Convert.ToInt64(_callContext.UserId).ToString(),
                (int)ShoppingCartStatus.InProcess,
                null);

            if (activeDeliveryTypes == null || activeDeliveryTypes.Count == 0)
            {
                var tempList = new List<DeliverySettingDTO>();

                foreach (var cartItems in cartItemsList)
                {
                    var deliveryTypes = new Repository.ReferenceDataRepository()
                        .GetActiveDeliverySettings(cartItems?.CategoryIID);

                    foreach (var deliveryType in deliveryTypes)
                    {
                        var data = DeliveryTypeMapper.Mapper(this._callContext).ToDTO(deliveryType);
                        tempList.Add(data);
                    }
                }

                // Distinct by DeliverySettingID (or another unique identifier)
                activeDeliveryTypes = tempList
                    .GroupBy(x => x.DeliveryTypeID) // Assuming DeliverySettingID is the unique identifier
                    .Select(g => g.First())
                    .ToList();
            }

            return activeDeliveryTypes;
        }

        public List<DeliveryTypeCutOffSlotDTO> GetCutOffDeliveryTimeSlots()
        {
            var cutOffSlots = Framework.CacheManager.MemCacheManager<List<DeliveryTypeCutOffSlotDTO>>
                .Get("GETCUTOFFDELIVERYTIMESLOTS");

            if (cutOffSlots == null)
            {
                cutOffSlots = DeliveryTypeCutOffSlotMapper.Mapper(_callContext)
                    .ToDTO(new Repository.ReferenceDataRepository().GetCutOffDeliveryTimeSlots());
                Framework.CacheManager.MemCacheManager<List<DeliveryTypeCutOffSlotDTO>>
                    .Add(cutOffSlots, "GETCUTOFFDELIVERYTIMESLOTS");
            }

            return cutOffSlots;
        }

        public List<DeliveryTypeTimeSlotDTO> GetDefaultDeliverySlots()
        {
            var defaultSlots = Framework.CacheManager.MemCacheManager<List<DeliveryTypeTimeSlotDTO>>
                .Get("GETDEFAULTDELIVERYSLOTS");

            if (defaultSlots == null)
            {
                defaultSlots = DeliveryTypeTimeSlotMapMapper.Mapper(this._callContext)
                .ToDTO(new Repository.ReferenceDataRepository().GetDefaultDeliverySlots());
                Framework.CacheManager.MemCacheManager<List<DeliveryTypeTimeSlotDTO>>
                    .Add(defaultSlots, "GETDEFAULTDELIVERYSLOTS");
            }

            return defaultSlots;
        }

        public bool TimeBetween(DateTime datetime, TimeSpan start, TimeSpan end)
        {
            // convert datetime to a TimeSpan
            TimeSpan now = datetime.TimeOfDay;
            // see if start comes before end
            if (start < end)
                return start <= now && now <= end;
            // start is after end, so do the inverse comparison
            return !(end < now && now < start);
        }
    }
}

