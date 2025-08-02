using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Students
{
    public class StudentGroupViewmodel : BaseMasterViewModel
    {
        public StudentGroupViewmodel()
        {
            IsActive = false;
        }

        public int StudentGroupID { get; set; }
        

        [Required]
        [ControlType(Eduegate.Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [CustomDisplay("GroupName")]
        public string GroupName { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("GroupType")]
        [LookUp("LookUps.StudentGroupType")]
        public string GroupTypeName { get; set; }
        public int? GroupTypeID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }


        [Required]
        [ControlType(Eduegate.Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as StudentGroupDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<StudentGroupViewmodel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<StudentGroupDTO, StudentGroupViewmodel>.CreateMap();
            var grpDto = dto as StudentGroupDTO;
            var vm = Mapper<StudentGroupDTO, StudentGroupViewmodel>.Map(grpDto);
            vm.GroupTypeName = grpDto.GroupTypeID.ToString();
            return vm;
        }
        public override BaseMasterDTO ToDTO()
        {
            Mapper<StudentGroupViewmodel, StudentGroupDTO>.CreateMap();
            var dto = Mapper<StudentGroupViewmodel, StudentGroupDTO>.Map(this);
            dto.GroupTypeID = int.Parse(this.GroupTypeName);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<StudentGroupDTO>(jsonString);
        }
    }
}
