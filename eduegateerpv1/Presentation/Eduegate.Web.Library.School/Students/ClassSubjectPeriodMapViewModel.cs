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

namespace Eduegate.Web.Library.School.Students
{
    public class ClassSubjectPeriodMapViewModel : BaseMasterViewModel
    {
        public ClassSubjectPeriodMapViewModel()
        {
            //Class = new KeyValueViewModel();
            Section = new List<KeyValueViewModel>();
            //OtherTeacher = new List<KeyValueViewModel>();
            //AssociateTeacher = new List<KeyValueViewModel>();
            OtherClassTeachers = new List<ClassSubjectPeriodMapMapViewModel>() { new ClassSubjectPeriodMapMapViewModel() };
        }

        // [Required]
        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("Id")]
        public long ClassClassTeacherMapIID { get; set; }

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
        [Select2("Classes", "Numeric", false, "FillClassWiseDropDowns($event, $element, CRUDModel.ViewModel)", false,optionalAttribute1: "ng-disabled='CRUDModel.ViewModel.ClassClassTeacherMapIID !=0'")]
        [LookUp("LookUps.Classes")]
        [CustomDisplay("Class")]
        public KeyValueViewModel Class { get; set; }

        public int? ClassID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Section", "Numeric", true, "FillClassWiseDropDowns($event, $element, CRUDModel.ViewModel)", false, optionalAttribute1: "ng-disabled='CRUDModel.ViewModel.ClassClassTeacherMapIID != 0'")]
        [LookUp("LookUps.Section")]
        [CustomDisplay("Section")]
        public List<KeyValueViewModel> Section { get; set; }

        public int? SectionID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "textleft", "ng-click='RefreshButtonClicks($event, $element, CRUDModel.ViewModel)'")]
        [CustomDisplay("Refresh")]
        public string Refresh { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("ClassTeachersSubjectMap")]
        public List<ClassSubjectPeriodMapMapViewModel> OtherClassTeachers { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("Teacher", "Numeric", true, "")]
        //[LookUp("LookUps.Teacher")]
        //[DisplayName("Other Teacher")]
        //public List<KeyValueViewModel> OtherTeacher { get; set; }

        public long? OtherTheacherID { get; set; }

        public string SubjectName { get; set; }

        public string HighestAcademicQualitication { get; set; }

        public string Description { get; set; }

        public string EmployeePhoto { get; set; }

        public string WorkEmail { get; set; }

        public byte? SchoolID { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as ClassClassTeacherMapDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<ClassSubjectPeriodMapViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<ClassClassTeacherMapDTO, ClassSubjectPeriodMapViewModel>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var mapDTO = dto as ClassClassTeacherMapDTO;
            var vm = Mapper<ClassClassTeacherMapDTO, ClassSubjectPeriodMapViewModel>.Map(mapDTO);
            var ClsTcrDto = dto as ClassTeacherMapDTO;

            vm.ClassClassTeacherMapIID = mapDTO.ClassClassTeacherMapIID;
            vm.Class = mapDTO.ClassID.HasValue ? new KeyValueViewModel() { Key = mapDTO.ClassID.ToString(), Value = mapDTO.ClassName } : new KeyValueViewModel();
            //vm.Section = mapDTO.SectionID.HasValue ? new KeyValueViewModel() { Key = mapDTO.SectionID.ToString(), Value = mapDTO.SectionName } : new KeyValueViewModel();
            vm.SchoolID = mapDTO.SchoolID;
            vm.AcademicYearID = mapDTO.AcademicYearID;

            vm.OtherClassTeachers = new List<ClassSubjectPeriodMapMapViewModel>();

            foreach (var otherTeacherSubj in mapDTO.ClassTeacherMaps)
            {
                if (otherTeacherSubj.OtherTeacherID.HasValue)
                {
                    vm.OtherClassTeachers.Add(new ClassSubjectPeriodMapMapViewModel()
                    {
                        ClassTeacherMapIID = otherTeacherSubj.ClassTeacherMapIID,
                        ClassClassTeacherMapID = otherTeacherSubj.ClassClassTeacherMapID, 
                        OtherTeacherID = otherTeacherSubj.OtherTeacherID,
                        SubjectID = otherTeacherSubj.SubjectID,
                        OtherTeacher = otherTeacherSubj.OtherTeacherID.HasValue ? new KeyValueViewModel() { Key = otherTeacherSubj.OtherTeacherID.ToString(), Value = otherTeacherSubj.OtherTeacherName } : new KeyValueViewModel(),
                        Subject = otherTeacherSubj.SubjectID.HasValue ? new KeyValueViewModel() { Key = otherTeacherSubj.SubjectID.ToString(), Value = otherTeacherSubj.SubjectName } : new KeyValueViewModel(),
                        SchoolID = otherTeacherSubj.SchoolID,
                        AcademicYearID = otherTeacherSubj.AcademicYearID
                    });
                }
            }

            foreach (var AscTchr in mapDTO.ClassAssociateTeacherMaps)
            {
                vm.Section.Add(new KeyValueViewModel()
                {
                    Key = AscTchr.Key,
                    Value = AscTchr.Value
                });
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<ClassSubjectPeriodMapViewModel, ClassClassTeacherMapDTO>.CreateMap();
            var dto = Mapper<ClassSubjectPeriodMapViewModel, ClassClassTeacherMapDTO>.Map(this);
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            //dto.SectionID = string.IsNullOrEmpty(this.Section.Key) ? (int?)null : int.Parse(this.Section.Key);
            dto.ClassID = string.IsNullOrEmpty(this.Class.Key) ? (int?)null : int.Parse(this.Class.Key);
            dto.SchoolID = this.SchoolID;
            dto.AcademicYearID = this.AcademicYearID;
            dto.ClassClassTeacherMapIID = this.ClassClassTeacherMapIID;

            List<KeyValueDTO> sections = new List<KeyValueDTO>();

            foreach (KeyValueViewModel vm in this.Section)
            {
                sections.Add(new KeyValueDTO { Key = vm.Key, Value = vm.Value });
            }
            dto.ClassAssociateTeacherMaps = sections;

            dto.ClassTeacherMaps = new List<ClassTeacherMapDTO>();

            foreach (var otherTeacherMap in this.OtherClassTeachers)
            {
                if (otherTeacherMap.Subject.Key != null)
                {
                    dto.ClassTeacherMaps.Add(new ClassTeacherMapDTO()
                    {
                        ClassTeacherMapIID = otherTeacherMap.ClassTeacherMapIID,
                        ClassClassTeacherMapID = otherTeacherMap.ClassClassTeacherMapID,
                        OtherTeacherID = string.IsNullOrEmpty(otherTeacherMap.OtherTeacher.Key) ? (long?)null : int.Parse(otherTeacherMap.OtherTeacher.Key),
                        SubjectID = string.IsNullOrEmpty(otherTeacherMap.Subject.Key) ? (int?)null : int.Parse(otherTeacherMap.Subject.Key),
                        SubjectName = string.IsNullOrEmpty(otherTeacherMap.Subject.Value) ? (string)null : (otherTeacherMap.Subject.Value).ToString(),
                        SchoolID = otherTeacherMap.SchoolID,
                        AcademicYearID = otherTeacherMap.AcademicYearID
                    });
                }
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<ClassClassTeacherMapDTO>(jsonString);
        }
    }
}