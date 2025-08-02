using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "ReferenceAndPastPerformance", "CRUDModel.ViewModel.ReferenceAndPastPerformance")]
    [DisplayName("Reference and Past Performance")]
    public class SupplierRefAndPastPerformanceViewModel : BaseMasterViewModel
    {
        public SupplierRefAndPastPerformanceViewModel()
        {
            Declaration = true;
        }

        [ControlType(Framework.Enums.ControlTypes.Label, "fullwidth")]
        [CustomDisplay("Client References :")]
        public string Label1 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Names of clients")]
        public string NamesOfClients { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Contact information")]
        public string ClientContactInformation { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Project details")]
        public string ClientProjectDetails { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label, "fullwidth")]
        [CustomDisplay("Previous Contracts and Projects :")]
        public string Label2 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Scope of work")]
        public string PrevContractScopeOfWork { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Value of contracts")]
        public string PrevValueOfContracts { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Duration")]
        public string PrevContractDuration { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Declaration")]
        public string Label4 { get; set; }        
        
        [ControlType(Framework.Enums.ControlTypes.CheckBox, "fullwidth alignleft")]
        [CustomDisplay("We confirm that the information provided for registration are true and correct and we will comply with your organization's standards, reducing risks and fostering a trustworthy partnership.")]
        public bool? Declaration { get; set; }
    }
}
