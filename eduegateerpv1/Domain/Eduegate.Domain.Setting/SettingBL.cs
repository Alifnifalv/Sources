using Eduegate.Domain.Repository.Settings;
using Eduegate.Domain.Setting.Mappers;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Framework;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Services.Contracts.School.Mutual;
using Eduegate.Services.Contracts.Settings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Domain.Setting
{
    public class SettingBL
    {
        private static SettingRepository repository = new SettingRepository();
        private CallContext _context;

        public SettingBL(CallContext context = null)
        {
            _context = context;
        }

        public SettingDTO GetSettingByType(SettingType type, string referenceID, string settingKey, int companyID, int siteID)
        {
            SettingDTO settingDetail = null;

            switch (type)
            {
                case SettingType.Category:
                    //var categorySetting = new CategoryBL().GetCategorySettings(companyID, referenceID, settingKey);

                    //if (categorySetting != null)
                    //    settingDetail = new SettingDTO()
                    //    {
                    //        CompanyID = companyID,
                    //        SiteID = siteID,
                    //        SettingCode = settingKey,
                    //        SettingValue = categorySetting.SettingValue
                    //    };
                    break;
            }

            return settingDetail;
        }

        public SettingDTO GetUserSettingDetail(string settingKey)
        {
            var settingDetail = Framework.CacheManager.MemCacheManager<SettingDTO>.Get("USER_SETTING_" + settingKey);
            if (settingDetail == null)
            {
                settingDetail = SettingMapper.Mapper(_context).ToDTO(repository.GetUserSettingDetail(_context.LoginID.Value, settingKey));
                Framework.CacheManager.MemCacheManager<SettingDTO>.Add(settingDetail, "USER_SETTING_" + settingKey);
            }

            return settingDetail;
        }

        public List<SettingDTO> GetAllUserSettingDetail()
        {
            var settingDetail = Framework.CacheManager.MemCacheManager<List<SettingDTO>>.Get("ALL_USER_SETTINGS");
            if (settingDetail == null)
            {
                settingDetail = SettingMapper.Mapper(_context).ToDTO(repository.GetAllUserSettingDetail(_context.LoginID.Value));
                Framework.CacheManager.MemCacheManager<List<SettingDTO>>.Add(settingDetail, "ALL_USER_SETTINGS");
            }

            return settingDetail;
        }

        public SettingDTO GetSettingDetail(string settingKey)
        {
            var settingDetail = Framework.CacheManager.MemCacheManager<SettingDTO>.Get("SETTING_" + settingKey);
            if (settingDetail == null)
            {
                settingDetail = SettingMapper.Mapper().ToDTO(repository.GetSettingDetail(settingKey));
                Framework.CacheManager.MemCacheManager<SettingDTO>.Add(settingDetail, "SETTING_" + settingKey);
            }

            return settingDetail;
        }

        public T GetSettingValue<T>(string settingKey)
        {
            return GetSettingValue<T>(settingKey, 0, default(T));
        }

        public T GetSettingValue<T>(string settingKey, T defaultValue = default(T))
        {
            return GetSettingValue<T>(settingKey, 0, defaultValue);
        }

        public T GetSettingValue<T>(string settingKey, long companyID = 0, T defaultValue = default(T))
        {
            var settingDetail = Framework.CacheManager.MemCacheManager<SettingDTO>.Get("SETTING_" + settingKey);
            if (settingDetail == null || settingDetail.SettingValue == null)
            {
                var settingEntity = repository.GetSettingDetail(settingKey);
                if (settingEntity != null)
                {
                    settingDetail = SettingMapper.Mapper().ToDTO(settingEntity);
                }
                else
                {
                    settingDetail = new SettingDTO()
                    {
                        SettingCode = settingKey,
                        SettingValue = defaultValue == null ? null : defaultValue.ToString(),
                        CompanyID = 0
                    };
                }

                if (settingKey != "REPORTING_SERVICE" && settingKey != "ReportDateFormat")
                {
                    Framework.CacheManager.MemCacheManager<SettingDTO>.Add(settingDetail, "SETTING_" + settingKey);
                }
            }

            try
            {
                Type t = typeof(T);
                t = Nullable.GetUnderlyingType(t) ?? t;

                if (typeof(T).IsEnum)
                    return (T)Enum.Parse(typeof(T), settingDetail.SettingValue);

                return (settingDetail == null || settingDetail.SettingValue == null || DBNull.Value.Equals(settingDetail.SettingValue)) ?
                           defaultValue : (T)Convert.ChangeType(settingDetail.SettingValue, t);
            }
            catch (Exception)
            {
                settingDetail.SettingValue = defaultValue.ToString();
                Framework.CacheManager.MemCacheManager<SettingDTO>.Add(settingDetail, "SETTING_" + settingKey);
                return defaultValue;
            }
        }

        public SettingDTO GetSettingDetail(string settingKey, long companyID)
        {
            var settingDetail = Framework.CacheManager.MemCacheManager<SettingDTO>.Get("SETTING_" + settingKey + "_" + companyID.ToString());
            if (settingDetail == null)
            {
                settingDetail = SettingMapper.Mapper(_context).ToDTO(repository.GetSettingDetail(settingKey, int.Parse(companyID.ToString())));
                Framework.CacheManager.MemCacheManager<SettingDTO>.Add(settingDetail, "SETTING_" + settingKey + "_" + companyID.ToString());
            }

            return settingDetail;
        }

        public SettingDTO GetSettingDetail(string settingKey, long companyID, long siteID)
        {
            var settingDetail = SettingMapper.Mapper(_context).ToDTO(repository.GetSettingDetail(settingKey, int.Parse(companyID.ToString())));
            return settingDetail;
        }

        public List<SettingDTO> GetAllSettings()
        {
            return SettingMapper.Mapper(_context).ToDTO(repository.GetAllSettings(_context.CompanyID.Value));
        }

        public List<IntegrationParamterDTO> GetAllIntegrationSettings()
        {
            return Eduegate.Domain.Setting.Mappers.IntegrationParameterMapper.Mapper(_context).ToDTO(repository.GetAllIntegrationParameters());
        }

        public List<SettingDTO> GetSettingByGroup(string groupName)
        {
            var dtos = new List<SettingDTO>();
            var settings = new SettingRepository().GetSettingDetailsByGroupName(groupName);

            foreach (var setting in settings)
            {
                dtos.Add(new SettingDTO()
                {
                    CompanyID = setting.CompanyID,
                    SiteID = setting.SiteID,
                    SettingCode = setting.SettingCode,
                    SettingValue = setting.SettingValue
                });
            }

            return dtos;
        }

        public UserSettingDTO SaveUserSettingValue(string settingKey, string settingValue)
        {
            var settingEntity = repository.GetUserSettingDetail(_context.LoginID.Value, settingKey);
            if (settingEntity == null)
            {
                settingEntity = new Entity.Setting.Models.UserSetting()
                {
                    LoginID = _context.LoginID.Value,
                    CompanyID = 1,
                    SettingCode = settingKey,
                    SettingValue = settingValue
                };
            }

            settingEntity.SettingValue = settingValue;
            var updateEntity = new SettingRepository().SaveUserSettings(settingEntity);
            return new UserSettingDTO()
            {
                CompanyID = settingEntity.CompanyID,
                SiteID = settingEntity.SiteID,
                SettingCode = settingEntity.SettingCode,
                SettingValue = settingEntity.SettingValue,
                LoginID = settingEntity.LoginID
            };
        }

        public List<UserSettingDTO> GetUserSettings(long loginID)
        {
            var settingDetails = repository.GetAllUserSettingDetail(loginID);
            return settingDetails.Select(x => new UserSettingDTO()
            {
                CompanyID = x.CompanyID,
                Description = x.Description,
                GroupName = x.GroupName,
                LoginID = x.LoginID,
                SettingValue = x.SettingValue,
                SettingCode = x.SettingCode,
            }).ToList();
        }

        public SettingDTO SaveSettingValue(string settingKey, string settingValue)
        {
            var settingEntity = repository.GetSettingDetail(settingKey);
            if (settingEntity == null)
            {
                settingEntity = new Entity.Setting.Models.Setting()
                {
                    CompanyID = 1,
                    SettingCode = settingKey,
                    SettingValue = settingValue
                };
            }

            settingEntity.SettingValue = settingValue;
            var mapper = SettingMapper.Mapper(_context);
            return mapper.ToDTO(new SettingRepository().SaveSettings(settingEntity));
        }

        public SettingDTO SaveSetting(SettingDTO dto)
        {
            var mapper = SettingMapper.Mapper(_context);
            return mapper.ToDTO(new SettingRepository().SaveSettings(mapper.ToEntity(dto)));
        }

        public IntegrationParamterDTO SaveIntegrationSettings(IntegrationParamterDTO dto)
        {
            var mapper = Eduegate.Domain.Setting.Mappers.IntegrationParameterMapper.Mapper(_context);
            return mapper.ToDTO(new SettingRepository().SaveIntegrationSettings(mapper.ToEntity(dto)));
        }

        public List<ViewDTO> GetViews()
        {
            //return ViewMapper2.Mapper(_context).ToDTO(repository.GetViews());
            return null;
        }

        public List<KeyValueDTO> GetCustomizedViews()
        {
            var dto = new List<KeyValueDTO>();

            foreach (var value in repository.GetCustomizedViews())
            {
                dto.Add(new KeyValueDTO()
                {
                    Key = value.ScreenID.ToString(),
                    Value = string.IsNullOrEmpty(value.DisplayName) ?
                    value.Name : value.DisplayName + '(' + value.Name + ')'
                });
            }

            return dto;
        }

        public List<KeyValueDTO> GetAIEnabledViews()
        {
            var dto = new List<KeyValueDTO>();

            foreach (var value in repository.GetAIEnabledViews())
            {
                dto.Add(new KeyValueDTO()
                {
                    Key = value.ScreenID.ToString(),
                    Value = string.IsNullOrEmpty(value.DisplayName) ?
                    value.Name : value.DisplayName + '(' + value.Name + ')'
                });
            }

            return dto;
        }

        public void SaveFieldSettings(List<ScreenFieldSettingDTO> dtos)
        {
            var repository = new SettingRepository();
            var mapper = SettingMapper.Mapper(_context);
            var firstSetting = dtos.FirstOrDefault();
            var screenID = firstSetting != null ? firstSetting.ScreenID : (long?)null;

            repository.SaveFieldSettings(
                mapper.SaveToFieldSettingEntity(dtos,
                screenID.HasValue ? repository.GetFieldSettingsByID(screenID.Value) : null));
        }

        public List<ScreenFieldSettingDTO> GetFieldSettings(long screenID)
        {
            var mapper = SettingMapper.Mapper(_context);
            return mapper.ToDTO(new SettingRepository().GetFieldSettings(screenID));
        }

        public List<ScreenFieldSettingDTO> GetFieldSettingsByField(long screenID, long screenFieldID, string fieldName)
        {
            var mapper = SettingMapper.Mapper(_context);
            return mapper.ToDTO(new SettingRepository().GetFieldSettings(screenID, fieldName));
        }

        public ClassDTO GetClassDetailByClassID(int? classID)
        {
            if (classID.HasValue)
            {
                var classDetail = Framework.CacheManager.MemCacheManager<ClassDTO>.Get("CLASSDETAILS_" + classID);
                if (classDetail == null || string.IsNullOrEmpty(classDetail.Code))
                {
                    var classDet = SettingMapper.Mapper(_context).GetClassDetailsByClassID(classID.Value);
                    if (classDet != null)
                    {
                        classDetail = new ClassDTO()
                        {
                            ClassID = classDet.ClassID,
                            ClassDescription = classDet.ClassDescription,
                            Code = classDet.Code,
                            SchoolID = classDet.SchoolID,
                            ORDERNO = classDet.ORDERNO,
                        };
                    }
                    else
                    {
                        classDetail = new ClassDTO()
                        {
                            ClassID = classID.Value,
                            ClassDescription = null,
                            Code = null,
                            SchoolID = null,
                            ORDERNO = null,
                        };
                    }

                    Framework.CacheManager.MemCacheManager<ClassDTO>.Add(classDetail, "CLASSDETAILS_" + classID);
                }

                return classDetail;
            }
            else
            {
                return null;
            }
        }

        public SchoolsDTO GetSchoolDetailByID(byte? schoolID)
        {
            if (schoolID.HasValue)
            {
                var schoolDetail = Framework.CacheManager.MemCacheManager<SchoolsDTO>.Get("SCHOOLDETAILS_" + schoolID);
                if (schoolDetail == null || string.IsNullOrEmpty(schoolDetail.SchoolShortName))
                {
                    var schoolDet = SettingMapper.Mapper(_context).GetSchoolDetailByID(schoolID.Value);
                    if (schoolDet != null)
                    {
                        schoolDetail = new SchoolsDTO()
                        {
                            SchoolID = schoolDet.SchoolID,
                            SchoolCode = schoolDet.SchoolCode,
                            SchoolName = schoolDet.SchoolName,
                            Description = schoolDet.Description,
                            SchoolShortName = schoolDet.SchoolShortName,
                        };
                    }
                    else
                    {
                        schoolDetail = new SchoolsDTO()
                        {
                            SchoolID = schoolID.Value,
                            SchoolCode = null,
                            SchoolName = null,
                            Description = null,
                            SchoolShortName = null,
                        };
                    }

                    Framework.CacheManager.MemCacheManager<SchoolsDTO>.Add(schoolDetail, "SCHOOLDETAILS_" + schoolID);
                }

                return schoolDetail;
            }
            else
            {
                return null;
            }
        }

    }
}