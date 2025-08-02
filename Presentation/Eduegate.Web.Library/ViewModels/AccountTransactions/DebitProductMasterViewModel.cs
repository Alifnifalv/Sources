using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Frameworks.Mvc.Attributes;
using Eduegate.Framework.Helper.Enums;

namespace Eduegate.Web.Library.ViewModels.AccountTransactions
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "DebitProductTransactionHead", "CRUDModel.Model.MasterViewModel")]
    [DisplayName("General")]
    public class DebitProductMasterViewModel : BaseMasterViewModel
    {
        public DebitProductMasterViewModel()
        {
            TransactionStatus = new KeyValueViewModel();
            DocumentStatus = new KeyValueViewModel();
            DocumentType= new KeyValueViewModel();
        }


        public long AccountTransactionHeadIID { get; set; }
        public Nullable<int> DocumentTypeID { get; set; }
        public Nullable<long> DocumentStatusID { get; set; }
        public Nullable<int> PaymentModeID { get; set; }
        public Nullable<long> AccountID { get; set; }
        public Nullable<int> CurrencyID { get; set; }
        public double? ExchangeRate { get; set; }
        public Nullable<bool> IsPrePaid { get; set; }
        public Nullable<int> DocumentReferenceTypeID { get; set; }

        public double? AdvanceAmount { get; set; }
        public Nullable<int> CostCenterID { get; set; }
        public double? AmountPaid { get; set; }
        public Nullable<byte> TransactionStatusID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker,"", "ng-change='GetNextTransactionNumber(CRUDModel.Model)'")]
        [DisplayName("Date")]
        public String TransactionDate { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("PaymentModes", "Numeric", false, "DocumentTypeChange($event, $element)")]
        //[LookUp("LookUps.PaymentModes")]
        //[DisplayName("PaymentModes")]
        public KeyValueViewModel PaymentModes { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("DocumentType", "Numeric",false, "", false, optionalAttribute1: "ng-disabled='true'")]
        [LookUp("LookUps.DocumentType")]
        [DisplayName("Type")]
        [HasClaim(HasClaims.HASMULTIDCOUMENT)]
        public KeyValueViewModel DocumentType { get; set; }
             
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Number")]
        public String TransactionNumber { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Currency", "Numeric", false)]
        [DisplayName("Currency")]
        [LookUp("LookUps.Currency")]
        [HasClaim(HasClaims.HASMULTICURRENCY)]
        public KeyValueViewModel Currency { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("CostCenter", "Numeric", false)]
        [DisplayName("CostCenter")]
        [LookUp("LookUps.CostCenter")]
        public KeyValueViewModel CostCenter { get; set; }

       

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("DocumentStatus", "Numeric", false)]
        [LookUp("LookUps.DocumentStatus")]
        [DisplayName("Document Status")]
        public KeyValueViewModel DocumentStatus { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("Credit Account")]
        [Select2("Accounts", "Numeric")]
        [LazyLoad("", "AssetMaster/AccountCodesSearch", "LookUps.Accounts")]
        public KeyValueViewModel Account { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("Cust/SupplierAccount")]
        [Select2("Accounts", "Numeric", false, "AccountCodeChange")]
        [LazyLoad("", "AssetMaster/AccountCodesSearch", "LookUps.Accounts")]
        public KeyValueViewModel DetailAccount { get; set; }
        public Nullable<long> DetailAccountID { get; set; }


        
        public double? Amount { get; set; }
        public String Allocate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(250, ErrorMessage = "Maximum Length should be within 250!")]
        [DisplayName("Remarks")]
        public string Remarks { get; set; }

        public KeyValueViewModel TransactionStatus { get; set; }



    }
}
