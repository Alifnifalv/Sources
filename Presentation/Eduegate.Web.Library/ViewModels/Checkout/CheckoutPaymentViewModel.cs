using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Services.Contracts.Checkout;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Web.Library.ViewModels.Checkout
{
    public class CheckoutPaymentViewModel : BaseViewModel
    {
        public CheckoutPaymentViewModel()
        {
            VoucherNo = "";
            SelectedPaymentOption = null;
            VoucherAmount = 0;
            SelectedBillingAddress = "";
            ShowCOD = false;
            //currencyID = "123";
            PaymentMethods = new List<PaymentMethodViewModel>();
        }

        public decimal CartTotal { get; set; }
        public decimal DeliveryCost { get; set; }

        public string VoucherNo { get; set; }
        public decimal VoucherAmount { get; set; }
        public string SelectedPaymentOption { get; set; }
        public string SelectedShippingAddress { get; set; }
        public string SelectedBillingAddress { get; set; }

        public string ShoppingCartID { get; set; }
        public decimal WalletAmount { get; set; }

        public string currencyID { get; set; }

        public decimal SubTotal { get; set; }

        public bool ShowCOD { get; set; }

        public List<PaymentMethodViewModel> PaymentMethods { get; set; }

        public bool IsProccedToPayment { get; set; }

        public static CheckoutPaymentDTO ToDTO(CheckoutPaymentViewModel vm)
        {
            return new CheckoutPaymentDTO()
            {
                ShoppingCartID = vm.ShoppingCartID,
                VoucherNo = vm.VoucherNo,
                VoucherAmount = vm.VoucherAmount,
                SelectedPaymentOption = vm.SelectedPaymentOption,
                SelectedShippingAddress = vm.SelectedShippingAddress,
                SelectedBillingAddress = vm.SelectedBillingAddress,
                WalletAmount = vm.WalletAmount,
                CurrencyID = int.Parse(vm.currencyID),
            };
        }

        public static CheckoutPaymentViewModel ToViewModel(CartDTO dto, CallContext _context)
        {
            return new CheckoutPaymentViewModel()
            {
                currencyID = _context.CurrencyCode,
                CartTotal = Convert.ToDecimal(dto.Total),
                ShoppingCartID = dto.ShoppingCartID.ToString(),
                DeliveryCost = dto.DeliveryCharge,
                CurrencyCode = _context.CurrencyCode,
                SubTotal = Convert.ToDecimal(dto.SubTotal),
            };
        }
    }
}
