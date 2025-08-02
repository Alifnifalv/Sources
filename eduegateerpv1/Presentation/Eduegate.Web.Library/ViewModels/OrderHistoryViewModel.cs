using System;
using System.Collections.Generic;
using Eduegate.Services.Contracts.OrderHistory;
using Eduegate.Framework.Helper;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Globalization;

namespace Eduegate.Web.Library.ViewModels
{
    public class OrderHistoryViewModel
    {
        //public BreadCrumbViewModel BreadCrumbModel { get; set; }

        public Nullable<int> DocumentTypeID { get; set; }
        public Nullable<long> CustomerID { get; set; }
        public Nullable<long> SupplierID { get; set; }
        public long TransactionOrderIID { get; set; }
        public string TransactionNo { get; set; }
        public string TransactionDate { get; set; }
        public DateTime Createddate { get; set; }
        public string Description { get; set; }
        public string PaymentMethod { get; set; }
        public string CurencyCode { get; set; }
        public List<OrderDetailViewModel> OrderDetails { get; set; }
        public string Total { get; set; }
        public string SubTotal { get; set; }
        public string VoucherAmount { get; set; }
        public string DeliveryCharge { get; set; }
        public Nullable<decimal> DiscountAmount { get; set; }
        public Nullable<decimal> DiscountPercentage { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public string TransactionStatus { get; set; }
        public string ActualOrderStatus { get; set; }
        //public UserDTO UserDetail { get; set; }
        public string StatusTransaction { get; set; }
        public string VoucherNo { get; set; }

        public string TransactionTime { get; set; }
        public ContactsViewModel DeliveryAddress { get; set; }
        public string DeliveryText { get; set; }



        public ContactsViewModel BillingAddress { get; set; }


        public long LoyaltyPoints { get; set; }

        public bool ShowResendEmail { get; set; }
        public string DeliveryDisplayText { get; set; }

        public List<KeyValueViewModel> ReplacementActions { get; set; }

        public static List<OrderHistoryViewModel> ToViewModel(List<OrderHistoryDTO> orderHistoryDTOList, string _dateFormat)
        {
            List<OrderHistoryViewModel> orderHistoryList = new List<OrderHistoryViewModel>();
            OrderHistoryViewModel orderHistory = null;
            if (orderHistoryDTOList != null && orderHistoryDTOList.Count > 0)
            {
                foreach (OrderHistoryDTO orderHistoryDTO in orderHistoryDTOList)
                {
                    orderHistory = new OrderHistoryViewModel();
                    orderHistory.OrderDetails = new List<OrderDetailViewModel>();
                    orderHistory.TransactionDate = String.Format(_dateFormat, orderHistoryDTO.TransactionDate);
                    orderHistory.TransactionTime = Convert.ToDateTime(orderHistoryDTO.TransactionDate).ToString("h:mm tt");
                    orderHistory.TransactionNo = orderHistoryDTO.TransactionNo;
                    orderHistory.TransactionOrderIID = orderHistoryDTO.TransactionOrderIID;
                    orderHistory.Description = orderHistoryDTO.Description;
                    orderHistory.CustomerID = orderHistoryDTO.CustomerID;
                    orderHistory.SupplierID = orderHistoryDTO.SupplierID;
                    orderHistory.DocumentTypeID = orderHistoryDTO.DocumentTypeID;
                    orderHistory.PaymentMethod = orderHistoryDTO.PaymentMethod;
                    orderHistory.SubTotal = Utility.FormatDecimal(orderHistoryDTO.SubTotal, 3);
                    orderHistory.Total = Utility.FormatDecimal(orderHistoryDTO.Total, 3);
                    orderHistory.VoucherAmount = Utility.FormatDecimal(orderHistoryDTO.VoucherAmount, 3);
                    orderHistory.TransactionStatus = ((int)((Services.Contracts.Enums.TransactionStatus)Enum.Parse(typeof(Services.Contracts.Enums.TransactionStatus), orderHistoryDTO.TransactionStatus.ToString()))).ToString();
                    //orderHistoryDTO.TransactionStatus.ToString();
                    orderHistory.DeliveryCharge = Utility.FormatDecimal((decimal)orderHistoryDTO.DeliveryCharge, 3);
                    orderHistory.DeliveryText = orderHistoryDTO.DeliveryText.IsNotNullOrEmpty() ? orderHistoryDTO.DeliveryText : "";
                    orderHistory.StatusTransaction = orderHistoryDTO.StatusTransaction;
                    orderHistory.ActualOrderStatus = ((int)((Services.Contracts.Enums.ActualOrderStatus)Enum.Parse(typeof(Services.Contracts.Enums.ActualOrderStatus), orderHistoryDTO.ActualOrderStatus.ToString()))).ToString();
                    orderHistory.VoucherNo = orderHistoryDTO.VoucherNo;
                    //orderHistory.DeliveryAddress = ContactsViewModel.ToVM(orderHistoryDTO.DeliveryAddress);
                    //orderHistory.BillingAddress = ContactsViewModel.ToVM(orderHistoryDTO.BillingAddress);
                    orderHistory.LoyaltyPoints = orderHistoryDTO.LoyaltyPoints;
                    orderHistory.CurencyCode = ResourceHelper.GetValue(orderHistoryDTO.Currency);
                    orderHistory.ShowResendEmail = orderHistoryDTO.DeliveryTypeID == (int)DeliveryTypes.Email ?
                        orderHistoryDTO.ActualOrderStatus == Eduegate.Services.Contracts.Enums.ActualOrderStatus.Delivered ? true : false :
                        orderHistoryDTO.ActualOrderStatus != Eduegate.Services.Contracts.Enums.ActualOrderStatus.Failed ? true : false;
                    if (orderHistoryDTO.OrderDetails != null && orderHistoryDTO.OrderDetails.Count > 0)
                    {
                        foreach (OrderDetailDTO orderDetailDTO in orderHistoryDTO.OrderDetails)
                        {
                            orderHistory.OrderDetails.Add(FromDTO(orderDetailDTO));
                        }
                    }
                    if (orderHistoryDTO.ReplacementActions.IsNotNull() && orderHistoryDTO.ReplacementActions.Count > 0)
                    {
                        orderHistory.ReplacementActions = KeyValueViewModel.FromDTO(orderHistoryDTO.ReplacementActions);
                    }
                    orderHistoryList.Add(orderHistory);
                }
            }
            return orderHistoryList;
        }

        public static OrderDetailViewModel FromDTO(OrderDetailDTO orderDetailDTO)
        {
            Mapper<OrderDetailDTO, OrderDetailViewModel>.CreateMap();
            var vm = Mapper<OrderDetailDTO, OrderDetailViewModel>.Map(orderDetailDTO);
            //vm.DetailIID = orderDetailDTO.DetailIID;
            //vm.ActualQuantity = orderDetailDTO.ActualQuantity.HasValue ? (int) orderDetailDTO.ActualQuantity.Value : 0;
            return vm;
        }
    }
}
