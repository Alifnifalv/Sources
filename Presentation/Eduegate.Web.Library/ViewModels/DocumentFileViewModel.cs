using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Payroll;
using Eduegate.Web.Library.ViewModels.Payroll;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Framework.Contracts.Common.Enums.DocManagement;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "Documents", "CRUDModel.Model.MasterViewModel.Document.Documents", "", "", "grid documents", "", true)]
    [DisplayName(" ")]
    public class DocumentFileViewModel : BaseMasterViewModel
    {
        public DocumentFileViewModel() { }
        public long DocumentFileIID { get; set; }
        public string FileName { get; set; }
        public EntityTypes EntityType { get; set; }
        public string EntityTypeValue { get; set; }

        public long ReferenceID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "ex-small-col-width")]
        [DisplayName(" ")]
        public string IsRowSelect { get; set; }


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
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
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
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
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

        public string FilePath { get; set; }

        public static List<DocumentFileViewModel> FromDTO(List<DocumentFileDTO> dto)
        {
            Mapper<DocumentFileDTO, DocumentFileViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var maps = Mapper<List<DocumentFileDTO>, List<DocumentFileViewModel>>.Map(dto);

            if (maps.Count == 0)
            {
                maps.Add(new DocumentFileViewModel());
            }
            return maps;
        }

        public static List<DocumentFileDTO> ToDTO(List<DocumentFileViewModel> vm)
        {
            Mapper<DocumentFileViewModel, DocumentFileDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var maps = Mapper<List<DocumentFileViewModel>, List<DocumentFileDTO>>.Map(vm);

            if (maps.Count == 0)
            {
                maps.Add(new DocumentFileDTO());
            }
            return maps;
        }
    }
}
