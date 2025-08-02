using System.Collections.Generic;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Contracts.Common.Enums;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDocumentService" in both code and config file together.
    public interface IDocumentService
    {
        List<DocumentFileDTO> GetDocuments(long referenceID, EntityTypes entityType);
        
        List<DocumentFileDTO> SaveDocuments(List<DocumentFileDTO> documents);
    }
}