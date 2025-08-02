using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.School.Academics
{
    public class TeacherActivityViewModel : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Id")]
        public long  TeacherActivityIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [DisplayName("EmployeeID")]
        public long?  EmployeeID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DateTimePicker)]
        [DisplayName("ActivityDate")]
        public System.DateTime?  ActivityDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TimePicker)]
        [DisplayName("TimeFrom")]
        public System.DateTime?  TimeFrom { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TimePicker)]
        [DisplayName("TimeTo")]
        public System.DateTime?  TimeTo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [DisplayName("SubjectID")]
        public int?  SubjectID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [DisplayName("ClassID")]
        public int?  ClassID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [DisplayName("SectionID")]
        public int?  SectionID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [DisplayName("ShiftID")]
        public byte?  ShiftID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [DisplayName("TopicID")]
        public long?  TopicID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [DisplayName("SubTopicID")]
        public long?  SubTopicID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [DisplayName("PeriodID")]
        public byte?  PeriodID { get; set; }
      
        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as TeacherActivityDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<TeacherActivityViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<TeacherActivityDTO, TeacherActivityViewModel>.CreateMap();
            var vm = Mapper<TeacherActivityDTO, TeacherActivityViewModel>.Map(dto as TeacherActivityDTO);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<TeacherActivityViewModel, TeacherActivityDTO>.CreateMap();
            var dto = Mapper<TeacherActivityViewModel, TeacherActivityDTO>.Map(this);
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<TeacherActivityDTO>(jsonString);
        }
    }
}

