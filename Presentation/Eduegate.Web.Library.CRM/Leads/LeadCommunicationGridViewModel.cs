using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Eduegate.Web.Library.CRM.Leads
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "CommunicationGrid", "CRUDModel.ViewModel.CommunicationDetails.CommunicationGrid")]
    [DisplayName(" ")]
    public class LeadCommunicationGridViewModel : BaseMasterViewModel
    {
        public LeadCommunicationGridViewModel()
        {
           
        }

        public long CommunicationIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Communication Type")]
        public string CommunicationType { get; set; }
        public byte? CommunicationTypeID { get; set; }


        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("Email Template")]
        public string EmailTemplate { get; set; }
        public int? EmailTemplateID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("MobileNumber")]
        public string MobileNumber { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("CC")]
        //public string EmailCC { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Email")]
        public string Email { get; set; }


        [ControlType(Framework.Enums.ControlTypes.HtmlLabel, "alignleft")]
        [CustomDisplay("Content")]
        public string EmailContent { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Subject")]
        public string Notes { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("CommunicationDate")]
        public string CommunicationDateString { get; set; }
        public DateTime? CommunicationDate { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Follow-Up Date")]
        public string FollowUpDateString { get; set; }
        public DateTime? FollowUpDate { get; set; }

    }
}
