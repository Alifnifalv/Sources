using System.ServiceModel;
using Eduegate.Domain.DocumentManagements;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;

namespace Eduegate.Services
{
    public class DocumentService : BaseService, IDocumentService
    {
        public  List<DocumentFileDTO> GetDocuments(long referenceID, EntityTypes entityType)
        {
            try
            {
                var result = new DocumentBL(CallContext).GetDocuments(referenceID, entityType);
                Eduegate.Logger.LogHelper<DocumentService>.Info("Service Result : " + Convert.ToString(result));
                return result;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<DocumentService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<DocumentFileDTO> SaveDocuments(List<DocumentFileDTO> documents)
        {
            try
            {
                var result = new DocumentBL(CallContext).SaveDocuments(documents, null, null);
                Eduegate.Logger.LogHelper<DocumentService>.Info("Service Result : " + Convert.ToString(result));
                return result;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<DocumentService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }
    }
}
