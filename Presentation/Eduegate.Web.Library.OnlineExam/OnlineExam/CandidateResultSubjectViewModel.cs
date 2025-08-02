using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;

namespace Eduegate.Web.Library.OnlineExam.OnlineExam
{

    [ContainerType(Framework.Enums.ContainerTypes.Grid, "OnlineExamQuestionMapDetail", "CRUDModel.ViewModel.OnlineExamQuestionMapDetail")]
    [DisplayName("Questions")]
    public class CandidateResultSubjectViewModel : BaseMasterViewModel
    {

        public CandidateResultSubjectViewModel()
        {

        }

        public long OnlineExamSubjectMapIID { get; set; }

        public long? OnlineExamID { get; set; }

        public int? SubjectID { get; set; }

        public string SubjectName { get; set; }

        public decimal? Marks { get; set; }

    }
}