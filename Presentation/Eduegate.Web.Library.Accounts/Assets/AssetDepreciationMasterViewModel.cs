using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Frameworks.Mvc.Attributes;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;

namespace Eduegate.Web.Library.Accounts.Assets
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "AssetDepreciationMaster", "CRUDModel.Model.MasterViewModel")]
    [DisplayName("Asset Depreciation Details")]
    public class AssetDepreciationMasterViewModel : BaseMasterViewModel
    {
        public AssetDepreciationMasterViewModel()
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            TransactionDateString = DateTime.Now.ToString(dateFormat);
            Assets = new List<KeyValueViewModel>();
            AssetCategories = new List<KeyValueViewModel>();
        }

        public long TransactionHeadIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("Branch", "String", false, "BranchChange($event, $element)", false)]
        [CustomDisplay("Branch")]
        [LookUp("LookUps.Branch")]
        [HasClaim(HasClaims.HASMULTIBRANCH)]
        public KeyValueViewModel Branch { get; set; }
        public long BranchID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("DocumentType", "String", false, "DocumentTypeChange($event, $element)", optionalAttribute1: "ng-disabled=CRUDModel.Model.MasterViewModel.DocumentType.Key")]
        [CustomDisplay("Document")]
        [LookUp("LookUps.DocumentType")]
        [HasClaim(HasClaims.HASMULTIDCOUMENT)]
        public KeyValueViewModel DocumentType { get; set; }
        public long? DocumentTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "onecol-header-left")]
        [CustomDisplay("TransactionNumber")]
        public string TransactionNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, "onecol-header-left")]
        [CustomDisplay("TransactionDate")]
        public string TransactionDateString { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("DocumentStatus", "Numeric", false)]
        [LookUp("LookUps.DocumentStatus")]
        [CustomDisplay("DocumentStatus")]
        public KeyValueViewModel DocumentStatus { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("AssetCategories", "String", true, "AssetCategoryChanges(CRUDModel.Model.MasterViewModel)")]
        [CustomDisplay("AssetCategory")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=AllAssetCategories", "LookUps.AllAssetCategories")]
        public List<KeyValueViewModel> AssetCategories { get; set; }
        public long? AssetCategoryID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        [Select2("Asset", "String", true, "AssetChanges(CRUDModel.Model.MasterViewModel)")]
        [CustomDisplay("Asset")]
        [LookUp("LookUps.AllAssets")]
        //[LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=AllAssets", "LookUps.AllAssets")]
        public List<KeyValueViewModel> Assets { get; set; }
        public long? AssetID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, "", "ng-disabled='CRUDModel.Model.MasterViewModel.IsTransactionCompleted'")]
        [CustomDisplay("DepreciationMonth")]
        [LookUp("LookUps.Months")]
        public string DepreciationMonth { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, "", "ng-disabled='CRUDModel.Model.MasterViewModel.IsTransactionCompleted'")]
        [CustomDisplay("DepreciationYears")]
        [LookUp("LookUps.Years")]
        public string DepreciationYear { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Button, "", "ng-click='DepreciationDetailButtonClick(CRUDModel.Model.MasterViewModel)' ng-disabled='CRUDModel.Model.MasterViewModel.IsTransactionCompleted'")]
        [CustomDisplay("ApplyDepreciation")]
        public string DepreciationDetailButton { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Button, "", "ng-click='AccountPostingButtonClick(CRUDModel.Model.MasterViewModel)' ng-disabled='CRUDModel.Model.MasterViewModel.TransactionHeadIID<=0 || !CRUDModel.Model.MasterViewModel.IsTransactionCompleted'")]
        [CustomDisplay("AccountPosting")]
        public string AccountPostingButton { get; set; }

        public DocumentReferenceTypes? DocumentReferenceTypeID { get; set; }

        public bool IsTransactionCompleted { get; set; }
        public int? CompanyID { get; set; }
        public string Remarks { get; set; }

    }
}