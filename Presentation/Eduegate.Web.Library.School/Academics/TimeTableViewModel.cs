using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Attendences;
using System.Globalization;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Academics
{
   public class TimeTableViewModel : BaseMasterViewModel
    {
        public TimeTableViewModel()
        {           
            //Academic = new KeyValueViewModel();
            IsActive = false;
        }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]        
        public int TimeTableID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("AcademicYear", "Numeric", false)]
        [LookUp("LookUps.AcademicYear")]
        [CustomDisplay("Academic Year")]
        public KeyValueViewModel Academic { get; set; }

        public int? AcademicYearID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Description")]
        public string TimeTableDescription { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as TimeTableDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<TimeTableViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            var tableDTO = dto as TimeTableDTO;
            Mapper<TimeTableDTO, TimeTableViewModel>.CreateMap();
            var vm = Mapper<TimeTableDTO, TimeTableViewModel>.Map(tableDTO);
            vm.TimeTableDescription = tableDTO.TimeTableDescription;
            vm.IsActive = tableDTO.IsActivice;
            vm.TimeTableID = tableDTO.TimeTableID;
            vm.AcademicYearID = tableDTO.AcademicYearID;
            vm.Academic = new KeyValueViewModel() { Key = tableDTO.AcademicYearID.ToString(), Value = tableDTO.AcademicYear.Value };          
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<TimeTableViewModel, TimeTableDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<TimeTableViewModel, TimeTableDTO>.Map(this);            
            dto.AcademicYearID = string.IsNullOrEmpty(this.Academic.Key) ? 0 : int.Parse(this.Academic.Key);           
            dto.TimeTableDescription = this.TimeTableDescription;
            dto.TimeTableID = this.TimeTableID;
            dto.IsActivice = this.IsActive;
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<TimeTableDTO>(jsonString);
        }
    }
}
