using System;
using System.Collections.Generic;
using System.IO;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services;
using Eduegate.Services.Contracts;

namespace Eduegate.Service.Client.Direct
{
    public class DataFeedServiceClient : BaseClient, IDataFeedService
    {
        DataFeedService docService = new DataFeedService();

        public DataFeedServiceClient(CallContext context = null, Action<string> logger = null)
            : base(context, logger)
        {
            docService.CallContext = context;
        }

        public bool FeedInventory(List<ProductInventoryDTO> inventoryDTOCollection)
        {
            return docService.FeedInventory(inventoryDTOCollection);
        }

        public List<DataFeedTypeDTO> GetDataFeedTypes()
        {
            return docService.GetDataFeedTypes();
        }

        public DataFeedTypeDTO GetDataFeedType(int templateID)
        {
            return docService.GetDataFeedType(templateID);
        }

        public DataFeedLogDTO SaveDataFeedLog(DataFeedLogDTO dataFeedLogDTO)
        {
            return docService.SaveDataFeedLog(dataFeedLogDTO);
        }

        public DataFeedLogDTO GetDataFeedLogByID(long ID)
        {
            return docService.GetDataFeedLogByID(ID);
        }

        public Stream DownloadDataFeedTemplate(int templateTypeID)
        {
            return docService.DownloadDataFeedTemplate(templateTypeID);
        }

        public DataFeedLogDTO UploadFeedFile(string fileName, long feedLogID, string filePath, Stream stream)
        {
            return docService.UploadFeedFile(fileName, feedLogID, filePath, stream);
        }

        public Stream DownloadProcessedFeedFile(long feedLogID)
        {
            return docService.DownloadProcessedFeedFile(feedLogID);
        }
    }
}
