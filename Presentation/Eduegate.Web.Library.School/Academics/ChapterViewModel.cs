using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Lms;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace Eduegate.Web.Library.School.Academics
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "Chapter", "CRUDModel.ViewModel")]
    [DisplayName("Chapter")]
    public class ChapterViewModel : BaseMasterViewModel
    {
        public ChapterViewModel()
        {
            Class = new KeyValueViewModel();
            Subject = new KeyValueViewModel();

        }

        public long ChapterIID { get; set; }  // Primary Key

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.AcademicYear")]
        [CustomDisplay("Academic Year")]
        public string AcademicYear { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Class", "Numeric", false, "")]
        [LookUp("LookUps.Classes")]
        [CustomDisplay("Class")]
        public KeyValueViewModel Class { get; set; }
        public int? ClassID { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Subject", "Numeric", false, "")]

        [LookUp("LookUps.Subject")]
        [CustomDisplay("Subject")]
        public KeyValueViewModel Subject { get; set; }
        public int? SubjectID { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Maximum Length should be 100!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Chapter Title")]
        public string ChapterTitle { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("Description")]
        public string Description { get; set; }

        [Required]
        [MaxLength(8, ErrorMessage = "Maximum Length should be within 8!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Total Periods")]
        public int? TotalPeriods { get; set; }

        [Required]
        [MaxLength(8, ErrorMessage = "Maximum Length should be within 8!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Total Hours")]
        public decimal? TotalHours { get; set; }

        public DateTime? CreatedDate { get; set; }

        public List<ChapterDTO> Chapters { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as ChapterDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<ChapterViewModel>(jsonString);
        }

        // Convert ViewModel to DTO

        // Convert DTO to ViewModel
        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<ChapterDTO, ChapterViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();

            var mpdto = dto as ChapterDTO;
            var vm = Mapper<ChapterDTO, ChapterViewModel>.Map(mpdto);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");


            vm.ChapterIID = mpdto.ChapterIID;
            vm.AcademicYear = mpdto.AcademicYearID?.ToString();
            vm.Class =  new KeyValueViewModel() { Key = mpdto.Class.Key, Value = mpdto.Class.Value };
            vm.Subject = new KeyValueViewModel() { Key = mpdto.Subject.Key, Value = mpdto.Subject.Value };
            vm.ChapterTitle = mpdto.ChapterTitle;
            vm.Description = mpdto.Description;
            vm.TotalPeriods = mpdto.TotalPeriods;
            vm.TotalHours = mpdto.TotalHours;

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<ChapterViewModel, ChapterDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();

            var dto = Mapper<ChapterViewModel, ChapterDTO>.Map(this);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");


            dto.ChapterIID = this.ChapterIID;
            dto.AcademicYearID = string.IsNullOrEmpty(this.AcademicYear) ? (int?)null : int.Parse(this.AcademicYear);
            dto.ClassID = string.IsNullOrEmpty(this.Class.Key) ? (int?)null : int.Parse(this.Class.Key);
            dto.SubjectID = string.IsNullOrEmpty(this.Subject.Key) ? (int?)null : int.Parse(this.Subject.Key);
            dto.ChapterTitle = this.ChapterTitle;
            dto.Description = this.Description;
            dto.TotalPeriods = this.TotalPeriods;
            dto.TotalHours = (long?)this.TotalHours;

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<ChapterDTO>(jsonString);
        }
    }

}