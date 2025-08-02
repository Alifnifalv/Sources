using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Framework.Contracts.Common.Enums.DocManagement;
using System.Collections.Generic;

namespace Eduegate.Web.Library.ViewModels
{
    public class UploadedFileDetailsViewModel
    {
        public string FileName { get; set; }
        public string ActualFileName { get; set; }
        public string FilePath { get; set; }
        public string ThumbImagePath { get; set; }
        public string FileType { get; set; }
        public bool? isSelected { get; set; }
        public long? ContentFileIID { get; set; }
        public int? ContentTypeID { get; set; }
        public string ContentType { get; set; }
        public long? ReferenceID { get; set; }
        public string ContentFileName { get; set; }

        public static List<DocumentFileDTO> ToDocumentVM(List<UploadedFileDetailsViewModel> files,
            EntityTypes type)
        {
            var docVM = new List<DocumentFileDTO>();

            if (files != null)
            {
                foreach (var file in files)
                {
                    docVM.Add(new DocumentFileDTO()
                    {
                        EntityType = type,
                        Description = string.IsNullOrEmpty(file.ActualFileName) ? file.ContentFileName : file.ActualFileName,
                        Title = string.IsNullOrEmpty(file.ActualFileName) ? file.ContentFileName : file.ActualFileName,
                        FileName = !string.IsNullOrEmpty(file.FileName) ? file.FileName : file.ContentFileName,
                        ActualFileName = string.IsNullOrEmpty(file.ActualFileName) ? file.ContentFileName : file.ActualFileName,
                        DocumentFileStatus = DocumentFileStatuses.Draft,
                        ReferenceID = file.ContentFileIID
                    });
                }
            }

            return docVM;
        }
    }
}