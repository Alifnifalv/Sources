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
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Exams
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "HealthEntry", "CRUDModel.ViewModel")]
    [DisplayName("Health Entry")]
    public class HealthEntryViewModel : BaseMasterViewModel
    {
        public HealthEntryViewModel()
        {
            StudentList = new List<HealthEntryStudentListViewModel>() { new HealthEntryStudentListViewModel() };
        }

        public long HealthEntryIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("AcademicYear", "Numeric", false,optionalAttribute1:"ng-disabled=CRUDModel.ViewModel.HealthEntryIID == 0")]
        [LookUp("LookUps.AcademicYear")]
        [CustomDisplay("Academic Year")]
        public KeyValueViewModel Academic { get; set; }
        public int? AcademicYearID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, attribs: "ng-disabled=CRUDModel.ViewModel.HealthEntryIID == 0")]
        [LookUp("LookUps.ExamGroups")]
        [CustomDisplay("AssesmentTerm")]
        public string ExamGroupName { get; set; }
        public long? ExamGroupID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Classes", "Numeric", false, "ClassSectionChange($event, $element, CRUDModel.ViewModel)", optionalAttribute1: "ng-disabled=CRUDModel.ViewModel.HealthEntryIID == 0")]
        [LookUp("LookUps.Classes")]
        [CustomDisplay("Class")]
        public KeyValueViewModel StudentClass { get; set; }
        public int? ClassID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Section", "Numeric", false, "ClassSectionChange($event, $element, CRUDModel.ViewModel)", optionalAttribute1: "ng-disabled=CRUDModel.ViewModel.HealthEntryIID == 0")]
        [LookUp("LookUps.Section")]
        [CustomDisplay("Section")]
        public KeyValueViewModel Section { get; set; }
        public int? SectionID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "textleft", "ng-click='ClassSectionChange($event, $element, CRUDModel.Model.MasterViewModel)'")]
        [CustomDisplay("Refresh")]
        public string Refresh { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("StudentList")]
        public List<HealthEntryStudentListViewModel> StudentList { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as HealthEntryDTO);
        }


        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<HealthEntryViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<HealthEntryDTO, HealthEntryViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var hlthdto = dto as HealthEntryDTO;
            var vm = Mapper<HealthEntryDTO, HealthEntryViewModel>.Map(hlthdto);

            vm.HealthEntryIID = hlthdto.HealthEntryIID;
            vm.AcademicYearID = hlthdto.AcademicYearID;
            vm.ClassID = hlthdto.ClassID;
            vm.SectionID = hlthdto.SectionID;
            vm.ExamGroupID = hlthdto.ExamGroupID;
            vm.ExamGroupName = hlthdto.ExamGroupID.ToString();
            vm.StudentClass = hlthdto.ClassID.HasValue ? new KeyValueViewModel() { Key = hlthdto.Class.Key, Value = hlthdto.Class.Value } : new KeyValueViewModel();
            vm.Section = hlthdto.SectionID.HasValue ? new KeyValueViewModel() { Key = hlthdto.Section.Key, Value = hlthdto.Section.Value } : new KeyValueViewModel();
            vm.Academic = hlthdto.AcademicYearID.HasValue ? new KeyValueViewModel() { Key = hlthdto.AcademicYear.Key, Value = hlthdto.AcademicYear.Value } : new KeyValueViewModel();

            vm.StudentList = new List<HealthEntryStudentListViewModel>();
            foreach (var studMap in hlthdto.HealthEntryStudentMap)
            {
                vm.StudentList.Add(new HealthEntryStudentListViewModel()
                {
                    StudentID = studMap.StudentID,
                    HealthEntryStudentMapIID = studMap.HealthEntryStudentMapIID,
                    StudentName = studMap.StudentName,
                    Height = studMap.Height,
                    Weight = studMap.Weight,
                    BMS = studMap.BMS,                   
                    Remarks = studMap.Remarks,
                });
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<HealthEntryViewModel, HealthEntryDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<HealthEntryViewModel, HealthEntryDTO>.Map(this);

            dto.ClassID = string.IsNullOrEmpty(this.StudentClass.Key) ? (int?)null : int.Parse(this.StudentClass.Key);
            dto.SectionID = string.IsNullOrEmpty(this.Section.Key) ? (int?)null : int.Parse(this.Section.Key);
            dto.AcademicYearID = string.IsNullOrEmpty(this.Academic.Key) ? (int?)null : int.Parse(this.Academic.Key);
            dto.ExamGroupID = string.IsNullOrEmpty(this.ExamGroupName) ? (int?)null : int.Parse(this.ExamGroupName);

            dto.HealthEntryStudentMap = new List<HealthEntryStudentMapDTO>();

            foreach (var studList in this.StudentList)
            {
                if (studList.StudentID.HasValue && studList.BMS != null)
                {
                    dto.HealthEntryStudentMap.Add(new HealthEntryStudentMapDTO()
                    {
                        HealthEntryStudentMapIID = studList.HealthEntryStudentMapIID,
                        StudentID = studList.StudentID,
                        Height = studList.Height,
                        Weight = studList.Weight,
                        BMS = studList.BMS,
                        Remarks = studList.Remarks,
                    });
                }
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<HealthEntryDTO>(jsonString);
        }

    }
}