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
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Academics
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "LessonPlanAttachments", "CRUDModel.ViewModel.LessonPlanAttachments")]
    [DisplayName("Attachment")]
    public class LessonPlanAttachmentMapViewModel : BaseMasterViewModel
    {
        public long LessonPlanAttachmentMapIID { get; set; }

        public long? LessonPlanID { get; set; }


        //[Required]
        [MaxLength(1000, ErrorMessage = "Maximum Length should be within 1000!")]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Description")]
        public string AttachmentDescription { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.AttachmentComponent)]
        [CustomDisplay("Attachment")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.Documents, "ContentFileIID", "")]
        public long? ContentFileIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("")]
        public string ContentFileName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width text-center", "ng-click='InsertGridRow($index, ModelStructure.LessonPlanAttachments[0], CRUDModel.ViewModel.LessonPlanAttachments)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width text-center", "ng-click='RemoveGridRow($index, ModelStructure.LessonPlanAttachments[0],CRUDModel.ViewModel.LessonPlanAttachments)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

    }
}