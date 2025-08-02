using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Eduegate.Web.Library.School.Students
{
   public class ClassSectionMapViewModel : BaseMasterViewModel
    {
        public ClassSectionMapViewModel()
        {
            //Class = new KeyValueViewModel();
            //Section = new List<KeyValueViewModel>();
        }

        ///[Required]
        ///[ControlType(Framework.Enums.ControlTypes.Label)]
        ///[DisplayName("Id")]
        public long ClassSectionMapIID { get; set; }
        
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Classes", "Numeric", false, "")]
        [LookUp("LookUps.Classes")]
        [CustomDisplay("Class")]
        public KeyValueViewModel StudentClass { get; set; }
        public int? ClassID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        //[DisplayName("")]
        //public string NewLine { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2(" Section", "Numeric", true, "")]
        [LookUp("LookUps.Section")]
        [CustomDisplay("Section")]
        public List<KeyValueViewModel> Section { get; set; }
        public int? SectionID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        //[MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        //[ControlType(Framework.Enums.ControlTypes.TextArea)]
        //[DisplayName("Description")]
        //public string Description { get; set; }

        [Required]
        [ControlType(Eduegate.Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("MinimumCapacity")]
        public int? MinCapacity { get; set; }

        [Required]
        [ControlType(Eduegate.Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("MaximumCapacity")]
        public int? MaxCapacity { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as ClassSectionMapDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<ClassSectionMapViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<ClassSectionMapDTO, ClassSectionMapViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var mapDTO = dto as ClassSectionMapDTO;
            var vm = Mapper<ClassSectionMapDTO, ClassSectionMapViewModel>.Map(dto as ClassSectionMapDTO);
            vm.StudentClass = new KeyValueViewModel() { Key = mapDTO.ClassID.ToString(), Value = mapDTO.Class.Value };
            //vm.Section = new KeyValueViewModel() { Key = mapDTO.SectionID.ToString(), Value = mapDTO.Section.Value };
            //vm.Description = mapDTO.Description;
            vm.MinCapacity = mapDTO.MinimumCapacity;
            vm.MaxCapacity = mapDTO.MaximumCapacity;

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<ClassSectionMapViewModel, ClassSectionMapDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<ClassSectionMapViewModel, ClassSectionMapDTO>.Map(this);
            
            List<KeyValueDTO> sectionList = new List<KeyValueDTO>();

            foreach (KeyValueViewModel vm in this.Section)
            {
                sectionList.Add(new KeyValueDTO { Key = vm.Key, Value = vm.Value }
                    );
            }
            dto.Section = sectionList;

            //dto.SectionID = string.IsNullOrEmpty(this.Section.Key) ? (int?)null : int.Parse(this.Section.Key);
            dto.ClassID = string.IsNullOrEmpty(this.StudentClass.Key) ? (int?)null : int.Parse(this.StudentClass.Key);
            //dto.Description = this.Description;
            dto.MinimumCapacity = this.MinCapacity;
            dto.MaximumCapacity = this.MaxCapacity;

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<ClassSectionMapDTO>(jsonString);
        }

        public static List<KeyValueViewModel> ToKeyValueViewModel(List<ClassSectionMapDTO> dtos)
        {
            var vMs = new List<KeyValueViewModel>();

            foreach (var dto in dtos)
            {
                vMs.Add(new KeyValueViewModel() { Key = dto.Sections.Key.ToString(), Value = dto.Sections.Value });
                vMs.Add(new KeyValueViewModel() { Key = dto.Class.Key.ToString(), Value = dto.Class.Value });
            }

            return vMs;
        }
    }
}
