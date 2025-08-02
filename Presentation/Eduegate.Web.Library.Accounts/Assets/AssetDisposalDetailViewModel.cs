using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;

namespace Eduegate.Web.Library.Accounts.Assets
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "", "CRUDModel.Model.DetailViewModel")]
    public class AssetDisposalDetailViewModel : BaseMasterViewModel
    {
        public long TransactionDetailID { get; set; }
        public long? TransactionHeadID { get; set; }
        public long? AssetTransactionSerialMapID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [CustomDisplay("Sr.No")]
        public long SerialNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width detailview-grid")]
        [Select2("Asset", "String", false, "AssetChanges(detail, CRUDModel.Model.MasterViewModel)")]
        [CustomDisplay("Asset")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Assets", "LookUps.Assets")]
        public KeyValueViewModel Asset { get; set; }
        public long? AssetID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("AssetDescription")]
        public string AssetDescription { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width detailview-grid")]
        [Select2("AssetSerialMap", "String", false, "AssetSerialMapChanges(detail, CRUDModel.Model.MasterViewModel)")]
        [LookUp("LookUps.AssetSerialMapCodes")]
        [CustomDisplay("SequenceMap")]
        public KeyValueViewModel AssetSerialMap { get; set; }
        public long? AssetSerialMapID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Asset Sequence Code")]
        public string AssetSequenceCode { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [DisplayName("Last Dep Date")]
        public string LastDepDateString { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Cost Value")]
        public decimal? CostAmount { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [DisplayName("Accumulated Depreciation Amount")]
        public decimal? AccumulatedDepreciationAmount { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Net Value")]
        public decimal? LastNetValue { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "", Attributes2 = "DisposedAmountChanges(detail)")]
        [CustomDisplay("Sold / Disposed Amount")]
        public decimal? DisposibleAmount { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Amount Difference")]
        public decimal? DifferenceAmount { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[CustomDisplay("Quantity")]
        public decimal? Quantity { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Button, "x-small-col-width", "ng-click='InsertGridRow($index, ModelStructure.Model.DetailViewModel[0], CRUDModel.Model.DetailViewModel)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "x-small-col-width", "ng-click='RemoveGridRow($index, CRUDModel.Model.DetailViewModel[0], CRUDModel.Model.DetailViewModel)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}