using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;

namespace Eduegate.Web.Library.OnlineExam.OnlineExam
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "QuestionOptions", "gridModel.QuestionOptions")]
    [DisplayName("QuestionOptions")]
    public class OnlineQuestionOptionsViewModel : BaseMasterViewModel
    {
        public OnlineQuestionOptionsViewModel()
        {
        }

        public long QuestionOptionMapIID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Option")]
        public string OptionText { get; set; }

        public long? QuestionID { get; set; }

        public string ImageName { get; set; }

        public long? ContentID { get; set; }

        public bool? IsSelected { get; set; }

        public long? OnlineExamIID { get; set; }

        public long? QuestionOptionCount { get; set; }

    }
}