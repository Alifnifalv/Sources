using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Extensions;

namespace Eduegate.Domain.Repository
{
    public class SupportRepository
    {
        public Ticket GetTicket(long ticketID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var ticket = dbContext.Tickets.Where(x => x.TicketIID == ticketID).FirstOrDefault();

                if (ticket.IsNotNull())
                {
                    dbContext.Entry(ticket).Collection(t => t.TicketProductMaps).Load();
                    dbContext.Entry(ticket).Collection(t => t.TicketActionDetailMaps).Load();

                    // loading action detail detail maps
                    if (ticket.TicketActionDetailMaps.IsNotNull() && ticket.TicketActionDetailMaps.Count > 0)
                    {
                        foreach (var actionDetail in ticket.TicketActionDetailMaps)
                        {
                            if (actionDetail.TicketActionDetailDetailMaps.IsNotNull() && actionDetail.TicketActionDetailDetailMaps.Count > 0)
                            {
                                dbContext.Entry(actionDetail).Reference(a => a.TicketActionDetailDetailMaps).Load();
                            }
                        }
                    }

                    // Loading ticket product maps
                    if (ticket.TicketProductMaps.IsNotNull() && ticket.TicketProductMaps.Count > 0)
                    {
                        foreach (var map in ticket.TicketProductMaps)
                        {
                            dbContext.Entry(map).Reference(m => m.ProductSKUMap).Load();
                            dbContext.Entry(map).Reference(m => m.Product).Load();
                        }

                    }
                }

                return ticket;
            }
        }

        public Ticket SaveTicket(Ticket entity)
        {
            try
            {
                if (entity.IsNotNull())
                {
                    using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                    {
                        dbContext.Tickets.Add(entity);

                        if (entity.TicketIID <= 0)
                            dbContext.Entry(entity).State = System.Data.Entity.EntityState.Added;
                        else
                            dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;


                        // Delete user-removed ticketMaps
                        var ticketProductMapIDs = entity.TicketProductMaps.Select(t => t.TicketProductMapIID).ToList();

                        var ticketMaps = dbContext.TicketProductMaps.Where(t => t.TicketID == entity.TicketIID && !ticketProductMapIDs.Contains(t.TicketProductMapIID)).ToList();
                        if (ticketMaps.IsNotNull() && ticketMaps.Count > 0)
                            dbContext.TicketProductMaps.RemoveRange(ticketMaps);


                        // Add/Update TicketProductMaps
                        if (entity.TicketProductMaps.IsNotNull() && entity.TicketProductMaps.Count > 0)
                        {
                            foreach (TicketProductMap ticketProductmap in entity.TicketProductMaps)
                            {
                                if (ticketProductmap.TicketProductMapIID <= 0)
                                    dbContext.Entry(ticketProductmap).State = System.Data.Entity.EntityState.Added;
                                else
                                    dbContext.Entry(ticketProductmap).State = System.Data.Entity.EntityState.Modified;
                            }
                        }


                        // Add/Update TicketActionDetails
                        if (entity.TicketActionDetailMaps.IsNotNull() && entity.TicketActionDetailMaps.Count > 0)
                        {
                            foreach (TicketActionDetailMap ticketActionDetailmap in entity.TicketActionDetailMaps)
                            {
                                if (ticketActionDetailmap.TicketActionDetailIID <= 0)
                                    dbContext.Entry(ticketActionDetailmap).State = System.Data.Entity.EntityState.Added;
                                else
                                    dbContext.Entry(ticketActionDetailmap).State = System.Data.Entity.EntityState.Modified;

                                // Add/Update TicketActionDetailDetailMap
                                if (ticketActionDetailmap.TicketActionDetailDetailMaps.IsNotNull() && ticketActionDetailmap.TicketActionDetailDetailMaps.Count > 0)
                                {
                                    foreach (var ticketActionDetailDetailMap in ticketActionDetailmap.TicketActionDetailDetailMaps)
                                    {

                                        if (ticketActionDetailDetailMap.TicketActionDetailDetailMapIID <= 0)
                                            dbContext.Entry(ticketActionDetailDetailMap).State = System.Data.Entity.EntityState.Added;
                                        else
                                            dbContext.Entry(ticketActionDetailDetailMap).State = System.Data.Entity.EntityState.Modified;
                                    }
                                }
                            }
                        }
                        else if(entity.TicketIID <= 0)
                        {
                            var Customer = entity.CustomerID.IsNotNull() ? dbContext.Customers.Where(x => x.LoginID == entity.CustomerID).FirstOrDefault() : null;
                            entity.CustomerID = Customer.IsNotNull() ? Customer.CustomerIID : (long?)null;
                            dbContext.Tickets.Add(entity); //from website 
                        }

                        dbContext.SaveChanges();

                        long ticketIID = entity.TicketIID;

                        entity = new Ticket();
                        entity = dbContext.Tickets.Where(x => x.TicketIID == ticketIID).FirstOrDefault();

                        if (entity.IsNotNull())
                            dbContext.Entry(entity).Collection(a => a.TicketProductMaps).Load();
                        dbContext.Entry(entity).Reference(a => a.TicketStatus).Load();
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

        public List<SupportAction> GetTicketActions()
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.SupportActions.ToList();
            }
        }

        public List<TicketPriority> GetTicketPriorities()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.TicketPriorities.OrderBy(a=> a.PriorityName).ToList();
            }
        }

