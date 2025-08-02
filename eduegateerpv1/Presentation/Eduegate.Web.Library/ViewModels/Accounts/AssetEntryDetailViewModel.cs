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
    public class AssetEntryDetailViewModel : BaseMasterViewModel
    {
        public AssetEntryDetailViewModel()
        {

        }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "ex-small-col-width", "ng-model='detail.IsRowSelected'")]
        [DisplayName("IsRowSelected")]
        public bool IsRowSelected { get; set; }
        public bool SelectAll { get; set; }
        public Nullable<long> AssetCategoryID { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("Sr.No")]
        public long SerialNo { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width detailview-grid","")]
        [Select2("AssetCode", "String", false, "AssetCodeChange")]
        [DisplayName("AssetCode")]
        [LazyLoad("", "AssetMaster/AssetCodesSearch", "LookUps.AssetCode")]
        public KeyValueViewModel AssetCode { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width")]
        [DisplayName("Asset GLAccount")]
        [Select2("Accounts", "Numeric", false, "AccountCodeChange",false, optionalAttribute1: "ng-disabled=detail.AssetCode.Key!=null")]
        [LazyLoad("", "AssetMaster/AccountCodesSearch", "LookUps.Accounts")]
        public KeyValueViewModel AssetGlAccount { get; set; }
        public long AssetGlAccID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [DisplayName("Description")]
        public string Description { get; set; }

        public string CategoryName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "small-col-width textright serialnum-wrap",  "ng-disabled='detail.AssetCode.Key == null'","", optionalAttribs: "OnAssetQuatityChange")]
        [DisplayName("Quantity")]
        public int Quantity { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, "medium-col-width ", "ng-disabled='(detail.AssetCode.Key == null && detail.AssetGlAccount.Key == null) || detail.AssetCode.Key == null'")]
        [DisplayName("Depr Start Date")]
        public String StartDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "small-col-width textright", "ng-disabled = '(detail.AssetCode.Key == null && detail.AssetGlAccount.Key == null) || detail.AssetCode.Key == null'", "",optionalAttribs: "OnDebitChange")]
        [DisplayName("Debit")]
        public Nullable<decimal> Debit { get; set; }

        [Required]        
        [ControlType(Framework.Enums.ControlTypes.TextBox, "small-col-width textright", "ng-disabled='(detail.AssetCode.Key == null && detail.AssetGlAccount.Key == null) || detail.AssetCode.Key != null'","", optionalAttribs: "OnCreditChange")]
        [DisplayName("Credit")]
        public Nullable<decimal> Credit { get; set; }

        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> DebitTotal { get; set; }
        public Nullable<decimal> CreditTotal { get; set; }


        

        

        public long DetailIID { get; set; }
        public Nullable<long> HeadID { get; set; }
        public Nullable<long> AssetID { get; set; }
        public Nullable<long> AccountID { get; set; }
        public Nullable<Decimal> AccumulatedDepreciation { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.Model.DetailViewModel[0], CRUDModel.Model.DetailViewModel)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, CRUDModel.Model.DetailViewModel[0], CRUDModel.Model.DetailViewModel)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}


