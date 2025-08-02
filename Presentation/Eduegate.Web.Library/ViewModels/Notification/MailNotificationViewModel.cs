using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Enums;
using System.Globalization;
using Eduegate.Web.Library.Common;
using System;
using Eduegate.Framework.Extensions;

namespace Eduegate.Web.Library.ViewModels.Notification
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "MailNotification", "CRUDModel.ViewModel.MailNotification")]
    [DisplayName("Mail notification")]
    public class MailNotificationViewModel : BaseMasterViewModel
    {
        public MailNotificationViewModel()
        {
            MailSubject = "Test Mail";
            PortNumber = new Domain.Setting.SettingBL(null).GetSettingValue<int>("SMTPPORT");
            IsUseDefaultCredentials = false;
            IsEnableSSL = true;
            IsMailContainsAttachment = false;
        }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Mail port number")]
        public int PortNumber { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [DisplayName("Is use default credentials")]
        public bool IsUseDefaultCredentials { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName(" ")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [DisplayName("Is enable ssl")]
        public bool IsEnableSSL { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [DisplayName("Is mail contains attachment")]
        public bool IsMailContainsAttachment { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName(" ")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("To mail address")]
        public string MailToAddress { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Mail subject")]
        public string MailSubject { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName(" ")]
        public string NewLine3 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.RichTextEditorInline)]
        [CustomDisplay("Mail message")]
        public string MailMessage { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName(" ")]
        public string NewLine4 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Button, attribs: "ng-click=\"SendMailNotification($event, $element,CRUDModel.ViewModel.MailNotification)\"")]
        [CustomDisplay("Send mail notification")]
        public string SendMailNotification { get; set; }

    }
}