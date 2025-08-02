using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;

namespace Eduegate.Web.Library.OnlineExam.OnlineExam
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "Questions", "gridModel.Questions")]
    [DisplayName("Questions")]
    public class AnswerQuestionsViewModel : BaseMasterViewModel
    {
        public AnswerQuestionsViewModel()
        {
            QuestionOptions = new List<OnlineQuestionOptionsViewModel>() { new OnlineQuestionOptionsViewModel() };
        }

        public long OnlineExamQuestionIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "alignleft")]
        [CustomDisplay("Question")]
        public string Question { get; set; }
        public long? QuestionID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.GridGroup, "Options", Attributes4 = "colspan=1")]
        [DisplayName("")]
        public List<OnlineQuestionOptionsViewModel> QuestionOptions { get; set; }

    }
}