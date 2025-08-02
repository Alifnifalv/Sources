using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.OnlineExam.OnlineExam
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "ExamQuestionGroupMaps", "CRUDModel.ViewModel.ExamQuestionGroupMaps")]
    [DisplayName("Exam Question Groups")]
    public class ExamQuestionGroupMapViewModel : BaseMasterViewModel
    {
        public ExamQuestionGroupMapViewModel()
        {
            QuestionGroup = new KeyValueViewModel();
        }

        public long ExamQuestionGroupMapIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("QuestionGroups", "Numeric", false, "QnGroupChanges(gridModel)")]
        [LookUp("LookUps.QuestionGroups")]
        [DisplayName("Question Group")]
        public KeyValueViewModel QuestionGroup { get; set; }
        public int? QuestionGroupID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Number Of Questions")]
        public long? NumberOfQuestions { get; set; }

        public long? GroupTotalQnCount { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("MaximumMarks")]
        public decimal? MaximumMarks { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index,ModelStructure.ExamQuestionGroupMaps[0], CRUDModel.ViewModel.ExamQuestionGroupMaps)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.ExamQuestionGroupMaps[0],CRUDModel.ViewModel.ExamQuestionGroupMaps)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

    }
}