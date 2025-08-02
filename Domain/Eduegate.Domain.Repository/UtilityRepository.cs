using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Helper;
using Eduegate.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Repository
{
    public class UtilityRepository 
    {
        public string GetCurrencyConfiguration(long ipAddress)
        {
            //var da = new CurrencyConfigurationDA<WBDbContext>();
            //return da.Query(a => a.BeginningIP <= ipAddress && a.EndingIP >= ipAddress).FirstOrDefault().TwoCountryCode;
            return null;
        }

        public static Language GetLanguageCultureId(string languageCode)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Languages.Include(i => i.Culture)
                    .Where(l => l.IsEnabled == true && l.Culture.CultureCode == languageCode)
                    .AsNoTracking()
                    .FirstOrDefault();             
            }
        }

        public static List<Language> GetLanguages()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Languages.Include(i => i.Culture).Where(x => x.IsEnabled == true)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public static List<KeyValuePair<string, string>> GetCurrencies(int companyID)
        {
            List<KeyValuePair<string, string>> currencyList = new List<KeyValuePair<string, string>>();
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var query = from c in dbContext.Currencies
                            join ccm in dbContext.CompanyCurrencyMaps on c.CurrencyID equals ccm.CurrencyID
                            where ccm.CompanyID == companyID
                            select c;
                query.AsNoTracking().ToList().ForEach(x => currencyList.Add(new KeyValuePair<string, string>(x.DisplayCode, x.AnsiCode)));
                return currencyList;
            }
        }

        public static decimal GetExchangeRate(int? companyID, string forCurrencyCode)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var query = from currencyExchange in dbContext.CompanyCurrencyMaps
                            join currencies in dbContext.Currencies on currencyExchange.CurrencyID equals currencies.CurrencyID
                            where currencyExchange.CompanyID == companyID && currencies.AnsiCode == forCurrencyCode
                            select currencyExchange;
                if (query.Any())
                    return (decimal)query.AsNoTracking().FirstOrDefault().ExchangeRate;
                else
                    return 1;
            }
        }

        public static List<Language> GetEnabledMultiLanguages()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Languages.Include(i => i.Culture).Where(l => l.IsEnabled == true).AsNoTracking().ToList();                
            }
        }

        //Get List of title master details
        public List<PropertyDTO> GetProperties(string propertyTypeIDs)
        {
            char[] splitChars = { Constants.COMMA };
            List<byte> propertyTypes = propertyTypeIDs.Split(splitChars, StringSplitOptions.RemoveEmptyEntries).Select(Byte.Parse).ToList();
            List<PropertyDTO> propertyDTOList = new List<PropertyDTO>();
            PropertyDTO propertyDTO = null;

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var propertyList = (from property in dbContext.Properties
                                    where propertyTypes.Contains((byte)property.PropertyTypeID)
                                    select property)
                                    .AsNoTracking()
                                    .ToList();

                if (propertyList != null && propertyList.Count > 0)
                {
                    foreach (var property in propertyList)
                    {
                        propertyDTO = new PropertyDTO();

                        propertyDTO.PropertyIID = property.PropertyIID;
                        propertyDTO.PropertyName = property.PropertyName;
                        propertyDTO.PropertyDescription = property.PropertyDescription;
                        propertyDTO.DefaultValue = property.DefaultValue;
                        propertyDTO.PropertyTypeID = property.PropertyTypeID;
                        propertyDTO.IsUnqiue = property.IsUnqiue;
                        propertyDTO.UIControlTypeID = property.UIControlTypeID;
                        propertyDTO.UIControlValidationID = property.UIControlValidationID;
                        propertyDTO.CreatedBy = property.CreatedBy;
                        propertyDTO.UpdatedBy = property.UpdatedBy;
                        propertyDTO.CreatedDate = property.CreatedDate;
                        propertyDTO.UpdatedDate = property.UpdatedDate;
                        //propertyDTO.TimeStamps = Convert.ToBase64String(property.TimeStamps);

                        propertyDTOList.Add(propertyDTO);
                    }
                }
            }

            return propertyDTOList;
        }

        public List<Entity.Models.PropertyType> GetPropertyTypes(string propertyTypeIDs)
        {
            char[] splitChars = { Constants.COMMA };
            List<byte> propertyTypes = propertyTypeIDs.Split(splitChars, StringSplitOptions.RemoveEmptyEntries).Select(Byte.Parse).ToList();
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return (from pt in dbContext.PropertyTypes
                        where propertyTypes.Contains((byte)pt.PropertyTypeID)
                        select pt)
                        .AsNoTracking()
                        .ToList();
            }
        }

        public static long GetCustomerID(string loginEmailID, string mobileNumber = "")
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if (!string.IsNullOrEmpty(loginEmailID))
                {
                    return (from login in dbContext.Logins
                            join cust in dbContext.Customers on login.LoginIID equals cust.LoginID
                            where login.LoginEmailID == loginEmailID
                            select cust.CustomerIID)
                            .FirstOrDefault();
                }
                else
                {
                    return (from login in dbContext.Logins
                            join cust in dbContext.Customers on login.LoginIID equals cust.LoginID
                            where cust.Telephone == mobileNumber
                            select cust.CustomerIID)
                            .FirstOrDefault();
                }
            }

        }

        public static long GetLoginID(string loginEmailID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return (from login in dbContext.Logins
                        where login.LoginEmailID == loginEmailID
                        select login.LoginIID).FirstOrDefault();
            }
        }

        public static decimal GetCurrencyPrice(CallContext context)
        {
            //dbEduegateERPContext db = new dbEduegateERPContext();
            //var ConvertedPrice = context == null || context.CurrencyCode == null ? 1 : (from c in db.Countries
            //                      where c.CurrencyCode == context.CurrencyCode && c.ConversionRate != null
            //                      select c.ConversionRate).FirstOrDefault();
            //return Convert.ToDecimal(ConvertedPrice);
            return 0;
        }

        public static Brand GetBrandDetail(long brandID)
        {
            dbEduegateERPContext db = new dbEduegateERPContext();
            Brand brand = db.Brands.Where(x => x.BrandIID == brandID)
                .AsNoTracking()
                .FirstOrDefault();
            return brand;
        }

        public static Currency GetCurrencyDetail(string ansiCode)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.Currencies.Where(c => c.AnsiCode == ansiCode)
                    .AsNoTracking()
                    .FirstOrDefault();
            }
        }

        public Currency GetCurrencyByID(long currencyID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.Currencies.Where(c => c.CurrencyID == currencyID)
                    .AsNoTracking()
                    .FirstOrDefault();
            }
        }
    }
}
