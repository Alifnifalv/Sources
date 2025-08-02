using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Mappers;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts;
using Eduegate.Utilities.DataExportImport;

namespace Eduegate.Domain
{
    public class DataFeedBL
    {
        private CallContext _context;

        public DataFeedBL()
        {
        }

        public DataFeedBL(CallContext context)
        {
            _context = context;
        }


        public List<DataFeedTypeDTO> GetDataFeedTypes()
        {
            DataFeedRepository repo = new DataFeedRepository();
            List<DataFeedTypeDTO> dataFeedTypeCollection = new List<DataFeedTypeDTO>();
            DataFeedMapper mapper = DataFeedMapper.Mapper(_context);
            foreach (DataFeedType dft in repo.GetDataFeedTypes())
                dataFeedTypeCollection.Add(mapper.ToDataFeedTypeDTO(dft));

            return dataFeedTypeCollection;
        }

        public DataFeedTypeDTO GetDataFeedType(int templateID)
        {
            return DataFeedMapper.Mapper(_context).ToDataFeedTypeDTO(new DataFeedRepository().GetDataFeedType(templateID));
        }

        public void FeedInventory(List<ProductInventoryDTO> inventoryDTOCollection)
        {
            DataFeedRepository repo = new DataFeedRepository();
            List<ProductInventory> productInventoryEntityCollection = new List<ProductInventory>();
            DataFeedMapper mapper = DataFeedMapper.Mapper(_context);
            foreach (ProductInventoryDTO inventoryDTO in inventoryDTOCollection)
            {
                productInventoryEntityCollection.Add(mapper.ToProductInventoryEntity(inventoryDTO));
            }
            repo.FeedInventory(productInventoryEntityCollection);
        }

        public DataFeedLogDTO SaveDataFeedLog(DataFeedLogDTO dto)
        {
            var mapper = DataFeedMapper.Mapper(_context);
            return mapper.ToDataFeedLogDTO(new DataFeedRepository().SaveDataFeedLog(mapper.ToDataFeedLogEntity(dto)));
        }

        public DataFeedLogDTO GetDataFeedLogByID(long ID)
        {
            DataFeedMapper mapper = DataFeedMapper.Mapper(_context);
            return mapper.ToDataFeedLogDTO(new DataFeedRepository().GetDataFeedLogByID(ID));
        }

        public string GetDefaultTemplate(int templateTypeID)
        {
            string WBDocumentsRootPath = ConfigurationExtensions.GetAppConfigValue("WBDocuments");
            var repo = new DataFeedRepository();
            var dft = repo.GetDataFeedTypes().Where(x => x.DataFeedTypeID == templateTypeID).FirstOrDefault();
            var tableColumns = repo.GetDataFeedColumnsByTemplateID(templateTypeID);

            var columnNames = tableColumns.OrderBy(x => x.SortOrder).Select(y => y.DisplayColumnName).ToList();
            string templatePath = Path.Combine(WBDocumentsRootPath, "DataFeed", "Templates");
            string exportedFile = Export.ToExcelTemplate(columnNames, templatePath, dft.TemplateName);
            return Path.Combine(templatePath, exportedFile);
        }

        public string GetProcessedFeedFilePath(long feedLogID)
        {
            string WBDocumentsRootPath = ConfigurationExtensions.GetAppConfigValue("WBDocuments");
            DataFeedRepository repo = new DataFeedRepository();
            DataFeedLog dfl = repo.GetDataFeedLogByID(feedLogID);

            return Path.Combine(WBDocumentsRootPath, "DataFeed", dfl.CreatedBy.ToString(), "FeedLogs", dfl.DataFeedLogIID.ToString(), dfl.FileName);
        }

