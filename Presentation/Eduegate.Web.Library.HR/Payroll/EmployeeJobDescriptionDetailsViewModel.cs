using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Eduegate.Web.Library.HR.Payroll
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "EmpJobDescriptionDetail", "CRUDModel.ViewModel.EmpJobDescriptionDetail")]
    [DisplayName("Components")]
    public class EmployeeJobDescriptionDetailsViewModel : BaseMasterViewModel
    {
        public EmployeeJobDescriptionDetailsViewModel()
        {
        }

        public long? JobDescriptionID { get; set; }
        public long JobDescriptionMapID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("Sr.No")]
        public long SerialNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("Description")]
        public string Description { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index,ModelStructure.EmpJobDescriptionDetail[0], CRUDModel.ViewModel.EmpJobDescriptionDetail)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.EmpJobDescriptionDetail[0],CRUDModel.ViewModel.EmpJobDescriptionDetail)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}

