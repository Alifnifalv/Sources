using Eduegate.Domain.Entity.School.Models;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Lms;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Services.Contracts.School.Inventory;
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
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "Units", "CRUDModel.ViewModel")]
    [DisplayName("Units")]
    public class UnitsViewModel : BaseMasterViewModel
    {
        public UnitsViewModel()
        {
            Class = new KeyValueViewModel();
            Chapter = new KeyValueViewModel();
        }

        public long UnitIID { get; set; }  // Primary Key


        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.AcademicYear")]
        [CustomDisplay("Academic Year")]
        public string AcademicYear { get; set; }
        public int? AcademicYearID { get; set; }

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
        [CustomDisplay("Unit Title")]
        public string UnitTitle { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("Description")]
        public string Description { get; set; }

        [Required]
        [MaxLength(8, ErrorMessage = "Maximum Length should be within 8!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Total Periods")]
        public long? TotalPeriods { get; set; }

        [Required]
        [MaxLength(8, ErrorMessage = "Maximum Length should be within 8!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Total Hours")]
        public long? TotalHours { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("ParentSubjectUnit", "Numeric", false, "")]
        [LookUp("LookUps.SubjectUnits")]  // Fetch options dynamically
        [CustomDisplay("Parent Subject Unit")]
        public KeyValueViewModel ParentSubjectUnit { get; set; }
        public long? ParentSubjectUnitID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Chapter", "Numeric", false, "")]
        [LookUp("LookUps.Chapters")]  // Fetch options dynamically
        [CustomDisplay("Chapter")]
        public KeyValueViewModel Chapter { get; set; }
        public long? ChapterID { get; set; }


        public string CreatedDateString { get; set; }



        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as SubjectUnitDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<UnitsViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<SubjectUnitDTO, UnitsViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();

            var mpdto = dto as SubjectUnitDTO;
            var vm = Mapper<SubjectUnitDTO, UnitsViewModel>.Map(mpdto);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            vm.UnitIID = mpdto.UnitIID;
            vm.ParentSubjectUnit = new KeyValueViewModel() { Key = mpdto.SubjectUnit.Key, Value = mpdto.SubjectUnit.Value };
            vm.Chapter =  new KeyValueViewModel() { Key = mpdto.Chapter.Key, Value = mpdto.Chapter.Value };
            vm.AcademicYear = mpdto.AcademicYearID?.ToString();
            vm.Class =  new KeyValueViewModel() { Key = mpdto.Class.Key, Value = mpdto.Class.Value };
            vm.Subject = new KeyValueViewModel() { Key = mpdto.Subject.Key, Value = mpdto.Subject.Value};
            vm.ParentSubjectUnit = new KeyValueViewModel() { Key = mpdto.SubjectUnit.Key, Value = mpdto.SubjectUnit.Value };

            vm.UnitTitle = mpdto.UnitTitle;
            vm.Description = mpdto.Description;
            vm.TotalPeriods = mpdto.TotalPeriods;
            vm.TotalHours = mpdto.TotalHours;
            vm.ParentSubjectUnitID = mpdto.ParentSubjectUnitID;
            vm.ChapterID = mpdto.ChapterID;
            return vm;
        }


        // Convert ViewModel to DTO
        public override BaseMasterDTO ToDTO()
        {
            Mapper<UnitsViewModel, SubjectUnitDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();

            var dto = Mapper<UnitsViewModel, SubjectUnitDTO>.Map(this);

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");



            dto.UnitIID = this.UnitIID;
            dto.ParentSubjectUnitID = string.IsNullOrEmpty(this.ParentSubjectUnit.Key) ? (int?)null : int.Parse(this.ParentSubjectUnit.Key);
            dto.ChapterID =  string.IsNullOrEmpty(this.Chapter.Key) ? (int?)null : int.Parse(this.Chapter.Key);
            dto.AcademicYearID = string.IsNullOrEmpty(this.AcademicYear) ? (int?)null : int.Parse(this.AcademicYear);
            dto.ClassID = string.IsNullOrEmpty(this.Class.Key) ? (int?)null : int.Parse(this.Class.Key);
            dto.SubjectID = string.IsNullOrEmpty(this.Subject.Key) ? (int?)null : int.Parse(this.Subject.Key);
            dto.UnitTitle = this.UnitTitle;
            dto.Description = this.Description;
            dto.TotalPeriods = this.TotalPeriods;
            dto.TotalHours = this.TotalHours;

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<SubjectUnitDTO>(jsonString);
        }
        // Convert DTO to ViewModel
    }
}