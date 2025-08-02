using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;
using System.Globalization;
using Eduegate.Framework.Extensions;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Exams
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "RemarksEntry", "CRUDModel.ViewModel")]
    [DisplayName("Remarks Entry")]
    public class RemarksEntryViewModel : BaseMasterViewModel
    {
        public RemarksEntryViewModel()
        {
            StudentList = new List<RemarksEntryStudentListViewModel>() { new RemarksEntryStudentListViewModel() };
        }

        public long RemarksEntryIID { get; set; }

        public long? TeacherID { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-disabled=CRUDModel.ViewModel.RemarksEntryIID == 0")]
        [LookUp("LookUps.ExamGroups")]
        [CustomDisplay("AssesmentTerm")]
        public string ExamGroupName { get; set; }
        public long? ExamGroupID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("MainTeacherClasses", "Numeric", false, "ClassSectionChange($event, $element, CRUDModel.ViewModel)", optionalAttribute1: "ng-disabled=CRUDModel.ViewModel.RemarksEntryIID == 0")]
        [LookUp("LookUps.Classes")]
        [CustomDisplay("Class")]
        public KeyValueViewModel StudentClass { get; set; }
        public int? ClassID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("MainTeacherSections", "Numeric", false, "ClassSectionChange($event, $element, CRUDModel.ViewModel)", optionalAttribute1: "ng-disabled=CRUDModel.ViewModel.RemarksEntryIID == 0")]
        [LookUp("LookUps.Section")]
        [CustomDisplay("Section")]
        public KeyValueViewModel Section { get; set; }
        public int? SectionID { get; set; }

        
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("ClasssectionStudents", "Numeric", false, "StudentSearch($event, $element, CRUDModel.ViewModel)")]
        [LookUp("LookUps.ClasssectionStudents")]
        [CustomDisplay("Student")]
        public KeyValueViewModel StudentSearch { get; set; }



        [ControlType(Framework.Enums.ControlTypes.Button, "textleft", "ng-click='ClassSectionChange($event, $element, CRUDModel.Model.MasterViewModel)'")]
        [CustomDisplay("Refresh")]
        public string Refresh { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("StudentList")]
        public List<RemarksEntryStudentListViewModel> StudentList { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as RemarksEntryDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<RemarksEntryViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<RemarksEntryDTO, RemarksEntryViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var rmrksdto = dto as RemarksEntryDTO;
            var vm = Mapper<RemarksEntryDTO, RemarksEntryViewModel>.Map(rmrksdto);

            vm.RemarksEntryIID = rmrksdto.RemarksEntryIID;
            vm.ExamGroupID = rmrksdto.ExamGroupID;
            vm.ExamGroupName = rmrksdto.ExamGroupID.ToString();
            vm.ClassID = rmrksdto.ClassID;
            vm.SectionID = rmrksdto.SectionID;
            vm.StudentClass = rmrksdto.ClassID.HasValue ? new KeyValueViewModel() { Key = rmrksdto.Class.Key, Value = rmrksdto.Class.Value } : new KeyValueViewModel();
            vm.Section = rmrksdto.SectionID.HasValue ? new KeyValueViewModel() { Key = rmrksdto.Section.Key, Value = rmrksdto.Section.Value } : new KeyValueViewModel();

            vm.StudentList = new List<RemarksEntryStudentListViewModel>();
            foreach (var studMap in rmrksdto.StudentsRemarks)
            {
                //var remarkExamMaps = new List<RemarksEntryExamMapViewModel>();

                //foreach (var ExamMap in studMap.RemarksExam)
                //{
                //    if (ExamMap.SubjectID.HasValue)
                //    {
                //        remarkExamMaps.Add(new RemarksEntryExamMapViewModel()
                //        {
                //            RemarksEntryExamMapIID = ExamMap.RemarksEntryExamMapIID,
                //            ExamID = ExamMap.ExamID,
                //            SubjectID = ExamMap.SubjectID,
                //            Remarks = ExamMap.Remarks,
                //            Exam = ExamMap.ExamID.HasValue ? new KeyValueViewModel() { Key = ExamMap.ExamID.Value.ToString(), Value = ExamMap.ExamName } : new KeyValueViewModel(),
                //            Subject = ExamMap.SubjectID.HasValue ?  new KeyValueViewModel() { Key = ExamMap.SubjectID.Value.ToString(), Value = ExamMap.SubjectName } : new KeyValueViewModel(),
                //        });
                //    }
                //}

                vm.StudentList.Add(new RemarksEntryStudentListViewModel()
                {
                    RemarksEntryStudentMapIID = studMap.RemarksEntryStudentMapIID,
                    StudentID = studMap.StudentID,
                    StudentName = studMap.StudentID.HasValue ? studMap.StudentName : null,
                    Remarks1 = studMap.Remarks1,
                    //Remarks2 = studMap.Remarks2,
                    //ExamSubjectGrid = remarkExamMaps,
                });
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<RemarksEntryViewModel, RemarksEntryDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<RemarksEntryViewModel, RemarksEntryDTO>.Map(this);

            dto.ClassID = string.IsNullOrEmpty(this.StudentClass.Key) ? (int?)null : int.Parse(this.StudentClass.Key);
            dto.SectionID = string.IsNullOrEmpty(this.Section.Key) ? (int?)null : int.Parse(this.Section.Key);
            dto.ExamGroupID = string.IsNullOrEmpty(this.ExamGroupName) ? (int?)null : int.Parse(this.ExamGroupName);

            dto.StudentsRemarks = new List<RemarksEntryStudentsDTO>();

            foreach (var studList in this.StudentList)
            {
                //var remarkExm = new List<RemarksEntryExamMapDTO>();

                //foreach (var ExmSubRemark in studList.ExamSubjectGrid)
                //{
                //    if (ExmSubRemark != null)
                //    {
                //        if (ExmSubRemark.Subject != null)
                //        {
                //            if (ExmSubRemark.Subject.Key != null)
                //            {
                //                remarkExm.Add(new RemarksEntryExamMapDTO
                //                {
                //                    RemarksEntryExamMapIID = ExmSubRemark.RemarksEntryExamMapIID,
                //                    ExamID = ExmSubRemark.Exam != null || string.IsNullOrEmpty(ExmSubRemark.Exam.Key) ? (long?)null : long.Parse(ExmSubRemark.Exam.Key),
                //                    SubjectID = !string.IsNullOrEmpty(ExmSubRemark.Subject.Key) ? int.Parse(ExmSubRemark.Subject.Key) : (int?)null,
                //                    Remarks = ExmSubRemark.Remarks,
                //                });
                //            }
                //        }
                //    }

                //}

                if (studList.StudentID.HasValue && studList.Remarks1 != null)
                {
                    dto.StudentsRemarks.Add(new RemarksEntryStudentsDTO()
                    {
                        RemarksEntryStudentMapIID = studList.RemarksEntryStudentMapIID,
                        StudentID = studList.StudentID,
                        Remarks1 = studList.Remarks1,
                        //Remarks2 = studList.Remarks2,
                        //RemarksExam = remarkExm,
                    });
                }
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<RemarksEntryDTO>(jsonString);
        }

    }
}