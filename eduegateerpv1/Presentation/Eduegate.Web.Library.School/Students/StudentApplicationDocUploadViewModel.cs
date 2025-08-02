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
using Eduegate.Services.Contracts.School.Students;
using System.Globalization;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.School.Students
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "DocumentsUpload", "CRUDModel.ViewModel.DocumentsUpload")]
    [DisplayName("Upload Documents")]
    public class StudentApplicationDocUploadViewModel : BaseMasterViewModel
    {

        public long ApplicationDocumentIID { get; set; }
        public long? ApplicationID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.AttachmentComponent, "text-left")]
        [CustomDisplay("BirthCertificate")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.Documents, "BirthCertificateReferenceID", "")]
        public string BirthCertificateReferenceID { get; set; }
        public string BirthCertificateAttach { get; set; }

        [ControlType(Framework.Enums.ControlTypes.AttachmentComponent, "text-left")]
        [CustomDisplay("Studentpassport")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.Documents, "StudentPassportReferenceID", "")]
        public string StudentPassportReferenceID { get; set; }
        public string StudentPassportAttach { get; set; }

        [ControlType(Framework.Enums.ControlTypes.AttachmentComponent, "text-left")]
        [CustomDisplay("TC")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.Documents, "TCReferenceID", "")]
        public string TCReferenceID { get; set; }
        public string TCAttach { get; set; }


        //[Required]
        [ControlType(Framework.Enums.ControlTypes.AttachmentComponent, "text-left")]
        [CustomDisplay("FatherQID")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.Documents, "FatherQIDReferenceID", "")]
        public string FatherQIDReferenceID { get; set; }
        public string FatherQIDAttach { get; set; }

        [ControlType(Framework.Enums.ControlTypes.AttachmentComponent, "text-left")]
        [CustomDisplay("MotherQID")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.Documents, "MotherQIDReferenceID", "")]
        public string MotherQIDReferenceID { get; set; }
        public string MotherQIDAttach { get; set; }


        [ControlType(Framework.Enums.ControlTypes.AttachmentComponent, "text-left")]
        [CustomDisplay("StudentQID")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.Documents, "StudentQIDReferenceID", "")]
        public string StudentQIDReferenceID { get; set; }
        public string StudentQIDAttach { get; set; }


    }
}
