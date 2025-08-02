using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.Common;
using Eduegate.Framework.Extensions;

namespace Eduegate.Web.Library.School.Students
{
    public class ClassSubjectPeriodMapViewModel : BaseMasterViewModel
    {
        public ClassSubjectPeriodMapViewModel()
        {
            Class = new KeyValueViewModel();
            Section = new KeyValueViewModel();
            //OtherTeacher = new List<KeyValueViewModel>();
            //AssociateTeacher = new List<KeyValueViewModel>();
            SubjectMaps = new List<ClassSubjectPeriodMapMapViewModel>() { new ClassSubjectPeriodMapMapViewModel() };
        }

        // [Required]
        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("Id")]
        public long PeriodMapIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Academic Year")]
        [LookUp("LookUps.AcademicYear")]
        public string AcademicYear { get; set; }
        public int? AcademicYearID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Classes", "Numeric", false, "ClassChanges($event, $element,CRUDModel.ViewModel)", false,optionalAttribute1: "ng-disabled='CRUDModel.ViewModel.PeriodMapIID !=0'")]
        [LookUp("LookUps.Classes")]
        [CustomDisplay("Class")]
        public KeyValueViewModel Class { get; set; }

        public int? ClassID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Section", "Numeric", false, "", false, optionalAttribute1: "ng-disabled='CRUDModel.ViewModel.PeriodMapIID != 0'")]
        [LookUp("LookUps.Section")]
        [CustomDisplay("Section")]
        public KeyValueViewModel Section { get; set; }

        public int? SectionID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Button, "textleft", "ng-click='RefreshButtonClicks($event, $element, CRUDModel.ViewModel)'")]
        //[CustomDisplay("Refresh")]
        //public string Refresh { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("ClassTeachersSubjectMap")]
        public List<ClassSubjectPeriodMapMapViewModel> SubjectMaps { get; set; }

        public int? SchoolID { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as ClassSectionSubjectPeriodMapDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<ClassSubjectPeriodMapViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<ClassSectionSubjectPeriodMapDTO, ClassSubjectPeriodMapViewModel>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var mapDTO = dto as ClassSectionSubjectPeriodMapDTO;
            var vm = Mapper<ClassSectionSubjectPeriodMapDTO, ClassSubjectPeriodMapViewModel>.Map(mapDTO);
            var ClsTcrDto = dto as ClassTeacherMapDTO;

            vm.PeriodMapIID = mapDTO.PeriodMapIID;
            vm.Class = mapDTO.ClassID.HasValue ? new KeyValueViewModel() { Key = mapDTO.ClassID.ToString(), Value = mapDTO.ClassName } : new KeyValueViewModel();
            vm.Section = mapDTO.SectionID.HasValue ? new KeyValueViewModel() { Key = mapDTO.SectionID.ToString(), Value = mapDTO.SectionName } : new KeyValueViewModel();
            vm.AcademicYear = mapDTO.AcademicYearID.HasValue ? mapDTO.AcademicYearID.Value.ToString() : null;
            vm.SchoolID = mapDTO.SchoolID;
            vm.AcademicYearID = mapDTO.AcademicYearID;
            vm.SubjectMaps = new List<ClassSubjectPeriodMapMapViewModel>();

            foreach (var map in mapDTO.SubjectMapDetails)
            {
                vm.SubjectMaps.Add(new ClassSubjectPeriodMapMapViewModel()
                {
                    PeriodMapDetailIID = map.PeriodMapIID,
                    Subject = new KeyValueViewModel()
                    {
                        Key = map.Subject.Key,
                        Value = map.Subject.Value,
                    },
                    WeekPeriods = map.WeekPeriods,
                    TotalPeriods = map.TotalPeriods,
                    MinimumPeriods = map.MinimumPeriods,
                    MaximumPeriods = map.MaximumPeriods,
                });
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<ClassSubjectPeriodMapViewModel, ClassSectionSubjectPeriodMapDTO>.CreateMap();
            var dto = Mapper<ClassSubjectPeriodMapViewModel, ClassSectionSubjectPeriodMapDTO>.Map(this);
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();

            dto.PeriodMapIID = this.PeriodMapIID;
            dto.SectionID = string.IsNullOrEmpty(this.Section.Key) ? (int?)null : int.Parse(this.Section.Key);
            dto.ClassID = string.IsNullOrEmpty(this.Class.Key) ? (int?)null : int.Parse(this.Class.Key);
            dto.SchoolID = this.SchoolID;
            dto.AcademicYearID = this.AcademicYearID;

            dto.SubjectMapDetails = new List<ClassSectionSubjectPeriodMapMapDTO>();

            foreach (var map in this.SubjectMaps)
            {
                if (map.Subject.Key.IsNotNull())
                {
                    dto.SubjectMapDetails.Add(new ClassSectionSubjectPeriodMapMapDTO()
                    {
                        PeriodMapIID = map.PeriodMapDetailIID ?? 0,
                        SubjectID = string.IsNullOrEmpty(map.Subject.Key) ? (int?)null : int.Parse(map.Subject.Key),
                        WeekPeriods = map.WeekPeriods,
                        TotalPeriods = map.TotalPeriods,
                        MinimumPeriods = map.MinimumPeriods,
                        MaximumPeriods = map.MaximumPeriods,
                    });
                }
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<ClassSectionSubjectPeriodMapDTO>(jsonString);
        }
    }
}