using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.OnlineExam.OnlineExam
{
    [ContainerType(Framework.Enums.ContainerTypes.FieldSet, "ObjectiveQuestionMaps", "CRUDModel.ViewModel.ObjectiveQuestionMaps")]
    [DisplayName("Exam Question Groups")]
    public class OnlineExamObjectiveMapViewModel : BaseMasterViewModel
    {
        public OnlineExamObjectiveMapViewModel()
        {
            //QuestionGroup = new KeyValueViewModel();
            ObjectiveQuestionMaps = new List<OnlineExamObjectiveMarksMapViewModel>() { new OnlineExamObjectiveMarksMapViewModel() };

        }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("SubjectiveQuestionMarks")]
        public decimal? SubjectiveQuestionMarks { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid, "fullwidth alignleft")]
        [DisplayName("")]
        public List<OnlineExamObjectiveMarksMapViewModel> ObjectiveQuestionMaps { get; set; }

    }
}