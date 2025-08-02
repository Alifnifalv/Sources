using Eduegate.Framework.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.School.Academics
{

    [ContainerType(Framework.Enums.ContainerTypes.Grid, "LessonPlanTopics", "CRUDModel.ViewModel.LessonPlanTopics")]
    [DisplayName("Lesson Plan Topics")]
    public class LessonPlanTopicMapsViewModel : BaseMasterViewModel
    {
        public LessonPlanTopicMapsViewModel()
        {
            LessonPlanFileUpload = new List<LessonPlanTopicAttachmentViewModel>() { new LessonPlanTopicAttachmentViewModel() };
            LessonPlanTopicTaskMap = new List<LessonPlanTopicTaskViewModel>() { new LessonPlanTopicTaskViewModel() };
        }

        public long LessonPlanTopicMapIID { get; set; }

        public long? LessonPlanID { get; set; }

        //[Required]

        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "w-100px")]
        [DisplayName("Period")]
        public int? Period { get; set; }


        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Lecture Code")]
        public string LectureCode { get; set; }

        //[Required]
        [MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [DisplayName("Topic")]
        public string Topic { get; set; }


        [ControlType(Framework.Enums.ControlTypes.AttachmentComponent, "FileUpload")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.Documents, "ProfileUrl", "")]
        [DisplayName("")]
        public List<LessonPlanTopicAttachmentViewModel> LessonPlanFileUpload { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width text-center", "ng-click='InsertGridRow($index, ModelStructure.LessonPlanTopics[0], CRUDModel.ViewModel.LessonPlanTopics)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width text-center", "ng-click='RemoveGridRow($index, ModelStructure.LessonPlanTopics[0],CRUDModel.ViewModel.LessonPlanTopics)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

        [ControlType(Framework.Enums.ControlTypes.GridGroup, "Task d-none", Attributes4 = "colspan=6" )]
        [DisplayName("")]
        public List<LessonPlanTopicTaskViewModel> LessonPlanTopicTaskMap { get; set; }
    }
}
