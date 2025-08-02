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
    public class ClassTeacherMapViewModel : BaseMasterViewModel
    {
        public ClassTeacherMapViewModel()
        {
            //Class = new KeyValueViewModel();
            //Section = new KeyValueViewModel();
            //OtherTeacher = new List<KeyValueViewModel>();
            AssociateTeacher = new List<KeyValueViewModel>();
            OtherClassTeachers = new List<ClassOtherTeacherMapViewModel>() { new ClassOtherTeacherMapViewModel() };
        }

        // [Required]
        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("Id")]
        public long ClassClassTeacherMapIID { get; set; }

        public long ClassTeacherMapIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Classes", "Numeric", false, "FillClassWiseDropDowns($event, $element, CRUDModel.ViewModel)", false,optionalAttribute1: "ng-disabled='CRUDModel.ViewModel.ClassClassTeacherMapIID !=0'")]
        [LookUp("LookUps.Classes")]
        [CustomDisplay("Class")]
        public KeyValueViewModel StudentClass { get; set; }

        public int? ClassID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2(" Section", "Numeric", false, "FillClassWiseDropDowns($event, $element, CRUDModel.ViewModel)", false, optionalAttribute1: "ng-disabled='CRUDModel.ViewModel.ClassClassTeacherMapIID != 0'")]
        [LookUp("LookUps.Section")]
        [CustomDisplay("Section")]
        public KeyValueViewModel Section { get; set; }

        public int? SectionID { get; set; }

        public int? OldSectionID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Teacher", "String", false, "")]
        [LookUp("LookUps.Teacher")]
        [CustomDisplay("ClassTeacher")]
        public KeyValueViewModel Employee { get; set; }

        public long? EmployeeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Teacher", "String", false, "")]
        [LookUp("LookUps.Coordinator")]
        [CustomDisplay("Coordinator")]
        public KeyValueViewModel Coordinator { get; set; }

        public long? CoordinatorID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Teacher", "Numeric", true)]
        [LookUp("LookUps.Teacher")]
        [CustomDisplay("Associate Teacher")]
        public List<KeyValueViewModel> AssociateTeacher { get; set; }
        public int? ClassAssociateTeacherMapIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "textleft", "ng-click='RefreshButtonClicks($event, $element, CRUDModel.ViewModel)'")]
        [CustomDisplay("Refresh")]
        public string Refresh { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("ClassTeachersSubjectMap")]
        public List<ClassOtherTeacherMapViewModel> OtherClassTeachers { get; set; }

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

        public int? AcademicYearID { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as ClassClassTeacherMapDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<ClassTeacherMapViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<ClassClassTeacherMapDTO, ClassTeacherMapViewModel>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var mapDTO = dto as ClassClassTeacherMapDTO;
            var vm = Mapper<ClassClassTeacherMapDTO, ClassTeacherMapViewModel>.Map(mapDTO);
            var ClsTcrDto = dto as ClassTeacherMapDTO;
            vm.Employee = mapDTO.ClassTeacherID.HasValue ? new KeyValueViewModel() { Key = mapDTO.ClassTeacherID.ToString(), Value = mapDTO.HeadTeacherName } : new KeyValueViewModel();
            vm.StudentClass = mapDTO.ClassID.HasValue ? new KeyValueViewModel() { Key = mapDTO.ClassID.ToString(), Value = mapDTO.ClassName } : new KeyValueViewModel();
            vm.Section = mapDTO.SectionID.HasValue ? new KeyValueViewModel() { Key = mapDTO.SectionID.ToString(), Value = mapDTO.SectionName } : new KeyValueViewModel();
            vm.Coordinator = mapDTO.CoordinatorID.HasValue ? new KeyValueViewModel() { Key = mapDTO.CoordinatorID.ToString(), Value = mapDTO.CoordinatorName } : new KeyValueViewModel();
            vm.SchoolID = mapDTO.SchoolID;
            vm.AcademicYearID = mapDTO.AcademicYearID;
            vm.OldSectionID = mapDTO.OldSectionID;
            vm.ClassClassTeacherMapIID = mapDTO.ClassClassTeacherMapIID;

            vm.OtherClassTeachers = new List<ClassOtherTeacherMapViewModel>();

            foreach (var otherTeacherSubj in mapDTO.ClassTeacherMaps)
            {
                if (otherTeacherSubj.OtherTeacherID.HasValue)
                {
                    vm.OtherClassTeachers.Add(new ClassOtherTeacherMapViewModel()
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
                vm.AssociateTeacher.Add(new KeyValueViewModel()
                {
                    Key = AscTchr.Key,
                    Value = AscTchr.Value
                });
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<ClassTeacherMapViewModel, ClassClassTeacherMapDTO>.CreateMap();
            var dto = Mapper<ClassTeacherMapViewModel, ClassClassTeacherMapDTO>.Map(this);
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            dto.ClassTeacherID = string.IsNullOrEmpty(this.Employee.Key) ? (long?)null : long.Parse(this.Employee.Key);
            dto.SectionID = string.IsNullOrEmpty(this.Section.Key) ? (int?)null : int.Parse(this.Section.Key);
            dto.ClassID = string.IsNullOrEmpty(this.StudentClass.Key) ? (int?)null : int.Parse(this.StudentClass.Key);
            dto.CoordinatorID = this.Coordinator != null ? string.IsNullOrEmpty(this.Coordinator.Key) ? (long?)null : long.Parse(this.Coordinator.Key) : null;
            dto.SchoolID = this.SchoolID;
            dto.AcademicYearID = this.AcademicYearID;
            dto.ClassClassTeacherMapIID = this.ClassClassTeacherMapIID;

            List<KeyValueDTO> assocaite = new List<KeyValueDTO>();

            foreach (KeyValueViewModel vm in this.AssociateTeacher)
            {
                assocaite.Add(new KeyValueDTO { Key = vm.Key, Value = vm.Value });
            }
            dto.ClassAssociateTeacherMaps = assocaite;

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