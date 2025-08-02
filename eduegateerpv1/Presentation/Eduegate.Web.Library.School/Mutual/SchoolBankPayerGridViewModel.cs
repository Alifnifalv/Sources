using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.Common;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Eduegate.Web.Library.School.Mutual
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "PayerGrid", "CRUDModel.ViewModel.PayerGrid")]
    [DisplayName("Payer Details")]
    public class SchoolBankPayerGridViewModel : BaseMasterViewModel
    {
        public SchoolBankPayerGridViewModel()
        {

        }

        public long PayerBankDetailIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("Sr.No")]
        public long SerialNo { get; set; }


        [ControlType(Framework.Enums.ControlTypes.CheckBox, "medium-col-width")]
        [CustomDisplay("Main operating")]
        public bool? IsMainOperating { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width")]
        [Select2("BankName", "String", false, "BankDropDownChanges(gridModel)")]
        [LookUp("LookUps.BankName")]
        [CustomDisplay("Bank")]
        public KeyValueViewModel Bank { get; set; }

        public int? BanksID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "text-left large-col-width")]
        [CustomDisplay("IBAN")]
        public string PayerIBAN { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox, "text-left small-col-width")]
        [CustomDisplay("Short Name")]
        public string PayerBankShortName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [DisplayName("")]
        public string BlankLine { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index,ModelStructure.PayerGrid[0],CRUDModel.ViewModel.PayerGrid)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index,ModelStructure.PayerGrid[0],CRUDModel.ViewModel.PayerGrid)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

    }
}