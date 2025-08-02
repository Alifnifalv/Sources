using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Services.Contracts.School.Settings;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Settings
{
    public class BuildingViewModel : BaseMasterViewModel
    {
        public int BuildingID { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("BuildingName")]
        public string BuildingName { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as BuildingDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<BuildingViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<BuildingDTO, BuildingViewModel>.CreateMap();
            var vm = Mapper<BuildingDTO, BuildingViewModel>.Map(dto as BuildingDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<BuildingViewModel, BuildingDTO>.CreateMap();
            var dto = Mapper<BuildingViewModel, BuildingDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<BuildingDTO>(jsonString);
        }


    }
}
