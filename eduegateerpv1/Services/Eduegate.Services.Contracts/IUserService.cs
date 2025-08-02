using System.Collections.Generic;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.ProductDetail;
using Eduegate.Services.Contracts.Security;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUserService" in both code and config file together.    
    public interface IUserService
    {
        string UpdateProfileDetails(UserDTO userDTO);

        string UpdateAddress(ContactDTO contact);

        bool DeleteAddressBook(decimal addressIID);

        List<OrderHistory.OrderHistoryDTO> GetOrderHistoryDetails(string documentTypeID, int pageNumber, int pageSize, string customerID, bool withCurrencyConversion, long orderID, bool showSerialKey);

        List<WishListDTO> GetWishList();

        bool AddWishListItem(long skuID);

        bool RemoveWishListItem(long skuID);

        bool UpdateNewsLetter(CustomerDTO customerDTO);

        //bool UpdateWishList(decimal productIID, ContextDTO contextualInformation);

        UserInfoDTO GetUserInfo(decimal loggedInUserIID);

        bool AddSaveForLater(long skuID);

        List<WishListDTO> GetSaveForLater();

        bool RemoveSaveForLater(long skuID);

        bool UpdatePersonalProfileDetails(UserDTO userDTO);

        List<OrderHistory.OrderHistoryDTO> GetOrderHistoryItemDetails(string documentTypeID, int pageNumber, int pageSize, string customerID, bool withCurrencyConversion, long orderID);

        List<OrderHistory.OrderHistoryDTO> GetOrderHistoryDetailsWithPagination(string documentTypeID, int pageNumber, int pageSize, string customerID, bool withCurrencyConversion);

        List<ProductSKUDetailDTO> GetProductsEmarsys(WidgetTypes widgetType, string productIDs, long cultureID);

        bool HasClaimAccess(long claimID, long userID);

        bool HasClaimAccessByResourceID(string resource, long userID);

        List<ClaimDTO> GetClaimsByTypeAndLoginID(ClaimType type, long userID);

        List<ClaimDTO> GetDashBoardByUserID(ClaimType type, long userID);

        List<KeyValueDTO> GetReplacementActions(long headID);

        void ResetForceLogout(long loginID);
    }
}