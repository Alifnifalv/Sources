using Eduegate.Framework;
using Eduegate.Services.Contracts.Distributions;
using System.Collections.Generic;
using Eduegate.Domain.Mappers.Distributions;
using Eduegate.Domain.Mappers;

namespace Eduegate.Domain.Handlers
{
    public class CacheDataHandler
    {
        CallContext _callContext;
        //dbSkienShoppingCartContext _dbContext;

        public static CacheDataHandler Handler(CallContext _callContext)
        {
            var handler = new CacheDataHandler();
            handler._callContext = _callContext;
            //handler._dbContext = dbContext;
            return handler;
        }

        public void SetContext()
        {
        }

        public List<DeliveryTimeSlotDTO> GetDefaultDeliverySlots()
        {
            var defaultSlots = Framework.CacheManager.MemCacheManager<List<DeliveryTimeSlotDTO>>.Get("GET_DEFAULT_TIME_SLOT");

            if (defaultSlots == null)
            {
                var slots = new Eduegate.Domain.Repository.ReferenceDataRepository().GetDefaultDeliverySlots();
                defaultSlots = DeliveryTypeTimeSlotMapper.Mapper(_callContext).ToDTO(slots);
                Framework.CacheManager.MemCacheManager<List<DeliveryTimeSlotDTO>>
                   .Add(defaultSlots, "GET_DEFAULT_TIME_SLOT");
            }

            return defaultSlots;
        }

        public List<DeliveryTypeCutOffSlotDTO> GetCutOffDeliveryTimeSlots()
        {
            var defaultSlots = Framework.CacheManager.MemCacheManager<List<DeliveryTypeCutOffSlotDTO>>.Get("GET_CUTOFF_DELIVERY_TIMESLOTS");

            if (defaultSlots == null)
            {
                var slots = new Eduegate.Domain.Repository.ReferenceDataRepository().GetCutOffDeliveryTimeSlots();
                defaultSlots = DeliveryTypeCutOffSlotMapper.Mapper(_callContext).ToDTO(slots);
                Framework.CacheManager.MemCacheManager<List<DeliveryTypeCutOffSlotDTO>>
                   .Add(defaultSlots, "GET_CUTOFF_DELIVERY_TIMESLOTS");
            }

            return defaultSlots;
        }


        // Codes we Don't use (Got From TVM)
        #region
        //public async Task<LanguageDTO> GetLanguageCultureId(string languageCode)
        //{
        //    var languageDTO = Framework.CacheManager.MemCacheManager<LanguageDTO>.Get("LANGUAGE_" + languageCode);

        //    if (languageDTO != null)
        //    {
        //        var languageEntity = await new Eduegate.Domain.Repository.ShoppingCarts
        //            .ReferenceDataRepository(_dbContext).GetLanguageCultureId(languageCode);
        //        languageDTO = languageEntity == null ? null : new LanguageDTO()
        //        {
        //            CultureID = (byte)languageEntity.CultureID,
        //            LanguageID = languageEntity.LanguageID,
        //            LanguageCode = languageEntity.Culture.CultureCode,
        //            DisplayText = languageEntity.Language1,
        //        };

        //        Framework.CacheManager.MemCacheManager<LanguageDTO>.Add(languageDTO, "LANGUAGE_" + languageCode);
        //    }

        //    return languageDTO;
        //}

        //public async Task<decimal> GetExchangeRate(int? companyID, string forCurrencyCode)
        //{
        //    var exchangeRate = Framework.CacheManager.MemCacheManager<decimal?>.Get("EXCHANGERATE_" + (companyID.HasValue
        //            ? companyID.Value.ToString() : "0") + forCurrencyCode);

        //    if (!exchangeRate.HasValue)
        //    {
        //        exchangeRate = await new Eduegate.Domain.Repository.ShoppingCarts.ReferenceDataRepository(_dbContext)
        //            .GetExchangeRate(_callContext.CompanyID, _callContext.CurrencyCode);
        //        Framework.CacheManager.MemCacheManager<decimal?>.Add(exchangeRate, "EXCHANGERATE_" + (companyID.HasValue
        //            ? companyID.Value.ToString() : "0") + forCurrencyCode);
        //    }

        //    return exchangeRate.Value;
        //}

