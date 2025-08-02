using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.DataAccess;
using WB.DataAccess.Interfaces;
using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Logs;
using Eduegate.Framework.Repository;
using Eduegate.Framework.Repository.Interfaces;
using Eduegate.Services.Contracts.Banner;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper;

namespace Eduegate.Domain.Repository
{
    public class BannerMangementRepository : IBannerManagementDA
    {
        public List<BannerConfiguration> GetBannerConfigurations()
        {
            var da = new BannerConfigurationDA<WBDbContext>();
            return da.GetAll();
        }

        public List<BannerConfiguration> GetBannerConfigurationsByProductID(long productID)
        {
            var da = new BannerConfigurationDA<WBDbContext>();
            return da.Query(a => a.BannerParameters.All( param => param.ParameterValue.Equals(productID.ToString()) && param.ParameterName.Equals("PRODUCTID"))).ToList();
        }

        public BannerConfiguration GetBannerConfiguration(long bannerConfigurationIID)
        {
            var da = new BannerConfigurationDA<WBDbContext>();
            return da.Query( a=> a.BannerConfigurationIID == bannerConfigurationIID).FirstOrDefault();
        }
        public BannerConfiguration GetBannerConfiguration(string bannerConfigurationName)
        {
            var da = new BannerConfigurationDA<WBDbContext>();
            return da.Query(a => a.BannerFileName == bannerConfigurationName).FirstOrDefault();
        }     

        public BannerConfiguration UpdateBannerConfiguration(BannerConfiguration bannerConfiguration, List<BannerWatermarkImageMap> imageMaps, List<BannerWatermarkTextMap> textMaps, List<BannerParameter> parameter)
        {
            using (var unitWork = new UnitOfWork(new DatabaseFactory<WBDbContext>()))
            {
                using (var transaction = unitWork.DataContext.Database.BeginTransaction())
                {
                    var daConfiguration = new BannerConfigurationDA<WBDbContext>(unitWork);
                    var daFetchConfiguration = new BannerConfigurationDA<WBDbContext>();
                    var existingConfigurationData = daFetchConfiguration.Query(a => a.BannerConfigurationIID == bannerConfiguration.BannerConfigurationIID).FirstOrDefault();

                    if (existingConfigurationData == null)
                    {
                        daConfiguration.Add(bannerConfiguration);
                    }
                    else
                    {
                        daConfiguration.Update(bannerConfiguration);
                    }

                    unitWork.Commit();

                    //remvoe all the map files and recreated
                    // Image map
                    var daImageMap = new BannerWatermarkImageMapDA<WBDbContext>(unitWork);

                    if (existingConfigurationData != null)
                    {
                        daImageMap.Delete(a => a.BannerConfigurationID == existingConfigurationData.BannerConfigurationIID);
                    }

                    foreach (var map in imageMaps)
                    {
                        map.BannerConfigurationID = bannerConfiguration.BannerConfigurationIID;
                        daImageMap.Add(map);
                    }

                    var daTextMap = new BannerWatermarkTextMapDA<WBDbContext>(unitWork);

                    if (existingConfigurationData != null)
                    {
                        daTextMap.Delete(a => a.BannerConfigurationID == existingConfigurationData.BannerConfigurationIID);
                    }

                    foreach (var map in textMaps)
                    {
                        map.BannerConfigurationID = bannerConfiguration.BannerConfigurationIID;
                        daTextMap.Add(map);
                    }

                    var daParameter = new BannerParameterDA<WBDbContext>(unitWork);

                    if (existingConfigurationData != null)
                    {
                        daParameter.Delete(a => a.BannerConfigurationID == existingConfigurationData.BannerConfigurationIID);
                    }

                    foreach (var param in parameter)
                    {
                        param.BannerConfigurationID = bannerConfiguration.BannerConfigurationIID;
                        daParameter.Add(param);
                    }

                    unitWork.Commit();
                    transaction.Commit();
                    return daConfiguration.Query(a => a.BannerConfigurationIID == bannerConfiguration.BannerConfigurationIID).FirstOrDefault();
                }
            }
        }

        public CategoryBannerMaster GetCategoryBanner(int bannerID)
        {
            try
            {
                using (dbBlinkContext db = new dbBlinkContext())
                {
                    return db.CategoryBannerMaster.Where(a => a.BannerID == bannerID).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to get Wallet transaction details", TrackingCode.Banner);
                return null;
            }
        }

        public CategoryBannerMaster UpdateCategoryBanner(CategoryBannerMaster banner)
        {
            CategoryBannerMaster bannerDetails = null;
            try
            {
                using (dbBlinkContext dbContext = new dbBlinkContext())
                {
                    bannerDetails = dbContext.CategoryBannerMaster.Where(x => x.BannerID == banner.BannerID).FirstOrDefault();

                    if (bannerDetails.IsNotNull())
                    {
                        bannerDetails.Active = banner.Active;
                        bannerDetails.BannerFile = banner.BannerFile;
                        bannerDetails.BannerName = banner.BannerName;
                        bannerDetails.BannerNameAr = banner.BannerNameAr;
                        bannerDetails.BannerType = banner.BannerType;
                        bannerDetails.Lang = banner.Lang;
                        bannerDetails.Link = banner.Link;
                        bannerDetails.Position = banner.Position;
                        bannerDetails.RefCategoryID = banner.RefCategoryID;
                        bannerDetails.RefCountryID = banner.RefCountryID;
                        bannerDetails.Target = banner.Target;
                        bannerDetails.ThumbFile = banner.ThumbFile;
                        bannerDetails.UpdatedOn = DateTime.Now;
                        bannerDetails.UseMap = banner.UseMap;
                        dbContext.SaveChanges();

                        //refetch the data with timestamp
                        bannerDetails = dbContext.CategoryBannerMaster.Where(x => x.BannerID == banner.BannerID).FirstOrDefault();
                    }
                }
            }
            catch (DbEntityValidationException entityEx)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = entityEx.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);
                SystemLog.Log("Application", EventLogEntryType.Error, "Not able to update category banner, banner id:" + banner.BannerID.ToString(), fullErrorMessage, TrackingCode.Banner);
            }
            catch (Exception ex)
            {
                SystemLog.Log("Application", EventLogEntryType.Error, ex, "Not able to update category banner, banner id:" + banner.BannerID.ToString(), TrackingCode.Banner);
            }

            return bannerDetails;
        }
    }
}
