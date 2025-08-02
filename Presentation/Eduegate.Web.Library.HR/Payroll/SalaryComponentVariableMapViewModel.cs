using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.HR.Payroll
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "VariableMap", "gridModel.SalaryComponentVariableMap", gridBindingPrefix: "SalaryComponentVariableMap")]
    [DisplayName("SalaryComponentVariableMap")]
    public class SalaryComponentVariableMapViewModel : BaseMasterViewModel
    {
        public long SalaryComponentVariableMapIID { get; set; }
        public long? SalaryStructureComponentMapID { get; set; }
        public int? SalaryComponentID { get; set; }
        [ControlType(Framework.Enums.ControlTypes.TextBox, "textleft")]
        [CustomDisplay("Key")]
        public string VariableKey { get; set; }
        [ControlType(Framework.Enums.ControlTypes.TextBox, "textleft")]
        [CustomDisplay("Value")]
        public string VariableValue { get; set; }
        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index,ModelStructure.gridModel.SalaryComponentVariableMap[0], gridModel.SalaryComponentVariableMap)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index,ModelStructure.gridModel.SalaryComponentVariableMap[0], gridModel.SalaryComponentVariableMap)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}
