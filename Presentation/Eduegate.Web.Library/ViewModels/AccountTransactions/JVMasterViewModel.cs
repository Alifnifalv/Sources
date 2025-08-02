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
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "JVTransactionHead", "CRUDModel.Model.MasterViewModel")]
    [DisplayName("General")]
    public class JVMasterViewModel : BaseMasterViewModel
    {
        public JVMasterViewModel()
        {
            TransactionStatus = new KeyValueViewModel();
            DocumentStatus = new KeyValueViewModel();
            DocumentType= new KeyValueViewModel();
        }


        public long AccountTransactionHeadIID { get; set; }
        public Nullable<int> DocumentTypeID { get; set; }
        public Nullable<long> DocumentStatusID { get; set; }
        public Nullable<int> PaymentModeID { get; set; }
        public Nullable<int> CurrencyID { get; set; }
        public double? ExchangeRate { get; set; }
        public Nullable<bool> IsPrePaid { get; set; }
        public Nullable<byte> TransactionStatusID { get; set; }
        
        public double? AdvanceAmount { get; set; }
        public Nullable<int> CostCenterID { get; set; }
        public double? AmountPaid { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, "", "ng-change='GetNextTransactionNumber(CRUDModel.Model)'")]
        [DisplayName("Voucher Date")]
        public String TransactionDate { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("PaymentModes", "Numeric", false, "DocumentTypeChange($event, $element)")]
        //[LookUp("LookUps.PaymentModes")]
        //[DisplayName("PaymentModes")]
        public KeyValueViewModel PaymentModes { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("DocumentType", "Numeric", false, "", false, optionalAttribute1: "ng-disabled='true'")]
        [LookUp("LookUps.DocumentType")]
        [DisplayName("Type")]
        [HasClaim(HasClaims.HASMULTIDCOUMENT)]
        public KeyValueViewModel DocumentType { get; set; }


        public KeyValueViewModel Currency { get; set; }
        public KeyValueViewModel CostCenter { get; set; }



        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Voucher Number")]
        public String TransactionNumber { get; set; }

        public KeyValueViewModel TransactionStatus { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("DocumentStatus", "Numeric", false)]
        [LookUp("LookUps.DocumentStatus")]
        [DisplayName("Status")]
        public KeyValueViewModel DocumentStatus { get; set; }

       
        public KeyValueViewModel Account { get; set; }
        public Nullable<long> AccountID { get; set; }
        public KeyValueViewModel DetailAccount { get; set; }
        public Nullable<long> DetailAccountID { get; set; }
        public decimal Amount { get; set; }
        public String Allocate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(250, ErrorMessage = "Maximum Length should be within 250!")]
        [DisplayName("Remarks")]
        public string Remarks { get; set; }


    }
}
