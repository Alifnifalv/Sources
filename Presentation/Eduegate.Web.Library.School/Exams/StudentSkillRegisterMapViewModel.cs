using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Eduegate.Web.Library.School.Exams
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "Skills", "CRUDModel.ViewModel.Skills")]
    [DisplayName("")]
    public class StudentSkillRegisterMapViewModel : BaseMasterViewModel
    {
        public StudentSkillRegisterMapViewModel()
        {
            SubSkills = new List<StudentSkillRegisterSplitMapViewModel>() { new StudentSkillRegisterSplitMapViewModel() };
            //Skill = new KeyValueViewModel();
        }

        public long StudentSkillRegisterID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.ExpandCollapseButton, "textleft", "small-col-width")]
        [DisplayName(" ")]
        public bool IsExpand { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Skills", "Numeric", false, "OnSkillSelect(gridModel, $select,$index)")]
        [LookUp("LookUps.Skills")]
        [DisplayName("Skill")]
        public KeyValueViewModel Skill { get; set; }
        public int SkillGroupMasterID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "textright")]
        [DisplayName("Minimum Mark")]
        public decimal? MinimumMarks { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "textright")]
        [DisplayName("Maximum Mark")]
        public decimal? MaximumMarks { get; set; }

        [Required]
        [MaxLength(5, ErrorMessage = "Maximum Length should be within 5!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "textright", Attributes2 = "ng-disabled=!gridModel.Skill.Key")]
        [DisplayName("Obtained Mark")]
        public string ObtainedMark { get; set; }
        public decimal? Mark { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.Skills[0], CRUDModel.ViewModel.Skills)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.Skills[0],CRUDModel.ViewModel.Skills)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

        [ControlType(Framework.Enums.ControlTypes.GridGroup, "SubSkills")]
        [DisplayName("")]
        public List<StudentSkillRegisterSplitMapViewModel> SubSkills { get; set; }

    }
}