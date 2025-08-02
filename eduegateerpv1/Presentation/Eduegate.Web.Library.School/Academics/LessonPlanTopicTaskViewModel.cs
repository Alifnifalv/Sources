using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;
using Eduegate.Framework.Enums;

namespace Eduegate.Web.Library.School.Academics
{

    [ContainerType(Framework.Enums.ContainerTypes.Grid, "LessonPlanTopicTaskMap", "gridModel.LessonPlanTopicTaskMap")]
    [DisplayName("Topic Task")]
    public class LessonPlanTopicTaskViewModel :BaseMasterViewModel
    {
        public LessonPlanTopicTaskViewModel()
        {
            TaskType = new KeyValueViewModel();
            LessonPlanTopicTaskAttach = new List<LessonPlanTopicTaskAttachmentViewModel>() { new LessonPlanTopicTaskAttachmentViewModel() };
        }

        public long LessonPlanTaskMapIID { get; set; }
        public long? LessonPlanTopicMapID { get; set; }
        public long? LessonPlanID { get; set; }

        //[MaxLength(8, ErrorMessage = "Maximum Length should be within 8!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Task Name")]
        public string Task { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2(" TaskType", "Numeric", false, "")]
        [LookUp("LookUps.LessonPlanTaskTypes")]
        [DisplayName("Task Type")]
        //public int? TaskTypeID { get; set; }
        public KeyValueViewModel TaskType { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("Start Date")]
        public string Date1String { get; set; }
        public DateTime? StartDate { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("End Date")]
        public string Date2String { get; set; }
        public DateTime? EndDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.AttachmentComponent, "TaskAttachment")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.Documents, "ProfileUrl", "")]
        [DisplayName("")]
        public List<LessonPlanTopicTaskAttachmentViewModel> LessonPlanTopicTaskAttach { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.Attachments[0], gridModel.LessonPlanTopicTaskMap)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.Attachments[0], gridModel.LessonPlanTopicTaskMap)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

    }
}
