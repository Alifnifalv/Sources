using Eduegate.Framework.Mvc.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Translator;
using Eduegate.Frameworks.Mvc.Attributes;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Services.Contracts.Notifications;
using Eduegate.Domain;
using Eduegate.Web.Library.Common;
using System.Collections.Generic;

namespace Eduegate.Web.Library.ViewModels.Notification
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "PushNotification", "CRUDModel.ViewModel")]
    [DisplayName("Details")]
    public class PushNotificationViewModel : BaseMasterViewModel
    {
        public PushNotificationViewModel()
        {
            Title = new Domain.Setting.SettingBL(null).GetSettingValue<string>("PUSHNOTIFICATIONTITLE");
            AlertStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULTALERTSTATUSID");
            IsEmail = false;
            MailNotification = new MailNotificationViewModel();
        }

        public bool? IsEmail { get; set; } 

        public long NotificationAlertIID { get; set; }

        public string AlertStatus { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Title")]
        public string Title { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown,attribs: "ng-change='UserChanges($event, $element, CRUDModel.ViewModel)'")]
        [CustomDisplay("Branch")]
        [LookUp("LookUps.Branch")]
        public string Branch { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName(" ")]
        public string NewLine9 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.MessageSendType")]
        [CustomDisplay("Message Send Type")]
        public string MessageSendType { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("NotificationUser", "Numeric", false, "UserChanges($event, $element, CRUDModel.ViewModel)")]
        [LookUp("LookUps.NotificationUser")]
        [CustomDisplay("User")]
        public KeyValueViewModel NotificationUser { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName(" ")]
        public string NewLine3 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("EmailTemplate", "Numeric", false, "NotificationTypeChanges($event, $element, CRUDModel.ViewModel)")]
        [LookUp("LookUps.NotificationType")]
        [CustomDisplay("NotificationType")]
        public KeyValueViewModel NotificationType { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        //[DisplayName(" ")]
        //public string NewLine1 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("MessageSendTo", "Numeric", true, OptionalAttribute1 = "ng-show='CRUDModel.ViewModel.MessageSendType == 2'")]
        [LookUp("LookUps.MessageSendTo")]
        [CustomDisplay("Send To")]
        public List<KeyValueViewModel> MessageSendTo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName(" ")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextArea, attribs: "ng-show='!CRUDModel.ViewModel.IsEmail'")]
        [CustomDisplay("Message")]
        //[CustomDisplay("TextMessage")]
        public string TextMessage { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName(" ")]
        public string NewLine4 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("EmailTemplate", "Numeric", false, "ChangeEmailTemplate($event, $element, CRUDModel.ViewModel)", false, OptionalAttribute1 = "ng-disabled=!CRUDModel.ViewModel.IsEmail")]
        [LookUp("LookUps.EmailTemplates")]
        [CustomDisplay("Email Templates")]
        public KeyValueViewModel EmailTemplate { get; set; }
        public int? EmailTemplateID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-disabled=!CRUDModel.ViewModel.IsEmail")]
        [CustomDisplay("Subject")]
        public string Subject { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName(" ")]
        public string NewLine0 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.RichTextEditorInline, optionalAttribs: "ng-show='CRUDModel.ViewModel.IsEmail'")]
        [CustomDisplay(" ")]
        public string Message { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName(" ")]
        public string NewLine5 { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("ImageURL")]
        public string ImageURL { get; set; }

        public long? FromLoginID { get; set; }
        public long? ToLoginID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.NotificationActionType")]
        [CustomDisplay("NotificationAction")]
        public string ActionType { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("ActionValue")]
        public string ActionValue { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName(" ")]
        public string NewLine6 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Button, attribs: "ng-click=\"SendPushNotification($event, $element,CRUDModel.ViewModel)\"")]
        [CustomDisplay("SendPushNotification")]
        public string SubmitPushNotification { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "MailNotification", "MailNotification")]
        [DisplayName("Mail Notification")]
        public MailNotificationViewModel MailNotification { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as PushNotificationDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<PushNotificationViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<PushNotificationDTO, PushNotificationViewModel>.CreateMap();
            var notifDTO = dto as PushNotificationDTO;
            var vm = Mapper<PushNotificationDTO, PushNotificationViewModel>.Map(notifDTO);

            vm.Branch = notifDTO.BranchID.HasValue ? notifDTO.BranchID.ToString() : null;
            //vm.PushNotificationUser = notifDTO.PushNotificationUserID.HasValue ? notifDTO.PushNotificationUserID.ToString() : null;
            vm.ActionType = notifDTO.AlertActionTypeID.HasValue ? notifDTO.AlertActionTypeID.ToString() : null;

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<PushNotificationViewModel, PushNotificationDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<PushNotificationViewModel, PushNotificationDTO>.Map(this);

            dto.BranchID = string.IsNullOrEmpty(this.Branch) ? (long?)null : long.Parse(this.Branch);
            dto.AlertActionTypeID = string.IsNullOrEmpty(this.ActionType) ? (byte?)null : byte.Parse(this.ActionType);
            dto.PushNotificationUserID = string.IsNullOrEmpty(this.NotificationUser.Key) ? (int?)null : int.Parse(this.NotificationUser.Key);
            dto.AlertStatusID = string.IsNullOrEmpty(this.AlertStatus) ? (int?)null : int.Parse(this.AlertStatus);
            dto.IsEmail = this.IsEmail;
            dto.Subject = this.Subject;
            dto.MessageSendType = this.MessageSendType;
            dto.PushNotificationUser = this.NotificationUser.Value;

            List<KeyValueDTO> usersList = new List<KeyValueDTO>();

            foreach (KeyValueViewModel vm in this.MessageSendTo)
            {
                usersList.Add(new KeyValueDTO { Key = vm.Key, Value = vm.Value }
                    );
            }
            dto.MessageSendTo = usersList;

            if (string.IsNullOrEmpty(this.ActionType))
            {
                if (this.NotificationType.Value== "Email")
                {
                    dto.Message = this.Message;
                }
                else
                {
                    dto.Message = this.TextMessage;
                }
            }


            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<PushNotificationDTO>(jsonString);
        }

    }
}