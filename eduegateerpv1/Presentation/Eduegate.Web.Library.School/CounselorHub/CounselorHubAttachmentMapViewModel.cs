using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Enums;
using System.Globalization;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.CounselorHub
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "CounselorHubAttachments", "CRUDModel.ViewModel.CounselorHubAttachments")]
    [DisplayName("Attachment")]
    public class CounselorHubAttachmentMapViewModel : BaseMasterViewModel
    {
        public long CounselorHubAttachmentMapIID { get; set; }

        public long? CounselorHubID { get; set; }

        public long? StudentID { get; set; }


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

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.CounselorHubAttachments[0], CRUDModel.ViewModel.CounselorHubAttachments)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.CounselorHubAttachments[0],CRUDModel.ViewModel.CounselorHubAttachments)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

    }
}