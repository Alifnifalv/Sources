using Newtonsoft.Json;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Exams
{
    //[ContainerType(Framework.Enums.ContainerTypes.Tab, "ClassSubjectSkill", "CRUDModel.ViewModel")]
    //[DisplayName("Class Subject Skill")]
    public class ClassSubjectSkillGroupMapViewModel : BaseMasterViewModel
    {
        public ClassSubjectSkillGroupMapViewModel()
        {
            //Class = new KeyValueViewModel();
            //Subject= new KeyValueViewModel();
            //Exam = new KeyValueViewModel();
            //Skill= new KeyValueViewModel();
            SubSkills = new List<ClassSubjectSkillGroupSkillMapViewModel>() { new ClassSubjectSkillGroupSkillMapViewModel() };
        }

        public long ClassSubjectSkillGroupMapID { get; set; }

        [Required]
        [MaxLength(250, ErrorMessage = "Maximum Length should be within 250!")]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("SkillSetName")]
        public string Description { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("Class", "Numeric", false, "SearchStudent($event, $element)")]
        //[LookUp("LookUps.Classes")]
        //[DisplayName("Class")]
        //public KeyValueViewModel StudentClass { get; set; }
        //public int ClassID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "textleft")]
        [CustomDisplay("IsScholastic")]
        public bool? ISScholastic { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Subject", "Numeric", true)]
        [LookUp("LookUps.Subject")]
        [CustomDisplay("Subject")]
        public List<KeyValueViewModel> Subject { get; set; }
        public int? SubjectID { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("Exam", "Numeric", false, "ExamChanges($event, $element, CRUDModel.ViewModel)")]
        //[LookUp("LookUps.Exams")]
        //[DisplayName("Exam")]
        //public KeyValueViewModel Exam { get; set; }
        //public long? ExamID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        //[DisplayName("")]
        //public string NewLine1 { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("Minimum Mark")]
        //public decimal? ExamMinimumMark { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("Maximum Mark")]
        //public decimal? ExamMaximumMark { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        //[DisplayName("")]
        //public string NewLine2 { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("Skill", "Numeric", false)]
        //[LookUp("LookUps.Skills")]
        //[DisplayName("Skill Group")]
        //public KeyValueViewModel Skill { get; set; }
        //public int SkillGroupMasterID { get; set; }


        //[MaxLength(5, ErrorMessage = "Maximum Length should be within 5!")]
        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Skill Group Min Marks")]
        //public decimal? MinimumMarks { get; set; }


        //[MaxLength(5, ErrorMessage = "Maximum Length should be within 5!")]
        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Skill Group Max Marks")]
        //public decimal? MaximumMarks { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("SubSkills")]
        public List<ClassSubjectSkillGroupSkillMapViewModel> SubSkills { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, Attributes = "ng-bind=GetTotalMarks(CRUDModel.ViewModel.SubSkills) | number")]
        [CustomDisplay("TotalMarks")]
        public decimal? TotalMarks { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine5 { get; set; }

       
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("MarkGrade", "Numeric", false)]
        [LookUp("LookUps.MarkGrade")]
        [CustomDisplay("MarkGrade")]
        public KeyValueViewModel MarkGrade { get; set; }
        public int? MarkGradeID { get; set; }


        [Required]
        [MaxLength(250, ErrorMessage = "Maximum Length should be within 250!")]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("ProgressCardHeader")]
        public string ProgressCardHeader { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("ConversionFactor")]
        public decimal? ConversionFactor { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as ClassSubjectSkillGroupMapDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<ClassSubjectSkillGroupMapViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<ClassSubjectSkillGroupMapDTO, ClassSubjectSkillGroupMapViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            Mapper<ClassSubjectSkillGroupSkillMapDTO, ClassSubjectSkillGroupSkillMapViewModel>.CreateMap();
            var skilldto = dto as ClassSubjectSkillGroupMapDTO;
            var vm = Mapper<ClassSubjectSkillGroupMapDTO, ClassSubjectSkillGroupMapViewModel>.Map(skilldto);

            vm.ClassSubjectSkillGroupMapID = skilldto.ClassSubjectSkillGroupMapID;
            vm.MarkGrade = skilldto.MarkGrade != null ? KeyValueViewModel.ToViewModel(skilldto.MarkGrade) : null;

            vm.Subject = new List<KeyValueViewModel>();
            if (skilldto.Subjects.Count > 0)
            {
                foreach (KeyValueDTO kvm in skilldto.Subjects)
                {
                    vm.Subject.Add(new KeyValueViewModel()
                    { Key = kvm.Key, Value = kvm.Value });
                }
            }
            vm.TotalMarks = skilldto.SubSkills.Select(x => x.MaximumMarks).Sum();

            //vm.ExamMinimumMark = skilldto.ExamMinimumMark;
            //vm.ExamMaximumMark = skilldto.ExamMaximumMark;
            vm.ISScholastic = skilldto.ISScholastic;
            vm.Description = skilldto.Description;
            vm.ProgressCardHeader = skilldto.ProgressCardHeader;
            vm.ConversionFactor = skilldto.ConversionFactor;
            vm.SubSkills = new List<ClassSubjectSkillGroupSkillMapViewModel>();
            foreach (var subSkill in skilldto.SubSkills)
            {
                if (subSkill.SkillGroupMasterID != 0)
                {
                    vm.SubSkills.Add(new ClassSubjectSkillGroupSkillMapViewModel()
                    {
                        ClassSubjectSkillGroupSkillMapID = subSkill.ClassSubjectSkillGroupSkillMapID,
                        ClassSubjectSkillGroupMapID = subSkill.ClassSubjectSkillGroupMapID,
                        SubSkill = KeyValueViewModel.ToViewModel(subSkill.SkillMaster),
                        SkillMasterID = subSkill.SkillMasterID,
                        MarkGradeID = subSkill.MarkGradeID,
                        MarkGrade = KeyValueViewModel.ToViewModel(subSkill.MarkGrade),
                        MinimumMarks = subSkill.MinimumMarks,
                        MaximumMarks = subSkill.MaximumMarks,
                        IsEnableInput = subSkill.IsEnableInput,
                        //SkillGroup = subSkill.SkillGroup != null || subSkill.SkillGroup.Key != null ? new KeyValueViewModel() { Key = subSkill.SkillGroup.Key, Value = subSkill.SkillGroup.Value} : new KeyValueViewModel(),
                        SkillGroup = subSkill.SkillGroupMasterID.HasValue ? new KeyValueViewModel() { Key = subSkill.SkillGroup.Key, Value = subSkill.SkillGroup.Value } : new KeyValueViewModel(),
                    });
                }
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<ClassSubjectSkillGroupMapViewModel, ClassSubjectSkillGroupMapDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<ClassSubjectSkillGroupSkillMapViewModel, ClassSubjectSkillGroupSkillMapDTO>.CreateMap();
            var dto = Mapper<ClassSubjectSkillGroupMapViewModel, ClassSubjectSkillGroupMapDTO>.Map(this);

            dto.ClassSubjectSkillGroupMapID = this.ClassSubjectSkillGroupMapID;

            if (this.MarkGrade != null)
            { 
            dto.MarkGradeID = string.IsNullOrEmpty(this.MarkGrade.Key) ? (int?)null : int.Parse(this.MarkGrade.Key);
            }
            this.TotalMarks = this.SubSkills.Select(x => x.MaximumMarks).Sum();
            dto.TotalMarks = this.TotalMarks;

            dto.ISScholastic = this.ISScholastic;
            dto.Description = this.Description;
            dto.ProgressCardHeader = this.ProgressCardHeader;
            dto.ConversionFactor = this.ConversionFactor;

            if (this.Subject != null && this.Subject.Count > 0)
            {
                dto.Subjects = new List<KeyValueDTO>();
                foreach (KeyValueViewModel kvm in this.Subject)
                {
                    dto.Subjects.Add(new KeyValueDTO()
                    { Key = kvm.Key, Value = kvm.Value });
                }
            }

            dto.SubSkills = new List<ClassSubjectSkillGroupSkillMapDTO>();
            foreach (var subSkill in this.SubSkills)
            {
                if (subSkill.SkillGroup != null)
                {
                    dto.SubSkills.Add(new ClassSubjectSkillGroupSkillMapDTO()
                    {
                        ClassSubjectSkillGroupSkillMapID = subSkill.ClassSubjectSkillGroupSkillMapID,
                        ClassSubjectSkillGroupMapID = this.ClassSubjectSkillGroupMapID,
                        SkillMasterID = subSkill.SubSkill == null || string.IsNullOrEmpty(subSkill.SubSkill.Key) ? null : int.Parse(subSkill.SubSkill.Key),
                        MarkGradeID = subSkill.MarkGrade == null || string.IsNullOrEmpty(subSkill.MarkGrade.Key) ? 0 : int.Parse(subSkill.MarkGrade.Key),
                        MinimumMarks = subSkill.MinimumMarks,
                        MaximumMarks = subSkill.MaximumMarks,
                        IsEnableInput = subSkill.IsEnableInput,
                        SkillGroupMasterID = string.IsNullOrEmpty(subSkill.SkillGroup.Key) ? 0 : int.Parse(subSkill.SkillGroup.Key),
                    });
                }
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<ClassSubjectSkillGroupMapDTO>(jsonString);
        }
    }
}