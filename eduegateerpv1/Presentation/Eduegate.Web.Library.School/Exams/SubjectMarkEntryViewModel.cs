using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Exams
{
    public class MarkGradeDetails : BaseMasterViewModel
    {
        public long MarksGradeMapIID { get; set; }
        public int? MarksGradeID { get; set; }
        public string GradeName { get; set; }
        public decimal? GradeFrom { get; set; }
        public decimal? GradeTo { get; set; }
        public bool? IsPercentage { get; set; }
        public string Description { get; set; }
    }

    [ContainerType(Framework.Enums.ContainerTypes.Tab, "SubjectMarkEntry", "CRUDModel.ViewModel")]
    [DisplayName("Subject Mark Entry")]
    public class SubjectMarkEntryViewModel : BaseMasterViewModel
    {
        public SubjectMarkEntryViewModel()
        {
            //Exam = new KeyValueViewModel();
            //Class = new KeyValueViewModel();
            //Section = new KeyValueViewModel();
            SubjectMarkEntryDetails = new List<SubjectMarkEntryDetailViewModel>() { new SubjectMarkEntryDetailViewModel() };
            SkillGradeList = new List<MarkGradeDetails>() { new MarkGradeDetails() };
        }
        public List<MarkGradeDetails> GradeList { get; set; }
        public List<MarkGradeDetails> SkillGradeList { get; set; }
        public List<MarkGradeDetails> SkillGrpGradeList { get; set; }
        public long MarkRegisterIID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [CustomDisplay("Exam")]
        [Select2("Exams", "Numeric", false, "ExamChanges($event, $element, CRUDModel.ViewModel)")]
        [LookUp("LookUps.Exams")]
        public KeyValueViewModel Exam { get; set; }
        public long? ExamID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Subject", "Numeric", false, "OnSubjectChange($event, $element, CRUDModel.ViewModel)")]
        [LookUp("LookUps.Subject")]
        [CustomDisplay("Subject")]
        public KeyValueViewModel Subject { get; set; }
        public int SubjectID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine01 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Class", "Numeric", false, "")]
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



        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine02 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("MarkEntry")]
        public List<SubjectMarkEntryDetailViewModel> SubjectMarkEntryDetails { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as SubjectMarkEntryDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<SubjectMarkEntryViewModel>(jsonString);
        }
        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<SubjectMarkEntryDTO, SubjectMarkEntryViewModel>.CreateMap();
            Mapper<SubjectMarkEntryDetailDTO, SubjectMarkEntryDetailViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            Mapper<MarkGradeMapDTO, MarkGradeDetails>.CreateMap();

            var sDto = dto as SubjectMarkEntryDTO;
            var vm = Mapper<SubjectMarkEntryDTO, SubjectMarkEntryViewModel>.Map(dto as SubjectMarkEntryDTO);

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
            vm.Subject = new KeyValueViewModel()
            {
                Key = sDto.SubjectID.ToString(),
                Value = sDto.Subject.Value
            };
            vm.SubjectMarkEntryDetails = new List<SubjectMarkEntryDetailViewModel>();

            if (sDto.SubjectMarkEntryDetails != null)
            {
                var markSplit = new List<SubjectMarkEntryDetailViewModel>();
                foreach (var stud in sDto.SubjectMarkEntryDetails)
                {
                    if (stud.StudentID.HasValue)
                    {
                        List<SubjectMarkSkillGroupViewModel> groupSplit = new List<SubjectMarkSkillGroupViewModel>();
                        if (stud.SubjectID.HasValue && stud.Mark.HasValue)
                        {

                            foreach (var skillGroup in stud.MarkRegisterSkillGroupDTO)
                            {
                                if (!string.IsNullOrEmpty(skillGroup.SkillGroup))
                                {
                                    List<SubjectMarkSkillViewModel> skillSplit = new List<SubjectMarkSkillViewModel>();
                                    foreach (var skills in skillGroup.MarkRegisterSkillsDTO)
                                    {
                                        if (!string.IsNullOrEmpty(skills.Skill))
                                        {
                                            skillSplit.Add(new SubjectMarkSkillViewModel
                                            {
                                                Skill = skills.Skill,
                                                Mark = skills.MarksObtained,
                                                Grade = skills.Grade,
                                                IsAbsent = skills.IsAbsent,
                                                IsPassed = skills.IsPassed,
                                                typeId = 3,
                                                SkillMasterID = skills.SkillMasterID,
                                                MaximumMark = skills.MaximumMark,
                                                MinimumMark = skills.MinimumMark,
                                                MarksGradeID = skills.MarksGradeID,
                                                MarksGradeMapID = skills.MarksGradeMapID,
                                                SkillGroupMasterID = skills.SkillGroupMasterID,
                                                MarkRegisterSkillIID = skills.MarkRegisterSkillIID,
                                                MarkRegisterSkillGroupID = skills.MarkRegisterSkillGroupID,
                                            });

                                        }
                                    }
                                    groupSplit.Add(new SubjectMarkSkillGroupViewModel
                                    {
                                        Mark = skillGroup.MarkObtained,
                                        Grade = skillGroup.Grade,
                                        SkillGroup = skillGroup.SkillGroup,
                                        IsAbsent = skillGroup.IsAbsent,
                                        IsPassed = skillGroup.IsPassed,
                                        typeId = 2,
                                        MaximumMark = skillGroup.MaximumMark,
                                        MinimumMark = skillGroup.MinimumMark,
                                        SkillGroupID = skillGroup.SkillGroupMasterID,
                                        MarksGradeID = skillGroup.MarksGradeID,
                                        MarksGradeMapID = skillGroup.MarksGradeMapID,
                                        MarkRegisterSkillGroupIID = skillGroup.MarkRegisterSkillGroupIID,
                                        MarkRegisterSubjectMapID = skillGroup.MarkRegisterSubjectMapID,
                                        SubjectMarkSkill = skillSplit
                                    });



                                }
                            }


                        }

                        vm.SubjectMarkEntryDetails.Add(
                            new SubjectMarkEntryDetailViewModel()
                            {
                                SubjectMarkSkillGroup = groupSplit,
                                MarkRegisterID = stud.MarkRegisterID,
                                StudentID = stud.StudentID,
                                StudentName = stud.StudentName,
                                Mark = stud.Mark,
                                Grade = stud.Grade,
                                IsAbsent = stud.IsAbsent,
                                IsPassed = stud.IsPassed,
                                SubjectID = stud.SubjectID,
                                MaximumMark = stud.MaximumMark,
                                MinimumMark = stud.MinimumMark,
                                MarksGradeID = stud.MarksGradeID,
                                MarksGradeMapID = stud.MarksGradeMapID,
                                MarkRegisterStudentMapID = stud.MarkRegisterStudentMapID,
                                MarkRegisterSubjectMapIID = stud.MarkRegisterSubjectMapIID,
                            }
                            );
                    }
                }
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<SubjectMarkEntryViewModel, SubjectMarkEntryDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<SubjectMarkEntryDetailViewModel, SubjectMarkEntryDetailDTO>.CreateMap();
            Mapper<MarkRegisterDetailsSplitViewModel, MarkRegisterDetailsSplitDTO>.CreateMap();
            Mapper<MarkGradeDetails, MarkGradeMapDTO>.CreateMap();
            var dto = Mapper<SubjectMarkEntryViewModel, SubjectMarkEntryDTO>.Map(this);
            dto.ExamID = string.IsNullOrEmpty(this.Exam.Key) ? (long?)null : long.Parse(this.Exam.Key);
            dto.ClassID = string.IsNullOrEmpty(this.StudentClass.Key) ? (int?)null : int.Parse(this.StudentClass.Key);
            dto.SectionID = string.IsNullOrEmpty(this.Section.Key) ? (int?)null : int.Parse(this.Section.Key);
            dto.SubjectID = string.IsNullOrEmpty(this.Subject.Key) ? (int?)null : int.Parse(this.Subject.Key);
            dto.SubjectMarkEntryDetails = new List<SubjectMarkEntryDetailDTO>();
            var markSplit = new List<SubjectMarkEntryDetailDTO>();
            if (this.SubjectMarkEntryDetails != null)
            {

                foreach (var stud in this.SubjectMarkEntryDetails)
                {
                    if (stud.StudentID.HasValue)
                    {
                        if ((stud.Mark.HasValue && (stud.Mark != 0 || (stud.Mark == 0 && stud.IsAbsent == true))))
                        {
                            List<MarkRegisterSkillGroupDTO> groupSplit = new List<MarkRegisterSkillGroupDTO>();
                            foreach (var skillGrp in stud.SubjectMarkSkillGroup)
                            {
                                if (!string.IsNullOrEmpty(skillGrp.SkillGroup) && (skillGrp.Mark != 0 || (skillGrp.Mark == 0 && skillGrp.IsAbsent == true)))
                                {
                                    var skillSplit = new List<MarkRegisterSkillsDTO>();
                                    foreach (var skills in skillGrp.SubjectMarkSkill)
                                    {
                                        if ((skillGrp.SkillGroupID == skills.SkillGroupMasterID) && !string.IsNullOrEmpty(skills.Skill) && (skills.Mark != 0 || (skills.Mark == 0 && skills.IsAbsent == true)))
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

                            var det = new SubjectMarkEntryDetailDTO()
                            {
                                Mark = stud.Mark,
                                Grade = stud.Grade,
                                IsAbsent = stud.IsAbsent,
                                IsPassed = stud.IsPassed,
                                MarksGradeID = stud.MarksGradeID,
                                MinimumMark = stud.MinimumMark,
                                MaximumMark = stud.MaximumMark,
                                SubjectID = dto.SubjectID,

                                MarkGradeDTO = new MarkGradeMapDTO()
                                {
                                    GradeName = stud.Grade,
                                    GradeTo = stud.MaximumMark,
                                    GradeFrom = stud.MinimumMark,
                                    MarksGradeID = stud.MarksGradeID,

                                },
                                MarksGradeMapID = stud.MarksGradeMapID,
                                MarkRegisterID = stud.MarkRegisterID,
                                MarkRegisterSubjectMapIID = stud.MarkRegisterSubjectMapIID,
                                StudentID = stud.StudentID,
                                StudentName = stud.StudentName,
                                MarkRegisterSkillGroupDTO = groupSplit,
                                MarkRegisterStudentMapID = stud.MarkRegisterStudentMapID
                            };
                            markSplit.Add(det);
                        }



                    }
                }
            }
            dto.SubjectMarkEntryDetails = markSplit;
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<SubjectMarkEntryDTO>(jsonString);
        }
    }
}

