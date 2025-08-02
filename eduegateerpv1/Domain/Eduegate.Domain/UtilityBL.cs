using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Services.Contracts;
using Eduegate.Domain.Mappers;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Framework.CacheManager;
using Eduegate.Domain.CachItems;

namespace Eduegate.Domain
{
    public class UtilityBL
    {
        private static UtilityRepository utilityManagement = new UtilityRepository();
        UtilityRepository utilityRepository = new UtilityRepository();

        public static string GetCurrencyConfiguration(long ipAddress)
        {
            return utilityManagement.GetCurrencyConfiguration(ipAddress);
        }

        public static List<LanguageDTO> GetLanguageList()
        {
            var languageList = new List<LanguageDTO>();
            var languageListDB = UtilityRepository.GetLanguages();
            foreach (var langDB in languageListDB)
            {
                var langDTO = new LanguageDTO
                {
                    LanguageCode = langDB.LanguageCodeThreeLetter,
                    DisplayText = langDB.Language1
                };

                languageList.Add(langDTO);
            }
            return languageList;
        }

        public static List<KeyValuePair<string, string>> GetCurrencies(int companyID)
        {
            return UtilityRepository.GetCurrencies(companyID);
        }

        public static List<LanguageDTO> GetLanguages()
        {
            var languageList = new List<LanguageDTO>();
            var cache = Eduegate.Framework.CacheManager.DataCacheManager<LanguageCaches>.SetContexts(null, RepositoryTypes.Data);
            var languages = cache.Get("Languages");

            if (languages == null)
            {
                var lngaugEntities = UtilityRepository.GetLanguages();

                foreach (var langDB in lngaugEntities)
                {
                    languageList.Add(new LanguageDTO
                    {
                        CultureID = (byte)langDB.CultureID,
                        LanguageID = langDB.LanguageID,
                        LanguageCode = langDB.Culture.CultureCode,
                        DisplayText = langDB.Language1
                    });
                }

                cache.Set(new CachItems.LanguageCaches("Languages") { Languages = languageList });
            }
            else
            {
                languageList = languages.Languages;
            }

            return languageList;
        }

        public LanguageDTO GetLanguageCultureId(string languageCode)
        {
            LanguageDTO languageDTO;
            var cache = DataCacheManager<LanguageCaches>.SetContexts(null, RepositoryTypes.Data);

            var multiLanguageList = cache.Get("Language_" + languageCode);

            if (multiLanguageList == null)
            {
                var languageEntity = UtilityRepository.GetLanguageCultureId(languageCode);
                if (languageEntity != null)
                {
                    languageDTO = new LanguageDTO()
                    {
                        CultureID = (byte)languageEntity.CultureID,
                        LanguageID = languageEntity.LanguageID,
                        LanguageCode = languageEntity.Culture.CultureCode,
                        DisplayText = languageEntity.Language1,
                    };
                }
                else { languageDTO = null; }

                cache.Set(new LanguageCaches("Language_" + languageCode) { Languages = new List<LanguageDTO>() { languageDTO } });
            }
            else
            {
                languageDTO = multiLanguageList.Languages[0];
            }

            return languageDTO;
        }

        /// <summary>
        /// Get title masters
        /// </summary>
        /// <returns>List of title masters</returns>
        public List<PropertyDTO> GetProperties(string propertyTypeIDs)
        {
            List<PropertyDTO> propertyDTOList = utilityRepository.GetProperties(propertyTypeIDs);
            return propertyDTOList;
        }

        public List<PropertyDTO> GetPropertyTypes(string propertyTypeIDs)
        {
            return PropertyTypeToDTO(new UtilityRepository().GetPropertyTypes(propertyTypeIDs));
        }

        public string GetCustomerID(CallContext callContext)
        {
            return Convert.ToString(UtilityRepository.GetCustomerID(callContext.EmailID));
        }

        public MenuBrandDTO GetBrandDetail(long brandID)
        {
            Brand brand = UtilityRepository.GetBrandDetail(brandID);
            return ToBrandDTOMap(brand);
        }

        public static MenuBrandDTO ToBrandDTOMap(Brand brand)
        {
            return new MenuBrandDTO()
            {
                BrandIID = brand.BrandIID,
                BrandName = brand.BrandName,
                Descirption = brand.Descirption,
                LogoFile = brand.LogoFile,
            };
        }

        private List<PropertyDTO> PropertyTypeToDTO(List<PropertyType> entityList)
        {
            var propertyTypeList = new List<PropertyDTO>();
            foreach (var proprtyType in entityList)
            {
                propertyTypeList.Add(new PropertyDTO
                {
                    PropertyTypeID = proprtyType.PropertyTypeID,
                    PropertyTypeName = proprtyType.PropertyTypeName,
                });
            }
            return propertyTypeList;
        }

        public CurrencyDTO GetCurrencyDetail(string ansiCode)
        {
            return  new CurrencyDTO() { Name=null,CurrencyID=0};// CurrencyMapper.ToDTO(UtilityRepository.GetCurrencyDetail(ansiCode));
        }
         
        public string GetAutoGeneratedSerialNumber()
        {
            return Guid.NewGuid().ToString().ToUpper();
        }

        public decimal GetExchangeRate(int companyID, string forCurrencyCode)
        {
            return UtilityRepository.GetExchangeRate(companyID, forCurrencyCode);
        }

        public CurrencyDTO GetCurrencyByID(long currencyID)
        {
            return new CurrencyDTO() { Name = null, CurrencyID = 0 };//CurrencyMapper.Mapper(CallContext).ToDTO(new UtilityRepository().GetCurrencyByID(currencyID));
        }

    }
}
