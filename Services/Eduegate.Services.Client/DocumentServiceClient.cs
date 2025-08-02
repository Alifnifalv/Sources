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
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Service.Client
{
    public class DocumentServiceClient : BaseClient, IDocumentService
    {
        private static string _serviceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string _documentService = string.Concat(_serviceHost, Eduegate.Framework.Helper.Constants.DOCUMENT_SERVICE_NAME);
        public DocumentServiceClient(CallContext callContext = null, Action<string> logger = null)
            :base(callContext, logger)
        {
        }

        public List<DocumentFileDTO> GetDocuments(long referenceID, EntityTypes entityType)
        {
            var uri = _documentService + "GetDocuments?referenceID=" + referenceID + "&entityType="  +  entityType;
            return ServiceHelper.HttpGetRequest<List<DocumentFileDTO>>(uri, _callContext);
        }

        public List<DocumentFileDTO> SaveDocuments(List<DocumentFileDTO> documents)
        {
            var uri = _documentService + "SaveDocuments";
            return ServiceHelper.HttpPostGetRequest<List<DocumentFileDTO>>(uri, documents, _callContext);
        }
    }
}
