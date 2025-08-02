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
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "MarkRegisterDetailsSplit", "gridModel.MarkRegisterDetailsSplit")]
    [DisplayName("Mark Register Details Split")]
    public class MarkRegisterDetailsSplitViewModel : BaseMasterViewModel
    {

        public MarkRegisterDetailsSplitViewModel()
        {

            MarkRegSkillGroupSplit = new List<MarkRegSkillGroupSplitViewModel>() { new MarkRegSkillGroupSplitViewModel() };
        }

        public long MarkRegisterSubjectMapIID { get; set; }
        public long? MarkRegisterStudentMapID { get; set; }
        public long? MarkRegisterID { get; set; }
        public long? MarksGradeMapID { get; set; }
        public int? MarksGradeID { get; set; }
        public int typeId { get; set; }
        [ControlType(Framework.Enums.ControlTypes.Hidden)]
        //[DisplayName("SubjectID")]
        public int? SubjectID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.ExpandCollapseButton, "textleft", "small-col-width")]
        [DisplayName(" ")]
        public bool IsExpand { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textleft small-col-width")]
        [CustomDisplay("Subject")]
        public string Subject { get; set; }


        
        [ControlType(Framework.Enums.ControlTypes.Label, "textright")]
        [CustomDisplay("MinimumMarks")]
        public decimal? MinimumMark { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "textright")]
        [CustomDisplay("MaximumMarks")]
        public decimal? MaximumMark { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "textright",Attributes2 ="ng-disabled=SubjectCanEdit(gridModel)", Attributes3 = "ng-blur=UpdateFromSubject(gridModel)")]
        [CustomDisplay("Mark")]
        public decimal? Mark { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "textleft")]
        [CustomDisplay("Grade")]
        public string Grade { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "textleft", "small-col-width")]
        [CustomDisplay("IsPassed")]
        public bool? IsPassed { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "textleft", "small-col-width", Attributes = "ng-change=UpdateAbsent(gridModel,null,null)")]
        [CustomDisplay("IsAbsent")]
        public bool? IsAbsent { get; set; }

        [ControlType(Framework.Enums.ControlTypes.GridGroup, "MarkRegSkillGroupSplit", Attributes4 = "colspan=8")]
        [DisplayName("")]
        public List<MarkRegSkillGroupSplitViewModel> MarkRegSkillGroupSplit { get; set; }
    }

    [ContainerType(Framework.Enums.ContainerTypes.Grid, "MarkRegSkillGroupSplit", "gridModel.MarkRegSkillGroupSplit")]
    [DisplayName("Mark Register Skill Group Split")]
    public class MarkRegSkillGroupSplitViewModel : BaseMasterViewModel
    {

        public MarkRegSkillGroupSplitViewModel()
        {

            MarkRegSkillSplit = new List<MarkRegSkillSplitViewModel>() { new MarkRegSkillSplitViewModel() };
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
        [CustomDisplay("Skill Group")]
        public string SkillGroup { get; set; }



        [ControlType(Framework.Enums.ControlTypes.Label, "textright")]
        [CustomDisplay("MinimumMarks")]
        public decimal? MinimumMark { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "textright")]
        [CustomDisplay("MaximumMarks")]
        public decimal? MaximumMark { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "textright", Attributes = "", Attributes2 = "ng-blur=UpdateSubject(gridModel,$parent)", Attributes3 = "ng-disabled=SkillGroupCanEdit(gridModel)")]
        [CustomDisplay("Mark")]
        public decimal? Mark { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "textleft")]
        [CustomDisplay("Grade")]
        public string Grade { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "textleft", "small-col-width")]
        [CustomDisplay("IsPassed")]
        public bool? IsPassed { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "textleft", "small-col-width", Attributes = "ng-change=UpdateAbsent(gridModel,$parent,null) ng-disabled=AbsentCanEdit($parent)")]
        [CustomDisplay("IsAbsent")]
        public bool? IsAbsent { get; set; }

        [ControlType(Framework.Enums.ControlTypes.GridGroup, "MarkRegSkillSplit", Attributes4 = "colspan=8")]
        [DisplayName("")]
        public List<MarkRegSkillSplitViewModel> MarkRegSkillSplit { get; set; }
    }

    [ContainerType(Framework.Enums.ContainerTypes.Grid, "MarkRegSkillSplit", "gridModel.MarkRegSkillSplit")]
    [DisplayName("Mark Register Skill  Split")]
    public class MarkRegSkillSplitViewModel : BaseMasterViewModel
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
        [ControlType(Framework.Enums.ControlTypes.Label, "textleft")]
        [CustomDisplay("Grade")]
        public string Grade { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "textleft", "small-col-width", Attributes = "ng-change=UpdateSkillAbsent(gridModel)")]
        [CustomDisplay("IsPassed")]
        public bool? IsPassed { get; set; }


        [ControlType(Framework.Enums.ControlTypes.CheckBox, "textleft", "small-col-width", Attributes = "ng-change=UpdateAbsent(gridModel,$parent,$parent.$parent) ng-disabled=AbsentCanEdit($parent)")]
        [CustomDisplay("IsAbsent")]
        public bool? IsAbsent { get; set; }

    }
}
