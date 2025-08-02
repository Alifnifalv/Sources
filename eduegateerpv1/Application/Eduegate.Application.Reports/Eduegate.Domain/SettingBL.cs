using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Settings;

namespace Eduegate.Domain
{
    public class SettingBL
    {
        private static SettingRepository repository = new SettingRepository();
        private CallContext _context;

        public SettingBL()
        {

        }

        public SettingBL(CallContext context)
        {
            _context = context;
        }

        public SettingDTO GetUserSettingDetail(string settingKey)
        {
            var settingDetail = Framework.CacheManager.MemCacheManager<SettingDTO>.Get("USER_SETTING_" + settingKey);
            if (settingDetail == null)
            {
                settingDetail = Mappers.Common.SettingMapper.Mapper(_context).ToDTO(repository.GetUserSettingDetail(_context.LoginID.Value, settingKey));
                Framework.CacheManager.MemCacheManager<SettingDTO>.Add(settingDetail, "USER_SETTING_" + settingKey);
            }

            return settingDetail;
        }

        public List<SettingDTO> GetAllUserSettingDetail()
        {
            //var settingDetail = Framework.CacheManager.MemCacheManager<List<SettingDTO>>.Get("ALL_USER_SETTINGS");
            //if (settingDetail == null)
            //{
                var settingDetail = Mappers.Common.SettingMapper.Mapper(_context).ToDTO(repository.GetAllUserSettingDetail(_context.LoginID.Value));
                //Framework.CacheManager.MemCacheManager<List<SettingDTO>>.Add(settingDetail, "ALL_USER_SETTINGS");
            //}

            return settingDetail;
        }

        public SettingDTO GetSettingDetail(string settingKey)
        {
            //SettingDTO settingDetail = Framework.CacheManager.MemCacheManager<SettingDTO>.Get("SETTING_" + settingKey);
            //if (settingDetail == null)
            //{
                var settingDetail = Mappers.Common.SettingMapper.Mapper().ToDTO(repository.GetSettingDetail(settingKey));
                //Framework.CacheManager.MemCacheManager<SettingDTO>.Add(settingDetail, "SETTING_" + settingKey);
            //}

            return settingDetail;
        }

        public SettingDTO GetSettingDetail(string settingKey, long companyID)
        {
            //SettingDTO settingDetail = Framework.CacheManager.MemCacheManager<SettingDTO>.Get("SETTING_" + settingKey + "_" + companyID.ToString());
            //if (settingDetail == null)
            //{
                var settingDetail = Mappers.Common.SettingMapper.Mapper(_context).ToDTO(repository.GetSettingDetail(settingKey, int.Parse(companyID.ToString())));
                //Framework.CacheManager.MemCacheManager<SettingDTO>.Add(settingDetail, "SETTING_" + settingKey + "_" + companyID.ToString());
            //}

            return settingDetail;
        }

        public SettingDTO GetSettingDetail(string settingKey, long companyID, long siteID)
        {
            SettingDTO settingDetail = Mappers.Common.SettingMapper.Mapper(_context).ToDTO(repository.GetSettingDetail(settingKey, int.Parse(companyID.ToString())));
            return settingDetail;
        }

        public List<SettingDTO> GetAllSettings()
        {
            return Eduegate.Domain.Mappers.Common.SettingMapper.Mapper(_context).ToDTO(repository.GetAllSettings(_context.CompanyID.Value));
        }

        public SettingDTO GetSettingByType(SettingType type, string referenceID, string settingKey, int companyID, int siteID)
        {
            SettingDTO settingDetail = null;

            switch (type)
            {
                case SettingType.Category:
                    var categorySetting = new CategoryRepository().GetCategorySettings(companyID, referenceID, settingKey);

                    if (categorySetting != null)
                        settingDetail = new SettingDTO()
                        {
                            CompanyID = companyID,
                            SiteID = siteID,
                            SettingCode = settingKey,
                            SettingValue = categorySetting.SettingValue
                        };
                    break;
            }

            return settingDetail;
        }

        public SettingDTO SaveSetting(SettingDTO dto)
        {
            var mapper = Mappers.Common.SettingMapper.Mapper(_context);
            return mapper.ToDTO(new SettingRepository().SaveSettings(mapper.ToEntity(dto)));
        }

        public List<ViewDTO> GetViews()
        {
            return Eduegate.Domain.Mappers.Common.ViewMapper.Mapper(_context).ToDTO(repository.GetViews());
        }

        public T GetSettingValue<T>(string settingKey, long companyID = 0, T defaultValue = default(T))
        {
            var settingDetail = Framework.CacheManager.MemCacheManager<SettingDTO>.Get("SETTING_" + settingKey);
            if (settingDetail == null || settingDetail.SettingValue == null)
            {
                settingDetail = Eduegate.Domain.Mappers.Common.SettingMapper.Mapper().ToDTO(repository.GetSettingDetail(settingKey));
                Framework.CacheManager.MemCacheManager<SettingDTO>.Add(settingDetail, "SETTING_" + settingKey);
            }

            try
            {
                Type t = typeof(T);
                t = Nullable.GetUnderlyingType(t) ?? t;

                return (settingDetail.SettingValue == null || DBNull.Value.Equals(settingDetail.SettingValue)) ?
                           defaultValue : (T)Convert.ChangeType(settingDetail.SettingValue, t);
            }
            catch (NullReferenceException)
            {
                return defaultValue;
            }
            catch (InvalidCastException)
            {
                return defaultValue;
            }

        }

        public List<SettingDTO> GetAllSettingDatas()
        {
            return Eduegate.Domain.Mappers.Common.SettingMapper.Mapper(_context).ToDTO(repository.GetAllSettingDatas());
        }

    }
}
