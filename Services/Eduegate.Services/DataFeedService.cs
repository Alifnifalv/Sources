using System.ServiceModel;
using Eduegate.Domain;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;

namespace Eduegate.Services
{
    public class DataFeedService : BaseService, IDataFeedService
    {
        public DataFeedLogDTO SaveDataFeedLog(DataFeedLogDTO dataFeedLogDTO)
        {
            try
            {
                return new DataFeedBL(CallContext).SaveDataFeedLog(dataFeedLogDTO);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<DataFeedService>.Fatal("Failed to save DataFeedLog entity", ex);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool FeedInventory(List<ProductInventoryDTO> inventoryDTOCollection)
        {
            bool isSuccessful = false;
            try
            {
                new DataFeedBL(CallContext).FeedInventory(inventoryDTOCollection);
                isSuccessful = true;
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<DataFeedService>.Fatal("Failed to update product inventory", ex);
                throw new FaultException("Internal server, please check with your administrator");
            }

            return isSuccessful;
        }

        public DataFeedLogDTO GetDataFeedLogByID(long ID)
        {
            try
            {
                return new DataFeedBL(CallContext).GetDataFeedLogByID(ID);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<DataFeedService>.Fatal("Failed to save DataFeedLog entity", ex);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<DataFeedTypeDTO> GetDataFeedTypes()
        {
            try
            {
               return new DataFeedBL(CallContext).GetDataFeedTypes();
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<DataFeedService>.Fatal("Failed to update product inventory", ex);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public Stream DownloadDataFeedTemplate(int templateTypeID)
        {
            try
            {
                string fileNameWithPath = new DataFeedBL().GetDefaultTemplate(templateTypeID);
                string fileName = Path.GetFileName(fileNameWithPath);
                String headerInfo = string.Concat("attachment; filename=", fileName);

                //TODO: Uncomment this code to download the file in browser
                //if (WebOperationContext.Current != null)
                //{
                //    WebOperationContext.Current.OutgoingResponse.Headers["Content-Disposition"] = headerInfo;
                //    WebOperationContext.Current.OutgoingResponse.ContentType = "application/octet-stream";
                //}

                return File.OpenRead(fileNameWithPath);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<DataFeedService>.Fatal("Failed to Download Template for template ID:" + templateTypeID.ToString(), ex);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public Stream DownloadProcessedFeedFile(long feedLogID)
        {
            try
            {
                string fileNameWithPath = new DataFeedBL().GetProcessedFeedFilePath(feedLogID);
                string fileName = Path.GetFileName(fileNameWithPath);
                String headerInfo = string.Concat("attachment; filename=", fileName);

                //TODO: Uncomment this code to download the file in browser
                //WebOperationContext.Current.OutgoingResponse.Headers["Content-Disposition"] = headerInfo;
                //WebOperationContext.Current.OutgoingResponse.ContentType = "application/octet-stream";

                return File.OpenRead(fileNameWithPath);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<DataFeedService>.Fatal("Failed to Download Processed Feed file for Data Feed Log ID:" + feedLogID.ToString(), ex);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public DataFeedLogDTO UploadFeedFile(string fileName, long feedLogID, string filePath, Stream stream)
        {
            //string WBDocumentsRootPath = new Domain.Setting.SettingBL().GetSettingValue<string>("WBDocuments");
            //string fileLocation = Path.Combine(WBDocumentsRootPath, "DataFeed", CallContext.LoginID.ToString(), "FeedLogs", feedLogID.ToString());

            //if (!Directory.Exists(fileLocation))
            //    Directory.CreateDirectory(fileLocation);

            //filePath = filePath.IsNullOrEmpty()? Path.Combine(fileLocation, fileName): filePath;

            //int length = 0;
            //using (FileStream writer = new FileStream(filePath, FileMode.Create))
            //{
            //    int readCount;
            //    var buffer = new byte[8192];
            //    while ((readCount = stream.Read(buffer, 0, buffer.Length)) != 0)
            //    {
            //        writer.Write(buffer, 0, readCount);
            //        length += readCount;
            //    }
            //}
            return new DataFeedBL(CallContext).ProcessFeed(filePath, feedLogID);
        }

        public DataFeedTypeDTO GetDataFeedType(int templateID)
        {
            try
            {
                return new DataFeedBL(CallContext).GetDataFeedType(templateID);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<DataFeedService>.Fatal("Failed to update product inventory", ex);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }
    }
}
