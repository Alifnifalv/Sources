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
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "RVMissionTransactionHead", "CRUDModel.Model.MasterViewModel")]
    [DisplayName("General")]
    public class RVMissionMasterViewModel: BaseMasterViewModel
    {
        public RVMissionMasterViewModel()
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
        public Nullable<bool> IsPrePaid { get; set; }
        public Nullable<byte> TransactionStatusID { get; set; }
        public Nullable<int> DocumentReferenceTypeID { get; set; }
        

        public double? AdvanceAmount { get; set; }
        public Nullable<int> CostCenterID { get; set; }
        public double? AmountPaid { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker,"", "DocumentTypeChange($event, $element)" )]
        [DisplayName("RVDate")]
        public String TransactionDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("PaymentModes", "Numeric", false, "DocumentTypeChange($event, $element)")]
        [LookUp("LookUps.PaymentModes")]
        [DisplayName("PaymentModes")]
        public KeyValueViewModel PaymentModes { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
       // [Select2("DocumentType", "Numeric", false, "DocumentTypeChange($event, $element)")]

        [Select2("DocumentType", "Numeric",false,"",false,optionalAttribute1: "ng-disabled='true'")]
        [LookUp("LookUps.DocumentType")]
        [DisplayName("RVType")]
        [HasClaim(HasClaims.HASMULTIDCOUMENT)]
        public KeyValueViewModel DocumentType { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("RVNumber")]
        public String TransactionNumber { get; set; }        

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Currency", "Numeric", false)]
        [DisplayName("Currency")]
        [LookUp("LookUps.Currency")]
        [HasClaim(HasClaims.HASMULTICURRENCY)]
        public KeyValueViewModel Currency { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(7, ErrorMessage = "Maximum Length should be within 7!")]
        [DisplayName("ExchangeRate")]
        public double? ExchangeRate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("CostCenter", "Numeric", false)]
        [DisplayName("CostCenter")]
        [LookUp("LookUps.CostCenter")]
        public KeyValueViewModel CostCenter { get; set; }


        public KeyValueViewModel TransactionStatus { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("DocumentStatus", "Numeric", false)]
        [LookUp("LookUps.DocumentStatus")]
        [DisplayName("Document Status")]
        public KeyValueViewModel DocumentStatus { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("Driver Accounts")]
        [Select2("DriverAccounts", "Numeric", false, "AccountCodeChange")]
        [LazyLoad("", "Accounts/RVMission/Get_AllDrivers_Accounts", "LookUps.DriverAccounts")]
     
        public KeyValueViewModel DetailAccount { get; set; }
        public Nullable<long> DetailAccountID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("Debit Accounts")]
        [Select2("DebitAccounts", "Numeric")]
        [LazyLoad("", "Accounts/RVMission/GetChildAccounts_ByParentAccountId", "LookUps.DebitAccounts")]
        public KeyValueViewModel Account { get; set; }
        public Nullable<long> AccountID { get; set; }



        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Amount must be numeric")]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "", "ng-blur='AllocationAmountChange($event, $element,CRUDModel.Model)'")]
        //[MaxLength(7, ErrorMessage = "Maximum Length should be within 7!")]
        [DisplayName("Amount")]
        public decimal? Amount { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Button, "", "ng-click ='AllocateAmount(CRUDModel.Model)' ng-hide='(CRUDModel.Model.MasterViewModel.Amount<=0)'")]
        [DisplayName("Allocate")]
        public String Allocate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(250, ErrorMessage = "Maximum Length should be within 250!")]
        [DisplayName("Remarks")]
        public string Remarks { get; set; }


    }
}
