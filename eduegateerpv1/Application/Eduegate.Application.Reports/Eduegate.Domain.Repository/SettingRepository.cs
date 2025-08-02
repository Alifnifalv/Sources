using System.Collections.Generic;
using System.Linq;
using Eduegate.Domain.Entity.Models;

namespace Eduegate.Domain.Repository
{
    public class SettingRepository
    {
        public List<View> GetViews()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Views.ToList();
            }
        }
        public Setting GetSettingDetail(string settingKey)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                return (from setting in dbContext.Settings
                        where setting.SettingCode.Equals(settingKey)
                        select setting).FirstOrDefault();
            }
        }

        public UserSetting GetUserSettingDetail(long loginID, string settingKey)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return (from setting in dbContext.UserSettings
                                  where setting.SettingCode.Equals(settingKey) && setting.LoginID == loginID
                                  select setting).FirstOrDefault();

            }
        }

        public List<UserSetting> GetAllUserSettingDetail(long loginID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return (from setting in dbContext.UserSettings
                                  where setting.LoginID == loginID
                                  select setting).ToList();

            }
        }

        public Setting GetSettingDetail(string settingKey, int companyID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                return (from setting in dbContext.Settings
                        where setting.SettingCode.Equals(settingKey)
                        && setting.CompanyID == companyID
                        select setting).FirstOrDefault();

            }
        }

        public List<Setting> GetAllSettings(int companyID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Settings.Where(a=> a.CompanyID == companyID).ToList();
            }
        }

        public Setting GetSettingDetail(string settingKey, int companyID, int siteID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                return (from setting in dbContext.Settings
                        where setting.SettingCode.Equals(settingKey)
                        && setting.CompanyID == companyID
                        && setting.SiteID == siteID
                        select setting).FirstOrDefault();

            }
        }

        public Setting SaveSettings(Setting entity)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var existing = dbContext.Settings.Where(a => a.CompanyID == entity.CompanyID && a.SettingCode == entity.SettingCode).FirstOrDefault();

                if (existing == null) {
                    dbContext.Settings.Add(entity);
                }
                else
                {
                    existing.SettingValue = entity.SettingValue;
                    dbContext.Entry(existing).State = System.Data.Entity.EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return entity;
        }

        public List<Setting> GetAllSettingDatas()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Settings.Where(s => s.SettingCode != null).ToList();
            }
        }

    }
}