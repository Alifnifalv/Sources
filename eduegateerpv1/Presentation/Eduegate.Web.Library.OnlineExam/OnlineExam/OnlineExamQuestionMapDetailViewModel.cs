using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;

namespace Eduegate.Web.Library.OnlineExam.OnlineExam
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "OnlineExamQuestionMapDetail", "CRUDModel.ViewModel.OnlineExamQuestionMapDetail")]
    [DisplayName("Questions")]
    public class OnlineExamQuestionMapDetailViewModel : BaseMasterViewModel
    {
        public OnlineExamQuestionMapDetailViewModel()
        {

        }

        public long OnlineExamQuestionIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "alignleft")]
        [CustomDisplay("Question")]
        public string Question { get; set; }
        public long? QuestionID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Answer Type")]
        public string AnswerType { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("No.Of Options")]
        public long? QuestionOptionCount { get; set; }

        public string QuestionGroup { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Points")]
        public decimal? Points { get; set; }

    }
}