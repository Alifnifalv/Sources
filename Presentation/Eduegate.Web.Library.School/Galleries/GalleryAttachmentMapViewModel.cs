using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using Eduegate.Framework.Enums;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Gallery
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "GalleryAttachments", "CRUDModel.ViewModel.GalleryAttachments")]
    [DisplayName("Attachment")]
    public class GalleryAttachmentMapViewModel : BaseMasterViewModel
    {
        public long GalleryAttachmentMapIID { get; set; }

        public long? GalleryID { get; set; }


        ////[Required]
        //[MaxLength(1000, ErrorMessage = "Maximum Length should be within 1000!")]
        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[CustomDisplay("Description")]
        //public string AttachmentDescription { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.FileUpload)]
        [CustomDisplay("StudentProfile")]
        [FileUploadInfo("Content/GalleryUploadContents", Framework.Enums.EduegateImageTypes.Documents, "AttachmentContentID", "")]
        public long? AttachmentContentID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("")]
        public string ContentFileName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.GalleryAttachments[0], CRUDModel.ViewModel.GalleryAttachments)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.GalleryAttachments[0],CRUDModel.ViewModel.GalleryAttachments)'")]
        [DisplayName("-")]
        public string Remove { get; set; }

    }
}