        public DataFeedLogDTO ProcessFeed(string feedFileLocation, long feedLogID)
        {
            bool isSuccessful = false;

            var repo = new DataFeedRepository();
            var feedLog = repo.GetDataFeedLogByID(feedLogID);
            var feedTemplateColumns = repo.GetDataFeedColumnsByTemplateID((int)feedLog.DataFeedTypeID);
            var feedDataSet = Export.ToDataSet(feedTemplateColumns.Select(a=> a.PhysicalColumnName).ToList<string>(), feedFileLocation, feedTemplateColumns.Count);
            AddValidationColumns(feedDataSet);

            if (ValidateFeed(feedTemplateColumns, feedDataSet))
            {
                isSuccessful = ExecuteDBOperation(feedTemplateColumns, feedDataSet, (int)feedLog.DataFeedTypeID, 2, feedLog.DataFeedLogIID);
                feedLog.DataFeedStatusID = (short)Eduegate.Services.Contracts.Enums.DataFeedStatus.Success;
            }
            else
            {
                feedLog.DataFeedStatusID = (short)Eduegate.Services.Contracts.Enums.DataFeedStatus.Failed;
            }

            feedLog.FileName = Path.GetFileName(feedFileLocation);
            Export.ToExcel(feedDataSet, Path.GetDirectoryName(feedFileLocation), feedLog.FileName);
            isSuccessful = true;

            return DataFeedMapper.Mapper(_context).ToDataFeedLogDTO(repo.SaveDataFeedLog(feedLog));
        }

        private bool ValidateFeed(List<DataFeedTableColumn> feedTemplateColumns, DataSet feedDataSet)
        {
            int errorCount = 0;
            StringBuilder errorLog = new StringBuilder();

            ValidateFeedColumnCount(feedTemplateColumns, feedDataSet, errorCount, errorLog);
            ValidateColumnNames(feedTemplateColumns, feedDataSet, errorCount, errorLog);

            foreach (DataTable dataTable in feedDataSet.Tables)
            {
                ValidateRecordCount(dataTable, errorCount, errorLog);

                //Stop further processing if feed file basic validation fails
                if (errorCount > 0)
                {
                    dataTable.Rows[1]["IsValid"] = false;
                    dataTable.Rows[1]["ValidationFailureReason"] = errorLog.ToString();
                    return false;
                }
            }

            foreach (DataTable dataTable in feedDataSet.Tables)
                ValidateData(feedTemplateColumns, dataTable, errorCount, errorLog);

            return errorCount == 0;

        }

        private static void ValidateData(List<DataFeedTableColumn> feedTemplateColumns, DataTable feedDataTable, int errorCount, StringBuilder errorLog)
        {
            foreach (DataRow row in feedDataTable.Rows)
            {
                for (int columnCounter = 0; columnCounter <= GetFeedColumnCountExcludingValidationFields(feedDataTable) - 1; columnCounter++)
                {
                    string expectedDataType = feedTemplateColumns[columnCounter].DataType;
                    switch (expectedDataType)
                    {
                        case "Numeric":
                            string receivedValue = row[columnCounter].ToString();
                            long actualValue;
                            if (!long.TryParse(receivedValue, out actualValue))
                            {
                                LogError(errorCount, errorLog, string.Format("Column: '{0}', Numeric value expected.", feedTemplateColumns[columnCounter].DisplayColumnName));
                            }
                            break;
                    }
                }
                if (errorCount > 0)
                {
                    row["IsValid"] = "False";
                    row["ValidationFailureReason"] = errorLog.ToString();
                }
                else
                {
                    row["IsValid"] = "True";
                }
            }
        }

        private static void ValidateRecordCount(DataTable feedDataTable, int errorCount, StringBuilder errorLog)
        {
            if (feedDataTable.Rows.Count < 1)
                LogError(errorCount, errorLog, "No data available for update!");
        }

        private static void ValidateColumnNames(List<DataFeedTableColumn> feedTemplateColumns, DataSet feedDataSet, int errorCount, StringBuilder errorLog)
        {
            foreach (DataTable dataTable in feedDataSet.Tables)
            {
                for (int columnCounter = 0; columnCounter <= GetFeedColumnCountExcludingValidationFields(dataTable) - 1; columnCounter++)
                {
                    DataColumn column = dataTable.Columns[columnCounter];
                    if (column.Caption != feedTemplateColumns[columnCounter].DisplayColumnName)
                    {
                        LogError(errorCount, errorLog, string.Format("Column '{0}' not matching with template column '{1}'", column.Caption, feedTemplateColumns[columnCounter].DisplayColumnName));
                    }
                }
            }
        }

        private static void ValidateFeedColumnCount(List<DataFeedTableColumn> feedTemplateColumns, DataSet feedDataSet, int errorCount, StringBuilder errorLog)
        {
            foreach (DataTable dataTable in feedDataSet.Tables)
            {
                int feedColumnCount = GetFeedColumnCountExcludingValidationFields(dataTable);
                int columnSettingCount = feedTemplateColumns.Count;
                if (feedColumnCount != columnSettingCount)
                {
                    LogError(errorCount, errorLog, "Feed column count not matching with template columns!Don't modify the column sequence or Number of columns defined in the downloaded template.");
                }
            }
        }

