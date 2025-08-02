using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;

namespace Eduegate.Web.Library.OnlineExam.OnlineExam
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "QuestionMapResults", "CRUDModel.ViewModel.QuestionMapResults")]
    [DisplayName("Question map")]
    public class OnlineExamResultQuestionViewModel : BaseMasterViewModel
    {
        public OnlineExamResultQuestionViewModel()
        {
            QuestionOptions = new List<OnlineQuestionOptionsViewModel>() { new OnlineQuestionOptionsViewModel() };
            IsExpand = false;
        }
        public long OnlineExamResultQuestionMapIID { get; set; }
        
        public long? OnlineExamResultID { get; set; }

        public string Question { get; set; }
        public long? QuestionID { get; set; }

        public decimal? Marks { get; set; }

        public string Remarks { get; set; }

        public string EntryType { get; set; }

        public decimal? OldMarks { get; set; }

        public string OldRemarks { get; set; }

        public string SelectedOption { get; set; }
        public long? SelectedOptionID { get; set; }

        public bool IsExpand { get; set; }

        public decimal? TotalMarksOfQuestion { get; set; }

        public string CandidateTextAnswer { get; set; }

        public List<OnlineQuestionOptionsViewModel> QuestionOptions { get; set; }

    }
}