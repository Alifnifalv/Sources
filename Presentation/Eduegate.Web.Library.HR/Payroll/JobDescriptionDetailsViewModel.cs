using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Eduegate.Web.Library.HR.Payroll
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "JDDetail", "CRUDModel.ViewModel.JobDescriptionDetail")]
    [DisplayName("Components")]
    public class JobDescriptionDetailsViewModel : BaseMasterViewModel
    {
        public JobDescriptionDetailsViewModel()
        {
        }

        public long? JDMasterID { get; set; }
        public long JDMapID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [DisplayName("Sr.No")]
        public long SerialNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("Description")]
        public string Description { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index,ModelStructure.JobDescriptionDetail[0], CRUDModel.ViewModel.JobDescriptionDetail)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.JobDescriptionDetail[0],CRUDModel.ViewModel.JobDescriptionDetail)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}

