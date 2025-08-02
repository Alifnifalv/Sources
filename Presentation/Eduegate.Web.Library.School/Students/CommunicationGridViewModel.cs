using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.School.Students
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "CommunicationGrid", "CRUDModel.ViewModel.CommunicationGrid")]
    [DisplayName("")]
    public class CommunicationGridViewModel : BaseMasterViewModel
    {
        public CommunicationGridViewModel()
        {
        }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [DisplayName("Pros No.")]
        public string ProsNumber { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "")]
        [DisplayName("Communication Type")]
        public string CommunicationType { get; set; }
        public DateTime? InvoiceDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "")]
        [DisplayName("Description")]
        public string Description { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "")]
        [DisplayName("Date")]
        public string DateString { get; set; }
        public DateTime? ProsDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "")]
        [DisplayName("Follow up Date")]
        public string FollowUpDateString { get; set; }
        public DateTime? FollowUpDate { get; set; }


    }
}
