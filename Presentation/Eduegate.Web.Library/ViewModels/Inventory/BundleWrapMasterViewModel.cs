using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Frameworks.Mvc.Attributes;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels.Inventory
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "BundleWrapMaster", "CRUDModel.Model.MasterViewModel")]
    [DisplayName("Bundle Wrap Details")]
    public class BundleWrapMasterViewModel : BaseMasterViewModel
    {
        public BundleWrapMasterViewModel()
        {
            TransactionStatus = new KeyValueViewModel();
            IsSummaryPanel = false;
            DocumentStatus = new KeyValueViewModel { Key = "1", Value = "Draft" };
        }

        public long? ReferenceHeadID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Branch", "String", false, "BranchChange($event, $element)", false )]
        [CustomDisplay("Branch")]
        [LookUp("LookUps.Branch")]
        [HasClaim(HasClaims.HASMULTIBRANCH)]
        public KeyValueViewModel Branch { get; set; }
        public long BranchID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Date")]
        public String TransactionDate { get; set; }

        public long TransactionHeadIID { get; set; }
        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        //[Select2("DocumentType", "String", false, "DocumentTypeChange($event, $element)")]
        //[DisplayName("Document")]
        //[LookUp("LookUps.DocumentType")]
        //[HasClaim(HasClaims.HASMULTIDCOUMENT)]
        //public KeyValueViewModel DocumentType { get; set; }

        public long DocumentTypeID { get; set; }
        public Nullable<Services.Contracts.Enums.DocumentReferenceTypes> DocumentReferenceTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("BNWPNo")]
        public string TransactionNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("DocumentStatus", "Numeric", false)]
        [LookUp("LookUps.DocumentStatus")]
        [DisplayName("Status")]
        public KeyValueViewModel DocumentStatus { get; set; }
        public byte? TransactionStatusID { get; set; }
        public byte? CurrentStatusID { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("Currency", "Numeric", false)]
        //[DisplayName("Currency")]
        //[LookUp("LookUps.Currency")]
        //[HasClaim(HasClaims.HASMULTICURRENCY)]
        //public KeyValueViewModel Currency { get; set; }


        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("Currency", "Numeric", false)]
        //[LookUp("LookUps.TransactionStatus")]
        //[DisplayName("TransactionStatus")]
        public KeyValueViewModel TransactionStatus { get; set; }


        public int DeliveryTypeID { get; set; }
        public int EntitlementID { get; set; }

        //public decimal DeliveryCharge { get; set; }
        //public decimal? Discount { get; set; }
        //public decimal? DiscountPercentage { get; set; }
        public bool IsTransactionCompleted { get; set; }

        public Nullable<int> CompanyID { get; set; } 
    }
}
