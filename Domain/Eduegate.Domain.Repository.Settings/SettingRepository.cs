using Eduegate.Domain.Entity.Setting.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Domain.Repository.Settings
{
    public class SettingRepository
    {
        public List<View> GetViews()
        {
            using (var dbContext = new dbEduegateSettingContext())
            {
                return dbContext.Views
                    .AsNoTracking()
                    .ToList();
            }
        }

        public List<ScreenMetadata> GetCustomizedViews()
        {
            using (var dbContext = new dbEduegateSettingContext())
            {
                return dbContext.ScreenMetadatas
                    //.Where(x => x.IsCustomizationEnabled.HasValue && x.IsCustomizationEnabled.Value)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public List<ScreenMetadata> GetAIEnabledViews()
        {
            using (var dbContext = new dbEduegateSettingContext())
            {
                return dbContext.ScreenMetadatas
                    //.Where(x => x.IsAIEnabled.HasValue && x.IsAIEnabled.Value)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public Setting GetSettingDetail(string settingKey)
        {
            using (var dbContext = new dbEduegateSettingContext())
            {
                return dbContext.Settings
                    .Where(x => x.SettingCode == settingKey)
                    .AsNoTracking()
                    .FirstOrDefault();
            }
        }

        public List<Setting> GetSettingDetailsByGroupName(string groupName)
        {
            using (var dbContext = new dbEduegateSettingContext())
            {
                return dbContext.Settings
                    .Where(x => x.GroupName == groupName)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public UserSetting GetUserSettingDetail(long loginID, string settingKey)
        {
            using (var dbContext = new dbEduegateSettingContext())
            {
                return dbContext.UserSettings
                    .Where(x => x.SettingCode == settingKey && x.LoginID == loginID)
                    .AsNoTracking()
                    .FirstOrDefault();
            }
        }

        public List<UserSetting> GetAllUserSettingDetail(long loginID)
        {
            using (var dbContext = new dbEduegateSettingContext())
            {
                return dbContext.UserSettings
                    .Where(x => x.LoginID == loginID)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public Setting GetSettingDetail(string settingKey, int companyID)
        {
            using (var dbContext = new dbEduegateSettingContext())
            {
                return dbContext.Settings
                    .Where(x => x.SettingCode == settingKey && x.CompanyID == companyID)
                    .AsNoTracking()
                    .FirstOrDefault();
            }
        }

        public List<Setting> GetAllSettings(int companyID)
        {
            using (var dbContext = new dbEduegateSettingContext())
            {
                return dbContext.Settings
                    .Where(a => a.CompanyID == companyID)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public List<IntegrationParameter> GetAllIntegrationParameters()
        {
            using (var dbContext = new dbEduegateSettingContext())
            {
                return dbContext.IntegrationParameters
                    .AsNoTracking()
                    .ToList();
            }
        }

        public Setting GetSettingDetail(string settingKey, int companyID, int siteID)
        {
            using (var dbContext = new dbEduegateSettingContext())
            {
                return dbContext.Settings
                    .Where(x => x.SettingCode.Equals(settingKey) && x.CompanyID == companyID && x.SiteID == siteID)
                    .AsNoTracking()
                    .FirstOrDefault();

            }
        }

        public Setting SaveSettings(Setting entity)
        {
            using (var dbContext = new dbEduegateSettingContext())
            {
                var existing = dbContext.Settings
                    .Where(a => a.CompanyID == entity.CompanyID && a.SettingCode == entity.SettingCode)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (existing == null)
                {
                    dbContext.Settings.Add(entity);
                }
                else
                {
                    existing.SettingValue = entity.SettingValue;
                    dbContext.Entry(existing).State = EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return entity;
        }

        public UserSetting SaveUserSettings(UserSetting entity)
        {
            using (var dbContext = new dbEduegateSettingContext())
            {
                var existing = dbContext.UserSettings
                    .Where(a => a.CompanyID == entity.CompanyID && a.LoginID == entity.LoginID && a.SettingCode == entity.SettingCode)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (existing == null)
                {
                    dbContext.UserSettings.Add(entity);
                }
                else
                {
                    existing.SettingValue = entity.SettingValue;
                    dbContext.Entry(existing).State = EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return entity;
        }

        public IntegrationParameter SaveIntegrationSettings(IntegrationParameter entity)
        {
            using (var dbContext = new dbEduegateSettingContext())
            {
                var existing = dbContext.IntegrationParameters
                    .Where(a => a.ParameterName == entity.ParameterName)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (existing == null)
                {
                    dbContext.IntegrationParameters.Add(entity);
                }
                else
                {
                    existing.ParameterValue = entity.ParameterValue;
                    dbContext.Entry(existing).State = EntityState.Modified;
                }

                dbContext.SaveChanges();
            }

            return entity;
        }

        public List<ScreenFieldSetting> GetFieldSettingsByID(long screenID)
        {
            using (var dbContext = new dbEduegateSettingContext())
            {
                return dbContext.ScreenFieldSettings
                    .Where(a => a.ScreenID == screenID)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public void SaveFieldSettings(List<ScreenFieldSetting> entities)
        {
            using (var dbContext = new dbEduegateSettingContext())
            {
                foreach (var entity in entities)
                {
                    dbContext.ScreenFieldSettings.Add(entity);

                    if (entity.ScreenFieldSettingID != 0)
                    {
                        dbContext.Entry(entity).State = EntityState.Modified;
                    }
                    else
                    {
                        var maxID = dbContext.ScreenFieldSettings.AsNoTracking().Max(a => (long?)a.ScreenFieldSettingID);
                        entity.ScreenFieldSettingID = maxID != null ? (long)maxID + 1 : 1;

                        //entity.ScreenFieldSettingID =
                        //    dbContext.ScreenFieldSettings.Max(x => x.ScreenFieldSettingID) + 1;
                    }
                }

                dbContext.SaveChanges();
            }
        }

        public List<ScreenFieldSetting> GetFieldSettings(long? screenID)
        {
            using (var dbContext = new dbEduegateSettingContext())
            {
                return dbContext.ScreenFieldSettings
                    .Where(a => a.ScreenID == screenID)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public List<ScreenFieldSetting> GetFieldSettings(long? screenID, string fieldName)
        {
            using (var dbContext = new dbEduegateSettingContext())
            {
                return dbContext.ScreenFieldSettings
                    .Where(a => a.ScreenID == screenID && a.ScreenField != null && a.ScreenField.FieldName == fieldName)
                    .AsNoTracking()
                    .ToList();
            }
        }

    }
}