using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.HR.Payroll
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "EmployeeSalaryStructureVariableMap", "gridModel.EmployeeSalaryStructureVariableMap",
       gridBindingPrefix: "EmployeeSalaryStructureVariableMap")]
    [DisplayName("EmployeeSalaryStructureVariableMap")]
    public class EmployeeSalaryStructureVariableMapViewModel : BaseMasterViewModel
    {
        public long EmployeeSalaryStructureVariableMapIID { get; set; }
        public long? EmployeeSalaryStructureComponentMapID { get; set; }
        public int? SalaryComponentID { get; set; }
        [ControlType(Framework.Enums.ControlTypes.TextBox, "textleft")]        
        [CustomDisplay("Key")]
        public string VariableKey { get; set; }
        [ControlType(Framework.Enums.ControlTypes.TextBox, "textleft")]
        [CustomDisplay("Value")]
        public string VariableValue { get; set; }
        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index,ModelStructure.gridModel.EmployeeSalaryStructureVariableMap[0], gridModel.EmployeeSalaryStructureVariableMap)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.gridModel.EmployeeSalaryStructureVariableMap[0], gridModel.EmployeeSalaryStructureVariableMap)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}
