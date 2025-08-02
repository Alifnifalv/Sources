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
using Eduegate.Services;

namespace Eduegate.Service.Client.Direct
{
    public class UserServiceClient :  IUserService
    {
        UserService service = new UserService();

        public UserServiceClient(CallContext callContext = null, Action<string> logger = null)
        {
            service.CallContext = callContext;
        }

        public string UpdateProfileDetails(UserDTO userDTO)
        {
            return service.UpdateProfileDetails(userDTO);
        }


        public string UpdateAddress(ContactDTO contact)
        {
            return service.UpdateAddress(contact);
        }

        public bool DeleteAddressBook(decimal addressIID)
        {
            return service.DeleteAddressBook(addressIID);
        }

        public List<Services.Contracts.OrderHistory.OrderHistoryDTO> GetOrderHistoryDetails(string documentTypeID, int pageNumber, int pageSize, string customerID, bool withCurrencyConversion, long orderID, bool showSerialKey)
        {
            return service.GetOrderHistoryDetails(documentTypeID, pageNumber, pageSize, customerID, withCurrencyConversion, orderID, showSerialKey);
        }

        public List<Services.Contracts.OrderHistory.OrderHistoryDTO> GetOrderHistoryDetailsWithPagination(string documentTypeID, int pageNumber, int pageSize, string customerID, bool withCurrencyConversion)
        {
            return service.GetOrderHistoryDetailsWithPagination(documentTypeID, pageNumber, pageSize, customerID, withCurrencyConversion);
        }

        public List<WishListDTO> GetWishList()
        {
            return service.GetWishList();
        }

        public bool AddWishListItem(long skuID)
        {
            return service.AddWishListItem(skuID);
        }

        public bool RemoveWishListItem(long skuID)
        {
            return service.RemoveWishListItem(skuID);
        }

        public bool UpdateNewsLetter(CustomerDTO customerDTO)
        {
            return service.UpdateNewsLetter(customerDTO);
        }

        public UserInfoDTO GetUserInfo(decimal loggedInUserIID)
        {
            return service.GetUserInfo(loggedInUserIID);
        }

        public bool AddSaveForLater(long skuID)
        {
            return service.AddSaveForLater(skuID);
        }

        public List<WishListDTO> GetSaveForLater()
        {
            return service.GetSaveForLater();
        }

        public bool RemoveSaveForLater(long skuID)
        {
            return service.RemoveSaveForLater(skuID);
        }

        public bool UpdatePersonalProfileDetails(UserDTO userDTO)
        {
            return service.UpdatePersonalProfileDetails(userDTO);
        }

        public List<Services.Contracts.OrderHistory.OrderHistoryDTO> GetOrderHistoryItemDetails(string documentTypeID, int pageNumber, int pageSize, string customerID, bool withCurrencyConversion, long orderID)
        {
            return service.GetOrderHistoryItemDetails(documentTypeID,  pageNumber, pageSize, customerID, withCurrencyConversion, orderID);
        }

        public List<ProductSKUDetailDTO> GetProductsEmarsys(WidgetTypes widgetType, string productIDs, long cultureID)
        {
            return service.GetProductsEmarsys(widgetType, productIDs, cultureID);
        }

        public bool HasClaimAccess(long claimID, long userID)
        {
            return service.HasClaimAccess(claimID, userID);
        }

        public bool HasClaimAccessByResourceID(string resource, long userID)
        {
            return service.HasClaimAccessByResourceID(resource, userID);
        }

        public List<KeyValueDTO> GetReplacementActions(long headID)
        {
            return service.GetReplacementActions(headID);
        }

        public List<Services.Contracts.Security.ClaimDTO> GetClaimsByTypeAndLoginID(ClaimType type, long userID)
        {
            return service.GetClaimsByTypeAndLoginID(type, userID);
        }

        public List<Services.Contracts.Security.ClaimDTO> GetDashBoardByUserID(ClaimType type, long userID)
        {
            return service.GetDashBoardByUserID(type, userID);
        }

        public void ResetForceLogout(long loginID)
        {
            service.ResetForceLogout(loginID);
        }

    }
}