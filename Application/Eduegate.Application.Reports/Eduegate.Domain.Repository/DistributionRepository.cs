using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Enums;
using System.Data.Entity;

namespace Eduegate.Domain.Repository
{
    public class DistributionRepository
    {

        #region Route

        /// <summary>
        /// Insert and Update the Route entity based on RouteID
        /// </summary>
        /// <param name="Route">object of Route</param>
        /// <returns>object of Route</returns>
        public Route SaveRoute(Route route)
        {

            if (route.IsNotNull())
            {
                using (dbEduegateERPContext db = new dbEduegateERPContext())
                {
                    // if routeId is 0 the get MaxID + 1
                    if (route.RouteID == 0)
                    {
                        var routeId = db.Routes.Max(x => (int?)x.RouteID);
                        route.RouteID = routeId.HasValue ? routeId.Value + 1 : 1;
                    }

                    if (db.Routes.Where(x => x.RouteID == route.RouteID).Any() == false) //Checking records exist or not from route table to create/update
                        db.Entry(route).State = System.Data.Entity.EntityState.Added;       // Insert
                    else
                        db.Entry(route).State = System.Data.Entity.EntityState.Modified;    // Update

                    db.SaveChanges();
                    if (route != null)
                    {
                        db.Entry(route).Reference(a => a.Country).Load();
                    }
                }
            }

            return route;
        }


        /// <summary>
        /// Get the Route detail based on RouteID
        /// </summary>
        /// <param name="route">object of Route</param>
        /// <returns>object of Route</returns>
        public Route GetRoute(long routeID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var route = db.Routes.Where(x => x.RouteID == routeID).FirstOrDefault();
                if (route != null)
                {
                    db.Entry(route).Reference(a => a.Country).Load();
                }
                return route;
            }
        }

