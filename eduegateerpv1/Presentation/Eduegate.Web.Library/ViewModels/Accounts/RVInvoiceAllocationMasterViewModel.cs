using Eduegate.Framework.Mvc.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Accounts
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "ReceiptAllocation", "CRUDModel.Model.MasterViewModel")]
    [DisplayName("Receipt Details")]
    public class RVInvoiceAllocationMasterViewModel : BaseMasterViewModel
    {
        public long ReceivableIID { get; set; }
        public int? DocumentReferenceTypeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Document Type")]
        public string DocumentType { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Transaction Number")]
        public string TransactionNumber { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Customer Number")]
        public string CustomerName { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[DisplayName("Customer Account")]
        //[Select2("CustomerAccounts", "Numeric", false, "LoadCustomerPendingInvoices")]
        //[LazyLoad("", "Accounts/RVMission/Get_AllCustomers_Accounts", "LookUps.CustomerAccounts")]
        //public KeyValueViewModel DetailAccount { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        [DisplayName("Remarks")]
        public string Remarks { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Amount")]
        public decimal Amount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "", "ng-click ='AllocateAmount(CRUDModel.Model)' ng-hide='(CRUDModel.Model.MasterViewModel.Amount<=0)'")]
        [DisplayName("Allocate")]
        public bool Allocate { get; set; }
    }
}
