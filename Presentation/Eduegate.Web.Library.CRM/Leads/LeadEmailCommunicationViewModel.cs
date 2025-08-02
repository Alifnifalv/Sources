using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.CRM.Leads
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "EmailCommunication", "CRUDModel.ViewModel.EmailCommunication")]
    [DisplayName("Email Communication")]
    public class LeadEmailCommunicationViewModel : BaseMasterViewModel
    {
        public LeadEmailCommunicationViewModel()
        {
            IsSendEmail = false;
        }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "textleft")]
        [CustomDisplay("Send Email")]
        public bool IsSendEmail { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DropDown, "","ng-change='ChangeEmailTemplate($event, $element, CRUDModel.ViewModel.EmailCommunication)'")]
        [LookUp("LookUps.EmailTemplates")]
        [CustomDisplay("Email Template")]
        public string EmailTemplate { get; set; }
        public int? EmailTemplateID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Subject")]
        public string Subject { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.RichTextEditorInline)]
        [CustomDisplay("Email Content")]
        public string EmailContent { get; set; }

    }
}
