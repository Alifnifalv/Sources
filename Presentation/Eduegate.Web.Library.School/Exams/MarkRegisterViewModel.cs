using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Exams
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "MarkRegister", "CRUDModel.ViewModel")]
    [DisplayName("MarkRegister Details")]
    public class MarkRegisterViewModel : BaseMasterViewModel
    {
        public MarkRegisterViewModel()
        {
            MarkRegisterDetails = new List<MarkRegisterDetailsViewmodel>() { new MarkRegisterDetailsViewmodel() };
        }

        public long MarkRegisterIID { get; set; }
        public List<MarkGradeDetails> GradeList { get; set; }
        public List<MarkGradeDetails> SkillGradeList { get; set; }
        public List<MarkGradeDetails> SkillGrpGradeList { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Exam")]
        [Select2("Exams", "Numeric", false, "ExamChanges($event, $element, CRUDModel.ViewModel)")]
        [LookUp("LookUps.Exams")]
        public KeyValueViewModel Exam { get; set; }
        public long? ExamID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine01 { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Class", "Numeric", false, "ClassSectionChange($event, $element, CRUDModel.ViewModel)")]
        [LookUp("LookUps.Classes")]
        [CustomDisplay("Class")]
        public KeyValueViewModel StudentClass { get; set; }
        public int? ClassID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Section", "Numeric", false, "ClassSectionChange($event, $element, CRUDModel.ViewModel)")]
        [LookUp("LookUps.Section")]
        [CustomDisplay("Section")]
        public KeyValueViewModel Section { get; set; }
        public int? SectionID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("MarkEntry")]
        public List<MarkRegisterDetailsViewmodel> MarkRegisterDetails { get; set; }



        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as MarkRegisterDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<MarkRegisterViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<MarkRegisterDTO, MarkRegisterViewModel>.CreateMap();            
            Mapper<MarkGradeMapDTO, MarkGradeDetails>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            Mapper<MarkGradeDetails, MarkGradeMapDTO>.CreateMap();
            Mapper<MarkRegisterDetailsDTO, MarkRegisterDetailsViewmodel>.CreateMap();

            var sDto = dto as MarkRegisterDTO;
            var vm = Mapper<MarkRegisterDTO, MarkRegisterViewModel>.Map(dto as MarkRegisterDTO);
            vm.Exam = new KeyValueViewModel()
            {
                Key = sDto.ExamID.ToString(),
                Value = sDto.Exam.Value
            };
            vm.StudentClass = new KeyValueViewModel()
            {
                Key = sDto.ClassID.ToString(),
                Value = sDto.Class.Value
            };
            vm.Section = new KeyValueViewModel()
            {
                Key = sDto.SectionID.ToString(),
                Value = sDto.Section.Value
            };
            vm.MarkRegisterDetails = new List<MarkRegisterDetailsViewmodel>();

            if (sDto.MarkRegistersDetails != null)
            {
                foreach (var stud in sDto.MarkRegistersDetails)
                {
                    if (!string.IsNullOrEmpty(stud.Student.Key))
                    {
                        var markSplit = new List<MarkRegisterDetailsSplitViewModel>();
                       
                       
                        foreach (var markregsplit in stud.MarkRegisterSplitDTO)
                        {
                            if (markregsplit.SubjectID.HasValue && markregsplit.Mark.HasValue)
                            {
                                List<MarkRegSkillGroupSplitViewModel> groupSplit = new List<MarkRegSkillGroupSplitViewModel>();
                                foreach (var skillGroup in markregsplit.MarkRegisterSkillGroupDTO)
                                {
                                    if (!string.IsNullOrEmpty(skillGroup.SkillGroup))
                                    {
                                        List<MarkRegSkillSplitViewModel> skillSplit = new List<MarkRegSkillSplitViewModel>();
                                        foreach (var skills in skillGroup.MarkRegisterSkillsDTO)
                                        {
                                            if (!string.IsNullOrEmpty(skills.Skill))
                                            {
                                                skillSplit.Add(new MarkRegSkillSplitViewModel
                                                {
                                                    Skill = skills.Skill,
                                                    Mark = skills.MarksObtained,
                                                    Grade = skills.Grade ,
                                                    IsAbsent = skills.IsAbsent,
                                                    IsPassed = skills.IsPassed,
                                                    typeId =3,
                                                    SkillMasterID = skills.SkillMasterID,
                                                    MaximumMark = skills.MaximumMark,
                                                    MinimumMark = skills.MinimumMark,
                                                    MarksGradeMapID = skills.MarksGradeMapID,
                                                    SkillGroupMasterID = skills.SkillGroupMasterID,
                                                    MarkRegisterSkillIID = skills.MarkRegisterSkillIID,
                                                    MarkRegisterSkillGroupID =skills.MarkRegisterSkillGroupID,                                                   
                                                });

                                            }
                                        }
                                        groupSplit.Add(new MarkRegSkillGroupSplitViewModel
                                        {
                                            Mark = skillGroup.MarkObtained,
                                            Grade = skillGroup.Grade,
                                            SkillGroup = skillGroup.SkillGroup,
                                            IsAbsent = skillGroup.IsAbsent,
                                            IsPassed = skillGroup.IsPassed,
                                            typeId=2,
                                            MaximumMark = skillGroup.MaximumMark,
                                            MinimumMark = skillGroup.MinimumMark,
                                            SkillGroupID = skillGroup.SkillGroupMasterID,
                                            MarksGradeMapID = skillGroup.MarksGradeMapID,
                                            MarkRegisterSkillGroupIID=skillGroup.MarkRegisterSkillGroupIID,
                                            MarkRegisterSubjectMapID =skillGroup.MarkRegisterSubjectMapID,                                            
                                            MarkRegSkillSplit = skillSplit
                                        });



                                    }
                                }


                                var det = new MarkRegisterDetailsSplitViewModel()
                                {
                                    typeId=1,
                                    Mark = markregsplit.Mark,
                                    Grade = markregsplit.Grade,                                    
                                    Subject = markregsplit.Subject,
                                    IsAbsent = markregsplit.IsAbsent,
                                    IsPassed = markregsplit.IsPassed,
                                    SubjectID = markregsplit.SubjectID,
                                    MaximumMark = markregsplit.MaximumMark,
                                    MinimumMark = markregsplit.MinimumMark,
                                    MarksGradeID = markregsplit.MarksGradeID,
                                    MarkRegisterID = markregsplit.MarkRegisterID,
                                    MarksGradeMapID = markregsplit.MarksGradeMapID,                                    
                                    MarkRegisterSubjectMapIID = markregsplit.MarkRegisterSubjectMapIID,
                                    MarkRegSkillGroupSplit = groupSplit,

                                };
                                markSplit.Add(det);
                            }
                        }
                        vm.MarkRegisterDetails.Add(
                            new MarkRegisterDetailsViewmodel()
                            {
                                MarkRegisterDetailsSplit = markSplit,
                                MarkRegisterID = stud.MarkRegisterID,
                                StudentID = int.Parse(stud.Student.Key),
                                Student = KeyValueViewModel.ToViewModel(stud.Student),
                                MarkRegisterStudentMapIID = stud.MarkRegisterStudentMapIID
                            }
                            );
                    }
                }
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<MarkRegisterViewModel, MarkRegisterDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<MarkRegisterDetailsViewmodel, MarkRegisterDetailsDTO>.CreateMap();
            Mapper<MarkRegisterDetailsSplitViewModel, MarkRegisterDetailsSplitDTO>.CreateMap();
            Mapper<MarkGradeDetails, MarkGradeMapDTO>.CreateMap();
            var dto = Mapper<MarkRegisterViewModel, MarkRegisterDTO>.Map(this);
            dto.ExamID = string.IsNullOrEmpty(this.Exam.Key) ? (long?)null : long.Parse(this.Exam.Key);
            dto.ClassID = string.IsNullOrEmpty(this.StudentClass.Key) ? (int?)null : int.Parse(this.StudentClass.Key);
            dto.SectionID = string.IsNullOrEmpty(this.Section.Key) ? (int?)null : int.Parse(this.Section.Key);

            dto.MarkRegistersDetails = new List<MarkRegisterDetailsDTO>();

            if (this.MarkRegisterDetails != null)
            {
                foreach (var stud in this.MarkRegisterDetails)
                {
                    if (!string.IsNullOrEmpty(stud.Student.Key))
                    {
                        var markSplit = new List<MarkRegisterDetailsSplitDTO>();
                        foreach (var markregslip in stud.MarkRegisterDetailsSplit)
                        {
                            if (markregslip.SubjectID.HasValue && (markregslip.Mark.HasValue && (markregslip.Mark!=0 || (markregslip.Mark == 0 && markregslip.IsAbsent == true))))
                            {
                                List<MarkRegisterSkillGroupDTO> groupSplit = new List<MarkRegisterSkillGroupDTO>();
                                foreach (var skillGrp in markregslip.MarkRegSkillGroupSplit)
                                {
                                    if (!string.IsNullOrEmpty(skillGrp.SkillGroup) && (skillGrp.Mark != 0 || (skillGrp.Mark == 0 && skillGrp.IsAbsent == true)))
                                    {
                                        List<MarkRegisterSkillsDTO> skillSplit = new List<MarkRegisterSkillsDTO>();
                                        foreach (var skills in skillGrp.MarkRegSkillSplit)
                                        {
                                            if ( (skillGrp.SkillGroupID==skills.SkillGroupMasterID)&& !string.IsNullOrEmpty(skills.Skill) && (skills.Mark != 0 || (skills.Mark == 0 && skills.IsAbsent == true)))
                                            {
                                                skillSplit.Add(new MarkRegisterSkillsDTO
                                                {
                                                    Skill = skills.Skill,
                                                    MarksObtained = skills.Mark,
                                                    MarkGradeMap = skills.Grade,
                                                    IsAbsent = skills.IsAbsent,
                                                    IsPassed = skills.IsPassed,
                                                    SkillMasterID = skills.SkillMasterID.Value,
                                                    MaximumMark = skills.MaximumMark,
                                                    MinimumMark = skills.MinimumMark,
                                                    MarksGradeMapID = skills.MarksGradeMapID,
                                                    SkillGroupMasterID = skills.SkillGroupMasterID.Value,
                                                    MarkRegisterSkillIID = skills.MarkRegisterSkillIID,
                                                    MarkRegisterSkillGroupID = skills.MarkRegisterSkillGroupID.Value,
                                                });

                                            }
                                        }

                                        groupSplit.Add(new MarkRegisterSkillGroupDTO
                                        {
                                            MarkObtained = skillGrp.Mark,
                                            MarkGradeMap = skillGrp.Grade,
                                            SkillGroup = skillGrp.SkillGroup,
                                            IsAbsent = skillGrp.IsAbsent,
                                            IsPassed = skillGrp.IsPassed,
                                            MaximumMark = skillGrp.MaximumMark,
                                            MinimumMark = skillGrp.MinimumMark,
                                            SkillGroupMasterID = skillGrp.SkillGroupID,
                                            MarksGradeMapID = skillGrp.MarksGradeMapID,
                                            MarkRegisterSkillGroupIID = skillGrp.MarkRegisterSkillGroupIID.HasValue ? skillGrp.MarkRegisterSkillGroupIID.Value : 0,
                                            MarkRegisterSubjectMapID = skillGrp.MarkRegisterSubjectMapID,
                                            MarkRegisterSkillsDTO = skillSplit
                                        });
                                    }
                                }

                                var det = new MarkRegisterDetailsSplitDTO()
                                {
                                    Mark = markregslip.Mark,
                                    Grade = markregslip.Grade,
                                    IsAbsent = markregslip.IsAbsent,
                                    IsPassed = markregslip.IsPassed,
                                    MarksGradeID = markregslip.MarksGradeID,
                                    MinimumMark = markregslip.MinimumMark,
                                    MaximumMark= markregslip.MaximumMark,                                    
                                    SubjectID = markregslip.SubjectID,
                                    Subject = markregslip.Subject,
                                    MarkGradeDTO = new MarkGradeMapDTO()
                                    {
                                        GradeName = markregslip.Grade,
                                        GradeTo = markregslip.MaximumMark,
                                        GradeFrom = markregslip.MinimumMark,
                                        MarksGradeID = markregslip.MarksGradeID,

                                    },
                                    MarksGradeMapID = markregslip.MarksGradeMapID,
                                    MarkRegisterID = markregslip.MarkRegisterID,
                                    MarkRegisterSubjectMapIID = markregslip.MarkRegisterSubjectMapIID,
                                    MarkRegisterSkillGroupDTO= groupSplit
                                        };
                                markSplit.Add(det);
                            }
                        }
                        dto.MarkRegistersDetails.Add(
                            new MarkRegisterDetailsDTO()
                            {
                                MarkRegisterSplitDTO = markSplit,
                                MarkRegisterID = dto.MarkRegisterIID,
                                StudentID = long.Parse(stud.Student.Key),
                                Student = new KeyValueDTO() { Key = stud.Student.Key, Value = stud.Student.Value },
                                MarkRegisterStudentMapIID = stud.MarkRegisterStudentMapIID
                            }
                            );

                    }
                }
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<MarkRegisterDTO>(jsonString);
        }
    }
}

