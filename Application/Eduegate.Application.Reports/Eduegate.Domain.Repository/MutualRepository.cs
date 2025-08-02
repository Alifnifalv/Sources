using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Entity.Models.Mutual;
using Eduegate.Domain.Entity.Models.Settings;
using Eduegate.Domain.Entity.Models.ValueObjects;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Logs;

namespace Eduegate.Domain.Repository
{
    public class MutualRepository
    {
        public AreaTree GetAreaTree(long nodeID)
        {
            var tree = new AreaTree();

            using (var db = new dbEduegateERPContext())
            {
                foreach (var category in db.Areas.Select(a => new { a.AreaID, a.AreaName }))
                {
                    var areaNode = new AreaTree() { AreaID = category.AreaID, AreaName = category.AreaName };
                    tree.AreaNodes.Add(areaNode);
                }
            }

            return tree;
        }

        public List<AreaTreeSearch> GetAreaTrees(long? parentID, string searchText)
        {
            using (var db = new dbEduegateERPContext())
            {
                if (string.IsNullOrEmpty(searchText))
                {
                    return db.AreaTreeSearches
                        .Where(x => x.ParentAreaID == parentID)
                        .ToList();
                }
                else
                {
                    if (parentID.HasValue)
                    {
                        return db.AreaTreeSearches
                               .Where(x => x.ParentAreaID == parentID
                               && x.TreePath.Contains(searchText)).ToList();
                    }
                    else
                    {
                        return db.AreaTreeSearches
                               .Where(x => x.TreePath.Contains(searchText)).ToList();
                    }
                }
            }
        }

