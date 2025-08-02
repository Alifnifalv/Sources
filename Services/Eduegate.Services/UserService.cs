using System.ServiceModel;
using Eduegate.Domain;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts.ProductDetail;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services
{
    public class UserService : BaseService, IUserService
    {
        UserServiceBL userDataBL;

        public UserService()
        {
            userDataBL = new UserServiceBL(CallContext);
        }

        public string UpdateProfileDetails(UserDTO userDTO)
        {
            try
            {
                string message = userDataBL.UpdateProfileDetails(userDTO);
                return message;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<UserService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public string UpdateAddress(ContactDTO contact)
        {
            return userDataBL.UpdateAddress(contact);
        }

        public bool DeleteAddressBook(decimal addressIID)
        {
            return userDataBL.DeleteAddressBook(addressIID);
        }

        //public List<OrderHistoryDTO> GetOrderHistoryDetails(
        //    )
        //{
        public List<Contracts.OrderHistory.OrderHistoryDTO> GetOrderHistoryDetails(
            string documentTypeID, int pageNumber, int pageSize, string customerID, bool withCurrencyConversion, long orderID, bool showSerialKey)
        {
            try
            {
                List<Contracts.OrderHistory.OrderHistoryDTO> orderHistoryDTOList = userDataBL.GetOrderHistoryDetails(documentTypeID, pageNumber, pageSize, customerID: customerID, context: this.CallContext, orderID: orderID, withCurrencyConversion: withCurrencyConversion, showSerialKey: showSerialKey);
                return orderHistoryDTOList;
                //return userDataBL.GetOrderHistoryDetails();

            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<UserService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<Contracts.OrderHistory.OrderHistoryDTO> GetOrderHistoryItemDetails(
      string documentTypeID, int pageNumber, int pageSize, string customerID, bool withCurrencyConversion, long orderID)
        {
            try
            {
                List<Contracts.OrderHistory.OrderHistoryDTO> orderHistoryDTOList = userDataBL.GetOrderHistoryItemDetails(documentTypeID, pageNumber, pageSize, customerID: customerID, context: this.CallContext, orderID: orderID, withCurrencyConversion: withCurrencyConversion);
                return orderHistoryDTOList;
                //return userDataBL.GetOrderHistoryDetails();

            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<UserService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }


        public List<WishListDTO> GetWishList()
        {
            try
            {
                List<WishListDTO> wishListDTODetails = userDataBL.GetWishListDetails(this.CallContext);
                return wishListDTODetails;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<UserService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool AddWishListItem(long skuID)
        {
            return userDataBL.AddWishListItem(skuID, this.CallContext);
        }



        public bool RemoveWishListItem(long skuID)
        {
            return userDataBL.RemoveWishListItem(skuID, this.CallContext);
        }

        public bool AddSaveForLater(long skuID)
        {
            return userDataBL.AddSaveForLater(skuID, this.CallContext);
        }

        public List<WishListDTO> GetSaveForLater()
        {
            try
            {
                List<WishListDTO> saveForLaterDTODetails = userDataBL.GetSaveForLater(this.CallContext);
                return saveForLaterDTODetails;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<UserService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool RemoveSaveForLater(long skuID)
        {
            return userDataBL.RemoveSaveForLater(skuID, this.CallContext);
        }

        public bool UpdateNewsLetter(CustomerDTO customerDTO)
        {
            try
            {
                bool result = userDataBL.UpdateNewsLetter(customerDTO);
                return result;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<UserService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        //public bool UpdateWishList(decimal productIID, ContextDTO contextualInformation)
        //{
        //    try
        //    {
        //        bool isWishListUpdated = userDataBL.UpdateWishList(productIID, contextualInformation);
        //        return isWishListUpdated;
        //    }
        //    catch (Exception exception)
        //    {
        //        Eduegate.Logger.LogHelper<UserService>.Fatal(exception.Message, exception);
        //        throw new FaultException("Internal server, please check with your administrator");
        //    }
        //}

        public UserInfoDTO GetUserInfo(decimal loggedInUserIID)
        {
            try
            {
                var userInfo = userDataBL.GetUserInfo(loggedInUserIID);
                return userInfo;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<UserService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool UpdatePersonalProfileDetails(UserDTO userDTO)
        {
            try
            {
                //string message = userDataBL.UpdatePersonalProfileDetails(userDTO);
                //return message;
                return userDataBL.UpdatePersonalProfileDetails(userDTO);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<UserService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<Contracts.OrderHistory.OrderHistoryDTO> GetOrderHistoryDetailsWithPagination(
            string documentTypeID, int pageNumber, int pageSize, string customerID, bool withCurrencyConversion)
        {
            try
            {
                List<Contracts.OrderHistory.OrderHistoryDTO> orderHistoryDTOList = userDataBL.GetOrderHistoryDetailsWithPagination(documentTypeID, customerID, pageNumber, pageSize, CallContext, withCurrencyConversion);
                return orderHistoryDTOList;
                //return userDataBL.GetOrderHistoryDetails();

            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<UserService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<ProductSKUDetailDTO> GetProductsEmarsys(WidgetTypes widgetType, string productIDs, long cultureID)
        {
            try
            {
                return userDataBL.GetProductsEmarsys(widgetType, productIDs, cultureID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<UserService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool HasClaimAccess(long claimID, long userID)
        {
            try
            {
                return userDataBL.HasClaimAccess(claimID, userID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<UserService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }


        public bool HasClaimAccessByResourceID(string resource, long userID)
        {
            try
            {
                return userDataBL.HasClaimAccessByResourceID(resource, userID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<UserService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<KeyValueDTO> GetReplacementActions(long headID)
        {
            try
            {
                return userDataBL.GetReplacementActions(headID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<UserService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<Contracts.Security.ClaimDTO> GetClaimsByTypeAndLoginID(ClaimType type, long userID)
        {
            try
            {
                return userDataBL.GetClaimsByTypeAndLoginID(type, userID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<UserService>.Fatal(exception.Message, exception);
                
                throw new FaultException("Internal server, please check with your administrator");
            }
        }


        public List<Contracts.Security.ClaimDTO> GetDashBoardByUserID(ClaimType type, long userID)
        {
            try
            {
                return userDataBL.GetDashBoardByUserID(type, userID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<UserService>.Fatal(exception.Message, exception);

                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public void ResetForceLogout(long loginID)
        {
            try
            {
                new Eduegate.Domain.Security.SecurityBL(CallContext).ResetForceLogout(loginID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<UserService>.Fatal(exception.Message, exception);
                throw new Exception("Internal server, please check with your administrator");
            }
        }

    }
}
