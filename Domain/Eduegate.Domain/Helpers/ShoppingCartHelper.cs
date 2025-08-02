using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using System;
using System.Security.Cryptography;

namespace Eduegate.Domain.Helpers
{
    public class ShoppingCartHelper
    {
        CallContext _callContext;

        private ShoppingCartHelper()
        {
        }

        public static ShoppingCartHelper Helper(CallContext callContext)
        {
            var manager = new ShoppingCartHelper();
            manager._callContext = callContext;
            return manager;
        }

        public T GetSettingValue<T>(string settingKey, long companyID, T defaultValue)
        {
            return new Domain.Setting.SettingBL(_callContext).GetSettingValue<T>(settingKey, companyID, defaultValue);
        }

        public bool RemoveItem(long productSKU, CallContext contextualInformation, long? customerID = null)
        {
            var shoppingCartRepository = new ShoppingCartRepository();
            return shoppingCartRepository.RemoveItem(productSKU, customerID.HasValue &&
                customerID.Value > 0 ? customerID.Value.ToString() :
                GetCustomerID(contextualInformation), GetSiteID(contextualInformation));
        }

        public bool RemoveAllItem(CallContext contextualInformation, long? customerID = null)
        {
            var shoppingCartRepository = new ShoppingCartRepository();
            return shoppingCartRepository.RemoveAllItem(customerID.HasValue &&
                customerID.Value > 0 ? customerID.Value.ToString() :
                GetCustomerID(contextualInformation), GetSiteID(contextualInformation));
        }

        public string GetCustomerID(CallContext callContext)
        {
            if (string.IsNullOrEmpty(callContext.EmailID) && string.IsNullOrEmpty(callContext.MobileNumber))
            {
                return callContext.GUID;
            }
            else
            {
                var customer = new ShoppingCartRepository().GetCustomerID(callContext.EmailID,
                    callContext.MobileNumber, "", callContext.LoginID);
                //callContext.MobileNumber, callContext.UserReferenceID, callContext.LoginID);
                callContext.UserId = customer.ToString();
                return customer == 0 ? null : customer.ToString();
            }
        }

        public long Get8Digits()
        {
            var bytes = new byte[4];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            ulong random = BitConverter.ToUInt64(bytes, 0) % 100000000;
            return long.Parse(random.ToString());
        }

        public long GetRandomNumber()
        {
            var bytes = new byte[8];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);

            ulong random = BitConverter.ToUInt64(bytes, 0);

            // Ensure the random number is within the 8-digit range
            long result = (long)(random % 100000000);

            return result;
        }

        public int? GetSiteID(CallContext _callContext)
        {
            var siteID = _callContext.IsNull() || string.IsNullOrEmpty(_callContext.SiteID) ? (int?)null :
                Convert.ToInt32(_callContext.SiteID);

            if (siteID.HasValue)
            {
                var site = new ShoppingCartRepository().GetSiteByCompany(_callContext.CompanyID);

                if (site != null)
                {
                    siteID = site.SiteID;
                }
                else
                {
                    siteID = (int?)null; //default
                }
            }

            return siteID;
        }
    }
}
