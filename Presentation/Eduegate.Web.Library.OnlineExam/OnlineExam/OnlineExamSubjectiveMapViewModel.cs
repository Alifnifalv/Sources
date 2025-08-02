using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.OnlineExam.OnlineExam
{
    [ContainerType(Framework.Enums.ContainerTypes.FieldSet, "SubjectiveQuestionMaps", "CRUDModel.ViewModel.SubjectiveQuestionMaps")]
    [DisplayName("Exam Question Groups")]
    public class OnlineExamSubjectiveMapViewModel : BaseMasterViewModel
    {
        public OnlineExamSubjectiveMapViewModel()
        {
            //QuestionGroup = new KeyValueViewModel();
            SubjectiveQuestionMaps = new List<OnlineExamSubjectiveMarksMapViewModel>() { new OnlineExamSubjectiveMarksMapViewModel() };

        }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("SubjectiveQuestionMarks")]
        public decimal? SubjectiveQuestionMarks { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid, "fullwidth", Attributes4 = "colspan=8")]
        [DisplayName("")]
        public List<OnlineExamSubjectiveMarksMapViewModel> SubjectiveQuestionMaps { get; set; }

    }
}