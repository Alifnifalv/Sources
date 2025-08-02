using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.Accounts.Assets
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "", "$root.AssetDetailViewModel.TransactionSerialMaps")]
    [DisplayName("Serials")]
    [Pagination(10, "default")]
    public class AssetPurchaseInvoiceSerialMapViewModel : BaseMasterViewModel
    {
        public AssetPurchaseInvoiceSerialMapViewModel()
        {
            IsRowDisabled = false;
            //SLNo = null;
        }

        public long AssetTransactionSerialMapIID { get; set; }
        public long? TransactionDetailID { get; set; }
        public long? AssetID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [CustomDisplay("Sr.No")]
        public long SerialNo { get; set; }
        //public decimal? SLNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("AssetNumber")]
        public string AssetSequenceCode { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "", optionalAttribs: "ng-disabled='gridModel.IsRowDisabled'")]
        [CustomDisplay("AssetSerialNumber")]
        public string AssetSerialNumber { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "", optionalAttribs: "ng-disabled='gridModel.IsRowDisabled'")]
        [CustomDisplay("AssetTag")]
        public string AssetTag { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, attribs: "ng-disabled='gridModel.IsRowDisabled'")]
        [CustomDisplay("DateOfFirstUse")]
        public string FirstUseDateString { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, Attributes2 = "ng-change='GridCostAmountChanges(CRUDModel.ViewModel)' ng-disabled='gridModel.IsRowDisabled'")]
        [CustomDisplay("CostAmount(Net)")]
        public decimal? CostPrice { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "", optionalAttribs: "ng-disabled='gridModel.IsRowDisabled'")]
        [CustomDisplay("ExpectedUsefulLife")]
        public int? ExpectedLife { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "", optionalAttribs: "ng-disabled='gridModel.IsRowDisabled'")]
        [CustomDisplay("RateOfDepreciation(Percentage)")]
        public decimal? DepreciationRate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "", optionalAttribs: "ng-disabled='gridModel.IsRowDisabled'")]
        [CustomDisplay("ExpectedNetRealisableScrapValue")]
        public decimal? ExpectedScrapValue { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "", optionalAttribs: "ng-disabled='gridModel.IsRowDisabled'")]
        [CustomDisplay("AccumulatedDepreciation")]
        public decimal? AccumulatedDepreciationAmount { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Supplier", "String", false, "", false, optionalAttribute1: "ng-disabled='gridModel.IsRowDisabled'")]
        [CustomDisplay("Supplier")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Supplier", "LookUps.Supplier")]
        public KeyValueViewModel Supplier { get; set; }
        public long? SupplierID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "", optionalAttribs: "ng-disabled='gridModel.IsRowDisabled'")]
        [CustomDisplay("BillNumber")]
        public string BillNumber { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, attribs: "ng-disabled='gridModel.IsRowDisabled'")]
        [CustomDisplay("BillDate")]
        public string BillDateString { get; set; }

        public bool IsRowDisabled { get; set; }

        public string ErrorMessage { get; set; }

        public long? AssetSerialMapID { get; set; }
    }
}