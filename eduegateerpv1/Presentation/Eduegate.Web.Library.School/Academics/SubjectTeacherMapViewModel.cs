using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Academics
{
    public class SubjectTeacherMapViewModel : BaseMasterViewModel
    {
        public SubjectTeacherMapViewModel()
        {
            //Employee = new KeyValueViewModel();
            //Subject = new KeyValueViewModel();
        }

       // [Required]
      // [ControlType(Framework.Enums.ControlTypes.Label)]
      //[DisplayName("Id")]
        public long  SubjectTeacherMapIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Classes", "Numeric", false, "")]
        [LookUp("LookUps.Subject")]
        [CustomDisplay("Subject")]
        public KeyValueViewModel Subject { get; set; }

        public int?  SubjectID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Teacher", "String", true)]
        [LookUp("LookUps.Teacher")]
        [CustomDisplay("Teacher")]
        public List<KeyValueViewModel> Teachers { get; set; }

        public long?  EmployeeID { get; set; }       

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as SubjectTeacherMapDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<SubjectTeacherMapViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<SubjectTeacherMapDTO, SubjectTeacherMapViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var teacherDTO = dto as SubjectTeacherMapDTO;
            var vm = Mapper<SubjectTeacherMapDTO, SubjectTeacherMapViewModel>.Map(teacherDTO);
            vm.Subject = new KeyValueViewModel() { Key = teacherDTO.SubjectID.ToString(), Value = teacherDTO.SubjectName.Value };
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<SubjectTeacherMapViewModel, SubjectTeacherMapDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<SubjectTeacherMapViewModel, SubjectTeacherMapDTO>.Map(this);
            List<KeyValueDTO> teacherList = new List<KeyValueDTO>();

            foreach (KeyValueViewModel emp in this.Teachers)
            {
                teacherList.Add(new KeyValueDTO { Key = emp.Key, Value = emp.Value }
                    );
            }
            dto.Teachers = teacherList;

            dto.SubjectID = string.IsNullOrEmpty(this.Subject.Key) ? (int?)null : int.Parse(this.Subject.Key);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<SubjectTeacherMapDTO>(jsonString);
        }
    }
}

