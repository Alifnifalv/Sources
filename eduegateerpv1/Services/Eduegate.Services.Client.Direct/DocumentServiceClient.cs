using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Service.Client.Direct
{
    public class DocumentServiceClient : BaseClient, IDocumentService
    {
        DocumentService docService = new DocumentService();

        public DocumentServiceClient(CallContext callContext = null, Action<string> logger = null)
            :base(callContext, logger)
        {
            docService.CallContext = callContext;
        }

        public List<DocumentFileDTO> GetDocuments(long referenceID, EntityTypes entityType)
        {
            return docService.GetDocuments(referenceID, entityType);
        }

        public List<DocumentFileDTO> SaveDocuments(List<DocumentFileDTO> documents)
        {
            return docService.SaveDocuments(documents);
        }
    }
}
