using System.Collections.Generic;
using System.IO;

namespace Eduegate.Services.Contracts
{
    public interface IDataFeedService
    {
        bool FeedInventory(List<ProductInventoryDTO> inventoryDTOCollection);

        List<DataFeedTypeDTO> GetDataFeedTypes();

        DataFeedTypeDTO GetDataFeedType(int templateID);

        DataFeedLogDTO SaveDataFeedLog(DataFeedLogDTO dataFeedLogDTO);

        DataFeedLogDTO GetDataFeedLogByID(long ID);

        Stream DownloadProcessedFeedFile(long feedLogID);

        Stream DownloadDataFeedTemplate(int templateTypeID);

        DataFeedLogDTO UploadFeedFile(string fileName, long feedLogID, string filePath, Stream stream);
    }
}