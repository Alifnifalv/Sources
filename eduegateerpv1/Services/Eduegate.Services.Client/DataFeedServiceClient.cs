using System;
using System.Collections.Generic;
using System.IO;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;

namespace Eduegate.Service.Client
{
    public class DataFeedServiceClient : BaseClient, IDataFeedService
    {
        private static string ServiceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string DataFeedService = string.Concat(ServiceHost, Eduegate.Framework.Helper.Constants.DATAFEED_SERVICE_NAME);

        public DataFeedServiceClient(CallContext context = null, Action<string> logger = null)
            : base(context, logger)
        {
        }

        public bool FeedInventory(List<ProductInventoryDTO> inventoryDTOCollection)
        {
            var uri = string.Format("{0}/{1}", DataFeedService, "FeedInventory");
            return bool.Parse(ServiceHelper.HttpPostRequest<List<ProductInventoryDTO>>(uri, inventoryDTOCollection, _callContext));
        }

        public List<DataFeedTypeDTO> GetDataFeedTypes()
        {
            var uri = string.Format("{0}/{1}", DataFeedService, "GetDataFeedTypes");
            return ServiceHelper.HttpGetRequest<List<DataFeedTypeDTO>>(uri, _callContext, _logger);
        }

        public DataFeedTypeDTO GetDataFeedType(int templateID)
        {
            var uri = string.Format("{0}/{1}?templateID={2}", DataFeedService, "GetDataFeedType", templateID);
            return ServiceHelper.HttpGetRequest<DataFeedTypeDTO>(uri, _callContext, _logger);
        }

        public DataFeedLogDTO SaveDataFeedLog(DataFeedLogDTO dataFeedLogDTO)
        {
            var uri = string.Format("{0}/{1}", DataFeedService, "SaveDataFeedLog");
            return ServiceHelper.HttpPostGetRequest<DataFeedLogDTO>(uri, dataFeedLogDTO, _callContext, _logger);
        }

        public DataFeedLogDTO GetDataFeedLogByID(long ID)
        {
            var uri = string.Format("{0}/{1}", DataFeedService, "GetDataFeedLogByID?ID=" + ID);
            return ServiceHelper.HttpGetRequest<DataFeedLogDTO>(uri, _callContext, _logger);
        }

        public Stream DownloadDataFeedTemplate(int templateTypeID)
        {
            var uri = string.Format("{0}/{1}", DataFeedService, "DownloadDataFeedTemplate?templateTypeID=" + templateTypeID);
            return ServiceHelper.DownloadFileStream(uri);
        }

        public DataFeedLogDTO UploadFeedFile(string fileName, long feedLogID, string filePath, Stream stream)
        {
            var uri = string.Format("{0}/{1}", DataFeedService, "UploadFeedFile?fileName=" + fileName + "&feedLogID=" + feedLogID);
            return ServiceHelper.UploadStream<DataFeedLogDTO>(uri, stream, filePath, null, _callContext);
        }

        public Stream DownloadProcessedFeedFile(long feedLogID)
        {
            throw new NotImplementedException();
        }
    }
}