        //public async Task<DeliveryTypeTimeSlotMapDTO> GetDeliveryTimeSlot(int deliveryTypeID)
        //{
        //    var mapSlots = Framework.CacheManager
        //        .MemCacheManager<List<DeliveryTypeTimeSlotMapDTO>>.Get("DELIVERYTIMESLOT_" + deliveryTypeID.ToString());

        //    if (mapSlots == null)
        //    {
        //        var slots = await new Repository.ShoppingCarts.ShoppingCartRepository(_dbContext)
        //            .GetDeliveryTimeSlot(deliveryTypeID);
        //        mapSlots = slots.Select(x => new DeliveryTypeTimeSlotMapDTO()
        //        {
        //            CutOffDays = x.CutOffDays,
        //            CutOffTime = x.CutOffTime,
        //            CutOffDisplayText = x.CutOffDisplayText,
        //            CutOffHour = x.CutOffHour,
        //            DeliveryTypeID = x.DeliveryTypeID,
        //            DeliveryTypeTimeSlotMapIID = x.DeliveryTypeTimeSlotMapIID,
        //            IsCutOff = x.IsCutOff,
        //            NoOfCutOffOrder = x.NoOfCutOffOrder,
        //            SlotName = x.SlotName,
        //            TimeFrom = x.TimeFrom,
        //            TimeTo = x.TimeTo,
        //            DeliveryTypeTimeSlotMapsCultures = x.DeliveryTypeTimeSlotMapsCultures
        //             .Select(y => new DeliveryTypeTimeSlotMapsCultureDTO()
        //             {
        //                 CultureID = y.CultureID,
        //                 CutOffDisplayText = y.CutOffDisplayText,
        //                 DeliveryTypeTimeSlotMapID = y.DeliveryTypeTimeSlotMapID
        //             }).ToList()
        //        }).ToList();

        //        Framework.CacheManager.MemCacheManager<List<DeliveryTypeTimeSlotMapDTO>>
        //            .Add(mapSlots, "DELIVERYTIMESLOT_" + deliveryTypeID.ToString());
        //    }

        //    TimeSpan ts = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        //    return mapSlots
        //        .FirstOrDefault(x => x.DeliveryTypeID == deliveryTypeID && x.TimeFrom <= ts && x.TimeTo >= ts);
        //}

        //public async Task<CountryDTO> GetCountryDetail(long contactIID)
        //{
        //    var countryDTO = Framework.CacheManager.MemCacheManager<CountryDTO>.Get("COUNTRY_BY_CONTACT_" + contactIID.ToString());

        //    if (countryDTO != null)
        //    {
        //        var country = await new Repository.ShoppingCarts.ReferenceDataRepository(_dbContext).GetCountryDetail(contactIID);
        //        countryDTO = country == null ? null : new CountryDTO()
        //        {
        //            CountryID = (byte)country.CountryID,
        //            CountryName = country.CountryName,
        //            CountryCode = country.ThreeLetterCode,
        //        };

        //        Framework.CacheManager.MemCacheManager<CountryDTO>.Add(countryDTO, "COUNTRY_BY_CONTACT_" + contactIID.ToString());
        //    }

        //    return countryDTO;
        //}



        //public async Task<PaymentMethodDTO> GetPaymentMethodDetails(string payMethod)
        //{
        //    var paymentMethod = Framework.CacheManager
        //        .MemCacheManager<PaymentMethodDTO>.Get("PAYMENTMETHODDETAILS_" + payMethod + "_" + _callContext.LanguageCode);

        //    if (paymentMethod == null)
        //    {
        //        int methodID;
        //        int.TryParse(payMethod, out methodID);
        //        Entity.ShoppingCart.Models.PaymentMethod method;

        //        if (methodID != 0)
        //        {
        //            method = await new Repository.ShoppingCarts.ShoppingCartRepository(_dbContext)
        //                .GetPaymentMethodByID(methodID);
        //        }
        //        else
        //        {
        //            method = await new Repository.ShoppingCarts.ShoppingCartRepository(_dbContext)
        //                .GetPaymentMethodByName(payMethod);
        //        }

