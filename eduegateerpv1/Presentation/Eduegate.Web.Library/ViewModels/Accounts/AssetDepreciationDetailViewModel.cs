using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Mvc.Attributes;


namespace Eduegate.Web.Library.ViewModels.Accounts
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "AssetTransactionDetails", "CRUDModel.Model.DetailViewModel")]
    public class AssetDepreciationDetailViewModel : BaseMasterViewModel
    {
        public AssetDepreciationDetailViewModel()
        {
        }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "ex-small-col-width", "ng-model='detail.IsRowSelected'")]
        [DisplayName("IsRowSelected")]
        public bool IsRowSelected { get; set; }
        public bool SelectAll { get; set; }
        public long AssetIID { get; set; }
        public Nullable<long> AssetCategoryID { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "ex-small-col-width")]
        [DisplayName("Sr.No")]
        public long SerialNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "small-col-width detailview-grid", "ng-change='OnDepreciationAssetCodeChange($event, $element)'")]
        [Select2("AssetCode", "String", false, "OnDepreciationAssetCodeChange")]
        [DisplayName("AssetCode")]
        [LazyLoad("", "AssetMaster/AssetCodesSearch", "LookUps.AssetCode")]
        public KeyValueViewModel AssetCode { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required]
        //[ControlType(Framework.Enums.ControlTypes.TextBox, "ex-small-col-width textright serialnum-wrap", "ng-disabled='detail.AssetCode.Key == null || detail.StartDate==null'", "", optionalAttribs: "OnDepreciationQuatityChange")]
        [ControlType(Framework.Enums.ControlTypes.Label, "ex-small-col-width")]
        [DisplayName("Quantity")]
        public int Quantity { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, "small-col-width ", "ng-disabled='detail.AssetCode.Key == null'", "", optionalAttribs:"OnDepreciationDateChange")]
        [DisplayName("Date To Calc Depr")]
        public String StartDate { get; set; }

        public string CategoryName { get; set; }

        public KeyValueViewModel AssetGlAccount { get; set; }
        public long AssetGlAccID { get; set; }



        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "ex-small-col-width")]
        [DisplayName("DeprAmt")]
        public Nullable<decimal> TotalDepreciationAmount { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Label, "ex-small-col-width")]
        //[DisplayName("DeprAmt")]
        public Nullable<decimal> Amount { get; set; }


        public long DetailIID { get; set; }
        public Nullable<long> HeadID { get; set; }
        public Nullable<long> AssetID { get; set; }
        public Nullable<long> AccountID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "ex-small-col-width")]
        [DisplayName("AccDepr")]
        public Nullable<Decimal> AccumulatedDepreciation { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("AssetDate")]
        public String AssetStartDate { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "ex-small-col-width")]
        [DisplayName("AssetAmt")]
        public Nullable<Decimal> AssetValue { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "ex-small-col-width")]
        [DisplayName("NetDepr")]
        public Nullable<Decimal> NetDeprecition { get; set; }

        public Nullable<int> DepreciationYears { get; set; }
        public Nullable<int> AssetQuanity { get; set; }



        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.Model.DetailViewModel[0], CRUDModel.Model.DetailViewModel)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, CRUDModel.Model.DetailViewModel[0], CRUDModel.Model.DetailViewModel)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}
