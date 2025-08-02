using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.CustomEntity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Domain.Repository
{
    public class WarehouseRepository
    {
        public List<JobStatus> GetJobStatuses()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.JobStatuses.OrderBy(a => a.StatusName).ToList();
            }
        }

        public List<JobActivity> GetJobActivities()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.JobActivities.OrderBy(a => a.ActivityName).ToList();
            }
        }

        public List<Priority> GetJobPriority()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Priorities.OrderBy(a => a.Description).ToList();
            }
        }

        public Priority GetJobPriority(int priorityID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Priorities.Where(a => a.PriorityID == priorityID).FirstOrDefault();
            }
        }

        public List<LocationType> GetLocationType()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.LocationTypes.OrderBy(a => a.Description).ToList();
            }
        }

        public List<JobStatus> GetJobStatusByID(long jobTypeID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var jobstatus = (from js in dbContext.JobStatuses
                                 join ja in dbContext.JobActivities
                                 on js.JobTypeID equals ja.JobActivityID
                                 where js.JobTypeID == jobTypeID
                                 select js).OrderBy(a => a.StatusName).ToList();
                return jobstatus;
            }
        }

        public JobEntryHead CreateUpdateJobEntry(JobEntryHead entity)
        {
            try
            {
                if (entity.IsNotNull())
                {
                    using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                    {
                        dbContext.JobEntryHeads.Add(entity);

                        if (entity.JobEntryHeadIID <= 0)
                            dbContext.Entry(entity).State = System.Data.Entity.EntityState.Added;
                        else
                            dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;

                        if (entity.OrderContactMap != null)
                        {
                            if(entity.OrderContactMap.OrderContactMapIID == 0)
                            {
                                dbContext.Entry(entity.OrderContactMap).State = System.Data.Entity.EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(entity.OrderContactMap).State = System.Data.Entity.EntityState.Modified;
                                entity.OrderContactMapID = entity.OrderContactMap.OrderContactMapIID;
                            }
                        }

                        if (entity.JobEntryDetails.IsNotNull() && entity.JobEntryDetails.Count > 0)
                        {
                            foreach (JobEntryDetail detailEntity in entity.JobEntryDetails)
                            {
                                if (detailEntity.JobEntryDetailIID <= 0)
                                    dbContext.Entry(detailEntity).State = System.Data.Entity.EntityState.Added;
                                else
                                    dbContext.Entry(detailEntity).State = System.Data.Entity.EntityState.Modified;
                            }
                        }

                        dbContext.SaveChanges();

                        long headIID = entity.JobEntryHeadIID;

                        entity = new JobEntryHead();
                        entity = dbContext.JobEntryHeads
                            .Include("TransactionHead")
                            .Include("TransactionHead.TransactionDetails")
                            .Where(a => a.JobEntryHeadIID == headIID).FirstOrDefault();

                        if (entity.IsNotNull())
                            dbContext.Entry(entity).Collection(a => a.JobEntryDetails).Load();
                    }
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<WarehouseRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }

            return entity;
        }

        public JobEntryHead GetJobEntryDetails(long jobEntryHeadIID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var head = dbContext.JobEntryHeads
                    .Include("TransactionHead")
                    .Include("TransactionHead.TransactionDetails")
                    .Include("Transactionhead.TransactionHeadShoppingCartMaps")
                    .Include("TransactionHead.OrderContactMaps")
                    .Include("TransactionHead.TransactionHeadEntitlementMaps")
                    .Include("Priority")
                    .Include("JobStatus")
                    .Include("JobOperationStatus")
                    .Include("DocumentType1")
                    .Include("OrderContactMap")
                    .Where(a => a.JobEntryHeadIID == jobEntryHeadIID)
                    .FirstOrDefault();

                if (head != null)
                {
                    dbContext.Entry(head).Collection(a => a.JobEntryDetails).Load();
                    // dbContext.Entry(head).Collection(a => a.TransactionHead.TransactionDetails).Load();
                    foreach (var item in head.JobEntryDetails)
                    {
                        /* Added if block as ProductSKUID is null in job details for mission, please add logic to get referenceJob Head 
                         * and then their detail and pick sku from that detail item if needed.
                         */
                        if (item.ProductSKUID.IsNotNull())
                        {
                            dbContext.Entry(item).Reference(a => a.ProductSKUMap).Load();
                            dbContext.Entry(item.ProductSKUMap).Reference(d => d.Product).Load();
                        }
                    }
                }

                return head;
            }
        }




        public PaymentDetailsTheFort GetPayfortDetails(long trackID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.PaymentDetailsTheForts.Where(x => x.TrackID == trackID).FirstOrDefault();
            }

        }



        public Currency GetCurrencyDetails(int currencyCode)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.Currencies.Where(c => c.NumericCode == currencyCode).FirstOrDefault();
            }
        }

        public Status GetStatusDetails(int statusID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.Statuses.Where(c => c.StatusID == statusID).FirstOrDefault();
            }
        }
        public List<JobEntryHead> GetJobOperationDetailsByJobIds(List<string> jobEntryHeadIIDs)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var heads = dbContext.JobEntryHeads
                     .Include("TransactionHead")
                     .Include("TransactionHead.TransactionDetails")
                     .Include("Transactionhead.TransactionHeadShoppingCartMaps")
                     .Include("TransactionHead.OrderContactMaps")
                     .Include("TransactionHead.TransactionHeadEntitlementMaps")
                    //.Include("TransactionHead.TransactionHeadShoppingCartMap")
                    .Where(a => jobEntryHeadIIDs.Contains(a.JobEntryHeadIID.ToString())).ToList();
                foreach (var head in heads)
                {
                    dbContext.Entry(head).Collection(a => a.JobEntryDetails).Load();
                }

                return heads;
            }
        }

        public List<JobEntryHead> GetJobOperationDetailsByJobId(long jobEntryHeadIID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var jobdetails = (from jed in dbContext.JobEntryDetails
                                  join jh in dbContext.JobEntryHeads on jed.ParentJobEntryHeadID equals jh.JobEntryHeadIID
                                  where jed.JobEntryHeadID == jobEntryHeadIID
                                  select jh)
                                  .Include(x => x.TransactionHead)
                                  .ToList();
                return jobdetails;

            }
        }

        public List<TransactionHeadShoppingCartMap> GetCartByJobId(long jobEntryHeadIID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                long cartID = 0;
                var salesOrders = new List<TransactionHeadShoppingCartMap>();
                var transactionHeadShoppingCartMap = (from jeh in dbContext.JobEntryHeads
                                                      join thscm in dbContext.TransactionHeadShoppingCartMaps on jeh.TransactionHeadID equals thscm.TransactionHeadID
                                                      where jeh.JobEntryHeadIID == jobEntryHeadIID
                                                      select thscm).SingleOrDefault();
                if (transactionHeadShoppingCartMap.IsNotNull())
                {
                    cartID = Convert.ToInt64(transactionHeadShoppingCartMap.ShoppingCartID);
                    salesOrders = dbContext.TransactionHeadShoppingCartMaps.Where(t => t.ShoppingCartID == cartID).ToList();
                }
                return salesOrders;
            }
        }

        public List<TransactionHeadShoppingCartMap> GetCartDetailsByHeadID(long HeadIID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                long cartID = 0;
                var salesOrders = new List<TransactionHeadShoppingCartMap>();
                var transactionHeadShoppingCartMap = (from thscm in dbContext.TransactionHeadShoppingCartMaps
                                                      join th in dbContext.TransactionHeads
                                                      on thscm.TransactionHeadID equals th.HeadIID
                                                      where th.HeadIID == HeadIID
                                                      select thscm).FirstOrDefault();
                if (transactionHeadShoppingCartMap.IsNotNull())
                {
                    cartID = Convert.ToInt64(transactionHeadShoppingCartMap.ShoppingCartID);
                    salesOrders = dbContext.TransactionHeadShoppingCartMaps.Where(t => t.ShoppingCartID == cartID).ToList();
                }
                return salesOrders;
            }
        }

        public TransactionHead GetTransactionByCartID(long cartId)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return (from sc in db.ShoppingCarts
                        join
thscm in db.TransactionHeadShoppingCartMaps on
sc.ShoppingCartIID equals thscm.ShoppingCartID
                        join
th in db.TransactionHeads on thscm.TransactionHeadID equals th.HeadIID
                        where sc.ShoppingCartIID == cartId
                        select th).Include(x => x.OrderContactMaps).FirstOrDefault();
            }
        }

        public List<JobOperationStatus> GetJobOperationStatuses(int jobStatusId)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (jobStatusId > 0)
                {
                    var operationStatus = (from op in dbContext.JobOperationStatuses
                                           join
                                           opm in dbContext.JobStatusOperationMaps on op.JobOperationStatusID equals
                                           opm.JobOperationStatusId
                                           where opm.JobStatusId == jobStatusId
                                           select op).OrderBy(a => a.Description).ToList();
                    return operationStatus;
                }
                else
                {
                    return dbContext.JobOperationStatuses.OrderBy(a => a.Description).ToList();
                }
            }
        }

        public JobOperationStatus GetJobOperation(long jobOperationStatusID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return (from jobOperation in dbContext.JobOperationStatuses
                        where jobOperation.JobOperationStatusID == jobOperationStatusID
                        select jobOperation).FirstOrDefault();
            }
        }

        public List<JobStatus> GetMissionJobStatuses()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.JobStatuses.OrderBy(a => a.StatusName).Where(x => x.JobStatusID > 6).ToList();
            }
        }

        public Basket GetBasket(long basketID)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    var basketDetail = dbContext.Basket.Where(a => a.BasketID == basketID).FirstOrDefault();
                    return basketDetail;
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<WarehouseRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }
        }


        public Basket GetBasketByJob(long jobEntryHeadID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var basketDetail = (from jeh in db.JobEntryHeads
                                    join bskt in db.Basket
on jeh.BasketID equals bskt.BasketID
                                    where jeh.JobEntryHeadIID == jobEntryHeadID
                                    select bskt).FirstOrDefault();
                return basketDetail;
            }
        }

        public Basket SaveBasket(Basket entity)
        {
            Basket updatedEntity = null;

            try
            {
                if (entity.IsNotNull())
                {
                    using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                    {
                        if (entity.BasketID == 0)
                        {
                            var basketID = dbContext.Basket.Max(x => (int?)x.BasketID);
                            entity.BasketID = basketID.HasValue ? basketID.Value + 1 : 1;
                        }

                        if (dbContext.Basket.Any(x => x.BasketID == entity.BasketID))
                            dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                        else
                            dbContext.Entry(entity).State = System.Data.Entity.EntityState.Added;

                        dbContext.SaveChanges();
                        updatedEntity = dbContext.Basket.Where(x => x.BasketID == entity.BasketID).FirstOrDefault();
                        return updatedEntity;
                    }
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<WarehouseRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }

            return updatedEntity;
        }

        public JobEntryHead AssignedJob(JobEntryHead jobEntryHead)
        {
            JobEntryHead entity = new JobEntryHead();

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    entity = dbContext.JobEntryHeads
                        .Where(x => x.JobEntryHeadIID == jobEntryHead.JobEntryHeadIID).FirstOrDefault();

                    if (entity.IsNotNull())
                    {
                        entity.JobOperationStatusID = jobEntryHead.JobOperationStatusID;
                        entity.EmployeeID = jobEntryHead.EmployeeID;
                        entity.UpdatedBy = jobEntryHead.UpdatedBy;
                        entity.UpdatedDate = jobEntryHead.UpdatedDate;

                        dbContext.SaveChanges();

                        entity = new JobEntryHead();
                        entity = dbContext.JobEntryHeads
                        .Include("TransactionHead")
                        .Include("TransactionHead.TransactionDetails")
                        .Include("Transactionhead.TransactionHeadShoppingCartMaps")
                        .Include("TransactionHead.OrderContactMaps")
                        .Include("TransactionHead.TransactionHeadEntitlementMaps")
                        .Where(a => a.JobEntryHeadIID == jobEntryHead.JobEntryHeadIID).FirstOrDefault();
                        dbContext.Entry(entity).Collection(a => a.JobEntryDetails).Load();
                    }

                    return entity;
                }
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<WarehouseRepository>.Fatal(ex.Message.ToString(), ex);
                throw ex;
            }
        }

        public Location SaveSKULocation(Location locationEntity)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    if (locationEntity.LocationIID <= 0)
                        dbContext.Entry(locationEntity).State = System.Data.Entity.EntityState.Added;

                    else
                        dbContext.Entry(locationEntity).State = System.Data.Entity.EntityState.Modified;

                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<WarehouseRepository>.Fatal(ex.Message.ToString(), ex);
                throw ex;
            }

            return locationEntity;
        }

        public JobStatus GetJobStatus(int jobStatusID)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    return dbContext.JobStatuses.Where(x => x.JobStatusID == jobStatusID).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<WarehouseRepository>.Fatal(ex.Message.ToString(), ex);
                throw ex;
            }
        }

        public bool DeleteJobEntry(List<JobEntryDetail> jobEntryDetails)
        {
            bool result = false;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    if (jobEntryDetails.IsNotNull() && jobEntryDetails.Count > 0)
                    {
                        foreach (JobEntryDetail jobEntryDetail in jobEntryDetails)
                        {
                            bool isParentJob = dbContext.JobEntryDetails.Any(x => x.ParentJobEntryHeadID == jobEntryDetail.ParentJobEntryHeadID);

                            if (isParentJob == true)
                            {
                                var jobEntryDetailEntity = (from jedEntity in dbContext.JobEntryDetails
                                                            where jedEntity.ParentJobEntryHeadID == jobEntryDetail.ParentJobEntryHeadID && jedEntity.JobEntryHeadID == jobEntryDetail.JobEntryHeadID
                                                            select jedEntity).FirstOrDefault();

                                dbContext.JobEntryDetails.Remove(jobEntryDetailEntity);

                                JobEntryHead jobEntryHead = GetJobEntryDetails(Convert.ToInt32(jobEntryDetail.ParentJobEntryHeadID));

                                if (jobEntryHead.IsNotNull())
                                {
                                    jobEntryHead.JobStatusID = (int)JobStatuses.Packed;
                                    // To prevent A referential integrity constraint violation occurred (not right)
                                    jobEntryHead.JobStatus = null;
                                    bool isStatusUpdated = UpdateJobStatusOnCreateMission(jobEntryHead);
                                }
                            }
                        }

                        dbContext.SaveChanges();
                        result = true;
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<WarehouseRepository>.Fatal(ex.Message.ToString(), ex);
                throw ex;
            }
        }

        public bool UpdateJobStatusOnCreateMission(JobEntryHead jobEntryHead)
        {
            bool result = false;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    if (jobEntryHead.JobEntryHeadIID <= 0)
                        dbContext.Entry(jobEntryHead).State = System.Data.Entity.EntityState.Added;
                    else
                        dbContext.Entry(jobEntryHead).State = System.Data.Entity.EntityState.Modified;

                    dbContext.SaveChanges();

                    result = true;
                }
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<WarehouseRepository>.Fatal(ex.Message.ToString(), ex);
                throw ex;
            }

            return result;
        }

        public List<JobEntryDetail> GetRemoveJobEntryDetail(JobEntryHead jobEntryHead)
        {
            List<JobEntryDetail> jobDetailsToRemove = new List<JobEntryDetail>();

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    if (jobEntryHead.JobEntryHeadIID > 0)
                    {
                        List<JobEntryDetail> jobDetailsEntity = dbContext.JobEntryDetails.Where(x => x.JobEntryHeadID == jobEntryHead.JobEntryHeadIID).ToList();

                        if (jobDetailsEntity.IsNotNull() && jobDetailsEntity.Count > 0)
                        {
                            foreach (JobEntryDetail jedEntity in jobDetailsEntity)
                            {
                                bool isExist = jobEntryHead.JobEntryDetails.Any(x => x.ParentJobEntryHeadID == jedEntity.ParentJobEntryHeadID);

                                if (isExist == false)
                                    jobDetailsToRemove.Add(jedEntity);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<WarehouseRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }

            return jobDetailsToRemove;
        }

        public List<JobEntryDetail> GetJobDetailsByParentJobEntryHeadIID(long parentJobEntryHeadID, long jobEntryHeadID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return (from jedEntity in dbContext.JobEntryDetails
                        where jedEntity.ParentJobEntryHeadID == parentJobEntryHeadID && jedEntity.JobEntryHeadID == jobEntryHeadID
                        select jedEntity).ToList();
            }
        }

        public List<JobSize> GetJobSizes()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.JobSizes.OrderBy(a => a.Description).ToList();
            }
        }

        public OrderContactMap GetOrderContact(long orderID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.OrderContactMaps.Where(x => x.OrderID == orderID).FirstOrDefault();
            }
        }

        public bool UpdateOrderContact(long ID, decimal Latitude, decimal Longitude)
        {
            bool result = false;

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                OrderContactMap orderContactMap = dbContext.OrderContactMaps.Where(x => x.OrderID == ID).FirstOrDefault();

                if (orderContactMap.IsNotNull())
                {
                    orderContactMap.Latitude = Latitude;
                    orderContactMap.Longitude = Longitude;

                    dbContext.SaveChanges();

                    result = true;
                }

                var transactionHead = dbContext.TransactionHeads.Where(y => y.HeadIID == ID).FirstOrDefault();

                if (transactionHead.IsNotNull())
                {
                    var contacts = dbContext.Contacts.Where(z => z.CustomerID == transactionHead.CustomerID).ToList();

                    if (contacts.IsNotNull() && contacts.Count > 0)
                    {
                        foreach (var contact in contacts)
                        {
                            Contact contactEntity = dbContext.Contacts.Where(c => c.ContactIID == contact.ContactIID).FirstOrDefault();

                            if (contactEntity.IsNotNull())
                            {
                                contactEntity.Latitude = Latitude;
                                contactEntity.Longitude = Longitude;

                                dbContext.SaveChanges();

                                result = true;
                            }
                        }
                    }
                }

                return result;
            }
        }

        public Contact GetCustomerDetailForJobMission(long transactionID)
        {
            Contact contact = new Contact();

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var transactionHead = dbContext.TransactionHeads.Include("Customer").Where(x => x.HeadIID == transactionID).FirstOrDefault();

                if (transactionHead.Customer.IsNotNull())
                    contact = dbContext.Contacts.Include("Customer").Include("Login").Where(y => y.LoginID == transactionHead.Customer.LoginID).FirstOrDefault();
            }

            return contact;
        }

        public bool ValidateSalesInvoiceForStockOut(long jobEntryHeadId, int documentRefTypeId)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return (from
                        th in db.TransactionHeads
                        join
d in db.DocumentTypes on th.DocumentTypeID equals d.DocumentTypeID
                        join
r in db.DocumentReferenceTypes on d.ReferenceTypeID equals r.ReferenceTypeID
                        where th.JobEntryHeadID == jobEntryHeadId && r.ReferenceTypeID == documentRefTypeId
                        select th).Any();
            }
        }

        public JobStatus UpdateJobStatus(long jobHeadId, byte jobOperationStatusId, int jobStatusId)
        {
            var jobStatus = new JobStatus();
            using (var db = new dbEduegateERPContext())
            {
                var job = db.JobEntryHeads.Where(x => x.JobEntryHeadIID == jobHeadId).FirstOrDefault();
                if (job.IsNotNull())
                {
                    job.JobStatusID = jobStatusId;
                    job.JobOperationStatusID = jobOperationStatusId;
                    try
                    {
                        db.SaveChanges();
                        jobStatus = db.JobStatuses.Where(x => x.JobStatusID == jobStatusId).FirstOrDefault();
                    }
                    catch (Exception)
                    {
                        jobStatus = null;
                    }
                }
            }
            return jobStatus;
        }

        public bool UpdateJobsStatus(List<JobEntryHead> jobEntryHeads)
        {
            bool result = false;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    foreach (JobEntryHead jobEntry in jobEntryHeads)
                    {
                        var JobEntryHeadInDB = dbContext.JobEntryHeads.Where(x => x.JobEntryHeadIID == jobEntry.JobEntryHeadIID).FirstOrDefault();
                        if (JobEntryHeadInDB != null)
                        {
                            JobEntryHeadInDB.JobStatusID = jobEntry.JobStatusID;
                            JobEntryHeadInDB.UpdatedBy = jobEntry.UpdatedBy;
                            JobEntryHeadInDB.UpdatedDate = DateTime.Now;
                        }

                    }
                    dbContext.SaveChanges();
                }

                return result;
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<WarehouseRepository>.Fatal(ex.Message.ToString(), ex);
                throw ex;
            }
        }
    }
}
