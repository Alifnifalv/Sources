using Eduegate.Framework;
using Eduegate.Service.Client;
using Eduegate.Services.Contents;
using Eduegate.Services.Contracts.Contents;
using Eduegate.Services.Contracts.Contents.Enums;
using Eduegate.Services.Contracts.Contents.Interfaces;
using Eduegate.Services.Contracts.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eduegate.Services.Client.Contents.Direct
{
    public class ContentServiceClient : BaseClient, IContentService
    {
        ContentService service = new ContentService();

        public ContentServiceClient(CallContext callContext = null, Action<string> logger = null)
        {
            service.CallContext = callContext;
        }

        public ContentFileDTO SaveFile(ContentFileDTO data)
        {
            return service.SaveFile(data);
        }

        public List<ContentFileDTO> SaveFiles(List<ContentFileDTO> datas)
        {
            return service.SaveFiles(datas);
        }

        public ContentFileDTO GetFile(ContentType contentType, long referenceID)
        {
            return service.GetFile(contentType, referenceID);
        }

        public ContentFileDTO ReadContentsById(long contentID)
        {
            return service.ReadContentsById(contentID);
        }
        public long DeleteEntity(long contentID)
        {
            return service.DeleteEntity(contentID);
        }
        public List<ContentFileDTO> SaveBulkContentFiles(List<ContentFileDTO> fileDTOs)
        {
            return service.SaveBulkContentFiles(fileDTOs);
        }
        public List<ContentFileDTO> GetContentFileList(List<ContentFileDTO> fileDTOs)
        {
            return service.GetContentFileList(fileDTOs);
        }

        public async Task<ContentFileDTO> ReadContentsByIdAsync(long contentID)
        {
            return await service.ReadContentsByIdAsync(contentID);
        }
    }
}
