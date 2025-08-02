using System;
using System.Globalization;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Mutual;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace Eduegate.Web.Library.ViewModels
{
    public class CommunicationViewModel : BaseMasterViewModel
    {
        public CommunicationViewModel()
        {
        }

        public long? ReferenceID { get; set; }

        public string FromEmail { get; set; }

        public string ToEmail { get; set; }

        public string Subject { get; set; }

        public string EmailContent { get; set; }

        //TODO: Need to check if this is required
        //[AllowHtml]
        public string ContentHtml { get; set; }

        public long? ScreenID { get; set; }

        public int? EmailTemplateID { get; set; }

        public string EmailTemplateKey { get; set; }

        public byte? CommunicationTypeID { get; set; }

        public string CommunicationTypeKey { get; set; }

        public string MobileNumber { get; set; }

        public string FollowUpDateString { get; set; }
        public DateTime? FollowUpDate { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as CommunicationDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<CommunicationViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<CommunicationDTO, CommunicationViewModel>.CreateMap();
            var cirDto = dto as CommunicationDTO;
            var vm = Mapper<CommunicationDTO, CommunicationViewModel>.Map(dto as CommunicationDTO);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            vm.CommunicationTypeKey = cirDto.CommunicationTypeID.HasValue ? cirDto.CommunicationTypeID.ToString() : null;
            vm.EmailTemplateKey = cirDto.EmailTemplateID.HasValue ? cirDto.EmailTemplateID.ToString() : null;
            vm.FollowUpDateString = cirDto.FollowUpDate.HasValue ? Convert.ToDateTime(cirDto.FollowUpDate).ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<CommunicationViewModel, CommunicationDTO>.CreateMap();
            var dto = Mapper<CommunicationViewModel, CommunicationDTO>.Map(this);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            dto.CommunicationTypeID = CommunicationTypeKey != null ? byte.Parse(CommunicationTypeKey) : (byte?)null;
            dto.EmailTemplateID = EmailTemplateKey != null ? int.Parse(EmailTemplateKey) : (int?)null;
            dto.FollowUpDate = string.IsNullOrEmpty(this.FollowUpDateString) ? (DateTime?)null : DateTime.ParseExact(this.FollowUpDateString, dateFormat, CultureInfo.InvariantCulture);

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<CommunicationDTO>(jsonString);
        }

    }
}