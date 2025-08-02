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
    public class LibraryBookCategoryViewModel : BaseMasterViewModel
    {
        /// [Required]
        /// [ControlType(Framework.Enums.ControlTypes.Label)]
        /// [DisplayName("LibraryBookCategoryID")]
        /// 
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("CategoryCode")]
        public string CategoryCode { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }

        public long  LibraryBookCategoryID { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("BookCategoryName")]
        public string  BookCategoryName { get; set; }
     
        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as LibraryBookCategoryDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<LibraryBookCategoryViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<LibraryBookCategoryDTO, LibraryBookCategoryViewModel>.CreateMap();
            var vm = Mapper<LibraryBookCategoryDTO, LibraryBookCategoryViewModel>.Map(dto as LibraryBookCategoryDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<LibraryBookCategoryViewModel, LibraryBookCategoryDTO>.CreateMap();
            var dto = Mapper<LibraryBookCategoryViewModel, LibraryBookCategoryDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<LibraryBookCategoryDTO>(jsonString);
        }
    }
}