        private static int GetFeedColumnCountExcludingValidationFields(DataTable feedDataTable)
        {
            return feedDataTable.Columns.Count - 4; //-4 is to reduce the validation columns that is added by the program initally at the time of processing
        }

        private static void LogError(int errorCount, StringBuilder errorLog, string message)
        {
            if (errorLog.Length > 0)
            {
                errorLog.Append(Environment.NewLine);
            }
            errorLog.Append(message);
            errorCount++;
        }

        private void AddValidationColumns(DataSet feedDataSet)
        {
            foreach(DataTable dt in feedDataSet.Tables)
            {
                DataColumn column = new DataColumn();
                column.ColumnName = "IsValid";
                column.DataType = typeof(string);
                dt.Columns.Add(column);

                column = new DataColumn();
                column.ColumnName = "ValidationFailureReason";
                column.DataType = typeof(string);
                dt.Columns.Add(column);

                column = new DataColumn();
                column.ColumnName = "IsDataUpdated";
                column.DataType = typeof(string);
                dt.Columns.Add(column);

                column = new DataColumn();
                column.ColumnName = "DataUpdationFailureReason";
                column.DataType = typeof(string);
                dt.Columns.Add(column);
            }
        }

        private bool ExecuteDBOperation(List<DataFeedTableColumn> feedColumns, DataSet feedDataSet, int feedTypeID, int operationID, long logIID)
        {
            try
            {
                new DataFeedRepository().ProcessDataFeed(feedTypeID, operationID, feedDataSet, logIID, _context.IsNotNull() && _context.CompanyID.HasValue ? _context.CompanyID.Value : 0);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool ExecuteDBUpdate(List<DataFeedTableColumn> feedColumns, DataSet feedDataSet, DataFeedTable physicalTable)
        {
            int errorCount = 0;

            foreach (DataTable dataTable in feedDataSet.Tables)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    try
                    {
                        StringBuilder updateQuery = new StringBuilder();
                        updateQuery.Append("UPDATE ");
                        updateQuery.Append(physicalTable.TableName);
                        updateQuery.Append(" SET ");
                        foreach (DataFeedTableColumn feedTableColumn in feedColumns.Where(x => x.IsPrimaryKey == false))
                        {
                            updateQuery.Append(feedTableColumn.PhysicalColumnName);
                            updateQuery.Append(" = ");
                            if (feedTableColumn.DataType.Equals("String", StringComparison.OrdinalIgnoreCase))
                            {
                                updateQuery.Append("'");
                                updateQuery.Append(row[feedTableColumn.DisplayColumnName].ToString().Replace("'", "''"));
                                updateQuery.Append("'");
                            }
                            else if (feedTableColumn.DataType.Equals("Numeric", StringComparison.OrdinalIgnoreCase))
                            {
                                updateQuery.Append(row[feedTableColumn.DisplayColumnName].ToString());
                            }

                        }
                        updateQuery.Append(" WHERE ");

                        foreach (DataFeedTableColumn feedTableColumn in feedColumns.Where(x => x.IsPrimaryKey == true))
                        {
                            updateQuery.Append(feedTableColumn.PhysicalColumnName);
                            updateQuery.Append(" = ");
                            if (feedTableColumn.DataType.Equals("String", StringComparison.OrdinalIgnoreCase))
                            {
                                updateQuery.Append("'");
                                updateQuery.Append(row[feedTableColumn.DisplayColumnName].ToString().Replace("'", "''"));
                                updateQuery.Append("'");
                            }
                            else if (feedTableColumn.DataType.Equals("Numeric", StringComparison.OrdinalIgnoreCase))
                            {
                                updateQuery.Append(row[feedTableColumn.DisplayColumnName].ToString());
                            }

                            updateQuery.Append(" AND ");

                        }

                        DataFeedRepository repo = new DataFeedRepository();
                        repo.RunSQLCommand(updateQuery.ToString().TrimEnd(" AND "));
                    }
                    catch (Exception ex)
                    {
                        row["IsDataUpdated"] = "No";
                        row["DataUpdationFailureReason"] = ex.Message.IsNotNull() ? ex.Message : string.Empty;
                        errorCount++;
                    }
                    row["IsDataUpdated"] = "Yes";
                }
            }

            return errorCount == 0;
        }

