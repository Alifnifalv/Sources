using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.HR.Leaves;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Eduegate.Web.Library.HR.Leaves
{
    public class LeaveSessionViewModel : BaseMasterViewModel
    {
        public byte LeaveSessionID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Description")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string SesionName { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as LeaveSessionDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<LeaveSessionViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<LeaveSessionDTO, LeaveSessionViewModel>.CreateMap();
            var lvDtO = dto as LeaveSessionDTO;
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var vm = Mapper<LeaveSessionDTO, LeaveSessionViewModel>.Map(dto as LeaveSessionDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<LeaveSessionViewModel, LeaveSessionDTO>.CreateMap();
            var dto = Mapper<LeaveSessionViewModel, LeaveSessionDTO>.Map(this);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<LeaveSessionDTO>(jsonString);
        }

    }
}
