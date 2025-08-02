using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Extensions;
using Eduegate.Domain.Entity;
using Microsoft.Data.SqlClient;

namespace Eduegate.Domain.Repository
{
    public class DataFeedRepository
    {
        public void FeedInventory(List<ProductInventory> inventoryCollection)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                foreach (ProductInventory productInventory in inventoryCollection)
                {
                    dbContext.ProductInventories.Attach(productInventory);
                    dbContext.Entry(productInventory).Property(x => x.Quantity).IsModified = true;
                    dbContext.Entry(productInventory).Property(x => x.UpdatedDate).IsModified = true;
                    dbContext.Entry(productInventory).Property(x => x.UpdatedBy).IsModified = true;
                }

                dbContext.SaveChanges();
            }
        }

        public List<DataFeedType> GetDataFeedTypes()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.DataFeedTypes.AsNoTracking().ToList();
            }
        }

        public DataFeedType GetDataFeedType(int templateID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.DataFeedTypes.Where(a=> a.DataFeedTypeID == templateID).AsNoTracking().FirstOrDefault();
            }
        }

        public List<DataFeedTableColumn> GetDataFeedColumnsByTemplateID(int templateTypeID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var result = from dftc in dbContext.DataFeedTableColumns
                             join dft in dbContext.DataFeedTables on dftc.DataFeedTableID equals dft.DataFeedTableID
                             where dft.DataFeedTypeID == templateTypeID
                             select dftc;
                return result.OrderBy(x => x.SortOrder).AsNoTracking().ToList();
            }
        }

        public DataFeedTable GetPhysicalTableDataFeedTableID(int dataFeedTableID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.DataFeedTables.Where(x => x.DataFeedTableID == dataFeedTableID).AsNoTracking().FirstOrDefault();
            }
        }

        public DataFeedLog SaveDataFeedLog(DataFeedLog dataFeedlog)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                dbContext.DataFeedLogs.Add(dataFeedlog);

                if (dataFeedlog.DataFeedLogIID == default(long))
                {
                    dbContext.Entry(dataFeedlog).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    if (dataFeedlog.DataFeedStatus != null)
                        dbContext.Entry(dataFeedlog.DataFeedStatus).State = EntityState.Detached;

                    if (dataFeedlog.DataFeedType != null)
                        dbContext.Entry(dataFeedlog.DataFeedType).State = EntityState.Detached;

                    dbContext.Entry(dataFeedlog).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();

                //Get all related entities
                return dbContext.DataFeedLogs.Where(x => x.DataFeedLogIID == dataFeedlog.DataFeedLogIID)
                    .Include(y => y.DataFeedStatus)
                    .Include(z => z.DataFeedType)
                    .AsNoTracking()
                    .FirstOrDefault();
            }
        }

        public DataFeedLog GetDataFeedLogByID(long ID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.DataFeedLogs.Where(x => x.DataFeedLogIID == ID)
                    .Include(y => y.DataFeedStatus)
                    .Include(z => z.DataFeedType)
                    .AsNoTracking()
                    .FirstOrDefault();
            }
        }

        public Customer GetCustomerByDataFeedID(long DatafeedID)
        {
            var datafeed = new Customer();

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                datafeed = (from dfl in dbContext.DataFeedLogs
                            join cus in dbContext.Customers on dfl.CreatedBy equals cus.LoginID
                            where dfl.DataFeedLogIID == DatafeedID
                            select cus)
                            .AsNoTracking()
                            .FirstOrDefault();
            }
            return datafeed;
        }

        public long GetNextBatchID(long productSKUMapID, int companyID, long branchID)
        {
            long? lastBatch = 0;
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var productInventories = dbContext.ProductInventories
                    .Where(x => x.ProductSKUMapID == productSKUMapID && x.BranchID == branchID);

                if (productInventories.Count() > 0)
                    lastBatch = productInventories.Select(x => (long?)x.Batch).Max();
            }

            return lastBatch.HasValue ? lastBatch.Value + 1 : 1;
        }

        public void RunSQLCommand(string sqlStatement)
        {
            using (SqlConnection conn = new SqlConnection(Infrastructure.ConfigHelper.GetDefaultConnectionString()))
            using (SqlCommand cmd = new SqlCommand(sqlStatement, conn))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                    conn.Open();

                cmd.CommandText = sqlStatement;
                cmd.ExecuteNonQuery();

                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
        }

        public void ProcessDataFeed(int feedType, int operationID, DataSet dataSet, long feedLogID, int companyID)
        {
            var feedTypeDetail = new DataFeedRepository().GetDataFeedType(feedType);

            using (SqlConnection conn = new SqlConnection(Infrastructure.ConfigHelper.GetDefaultConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(feedTypeDetail.ProcessingSPName, conn))
                {
                    if (conn.State == System.Data.ConnectionState.Closed)
                        conn.Open();


                    var sw = new StringWriter(new StringBuilder(dataSet.GetXml()));
                    var xw = new XmlTextWriter(sw);
                    var transactionXml = new StringReader(sw.ToString());
                    var xmlReader = new XmlTextReader(transactionXml);
                    var sqlXml = new SqlXml(xmlReader); 

                    cmd.Parameters.Add("@FeedType", SqlDbType.Int).Value = feedType;
                    cmd.Parameters.Add("@OperationID", SqlDbType.Int).Value = operationID;
                    cmd.Parameters.Add("@FeedLogID", SqlDbType.Int).Value = feedLogID;
                    cmd.Parameters.Add("@CompanyID", SqlDbType.Int).Value = companyID;
                    cmd.Parameters.AddWithValue("@XMLData", sqlXml);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();

                    if (conn.State == System.Data.ConnectionState.Open)
                        conn.Close();
                }
            }
        }
    }
}
