using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Library;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Library
{
    public class LibraryBookConditionViewModel : BaseMasterViewModel
    {
       /// [Required]
        ///[ControlType(Framework.Enums.ControlTypes.Label)]
        ///[DisplayName("Id")]
        public byte  BookConditionID { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Description")]
        public string  BookConditionName { get; set; }
     
        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as LibraryBookConditionDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<LibraryBookConditionViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<LibraryBookConditionDTO, LibraryBookConditionViewModel>.CreateMap();
            var vm = Mapper<LibraryBookConditionDTO, LibraryBookConditionViewModel>.Map(dto as LibraryBookConditionDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<LibraryBookConditionViewModel, LibraryBookConditionDTO>.CreateMap();
            var dto = Mapper<LibraryBookConditionViewModel, LibraryBookConditionDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<LibraryBookConditionDTO>(jsonString);
        }
    }
}

