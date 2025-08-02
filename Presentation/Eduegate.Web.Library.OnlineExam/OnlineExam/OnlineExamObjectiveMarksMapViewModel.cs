using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.OnlineExam.OnlineExam
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "ObjectiveQuestionMaps", "CRUDModel.ViewModel.ObjectiveQuestionMaps")]
    [DisplayName("Exam Question Groups")]
    public class OnlineExamObjectiveMarksMapViewModel : BaseMasterViewModel
    {
        public OnlineExamObjectiveMarksMapViewModel()
        {
            //QuestionGroup = new KeyValueViewModel();
        }

        public long ExamQuestionGroupMapIID { get; set; }

        public int? QuestionGroupID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Marks")]
        public long? ObjMarkGroup { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("TotalNoOfQuestions")]
        public long? TotalNoOfQuestions { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("NoOfQuestions")]
        public long? ObjNoOfQuestions { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("TotalMarks")]
        public long? TotalMarks { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index,ModelStructure.ObjectiveQuestionMaps[0], CRUDModel.ViewModel.ObjectiveQuestionMaps)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.ObjectiveQuestionMaps[0],CRUDModel.ViewModel.ObjectiveQuestionMaps)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

    }
}