        public List<Route> GetRoutes(int? companyID = 1)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.Routes.Where(x => x.CompanyID == companyID).OrderBy(a => a.Description).ToList();
            }
        }

        #endregion

        #region Service Provider

        public ServiceProvider SaveServiceProvider(ServiceProvider entity)
        {
            try
            {
                if (entity.IsNotNull())
                {
                    using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                    {
                        if (dbContext.ServiceProviders.Where(a => a.ServiceProviderID == entity.ServiceProviderID).Any() == false) //Checking records exists or not from service providers table to created/Update
                            dbContext.Entry(entity).State = System.Data.Entity.EntityState.Added;  //Insert Service Provider
                        else
                            dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified; //Update Service Provider

                        dbContext.SaveChanges();
                    }
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<DistributionRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }

            return entity;
        }

        public ServiceProvider GetServiceProvider(int serviceProviderID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.ServiceProviders.Where(a => a.ServiceProviderID == serviceProviderID).FirstOrDefault();
            }
        }

        public List<ServiceProvider> GetServiceProviders()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.ServiceProviders.OrderBy(a => a.ProviderName).ToList();
            }
        }

        public List<DeliveryTypeStatus> GetDeliveryTypeStatuses()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.DeliveryTypeStatuses.OrderBy(a => a.StatusName).ToList();
            }
        }

        public List<DeliveryTypes1> GetDeliveryTypes()
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                return dbContext.DeliveryTypes1.OrderBy(a => a.DeliveryTypeName).ToList();
            }
        }

        public DeliveryTypes1 GetDeliverySettings(int deliveryTypeID, bool isReferenceData = true)
        {
            var deliveryType = new DeliveryTypes1();

            using (var dbContext = new dbEduegateERPContext())
            {
                try
                {
                    dbContext.Configuration.LazyLoadingEnabled = false;
                    deliveryType = dbContext.DeliveryTypes1.Where(a => a.DeliveryTypeID == deliveryTypeID)
                                            .Select(b => new
                                            {
                                                b,
                                                DeliveryTypeTimeSlotMaps = b.DeliveryTypeTimeSlotMaps.ToList(),
                                                DeliveryTypeAllowedAreaMaps = b.DeliveryTypeAllowedAreaMaps.ToList(),
                                                CustomerGroupDeliveryTypeMaps = b.CustomerGroupDeliveryTypeMaps.ToList(),
                                                ProductDeliveryTypeMaps = b.ProductDeliveryTypeMaps
                                                .Where(p => p.Product.StatusID == (byte)ProductStatuses.Active).Take(100).ToList(),
                                            }).AsEnumerable().Select(x => x.b).FirstOrDefault();

                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return deliveryType;
            }
        }

        public List<DeliveryTypeAllowedAreaMap> GetAreaDeliverySettings(int areaID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                return dbContext.DeliveryTypeAllowedAreaMaps.Where(a => a.AreaID == areaID).ToList();
            }
        }

        public List<DeliveryTypeAllowedZoneMap> GetZoneDeliveryTypes(short zoneID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var ZoneDTMaps = new List<DeliveryTypeAllowedZoneMap>();

                if (zoneID > 0)
                {
                    ZoneDTMaps = (from dtzmaps in dbContext.DeliveryTypeAllowedZoneMaps
                                  where dtzmaps.ZoneID == zoneID
                                  select dtzmaps).Include(x => x.DeliveryTypes1).ToList();
                }

                return ZoneDTMaps;
            }
        }

        public DeliveryTypeAllowedZoneMap GetDeliveryTypeByAllowedZoneMaps(short zoneID, long deliveryTypeID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var dtzmaps = (from dtamaps in dbContext.DeliveryTypeAllowedZoneMaps
                               where dtamaps.ZoneID == zoneID && dtamaps.DeliveryTypeID == deliveryTypeID
                               select dtamaps).FirstOrDefault();
                return dtzmaps;
            }
        }
        public List<DeliveryTypeAllowedAreaMap> GetAreaDeliveryTypes(int areaID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var AreaDTMaps = new List<DeliveryTypeAllowedAreaMap>();

                if (areaID > 0)
                {
                    AreaDTMaps = (from dtamaps in dbContext.DeliveryTypeAllowedAreaMaps
                                  where dtamaps.AreaID == areaID
                                  select dtamaps).Include(x => x.DeliveryTypes1).ToList();
                }

                return AreaDTMaps;
            }
        }

        public DeliveryTypeAllowedAreaMap GetDeliveryTypeByAreaMaps(long areaID, long deliveryTypeID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var dtzchildmaps = (from dtareamaps in dbContext.DeliveryTypeAllowedAreaMaps
                                    where dtareamaps.AreaID == areaID && dtareamaps.DeliveryTypeID == deliveryTypeID
                                    select dtareamaps).FirstOrDefault();
                return dtzchildmaps;
            }
        }

        public DeliveryTypes1 SaveDeliverySettings(DeliveryTypes1 entity)
        {
            List<long> ids = new List<long>();
            try
            {
                if (entity.IsNotNull())
                {
                    using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                    {

                        //dbContext.DeliveryTypes1.Add(entity);

                        var deliveryTypeDB = dbContext.DeliveryTypes1.Include(a => a.DeliveryTypeTimeSlotMaps).FirstOrDefault(a => a.DeliveryTypeID == entity.DeliveryTypeID);
                        var timeSlotinDB = deliveryTypeDB.DeliveryTypeTimeSlotMaps.ToList();
                        if (entity.DeliveryTypeTimeSlotMaps.IsNotNull())
                        {
                            var deliveryTypesTimeSlot = dbContext.DeliveryTypeTimeSlotMaps.Where(c => c.DeliveryTypeID == entity.DeliveryTypeID);
                            //if (deliveryTypesTimeSlot.IsNotNull())
                            //    dbContext.DeliveryTypeTimeSlotMaps.RemoveRange(deliveryTypesTimeSlot);
                            if (entity.DeliveryTypeTimeSlotMaps.IsNotNull() && entity.DeliveryTypeTimeSlotMaps.Count > 0)
                            {

                                if (dbContext.DeliveryTypes1.Where(a => a.DeliveryTypeID == entity.DeliveryTypeID).Any() == false) //Checking records exists or not from service providers table to created/Update
                                {
                                    dbContext.DeliveryTypes1.Add(entity);
                                    dbContext.Entry(entity).State = System.Data.Entity.EntityState.Added;  //Insert Service Provider
                                }
                                else
                                {

                                    //dbContext.DeliveryTypes1.Attach(entity);
                                    //dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified; //Update Service Provider
                                    dbContext.Entry(deliveryTypeDB).CurrentValues.SetValues(entity);
                                }
                                //foreach (var item in entity.DeliveryTypeTimeSlotMaps.ToList())
                                //{
                                //    //dbContext.DeliveryTypeTimeSlotMaps.Add(item);
                                //    if (dbContext.DeliveryTypeTimeSlotMaps.Where(a => a.DeliveryTypeTimeSlotMapIID == item.DeliveryTypeTimeSlotMapIID).Any() == false)
                                //    {
                                //        dbContext.DeliveryTypeTimeSlotMaps.Add(item);
                                //        item.DeliveryTypeTimeSlotMapIID = 0;
                                //        dbContext.Entry(item).State = System.Data.Entity.EntityState.Added;
                                //    }
                                //    else
                                //    {
                                //        if (ids.Contains(item.DeliveryTypeTimeSlotMapIID))
                                //        {
                                //            item.DeliveryTypeTimeSlotMapIID = 0;
                                //            dbContext.DeliveryTypeTimeSlotMaps.Add(item);

                                //            //dbContext.Entry(item).State = System.Data.Entity.EntityState.Added;
                                //        }
                                //        else
                                //        {
                                //            //dbContext.DeliveryTypeTimeSlotMaps.Attach(item);
                                //            ids.Add(item.DeliveryTypeTimeSlotMapIID);
                                //            //dbContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
                                //            dbContext.Entry(item).CurrentValues.SetValues(item);
                                //        }

                                //    }

                                //}
                                foreach (var timeSlot in entity.DeliveryTypeTimeSlotMaps)
                                {
                                    if (ids.Contains(timeSlot.DeliveryTypeTimeSlotMapIID))
                                    {
                                        timeSlot.DeliveryTypeTimeSlotMapIID = 0;
                                    }
                                    else
                                    {
                                        ids.Add(timeSlot.DeliveryTypeTimeSlotMapIID);
                                    }
                                }
                                foreach (var timeSlotDB in timeSlotinDB)
                                {
                                    var timeSlot = entity.DeliveryTypeTimeSlotMaps.FirstOrDefault(a => a.DeliveryTypeTimeSlotMapIID == timeSlotDB.DeliveryTypeTimeSlotMapIID && a.DeliveryTypeTimeSlotMapIID != 0);
                                    if (timeSlot.IsNotNull())
                                    {
                                        dbContext.Entry(timeSlotDB).CurrentValues.SetValues(timeSlot);
                                    }
                                    else
                                    {
                                        dbContext.DeliveryTypeTimeSlotMapsCultures.RemoveRange(dbContext.DeliveryTypeTimeSlotMapsCultures.Where(a => a.DeliveryTypeTimeSlotMapID == timeSlotDB.DeliveryTypeTimeSlotMapIID));
                                        dbContext.DeliveryTypeTimeSlotMaps.Remove(timeSlotDB);
                                    }
                                }

                                foreach(var timeSlot in entity.DeliveryTypeTimeSlotMaps)
                                {
                                    if (!timeSlotinDB.Any(i => i.DeliveryTypeTimeSlotMapIID == timeSlot.DeliveryTypeTimeSlotMapIID))
                                        // Yes: Add it as a new child
                                        dbContext.DeliveryTypeTimeSlotMaps.Add(timeSlot);
                                }

                                dbContext.SaveChanges();

                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<DistributionRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }

            return entity;
        }

        public List<ServiceProviderSetting> GetServiceProviderSettings(long serviceProviderID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.ServiceProviderSettings.Where(a => a.ServiceProviderID == serviceProviderID).ToList();
            }
        }

        public void SaveServiceProviderLogs(ServiceProviderLog log)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                dbContext.ServiceProviderLogs.Add(log);
                dbContext.SaveChanges();
            }
        }

        public void SendPickupRequest(OrderTrackingDTO dto)
        {

        }

        public List<ProductTypeDeliveryTypeMap> GetProductTypeDeliveryTypes(long productID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var productTypeDTMaps = new List<ProductTypeDeliveryTypeMap>();

                if (productID > 0)
                {
                    productTypeDTMaps = (from p in dbContext.Products
                                         join ptdtm in dbContext.ProductTypeDeliveryTypeMaps on p.ProductTypeID equals ptdtm.ProductTypeID
                                         where p.ProductIID == productID
                                         select ptdtm).Include(x => x.DeliveryTypes1).Include(y => y.ProductType).Include(z => z.Company).ToList();
                }

                return productTypeDTMaps;
            }
        }

        public List<ProductTypeDeliveryTypeMap> GetProductSKUTypeDeliveryTypes(long skuID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var productSKUTypeDTMaps = new List<ProductTypeDeliveryTypeMap>();

                if (skuID > 0)
                {
                    productSKUTypeDTMaps = (from p in dbContext.Products
                                            join s in dbContext.ProductSKUMaps on p.ProductIID equals s.ProductID
                                            join ptdtm in dbContext.ProductTypeDeliveryTypeMaps on p.ProductTypeID equals ptdtm.ProductTypeID
                                            where s.ProductSKUMapIID == skuID
                                            select ptdtm).Include(x => x.DeliveryTypes1).Include(y => y.ProductType).Include(z => z.Company).ToList();
                }

                return productSKUTypeDTMaps;
            }
        }

        public ProductDeliveryTypeMap GetProductDeliveryTypeByProductID(long productID, long deliveryTypeID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var pdtMap = (from pmap in dbContext.ProductDeliveryTypeMaps
                              where pmap.ProductID == productID && pmap.DeliveryTypeID == deliveryTypeID
                              select pmap).FirstOrDefault();

                return pdtMap;
            }
        }

        public ProductDeliveryTypeMap GetProductDeliveryTypeBySkuID(long skuID, long deliveryTypeID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var pdtMap = (from pmap in dbContext.ProductDeliveryTypeMaps
                              where pmap.ProductSKUMapID == skuID && pmap.DeliveryTypeID == deliveryTypeID
                              select pmap).FirstOrDefault();

                return pdtMap;
            }
        }

        public CustomerGroupDeliveryTypeMap GetProductDeliveryTypeByCustomerGroupID(long customergroupID, long deliveryTypeID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var pdtMap = (from cgdtmap in dbContext.CustomerGroupDeliveryTypeMaps
                              where cgdtmap.CustomerGroupID == customergroupID && cgdtmap.DeliveryTypeID == deliveryTypeID
                              join dt in dbContext.DeliveryTypes1 on cgdtmap.DeliveryTypeID equals dt.DeliveryTypeID
                              select cgdtmap).FirstOrDefault();
                return pdtMap;
            }
        }
        public List<CustomerGroupDeliveryTypeMap> GetProductDeliveryTypeByCustomerGroupID(long customergroupID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var CustomerGroupDTMaps = new List<CustomerGroupDeliveryTypeMap>();
                if (customergroupID > 0)
                {
                    CustomerGroupDTMaps = (from cg in dbContext.CustomerGroups
                                           join cgdtm in dbContext.CustomerGroupDeliveryTypeMaps on cg.CustomerGroupIID equals cgdtm.CustomerGroupID
                                           //join dt in dbContext.DeliveryTypes1 on cgdtm.DeliveryTypeID equals dt.DeliveryTypeID
                                           where cg.CustomerGroupIID == customergroupID
                                           select cgdtm).Include(x => x.DeliveryTypes1).Include(y => y.CustomerGroup).ToList();
                }
                return CustomerGroupDTMaps;
            }
        }


        public List<ProductDeliveryTypeMap> GetProductDeliveryTypeMaps(long IID, bool isProduct, long branchId)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (isProduct == true)
                    return dbContext.ProductDeliveryTypeMaps.Where(x => x.ProductID == IID).ToList();
                else
                    return dbContext.ProductDeliveryTypeMaps.Where(x => x.ProductSKUMapID == IID && x.BranchID == branchId).ToList();
            }
        }
        public List<ProductDeliveryTypeMap> GetProductDeliveryTypeMaps(long IID, bool isProduct, int companyID = 0)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (isProduct == true)
                    return dbContext.ProductDeliveryTypeMaps.Where(x => x.ProductID == IID).ToList();
                else
                    return dbContext.ProductDeliveryTypeMaps.Where(x => x.ProductSKUMapID == IID && x.CompanyID == companyID).ToList();
            }
        }

        public string GetDeliveryTypeName(int deliveryTypeID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var deliveryType = dbContext.DeliveryTypes1.Where(x => x.DeliveryTypeID == deliveryTypeID).FirstOrDefault();

                if (deliveryType.IsNotNull())
                    return deliveryType.DeliveryTypeName;
                else
                    return string.Empty;
            }
        }


        public ProductDeliveryTypeMap GetBranchBySkuID(long skuID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.ProductDeliveryTypeMaps.Where(x => x.ProductSKUMapID == skuID).FirstOrDefault();
            }
        }

        public bool SaveProductDeliveryTypeMaps(List<ProductDeliveryTypeMap> pdtMaps, long productID, List<ProductDeliveryTypeMap> deleteEntities)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    if (deleteEntities.IsNotNull() && deleteEntities.Count > 0)
                    {
                        foreach (var delete in deleteEntities)
                        {
                            var pdtm = dbContext.ProductDeliveryTypeMaps.Where(x => x.ProductDeliveryTypeMapIID == delete.ProductDeliveryTypeMapIID).FirstOrDefault();
                            dbContext.ProductDeliveryTypeMaps.Remove(pdtm);
                        }

                        dbContext.SaveChanges();
                    }
                    if (pdtMaps.IsNotNull() && pdtMaps.Count > 0)
                    {
                        foreach (var entity in pdtMaps)
                        {
                            if (entity.ProductDeliveryTypeMapIID == 0)
                            {
                                dbContext.ProductDeliveryTypeMaps.Add(entity);
                                dbContext.Entry(entity).State = EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(entity).State = EntityState.Modified;
                            }
                        }
                    }

                    var distinctSKUList = pdtMaps.Select(x=>x.ProductSKUMapID).Distinct().ToList();
                    if (distinctSKUList.IsNotNull())
                    {
                        dbContext.ProductSKUMaps.Where(x => distinctSKUList.Contains(x.ProductSKUMapIID)).ToList().ForEach(x => x.UpdatedDate = DateTime.Now);
                    }
                    dbContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<DistributionRepository>.Fatal(ex.Message.ToString(), ex);
                throw ex;
            }
        }

        #endregion


        #region Zone Delivery Types

        public List<DeliveryTypeAllowedZoneMap> GetZoneDeliveryTypeMaps(short zoneID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.DeliveryTypeAllowedZoneMaps.Where(x => x.ZoneID == zoneID).ToList();
            }
        }

        public bool SaveZoneDeliveryTypeMaps(List<DeliveryTypeAllowedZoneMap> zoneMaps, long zoneID, List<DeliveryTypeAllowedZoneMap> deleteEntities)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    if (deleteEntities.IsNotNull() && deleteEntities.Count > 0)
                    {
                        foreach (var delete in deleteEntities)
                        {
                            var pdtm = dbContext.DeliveryTypeAllowedZoneMaps.Where(x => x.ZoneDeliveryTypeMapIID == delete.ZoneDeliveryTypeMapIID).FirstOrDefault();
                            dbContext.DeliveryTypeAllowedZoneMaps.Remove(pdtm);
                        }

                        dbContext.SaveChanges();
                    }
                    if (zoneMaps.IsNotNull() && zoneMaps.Count > 0)
                    {
                        foreach (var entity in zoneMaps)
                        {
                            if (entity.ZoneDeliveryTypeMapIID <= 0)
                            {
                                dbContext.DeliveryTypeAllowedZoneMaps.Add(entity);
                                dbContext.Entry(entity).State = EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(entity).State = EntityState.Modified;
                            }
                        }

                        dbContext.SaveChanges();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<DistributionRepository>.Fatal(ex.Message.ToString(), ex);
                throw ex;
            }
        }

        #endregion

        #region Area Delivery Types

        public List<DeliveryTypeAllowedAreaMap> GetAreaDeliveryTypeMaps(int areaID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.DeliveryTypeAllowedAreaMaps.Where(x => x.AreaID == areaID).ToList();
            }
        }

        public bool SaveAreaDeliveryTypeMaps(List<DeliveryTypeAllowedAreaMap> areaMaps, int areaID, List<DeliveryTypeAllowedAreaMap> deleteEntities)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    if (deleteEntities.IsNotNull() && deleteEntities.Count > 0)
                    {
                        foreach (var delete in deleteEntities)
                        {
                            var pdtm = dbContext.DeliveryTypeAllowedAreaMaps.Where(x => x.AreaDeliveryTypeMapIID == delete.AreaDeliveryTypeMapIID).FirstOrDefault();
                            dbContext.DeliveryTypeAllowedAreaMaps.Remove(pdtm);
                        }

                        dbContext.SaveChanges();
                    }
                    if (areaMaps.IsNotNull() && areaMaps.Count > 0)
                    {
                        foreach (var entity in areaMaps)
                        {
                            if (entity.AreaDeliveryTypeMapIID <= 0)
                            {
                                dbContext.DeliveryTypeAllowedAreaMaps.Add(entity);
                                dbContext.Entry(entity).State = EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(entity).State = EntityState.Modified;
                            }
                        }

                        dbContext.SaveChanges();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<DistributionRepository>.Fatal(ex.Message.ToString(), ex);
                throw ex;
            }
        }

        #endregion

        #region CustomerGroup Delivery Types

        public List<CustomerGroupDeliveryTypeMap> GetCustomerGroupDeliveryTypeMaps(long customerGroupID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.CustomerGroupDeliveryTypeMaps.Where(x => x.CustomerGroupID == customerGroupID).ToList();
            }
        }

        public bool SaveCustomerGroupDeliveryTypeMaps(List<CustomerGroupDeliveryTypeMap> cgMaps, long customerGroupID, List<CustomerGroupDeliveryTypeMap> deleteEntities)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    if (deleteEntities.IsNotNull() && deleteEntities.Count > 0)
                    {
                        foreach (var delete in deleteEntities)
                        {
                            var cgm = dbContext.CustomerGroupDeliveryTypeMaps.Where(x => x.CustomerGroupDeliveryTypeMapIID == delete.CustomerGroupDeliveryTypeMapIID).FirstOrDefault();
                            dbContext.CustomerGroupDeliveryTypeMaps.Remove(cgm);
                        }

                        dbContext.SaveChanges();
                    }
                    if (cgMaps.IsNotNull() && cgMaps.Count > 0)
                    {
                        foreach (var entity in cgMaps)
                        {
                            if (entity.CustomerGroupDeliveryTypeMapIID <= 0)
                            {
                                dbContext.CustomerGroupDeliveryTypeMaps.Add(entity);
                                dbContext.Entry(entity).State = EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(entity).State = EntityState.Modified;
                            }
                        }

                        dbContext.SaveChanges();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<DistributionRepository>.Fatal(ex.Message.ToString(), ex);
                throw ex;
            }
        }

        #endregion

        public List<ProductDeliveryTypeMap> GetBranchDeliveryTypeBySkuID(long skuID, int companyID = 0)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var pdtMap = (from pmap in dbContext.ProductDeliveryTypeMaps
                              where pmap.ProductSKUMapID == skuID && pmap.CompanyID == companyID
                              select pmap).ToList();

                return pdtMap;
            }
        }

        public string GetBranchName(long branchID)
        {
            string branch = string.Empty;
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                Branch detail = dbContext.Branches.Where(a => a.BranchIID == branchID).FirstOrDefault();

                if (detail.IsNotNull())
                    branch = detail.BranchName;
            }
            return branch;
        }

    }
}
