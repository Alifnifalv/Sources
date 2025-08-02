using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.School.Students
{
    public class StudentCategoryViewModel : BaseMasterViewModel
    {
       // [Required]
        //[ControlType(Framework.Enums.ControlTypes.Label, "fullwidth alignleft")]
       // [DisplayName("Id")]
        public int  StudentCategoryID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [DisplayName("Description")]
        public string  Description { get; set; }
     
        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as StudentCategoryDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<StudentCategoryViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<StudentCategoryDTO, StudentCategoryViewModel>.CreateMap();
            var vm = Mapper<StudentCategoryDTO, StudentCategoryViewModel>.Map(dto as StudentCategoryDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<StudentCategoryViewModel, StudentCategoryDTO>.CreateMap();
            var dto = Mapper<StudentCategoryViewModel, StudentCategoryDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<StudentCategoryDTO>(jsonString);
        }
    }
}

