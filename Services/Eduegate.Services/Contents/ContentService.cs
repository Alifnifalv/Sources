using Eduegate.Domain.Content;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts.Contents;
using Eduegate.Services.Contracts.Contents.Enums;
using Eduegate.Services.Contracts.Contents.Interfaces;
using System.ServiceModel;

namespace Eduegate.Services.Contents
{
    public class ContentService : BaseService, IContentService
    {
        public ContentFileDTO SaveFile(ContentFileDTO data)
        {
            try
            {
                return new ContentBL(CallContext).SaveFile(data);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ContentService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<ContentFileDTO> SaveFiles(List<ContentFileDTO> datas)
        {
            try
            {
                return new ContentBL(CallContext).SaveFiles(datas);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ContentService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public ContentFileDTO GetFile(ContentType contentType, long referenceID)
        {
            try
            {
                return new ContentBL(CallContext).GetFile(contentType, referenceID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ContentService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public ContentFileDTO ReadContentsById(long contentID)
        {
            try
            {
                return new ContentBL(CallContext).ReadContentsById(contentID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ContentService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public long DeleteEntity(long contentID)
        {
            try
            {
                return new ContentBL(CallContext).DeleteEntity(contentID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ContentService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }
        public List<ContentFileDTO> SaveBulkContentFiles(List<ContentFileDTO> fileDTOs)
        {
            try
            {
                return new ContentBL(CallContext).SaveBulkContentFiles(fileDTOs);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ContentService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }
        public List<ContentFileDTO> GetContentFileList(List<ContentFileDTO> fileDTOs)
        {
            try
            {
                return new ContentBL(CallContext).GetContentFileList(fileDTOs);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ContentService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public async Task<ContentFileDTO> ReadContentsByIdAsync(long contentID)
        {
            try
            {
                return await new ContentBL(CallContext).ReadContentsByIdAsync(contentID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ContentService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }
    }
}
