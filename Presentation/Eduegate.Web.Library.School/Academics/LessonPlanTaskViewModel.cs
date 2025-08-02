using Eduegate.Framework.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Eduegate.Web.Library.School.Academics
{

    [ContainerType(Framework.Enums.ContainerTypes.Grid, "LessonPlanTasks", "CRUDModel.ViewModel.LessonPlanTasks")]
    [DisplayName("Lesson Plan Task")]
    public class LessonPlanTaskViewModel : BaseMasterViewModel
    {
        public LessonPlanTaskViewModel()
        {
            LessonPlanTaskAttach = new List<LessonPlanTaskAttachmentViewModel>() { new LessonPlanTaskAttachmentViewModel() };
        }

        public long LessonPlanTaskMapIID { get; set; }

        public long? LessonPlanTopicMapID { get; set; }
        public long? LessonPlanID { get; set; }

        //[MaxLength(8, ErrorMessage = "Maximum Length should be within 8!")]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [DisplayName("Task Name")]
        public string Task { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2(" TaskType", "Numeric", false, "")]
        [LookUp("LookUps.LessonPlanTaskTypes")]
        [DisplayName("Task Type")]
        //public int? TaskTypeID { get; set; }
        public KeyValueViewModel TaskType { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("TimeDuration")]
        public int? TimeDuration { get; set; }



        [ControlType(Framework.Enums.ControlTypes.AttachmentComponent, "TaskAttachment")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.Documents, "ProfileUrl", "")]
        [DisplayName("")]
        public List<LessonPlanTaskAttachmentViewModel> LessonPlanTaskAttach { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.LessonPlanTasks[0], CRUDModel.ViewModel.LessonPlanTasks)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.LessonPlanTasks[0],CRUDModel.ViewModel.LessonPlanTasks)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}