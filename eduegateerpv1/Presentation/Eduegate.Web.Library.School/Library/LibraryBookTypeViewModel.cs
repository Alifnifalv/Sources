using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
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
    public class LibraryBookTypeViewModel : BaseMasterViewModel
    {
        ///[Required]
        ///[ControlType(Framework.Enums.ControlTypes.Label)]
        ///[DisplayName("ID")]
        public byte?  LibraryBookTypeID { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("BookTypeName")]
        public string  BookTypeName { get; set; }
     
        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as LibraryBookTypeDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<LibraryBookTypeViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<LibraryBookTypeDTO, LibraryBookTypeViewModel>.CreateMap();
            var vm = Mapper<LibraryBookTypeDTO, LibraryBookTypeViewModel>.Map(dto as LibraryBookTypeDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<LibraryBookTypeViewModel, LibraryBookTypeDTO>.CreateMap();
            var dto = Mapper<LibraryBookTypeViewModel, LibraryBookTypeDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<LibraryBookTypeDTO>(jsonString);
        }
    }
}

