using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Eduegate.Web.Library.School.Exams
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "StudentSkill", "CRUDModel.ViewModel")]
    [DisplayName("Student Skill")]
    public class StudentSkillRegisterViewModel : BaseMasterViewModel
    {
        public StudentSkillRegisterViewModel()
        {
            //Class = new KeyValueViewModel();
            //Student = new KeyValueViewModel();
            //Exam = new KeyValueViewModel();
            //Subject = new KeyValueViewModel();
            Skills = new List<StudentSkillRegisterMapViewModel>() { new StudentSkillRegisterMapViewModel() };
            IsAbsent = false;
            IsPassed = false;
        }

        public long StudentSkillRegisterIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Class", "Numeric", false,"ClassChanges($event, $element, CRUDModel.ViewModel)")]
        [LookUp("LookUps.Classes")]
        [DisplayName("Class")]
        public KeyValueViewModel StudentClass { get; set; }
        public int? ClassID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Student", "Numeric", false, "")]
        [LookUp("LookUps.Student")]
        //[LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Student", "LookUps.Student")]
        [DisplayName("Student Name")]
        public KeyValueViewModel Student { get; set; }
        public long? StudentId { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("Exam", "Numeric", false, "")]
        //[LookUp("LookUps.Exams")]
        //[DisplayName("Exam")]
        //public KeyValueViewModel Exam { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Exam", "Numeric", false, "")]
        [LookUp("LookUps.Exams")]
        [DisplayName("Exam")]
        public KeyValueViewModel Exam { get; set; }
        public long? ExamID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Subject", "Numeric", false, "")]
        [LookUp("LookUps.Subject")]
        [DisplayName("Subject")]
        public KeyValueViewModel Subject { get; set; }
        public int? SubjectID { get; set; }



        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [DisplayName("Skills")]
        public List<StudentSkillRegisterMapViewModel> Skills { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [DisplayName("Is Passed")]
        public bool? IsPassed { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [DisplayName("Is Absent")]
        public bool? IsAbsent { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(5, ErrorMessage = "Maximum Length should be within 5!")]
        [DisplayName("Total Mark")]
        public decimal? TotalMarks { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.MarkGrade")]
        [DisplayName("Grade Map")]
        public string MarkGrade { get; set; }
        public int MarkGradeID { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as StudentSkillRegisterDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<StudentSkillRegisterViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<StudentSkillRegisterDTO, StudentSkillRegisterViewModel>.CreateMap();
            Mapper<StudentSkillRegisterMapDTO, StudentSkillRegisterMapViewModel>.CreateMap();
            Mapper<StudentSkillRegisterSplitDTO, StudentSkillRegisterSplitMapViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var skillDto = dto as StudentSkillRegisterDTO;
            var vm = Mapper<StudentSkillRegisterDTO, StudentSkillRegisterViewModel>.Map(skillDto);

            vm.StudentSkillRegisterIID = skillDto.StudentSkillRegisterIID;
            vm.ExamID = skillDto.ExamID;
            vm.ClassID = skillDto.ClassID;
            vm.SubjectID = skillDto.SubjectID;
            vm.StudentId = skillDto.StudentId;
            vm.StudentClass = new KeyValueViewModel() { Key = skillDto.ClassID.ToString(), Value = skillDto.Class.Value };
            vm.Exam = new KeyValueViewModel() { Key = skillDto.ExamID.ToString(), Value = skillDto.Exam.Value };
            vm.Subject = new KeyValueViewModel() { Key = skillDto.ExamID.ToString(), Value = skillDto.Subject.Value };
            vm.Student = new KeyValueViewModel() { Key = skillDto.StudentId.ToString(), Value = skillDto.Student.Value };

            vm.Skills = new List<StudentSkillRegisterMapViewModel>();

            if (skillDto.StudentSkillRegisterMap != null)
            {
                foreach (var skl in skillDto.StudentSkillRegisterMap)
                {
                    if (!string.IsNullOrEmpty(skl.Skill.Key))
                    {
                        var skillSplit = new List<StudentSkillRegisterSplitMapViewModel>();
                        foreach (var skilregsplit in skl.StudentSkillRegisterSkillMapDTO)
                        {
                            if (skilregsplit.SkillMasterID != 0 && skilregsplit.Mark.HasValue)
                            {
                                var det = new StudentSkillRegisterSplitMapViewModel()
                                {
                                    StudentSkillRegisterMapIID = skilregsplit.StudentSkillRegisterMapIID,
                                    Mark = skilregsplit.Mark,
                                    MaximumMarks = skilregsplit.MaximumMarks,
                                    MinimumMarks = skilregsplit.MinimumMarks,
                                    MarkGradeID = skilregsplit.MarkGradeID,
                                    MarkGrade = skilregsplit.MarkGradeID.ToString(),
                                    SkillMasterID = skilregsplit.SkillMasterID,
                                    MarksGradeMapID = skilregsplit.MarksGradeMapID,
                                };
                                skillSplit.Add(det);
                            }
                        }
                        vm.Skills.Add(
                            new StudentSkillRegisterMapViewModel()
                            {
                                StudentSkillRegisterID = skl.StudentSkillRegisterID,
                                SubSkills = skillSplit,
                                SkillGroupMasterID = skl.SkillGroupMasterID,
                                Skill = new KeyValueViewModel() { Key = skl.SkillGroupMasterID.ToString(), Value = skl.Skill.Value },
                            }
                            );
                    }
                }
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<StudentSkillRegisterViewModel, StudentSkillRegisterDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<StudentSkillRegisterMapViewModel, StudentSkillRegisterMapDTO>.CreateMap();
            Mapper<StudentSkillRegisterSplitMapViewModel, StudentSkillRegisterSplitDTO>.CreateMap();
            var dto = Mapper<StudentSkillRegisterViewModel, StudentSkillRegisterDTO>.Map(this);

            dto.StudentSkillRegisterIID = this.StudentSkillRegisterIID;
            dto.ExamID = string.IsNullOrEmpty(this.Exam.Key) ? 0 : int.Parse(this.Exam.Key);
            dto.ClassID = string.IsNullOrEmpty(this.StudentClass.Key) ? 0 : int.Parse(this.StudentClass.Key);
            dto.SubjectID = this.SubjectID;
            dto.Mark = this.TotalMarks;
            dto.StudentId = string.IsNullOrEmpty(this.Student.Key) ? 0 : int.Parse(this.Student.Key);

            dto.StudentSkillRegisterMap = new List<StudentSkillRegisterMapDTO>();

            if (this.Skills != null)
            {
                foreach (var skill in this.Skills)
                {
                    if (!string.IsNullOrEmpty(skill.SkillGroupMasterID.ToString()))
                    {
                        var skillSplit = new List<StudentSkillRegisterSplitDTO>();
                        foreach (var studskill in skill.SubSkills)
                        {
                            if (studskill.SkillMasterID != 0 && studskill.ObtainedMark != null)
                            {
                                var skl = new StudentSkillRegisterSplitDTO()
                                {
                                    Mark = decimal.Parse(studskill.ObtainedMark),
                                    MarkGradeID = studskill.MarkGradeID,
                                    MaximumMarks = studskill.MaximumMarks,
                                    MinimumMarks = studskill.MinimumMarks,
                                    SkillMasterID = studskill.SkillMasterID,
                                    MarkGradeDTO = new MarkGradeMapDTO()
                                    {
                                        GradeName = studskill.MarkGrade,
                                        GradeTo = studskill.MaximumMarks,
                                        GradeFrom = studskill.MinimumMarks,
                                        MarksGradeID = studskill.MarkGradeID,
                                    },
                                };
                                skillSplit.Add(skl);
                            }
                        }
                        dto.StudentSkillRegisterMap.Add(
                            new StudentSkillRegisterMapDTO()
                            {
                                StudentSkillRegisterSkillMapDTO = skillSplit,
                                StudentSkillRegisterID = skill.StudentSkillRegisterID,
                                SkillGroupMasterID = string.IsNullOrEmpty(skill.Skill.Key) ? 0 : int.Parse(skill.Skill.Key),
                            }
                        );
                    }
                }
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<StudentSkillRegisterDTO>(jsonString);
        }
    }
}
