using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Frameworks.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "SupplierBranch", "CRUDModel.ViewModel.Branch")]
    public class SupplierBranchViewModel : BaseMasterViewModel
    {
        public SupplierBranchViewModel()
        {
            Branch = new KeyValueViewModel();
            ReceivingMethods = new KeyValueViewModel();
            ReturnMethods = new KeyValueViewModel();
        }
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [DisplayName("MarketPlace")]
        public bool IsMarketPlace { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [LookUp("LookUps.SupplierBranch")]
        [DisplayName("Branch")]
        [Select2("Branch", "Numeric", false)]
        [HasClaim(HasClaims.HASMULTIBRANCH)]
        public KeyValueViewModel Branch { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox,
            attribs: "ng-disabled='CRUDModel.ViewModel.Branch.IsMarketPlace ? false : true'")]
        [DisplayName("Profit %")]
        public Nullable<decimal> Profit { get; set; }

        public string BranchID { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2,
            attribs: "ng-disabled='CRUDModel.ViewModel.Branch.IsMarketPlace ? false : true'")]
        [LookUp("LookUps.ReceivingMethod")]
        [DisplayName("Receiving Method")]
        [Select2("ReceivingMethod", "Numeric", false)]
        public KeyValueViewModel ReceivingMethods { get; set; }

        public string ReceivingMethodID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2,
            attribs: "ng-disabled='CRUDModel.ViewModel.Branch.IsMarketPlace ? false : true'")]
        [LookUp("LookUps.ReturnMethod")]
        [DisplayName("Return Method")]
        [Select2("ReturnMethod", "Numeric", false)]
        public KeyValueViewModel ReturnMethods { get; set; }

        public string ReturnMethodID { get; set; }
    }
}