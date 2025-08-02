using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Extensions;

namespace Eduegate.Domain.Repository
{
    public class SchedulerRepository
    {
        public EntityScheduler GetEntityScheduler(long schedulerID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.EntitySchedulers.Where(a => a.EntitySchedulerIID == schedulerID).FirstOrDefault();
            }

        }

        public List<EntityScheduler> GetEntityScheduler(int schedulerTypeID, string entityID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.EntitySchedulers.Where(a => a.SchedulerTypeID == schedulerTypeID && a.EntityID == entityID).ToList();
            }
        }

        public EntityScheduler GetEntityScheduler(int schedulerTypeID, int schedulerEntityTypeID, string entityID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.EntitySchedulers.Where(a => a.SchedulerTypeID == schedulerTypeID && a.SchedulerEntityTypeID == schedulerEntityTypeID && a.EntityID == entityID).FirstOrDefault();
            }
        }

        public List<EntityScheduler> SaveEntityScheduler(List<EntityScheduler> entities)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    var dbData = new SchedulerRepository().GetEntityScheduler(entities[0].SchedulerTypeID.Value, entities[0].EntityID);
                    if (dbData.IsNotNull() && dbData.Count > 0)
                    {
                        foreach (var data in dbData)
                        {
                            var dtm = dbContext.EntitySchedulers.Where(x => x.EntityID == data.EntityID).ToList();
                            dbContext.EntitySchedulers.RemoveRange(dtm);
                        }

                        dbContext.SaveChanges();
                    }
                    if (entities.IsNotNull() && entities.Count > 0)
                    {
                        foreach (var entity in entities)
                        {
                            if (entity.EntitySchedulerIID <= 0)
                            {
                                dbContext.EntitySchedulers.Add(entity);
                                dbContext.Entry(entity).State = EntityState.Added;
                            }
                        }

                        dbContext.SaveChanges();
                    }

                    return GetEntityScheduler(entities[0].SchedulerTypeID.Value, entities[0].EntityID);
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetailRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }
        }
    }
}
