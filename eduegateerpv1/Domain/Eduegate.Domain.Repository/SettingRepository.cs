using System.Collections.Generic;
using System.Linq;
using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Repository
{
    public class SettingRepository
    {
        public List<View> GetViews()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Views.AsNoTracking().ToList();
            }
        }
        public Entity.Models.Setting GetSettingDetail(string settingKey)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                return (from setting in dbContext.Settings
                        where setting.SettingCode.Equals(settingKey)
                        select setting).AsNoTracking().FirstOrDefault();
            }
        }

        public UserSetting GetUserSettingDetail(long loginID, string settingKey)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return (from setting in dbContext.UserSettings
                                  where setting.SettingCode.Equals(settingKey) && setting.LoginID == loginID
                                  select setting)
                                  .AsNoTracking()
                                  .FirstOrDefault();

            }
        }

        public List<UserSetting> GetAllUserSettingDetail(long loginID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return (from setting in dbContext.UserSettings
                                  where setting.LoginID == loginID
                                  select setting)
                                  .AsNoTracking()
                                  .ToList();

            }
        }

        public Entity.Models.Setting GetSettingDetail(string settingKey, int companyID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                return (from setting in dbContext.Settings
                        where setting.SettingCode.Equals(settingKey)
                        && setting.CompanyID == companyID
                        select setting)
                        .AsNoTracking()
                        .FirstOrDefault();

            }
        }

        public List<Entity.Models.Setting> GetAllSettings(int companyID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Settings.Where(a => a.CompanyID == companyID)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public Entity.Models.Setting GetSettingDetail(string settingKey, int companyID, int siteID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                return (from setting in dbContext.Settings
                        where setting.SettingCode.Equals(settingKey)
                        && setting.CompanyID == companyID
                        && setting.SiteID == siteID
                        select setting)
                        .AsNoTracking()
                        .FirstOrDefault();

            }
        }

        public Entity.Models.Setting SaveSettings(Entity.Models.Setting entity)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var existing = dbContext.Settings.Where(a => a.CompanyID == entity.CompanyID && a.SettingCode == entity.SettingCode)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (existing == null) {
                    dbContext.Settings.Add(entity);
                }
                else
                {
                    existing.SettingValue = entity.SettingValue;
                    dbContext.Entry(existing).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return entity;
        }

        public List<Entity.Models.Setting> GetAllSettingDatas()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Settings.Where(s => s.SettingCode != null)
                    .AsNoTracking()
                    .ToList();
            }
        }

    }
}