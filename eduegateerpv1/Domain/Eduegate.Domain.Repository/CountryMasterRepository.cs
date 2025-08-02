using System.Collections.Generic;
using System.Linq;
using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Services.Contracts.Eduegates;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Repository
{
    public class CountryMasterRepository
    {
        public List<Company> GetCompanies()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Companies.AsNoTracking().ToList();
            }
        }

        public List<Currency> GetBaseCurrencyCodes()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {

                return (from comp in dbContext.Companies
                        join curr in dbContext.Currencies on comp.BaseCurrencyID equals curr.CurrencyID
                        select curr).AsNoTracking().ToList();

            }
        }
        public List<Currency> GetCurrenciesByCompanyID(int companyID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return (from curr in dbContext.Currencies
                        join comCurr in dbContext.CompanyCurrencyMaps on curr.CurrencyID equals comCurr.CurrencyID
                        where comCurr.CompanyID == companyID
                        select curr).AsNoTracking().ToList();
            }
        }

        public bool UpdateExchangeRates(List<CompanyCurrencyMap> exchangeRates)
        {
            bool isSuccess = false;
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                foreach (CompanyCurrencyMap rate in exchangeRates)
                {
                    db.Entry(rate).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                db.SaveChanges();
                isSuccess = true;
            }

            return isSuccess;
        }

        //Get List of country master details
        public List<CountryMasterDTO> GetCountryMasters()
        {
            List<CountryMasterDTO> countryMasterDetails = new List<CountryMasterDTO>();
            CountryMasterDTO countryMasterDTO = null;

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                // apply where condition that ConversionRate should not null other wise currency amount will become zero
                var countryMasterList = (from currency in dbContext.CountryMasters
                                         where currency.ConversionRate != null
                                         select currency).AsNoTracking().ToList();

                foreach (var countryMaster in countryMasterList)
                {
                    countryMasterDTO = new CountryMasterDTO();

                    countryMasterDTO.CountryID = countryMaster.CountryID;
                    countryMasterDTO.CountryCode = countryMaster.CountryCode;
                    countryMasterDTO.CountryNameEn = countryMaster.CountryNameEn;
                    countryMasterDTO.NoofDecimals = countryMaster.NoofDecimals;
                    countryMasterDTO.Active = countryMaster.Active;
                    countryMasterDTO.BaseCurrency = countryMaster.BaseCurrency;
                    countryMasterDTO.ConversionRate = countryMaster.ConversionRate;
                    countryMasterDTO.CountryNameAr = countryMaster.CountryNameAr;
                    countryMasterDTO.DataFeedDateTime = countryMaster.DataFeedDateTime;
                    countryMasterDTO.Operation = countryMaster.Operation;
                    countryMasterDTO.RefGroupID = countryMaster.RefGroupID;

                    countryMasterDetails.Add(countryMasterDTO);
                }
            }

            return countryMasterDetails;
        }

        public Country GetCountryDetail(long id)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                Country country = (from c in db.Contacts
                                   join d in db.Countries on c.CountryID equals d.CountryID
                                   where c.ContactIID == id
                                   select d).AsNoTracking().FirstOrDefault();
                return country;
            }
        }

        public Currency GetCurrencyDetail(int currencyID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.Currencies.Where(x => x.CurrencyID == currencyID).AsNoTracking().FirstOrDefault();
            }
        }


        public List<Country> GetCountries()
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.Countries.AsNoTracking().ToList();
            }
        }

        public List<Country> GetCountriesBySiteMap(int siteID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var country = (from c in db.SiteCountryMaps
                                   join d in db.Countries on c.CountryID equals d.CountryID
                                   where c.SiteID == siteID
                               select d).OrderBy(x => x.CountryName).AsNoTracking().ToList();

                return country;
            }
        }

        public Country GetCountry(int countryId)
        {
            using (var db = new dbEduegateERPContext())
            {
                return db.Countries.Where(x=> x.CountryID == countryId).AsNoTracking().FirstOrDefault();
            }
        }

    }
}
