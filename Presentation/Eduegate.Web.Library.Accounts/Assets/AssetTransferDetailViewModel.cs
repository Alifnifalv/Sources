using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;

namespace Eduegate.Web.Library.Accounts.Assets
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "", "CRUDModel.Model.DetailViewModel")]
    public class AssetTransferDetailViewModel : BaseMasterViewModel
    {
        public AssetTransferDetailViewModel()
        {
            IsSerialNumberOnAssetEntry = true;
            TransactionSerialMaps = new List<AssetTransactionSerialMapViewModel>() { new AssetTransactionSerialMapViewModel() };
            IsRequiredSerialNumber = false;
            IsShowDynamicPopup = false;
        }

        public long TransactionDetailID { get; set; }

        public long? TransactionHeadID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [CustomDisplay("Sr.No")]
        public long SerialNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width detailview-grid")]
        [Select2("Asset", "String", false, "AssetChanges(detail, CRUDModel.Model.MasterViewModel)", optionalAttribute1: "ng-disabled='CRUDModel.Model.MasterViewModel.ReferenceTransactionHeaderID > 0'")]
        [CustomDisplay("Asset")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Assets", "LookUps.Assets")]
        public KeyValueViewModel Asset { get; set; }
        public long? AssetID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("AssetDescription")]
        public string AssetDescription { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("AvailableQuantity")]
        public decimal? AvailableQuantity { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, "", Attributes2 = "CheckLastDayOfMonth(detail, \"CutOffDateString\")")]
        [CustomDisplay("Cut-Off Date")]
        public string CutOffDateString { get; set; }

        [Required]
        [RegularExpression("^[1-9][0-9]*$", ErrorMessage = "Invalid format")]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textright", attribs: "ng-disabled='CRUDModel.Model.MasterViewModel.ReferenceTransactionHeaderID > 0'", Attributes2 = "QuantityChanges(detail)")]
        [DisplayName("Quantity")]
        public decimal? Quantity { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.IconButton, "", "ng-click='BringAssetSerialPopup(detail)'", Attributes2 = "colspan=4")]
        [DisplayName("Serials")]
        public List<AssetTransactionSerialMapViewModel> TransactionSerialMaps { get; set; }

        public decimal? Amount { get; set; }

        public bool IsShowDynamicPopup { get; set; }

        public bool? IsSerialNumberOnAssetEntry { get; set; }

        public bool? IsRequiredSerialNumber { get; set; }

        public long? AccountID { get; set; }
        public long? AssetGlAccID { get; set; }
        public long? AccumulatedDepGLAccID { get; set; }
        public long? DepreciationExpGLAccID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Button, "x-small-col-width", "ng-click='InsertGridRow($index, ModelStructure.DetailViewModel, CRUDModel.Model.DetailViewModel)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "x-small-col-width", "ng-click='RemoveGridRow($index, CRUDModel.Model.DetailViewModel[0], CRUDModel.Model.DetailViewModel)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}