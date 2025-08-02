using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Students
{
    public class StudentGroupMapViewModel : BaseMasterViewModel
    {
        public StudentGroupMapViewModel()
        {
            //Student = new KeyValueViewModel();
            //StudentGroups = new KeyValueViewModel();
            IsActive = true;


        }
        public long StudentGroupMapIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Student", "Numeric", false, "")]
        //[LookUp("LookUps.Student")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Student", "LookUps.Student")]
        [CustomDisplay("Student")]
        public KeyValueViewModel Student { get; set; }
        public long? StudentID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("StudentGroup", "Numeric", false, "")]
        [LookUp("LookUps.StudentGroup")]
        [CustomDisplay("StudentGroup")]
        public KeyValueViewModel StudentGroups { get; set; }
        public int? StudentGroupID { get; set; }

        [Required]
        [ControlType(Eduegate.Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as StudentGroupMapDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<StudentGroupMapViewModel>(jsonString);
        }
        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<StudentGroupMapDTO, StudentGroupMapViewModel>.CreateMap();
            var mapDTO = dto as StudentGroupMapDTO;
            var vm = Mapper<StudentGroupMapDTO, StudentGroupMapViewModel>.Map(mapDTO);
            vm.Student = new KeyValueViewModel() 
            { 
                Key = mapDTO.StudentID.ToString(), 
                Value = mapDTO.StudentName 
            };
            vm.StudentGroups = new KeyValueViewModel() 
            { 
                Key = mapDTO.StudentGroupID.ToString(), 
                Value = mapDTO.StudentGroup 
            };
            return vm;
        }
        public override BaseMasterDTO ToDTO()
        {
            Mapper<StudentGroupMapViewModel, StudentGroupMapDTO>.CreateMap();
            var dto = Mapper<StudentGroupMapViewModel, StudentGroupMapDTO>.Map(this);
            dto.StudentID = string.IsNullOrEmpty(this.Student.Key) ? (int?)null : int.Parse(this.Student.Key);
            dto.StudentGroupID = string.IsNullOrEmpty(this.StudentGroups.Key) ? (int?)null : int.Parse(this.StudentGroups.Key);
            
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<StudentGroupMapDTO>(jsonString);
        }
    }
}
