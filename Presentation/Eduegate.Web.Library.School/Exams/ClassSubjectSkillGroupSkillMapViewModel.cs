using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.School.Exams
{

    [ContainerType(Framework.Enums.ContainerTypes.Grid, "ClassSubjectSkill", "CRUDModel.ViewModel.SubSkills")]
    [DisplayName("Sub Skills")]
    public class ClassSubjectSkillGroupSkillMapViewModel : BaseMasterViewModel
    {
        public ClassSubjectSkillGroupSkillMapViewModel()
        {
            SubSkill = new KeyValueViewModel();
            MarkGrade = new KeyValueViewModel();
            IsEnableInput = false;
        }
        public long ClassSubjectSkillGroupSkillMapID { get; set; }

        public long ClassSubjectSkillGroupMapID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Skill", "Numeric", false, "SkillGroupChanges($event, $element, gridModel)")]
        [LookUp("LookUps.Skills")]
        [CustomDisplay("SkillGroup")]
        public KeyValueViewModel SkillGroup { get; set; }
        public int? SkillGroupMasterID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("SubSkills", "Numeric", false )]
        [LookUp("LookUps.SubSkills")]
        [CustomDisplay("SubSkills")]
       
        public KeyValueViewModel SubSkill { get; set; }
        public int? SkillMasterID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("MarkGrade", "Numeric", false, "")]
        [LookUp("LookUps.MarkGrade")]
        [CustomDisplay("GradeMap")]
        public KeyValueViewModel MarkGrade { get; set; }
        public int MarkGradeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox,"textleft")]
        [CustomDisplay("IsEnableInput")]
        public bool? IsEnableInput { get; set; }

        [MaxLength(5, ErrorMessage = "Maximum Length should be within 5!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox, Attributes2 = "ng-disabled=!gridModel.IsEnableInput")]
        [CustomDisplay("Minimum")]
        public decimal? MinimumMarks { get; set; }

        [MaxLength(5, ErrorMessage = "Maximum Length should be within 5!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox, Attributes2 = "ng-disabled=!gridModel.IsEnableInput")]
        [CustomDisplay("Maximum")]
        public decimal? MaximumMarks { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.SubSkills[0], CRUDModel.ViewModel.SubSkills)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.SubSkills[0],CRUDModel.ViewModel.SubSkills)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}