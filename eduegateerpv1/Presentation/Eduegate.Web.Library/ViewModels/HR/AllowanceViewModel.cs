using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.HR
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "AllowancesVM", "CRUDModel.Model.MasterViewModel.Allowance.Allowances", "header-list", "", "", "", true)]
    [DisplayName("AllowancesVM")]
    public class AllowanceViewModel : BaseMasterViewModel
    {
        [ControlType(Framework.Enums.ControlTypes.CheckBox, "ex-small-col-width", "ng-model='detail.IsRowSelected'")]
        [DisplayName(" ")]
        public bool IsRowSelected { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width detailview-grid")]
        [Select2("Allowance", "String",false,"AllowanceChange($index+1)", false, "ng-click=LoadAllowences($index)")]
        [DisplayName("ALLOWANCE")]
        [LookUp("LookUps.AllowanceFiltered")]
        public KeyValueViewModel Allowance { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "large-col-width detailview-grid textright")]
        [DisplayName("AMOUNT")]
        public decimal Amount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "large-col-width detailview-grid textright")]
        [DisplayName("AMOUNT AFTER PROBATION")]
        public decimal AmountAfterProbation { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "large-col-width detailview-grid")]
        [DisplayName("REMARK")]
        public string Remark { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Button, "ex-small-col-width", "ng-click='InsertGridRow($index, ModelStructure.Model.MasterViewModel.Allowance.Allowances[0], CRUDModel.Model.MasterViewModel.Allowance.Allowances)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Button, "ex-small-col-width"  , "ng-click='RemoveGridRow($index, CRUDModel.Model.MasterViewModel.Allowance.Allowances[0], CRUDModel.Model.MasterViewModel.Allowance.Allowances)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

        public string CRE_WEBUSER { get; set; }

        public string CRE_IP { get; set; }

        public DateTime? CRE_DT { get; set; }

        public string CRE_BY { get; set; }

        public DateTime? REQ_DT { get; set; }
    }
}
