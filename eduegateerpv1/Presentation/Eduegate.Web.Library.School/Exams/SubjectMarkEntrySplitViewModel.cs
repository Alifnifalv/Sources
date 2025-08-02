using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.School.Exams
{
  
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "", "gridModel.SubjectMarkSkillGroup")]
    [DisplayName("SubjectMarkSkillGroup")]
    public class SubjectMarkSkillGroupViewModel : BaseMasterViewModel
    {

        public SubjectMarkSkillGroupViewModel()
        {

            SubjectMarkSkill = new List<SubjectMarkSkillViewModel>() { new SubjectMarkSkillViewModel() };
        }
        public long? MarkRegisterSubjectMapID { get; set; }
        public long? MarkRegisterSkillGroupIID { get; set; }
        public long? MarkRegisterID { get; set; }
        public long? MarksGradeMapID { get; set; }
        public int? MarksGradeID { get; set; }

        public int typeId { get; set; }



        public int? SkillGroupID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.ExpandCollapseButton, "textleft", "small-col-width")]
        [DisplayName(" ")]
        public bool IsExpand { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textleft small-col-width")]
        [CustomDisplay("SkillGroup")]
        public string SkillGroup { get; set; }



        [ControlType(Framework.Enums.ControlTypes.Label, "textright")]
        [CustomDisplay("MinimumMarks")]
        public decimal? MinimumMark { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "textright")]//, Attributes2 = "ng-init=skillGroupItem=gridModel"
        [CustomDisplay("MaximumMarks")]
        public decimal? MaximumMark { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "textright", Attributes = "", Attributes2 = "ng-disabled=SkillGroupCanEdit(gridModel)", Attributes3 = "ng-blur=UpdateSubject($parent)")]
        [CustomDisplay("Mark")]
        public decimal? Mark { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "textleft")] //, Attributes = "ng-bind=GetSkillGroupGrade(gridModel,2)"
        [CustomDisplay("Grade")]
        public string Grade { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "textleft", "small-col-width")]
        [CustomDisplay("IsPassed")]
        public bool? IsPassed { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "textleft", "small-col-width", Attributes = "ng-change=UpdateAbsent(gridModel,$parent,null) ng-disabled=AbsentCanEdit($parent)")]
        [CustomDisplay("IsAbsent")]
        public bool? IsAbsent { get; set; }

        [ControlType(Framework.Enums.ControlTypes.GridGroup, "SubjectMarkSkill", Attributes4 = "colspan=10")]
        [DisplayName("")]
        public List<SubjectMarkSkillViewModel> SubjectMarkSkill { get; set; }
    }

    [ContainerType(Framework.Enums.ContainerTypes.Grid, "SubjectMarkSkill", "gridModel.SubjectMarkSkill")]
    [DisplayName("Mark Register Skill  Split")]
    public class SubjectMarkSkillViewModel : BaseMasterViewModel
    {


        public long MarkRegisterSkillIID { get; set; }
        public int? SkillGroupMasterID { get; set; }
        public long? MarkRegisterSkillGroupID { get; set; }
        public long? MarksGradeMapID { get; set; }
        public int? MarksGradeID { get; set; }

        public int? SkillMasterID { get; set; }

        public int typeId { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textleft small-col-width")]
        [CustomDisplay("Skill")]
        public string Skill { get; set; }



        [ControlType(Framework.Enums.ControlTypes.Label, "textright")]
        [CustomDisplay("MinimumMarks")]
        public decimal? MinimumMark { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "textright")]
        [CustomDisplay("MaximumMarks")]
        public decimal? MaximumMark { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "textright", Attributes2 = "ng-disabled=SkillCanEdit(gridModel)", Attributes3 = "ng-blur=UpdateSkillGroup(gridModel,$parent,$parent.$parent)")]//UpdateSkillGroup(gridModel,skillGroupItem,subjectItem)
        [CustomDisplay("Mark")]
        public decimal? Mark { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "textleft")] //, Attributes = "ng-bind=GetSkillGrade(gridModel,3)"
        [CustomDisplay("Grade")]
        public string Grade { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "textleft", "small-col-width")]
        [CustomDisplay("IsPassed")]
        public bool? IsPassed { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "textleft", "small-col-width", Attributes = "ng-change=UpdateAbsent(gridModel,$parent,$parent.$parent) ng-disabled=AbsentCanEdit($parent)")]
        [CustomDisplay("IsAbsent")]
        public bool? IsAbsent { get; set; }

    }
}
