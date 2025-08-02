using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;

namespace Eduegate.Web.Library.Support.CustomerSupport
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "TicketCommunication", "CRUDModel.ViewModel.TicketCommunication")]
    [DisplayName("Communications")]
    public class TicketingCommunicationViewModel : BaseMasterViewModel
    {
        public TicketingCommunicationViewModel()
        {
            CommunicationGrid = new List<TicketingCommunicationGridViewModel>() { new TicketingCommunicationGridViewModel() };
        }

        public long TicketCommunicationIID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.RichTextEditorInline)]
        //[DisplayName("Notes")]
        //public string Notes { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("Notes")]
        public string Notes { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("PreviousCommunications")]
        public List<TicketingCommunicationGridViewModel> CommunicationGrid { get; set; }

    }
}