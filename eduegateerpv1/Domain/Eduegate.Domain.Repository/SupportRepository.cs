using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Supports;
using Eduegate.Domain.Entity.Supports.Models;
using Eduegate.Framework.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Repository
{
    public class SupportRepository
    {
        public Ticket GetTicket(long ticketID)
        {
            using (var dbContext = new dbEduegateSupportContext())
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
            if (entity.IsNotNull())
            {
                using (var dbContext = new dbEduegateSupportContext())
                {
                    dbContext.Tickets.Add(entity);

                    if (entity.TicketIID <= 0)
                        dbContext.Entry(entity).State = EntityState.Added;
                    else
                        dbContext.Entry(entity).State = EntityState.Modified;


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
                                dbContext.Entry(ticketProductmap).State = EntityState.Added;
                            else
                                dbContext.Entry(ticketProductmap).State = EntityState.Modified;
                        }
                    }


                    // Add/Update TicketActionDetails
                    if (entity.TicketActionDetailMaps.IsNotNull() && entity.TicketActionDetailMaps.Count > 0)
                    {
                        foreach (TicketActionDetailMap ticketActionDetailmap in entity.TicketActionDetailMaps)
                        {
                            if (ticketActionDetailmap.TicketActionDetailIID <= 0)
                                dbContext.Entry(ticketActionDetailmap).State = EntityState.Added;
                            else
                                dbContext.Entry(ticketActionDetailmap).State = EntityState.Modified;

                            // Add/Update TicketActionDetailDetailMap
                            if (ticketActionDetailmap.TicketActionDetailDetailMaps.IsNotNull() && ticketActionDetailmap.TicketActionDetailDetailMaps.Count > 0)
                            {
                                foreach (var ticketActionDetailDetailMap in ticketActionDetailmap.TicketActionDetailDetailMaps)
                                {

                                    if (ticketActionDetailDetailMap.TicketActionDetailDetailMapIID <= 0)
                                        dbContext.Entry(ticketActionDetailDetailMap).State = EntityState.Added;
                                    else
                                        dbContext.Entry(ticketActionDetailDetailMap).State = EntityState.Modified;
                                }
                            }
                        }
                    }
                    else if (entity.TicketIID <= 0)
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

            return entity;
        }

        public TicketStatus GetTicketStatusByStatusID(int statusID)
        {
            using (var db = new dbEduegateSupportContext())
            {
                return db.TicketStatuses.FirstOrDefault(x => x.TicketStatusID == statusID);
            }
        }

        public SupportAction GetTicketActionByActionID(int actionID)
        {
            using (var db = new dbEduegateSupportContext())
            {
                return db.SupportActions.FirstOrDefault(x => x.SupportActionID == actionID);
            }
        }

        public List<SupportAction> GetTicketActions()
        {
            using (var db = new dbEduegateSupportContext())
            {
                return db.SupportActions.ToList();
            }
        }

        public List<TicketPriority> GetTicketPriorities()
        {
            using (var dbContext = new dbEduegateSupportContext())
            {
                return dbContext.TicketPriorities.OrderBy(a => a.PriorityName).ToList();
            }
        }

        public List<SupportAction> GetSupportActions(int actionTypeID = 0)
        {
            using (var dbContext = new dbEduegateSupportContext())
            {
                if (actionTypeID > 0)
                {
                    return dbContext.SupportActions.Where(a => a.ActionTypeID == actionTypeID).OrderBy(a => a.ActionName).ToList();
                }
                else
                {
                    return dbContext.SupportActions.Where(a => a.ActionTypeID == null).OrderBy(a => a.ActionName).ToList();
                }
            }
        }

        public List<TicketStatus> GetTicketStatuses()
        {
            using (var dbContext = new dbEduegateSupportContext())
            {
                return dbContext.TicketStatuses.OrderBy(a => a.StatusName).ToList();
            }
        }

        public TicketPriority GetTicketPriority(int ticketPriorityID)
        {
            using (var dbContext = new dbEduegateSupportContext())
            {
                return dbContext.TicketPriorities.Where(x => x.TicketPriorityID == ticketPriorityID).FirstOrDefault();
            }
        }

        public SupportAction GetSupportAction(int supportActionID)
        {
            using (var dbContext = new dbEduegateSupportContext())
            {
                return dbContext.SupportActions.Where(x => x.SupportActionID == supportActionID).FirstOrDefault();
            }
        }

        public TicketStatus GetTicketStatus(int ticketStatusID)
        {
            using (var dbContext = new dbEduegateSupportContext())
            {
                return dbContext.TicketStatuses.Where(x => x.TicketStatusID == ticketStatusID).FirstOrDefault();
            }
        }

        public List<TicketReason> GetTicketReasons()
        {
            using (var dbContext = new dbEduegateSupportContext())
            {
                return dbContext.TicketReasons.OrderBy(a => a.TicketReasonName).ToList();
            }
        }

        public bool AddCustomerSupportTicket(CustomerSupportTicket ticket)
        {
            using (var db = new dbEduegateSupportContext())
            {
                db.CustomerSupportTickets.Add(ticket);
                db.SaveChanges();
                return true;
            }
        }

        public bool JustAskInsert(CustomerJustAsk justAsk)
        {
            using (var db = new dbEduegateSupportContext())
            {
                db.CustomerJustAsks.Add(justAsk);
                db.SaveChanges();
                return true;
            }
        }

        public bool DeleteTicketProductMaps(List<TicketProductMap> ticketProductMaps)
        {
            using (var db = new dbEduegateSupportContext())
            {
                db.TicketProductMaps.RemoveRange(ticketProductMaps);
                db.SaveChanges();
                return true;
            }
        }

        public bool JobInsert(Entity.Models.UserJobApplication application)
        {
            try
            {
                using (var db = new Entity.dbEduegateERPContext())
                {
                    db.UserJobApplications.Add(application);
                    db.SaveChanges();
                    return true;
                }

            }
            catch (Exception ex) { return false; }
        }

    }
}
