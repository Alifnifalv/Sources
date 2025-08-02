using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Application.Mvc;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Web.Library.Helpers;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.ShoppingCart;
using Microsoft.AspNetCore.Mvc;
using Eduegate.Domain;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Eduegate.ERP.Admin.Areas.Inventories.Controllers
{
    [Area("Inventories")]
    public class ShoppingCartController : BaseSearchController
    {
        // GET: Inventories/ShoppingCart
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Infrastructure.Enums.SearchView.ShoppingCart);

            return View(new SearchListViewModel
            {
                ControllerName = "Inventories/" + Infrastructure.Enums.SearchView.ShoppingCart.ToString(),
                ViewName = Infrastructure.Enums.SearchView.ShoppingCart,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                //                InfoBar = @"<li class='status-label-mobile'>
                //                                        <div class='right status-label'>
                //                                            <div class='status-label-color'><label class='status-color-label  green'></label>Active</div>
                //                                            <div class='status-label-color'><label class='status-color-label red'></label>In Active</div>
                //                                        </div>
                //                                    </li>"
                IsChild = false,
                HasChild = true,
                HasFilters = metadata.HasFilters,
                FilterColumns = metadata.QuickFilterColumns,
                IsEditableLink = false,
                UserValues = metadata.UserValues
            });
        }

        [HttpGet]
        public async Task<IActionResult> ChildList(long ID)
        {
            // call the service and get the meta data
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList(Services.Contracts.Enums.SearchView.ShoppingCartItems);

            return PartialView("_ChildList", new SearchListViewModel
            {
                //ControllerName = "Inventories/" + Infrastructure.Enums.SearchView.ShoppingCart.ToString(),
                ViewName = Infrastructure.Enums.SearchView.ShoppingCartItems,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                FilterColumns = metadata.QuickFilterColumns,
                RuntimeFilter = Convert.ToString("ShoppingCartIID=" + ID.ToString()).Trim(), // child view must have this column
                IsChild = true,
                ParentViewName = Infrastructure.Enums.SearchView.ShoppingCart,
                IsEditableLink = false
            });
        }

        [HttpGet]
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {
            var view = Infrastructure.Enums.SearchView.ShoppingCart;
            if (runtimeFilter.Trim().IsNotNullOrEmpty())
            {
                view = Infrastructure.Enums.SearchView.ShoppingCartItems;
                //runtimeFilter = runtimeFilter + " AND CompanyID = " + CallContext.CompanyID.ToString();
            }
            else
            {
                runtimeFilter = " CompanyID = " + CallContext.CompanyID.ToString();
            }

            return base.SearchData(view, currentPage, orderBy, runtimeFilter);
        }

        [HttpGet]
        public Task<IActionResult> SearchSummaryData()
        {
            return base.SearchSummaryData(Infrastructure.Enums.SearchView.ShoppingCartSummary);
        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.HasCartAdminAccess = ClientFactory.UserServiceClient(CallContext).HasClaimAccessByResourceID("CARTADMIN", CallContext.LoginID.Value);
            ViewBag.IID = IID;
            return View();
        }


        //public ActionResult Create(long ID = 0)
        //{
        //    var vm = new CartViewModel()
        //    {
        //        DetailViewModel = new List<ShoppingCartItemViewModel>() { new ShoppingCartItemViewModel() { } },
        //        MasterViewModel = new CartMasterViewModel()
        //        {
        //            Currency = KeyValueViewModel.ToViewModel(ClientFactory.ReferenceDataServiceClient(CallContext).GetDefaultCurrency()),

        //        },
        //    };

        //    return Json(vm);
        //}

        [HttpGet]
        public ActionResult Edit(long ID = 0)
        {
            return RedirectToAction("Create", new { ID = ID });
        }

        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            var vm = CartViewModel.ToViewModel(ClientFactory.ShoppingCartServiceClient(CallContext).GetCart(ID.ToString(), true));
            if (vm != null)
            {
                vm.DetailViewModel = new List<ShoppingCartItemViewModel>();
            }
            return Json(vm);
        }

        public void ResetCallContext(CallContext oldContext)
        {
            base.CallContext.EmailID = oldContext.EmailID;
            base.CallContext.UserId = oldContext.UserId;
            base.CallContext.LoginID = oldContext.LoginID;
            base.CallContext.CurrencyCode = oldContext.CurrencyCode;
        }

        [HttpGet]
        public JsonResult GetInventoryDetailsSKUID(long skuID, long customerID)
        {
            var vm = new ProductInventoryBranchViewModel();
            var dto = ClientFactory.ProductDetailServiceClient(base.CallContext).GetInventoryDetailsSKUID(skuID, customerID, true);
            if (dto.IsNotNull())
            {
                vm = ProductInventoryBranchViewModel.ToVM(dto);
            }
            return Json(new { ViewModel = vm });

        }

        [HttpPost]
        public JsonResult ProcessOrder(ProcessOrderViewModel vm)
        {
            vm.Message = "";
            if (string.IsNullOrEmpty(vm.Description)) { vm.Message = "Description cannot be empty"; return Json(vm); }
            var cartClient = ClientFactory.ShoppingCartServiceClient(CallContext);

            if (!vm.IsForceCreation)
            {
                //check if any payment information exists in the system
                var cartPaymentDetailsDTO = cartClient.GetCartStatusWithPaymentGateWayTrackKey(vm.ShoppingCartIID, vm.PaymentgateWayID);

                if (!(vm.PaymentgateWayID > 0 && cartPaymentDetailsDTO.CartStatusID == (int)ShoppingCartStatus.PaymentInitiated))
                {
                    vm.Message = "Cart status is not initilazied or payment gateway missing.";
                    return Json(vm);
                }
            }

            var currentStatus = (ShoppingCartStatus)Enum.Parse(typeof(ShoppingCartStatus), vm.CartStatusID.ToString());
            //Update the shopping cart to Payment Initiated
            var updateStatus = cartClient.UpdateCartStatus(vm.ShoppingCartIID, ShoppingCartStatus.PaymentInitiated, vm.PaymentMethod, currentStatus);

            if (!updateStatus)
            {
                vm.Message = "Not able update the status to payment intiated, may be already processed.";
                return Json(vm);
            }

            try
            {
                var loginID = ClientFactory.AccountServiceClient(CallContext).GetLoginIDbyCustomerID(vm.CartID);

                if (loginID == 0)
                {
                    vm.Message = "Customer login information is missing";
                    return Json(vm);
                }
            }
            catch
            {
                vm.Message = "Error in fetching customer details.";
                return Json(vm);
            }

            //process the irder
            var oldContext = base.CallContext;
            var loginDetail = ClientFactory.AccountServiceClient(CallContext).GetUserDetailsByCustomerID(vm.CartID);

            base.CallContext.EmailID = loginDetail.LoginEmailID;
            base.CallContext.UserId = loginDetail.Customer.CustomerIID.ToString();
            base.CallContext.LoginID = long.Parse(loginDetail.LoginID);
            //TODO : Currency from the customer default country.
            base.CallContext.CurrencyCode = CallContext.CompanyID == 1 ? "KWD" : "SAR";

            try
            {
                if (!ClientFactory.OrderServiceClient(CallContext).SetTemporaryBranchTransfer(vm.ShoppingCartIID, false))
                {
                    vm.Message = "Inventory blocking failed: Inventory not available for processing.";
                    return Json(vm);
                }

                var orderServiceCleint = ClientFactory.OrderServiceClient(base.CallContext);
                var transactionID = orderServiceCleint.ConfirmOnlineOrderCartID(vm.PaymentMethod, 0, vm.ShoppingCartIID, 0);

                if (!string.IsNullOrEmpty(transactionID))
                {
                    vm.CartStatus = (int)ShoppingCartStatus.CheckedOut;
                    vm.Description += ":Processed by " + oldContext.EmailID;

                    var processOrderDTO = ClientFactory.ShoppingCartServiceClient(CallContext).UpdateCartDescription(ProcessOrderViewModel.ToDTO(vm));
                    if (processOrderDTO.IsNotNull())
                    {
                        vm = ProcessOrderViewModel.ToVM(processOrderDTO);
                        vm.Message = "Order processed successfully";

                        //generate notification
                        if (!GenerateOrderNotification(transactionID))
                        {
                            vm.Message = "Order processed successfully, email generation failed.";
                        }
                    }
                }
                else
                {
                    //revert back the blocked inventory
                    orderServiceCleint.SetTemporaryBranchTransfer(vm.ShoppingCartIID, true);
                    vm.Message = "Error occured while generating order, some informations are missing.";
                }
            }
            catch (Exception ex)
            {
                vm.Message = ex.Message;
                return Json(vm);
            }

            return Json(vm);
        }

        private bool GenerateOrderNotification(string transactionIDs)
        {
            try
            {
                var transactionHeadIDs = transactionIDs.Split(',');
                var emailNotificationTypeDetail = ClientFactory.NotificationServiceClient(CallContext).GetEmailNotificationType(Services.Contracts.Enums.EmailNotificationTypes.OrderConfirmation);
                var helper = new NotificationHelper(this as BaseController, base.CallContext);
                var settingDetail = ClientFactory.SettingServiceClient(CallContext).GetSettingDetailByCompany(Constants.TransactionSettings.ONLINESALESDOCTTYPEID, (long)this.CallContext.CompanyID);

                foreach (var TransactionID in transactionHeadIDs)
                {
                    var orderHistoryDTOList = ClientFactory.UserServiceClient(CallContext).GetOrderHistoryDetails(settingDetail.SettingValue, 0, 1, "0", false, Convert.ToInt64(TransactionID), false);

                    foreach (var orderHistoryDTO in orderHistoryDTOList)
                    {
                        helper.SendConfirmationEmail(orderHistoryDTO.TransactionOrderIID, emailNotificationTypeDetail, orderHistoryDTO.TransactionNo,
                            orderHistoryDTO.CompanyID, orderHistoryDTO.CompanyID == 1 ? 1 : 2);
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpGet]
        public JsonResult CreateCart(long customerID)
        {
            var cartItem = new CartMasterViewModel();

            if (customerID == 0)
            {
                return Json(new { IsError = true, Message = "Please select a customer." });
            }

            try
            {
                cartItem = CartMasterViewModel.ToViewModel(ClientFactory.ShoppingCartServiceClient(CallContext).CreateCart(customerID));
            }

            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ShoppingCartController>.Fatal(exception.Message.ToString(), exception);
            }

            return Json(cartItem);
        }

        //[HttpPost]
        //public JsonResult AddToCart(CartProductViewModel product)
        //{
        //    var productDTO = new CartProductDTO();
        //    var result = new AddToCartStatusDTO();
        //    var vm = new ShoppingCartViewModel();
        //    if (string.IsNullOrEmpty(CallContext.EmailID) && string.IsNullOrEmpty(CallContext.GUID))
        //    {
        //        CallContext.GUID = Convert.ToString(Guid.NewGuid());
        //        ResetCallContext(CallContext);
        //    }
        //    base.CallContext.CurrencyCode = CallContext.CompanyID == 1 ? "KWD" : "SAR";
        //    result = new ShoppingCartHelpers(CallContext).AddToCart(product);
        //    CartDTO cart = ClientFactory.ShoppingCartServiceClient(base.CallContext).GetCart("0", isDeliveryCharge: false, isValidation: true, status: ShoppingCartStatus.InProcess, customerID: product.CustomerID.IsNotNull() ? product.CustomerID : 0);
        //    vm = ShoppingCartViewModel.ToViewModel(cart);

        //    return Json(new { data = vm });
        //}

        [HttpGet]
        public JsonResult UpdateCartStatus(long cartID, ShoppingCartStatus statusID, string paymentMethod)
        {
            bool IsCartStatusUpdated = false;
            try
            {
                IsCartStatusUpdated = ClientFactory.ShoppingCartServiceClient(base.CallContext).UpdateCartStatus(cartID, statusID, null, ShoppingCartStatus.None);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<ShoppingCartController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = !IsCartStatusUpdated, Message = ex.Message.ToString() });
            }

            return Json(new { IsError = !IsCartStatusUpdated, data = IsCartStatusUpdated });
        }


        //[HttpGet]
        //public JsonResult SaveOrderAddresses(long billingAddressID, long shippingAddressID, long? customerID = null)
        //{
        //    string serviceResult = "false";
        //    var cartDTO = new CartDTO();

        //    if (billingAddressID.IsNotNull() && billingAddressID > 0)
        //        cartDTO.BillingAddressID = billingAddressID;

        //    if (shippingAddressID.IsNotNull() && shippingAddressID > 0)
        //        cartDTO.ShippingAddressID = shippingAddressID;

        //    if (customerID.IsNotNull() && customerID > 0)
        //        cartDTO.CustomerID = customerID;

        //    if (cartDTO.BillingAddressID.IsNotNull() || cartDTO.ShippingAddressID.IsNotNull())
        //        serviceResult = ServiceHelper.HttpPostRequest(ServiceHost + Eduegate.Framework.Helper.Constants.SHOPPING_CART_SERVICE_NAME + "UpdateShoppingCart", cartDTO, CallContext);

        //    return Json(new { IsError = !Convert.ToBoolean(serviceResult) });
        //}

        [HttpPost]
        public JsonResult Save(string model)
        {
            var jsonData = JObject.Parse(model);
            var vm = JsonConvert.DeserializeObject<CartViewModel>(jsonData.SelectToken("data").ToString());

            var paymentMethod = Framework.Payment.PaymentGatewayType.COD.ToString();
            var cartID = vm.MasterViewModel.ShoppingCartID;
            var CustomerID = vm.MasterViewModel.Customer.Key;
            var isPaymentInitalized = ClientFactory.ShoppingCartServiceClient(base.CallContext).UpdateCartStatus(cartID, ShoppingCartStatus.PaymentInitiated, paymentMethod, Framework.Helper.Enums.ShoppingCartStatus.InProcess);

            if (!isPaymentInitalized)
            {
                return Json(new { IsError = true, UserMessage = "Payment initalization failed" });
            }

            if (!ClientFactory.OrderServiceClient(CallContext).SetTemporaryBranchTransfer(vm.MasterViewModel.ShoppingCartID, false))
            {
                return Json(new { IsError = false, UserMessage = "Inventory blocking failed: Inventory not available for processing.", isAutoClose = true });
            }

            string orderService = string.Concat(new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"), Constants.ORDER_SERVICE_NAME);
            var transactionID = ClientFactory.OrderServiceClient(CallContext).ConfirmOnlineOrderCartID(paymentMethod, 0, 0, long.Parse(CustomerID), vm.MasterViewModel.DeliveryCharge.HasValue ? vm.MasterViewModel.DeliveryCharge.Value : 0);

            //generate notification
            if (!GenerateOrderNotification(transactionID))
            {
                return Json(new { IsError = false, UserMessage = "Successfully saved, email generation failed.", isAutoClose = true });
            }

            return Json(new { IsError = false, UserMessage = "Successfully saved", isAutoClose = true });
        }


        //[HttpPost]
        //public JsonResult UpdateCart(long SKUID, decimal quantity, long customerID = 0)
        //{
        //    try
        //    {
        //        base.CallContext.CurrencyCode = CallContext.CompanyID == 1 ? "KWD" : "SAR";
        //        if (new ShoppingCartHelpers(CallContext).UpdateCart(SKUID, quantity, customerID))
        //        {
        //            return GetCartPrice((long)quantity, customerID: customerID);
        //        }

        //        return Json(false);
        //    }
        //    catch (Exception ex)
        //    {
        //        Eduegate.Logger.LogHelper<ShoppingCartController>.Fatal(ex.Message.ToString(), ex);
        //        return Json(new { IsError = false, Message = ex.Message.ToString() });
        //    }
        //}

        //[HttpGet]
        //public JsonResult GetCartPrice(long currentUpdatedQty = 0, long customerID = 0)
        //{
        //    var cart = new CartDTO();
        //    try
        //    {
        //        cart = new ShoppingCartHelpers(CallContext).GetCartPrice(customerID);
        //    }
        //    catch (Exception ex)
        //    {
        //        Eduegate.Logger.LogHelper<ShoppingCartController>.Fatal(ex.Message.ToString(), ex);
        //        return Json(new { IsError = true, Message = ex.Message.ToString() });
        //    }
        //    return Json(new { IsError = false, data = cart, CartQtyUpdated = currentUpdatedQty });
        //}

        //[HttpGet]
        //public JsonResult RemoveItem(long SKUID, long customerID = 0)
        //{
        //    bool IsItemRemovedFromCart = false;
        //    try
        //    {
        //        IsItemRemovedFromCart = new ShoppingCartHelpers(CallContext).RemoveItem(SKUID, customerID);
        //        if (IsItemRemovedFromCart)
        //        {
        //            return GetCartPrice(0, customerID);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Eduegate.Logger.LogHelper<ShoppingCartController>.Fatal(ex.Message.ToString(), ex);
        //        return Json(new { IsError = !IsItemRemovedFromCart, Message = ex.Message.ToString() });
        //    }
        //    return Json(new { IsError = !IsItemRemovedFromCart });
        //}

        //[HttpPost]
        //public JsonResult UpdateCartDelivery(long SKUID, System.Int16 deliveryTypeID, long customerID = 0)
        //{
        //    base.CallContext.CurrencyCode = CallContext.CompanyID == 1 ? "KWD" : "SAR";
        //    try
        //    {
        //        if (new ShoppingCartHelpers(CallContext).UpdateCartDelivery(SKUID, deliveryTypeID, customerID))
        //        {
        //            return GetCartPrice(0, customerID);
        //        }

        //        return Json(false);
        //    }
        //    catch (Exception ex)
        //    {
        //        Eduegate.Logger.LogHelper<ShoppingCartController>.Fatal(ex.Message.ToString(), ex);
        //        return Json(new { IsError = false, Message = ex.Message.ToString() });
        //    }
        //}

        [HttpGet]
        public JsonResult UpdateCartCustomerAndStatus(long cartID, long customerID, ShoppingCartStatus cartStatus, PaymentMethods paymentMethod)
        {
            bool IsCartStatusUpdated = false;
            try
            {
                IsCartStatusUpdated = ClientFactory.ShoppingCartServiceClient(base.CallContext).UpdateCartStatus(cartID, cartStatus, paymentMethod.ToString(), ShoppingCartStatus.None);
                IsCartStatusUpdated = ClientFactory.ShoppingCartServiceClient(base.CallContext).UpdateCartCustomerID(cartID, customerID);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<ShoppingCartController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = !IsCartStatusUpdated, Message = ex.Message.ToString() });
            }

            return Json(new { IsError = !IsCartStatusUpdated, data = IsCartStatusUpdated });
        }
    }
}