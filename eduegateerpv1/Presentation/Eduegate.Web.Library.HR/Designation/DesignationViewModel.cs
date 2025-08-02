using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.HR.Designation;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Eduegate.Web.Library.HR.Designation
{
    public class DesignationViewModel: BaseMasterViewModel
    {
       // [Required]
      //  [ControlType(Framework.Enums.ControlTypes.Label)]
       // [DisplayName("Designation ID")]
        public int DesignationID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("DesignationName")]
        public string DesignationName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("need transport notification alert sending ?")]
        public bool? IsTransportNotification { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as DesignationDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<DesignationViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<DesignationDTO, DesignationViewModel>.CreateMap();
            var vm = Mapper<DesignationDTO, DesignationViewModel>.Map(dto as DesignationDTO);
            IsTransportNotification = vm.IsTransportNotification;
            DesignationID = vm.DesignationID;
            DesignationName = vm.DesignationName;
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<DesignationViewModel, DesignationDTO>.CreateMap();
            var dto = Mapper<DesignationViewModel, DesignationDTO>.Map(this);
            IsTransportNotification = dto.IsTransportNotification;
            DesignationID = dto.DesignationID;
            DesignationName = dto.DesignationName;
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<DesignationDTO>(jsonString);
        }
    }
}
