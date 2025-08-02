using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Leads;
using System.Globalization;

namespace Eduegate.Web.Library.CRM.Leads
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "CommunicationDetails", "CRUDModel.ViewModel.CommunicationDetails")]
    [DisplayName("Communication Details")]
    public class LeadCommunicationViewModel : BaseMasterViewModel
    {
        public LeadCommunicationViewModel()
        {
            CommunicationGrid = new List<LeadCommunicationGridViewModel>() { new LeadCommunicationGridViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.Button, Attributes = "ng-disabled='CRUDModel.ViewModel.LeadIID' ng-Click='LoadCommunication(CRUDModel.ViewModel,$event,CRUDModel.ViewModel.LeadIID,2344)'")]
        [CustomDisplay("Communication")]
        public string CommunicationButton { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [DisplayName(" ")]
        public List<LeadCommunicationGridViewModel> CommunicationGrid { get; set; }

    }
}