        private static bool ExecuteDBInsert(List<DataFeedTableColumn> feedColumns, DataSet feedDataSet, DataFeedTable physicalTable, int feedTypeID)
        {
            int errorCount = 0;
            if (feedTypeID != 2) //2 = inventory.ProductInventories, it has special logic to get next available BatchID to insert data
            {
                foreach (DataTable table in feedDataSet.Tables)
                    foreach (DataRow row in table.Rows)
                    {
                        try
                        {
                            StringBuilder insertQuery = new StringBuilder();
                            insertQuery.Append("INSERT INTO ");
                            insertQuery.Append(physicalTable.TableName);
                            insertQuery.Append(" ( ");
                            feedColumns.ForEach(x => insertQuery.Append(string.Concat(x.PhysicalColumnName, ",")));
                            insertQuery = new StringBuilder(insertQuery.ToString().TrimEnd(","));
                            insertQuery.Append(" ) ");
                            insertQuery.Append(" VALUES ");
                            insertQuery.Append(" ( ");

                            foreach (DataFeedTableColumn feedTableColumn in feedColumns)
                            {
                                if (feedTableColumn.DataType.Equals("String", StringComparison.OrdinalIgnoreCase))
                                {
                                    insertQuery.Append("'");
                                    insertQuery.Append(row[feedTableColumn.DisplayColumnName].ToString().Replace("'", "''"));
                                    insertQuery.Append("'");
                                }
                                else if (feedTableColumn.DataType.Equals("Numeric", StringComparison.OrdinalIgnoreCase))
                                {
                                    insertQuery.Append(row[feedTableColumn.DisplayColumnName].ToString());
                                }

                                insertQuery.Append(",");

                            }
                            insertQuery = new StringBuilder(insertQuery.ToString().TrimEnd(","));
                            insertQuery.Append(" ) ");

                            DataFeedRepository repo = new DataFeedRepository();
                            repo.RunSQLCommand(insertQuery.ToString());

                        }
                        catch (Exception ex)
                        {
                            row["IsDataUpdated"] = "No";
                            row["DataUpdationFailureReason"] = ex.Message.IsNotNull() ? ex.Message : string.Empty;
                            errorCount++;
                        }
                        row["IsDataUpdated"] = "Yes";
                    }

                return errorCount == 0;
            }
            else
                return ExecuteDBInsertProductInventory(feedColumns, feedDataSet, physicalTable);

        }

        private static bool ExecuteDBInsertProductInventory(List<DataFeedTableColumn> feedColumns, DataSet feedDataTable, DataFeedTable physicalTable)
        {
            int errorCount = 0;
            string physicialColumnNames = string.Join(",", feedColumns.Select(x => x.PhysicalColumnName));
            physicialColumnNames = string.Concat(physicialColumnNames, ",", "Batch");
            var insertQuery = new StringBuilder();
            insertQuery.Append(string.Format("INSERT INTO {0} ({1}) VALUES (", physicalTable.TableName, physicialColumnNames));
            DataFeedRepository repo = new DataFeedRepository();

            foreach (DataTable table in feedDataTable.Tables)
                foreach (DataRow row in table.Rows)
                {
                    try
                    {
                        long productSKUID = long.Parse(row["SKU ID"].ToString());
                        int companyID = int.Parse(row["Company ID"].ToString());
                        int branchID = int.Parse(row["Branch ID"].ToString());
                        long nextBatchID = repo.GetNextBatchID(productSKUID, companyID, branchID);
                        long quantity = long.Parse(row["Quantity"].ToString());
                        var query = new StringBuilder();
                        query.Append(insertQuery.ToString());
                        query.Append(string.Format("{0},{1},{2},{3},{4}"
                            , productSKUID.ToString(), companyID.ToString(), branchID.ToString(), quantity.ToString(), nextBatchID.ToString()));

                        query.Append(" ) ");

                        repo.RunSQLCommand(query.ToString());
                    }
                    catch (Exception ex)
                    {
                        row["IsDataUpdated"] = "No";
                        row["DataUpdationFailureReason"] = ex.Message.IsNotNull() ? ex.Message : string.Empty;
                        errorCount++;
                    }

                    row["IsDataUpdated"] = "Yes";
                }

            return errorCount == 0;
        }
    }
}
