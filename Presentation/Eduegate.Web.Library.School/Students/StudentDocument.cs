using Eduegate.Framework.Contracts.Common.Enums.DocManagement;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.School.Students
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "Documents", "CRUDModel.Model.Document.Documents", "", "", "grid documents", "", true)]
    [DisplayName(" ")]
    public class StudentDocument : BaseMasterViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width detailview-grid")]
        //[Select2("DocType", "String", false, "UploadDocumentTypeChange", false)]
        [Select2("DocType", "Numeric", false, "DocumentChange($index+1)", false, "ng-click=LoadDocuments($index)")]
        [DisplayName("Type")]
        [LookUp("LookUps.DocType")]
        public KeyValueViewModel UploadDocumentType { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.FileUpload)]
        [DisplayName("Document")]
        [FileUploadInfo("Mutual/UploadDocument", Framework.Enums.EduegateImageTypes.Documents, "Docfile", "")]
        public string Docfile { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.FileUpload)]
        ////[Select2("DocType", "String", false, "UploadDocumentTypeChange", false)]
        //[DisplayName("Select File")]
        //public string UploadedFile { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
        [DisplayName("Title")]
        public string Title { get; set; }

        public string Tags { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        [DisplayName("Description")]
        public string Description { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [LookUp("LookUps.OwnerEmployee")]
        [Select2("OwnerEmployeeID", "Numeric", false)]
        [DisplayName("Owner Name")]
        public KeyValueViewModel OwnerEmployeeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [DisplayName("Version")]
        public string Version { get; set; }

        public string DocumentStatusID { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [DisplayName("Status")]
        [LookUp("LookUps.DocumentStatuses")]
        public string FileStatus { get; set; }

        public DocumentFileStatuses DocumentFileStatus { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "ex-small-col-width", "ng-click='RemoveGridRow($index, CRUDModel.Model.MasterViewModel.Document.Documents[0], CRUDModel.Model.MasterViewModel.Document.Documents)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}
