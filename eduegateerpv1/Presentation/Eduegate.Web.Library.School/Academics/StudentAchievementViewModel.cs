using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Runtime.Serialization;

namespace Eduegate.Web.Library.School.Academics
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "LessonPlan", "CRUDModel.ViewModel")]
    [DisplayName("LessonPlan")]
    public class StudentAchievementViewModel : BaseMasterViewModel
    {
        public StudentAchievementViewModel()
        {
        }

        public long StudentAchievementIID { get; set; }

        public byte? SchoolID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.AcademicYear")]
        [CustomDisplay("AcademicYear")]
        public string AcademicYear { get; set; }
        public int? AcademicYearID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Students", "Numeric", false, "StudentChanges(CRUDModel.ViewModel)")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Student", "LookUps.Students")]
        [CustomDisplay("Student")]
        public KeyValueViewModel Student { get; set; }
        public long? StudentID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Class")]
        public string Class { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Section")]
        public string Section { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.AchievementCategory")]
        [DisplayName("Category")]
        public string AchievementCategory { get; set; }
        public int? CategoryID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.AchievementType")]
        [DisplayName("Type")]
        public string AchievementType { get; set; }
        public int? TypeID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Title")]
        public string Title { get; set; }

        

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Venue")]
        public string AchievementDescription { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("HeldOn")]
        public String HeldOnDateString { get; set; }

        public DateTime? HeldOnDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.AchievementRanking")]
        [DisplayName("Ranking")]
        public string AchievementRanking { get; set; }
        public int? RankingID { get; set; }




        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as StudentAchievementDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<StudentAchievementViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<StudentAchievementDTO, StudentAchievementViewModel>.CreateMap();
            var achDTO = dto as StudentAchievementDTO;
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var vm = Mapper<StudentAchievementDTO, StudentAchievementViewModel>.Map(achDTO);

            vm.StudentAchievementIID = achDTO.StudentAchievementIID;
            vm.SchoolID = achDTO.SchoolID;
            vm.AcademicYear = achDTO.AcademicYearID.HasValue ? achDTO.AcademicYearID.ToString() : null;
            vm.Student = achDTO.StudentID.HasValue ? new KeyValueViewModel()
            {
                Key = achDTO.StudentID.ToString(),
                Value = achDTO.StudentName
            } : new KeyValueViewModel();
            vm.Class = achDTO.Class;
            vm.Section = achDTO.Section;
            vm.AchievementType = achDTO.TypeID.HasValue ? achDTO.TypeID.ToString() : null;
            vm.AchievementCategory = achDTO.CategoryID.HasValue ? achDTO.CategoryID.ToString() : null;
            vm.AchievementRanking = achDTO.RankingID.HasValue ? achDTO.RankingID.ToString() : null;
            vm.AchievementDescription = achDTO.AchievementDescription;
            vm.Title= achDTO.Title;
            vm.HeldOnDateString = achDTO.HeldOnDate.HasValue ? achDTO.HeldOnDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;


            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<StudentAchievementViewModel, StudentAchievementDTO>.CreateMap();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var dto = Mapper<StudentAchievementViewModel, StudentAchievementDTO>.Map(this);

            dto.StudentAchievementIID = this.StudentAchievementIID;
            dto.AcademicYearID = string.IsNullOrEmpty(this.AcademicYear) ? (int?)null : int.Parse(this.AcademicYear);
            dto.StudentID = this.Student != null ? string.IsNullOrEmpty(this.Student.Key) ? (long?)null : long.Parse(this.Student.Key) : (long?)null;
            dto.TypeID = string.IsNullOrEmpty(this.AchievementType) ? (int?)null : int.Parse(this.AchievementType);
            dto.CategoryID = string.IsNullOrEmpty(this.AchievementCategory) ? (int?)null : int.Parse(this.AchievementCategory);
            dto.RankingID = string.IsNullOrEmpty(this.AchievementRanking) ? (int?)null : int.Parse(this.AchievementRanking);
            dto.SchoolID = this.SchoolID;
            dto.AchievementDescription = this.AchievementDescription;
            dto.Title=this.Title;
            dto.HeldOnDate = string.IsNullOrEmpty(this.HeldOnDateString) ? (DateTime?)null : DateTime.ParseExact(this.HeldOnDateString, dateFormat, CultureInfo.InvariantCulture);

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<StudentAchievementDTO>(jsonString);
        }

    }
}