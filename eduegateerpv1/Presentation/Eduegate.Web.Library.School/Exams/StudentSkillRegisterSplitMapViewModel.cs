using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.School.Exams
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "SubSkills", "gridModel.SubSkills")]
    [DisplayName("Sub Skills")]
    public class StudentSkillRegisterSplitMapViewModel : BaseMasterViewModel
    {
        public long StudentSkillRegisterMapIID { get; set; }

        public long StudentSkillRegisterID { get; set; }

        public long? MarksGradeMapID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "textright")]
        [LookUp("LookUps.SubSkills")]
        [DisplayName("Sub Skills")]
        public string SubSkill { get; set; }
        public int? SkillMasterID { get; set; }

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
        [ControlType(Framework.Enums.ControlTypes.TextBox, "textright")]
        [DisplayName("Obtained Mark")]
        public string ObtainedMark { get; set; }
        public decimal? Mark { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "textleft", "small-col-width", Attributes = "ng-bind=GetGrade($index,gridModel)")]
        //[LookUp("LookUps.MarkGrade")]
        [DisplayName("Grade")]
        public string MarkGrade { get; set; }
        public int MarkGradeID { get; set; }
    }
}