using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.Accounts.Assets
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "AssetSerialEntry", "CRUDModel.ViewModel.AssetSerialEntry")]
    [DisplayName("Serial Entries")]
    public class AssetEntrySerialViewModel : BaseMasterViewModel
    {
        public AssetEntrySerialViewModel()
        {
            SerialEntryGrids = new List<AssetSerialEntryGridViewModel>() { new AssetSerialEntryGridViewModel() };
        }

        public long TransactionDetailIID { get; set; }
        public long? AssetID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "", attribs: "ng-change=QuantityChanges(CRUDModel.ViewModel)")]
        [CustomDisplay("Quantity")]
        public decimal? Quantity { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid, "fullwidth", Attributes4 = "colspan=12")]
        [CustomDisplay("SerialMaps")]
        public List<AssetSerialEntryGridViewModel> SerialEntryGrids { get; set; }
    }
}