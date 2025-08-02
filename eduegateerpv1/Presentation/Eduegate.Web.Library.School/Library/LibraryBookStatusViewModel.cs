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
    public class LibraryBookStatusViewModel : BaseMasterViewModel
    {
      ///  [Required]
       /// [ControlType(Framework.Enums.ControlTypes.Label)]
        ///[DisplayName("LibraryBookStatusID")]
        public byte  LibraryBookStatusID { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Description")]
        public string  Description { get; set; }
     
        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as LibraryBookStatusDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<LibraryBookStatusViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<LibraryBookStatusDTO, LibraryBookStatusViewModel>.CreateMap();
            var vm = Mapper<LibraryBookStatusDTO, LibraryBookStatusViewModel>.Map(dto as LibraryBookStatusDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<LibraryBookStatusViewModel, LibraryBookStatusDTO>.CreateMap();
            var dto = Mapper<LibraryBookStatusViewModel, LibraryBookStatusDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<LibraryBookStatusDTO>(jsonString);
        }
    }
}

