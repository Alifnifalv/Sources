using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Fines;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Eduegate.Web.Library.School.Fines
{
  public  class FineMasterStudentMapNewViewModel : BaseMasterViewModel
    {
        public long FineMasterStudentMapIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Student", "Numeric", false, "")]
        //[LookUp("LookUps.Student")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Student", "LookUps.Student")]
        [CustomDisplay("StudentName")]
        public KeyValueViewModel Student { get; set; }
        public long? StudentId { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("FineMaster", "Numeric", false, "FineMasterChanges($event, $element, CRUDModel.ViewModel)")]
        [CustomDisplay("FineMaster")]
        [LookUp("LookUps.FineMaster")]
        public KeyValueViewModel FineMasterName { get; set; }
        public int? FineMasterID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("FineMapDate")]
        public string FineMapDateString { get; set; }      
        public DateTime? FineMapDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Amount")]
        public decimal? Amount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine13 { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("Remarks")]
        [StringLength(250, ErrorMessage = "Maximum Length should be within 250!")]
        public string Remarks { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as FineMasterStudentMapDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<FineMasterStudentMapNewViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<FineMasterStudentMapDTO, FineMasterStudentMapNewViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var fmDto = dto as FineMasterStudentMapDTO;
            var vm = Mapper<FineMasterStudentMapDTO, FineMasterStudentMapNewViewModel>.Map(fmDto);
            vm.Student = KeyValueViewModel.ToViewModel(fmDto.Student);
            vm.StudentId = fmDto.StudentId;
            vm.Amount = fmDto.Amount;
            vm.FineMasterID = fmDto.FineMasterID;
            vm.Remarks = fmDto.Remarks;
            vm.FineMapDate = fmDto.FineMapDate;
            vm.FineMasterName = KeyValueViewModel.ToViewModel(fmDto.FineMaster);
            vm.FineMapDateString= (fmDto.FineMapDate.HasValue ? fmDto.FineMapDate.Value : DateTime.Now).ToString(dateFormat, CultureInfo.InvariantCulture);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<FineMasterStudentMapNewViewModel, FineMasterStudentMapDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var dto = Mapper<FineMasterStudentMapNewViewModel, FineMasterStudentMapDTO>.Map(this);
            dto.StudentId  = long.Parse(this.Student.Key);
            dto.Amount = this.Amount;
            dto.FineMasterID = int.Parse(this.FineMasterName.Key);
            dto.Remarks = this.Remarks;
            dto.FineMapDate = string.IsNullOrEmpty(this.FineMapDateString) ? (DateTime?)null : DateTime.ParseExact(this.FineMapDateString, dateFormat, CultureInfo.InvariantCulture); 

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<FineMasterStudentMapDTO>(jsonString);
        }

    }
}
