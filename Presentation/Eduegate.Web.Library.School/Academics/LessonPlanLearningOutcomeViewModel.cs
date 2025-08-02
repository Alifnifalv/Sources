using Eduegate.Framework.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Eduegate.Web.Library.School.Academics
{

    [ContainerType(Framework.Enums.ContainerTypes.Grid, "LessonPlanLearningOutcomes", "CRUDModel.ViewModel.LessonPlanLearningOutcomes")]
    [DisplayName("Lesson Learning Outcome")]
    public class LessonPlanLearningOutcomeViewModel : BaseMasterViewModel
    {
        public LessonPlanLearningOutcomeViewModel()
        {
        }

        public long? LessonPlanID { get; set; }

        public long LessonLearningOutcomeID { get; set; }


        //[MaxLength(8, ErrorMessage = "Maximum Length should be within 8!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Learning Outcomes")]
        public string LessonLearningOutcomeName { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.LessonPlanLearningOutcomes[0], CRUDModel.ViewModel.LessonPlanLearningOutcomes)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.LessonPlanLearningOutcomes [0],CRUDModel.ViewModel.LessonPlanLearningOutcomes)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}