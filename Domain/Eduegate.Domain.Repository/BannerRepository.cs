using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Helper;
using Eduegate.Framework.Logs;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Repository
{
    public class BannerRepository
    {
        public List<Banner> GetBanners()
        {
            List<Banner> entity = null;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    entity = dbContext.Banners.AsNoTracking().ToList();
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get the banners", TrackingCode.ERP);
            }

            return entity;
        }

        public List<Banner> GetBanners(int bannerTypeID, int statusID, int companyID = 0)
        {
            List<Banner> entity = null;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    if (statusID == 0)
                        entity = dbContext.Banners.Where(a => a.BannerTypeID == bannerTypeID /*&& a.CompanyID == companyID*/).OrderBy(r => r.SerialNo == null).ThenBy(r => r.SerialNo).AsNoTracking().ToList();

                    else
                        entity = dbContext.Banners.Where(a => a.BannerTypeID == bannerTypeID && a.StatusID == statusID /*&& a.CompanyID == companyID*/).OrderBy(r => r.SerialNo == null).ThenBy(r => r.SerialNo).AsNoTracking().ToList();
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get the banners", TrackingCode.ERP);
            }

            return entity;
        }

        public List<BannerStatus> GetBannerStatuses()
        {
            List<BannerStatus> entity = null;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    entity = dbContext.BannerStatuses.OrderBy(a => a.BannerStatusName).AsNoTracking().ToList();
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get the banners", TrackingCode.ERP);
            }

            return entity;
        }

        public List<BannerType> GetBannerTypes()
        {
            List<BannerType> entity = null;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    entity = dbContext.BannerTypes.OrderBy(a => a.BannerTypeName).AsNoTracking().ToList();
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get the banners", TrackingCode.ERP);
            }

            return entity;
        }

        public Banner GetBanner(long bannerID, int companyID)
        {
            Banner entity = null;

            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    entity = dbContext.Banners.Where(x => x.BannerIID == bannerID /*&& x.CompanyID == companyID*/)
                        .Include(i => i.BannerStatus)
                        .Include(i => i.BannerType)
                        .AsNoTracking()
                        .FirstOrDefault();
                    //dbContext.Entry(entity).Reference(a => a.BannerStatus).Load();
                    //dbContext.Entry(entity).Reference(a => a.BannerType).Load();
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get the banners", TrackingCode.ERP);
            }

            return entity;
        }

        public Banner SaveBanner(Banner banner, int companyID)
        {
            Banner updatedEntity = null;
            if (banner.BannerIID != 0)
            {
                var oldBannerSerial = GetBanner(banner.BannerIID, companyID);
                if (oldBannerSerial.SerialNo == banner.SerialNo)
                {
                    try
                    {
                        using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                        {
                            if (oldBannerSerial.BannerTypeID != banner.BannerTypeID)
                            {
                                var updateBanner = (from ban in dbContext.Banners
                                                    where ban.BannerTypeID == oldBannerSerial.BannerTypeID && ban.SerialNo > banner.SerialNo && ban.CompanyID == banner.CompanyID
                                                    select ban).AsNoTracking().ToList();
                                foreach (var ban in updateBanner)
                                {
                                    ban.SerialNo = ban.SerialNo - 1;
                                    dbContext.Banners.Add(ban);

                                    dbContext.Entry(ban).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                    //dbContext.SaveChanges();
                                }
                            }
                            dbContext.Banners.Add(banner);

                            dbContext.Entry(banner).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                            dbContext.SaveChanges();
                            updatedEntity = dbContext.Banners.Where(x => x.BannerIID == banner.BannerIID && x.CompanyID == companyID).AsNoTracking().FirstOrDefault();
                        }
                    }
                    catch (Exception exception)
                    {
                        Eduegate.Logger.LogHelper<ProductDetailRepository>.Fatal(exception.Message.ToString(), exception);
                        throw exception;
                    }
                }
                else if (banner.SerialNo < oldBannerSerial.SerialNo)
                {
                    using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                    {
                        if (oldBannerSerial.BannerTypeID != banner.BannerTypeID)
                        {
                            var updateBanner = (from ban in dbContext.Banners
                                                where ban.BannerTypeID == oldBannerSerial.BannerTypeID && ban.SerialNo > banner.SerialNo && ban.CompanyID == banner.CompanyID
                                                && ban.SerialNo != oldBannerSerial.SerialNo
                                                select ban).AsNoTracking().ToList();
                            foreach (var ban in updateBanner)
                            {
                                ban.SerialNo = ban.SerialNo - 1;
                                dbContext.Banners.Add(ban);

                                dbContext.Entry(ban).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                //dbContext.SaveChanges();
                            }
                        }
                        var getbanners = (from ban in dbContext.Banners
                                          where ban.BannerTypeID == banner.BannerTypeID && ban.CompanyID == banner.CompanyID && ban.SerialNo >= banner.SerialNo
                                          && ban.SerialNo != oldBannerSerial.SerialNo && ban.SerialNo < oldBannerSerial.SerialNo
                                          select ban).ToList();
                        foreach (var ban in getbanners)
                        {
                            ban.SerialNo = ban.SerialNo + 1;
                            dbContext.Banners.Add(ban);
                            dbContext.Entry(ban).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            //dbContext.SaveChanges();
                        }
                        
                        dbContext.Banners.Add(banner);
                        
                        dbContext.Entry(banner).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                        dbContext.SaveChanges();
                        updatedEntity = dbContext.Banners.Where(x => x.BannerIID == banner.BannerIID).AsNoTracking().FirstOrDefault();
                    }
                }
                else
                {
                    using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                    {
                        if (oldBannerSerial.BannerTypeID != banner.BannerTypeID)
                        {
                            var updateBanner = (from ban in dbContext.Banners
                                                where ban.BannerTypeID == oldBannerSerial.BannerTypeID && ban.SerialNo > oldBannerSerial.SerialNo && ban.CompanyID == banner.CompanyID
                                                select ban).AsNoTracking().ToList();
                            foreach (var ban in updateBanner)
                            {
                                ban.SerialNo = ban.SerialNo - 1;
                                dbContext.Banners.Add(ban);

                                dbContext.Entry(ban).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                //dbContext.SaveChanges();
                            }
                        }
                        var getbanners = (from ban in dbContext.Banners
                                          where ban.BannerTypeID == banner.BannerTypeID && ban.CompanyID == banner.CompanyID && ban.SerialNo > oldBannerSerial.SerialNo
                                          && ban.SerialNo != oldBannerSerial.SerialNo && ban.SerialNo <= banner.SerialNo
                                          select ban).AsNoTracking().ToList();
                        foreach (var ban in getbanners)
                        {
                            ban.SerialNo = ban.SerialNo - 1;
                            dbContext.Banners.Add(ban);

                            dbContext.Entry(ban).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            //dbContext.SaveChanges();
                        }
                        
                        dbContext.Banners.Add(banner);

                        dbContext.Entry(banner).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                        dbContext.SaveChanges();
                        updatedEntity = dbContext.Banners.Where(x => x.BannerIID == banner.BannerIID && x.CompanyID == companyID).AsNoTracking().FirstOrDefault();
                    }

                }
            }
            else
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    var banserial = (from ban in dbContext.Banners
                                     where ban.BannerTypeID == banner.BannerTypeID && ban.CompanyID == banner.CompanyID
                                     select ban).Max(a => (long?)a.SerialNo);

                    banner.SerialNo = banserial.HasValue ? banserial.Value + 1 : 1;
                    dbContext.Banners.Add(banner);

                    dbContext.Entry(banner).State = Microsoft.EntityFrameworkCore.EntityState.Added;

                    dbContext.SaveChanges();
                    updatedEntity = dbContext.Banners.Where(x => x.BannerIID == banner.BannerIID && x.CompanyID == companyID).AsNoTracking().FirstOrDefault();
                }
            }

            return updatedEntity;
        }
    }
}
