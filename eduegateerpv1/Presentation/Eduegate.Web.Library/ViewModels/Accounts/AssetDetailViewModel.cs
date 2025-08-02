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
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "", "CRUDModel.Model.DetailViewModel")]
    public class AssetDetailViewModel:BaseMasterViewModel
    {
        public AssetDetailViewModel()
        {
            
        }


        [ControlType(Framework.Enums.ControlTypes.CheckBox, "ex-small-col-width", "ng-model='detail.IsRowSelected'")]
        [DisplayName("IsRowSelected")]
        public bool IsRowSelected { get; set; }
        public bool SelectAll { get; set; }
        public long AssetIID { get; set; }
        

        
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "ex-small-col-width")]
        [DisplayName("Sr.No")]
        public long SerialNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBoxWithPopup, "small-col-width ")]
        [DisplayName("Code")]
        public string AssetCode { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "large-col-width")]
        [DisplayName("Description")]
        public string Description { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "medium-col-width")]
        [Select2("AssetCategories", "Numeric", false)]
        [DisplayName("Category")]
        [LookUp("LookUps.AssetCategories")]
        public KeyValueViewModel AssetCategory { get; set; }
        public long AssetCategoryID { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "medium-col-width")]
        [DisplayName("GL A/c")]
        [Select2("Accounts", "Numeric", false)]
        [LookUp("LookUps.Accounts")]
        public KeyValueViewModel AssetGlAccount { get; set; }
        public long AssetGlAccID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "medium-col-width")]
        [DisplayName("Acc. Depr. GL")]
        [Select2("Accounts", "Numeric", false)]
        [LookUp("LookUps.Accounts")]
        public KeyValueViewModel AccumulatedDepGLAccount { get; set; }
        public long AccumulatedDepGLAccID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width")]
        [DisplayName("Depr. Exp. GL")]
        [Select2("Accounts", "Numeric", false)]
        [LookUp("LookUps.Accounts")]
        public KeyValueViewModel DepreciationExpGLAccount { get; set; }
        public long DepreciationExpGLAccId { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width")]
        [DisplayName("Depreciation(years)")]
        public int DepreciationYears { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.Model.DetailViewModel[0], CRUDModel.Model.DetailViewModel)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, CRUDModel.Model.DetailViewModel[0], CRUDModel.Model.DetailViewModel)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}


