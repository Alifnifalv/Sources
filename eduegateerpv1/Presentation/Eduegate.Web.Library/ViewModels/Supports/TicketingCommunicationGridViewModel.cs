using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using System;
using System.ComponentModel;

namespace Eduegate.Web.Library.ViewModels.Supports
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "CommunicationGrid", "CRUDModel.ViewModel.TicketCommunication.CommunicationGrid")]
    [DisplayName(" ")]
    public class TicketingCommunicationGridViewModel : BaseMasterViewModel
    {
        public TicketingCommunicationGridViewModel()
        {

        }

        public long TicketCommunicationIID { get; set; }

        public long? TicketID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("User")]
        public string LoginUserID { get; set; }
        public long? LoginID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("CommunicationDate")]
        public string CommunicationDateString { get; set; }
        public DateTime? CommunicationDate { get; set; }


        [ControlType(Framework.Enums.ControlTypes.HtmlLabel, "alignleft")]
        [CustomDisplay("Notes")]
        public string Notes { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Follow-Up Date")]
        public string FollowUpDateString { get; set; }
        public DateTime? FollowUpDate { get; set; }

    }
}