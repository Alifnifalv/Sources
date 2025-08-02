using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;
using System.Globalization;
using Eduegate.Framework.Extensions;
using System.Linq;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Academics
{
    public class ClassSubjectMapViewModel : BaseMasterViewModel
    {
        public ClassSubjectMapViewModel()
        {
            //Class = new KeyValueViewModel();
            Subjects = new List<KeyValueViewModel>();
            Sections = new List<KeyValueViewModel>();
            IsClassDisable = false;
            WorkFlowList = new List<ClassSubjectWorkFlowListViewModel>() { new ClassSubjectWorkFlowListViewModel() };
        }

        public long ClassSubjectMapIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Class", "Numeric", false,  optionalAttribute1: "ng-disabled=CRUDModel.ViewModel.IsClassDisable")]
        [LookUp("LookUps.Classes")]
        [CustomDisplay("Class")]
        public KeyValueViewModel StudentClass { get; set; }

        public int? ClassID { get; set; }

        public bool IsClassDisable { get; set; }
        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Section", "Numeric", true)]
        [LookUp("LookUps.Section")]
        [CustomDisplay("Section")]
        public List<KeyValueViewModel> Sections { get; set; }

        public int? SectionID { get; set; }
        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Subjects", "Numeric", true/*, optionalAttribute1: "ng-change='FillWorkFlowWithSubject($event, $element,CRUDModel.ViewModel)'"*/)]
        [LookUp("LookUps.Subject")]
        [CustomDisplay("Subject")]
        public List<KeyValueViewModel> Subjects { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Workflow")]
        public List<ClassSubjectWorkFlowListViewModel> WorkFlowList { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as ClassSubjectMapDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<ClassSubjectMapViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<ClassSubjectMapDTO, ClassSubjectMapViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var subDto = dto as ClassSubjectMapDTO;
            var vm = Mapper<ClassSubjectMapDTO, ClassSubjectMapViewModel>.Map(subDto);
            vm.ClassID = subDto.ClassID;
            //vm.SectionID = subDto.SectionID;
            vm.ClassSubjectMapIID = subDto.ClassSubjectMapIID;
            //vm.Section = new KeyValueViewModel() { Key = vm.SectionID.ToString(), Value = subDto.SectionName };
            vm.IsClassDisable = true;
            vm.StudentClass = subDto.ClassID.HasValue ? new KeyValueViewModel()
            {
                Key = subDto.Class.Key,
                Value = subDto.Class.Value
            } : new KeyValueViewModel();

            foreach (var listSub in subDto.Subject)
            {
                vm.Subjects.Add(new KeyValueViewModel()
                {
                    Key = listSub.Key,
                    Value = listSub.Value
                });
            }

            foreach (var listSec in subDto.Section)
            {
                vm.Sections.Add(new KeyValueViewModel()
                {
                    Key = listSec.Key,
                    Value = listSec.Value
                });
            }

            vm.WorkFlowList = new List<ClassSubjectWorkFlowListViewModel>();
            foreach (var workflowdMap in subDto.ClassSubjectWorkFlow)
            {
                vm.WorkFlowList.Add(new ClassSubjectWorkFlowListViewModel()
                {
                    ClassSubjectWorkflowEntityMapIID = workflowdMap.ClassSubjectWorkflowEntityMapIID,
                    ClassSubjectMapID = workflowdMap.ClassSubjectMapID,
                    Subject = workflowdMap.SubjectID.HasValue ? workflowdMap.SubjectID.ToString() : null,
                    WorkFlow2 = workflowdMap.workflowID.HasValue ? workflowdMap.workflowID.ToString() : null,
                    WorkFlow1 = workflowdMap.WorkflowEntityID.HasValue ? workflowdMap.WorkflowEntityID.ToString() : null,
                });
            }
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<ClassSubjectMapViewModel, ClassSubjectMapDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<ClassSubjectMapViewModel, ClassSubjectMapDTO>.Map(this);
            dto.ClassSubjectMapIID = this.ClassSubjectMapIID;
            dto.ClassID = string.IsNullOrEmpty(this.StudentClass.Key) ? (int?)null : int.Parse(this.StudentClass.Key);
            dto.ClassSectionSubjectList = new ClassSectionSubjectListMapDTO();

            if (this.Subjects != null)
            {
                foreach (var splitSubj in Subjects)
                {
                    foreach (var splitSec in Sections)
                    {
                        dto.ListData.Add(new ClassSectionSubjectListMapDTO()
                        {
                            SectionID = int.Parse(splitSec.Key),
                            SubjectID = int.Parse(splitSubj.Key),
                        });
                    }
                }
            }

            dto.ClassSubjectWorkFlow = new List<ClassSubjectWorkFlowMapDTO>();

            foreach (var dataList in this.WorkFlowList)
            {
                if (dataList.Subject != null)
                {
                    dto.ClassSubjectWorkFlow.Add(new ClassSubjectWorkFlowMapDTO()
                    {
                        ClassSubjectWorkflowEntityMapIID = dataList.ClassSubjectWorkflowEntityMapIID,
                        ClassSubjectMapID = dataList.ClassSubjectMapID,
                        SubjectID = string.IsNullOrEmpty(dataList.Subject) ? (int?)null : int.Parse(dataList.Subject),
                        workflowID = string.IsNullOrEmpty(dataList.WorkFlow2) ? (long?)null : long.Parse(dataList.WorkFlow2),
                        WorkflowEntityID = string.IsNullOrEmpty(dataList.WorkFlow1) ? (int?)null : int.Parse(dataList.WorkFlow1),
                    });
                }
            }

                return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<ClassSubjectMapDTO>(jsonString);
        }

        public static List<KeyValueViewModel> ToKeyValueViewModel(List<ClassSubjectMapDTO> dtos)
        {
            var vMs = new List<KeyValueViewModel>();

            foreach (var dto in dtos)
            {
                //vMs.Add(new KeyValueViewModel() { Key = dto.Section.Key.ToString(), Value = dto.Section.Value });
                vMs.Add(new KeyValueViewModel() { Key = dto.Class.Key.ToString(), Value = dto.Class.Value });
            }

            return vMs;
        }

    }
}

