using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.OrderHistory;
using Eduegate.Services.Contracts.ProductDetail;
using Eduegate.Services.Contracts.Security;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUserService" in both code and config file together.
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "UpdateProfileDetails")]
        string UpdateProfileDetails(UserDTO userDTO);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "UpdateAddress")]
        string UpdateAddress(ContactDTO contact);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "DeleteAddressBook")]
        bool DeleteAddressBook(decimal addressIID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetOrderHistoryDetails?documentTypeID={documentTypeID}&pageNumber={pageNumber}&pageSize={pageSize}&customerID={customerID}&withCurrencyConversion={withCurrencyConversion}&orderID={orderID}&showSerialKey={showSerialKey}")]
        List<OrderHistory.OrderHistoryDTO> GetOrderHistoryDetails(string documentTypeID, int pageNumber, int pageSize, string customerID, bool withCurrencyConversion, long orderID, bool showSerialKey);


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetWishList")]
        List<WishListDTO> GetWishList();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "AddWishListItem")]
        bool AddWishListItem(long skuID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "RemoveWishListItem")]
        bool RemoveWishListItem(long skuID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "UpdateNewsLetter")]
        bool UpdateNewsLetter(CustomerDTO customerDTO);

        //[OperationContract]
        //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "UpdateWishList?productIID={productIID}")]
        //bool UpdateWishList(decimal productIID, ContextDTO contextualInformation);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetUserInfo?loggedInUserIID={loggedInUserIID}")]
        UserInfoDTO GetUserInfo(decimal loggedInUserIID);


        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "AddSaveForLater")]
        bool AddSaveForLater(long skuID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetSaveForLater")]
        List<WishListDTO> GetSaveForLater();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "RemoveSaveForLater")]
        bool RemoveSaveForLater(long skuID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "UpdatePersonalProfileDetails")]
        bool UpdatePersonalProfileDetails(UserDTO userDTO);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetOrderHistoryItemDetails?documentTypeID={documentTypeID}&pageNumber={pageNumber}&pageSize={pageSize}&customerID={customerID}&withCurrencyConversion={withCurrencyConversion}&orderID={orderID}")]
        List<OrderHistory.OrderHistoryDTO> GetOrderHistoryItemDetails(string documentTypeID, int pageNumber, int pageSize, string customerID, bool withCurrencyConversion, long orderID);


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetOrderHistoryDetailsWithPagination?documentTypeID={documentTypeID}&pageNumber={pageNumber}&pageSize={pageSize}&customerID={customerID}&withCurrencyConversion={withCurrencyConversion}")]
        List<OrderHistory.OrderHistoryDTO> GetOrderHistoryDetailsWithPagination(string documentTypeID, int pageNumber, int pageSize, string customerID, bool withCurrencyConversion);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetProductsEmarsys?widgetType={widgetType}&productIDs={productIDs}&cultureID={cultureID}")]
        List<ProductSKUDetailDTO> GetProductsEmarsys(WidgetTypes widgetType, string productIDs, long cultureID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "HasClaimAccess?claimID={claimID}&userID={userID}")]
        bool HasClaimAccess(long claimID, long userID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "HasClaimAccessByResourceID?resource={resource}&userID={userID}")]
        bool HasClaimAccessByResourceID(string resource, long userID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetClaimsByTypeAndLoginID?type={type}&userID={userID}")]
        List<ClaimDTO> GetClaimsByTypeAndLoginID(ClaimType type, long userID);


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetDashBoardByUserID?type={type}&userID={userID}")]
        List<ClaimDTO> GetDashBoardByUserID(ClaimType type, long userID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetReplacementActions?headID={headID}")]
        List<KeyValueDTO> GetReplacementActions(long headID);
    }
}
