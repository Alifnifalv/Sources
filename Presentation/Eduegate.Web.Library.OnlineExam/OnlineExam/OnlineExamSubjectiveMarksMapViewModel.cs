using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.OnlineExam.OnlineExam
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "SubjectiveQuestionMaps", "CRUDModel.ViewModel.SubjectiveQuestionMaps", gridBindingPrefix: "SubjectiveQuestionMaps")]
    [DisplayName("Exam Question Groups")]
    public class OnlineExamSubjectiveMarksMapViewModel : BaseMasterViewModel
    {
        public OnlineExamSubjectiveMarksMapViewModel()
        {
            //QuestionGroup = new KeyValueViewModel();
        }

        public long ExamQuestionGroupMapIID { get; set; }

        public int? QuestionGroupID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Marks")]
        public long? SubMarkGroup { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("TotalNoOfQuestions")]
        public long? TotalNoOfQuestions { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("NoOfQuestions")]
        public long? SubNoOfQuestions { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("TotalMarks")]
        public long? TotalMarks { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.SubjectiveQuestionMaps[0], CRUDModel.ViewModel.SubjectiveQuestionMaps)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.SubjectiveQuestionMaps[0], CRUDModel.ViewModel.SubjectiveQuestionMaps)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

    }
}