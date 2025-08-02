using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;

namespace Eduegate.Web.Library.Accounts.Assets
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "", "$root.AssetDetailViewModel.TransactionSerialMaps")]
    [DisplayName("Serials")]
    public class AssetTransactionSerialMapViewModel : BaseMasterViewModel
    {
        public long AssetTransactionSerialMapIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [CustomDisplay("SI")]
        public long SerialNo { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width detailview-grid")]
        [Select2("AssetSerialMap", "String", false, "AssetSerialMapChanges(gridModel, CRUDModel.Model.MasterViewModel)")]
        [CustomDisplay("SequenceMap")]
        [LookUp("LookUps.AssetSerialMapCodes")]
        public KeyValueViewModel AssetSerialMap { get; set; }
        public long? AssetSerialMapID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Asset Sequence Code")]
        public string AssetSequenceCode { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [DisplayName("Serial No")]
        public string SerialNumber { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [DisplayName("Asset Tag")]
        public string AssetTag { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [DisplayName("First Use Date")]
        public string FirstUseDateString { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [DisplayName("Last Dep Date")]
        public string LastDepDateString { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [DisplayName("Accumulated Depreciation Amount")]
        public decimal? AccumulatedDepreciationAmount { get; set; }

        public long? TransactionDetailID { get; set; }
        public long? AssetID { get; set; }
        public bool? IsRequiredSerialNumber { get; set; }

        public decimal? CostPrice { get; set; }
        public decimal? NetValue { get; set; }

        public int? ExpectedLife { get; set; }
        public decimal? DepreciationRate { get; set; }
        public decimal? ExpectedScrapValue { get; set; }

        public string ErrorMessage { get; set; }
    }
}