        public List<SupportAction> GetSupportActions(int actionTypeID = 0)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (actionTypeID > 0)
                {
                    return dbContext.SupportActions.Where(a => a.ActionTypeID == actionTypeID).OrderBy(a=> a.ActionName).ToList();
                }
                else
                {
                    return dbContext.SupportActions.Where(a => a.ActionTypeID == null).OrderBy(a => a.ActionName).ToList();
                }
            }
        }

        public List<TicketStatus> GetTicketStatuses()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.TicketStatuses.OrderBy(a=> a.StatusName).ToList();
            }
        }

        public TicketPriority GetTicketPriority(int ticketPriorityID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.TicketPriorities.Where(x => x.TicketPriorityID == ticketPriorityID).FirstOrDefault();
            }
        }

        public SupportAction GetSupportAction(int supportActionID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.SupportActions.Where(x => x.SupportActionID == supportActionID).FirstOrDefault();
            }
        }

        public TicketStatus GetTicketStatus(int ticketStatusID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.TicketStatuses.Where(x => x.TicketStatusID == ticketStatusID).FirstOrDefault();
            }
        }

        public List<TicketReason> GetTicketReasons()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.TicketReasons.OrderBy(a=> a.TicketReasonName).ToList();
            }
        }

        public long GetProductID(long skuID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var productSku = dbContext.ProductSKUMaps.Where(p => p.ProductSKUMapIID == skuID).FirstOrDefault();

                return Convert.ToInt32(productSku.ProductID);
            }
        }

        public bool AddCustomerSupportTicket(CustomerSupportTicket ticket)
        {
            try
            {
                using (dbEduegateERPContext db = new dbEduegateERPContext())
                {
                    db.CustomerSupportTickets.Add(ticket);
                    db.SaveChanges();
                    return true;
                }

            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                return false;
            }
            catch (Exception ex) { return false; }
        }

        public bool JustAskInsert(CustomerJustAsk justAsk)
        {
            try
            {
                using (dbEduegateERPContext db = new dbEduegateERPContext())
                {
                    db.CustomerJustAsks.Add(justAsk);
                    db.SaveChanges();
                    return true;
                }

            }
            catch (Exception ex) { return false; }
        }

        public bool JobInsert(UserJobApplication application)
        {
            try
            {
                using (dbEduegateERPContext db = new dbEduegateERPContext())
                {
                    db.UserJobApplications.Add(application);
                    db.SaveChanges();
                    return true;
                }

            }
            catch (Exception ex) { return false; }
        }

        public bool DeleteTicketProductMaps(List<TicketProductMap> ticketProductMaps)
        {
            try
            {
                using (dbEduegateERPContext db = new dbEduegateERPContext())
                {
                    db.TicketProductMaps.RemoveRange(ticketProductMaps);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<SupportRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }

        }

    }
}
