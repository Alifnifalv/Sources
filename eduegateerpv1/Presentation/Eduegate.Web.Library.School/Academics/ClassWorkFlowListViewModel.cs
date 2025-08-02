using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Web.Library.School.Common;
using Eduegate.Services.Contracts.School.Common;
using System.Globalization;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Academics
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "WorkFlowList", "CRUDModel.ViewModel.WorkFlowList")]
    [DisplayName("Class WorkFlowList")]
    public class ClassWorkFlowListViewModel : BaseMasterViewModel
    {
        public ClassWorkFlowListViewModel()
        {

        }

        //[ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[DisplayName("Coordinator")]
        //[LookUp("LookUps.Subject")]
        //public string Coordinator { get; set; }
        //public int? CoordinatorID { get; set; }

        public long ClassWorkFlowIID { get; set; }
        public int? ClassID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.WorkflowEntitys")]
        [CustomDisplay("WorkFlowType")]
        public string WorkFlow1 { get; set; }
        public int? WorkflowEntityID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.WorkFlow")]
        [CustomDisplay("Workflow")]
        public string WorkFlow2 { get; set; }
        public long? WorkflowID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.WorkFlowList[0], CRUDModel.ViewModel.WorkFlowList)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.WorkFlowList[0],CRUDModel.ViewModel.WorkFlowList)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}
