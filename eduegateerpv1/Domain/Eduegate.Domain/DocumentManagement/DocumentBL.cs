using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Framework.Contracts.Common.Enums.DocManagement;
using Eduegate.Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Eduegate.Domain.DocumentManagements
{
    public class DocumentBL
    {
        private CallContext _callContext;
        public DocumentBL(CallContext callContext)
        {
            _callContext = callContext;
        }

        public List<DocumentFileDTO> GetDocuments(long? referenceID, EntityTypes entityType)
        {
            // Get documents
            var documents = new DocumentRepository().GetDocuments(referenceID, (int)entityType);

            var documentsDTO = new List<DocumentFileDTO>();
            if (documents.IsNotNull() && documents.Count > 0)
            {
                // traverse list to assign entity values
                //foreach (var doc in documents)
                //{
                //    documentsDTO.Add(new DocumentFileDTO
                //    {
                //        DocumentFileIID = doc.DocumentFileIID,
                //        DocumentFileStatus = (DocumentFileStatuses)Enum.Parse(typeof(DocumentFileStatuses), doc.DocumentFileStatus.StatusName),
                //        DocumentFileType = (DocumentFileTypes)doc.DocFileTypeID.Value,
                //        Description = doc.Description,
                //        //OwnerEmployeeID = doc.OwnerEmployeeID.HasValue ?
                //        //        new KeyValueDTO
                //        //        {
                //        //            Key = Convert.ToString(doc.OwnerEmployee.EmployeeIID),
                //        //            Value = Convert.ToString(doc.OwnerEmployee.EmployeeName)
                //        //        } : null,
                //        EntityType = (EntityTypes)Enum.Parse(typeof(EntityTypes), doc.EntityTypeID.ToString()),
                //        FileName = doc.FileName,
                //        Title = doc.Title,
                //        Version = doc.Version,
                //        ReferenceID = doc.ReferenceID,
                //        DocumentStatusID = Convert.ToString(doc.DocumentStatus.DocumentStatusID),
                //        //TimeStamps = Convert.ToBase64String(doc.TimeStamps),
                //        ExtractedData = doc.ExtractedData
                //    });
                //}
            }
            return documentsDTO;
        }

        
        public List<DocumentFileDTO> SaveDocuments(List<DocumentFileDTO> documents, EntityTypes? entityType, long? referenceID)
        {
            var entityDocs = new List<DocumentFile>();
            var dtoDocs = new List<DocumentFileDTO>();

            var localRefID = referenceID;
            var localEntityTypeID = entityType;
            // Create entity from dto
            if (documents.Count > 0)
            {
                if (!referenceID.HasValue)
                {
                    localRefID = documents.First().ReferenceID;
                }

                if (!localEntityTypeID.HasValue)
                {
                    localEntityTypeID = documents.First().EntityType;
                }

                foreach (var doc in documents)
                {
                    if (doc.FileName.IsNotNullOrEmpty())
                    {
                        if (!doc.DocumentFileType.HasValue)
                        {
                            var extension = Path.GetExtension(doc.FileName);
                            var fileExtenstion = string.IsNullOrEmpty(extension) ? null : extension.Substring(1).ToUpper();
                            doc.DocumentFileType = fileExtenstion == null ? (DocumentFileTypes?)null : (DocumentFileTypes)Enum.Parse(typeof(DocumentFileTypes), fileExtenstion);
                        }

                        try
                        {
                            if (doc.ContentData != null)
                            {
                                switch (doc.DocumentFileType)
                                {
                                    case DocumentFileTypes.PDF:
                                       // doc.ExtractedData = InvoiceExtractor.ExtractText(doc.ContentData);
                                       // doc.ExtractedGridData = DocumentRepository.ExtractGridToJson(doc.ContentData, "spreadhsheet");
                                        doc.ExtractedData1 = DocumentRepository.ExtractHeaderToJson(doc.ContentData, "nurminen");
                                        doc.ExtractedData2 = DocumentRepository.ExtractGridToJson(doc.ContentData, "simplenurminen");
                                        break;
                                    case DocumentFileTypes.JPG:
                                    case DocumentFileTypes.PNG:
                                        using (var ms = new MemoryStream(doc.ContentData))
                                        {
#pragma warning disable CA1416 // Validate platform compatibility
                                            //doc.ExtractedData = InvoiceExtractor.ExtractImage(new Bitmap(ms));
#pragma warning restore CA1416 // Validate platform compatibility
                                        }
                                        break;
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            Eduegate.Logger.LogHelper<DocumentBL>.Fatal(ex.Message, ex);
                        }

                        var entityFile = new DocumentFile
                        {
                            Description = doc.Description,
                            //DocumentStatusID = (int)doc.DocumentFileStatus,
                            EntityTypeID = (int)localEntityTypeID.Value,
                            DocFileTypeID = (int?)doc.DocumentFileType,
                            FileName = doc.FileName,
                            ActualFileName = doc.ActualFileName,
                            ReferenceID = localRefID,
                            OwnerEmployeeID = doc.OwnerEmployeeID != null ? Convert.ToInt64(doc.OwnerEmployeeID.Key) : default(long?),
                            Title = doc.Title,
                            Version = doc.Version,
                            //TimeStamps = string.IsNullOrEmpty(doc.TimeStamps) ? null : Convert.FromBase64String(doc.TimeStamps),
                            UpdatedBy = Convert.ToInt64(_callContext.UserId),
                            UpdatedDate = DateTime.UtcNow,
                            ExtractedData = doc.ExtractedData,
                            ExtractedGridData  = doc.ExtractedGridData,

                        };

                        if (doc.DocumentFileIID <= 0)
                        {
                            entityFile.CreatedBy = Convert.ToInt64(_callContext.UserId);
                            entityFile.CreatedDate = DateTime.UtcNow;
                        }
                        else
                        {
                            entityFile.DocumentFileIID = doc.DocumentFileIID;
                            entityFile.UpdatedBy = Convert.ToInt64(_callContext.UserId);
                            entityFile.UpdatedDate = DateTime.UtcNow;
                        }

                        entityDocs.Add(entityFile);
                    }
                }
            }

            // TODO
            //  save document maps
            //var result = new DocumentRepository().SaveDocuments(entityDocs);

            //if (result)
            //{
            //    dtoDocs = GetDocuments(documents.First().ReferenceID, documents.First().EntityType);

            //}           

            return documents;
        }
    }
}
