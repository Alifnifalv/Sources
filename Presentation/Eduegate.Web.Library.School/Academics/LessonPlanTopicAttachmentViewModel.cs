using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Framework.Enums;

namespace Eduegate.Web.Library.School.Academics
{

    [ContainerType(Framework.Enums.ContainerTypes.Grid, "LessonPlanFileUpload", "gridModel.LessonPlanFileUpload")]
    [DisplayName("File Upload")]
    public class LessonPlanTopicAttachmentViewModel :BaseMasterViewModel
    {
        public long LessonPlanTopicAttachmentMapIID { get; set; }

        public long? LessonPlanTopicMapID { get; set; }


        //[Required]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Attachment Name")]
        public string AttachmentName { get; set; }

        //[Required]
        public string ProfileFile { get; set; }

        public string ProfileUrl { get; set; }


        //[Required]
        [ControlType(Framework.Enums.ControlTypes.FileUpload)]
        [DisplayName("Attachment")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.Documents, "ProfileUrl", "")]
        public string ProfileUploadFile { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Attachment Reference ID")]
        public long? AttachmentReferenceID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.Attachments[0], gridModel.LessonPlanFileUpload)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.Attachments[0], gridModel.LessonPlanFileUpload)'")]
        [DisplayName("-")]
        public string Remove{ get; set; }
    }
}
