using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Services.Contracts.Enums.Synchronizer;
using System.Data;
using System.Data.SqlClient;
using Eduegate.Framework.Extensions;

namespace Eduegate.Domain.Repository
{
    public class SynchronizerRepository
    {
        public List<EntityChangeTracker> GetNextChangeFromQueue(Eduegate.Services.Contracts.Enums.Synchronizer.Entities entity, int numberOfChanges)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                db.Database.CommandTimeout = 0;
                List<EntityChangeTrackersQueue> queues = null;
                List<EntityChangeTracker> changeTracker = new List<EntityChangeTracker>();

                queues = db.EntityChangeTrackersQueues.Take(numberOfChanges).ToList();

                foreach (var queue in queues)
                {
                    var vTracker = from element in db.EntityChangeTrackers where element.EntityChangeTrackerIID == queue.EntityChangeTrackeID select element;
                    //changeTracker.Add(db.EntityChangeTrackers.Where(a => a.EntityChangeTrackerIID == queue.EntityChangeTrackeID).FirstOrDefault());
                    changeTracker.Add(vTracker.FirstOrDefault());
                }

                foreach (var queue in queues)
                {
                    db.EntityChangeTrackersInProcesses.Add(new EntityChangeTrackersInProcess()
                    {
                        EntityChangeTrackerID = queue.EntityChangeTrackeID,
                        CreatedDate = DateTime.Now,
                        IsReprocess = queue.IsReprocess,
                    });
                    db.EntityChangeTrackersQueues.Remove(queue);

                }

                db.SaveChanges();


                return changeTracker;
            }
        }

        public bool UpdateEntityChangeTrackerStatus(long changeTrackerID, Eduegate.Services.Contracts.Enums.Synchronizer.TrackerStatuses statusID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                try
                {
                    var entity = dbContext.EntityChangeTrackersInProcesses.Where(a => a.EntityChangeTrackerID == changeTrackerID).FirstOrDefault();
                    dbContext.EntityChangeTrackersInProcesses.Remove(entity);

                    //insert into log
                    var entityTracker = dbContext.EntityChangeTrackers.Where(a => a.EntityChangeTrackerIID == changeTrackerID).FirstOrDefault();
                    entityTracker.TrackerStatusID = (int)statusID;
                    entityTracker.UpdatedDate = DateTime.Now;
                    dbContext.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public EntityChangeTracker SaveEntityChangeTracker(EntityChangeTracker tracker)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var result = dbContext.EntityChangeTrackers.Add(tracker);
                dbContext.SaveChanges();
                return dbContext.EntityChangeTrackers.Where(a => a.EntityChangeTrackerIID == tracker.EntityChangeTrackerIID).FirstOrDefault();
            }
        }

        public EntityChangeTrackersQueue AddEntityChangeTrackersQueue(EntityChangeTrackersQueue queueItem)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var result = dbContext.EntityChangeTrackersQueues.Add(queueItem);
                dbContext.SaveChanges();
                return dbContext.EntityChangeTrackersQueues.Where(a => a.EntityChangeTrackerQueueIID == queueItem.EntityChangeTrackerQueueIID).FirstOrDefault();
            }
        }

        public List<SyncFieldMap> GetFieldMaps(FieldMapTypes mapType)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.SyncFieldMaps.Where(a => a.SynchFieldMapTypeID.Value == (int)mapType).ToList();
            }
        }


        public int SyncGetQueueCount(DateTime SyncLastDatetime, DateTime SyncCurrentDatetime)
        {
            return Convert.ToInt32(SearchSyncData(1, SyncLastDatetime, SyncCurrentDatetime,0,0).Rows[0]["SkuCount"]);
        }

        public List<string> SyncGetQueueList(DateTime SyncLastDatetime, DateTime SyncCurrentDatetime,int PageSize, int PageNo)
        {
            List<string> list = new List<string>();
            DataTable dt =  SearchSyncData(2, SyncLastDatetime, SyncCurrentDatetime, PageSize,  PageNo);
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(dr["SKUID"].ToString());
            }
            return list;
        }



        public DataTable SearchSyncData(Int16 type, DateTime SyncLastDatetime, DateTime SyncCurrentDatetime, int PageSize, int PageNo)
        {
            DataTable syncDataList = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationExtensions.GetConnectionString("dbEduegateERPContext").ToString()))
                using (SqlCommand cmd = new SqlCommand("catalog.SpcSkuIIDSyncList", conn))
                {
                    cmd.CommandTimeout = 0;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@Type", SqlDbType.TinyInt));
                    adapter.SelectCommand.Parameters["@Type"].Value = (int)type;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@SyncLastDatetime", SqlDbType.DateTime));
                    adapter.SelectCommand.Parameters["@SyncLastDatetime"].Value = SyncLastDatetime.ToString("yyyy-MM-dd HH:mm");

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@SyncCurrentDatetime", SqlDbType.DateTime));
                    adapter.SelectCommand.Parameters["@SyncCurrentDatetime"].Value = SyncCurrentDatetime.ToString("yyyy-MM-dd HH:mm");

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@PageSize", SqlDbType.Int));
                    adapter.SelectCommand.Parameters["@PageSize"].Value = PageSize;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@PageNo", SqlDbType.Int));
                    adapter.SelectCommand.Parameters["@PageNo"].Value = PageNo;

                    DataSet dt = new DataSet();
                    adapter.Fill(dt);
                    syncDataList = dt.Tables[0];
                }

            }
            catch (Exception)
            {
            }

            return syncDataList;
        }
    }
}
