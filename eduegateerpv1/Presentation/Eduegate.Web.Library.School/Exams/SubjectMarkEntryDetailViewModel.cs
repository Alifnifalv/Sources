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
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "SubjectMarkEntryDetails", "CRUDModel.ViewModel.SubjectMarkEntryDetails")]
    [DisplayName("Subject Mark Entry Details")]
    public class SubjectMarkEntryDetailViewModel : BaseMasterViewModel
    {
        public SubjectMarkEntryDetailViewModel()
        {
            //Student = new KeyValueViewModel();
            //SubjectMarkEntrySplit = new List<SubjectMarkEntrySplitViewModel>() { new SubjectMarkEntrySplitViewModel() };
            SubjectMarkSkillGroup = new List<SubjectMarkSkillGroupViewModel>() { new SubjectMarkSkillGroupViewModel() };
        }

        public long MarkRegisterSubjectMapIID { get; set; }
        public long? MarkRegisterStudentMapID { get; set; }
        public long? MarkRegisterID { get; set; }
        public long? MarksGradeMapID { get; set; }
        public int? MarksGradeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.ExpandCollapseButton, "medium - col - width textleft")]
        [DisplayName(" ")]
        public bool IsExpand { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[DisplayName("Student")]
        //[Select2("Students", "Numeric", false, "OnStudentChange(gridModel, $select,$index)", false, "ng-click=LoadStudent($index)")]
        //[LookUp("LookUps.Students")]
        ////[LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=Student", "LookUps.Student")]
        //public KeyValueViewModel Student { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label, "textleft large-col-width")]
        [CustomDisplay("StudentName")]
        public string StudentName { get; set; }
        public long? StudentID { get; set; }

       
        public int typeId { get; set; }
        [ControlType(Framework.Enums.ControlTypes.Hidden)]
        //[DisplayName("SubjectID")]
        public int? SubjectID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "textright")]
        [CustomDisplay("MinimumMarks")]
        public decimal? MinimumMark { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "textright")]
        [CustomDisplay("MaximumMarks")]
        public decimal? MaximumMark { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "textright", Attributes2 = "ng-disabled=SubjectCanEdit(gridModel)")]
        [CustomDisplay("Mark")]
        public decimal? Mark { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "textleft")]//, Attributes = "ng-bind=GetGrade(gridModel,1)"
        [CustomDisplay("Grade")]
        public string Grade { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "textleft", "small-col-width")]//{{gridModel.MinimumMark < gridModel.Mark}}
        [CustomDisplay("IsPassed")]
        public bool? IsPassed { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "textleft", "small-col-width", Attributes = "ng-change=UpdateAbsent(gridModel,null,null)")]
        [CustomDisplay("IsAbsent")]
        public bool? IsAbsent { get; set; }

        [ControlType(Framework.Enums.ControlTypes.GridGroup, "SubjectMarkSkillGroup", Attributes4 = "colspan=10")]
        [DisplayName("")]
        public List<SubjectMarkSkillGroupViewModel> SubjectMarkSkillGroup { get; set; }
        
    }
}
