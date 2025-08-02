using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.OrderHistory;
using Eduegate.Services.Contracts.ProductDetail;

namespace Eduegate.Service.Client
{
    public class UserServiceClient : BaseClient, IUserService
    {
        private static string serviceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string userService = string.Concat(serviceHost, Eduegate.Framework.Helper.Constants.USER_SERVICE_NAME);

        public UserServiceClient(CallContext callContext = null, Action<string> logger = null)
            :base(callContext, logger)
        {
        }

        public string UpdateProfileDetails(UserDTO userDTO)
        {
            string request = ServiceHelper.HttpPostRequest(userService + "UpdateProfileDetails", userDTO);
            return JsonConvert.DeserializeObject<string>(request);
        }


        public string UpdateAddress(ContactDTO contact)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAddressBook(decimal addressIID)
        {
            throw new NotImplementedException();
        }

        public List<Services.Contracts.OrderHistory.OrderHistoryDTO> GetOrderHistoryDetails(string documentTypeID, int pageNumber, int pageSize, string customerID, bool withCurrencyConversion, long orderID, bool showSerialKey)
        {
            var uri = string.Format("{0}/{1}?documentTypeID={2}&pageNumber={3}&pageSize={4}&customerID={5}&withCurrencyConversion={6}&orderID={7}&showSerialKey={8}", userService, "GetOrderHistoryDetails", documentTypeID, pageNumber, pageSize, customerID, withCurrencyConversion, orderID, showSerialKey);
            return (ServiceHelper.HttpGetRequest<List<Services.Contracts.OrderHistory.OrderHistoryDTO>>(uri, _callContext, _logger));
        }

        public List<Services.Contracts.OrderHistory.OrderHistoryDTO> GetOrderHistoryDetailsWithPagination(string documentTypeID, int pageNumber, int pageSize, string customerID, bool withCurrencyConversion)
        {
            var uri = string.Format("{0}/{1}?documentTypeID={2}&pageNumber={3}&pageSize={4}&customerID={5}&withCurrencyConversion={6}", userService, "GetOrderHistoryDetailsWithPagination", documentTypeID, pageNumber, pageSize, customerID, withCurrencyConversion);
            return (ServiceHelper.HttpGetRequest<List<Services.Contracts.OrderHistory.OrderHistoryDTO>>(uri, _callContext, _logger));
        }

        public List<WishListDTO> GetWishList()
        {
            throw new NotImplementedException();
        }

        public bool AddWishListItem(long skuID)
        {
            throw new NotImplementedException();
        }

        public bool RemoveWishListItem(long skuID)
        {
            throw new NotImplementedException();
        }

        public bool UpdateNewsLetter(CustomerDTO customerDTO)
        {
            throw new NotImplementedException();
        }

        public UserInfoDTO GetUserInfo(decimal loggedInUserIID)
        {
            throw new NotImplementedException();
        }

        public bool AddSaveForLater(long skuID)
        {
            throw new NotImplementedException();
        }

        public List<WishListDTO> GetSaveForLater()
        {
            throw new NotImplementedException();
        }

        public bool RemoveSaveForLater(long skuID)
        {
            throw new NotImplementedException();
        }


        public bool UpdatePersonalProfileDetails(UserDTO userDTO)
        {
            string request = ServiceHelper.HttpPostRequest(userService + "UpdatePersonalProfileDetails", userDTO);
            return JsonConvert.DeserializeObject<bool>(request);
        }

        public List<Services.Contracts.OrderHistory.OrderHistoryDTO> GetOrderHistoryItemDetails(string documentTypeID, int pageNumber, int pageSize, string customerID, bool withCurrencyConversion, long orderID)
        {
            var uri = string.Format("{0}/{1}?documentTypeID={2}&pageNumber={3}&pageSize={4}&customerID={5}&withCurrencyConversion={6}&orderID={7}", userService, "GetOrderHistoryItemDetails", documentTypeID, pageNumber, pageSize, customerID, withCurrencyConversion, orderID);
            return (ServiceHelper.HttpGetRequest<List<Services.Contracts.OrderHistory.OrderHistoryDTO>>(uri, _callContext, _logger));
        }

        public List<ProductSKUDetailDTO> GetProductsEmarsys(WidgetTypes widgetType, string productIDs, long cultureID)
        {
            var uri = string.Format("{0}/{1}?widgetType={2}&productIDs={3}&cultureID={4}", userService, "GetProductsEmarsys", widgetType, productIDs, cultureID);
            return (ServiceHelper.HttpGetRequest<List<ProductSKUDetailDTO>>(uri, _callContext, _logger));
        }

        public bool HasClaimAccess(long claimID, long userID)
        {
            var uri = string.Format("{0}/{1}?claimID={2}&userID={3}", userService, "HasClaimAccess", claimID, userID);
            return (ServiceHelper.HttpGetRequest<bool>(uri, _callContext, _logger));
        }

        public bool HasClaimAccessByResourceID(string resource, long userID)
        {
            var uri = string.Format("{0}/{1}?resource={2}&userID={3}", userService, "HasClaimAccessByResourceID", resource, userID);
            return (ServiceHelper.HttpGetRequest<bool>(uri, _callContext, _logger));
        }

        public List<KeyValueDTO> GetReplacementActions(long headID)
        {
            var uri = string.Format("{0}/{1}?headID={2}", userService, "GetReplacementActions", headID);
            return (ServiceHelper.HttpGetRequest<List<KeyValueDTO>>(uri, _callContext, _logger));
        }

        public List<Services.Contracts.Security.ClaimDTO> GetClaimsByTypeAndLoginID(ClaimType type, long userID)
        {
            var uri = string.Format("{0}/{1}?type={2}&userID={3}", userService, "GetClaimsByTypeAndLoginID", type, userID);
            return (ServiceHelper.HttpGetRequest<List<Services.Contracts.Security.ClaimDTO>>(uri, _callContext, _logger));
        }

        public List<Services.Contracts.Security.ClaimDTO> GetDashBoardByUserID(ClaimType type, long userID)
        {
            var uri = string.Format("{0}/{1}?type={2}&userID={3}", userService, "GetDashBoardByUserID", type, userID);
            return (ServiceHelper.HttpGetRequest<List<Services.Contracts.Security.ClaimDTO>>(uri, _callContext, _logger));
        }

        public void ResetForceLogout(long loginID)
        {
            throw new NotImplementedException();
        }

    }
}
