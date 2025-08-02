using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Academics
{
    public class ClassGroupViewModel : BaseMasterViewModel
    {
        public ClassGroupViewModel()
        {
            //HeadTeacher = new KeyValueViewModel();
            //ClassTeacher = new List<KeyValueViewModel>();
        }

        /// [Required]
        /// [ControlType(Framework.Enums.ControlTypes.Label)]
        /// [DisplayName("Id")]
        public long  ClassGroupID { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("GroupDescription")]
        public string  GroupDescription { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("HeadTeacher")]
        [Select2("Teacher", "String", false, "")]
        //[LookUp("LookUps.Employee")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Teacher", "LookUps.Teacher")]
        public KeyValueViewModel HeadTeacher { get; set; }
        public long? HeadTeacherID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("ClassTeacher")]
        [Select2("Teacher", "Numeric", true, "")]
        //[LookUp("LookUps.Employee")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Teacher", "LookUps.Teacher")]
        public List<KeyValueViewModel> ClassTeacher { get; set; }
        public long? ClassTeacherID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Subject")]
        [Select2("Subject", "Numeric", false, "")]
        [LookUp("LookUps.Subject")]
        public KeyValueViewModel Subject { get; set; }

        public int? SubjectID { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as ClassGroupDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<ClassGroupViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<ClassGroupDTO, ClassGroupViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var classGroup = dto as ClassGroupDTO;
            var sDto = dto as ClassGroupDTO;
            var vm = Mapper<ClassGroupDTO, ClassGroupViewModel>.Map(dto as ClassGroupDTO);

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<ClassGroupViewModel, ClassGroupDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            //Mapper<ClassSubjectMapViewModel, ClassSubjectMapDTO>.CreateMap();
            //Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            //var dto = Mapper<ClassSubjectMapViewModel, ClassSubjectMapDTO>.Map(this);

            var dto = Mapper<ClassGroupViewModel, ClassGroupDTO>.Map(this);

            dto.HeadTeacherID = string.IsNullOrEmpty(this.HeadTeacher.Key) ? (long?)null : long.Parse(this.HeadTeacher.Key);
            dto.SubjectID = string.IsNullOrEmpty(this.Subject.Key) ? (int?)null : int.Parse(this.Subject.Key);

            List<KeyValueDTO> classTeacherList = new List<KeyValueDTO>();

            foreach (KeyValueViewModel vm in this.ClassTeacher)
            {
                classTeacherList.Add(new KeyValueDTO { Key = vm.Key, Value = vm.Value }
                    );
            }
            dto.ClassTeacher = classTeacherList;

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<ClassGroupDTO>(jsonString);
        }
    }
}

