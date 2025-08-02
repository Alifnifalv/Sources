using Newtonsoft.Json;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Service.Client;
using Eduegate.Services.Contracts.Contents;
using Eduegate.Services.Contracts.Contents.Enums;
using Eduegate.Services.Contracts.Contents.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eduegate.Services.Client.Contents
{
    public class ContentServiceClient : BaseClient, IContentService
    {
        private static string ServiceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string Service = string.Concat(ServiceHost, "Contents/ContentService.svc/");

        public ContentServiceClient(CallContext callContext = null, Action<string> logger = null)
          : base(callContext, logger)
        {
        }

        public ContentFileDTO SaveFile(ContentFileDTO data)
        {
            string uri = string.Concat(Service + "\\SaveFile");
            return ServiceHelper.HttpPostGetRequest(uri, data, _callContext);
        }

        public List<ContentFileDTO> SaveFiles(List<ContentFileDTO> datas)
        {
            string uri = string.Concat(Service + "\\SaveFile");
            return ServiceHelper.HttpPostGetRequest(uri, datas, _callContext);
        }

        public ContentFileDTO GetFile(ContentType contentType, long referenceID)
        {
            string uri = string.Concat(Service + "\\GetFile?contentType=" + contentType + "&referenceID=" + referenceID);
            string result = ServiceHelper.HttpGetRequest(uri, _callContext);
            return JsonConvert.DeserializeObject<ContentFileDTO>(result);
        }

        public ContentFileDTO ReadContentsById(long contentID)
        {
            string uri = string.Concat(Service + "\\ReadContentsById?contentID=" + contentID);
            string result = ServiceHelper.HttpGetRequest(uri, _callContext);
            return JsonConvert.DeserializeObject<ContentFileDTO>(result);
        }
        
        public long DeleteEntity(long contentID)
        {
            string uri = string.Concat(Service + "\\DeleteEntity?contentID=" + contentID);
            string result = ServiceHelper.HttpGetRequest(uri, _callContext);
            return JsonConvert.DeserializeObject<long>(result);
        }
        public List<ContentFileDTO> SaveBulkContentFiles(List<ContentFileDTO> fileDTOs)
        {
            string uri = string.Concat(Service + "\\SaveBulkContentFiles?fileDTOs=" + fileDTOs);
            return ServiceHelper.HttpPostGetRequest(uri, fileDTOs, _callContext);
        }
        public List<ContentFileDTO> GetContentFileList(List<ContentFileDTO> fileDTOs)
        {
            string uri = string.Concat(Service + "\\GetContentFileList?fileDTOs=" + fileDTOs);
            return ServiceHelper.HttpPostGetRequest(uri, fileDTOs, _callContext);
        }
        public Task<ContentFileDTO> ReadContentsByIdAsync(long contentID)
        {
            string uri = string.Concat(Service + "\\ReadContentsByIdAsync?contentID=" + contentID);
            string result = ServiceHelper.HttpGetRequest(uri, _callContext);
            return JsonConvert.DeserializeObject<Task<ContentFileDTO>>(result);

        }
    }
}
