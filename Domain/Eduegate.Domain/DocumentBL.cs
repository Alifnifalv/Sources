using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Mappers;
using Eduegate.Domain.Mappers.Mutual;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Framework.Contracts.Common.Enums.DocManagement;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Mutual;


namespace Eduegate.Domain
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
                foreach (var doc in documents)
                {
                    documentsDTO.Add(new DocumentFileDTO
                    {
                        DocumentFileIID = doc.DocumentFileIID,
                        DocumentFileStatus = (DocumentFileStatuses)Enum.Parse(typeof(DocumentFileStatuses), doc.DocumentFileStatus.StatusName),
                        DocumentFileType = (DocumentFileTypes)doc.DocFileTypeID.Value,
                        Description = doc.Description,
                        OwnerEmployeeID = new KeyValueDTO { Key = Convert.ToString(doc.Employee.EmployeeIID), Value = Convert.ToString(doc.Employee.EmployeeName) },
                        EntityType = (EntityTypes)Enum.Parse(typeof(EntityTypes), doc.EntityTypeID.ToString()),
                        FileName = doc.FileName,
                        Title = doc.Title,
                        Version = doc.Version,
                        ReferenceID = doc.ReferenceID,
                        DocumentStatusID = Convert.ToString(doc.DocumentFileStatus.DocumentStatusID),
                        //TimeStamps = Convert.ToBase64String(doc.TimeStamps),
                    });
                }
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
                        if(!doc.DocumentFileType.HasValue)
                        {
                            var fileExtenstion = Path.GetExtension(doc.FileName).Substring(1);
                            doc.DocumentFileType = (DocumentFileTypes)Enum.Parse(typeof(DocumentFileTypes), fileExtenstion.ToUpper());
                        }

                        var entityFile = new DocumentFile
                        {
                            Description = doc.Description,
                            //DocumentStatusID = (int)doc.DocumentFileStatus,
                            EntityTypeID = (int)localEntityTypeID.Value,
                            DocFileTypeID = (int)doc.DocumentFileType,
                            FileName = doc.FileName,
                            ActualFileName = doc.ActualFileName,
                            ReferenceID = localRefID,
                            OwnerEmployeeID = doc.OwnerEmployeeID != null ? Convert.ToInt64(doc.OwnerEmployeeID.Key) : default(long?),
                            Title = doc.Title,
                            Version = doc.Version,
                            //TimeStamps = string.IsNullOrEmpty(doc.TimeStamps) ? null : Convert.FromBase64String(doc.TimeStamps),
                            UpdatedBy = Convert.ToInt64(_callContext.UserId),
                            UpdatedDate = DateTime.UtcNow,

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

            // save document maps
            var result = new DocumentRepository().SaveDocuments(entityDocs);
            if (result)
            {
                dtoDocs = GetDocuments(documents.First().ReferenceID, documents.First().EntityType);
            }
            return dtoDocs;
        }

        public List<DocumentTypeSettingsDTO> GetDocumentTypeSettingsByTypeID(int documentTypeID)
        {
            var result = DocumentTypeSettingsMapper.Mapper(_callContext).GetDocumentTypeSettingsByTypeID(documentTypeID);

            return result;
        }

    }
}
