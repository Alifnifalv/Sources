using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Eduegates;

namespace Eduegate.Web.Library.ViewModels.ShoppingCart
{
    public class CartViewModel : BaseMasterViewModel
    {
        public CartViewModel()
        {
            MasterViewModel = new CartMasterViewModel();
            DetailViewModel = new List<ShoppingCartItemViewModel>();
        }

        public CartMasterViewModel MasterViewModel { get; set; }
        public List<ShoppingCartItemViewModel> DetailViewModel { get; set; }

        public static CartViewModel ToViewModel(CartDTO dto)
        {
            return new CartViewModel()
            {
                MasterViewModel = new CartMasterViewModel()
                {
                    ShoppingCartID = dto.ShoppingCartID,
                    ShippingAddressID = dto.ShippingAddressID,
                    BillingAddressID = dto.BillingAddressID,
                    DeliveryCharge = dto.DeliveryCharge,
                    VoucherValue = dto.VoucherValue,
                    IsVoucherApplied = dto.IsVoucherApplied,
                    IsDeliveryCharge = dto.IsDeliveryCharge,
                    IsEmailDeliveryInCart = dto.IsEmailDeliveryInCart,
                    PaymentMethod = dto.PaymentMethod,
                    CompanyID = dto.CompanyID, 
                    DeliveryDetails = new Inventory.DeliveryAddressViewViewModel()
                    {
                         
                    },
                }
            };
        }

        public static CartDTO ToDTO(CartViewModel vm,int CompanyID = 0)
        {
            return new CartDTO()
            {
                BillingAddressID = vm.MasterViewModel.BillingAddressID,
                DeliveryCharge = vm.MasterViewModel.DeliveryCharge.HasValue ? vm.MasterViewModel.DeliveryCharge.Value : 0,
                VoucherValue = vm.MasterViewModel.VoucherValue,
                ShoppingCartID = vm.MasterViewModel.ShoppingCartID,
                ShippingAddressID = vm.MasterViewModel.ShippingAddressID,
                PaymentMethod = vm.MasterViewModel.PaymentMethod,
                IsDeliveryCharge = vm.MasterViewModel.IsDeliveryCharge,
                IsVoucherApplied = vm.MasterViewModel.IsVoucherApplied,
                Products = ShoppingCartItemViewModel.ToDTOList(vm.DetailViewModel),
                CompanyID = vm.MasterViewModel.CompanyID != null ? vm.MasterViewModel.CompanyID : CompanyID,

            };
        }
    }
}
