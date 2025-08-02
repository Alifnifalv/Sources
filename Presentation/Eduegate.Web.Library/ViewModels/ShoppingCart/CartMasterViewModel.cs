using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Frameworks.Mvc.Attributes;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Web.Library.ViewModels.Inventory;

namespace Eduegate.Web.Library.ViewModels.ShoppingCart
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "CartMaster", "CRUDModel.Model.MasterViewModel")]
    [DisplayName("Cart Details")]
    public class CartMasterViewModel : BaseMasterViewModel
    {
        public CartMasterViewModel()
        {
            DeliveryTypes = new List<DeliveryTypeViewModel>();
            DeliveryDetails = new DeliveryAddressViewViewModel(); 
        }

        [ControlType(Framework.Enums.ControlTypes.Label, "onecol-header-left")]
        [DisplayName("Cart ID")]
        public long ShoppingCartID { get; set; }

        public string SubTotal { get; set; }

        public string Total { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("Customer")]
        [Select2("Customer", "Numeric", false, "OnChangeSelect2", false, "ng-disabled=(CRUDModel.Model.MasterViewModel.ShoppingCartID!=0)")]
        [LazyLoad("", "Inventories/Customer/GetCustomerByCustomerIdAndCR", "LookUps.Customer")]
        [QuickSmartView("Customer")]
        [QuickCreate("Customer")]
        public KeyValueViewModel Customer { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "", "ng-click ='CreateCart(CRUDModel.Model.MasterViewModel.Customer.Key)' ng-hide='(CRUDModel.Model.MasterViewModel.ShoppingCartID!=0)'")]
        [DisplayName("Create Cart")]
        public string Cart { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Currency", "Numeric", false)]
        [DisplayName("Currency")]
        [LookUp("LookUps.Currency")]
        [HasClaim(HasClaims.HASMULTICURRENCY)]
        public KeyValueViewModel Currency { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("")]
        public new string UserMessage { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox, "onecol-header-left")]
        //[DisplayName("Payments")]
        public string PaymentMethod { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [DisplayName("Voucher Applied")]
        public bool IsVoucherApplied { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "", "ng-disabled='!CRUDModel.Model.MasterViewModel.IsVoucherApplied'")]
        [DisplayName("Voucher Value")]
        public string VoucherValue { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "", "ng-click ='ResetCart(CRUDModel.Model.MasterViewModel.ShoppingCartID,10)' ng-hide='(CRUDModel.Model.MasterViewModel.ShoppingCartID==0)'")]
        [DisplayName("Reset")]
        public string Reset { get; set; }

        public Nullable<long> ShippingAddressID { get; set; }

        public Nullable<long> BillingAddressID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.CheckBox)]
        //[DisplayName("Delivery Charge")]
        public bool IsDeliveryCharge { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox, "", "ng-disabled='!CRUDModel.Model.MasterViewModel.IsDeliveryCharge'")]
        //[DisplayName("Delivery Charge")]
        public decimal? DeliveryCharge { get; set; }        

        //[ControlType(Framework.Enums.ControlTypes.CheckBox)]
        //[DisplayName("Deleted")]
        public bool IsCartItemDeleted { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.CheckBox)]
        //[DisplayName("Cart item out-of-stock")]
        public bool IsCartItemOutOfStock { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.CheckBox)]
        //[DisplayName("Quantity adjusted")]
        public bool IsCartItemQuantityAdjusted { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.CheckBox)]
        //[DisplayName("Physical Items")]
        public bool IsOnlineBranchPhysicalCartItems { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.CheckBox)]
        //[DisplayName("Delivery charge assigned")]
        public bool IsDeliveryChargeAssigned { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.CheckBox)]
        //[DisplayName("Email Delivery")]
        public bool IsEmailDeliveryInCart { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.CheckBox)]
        //[DisplayName("Store pickup")]
        public bool IsStorePickUpInCart { get; set; }
        public Nullable<int>  CompanyID {get; set; }

        public List<DeliveryTypeViewModel> DeliveryTypes { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "DeliveryDetails", "CRUDModel.Model.MasterViewModel.DeliveryDetails")]
        [DisplayName("Delivery Details")]
        public DeliveryAddressViewViewModel DeliveryDetails { get; set; }


        public Nullable<Services.Contracts.Enums.DocumentReferenceTypes> DocumentReferenceTypeID { get; set; }

        public static CartMasterViewModel ToViewModel(CartDTO dto)
        {
            Mapper<CartDTO, CartMasterViewModel>.CreateMap();
            var mapper = Mapper<CartDTO, CartMasterViewModel>.Map(dto);
            return mapper;
        }
    }
}