        //        paymentMethod = new PaymentMethodDTO()
        //        {
        //            PaymentMethodID = method == null ? (short)0 : method.PaymentMethodID,
        //            PaymentMethodName = method == null ? payMethod : method.PaymentMethod1,
        //            Description = method == null ? payMethod : method.Description,
        //            ImageName = method == null ? null : method.ImageName,
        //        };

        //        if (method != null && method.PaymentMethodCultureDatas != null)
        //        {
        //            var cultureData = method.PaymentMethodCultureDatas.FirstOrDefault(x => x.Culture.CultureCode == _callContext.LanguageCode);
        //            if (cultureData != null)
        //            {
        //                paymentMethod.PaymentMethodName = cultureData.PaymentMethod;
        //            }
        //        }

        //        Framework.CacheManager.MemCacheManager<PaymentMethodDTO>
        //            .Add(paymentMethod, "PAYMENTMETHODDETAILS_" + payMethod + "_" + _callContext.LanguageCode);
        //    }

        //    return paymentMethod;
        //}

        //public async Task<List<CartChargeDTO>> GetCartCharges()
        //{
        //    var charges = Framework.CacheManager
        //        .MemCacheManager<List<CartChargeDTO>>.Get("CARTCHARGE_" + _callContext.LanguageCode);

        //    if (charges == null)
        //    {
        //        var chargeEntities = await new Repository.ShoppingCarts.ShoppingCartRepository(_dbContext)
        //                   .GetCartCharges();
        //        charges = chargeEntities
        //            .Select(x => new CartChargeDTO() { CartChargeID = x.CartChargeID, Description = x.Description, Amount = x.Amount, Percentage = x.Percentage })
        //            .ToList();

        //        Framework.CacheManager.MemCacheManager<List<CartChargeDTO>>
        //           .Add(charges, "CARTCHARGE_" + _callContext.LanguageCode);
        //    }

        //    return JsonConvert.DeserializeObject<List<CartChargeDTO>>(JsonConvert.SerializeObject(charges));
        //}

        //public async Task<BranchDTO> GetBranch(long branchID)
        //{
        //    var branch = Framework.CacheManager.MemCacheManager<BranchDTO>.Get("Branch_" + branchID.ToString());

        //    if (branch == null)
        //    {
        //        var branchEntity = await new Eduegate.Domain.Repository.ShoppingCarts.ReferenceDataRepository(_dbContext).GetBranch(branchID);
        //        branch = new BranchDTO()
        //        {
        //            BranchCode = branchEntity.BranchCode,
        //            BranchGroupID = branchEntity.BranchGroupID,
        //            BranchIID = branchEntity.BranchIID,
        //            BranchName = branchEntity.BranchName,
        //            CompanyID = branchEntity.CompanyID,
        //            IsVirtual = branchEntity.IsVirtual,
        //            StatusID = branchEntity.StatusID.HasValue ? branchEntity.StatusID.Value : (byte)1,
        //            TransactionNoPrefix = branchEntity.TransactionNoPrefix
        //        };

        //        Framework.CacheManager.MemCacheManager<BranchDTO>
        //           .Add(branch, "Branch_" + branchID.ToString());
        //    }

        //    return branch;
        //}

        //public async Task<SupplierDTO> GetSupplierByBranchID(long branchID)
        //{
        //    var supplier = Framework.CacheManager.MemCacheManager<SupplierDTO>.Get("SupplierByBranch_" + branchID.ToString());

        //    if (supplier == null)
        //    {
        //        var supplierEntity = await new Eduegate.Domain.Repository.ShoppingCarts.ReferenceDataRepository(_dbContext).GetSupplierByBranchID(branchID);
        //        supplier = supplierEntity == null ? new SupplierDTO() : new SupplierDTO()
        //        {
        //            SupplierIID = supplierEntity.SupplierIID,
        //            BranchID = supplierEntity.BranchID,
        //            SupplierEmail = supplierEntity.SupplierEmail,
        //            FirstName = supplierEntity.FirstName,
        //            LastName = supplierEntity.LastName,
        //            EmployeeID = supplierEntity.EmployeeID.Value,
        //            VendorCR = supplierEntity.VendorCr
        //        };

        //        Framework.CacheManager.MemCacheManager<SupplierDTO>
        //           .Add(supplier, "SupplierByBranch_" + branchID.ToString());
        //    }

        //    return supplier;
        //}
        #endregion
    }
}
