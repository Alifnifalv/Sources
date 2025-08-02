using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.School.Mutual;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.School.Mutual
{
    public class LibraryBookConditionsViewModel : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("BookConditionID")]
        public byte  BookConditionID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("BookConditionName")]
        public string  BookConditionName { get; set; }
     
        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as LibraryBookConditionsDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<LibraryBookConditionsViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<LibraryBookConditionsDTO, LibraryBookConditionsViewModel>.CreateMap();
            var vm = Mapper<LibraryBookConditionsDTO, LibraryBookConditionsViewModel>.Map(dto as LibraryBookConditionsDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<LibraryBookConditionsViewModel, LibraryBookConditionsDTO>.CreateMap();
            var dto = Mapper<LibraryBookConditionsViewModel, LibraryBookConditionsDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<LibraryBookConditionsDTO>(jsonString);
        }
    }
}

