using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Web.Library.ViewModels
{
    public class VoucherViewModel : BaseMasterViewModel
    {
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Voucher ID")]
        public long VoucherIID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Voucher No")]
        public string VoucherNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "", "ng-click='CreateVoucher()'")]
        [DisplayName("Create Voucher")]
        public string Create { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.VoucherType")]
        [DisplayName("Voucher Type")]
        public string VoucherTypeID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.CheckBox)]
        //[DisplayName("Is Sharable")]
        //public bool IsSharable { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[LookUp("LookUps.Customer")]
        //[DisplayName("Customer")]
        //[Select2("CustomerID", "Numeric")]
        //public string Customer { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[DisplayName("Customer")]
        //[LookUp("LookUps.Customer")]
        //public string CustomerID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("Customer")]
        //[Select2("Supplier", "Numeric", false)]
        //[LazyLoad("", "Mutual/GetLazyLookUpData?lookType=Customer", "LookUps.Customer")]
        [LazyLoad("", "Inventories/Customer/GetCustomerByCustomerIdAndCR", "LookUps.Customer")]
        [Select2("CustomerID", "Numeric", false, "OnChangeSelect2")]
        [LookUp("LookUps.Customer")]
        [QuickSmartView("Customer")]
        [QuickCreate("Create,Frameworks/CRUD/Create?screen=Customer, $event,Create Customer, Customer")]
        public KeyValueViewModel Customer { get; set; }
        public string CustomerID { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(7, ErrorMessage = "Maximum Length should be within 7!")]
        [DisplayName("Amount")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "Please enter a valid amount")]
        public decimal Amount { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Expiry Date")]
        public string ExpiryDate { get; set; }
        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Minimum Amount")]
        public Nullable<decimal> MinimumAmount { get; set; }
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Current Balance")]
        public decimal CurrentBalance { get; set; }
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Description")]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        public string Description { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.VoucherStatus")]
        [DisplayName("Status")]
        public string StatusID { get; set; }
        public int? CompanyID { get; set; }

        public static VoucherViewModel FromDTO(VoucherMasterDTO dto)
        {
            return new VoucherViewModel()
            {
                Amount = dto.VoucherAmount,
                CurrentBalance = dto.CurrentBalance,
                ExpiryDate = dto.ValidTillDate.IsNull() ? null : Convert.ToDateTime(dto.ValidTillDate).ToString("MM/dd/yyyy"),
                VoucherTypeID = dto.VoucherType,
                VoucherIID = dto.VoucherID,
                VoucherNo = dto.VoucherNo,
                StatusID = dto.StatusID.ToString(),
                Description = dto.Description,
                CustomerID = dto.CustomerID.IsNull() ? null : Convert.ToString(dto.CustomerID),
                Customer = dto.Customer.IsNull() ? null : new KeyValueViewModel()
                {
                    Key = dto.Customer.Key,
                    Value = dto.Customer.Value,
                },
                CompanyID = dto.CompanyID == null ? null : dto.CompanyID,
                //MinimumAmount = dto.MinAmount,
            };
        }

        public static VoucherMasterDTO ToDTO(VoucherViewModel vm)
        {
            return new VoucherMasterDTO()
            {
                VoucherAmount = vm.Amount,
                CurrentBalance = vm.CurrentBalance,
                ValidTillDate = vm.ExpiryDate != null ? Convert.ToDateTime(vm.ExpiryDate) : (DateTime?)null,
                //IsSharable = vm.IsSharable,
                VoucherID = vm.VoucherIID,
                VoucherNo = vm.VoucherNo,
                VoucherType = vm.VoucherTypeID,
                StatusID = byte.Parse(vm.StatusID),
                Description = vm.Description,
                CustomerID = vm.Customer.IsNull() ? (long?)null : Convert.ToInt64(vm.Customer.Key), // vm.CustomerID.IsNull() ? (long?)null : Convert.ToInt32(vm.CustomerID)
                Customer = vm.Customer.IsNull() ? null : new KeyValueDTO()
                {
                    Key = vm.Customer.Key,
                    Value = vm.Customer.Value,
                },
                CompanyID = vm.CompanyID == null ? null : vm.CompanyID,
                //MinAmount = vm.MinimumAmount,
            };
        }
    }
}