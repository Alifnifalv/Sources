using Eduegate.Framework.Mvc.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Notifications;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels.Notification
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "EmailTemplate", "CRUDModel.ViewModel")]
    [DisplayName("Details")]
    public class EmailTemplateViewModel : BaseMasterViewModel
    {
        public EmailTemplateViewModel()
        {
        }

        public int EmailTemplateID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "textleft", attribs: "ng-disabled=true")]
        [CustomDisplay("TemplateName")]
        public string TemplateName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Subject")]
        public string Subject { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName(" ")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.RichTextEditorInline)]
        [CustomDisplay("EmailTemplate")]
        public string EmailTemplate { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as NotificationEmailTemplateDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<EmailTemplateViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<NotificationEmailTemplateDTO, EmailTemplateViewModel>.CreateMap();
            var tempDTO = dto as NotificationEmailTemplateDTO;
            var vm = Mapper<NotificationEmailTemplateDTO, EmailTemplateViewModel>.Map(tempDTO);

            vm.EmailTemplateID = tempDTO.EmailTemplateID;
            vm.TemplateName = tempDTO.TemplateName;
            vm.Subject = tempDTO.Subject;
            vm.EmailTemplate = tempDTO.EmailTemplate;

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<EmailTemplateViewModel, NotificationEmailTemplateDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<EmailTemplateViewModel, NotificationEmailTemplateDTO>.Map(this);

            dto.EmailTemplateID = this.EmailTemplateID;
            dto.TemplateName = this.TemplateName;
            dto.Subject = this.Subject;
            dto.EmailTemplate = this.EmailTemplate;

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<NotificationEmailTemplateDTO>(jsonString);
        }

    }
}