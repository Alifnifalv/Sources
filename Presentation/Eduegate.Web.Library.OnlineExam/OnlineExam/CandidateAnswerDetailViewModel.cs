using Eduegate.Web.Library.ViewModels;
using System;
using Eduegate.Framework.Mvc.Attributes;
using System.ComponentModel;
using System.Collections.Generic;

namespace Eduegate.Web.Library.OnlineExam.OnlineExam
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "AnswerDetails", "CRUDModel.ViewModel.AnswerDetails")]
    [DisplayName("")]
    public class CandidateAnswerDetailViewModel : BaseMasterViewModel
    {
        public CandidateAnswerDetailViewModel()
        {
            Questions = new List<AnswerQuestionsViewModel>() { new AnswerQuestionsViewModel() };
        }

        public long CandidateAnswerIID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("Date Of Answer")]
        public string DateOfAnswerString { get; set; }
        public DateTime? DateOfAnswer { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("Comments")]
        public string Comments { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Selected Option")]
        public string QuestionOptionMap { get; set; }
        public long? QuestionOptionMapID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.GridGroup, "Questions", Attributes4 = "colspan=6")]
        [DisplayName("")]
        public List<AnswerQuestionsViewModel> Questions { get; set; }


        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("Other Details")]
        public string OtherDetails { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[DisplayName("Other Answers")]
        public string OtherAnswers { get; set; }

    }
}