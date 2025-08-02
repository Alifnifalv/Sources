using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Academics
{
    public class ShiftViewModel : BaseMasterViewModel
    {
       /// [Required]
       /// [ControlType(Framework.Enums.ControlTypes.Label)]
       /// [DisplayName("Id")]
        public byte  ClassShiftID { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("ShiftDescription")]
        public string  ShiftDescription { get; set; }
     
        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as ShiftDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<ShiftViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<ShiftDTO, ShiftViewModel>.CreateMap();
            var vm = Mapper<ShiftDTO, ShiftViewModel>.Map(dto as ShiftDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<ShiftViewModel, ShiftDTO>.CreateMap();
            var dto = Mapper<ShiftViewModel, ShiftDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<ShiftDTO>(jsonString);
        }
    }
}

