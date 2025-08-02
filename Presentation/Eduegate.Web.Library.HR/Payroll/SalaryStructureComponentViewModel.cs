using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Services.Contracts.HR.Payroll;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.HR.Payroll
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "SalaryStructure", "CRUDModel.ViewModel.SalaryComponents")]
    [DisplayName("Components")]
    public class SalaryStructureComponentViewModel : BaseMasterViewModel
    {
        public SalaryStructureComponentViewModel()
        {
            SalaryComponent = new KeyValueViewModel();
            //Variables = new List<KeyValueViewModel>();
            //Operators = new List<KeyValueViewModel>();
            SalaryComponentVariableMap = new List<SalaryComponentVariableMapViewModel>() { new SalaryComponentVariableMapViewModel() };
        }

        public long SalaryStructureComponentMapIID { get; set; }

        public byte? ComponentTypeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Components", "Numeric", false, "SalaryComponentChanges($event, $element, gridModel.SalaryComponent,gridModel)")]
        [LookUp("LookUps.SalaryComponent")]
        [CustomDisplay("Components")]
        public KeyValueViewModel SalaryComponent { get; set; }
        public int? SalaryComponentID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(10, ErrorMessage = "Maximum Length should be within 10!")]
        [CustomDisplay("MinAmount")]
        public decimal? MinAmount { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(10, ErrorMessage = "Maximum Length should be within 10!")]
        [CustomDisplay("MaxAmount")]
        public decimal? MaxAmount { get; set; }

       
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("OtherComponents", "Numeric", true, "OtherComponentsChanges($event, $element, gridModel)")]
        //[LookUp("LookUps.SalaryComponent")]
        //[CustomDisplay("Components")]
        public List<KeyValueViewModel> OtherComponents { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("Operators", "Numeric", true, "OperatorsChanges($event, $element, gridModel)")]
        //[LookUp("LookUps.Operators")]
        //[DisplayName("Operators")]
        //public List<KeyValueViewModel> Operators { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("Variables", "Numeric", true, "VariablesChanges($event, $element, gridModel)")]
        //[LookUp("LookUps.Variables")]
        //[DisplayName("Variables")]
        //public List<KeyValueViewModel> Variables { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.ExpressionBuilder)]
        //[CustomDisplay("Expression")]
        public string Expression { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.SalaryComponents[0], CRUDModel.ViewModel.SalaryComponents)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.SalaryComponents[0],CRUDModel.ViewModel.SalaryComponents)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

        [ControlType(Framework.Enums.ControlTypes.GridGroup, "VariableMap", Attributes4 = "colspan=4")]
        [DisplayName("")]
        public List<SalaryComponentVariableMapViewModel> SalaryComponentVariableMap { get; set; }


    }
}