        public GeoLocationLog GetLastGeoLocation(string type, string refrenceID1)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.GeoLocationLogs.Where(a => a.Type.ToLower() == type.ToLower() && a.ReferenceID1 == refrenceID1)
                    .OrderByDescending(a => a.CreatedDate)
                    .Take(1).FirstOrDefault();
            }
        }

        public GeoLocationLog SaveGeoLocation(GeoLocationLog entity)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                if (entity.GeoLocationLogIID == 0)
                    db.Entry(entity).State = System.Data.Entity.EntityState.Added;
                else
                    db.Entry(entity).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();
                return entity;
            }
        }

        public EntityTypeRelationMap SaveEntityTypeRelationMaps(EntityTypeRelationMap entity)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                if (entity.EntityTypeRelationMapsIID == 0)
                    db.Entry(entity).State = System.Data.Entity.EntityState.Added;
                else
                    db.Entry(entity).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();
                return entity;
            }
        }


        public bool RemoveEntityTypeRelationMaps(EntityTypeRelationMap entity)
        {
            bool isSuccess = false;
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                List<EntityTypeRelationMap> deleteEntity = db.EntityTypeRelationMaps
                    .Where(x => x.FromEntityTypeID == entity.FromEntityTypeID
                    && x.ToEntityTypeID == entity.ToEntityTypeID
                    && x.FromRelationID == entity.FromRelationID).ToList();

                if (deleteEntity != null)
                {
                    //db.Entry(deleteEntity).State = System.Data.Entity.EntityState.Deleted;
                    //NOTE: Remove will also remove the child objects, but using Deleted will not. You should really be using Remove for this very reason. If you really want to use Deleted, you'd have to make your foreign keys nullable, but then you'd end up with orphaned records (which is one of the main reasons you shouldn't be doing that in the first place).
                    db.EntityTypeRelationMaps.RemoveRange(deleteEntity);
                    db.SaveChanges();
                    isSuccess = true;
                }
                return isSuccess;
            }
        }

        public List<Employee> GetEmployeeIdNameEntityTypeRelation(EntityTypeRelationMap entity)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                List<Employee> lists = (from e in db.Employees
                                        join r in db.EntityTypeRelationMaps on e.EmployeeIID equals r.ToRelationID
                                        where r.FromEntityTypeID == entity.FromEntityTypeID
                                            && r.ToEntityTypeID == entity.ToEntityTypeID
                                            && r.FromRelationID == entity.FromRelationID
                                        select e).ToList();
                return lists;
            }
        }

        public List<EntityProperty> GetEntityPropertiesByType(int entityType)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                List<EntityProperty> lists = (from e in db.EntityProperties
                                              where e.EntityPropertyTypeID == entityType
                                              select e).ToList();
                return lists;
            }
        }

        public List<EntityPropertyMap> CreateEntityProperties(List<EntityPropertyMap> entityPropertyMaps)
        {
            var updateEntityPropertyMaps = new List<EntityPropertyMap>();

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    if (entityPropertyMaps.IsNotNull() && entityPropertyMaps.Count > 0)
                    {
                        foreach (EntityPropertyMap entityPropertyMap in entityPropertyMaps)
                        {
                            if (entityPropertyMap.EntityPropertyMapIID <= 0) //Insert
                            {
                                dbContext.EntityPropertyMaps.Add(entityPropertyMap);
                            }
                            else //Update
                            {
                                EntityPropertyMap epm = dbContext.EntityPropertyMaps.Where(x => x.EntityPropertyMapIID == entityPropertyMap.EntityPropertyMapIID).FirstOrDefault();

                                epm.EntityPropertyMapIID = entityPropertyMap.EntityPropertyMapIID;
                                epm.EntityPropertyTypeID = entityPropertyMap.EntityPropertyTypeID;
                                epm.EntityTypeID = entityPropertyMap.EntityTypeID;
                                epm.Value1 = entityPropertyMap.Value1;
                                epm.Value2 = entityPropertyMap.Value2;
                                epm.ReferenceID = entityPropertyMap.ReferenceID;
                                epm.Sequence = entityPropertyMap.Sequence;
                            }

                            dbContext.SaveChanges();
                            updateEntityPropertyMaps.Add(entityPropertyMap);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<MutualRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }

            return updateEntityPropertyMaps;
        }

        public List<EntityTypeEntitlement> GetEntityTypeEntitlementByEntityType(int entityType)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                List<EntityTypeEntitlement> entity = db.EntityTypeEntitlements.Where(x => x.EntityTypeID == entityType).ToList();
                return entity;
            }
        }

        public EntitlementMap SaveEntitlementMap(EntitlementMap entity)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                // Saving or accepting changes failed because more than one entity of type  have the same primary key value. Ensure that explicitly set primary key values are unique. Ensure that database-generated primary keys are configured correctly in the database and in the Entity Framework model. Use the Entity Designer for Database First/Model First configuration. Use the 'HasDatabaseGeneratedOption" fluent API or 'DatabaseGeneratedAttribute' for Code First configuration.
                var entityExist = db.EntitlementMaps.Where(x => x.ReferenceID == entity.ReferenceID && x.EntitlementID == entity.EntitlementID).FirstOrDefault();
                if (entityExist != null)
                {
                    entityExist.ReferenceID = entity.ReferenceID;
                    entityExist.IsLocked = entity.IsLocked;
                    entityExist.EntitlementAmount = entity.EntitlementAmount;
                    entityExist.EntitlementDays = entity.EntitlementDays;
                    entityExist.EntitlementID = entity.EntitlementID;
                    entityExist.UpdatedDate = entity.UpdatedDate;
                }
                else
                {
                    db.EntitlementMaps.Add(entity);
                }
                db.SaveChanges();
                return entity;
            }
        }

        public List<EntitlementMap> GetEntitlementMaps(EntitlementMap entity, short entityTypeID)
        {
            dbEduegateERPContext db = new dbEduegateERPContext();

            return (from em in db.EntitlementMaps
                    join ete in db.EntityTypeEntitlements on em.EntitlementID equals ete.EntitlementID
                    where em.ReferenceID == entity.ReferenceID && ete.EntityTypeID == entityTypeID
                    select em).ToList();

        }

        public EntityTypePaymentMethodMap SaveEntityTypePaymentMethodMap(EntityTypePaymentMethodMap entity)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {

                if (entity.EntityTypePaymentMethodMapIID > 0)
                {
                    // Update
                    var existEntity = db.EntityTypePaymentMethodMaps.Where(x => x.EntityTypePaymentMethodMapIID == entity.EntityTypePaymentMethodMapIID).FirstOrDefault();
                    existEntity.EntityTypeID = entity.EntityTypeID;
                    existEntity.PaymentMethodID = entity.PaymentMethodID;
                    existEntity.ReferenceID = entity.ReferenceID;
                    existEntity.EntityPropertyID = entity.EntityPropertyID;
                    existEntity.EntityPropertyTypeID = entity.EntityPropertyTypeID;
                    existEntity.AccountID = entity.AccountID;
                    existEntity.AccountName = entity.AccountName;
                    existEntity.BankName = entity.BankName;
                    existEntity.BankBranch = entity.BankBranch;
                    existEntity.IBANCode = entity.IBANCode;
                    existEntity.SWIFTCode = entity.SWIFTCode;
                    existEntity.IFSCCode = entity.IFSCCode;
                    existEntity.NameOnCheque = entity.NameOnCheque;
                    existEntity.UpdatedDate = entity.UpdatedDate;

                }
                else
                {
                    // Insert
                    db.EntityTypePaymentMethodMaps.Add(entity);
                }

                db.SaveChanges();
            }
            return entity;
        }

        public List<EntityTypePaymentMethodMap> GetEntityTypePaymentMethodMapByReferenceID(EntityTypePaymentMethodMap entity)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.EntityTypePaymentMethodMaps.Where(x => x.ReferenceID == entity.ReferenceID).ToList();
            }
        }

        public bool DeleteEntityTypePaymentMethodMapByReferenceID(EntityTypePaymentMethodMap entity)
        {
            bool isSuccess = false;
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var list = db.EntityTypePaymentMethodMaps.Where(x => x.ReferenceID == entity.ReferenceID).ToList();
                if (list.IsNull() || list.Count == 0)
                    return isSuccess;
                db.EntityTypePaymentMethodMaps.RemoveRange(list);
                db.SaveChanges();
                isSuccess = true;
                return isSuccess;
            }
        }

        public List<ProductPriceListCustomerMap> GetCustomerEntitlementPriceListMaps(long customerIID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.ProductPriceListCustomerMaps
                    .Where(m => m.CustomerID == customerIID)
                    .Include(m => m.EntityTypeEntitlement)
                    .Include(m => m.ProductPriceList)
                    .ToList();
            }
        }


        #region City

        /// <summary>
        /// Insert and Update the City entity based on CityID
        /// </summary>
        /// <param name="city">object of City</param>
        /// <returns>object of City</returns>
        public City SaveCity(City city)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                if (city.CityID == 0)
                {
                    var cityID = db.Cities.Max(x => (int?)x.CityID);
                    city.CityID = Convert.ToInt16((cityID.HasValue ? cityID.Value : 0) + 1);
                }

                bool isBool = db.Cities.Where(x => x.CityID == city.CityID).Any();
                if (isBool)
                {
                    // Update
                    db.Entry(city).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    // Insert
                    db.Entry(city).State = System.Data.Entity.EntityState.Added;
                }
                db.SaveChanges();

                return city;
            }
        }


        /// <summary>
        /// Get the City detail based on cityID
        /// </summary>
        /// <param name="city">object of City</param>
        /// <returns>object of City</returns>



        /// <summary>
        /// get the list of City
        /// </summary>
        /// <returns>list of City</returns>
        public List<City> GetCities()
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.Cities.OrderBy(a => a.CityName).ToList();
            }
        }

        #endregion


        #region Zone

        /// <summary>
        /// Insert and Update the Zone entity based on ZoneID
        /// </summary>
        /// <param name="zone">object of Zone</param>
        /// <returns>object of Zone</returns>
        public Zone SaveZone(Zone zone)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                if (zone.ZoneID == 0)
                {
                    var zoneId = db.Zones.Max(x => (int?)x.ZoneID);
                    zone.ZoneID = Convert.ToInt16((zoneId.HasValue ? zoneId.Value : 0) + 1);
                }

                if (db.Zones.Where(x => x.ZoneID == zone.ZoneID).Any() == false) //Checking records exist or not from route table to create/update
                {
                    db.Entry(zone).State = System.Data.Entity.EntityState.Added;       // Insert
                }
                else
                {
                    db.Entry(zone).State = System.Data.Entity.EntityState.Modified;    // Update
                }

                db.SaveChanges();

                if (zone != null)
                {
                    db.Entry(zone).Reference(a => a.Country).Load();
                }
            };

            return zone;
        }


        /// <summary>
        /// Get the Zone detail based on zoneID
        /// </summary>
        /// <param name="zone">object of Zone</param>
        /// <returns>object of Zone</returns>
        public Zone GetZone(long zoneID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var zone = db.Zones.Where(x => x.ZoneID == zoneID).FirstOrDefault();
                if (zone != null)
                {
                    db.Entry(zone).Reference(a => a.Country).Load();
                }
                return zone;
            }
        }

        /// <summary>
        /// get the list of Zone
        /// </summary>
        /// <returns>list of the Zone</returns>
        public List<Zone> GetZones(int? companyID = 0)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.Zones.Where(x => x.CompanyID == companyID).OrderBy(a => a.ZoneName).ToList();
            }
        }

        #endregion


        #region Area

        /// <summary>
        /// Insert and Update the Area entity based on AreaID
        /// </summary>
        /// <param name="Area">object of Area</param>
        /// <returns>object of Area</returns>
        public Area SaveArea(Area area)
        {

            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                // if routeId is 0 the get MaxID + 1
                if (area.AreaID == 0)
                {
                    var areaId = db.Areas.Max(x => (int?)x.AreaID);
                    area.AreaID = areaId.HasValue ? areaId.Value + 1 : 1;
                }

                if (db.Areas.Where(x => x.AreaID == area.AreaID).Any() == false) //Checking records exist or not from area table to create/update
                {
                    db.Entry(area).State = System.Data.Entity.EntityState.Added;       // Insert
                }
                else
                {
                    db.Entry(area).State = System.Data.Entity.EntityState.Modified;    // Update
                }



                db.SaveChanges();


                if (area != null)
                {
                    db.Entry(area).Reference(a => a.Country).Load();
                    db.Entry(area).Reference(a => a.City).Load();
                }

                return area;

            }
        }

        /// <summary>
        /// Get the Area detail based on AreaID
        /// </summary>
        /// <param name="area">object of Area</param>
        /// <returns>object of Area</returns>
        public Area GetArea(long areaID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var area = db.Areas.Where(x => x.AreaID == areaID).FirstOrDefault();
                if (area != null)
                {
                    db.Entry(area).Reference(a => a.Country).Load();
                    db.Entry(area).Reference(a => a.City).Load();
                }
                return area;
            }
        }

        public City GetCity(long cityID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.Cities.Where(x => x.CityID == cityID).FirstOrDefault();
            }
        }

        /// <summary>
        /// get the list of Area
        /// </summary>
        /// <returns>list of Area</returns>
        public List<Area> GetAreas()
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.Areas.OrderBy(a => a.AreaName).ToList();
            }
        }

        public List<Area> GetAreaByCountryID(long? countryID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                if (countryID.HasValue)
                {
                    return (from ar in db.Areas
                            join city in db.Cities on ar.CityID equals city.CityID
                            where city.CountryID == countryID && ar.IsActive == true
                            select ar).Distinct().OrderBy(a => a.AreaName).ToList();
                }
                else
                {
                    return (from ar in db.Areas
                            join city in db.Cities on ar.CityID equals city.CityID
                            where ar.IsActive == true
                            select ar).Distinct().OrderBy(a => a.AreaName).ToList();
                }
            }
        }

        public List<City> GetCityByCountryID(long? countryID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                if (countryID.HasValue)
                {
                    return (from city in db.Cities
                            where city.CountryID == countryID.Value && city.IsActive == true
                            select city).Distinct().OrderBy(x => x.CityName).ToList();
                }
                else
                {
                    return (from city in db.Cities
                            where city.IsActive == true
                            select city).Distinct().OrderBy(x => x.CityName).ToList();
                }
            }
        }


        public List<Area> GetAreaByCompanyID(long companyID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return (from ar in db.Areas
                        join city in db.Cities on ar.CityID equals city.CityID
                        join company in db.Companies on city.CountryID equals company.CountryID
                        where company.CompanyID == companyID && ar.IsActive == true
                        select ar).Distinct().OrderBy(a => a.AreaName).ToList();
            }
        }

        public List<Area> GetAreaByCityID(long? cityID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                if (cityID.HasValue)
                {
                    return (from ar in db.Areas
                            join city in db.Cities on ar.CityID equals city.CityID
                            where ar.CityID == cityID && ar.IsActive == true
                            select ar).Distinct().OrderBy(x => x.AreaName).ToList();
                }
                else
                {
                    return (from ar in db.Areas
                            join city in db.Cities on ar.CityID equals city.CityID
                            where ar.IsActive == true
                            select ar).Distinct().OrderBy(x => x.AreaName).ToList();
                }
            }
        }


        #endregion


        #region Vehicle

        /// <summary>
        /// save the vehicle entity
        /// </summary>
        /// <param name="vehicle">object of Vehicle</param>
        /// <returns>object of Vehicle</returns>
        public Vehicle SaveVehicle(Vehicle vehicle)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                if (vehicle.VehicleIID > 0)
                {
                    // Update
                    db.Entry(vehicle).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    // Insert
                    db.Entry(vehicle).State = System.Data.Entity.EntityState.Added;
                }
                db.SaveChanges();

                if (vehicle != null)
                {
                    db.Entry(vehicle).Reference(a => a.Country).Load();
                    db.Entry(vehicle).Reference(a => a.City).Load();
                }

                return vehicle;
            }
        }


        /// <summary>
        /// get the Vehicle detail based on vehicleIID
        /// </summary>
        /// <param name="vehicleID">long</param>
        /// <returns>object of vehicle entity</returns>
        public Vehicle GetVehicle(long vehicleID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var vehicle = db.Vehicles.Where(x => x.VehicleIID == vehicleID).FirstOrDefault();
                if (vehicle != null)
                {
                    db.Entry(vehicle).Reference(a => a.Country).Load();
                    db.Entry(vehicle).Reference(a => a.City).Load();
                }
                return vehicle;
            }
        }


        public List<Vehicle> GetVehicles()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Vehicles.OrderBy(a => a.VehicleCode).ToList();
            }
        }

        #endregion


        #region VehicleType

        /// <summary>
        /// get the list of VehicleType
        /// </summary>
        /// <returns>list of VehicleType</returns>
        public List<VehicleType> GetVehicleTypes()
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.VehicleTypes.OrderBy(a => a.VehicleTypeName).ToList();
            }
        }

        #endregion


        #region VehicleOwnershipType

        /// <summary>
        /// get the list of VehicleOwnershipType
        /// </summary>
        /// <returns>list of VehicleOwnershipType</returns>
        public List<VehicleOwnershipType> GetVehicleOwnershipTypes()
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.VehicleOwnershipTypes.OrderBy(a => a.OwnershipTypeName).ToList();
            }
        }

        #endregion

        #region Baskets

        public List<Basket> GetBaskets()
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.Basket.OrderBy(a => a.BasketCode).ToList();
            }
        }

        #endregion

        #region Comments

        public Comment SaveComment(Comment entity)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                if (entity.CommentIID == 0)
                    db.Entry(entity).State = System.Data.Entity.EntityState.Added;
                else
                    db.Entry(entity).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();
                return entity;
            }
        }

        public List<Comment> GetComments(long entityTypeID, long referenceID, long departmentID = 0)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                if (departmentID > 0)
                {
                    return db.Comments.Where(c => c.EntityTypeID == entityTypeID && c.ReferenceID == referenceID && c.DepartmentID == departmentID).ToList();
                }
                else
                {
                    return db.Comments.Where(c => c.EntityTypeID == entityTypeID && c.ReferenceID == referenceID).OrderByDescending(x => x.CommentIID).ToList();
                }
            }
        }

        public List<Comment> GetCommentsByEntityType(long entityTypeID, long referenceID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.Comments.Where(c => c.EntityTypeID == entityTypeID && c.ReferenceID == referenceID).ToList();
            }
        }

        public JobEntryHead GetJobByHeadID(long HeadID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var GetJobID = (from th in db.TransactionHeads
                                join jeh in db.JobEntryHeads on th.HeadIID equals jeh.TransactionHeadID
                                where jeh.TransactionHeadID == HeadID
                                select jeh).
                              Include(x => x.JobOperationStatus)
                              .Include(x => x.JobEntryDetails1)
                              .Include(x => x.JobEntryDetails)
                              .Include(y => y.JobStatus)
                              .Include(x => x.TransactionHead)
                              .FirstOrDefault();
                return GetJobID;
            }
        }


        public JobEntryHead GetJobByJobHeadID(long HeadID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.JobEntryHeads.Where(j => j.JobEntryHeadIID == HeadID).
                              Include(x => x.JobOperationStatus)
                              .Include(x => x.JobEntryDetails1)
                              .Include(x => x.JobEntryDetails)
                              .Include(y => y.JobStatus)
                              .Include(x => x.TransactionHead).FirstOrDefault();
            }
        }

        public JobEntryHead GetMissionByJobID(long jobID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var missionDetails = (from jed in db.JobEntryDetails
                                      join jeh in db.JobEntryHeads
       on jed.JobEntryHeadID equals jeh.JobEntryHeadIID
                                      where jed.ParentJobEntryHeadID == jobID
                                      select jed).FirstOrDefault();
                if (missionDetails.IsNull())
                    return new JobEntryHead();
                var JobID = Convert.ToInt64(missionDetails.JobEntryHeadID);
                return db.JobEntryHeads
                    .Where(x => x.JobEntryHeadIID == JobID)
                    .Include(x => x.ServiceProvider).Include(x => x.Employee).FirstOrDefault();
            }

        }

        public Comment GetComment(long commentID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.Comments.Where(c => c.CommentIID == commentID).SingleOrDefault();
            }
        }

        public bool DeleteComment(long commentID)
        {
            var exit = false;
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                //db.Comments.Where(c => c.CommentIID == commentID).Delete();
                // What will happen to children when we remove parent? 
                // Shall we remove them too and if not who will be their parent now ?
                exit = true;
            }
            return exit;
        }
        #endregion


        public Account GetAccountBySupplierID(long? supplierID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return (from sa in db.SupplierAccountMaps
                        join ac in db.Accounts on sa.AccountID equals ac.AccountID
                        where sa.SupplierID == supplierID
                        select ac).FirstOrDefault();

            }
        }
        public List<AdditionalExpenseProvisionalAccountMap> GetProvisionalAccountByAdditionalExpense(int? additionalExpenseID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return (from sa in db.AdditionalExpenseProvisionalAccountMaps
                        join ac in db.Accounts on sa.ProvisionalAccountID equals ac.AccountID
                        where (additionalExpenseID == null || sa.AdditionalExpenseID == additionalExpenseID)
                        select sa).ToList();

            }
        }

        public List<Account> GetAccountDetByAccountIDs(List<long?> accountIDs)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return (from ac in db.Accounts
                        where accountIDs.Contains(ac.AccountID)
                        select ac).ToList();

            }
        }

        public Attachment SaveAttachment(Attachment entity)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                if (entity.AttachmentIID == 0)
                    db.Entry(entity).State = System.Data.Entity.EntityState.Added;
                else
                    db.Entry(entity).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();
                return entity;
            }
        }

        public List<Attachment> GetAttachments(long entityTypeID, long referenceID, long departmentID = 0)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                if (departmentID > 0)
                {
                    return db.Attachments.Where(c => c.EntityTypeID == entityTypeID && c.ReferenceID == referenceID && c.DepartmentID == departmentID).ToList();
                }
                else
                {
                    return db.Attachments.Where(c => c.EntityTypeID == entityTypeID && c.ReferenceID == referenceID).OrderByDescending(x => x.AttachmentIID).ToList();
                }
            }
        }
        #region Delivery Charge

        public bool SaveDeliveryCharges(List<ProductDeliveryTypeMap> pdtMaps, long IID, bool isProduct)
        {
            try
            {
                bool result = false;

                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    if (isProduct)
                        dbContext.ProductDeliveryTypeMaps.RemoveRange(dbContext.ProductDeliveryTypeMaps.Where(x => x.ProductID == IID));
                    else
                        dbContext.ProductDeliveryTypeMaps.RemoveRange(dbContext.ProductDeliveryTypeMaps.Where(x => x.ProductSKUMapID == IID));

                    if (pdtMaps.IsNotNull() && pdtMaps.Count > 0)
                    {
                        foreach (var entity in pdtMaps)
                        {
                            dbContext.ProductDeliveryTypeMaps.Add(entity);
                        }
                    }

                    dbContext.SaveChanges();
                    result = true;
                }

                return result;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<PageRenderRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }
        }

        public bool SaveCustomerDeliveryCharges(List<CustomerGroupDeliveryTypeMap> cgdtMaps, int customerGroupID)
        {
            try
            {
                bool result = false;
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    dbContext.CustomerGroupDeliveryTypeMaps.RemoveRange(dbContext.CustomerGroupDeliveryTypeMaps.Where(x => x.CustomerGroupID == customerGroupID));


                    if (cgdtMaps != null && cgdtMaps.Count > 0)
                    {
                        foreach (var deliverySetting in cgdtMaps)
                        {
                            dbContext.CustomerGroupDeliveryTypeMaps.Add(deliverySetting);
                        }
                    }
                    dbContext.SaveChanges();
                    result = true;
                }

                return result;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<PageRenderRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }
        }
        public bool SaveZoneDeliveryCharges(List<DeliveryTypeAllowedZoneMap> dtamaps, short zoneID)
        {
            try
            {
                bool result = false;
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    dbContext.DeliveryTypeAllowedZoneMaps.RemoveRange(dbContext.DeliveryTypeAllowedZoneMaps.Where(x => x.ZoneID == zoneID));


                    if (dtamaps != null && dtamaps.Count > 0)
                    {
                        foreach (var deliverySetting in dtamaps)
                        {
                            dbContext.DeliveryTypeAllowedZoneMaps.Add(deliverySetting);
                        }
                    }
                    dbContext.SaveChanges();
                    result = true;
                }

                return result;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<PageRenderRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }
        }

        public bool SaveAreaDeliveryCharges(List<DeliveryTypeAllowedAreaMap> dtamaps, int areaID)
        {
            try
            {
                bool result = false;
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    dbContext.DeliveryTypeAllowedAreaMaps.RemoveRange(dbContext.DeliveryTypeAllowedAreaMaps.Where(x => x.AreaID == areaID));


                    if (dtamaps != null && dtamaps.Count > 0)
                    {
                        foreach (var deliverySetting in dtamaps)
                        {
                            dbContext.DeliveryTypeAllowedAreaMaps.Add(deliverySetting);
                        }
                    }
                    dbContext.SaveChanges();
                    result = true;
                }

                return result;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<PageRenderRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }
        }
        #endregion

        public List<ImageType> GetImageTypes()
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.ImageTypes.OrderBy(a => a.TypeName).ToList();
            }
        }

        public EntityTypeEntitlement GetEntitlementById(long id)
        {
            using (var db = new dbEduegateERPContext())
            {
                return db.EntityTypeEntitlements.Where(x => x.EntitlementID == id).FirstOrDefault();
            }
        }


        public Area GetAreaById(int areaId)
        {
            using (var db = new dbEduegateERPContext())
            {
                return db.Areas.Where(x => x.AreaID == areaId).FirstOrDefault();
            }
        }

        public City GetCityById(int cityId)
        {
            using (var db = new dbEduegateERPContext())
            {
                return db.Cities.Where(x => x.CityID == cityId).FirstOrDefault();
            }
        }

        public List<City> GetCityByCompanyID(long companyID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return (from city in db.Cities
                        join company in db.Companies on city.CountryID equals company.CountryID
                        where company.CompanyID == companyID && city.IsActive == true
                        select city).Distinct().OrderBy(a => a.CityName).ToList();
            }
        }

        public List<DocumentReferenceStatusMap> GetDocumentStatusesByReferenceType(int referenceTypeID)
        {
            try
            {
                using (dbEduegateERPContext db = new dbEduegateERPContext())
                {
                    return db.DocumentReferenceStatusMaps.Where(d => d.ReferenceTypeID == referenceTypeID).Include(d => d.DocumentStatus).ToList();
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get the banners", Framework.Helper.TrackingCode.ERP);
                throw ex;
            }
        }

        public DocumentStatus GetDocumentStatus(long statusID)
        {
            try
            {
                using (dbEduegateERPContext db = new dbEduegateERPContext())
                {
                    return db.DocumentStatuses.Where(d => d.DocumentStatusID == statusID).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get the banners", Framework.Helper.TrackingCode.ERP);
                throw ex;
            }
        }

        public List<DocumentStatus> GetDocumentStatus()
        {
            try
            {
                using (dbEduegateERPContext db = new dbEduegateERPContext())
                {
                    return db.DocumentStatuses.OrderBy(d => d.StatusName).ToList();
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Failed to load Document Statuses", Framework.Helper.TrackingCode.ERP);
                throw ex;
            }
        }

        public DocumentReferenceStatusMap GetDocumentReferenceStatusMap(int documentReferenceTypeID)
        {
            try
            {
                using (dbEduegateERPContext db = new dbEduegateERPContext())
                {
                    return db.DocumentReferenceStatusMaps.Where(d => d.DocumentReferenceStatusMapID == documentReferenceTypeID).Include(d => d.DocumentStatus).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get the banners", Framework.Helper.TrackingCode.ERP);
                throw ex;
            }
        }

        public IEnumerable<ActionLinkType> GetActionLinkTypes()
        {
            using (var db = new dbEduegateERPContext())
            {
                return db.ActionLinkTypes.OrderBy(a => a.ActionLinkTypeName).ToList();
            }
        }

        public EntitlementMap GetEntitlementMap(long supplierID, short supplier)
        {
            dbEduegateERPContext db = new dbEduegateERPContext();

            var ent = (from em in db.EntitlementMaps
                       join ete in db.EntityTypeEntitlements on em.EntitlementID equals ete.EntitlementID
                       where em.ReferenceID == supplierID && ete.EntityTypeID == supplier
                       select em).FirstOrDefault();
            return ent;
        }

        //public bool GetHoliday(DateTime date, int companyID)
        //{
        //    using (var db = new dbEduegateERPContext())
        //    {
        //        return db.Holidays.Where(x => DbFunctions.TruncateTime(x.HolidayDate) == DbFunctions.TruncateTime(date) && x.CompanyID  == companyID).Any();
        //    }
        //}

        public DateTime GetHoliday(int deliveryTypeID, int siteID, int addedDays)
        {
            DateTime deliveryDateTime = DateTime.Now;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationExtensions.GetConnectionString("dbEduegateERPContext").ToString()))
                using (SqlCommand cmd = new SqlCommand("distribution.spcOrderDeliveryHolidaysDays", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@DeliveryTypeID", SqlDbType.Int).Value = deliveryTypeID;
                    cmd.Parameters.Add("@SiteID", SqlDbType.Int).Value = siteID;
                    cmd.Parameters.Add("@AddedDays", SqlDbType.Int).Value = addedDays;

                    cmd.Parameters.Add("@DeliveryDateTime", SqlDbType.DateTime).Value = null;
                    cmd.Parameters["@DeliveryDateTime"].Direction = ParameterDirection.Output;

                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();

                    deliveryDateTime = Convert.ToDateTime(cmd.Parameters["@DeliveryDateTime"].Value);
                    cmd.Connection.Close();
                }
            }
            catch (Exception)
            {
            }
            return deliveryDateTime;
        }

        public string GetCultureCode(int cultureID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.Cultures.Where(x => x.CultureID == cultureID).Select(x => x.CultureCode).FirstOrDefault();
            }
        }

        public List<ScreenShortCut> GetShortCuts(long screenID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.ScreenShortCuts.Where(x => x.ScreenID == screenID).ToList();
            }
        }

        public Sequence GetNextSequence(string sequenceTypes)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var sequence = db.Sequences.Where(x => x.SequenceType == sequenceTypes).FirstOrDefault();
                sequence.LastSequence = sequence.LastSequence.HasValue ? sequence.LastSequence + 1 : 1;
                db.SaveChanges();
                return sequence;
            }
        }

        public Setting GetSettingData(string sequenceTypes)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var querySettings = (from st in db.Settings where st.CompanyID == 1 select st);
                var queryTransferProcess = querySettings.Where(x => x.SettingCode == sequenceTypes).FirstOrDefault();
                return queryTransferProcess;
            }
        }


    }
}