using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Entity.Models.ValueObjects;
using Eduegate.Framework.Extensions;

namespace Eduegate.Domain.Repository.Security
{
    public class SecurityRepository
    {
        public List<string> GetUserClaimKey(long userID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var userClaims = new List<string>();
                var claims = ((from map in dbContext.ClaimSetLoginMaps
                               join clsetmap in dbContext.ClaimSetClaimMaps
                               on map.ClaimSetID equals clsetmap.ClaimSetID
                               where map.LoginID.Value == userID
                               select clsetmap.Claim
                                  ).Union(

                                  (from map in dbContext.ClaimLoginMaps
                                   join clmap in dbContext.ClaimLoginMaps
                                  on map.LoginID equals clmap.LoginID
                                   where map.LoginID.Value == userID
                                   select clmap.Claim
                                )));

                foreach (var claim in claims.ToList())
                {
                    userClaims.Add(((Eduegate.Framework.Security.Enums.ClaimType)Enum.Parse(typeof(Eduegate.Framework.Security.Enums.ClaimType), claim.ClaimTypeID.ToString())).ToString() + "." + claim.ResourceName);
                }

                return userClaims;
            }
        }

        public List<string> GetUserClaimKey(long userID, int claimTypeID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var userClaims = new List<string>();
                var claims = ((from map in dbContext.ClaimSetLoginMaps
                               join clsetmap in dbContext.ClaimSetClaimMaps
                               on map.ClaimSetID equals clsetmap.ClaimSetID
                               where map.LoginID.Value == userID && clsetmap.Claim.ClaimTypeID == claimTypeID
                               select clsetmap.Claim
                                  ).Union(

                                  (from map in dbContext.ClaimLoginMaps
                                   join clmap in dbContext.ClaimLoginMaps
                                  on map.LoginID equals clmap.LoginID
                                   where map.LoginID.Value == userID && clmap.Claim.ClaimTypeID == claimTypeID
                                   select clmap.Claim
                                )));

                foreach (var claim in claims.ToList())
                {
                    userClaims.Add(((Eduegate.Framework.Security.Enums.ClaimType)Enum.Parse(typeof(Eduegate.Framework.Security.Enums.ClaimType), claim.ClaimTypeID.ToString())).ToString() + "." + claim.ResourceName);
                }

                return userClaims;
            }
        }

        public List<Eduegate.Domain.Entity.Models.Claim> GetUserClaims(long userID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var claims = ((from map in dbContext.ClaimSetLoginMaps
                               join clsetmap in dbContext.ClaimSetClaimMaps
                               on map.ClaimSetID equals clsetmap.ClaimSetID
                               where map.LoginID.Value == userID
                               select clsetmap.Claim
                                  ).Union(

                                  (from map in dbContext.ClaimLoginMaps
                                   join clmap in dbContext.ClaimLoginMaps
                                  on map.LoginID equals clmap.LoginID
                                   where map.LoginID.Value == userID
                                   select clmap.Claim
                                )));

                return claims.ToList();
            }
        }

        public List<Eduegate.Domain.Entity.Models.Claim> GetUserClaims(long userID, int claimTypeID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (userID == 0)
                {
                    return dbContext.Claims.Where(a => a.ClaimTypeID == claimTypeID).ToList();
                }
                else
                {
                    var claims = ((from map in dbContext.ClaimSetLoginMaps
                                   join clsetmap in dbContext.ClaimSetClaimMaps
                                   on map.ClaimSetID equals clsetmap.ClaimSetID
                                   where map.LoginID.Value == userID && clsetmap.Claim.ClaimTypeID == claimTypeID
                                   select clsetmap.Claim
                                      ).Union(

                                      (from map in dbContext.ClaimLoginMaps
                                       join clmap in dbContext.ClaimLoginMaps
                                      on map.LoginID equals clmap.LoginID
                                       where map.LoginID.Value == userID && clmap.Claim.ClaimTypeID == claimTypeID
                                       select clmap.Claim
                                    )));

                    return claims.ToList();
                }
            }
        }

        public List<string> GetUserClaimKeyByType(long userID, int claimTypeID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var userClaims = new List<string>();
                var claims = ((from map in dbContext.ClaimSetLoginMaps
                               join clsetmap in dbContext.ClaimSetClaimMaps
                               on map.ClaimSetID equals clsetmap.ClaimSetID
                               join claim in dbContext.Claims 
                               on clsetmap.ClaimID equals claim.ClaimIID
                               where map.LoginID.Value == userID && claim.ClaimTypeID == claimTypeID
                               select clsetmap.Claim
                                  ).Union(

                                  (from map in dbContext.ClaimLoginMaps
                                   join clmap in dbContext.ClaimLoginMaps
                                  on map.LoginID equals clmap.LoginID
                                   join claim in dbContext.Claims
                                    on clmap.ClaimID equals claim.ClaimIID
                                   where map.LoginID.Value == userID && claim.ClaimTypeID == claimTypeID
                                   select clmap.Claim
                                ))).Where(a=> a.ClaimTypeID == claimTypeID);

                foreach (var claim in claims.ToList())
                {
                    userClaims.Add(((Eduegate.Framework.Security.Enums.ClaimType)Enum.Parse(typeof(Eduegate.Framework.Security.Enums.ClaimType), claim.ClaimTypeID.ToString())).ToString() + "." + claim.ResourceName);
                }

                return userClaims;
            }
        }

        public List<Eduegate.Domain.Entity.Models.Claim> GetUserClaimsByType(long userID, int claimTypeID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var userClaims = new List<string>();
                var claims = ((from map in dbContext.ClaimSetLoginMaps
                               join clsetmap in dbContext.ClaimSetClaimMaps
                               on map.ClaimSetID equals clsetmap.ClaimSetID
                               join claim in dbContext.Claims
                               on clsetmap.ClaimID equals claim.ClaimIID
                               where map.LoginID.Value == userID && claim.ClaimTypeID == claimTypeID
                               select clsetmap.Claim
                                  ).Union(

                                  (from map in dbContext.ClaimLoginMaps
                                   join clmap in dbContext.ClaimLoginMaps
                                  on map.LoginID equals clmap.LoginID
                                   join claim in dbContext.Claims
                                    on clmap.ClaimID equals claim.ClaimIID
                                   where map.LoginID.Value == userID && claim.ClaimTypeID == claimTypeID
                                   select clmap.Claim
                                ))).Where(a => a.ClaimTypeID == claimTypeID);

                //var newClaims = dbContext.Claims.Where(x => x.ClaimTypeID == claimTypeID && x.ClaimLoginMaps.Any(l => l.LoginID == userID)).ToList();

                return claims.ToList();
            }
        }


        public List<Eduegate.Domain.Entity.Models.Claim> GetDashBoardByUserID(long userID, int claimTypeID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var userClaims = new List<Entity.Models.Claim>();

                userClaims = dbContext.Claims.Where(x => x.ClaimTypeID == claimTypeID && x.ClaimLoginMaps.Any(l => l.LoginID == userID)).ToList();

                if (userClaims.Count <= 0)
                {

                    var claimID = dbContext.ClaimSetLoginMaps.Where(x => x.LoginID == userID)?.ToList().LastOrDefault()?.ClaimSet.ClaimSetClaimMaps?.Where(y => y.Claim.ClaimTypeID == claimTypeID)?.Select(z => z.ClaimID)?.FirstOrDefault();

                    userClaims = dbContext.Claims.Where(c => c.ClaimTypeID == claimTypeID && c.ClaimIID == claimID && c.ClaimTypeID == claimTypeID).ToList();

                }
                return userClaims;

            }
        }

        public List<Eduegate.Domain.Entity.Models.ValueObjects.ClaimDetails> GetClaimsByClaimType(int claimTypeID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var claims = (from claim in dbContext.Claims
                              join clType in dbContext.ClaimTypes
                              on claim.ClaimTypeID equals clType.ClaimTypeID
                              select new ClaimDetails()
                              {
                                  ClaimIID = claim.ClaimIID,
                                  ClaimName = claim.ClaimName,
                                  ClaimTypeID = claim.ClaimTypeID,
                                  ClaimTypeName = clType.ClaimTypeName,
                                  ResourceName = claim.ResourceName
                              }).Where(a=> a.ClaimTypeID== claimTypeID).ToList();

                return claims;
            }
        }

        public List<Eduegate.Domain.Entity.Models.ValueObjects.ClaimDetails> GetClaims()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var claims = (from claim in dbContext.Claims
                              join clType in dbContext.ClaimTypes
                              on claim.ClaimTypeID equals clType.ClaimTypeID
                              select new ClaimDetails()
                              {
                                  ClaimIID = claim.ClaimIID,
                                  ClaimName = claim.ClaimName,
                                  ClaimTypeID = claim.ClaimTypeID,
                                  ClaimTypeName = clType.ClaimTypeName,
                                  ResourceName = claim.ResourceName
                              }).OrderBy(a => new { a.ClaimName, a.ClaimTypeName }).ToList();

                return claims;
            }
        }

        public Eduegate.Domain.Entity.Models.Claim GetClaim(long claimID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return (from claim in dbContext.Claims
                              join clType in dbContext.ClaimTypes
                              on claim.ClaimTypeID equals clType.ClaimTypeID
                              select claim).Where(a=> a.ClaimIID == claimID).FirstOrDefault();
            }
        }

        public ClaimSet GetClaimSet(long claimSetID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var claimSet = dbContext.ClaimSets.Where(a=> a.ClaimSetIID == claimSetID).FirstOrDefault();

                foreach (var claim in claimSet.ClaimSetClaimMaps)
                {
                    dbContext.Entry(claim).Reference(a => a.Claim).Load();
                    dbContext.Entry(claim.Claim).Reference(a => a.ClaimType).Load();
                    dbContext.Entry(claim).Reference(a => a.ClaimSet).Load();
                }

                foreach (var claim in claimSet.ClaimSetClaimSetMaps)
                {
                    dbContext.Entry(claim).Reference(a => a.ClaimSet).Load();
                }

                return claimSet;
            }
        }

        public List<ClaimSet> GetClaimSets()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.ClaimSets.OrderBy(a=> a.ClaimSetName).ToList();    
            }
        }

        public List<ClaimType> GetClaimTypes()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.ClaimTypes.OrderBy(a=> a.ClaimTypeName).ToList();
            }
        }

        public Eduegate.Domain.Entity.Models.Claim SaveClaim(Eduegate.Domain.Entity.Models.Claim entity)
        {
            Eduegate.Domain.Entity.Models.Claim updatedEntity = null;
            try
            {
                if (entity.IsNotNull())
                {
                    using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                    {
                        if (entity.ClaimIID <= 0)
                            dbContext.Entry(entity).State = System.Data.Entity.EntityState.Added;
                        else
                            dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;

                        dbContext.SaveChanges();
                        updatedEntity = dbContext.Claims.Where(x => x.ClaimIID == entity.ClaimIID).FirstOrDefault();                        
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

        public void CreateOrUpdateClaimsByResource(string resource, string name, int type)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    if (resource.IsNotNullOrEmpty())
                    {
                        long claimID;
                        var claim = dbContext.Claims.FirstOrDefault(a => a.ResourceName == resource && a.ClaimTypeID == type);

                        if (claim == null)
                        {
                            claim = new Entity.Models.Claim() { ClaimName = name, ResourceName = resource, ClaimTypeID = type };
                            dbContext.Claims.Add(claim);
                            dbContext.SaveChanges();
                            claimID = claim.ClaimIID;
                        }
                        else
                        {
                            claim.ClaimName = name;
                            dbContext.SaveChanges();
                            claimID = claim.ClaimIID;
                        }

                        //add claims into the admin claimset by default
                        if (!dbContext.ClaimSetClaimMaps.Any(a => a.ClaimID == claimID && a.ClaimSetID == 1))
                        {
                            dbContext.ClaimSetClaimMaps.Add(new ClaimSetClaimMap() { ClaimSetID = 1, ClaimID = claimID });
                            dbContext.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<WarehouseRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }
        }

        public Eduegate.Domain.Entity.Models.ClaimSet SaveClaimSets(Eduegate.Domain.Entity.Models.ClaimSet entity)
        {
            Eduegate.Domain.Entity.Models.ClaimSet updatedEntity = null;
            try
            {
                if (entity.IsNotNull())
                {
                    using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                    {
                        dbContext.ClaimSets.Add(entity);

                        if (entity.ClaimSetIID <= 0)
                            dbContext.Entry(entity).State = System.Data.Entity.EntityState.Added;
                        else
                        {
                            var claimIIDs = entity.ClaimSetClaimMaps.Select(a => a.ClaimID).ToList();

                            //Delete ClaimSetClaimMaps
                            var deletedClaimMaps = dbContext.ClaimSetClaimMaps.Where(x => x.ClaimSetID == entity.ClaimSetIID && !claimIIDs.Contains(x.ClaimID)).ToList();
                            if (deletedClaimMaps.IsNotNull())
                                dbContext.ClaimSetClaimMaps.RemoveRange(deletedClaimMaps);

                            foreach (var claimMap in entity.ClaimSetClaimMaps)
                            {
                                if (claimMap.ClaimSetClaimMapIID == 0)
                                    dbContext.Entry(claimMap).State = System.Data.Entity.EntityState.Added;
                                else
                                    dbContext.Entry(claimMap).State = System.Data.Entity.EntityState.Modified;
                            }

                            var claimSetIDs = entity.ClaimSetClaimSetMaps.Select(a => a.ClaimSetID).ToList();
                            //Delete ClaimSetClaimSetMaps
                            var deletedClaimSetClaimSetMaps = dbContext.ClaimSetClaimSetMaps.Where(x => x.ClaimSetID == entity.ClaimSetIID && !claimSetIDs.Contains(x.LinkedClaimSetID)).ToList();
                            if (deletedClaimSetClaimSetMaps.IsNotNull())
                                dbContext.ClaimSetClaimSetMaps.RemoveRange(deletedClaimSetClaimSetMaps);

                            foreach (var calimSetMap in entity.ClaimSetClaimSetMaps)
                            {
                                if (calimSetMap.ClaimSetClaimSetMapIID == 0)
                                    dbContext.Entry(calimSetMap).State = System.Data.Entity.EntityState.Added;
                            }

                            dbContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                        }

                        dbContext.SaveChanges();
                        updatedEntity = GetClaimSet(entity.ClaimSetIID);
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

        public long GetClaimSetClaimMapIID(long claimSetID, long claimID)
        {
            long claimMapID = default(long);

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    var claimSetMaps = dbContext.ClaimSetClaimMaps.Where(x => x.ClaimSetID == claimSetID && x.ClaimID == claimID).FirstOrDefault();
                    if(claimSetMaps != null)
                    {
                        claimMapID = claimSetMaps.ClaimSetClaimMapIID;
                    }
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<WarehouseRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }

            return claimMapID;
        }


        public string GetClaimName(long claimIID)
        {
            string claimName = string.Empty;
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    claimName = dbContext.Claims.Where(x => x.ClaimIID == claimIID).Select(x => x.ClaimName).FirstOrDefault().ToString();
                }
            }
            catch (Exception exception)
            {                
                Eduegate.Logger.LogHelper<WarehouseRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }

            return claimName;
        }

        public long GetClaimSetClaimSetMapIID(long claimSetID, long linkedClaimSetID)
        {
            long claimSetMapID = default(long);

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    var claimSetMaps = dbContext.ClaimSetClaimSetMaps.Where(x => x.ClaimSetID == claimSetID && x.LinkedClaimSetID == linkedClaimSetID).FirstOrDefault();
                    if (claimSetMaps != null)
                    {
                        claimSetMapID = claimSetMaps.ClaimSetClaimSetMapIID;
                    }
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<WarehouseRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }

            return claimSetMapID;
        }

        public string GetClaimSetName(long claimSetIID)
        {
            string claimSetName = string.Empty;
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    claimSetName = dbContext.ClaimSets.Where(x => x.ClaimSetIID == claimSetIID).Select(x => x.ClaimSetName).FirstOrDefault().ToString();
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<WarehouseRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }

            return claimSetName;
        }

        public bool HasClaimAccess(long claimID, long userID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                if (db.ClaimLoginMaps.Where(a => a.LoginID == userID && a.ClaimID == claimID).Any())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool HasClaimSetAccessByResource(string resourceID, long userID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                if (db.ClaimLoginMaps.Where(a => a.LoginID == userID && a.Claim.ResourceName == resourceID).Any())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        public bool HasClaimAccessByResourceID(string resource, long userID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                if ((from map in db.ClaimSetLoginMaps
                               join clsetmap in db.ClaimSetClaimMaps
                               on map.ClaimSetID equals clsetmap.ClaimSetID
                                where map.LoginID.Value == userID && clsetmap.Claim.ResourceName == resource
                               select clsetmap.Claim
                                 ).Union(

                                 (from map in db.ClaimLoginMaps
                                  join clmap in db.ClaimLoginMaps
                                 on map.LoginID equals clmap.LoginID
                                  where map.LoginID.Value == userID && clmap.Claim.ResourceName == resource
                                  select clmap.Claim
                               )).Any())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool HasClaimSetAccess(long claimSetID,long userID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                if (db.ClaimSetLoginMaps.Where(a => a.LoginID == userID && a.ClaimSetID == claimSetID).Any())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<LoginRoleMap> GetUserRoles(long loginID)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    var roles = dbContext.LoginRoleMaps.Where(x => x.LoginID == loginID).ToList();

                    foreach(var login in roles)
                    {
                        dbContext.Entry(login).Reference(a => a.Login).Load();
                        dbContext.Entry(login).Reference(a => a.Role).Load();
                    }

                    return roles;
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<WarehouseRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }
        }

        public Login GetLogin(long loginID)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    var login = dbContext.Logins.Where(x => x.LoginIID == loginID).FirstOrDefault();
                    dbContext.Entry(login).Collection(a => a.LoginRoleMaps).Load();
                    dbContext.Entry(login).Collection(a => a.ClaimLoginMaps).Load();
                    dbContext.Entry(login).Collection(a => a.ClaimSetLoginMaps).Load();
                    return login;
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<WarehouseRepository>.Fatal(exception.Message.ToString(), exception);
                throw exception;
            }
        }
    }
}
