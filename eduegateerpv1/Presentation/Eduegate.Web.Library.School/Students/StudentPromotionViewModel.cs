using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Framework.Extensions;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Students
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "StudentPromotion", "CRUDModel.ViewModel")]
    [DisplayName("Student Promotions")]
    public class StudentPromotionViewModel : BaseMasterViewModel
    {
        public StudentPromotionViewModel()
        {
            Student = new List<KeyValueViewModel>();
            ShiftFromClass = new List<KeyValueViewModel>();
            Class = new List<KeyValueViewModel>();
            ShiftFromSection = new List<KeyValueViewModel>();
            Section = new List<KeyValueViewModel>();
            PromoteStudent = new List<KeyValueViewModel>();
        }
        public long? StudentPromotionLogIID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[DisplayName("From Syllabus")]
        //[LookUp("LookUps.SchoolSyllabus")]

        //public string CurriculamString { get; set; }
        //public byte? CurriculamID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[DisplayName("To Syllabus")]
        //[LookUp("LookUps.SchoolSyllabus")]

        //public string CurriculamString2 { get; set; }
        //public byte? CurriculamID2 { get; set; }


        [ControlType(Eduegate.Framework.Enums.ControlTypes.CheckBox, "textleft2", Attributes = "ng-change=ChangePromotionStatus(CRUDModel.ViewModel)")]
        [CustomDisplay("Is Normal Promotion")]
        public bool? IsPromoted { get; set; } = true;

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-change='ChangePromotionStatus($event, $element,CRUDModel.ViewModel)'")]
        [LookUp("LookUps.PromotionStatus")]
        [CustomDisplay("PromotionStatus")]
        public string PromotionStatus { get; set; }

        public byte?  PromotionStatusID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine001 { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-change='SchoolChanges($event, $element,CRUDModel.ViewModel,1)'")]
        [CustomDisplay("FromSchool")]
        [LookUp("LookUps.School")]
        public string ShiftFromSchool { get; set; }
        public byte? ShiftFromSchoolID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-change='SchoolChanges($event, $element,CRUDModel.ViewModel,2)'")]
        [CustomDisplay("ToSchool")]
        [LookUp("LookUps.School")]
        public string ToASchool { get; set; }
        public byte? SchoolID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine01 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("FromAcademicyear")]
        [LookUp("LookUps.ShiftFromAcademicYear")]
        public string ShiftFromAcademicYear { get; set; }
        public int? ShiftFromAcademicYearID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("ToAcademicYear")]
        [LookUp("LookUps.SchoolAcademicyear")]
        public string SchoolAcademicyear { get; set; }
        public int? AcademicyearID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine02 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2(" Class", "Numeric", true, "FillStudentsByClassSection($event, $element,CRUDModel.ViewModel)")]
        [LookUp("LookUps.ShiftFromClass")]
        [CustomDisplay("FromClass")]
        public List<KeyValueViewModel> ShiftFromClass { get; set; }
        public int? ShiftFromClassID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2(" Class", "Numeric", true, "")]
        [LookUp("LookUps.ToClass")]
        [CustomDisplay("ToClass")]
        public List<KeyValueViewModel> Class { get; set; }
        public int? ClassID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2(" Section", "Numeric", true, "FillStudentsByClassSection($event, $element,CRUDModel.ViewModel)")]
        [LookUp("LookUps.ShiftFromSection")]
        [CustomDisplay("FromSection")]
        public List<KeyValueViewModel> ShiftFromSection { get; set; }
        public int? ShiftFromSectionID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2(" Section", "Numeric", true, "")]
        [LookUp("LookUps.ToSection")]
        [CustomDisplay("ToSection")]
        public List<KeyValueViewModel> Section { get; set; }
        public int? SectionID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Student", "Numeric", true)]
        [LookUp("LookUps.Students")]
        [CustomDisplay("Promote Student list")]
        public List<KeyValueViewModel> PromoteStudent { get; set; }
        public long? PromoteStudentID { get; set; }


        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("Student", "Numeric", true/*,optionalAttribute1: "ng-disabled='CRUDModel.ViewModel.IsPromoted'"*/)]
        ////[LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Student", "LookUps.Students")]
        //[LookUp("LookUps.Students")]
        //[CustomDisplay("StudentExceptList")]
        public List<KeyValueViewModel> Student { get; set; }
        public long? StudentID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [MaxLength(250, ErrorMessage = "Maximum Length should be within 250!")]
        [CustomDisplay("Remarks")]
        public string Remarks { get; set; } = "Successfully Promoted";

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as StudentPromotionDTO);
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<StudentPromotionViewModel, StudentPromotionDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<StudentPromotionViewModel, StudentPromotionDTO>.Map(this);

            List<KeyValueDTO> studentList = new List<KeyValueDTO>();
            List<KeyValueDTO> classList = new List<KeyValueDTO>();
            List<KeyValueDTO> shiftclassList = new List<KeyValueDTO>();
            List<KeyValueDTO> sectionList = new List<KeyValueDTO>();
            List<KeyValueDTO> shiftsectionList = new List<KeyValueDTO>();
            List<KeyValueDTO> promoteStudentList = new List<KeyValueDTO> ();

            dto.ShiftFromSchoolID = !string.IsNullOrEmpty(this.ShiftFromSchool) ? byte.Parse(this.ShiftFromSchool) : (byte?)0;
            dto.SchoolID = !string.IsNullOrEmpty(this.ToASchool) ? byte.Parse(this.ToASchool) : (byte?)0;
            dto.ShiftFromAcademicYearID =!string.IsNullOrEmpty(this.ShiftFromAcademicYear)? int.Parse(this.ShiftFromAcademicYear):0;
            dto.AcademicYearID = !string.IsNullOrEmpty(this.SchoolAcademicyear) ? int.Parse(this.SchoolAcademicyear) : 0;
            dto.IsPromoted = this.IsPromoted;
            dto.PromotionStatusID = string.IsNullOrEmpty(this.PromotionStatus) ? (byte?)null : byte.Parse(this.PromotionStatus);
            if (this.Student != null && this.Student.Count > 0)
            {
                foreach (KeyValueViewModel vm in this.Student)
                {
                    studentList.Add(new KeyValueDTO { Key = vm.Key, Value = vm.Value }
                        );
                }
            }
            dto.Student = studentList;

            if (this.PromoteStudent != null && this.PromoteStudent.Count > 0)
            {
                foreach (KeyValueViewModel vm in this.PromoteStudent)
                {
                    promoteStudentList.Add(new KeyValueDTO { Key = vm.Key, Value = vm.Value }
                        );
                }
            }
            dto.PromoteStudent = promoteStudentList;

            if (this.Class != null && this.Class.Count > 0)
            {
                foreach (KeyValueViewModel vm in this.Class)
                {
                    classList.Add(new KeyValueDTO { Key = vm.Key, Value = vm.Value }
                        );
                }
            }
            dto.Class = classList;

            if (this.ShiftFromClass != null && this.ShiftFromClass.Count > 0)
            {
                foreach (KeyValueViewModel vm in this.ShiftFromClass)
                {
                    shiftclassList.Add(new KeyValueDTO { Key = vm.Key, Value = vm.Value }
                        );
                }
            }
            dto.ShiftFromClass = shiftclassList;

            if (this.Section != null && this.Section.Count > 0)
            {
                foreach (KeyValueViewModel vm in this.Section)
                {
                    sectionList.Add(new KeyValueDTO { Key = vm.Key, Value = vm.Value }
                        );
                }
            }
            dto.Section = sectionList;

            if (this.ShiftFromSection != null && this.ShiftFromSection.Count > 0)
            {
                foreach (KeyValueViewModel vm in this.ShiftFromSection)
                {
                    shiftsectionList.Add(new KeyValueDTO { Key = vm.Key, Value = vm.Value }
                        );
                }
            }
            dto.ShiftFromSection = shiftsectionList;

            return dto;
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<StudentPromotionViewModel>(jsonString);
        }

    }
}