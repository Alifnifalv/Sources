using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Extensions;
using Eduegate.Domain.Repository;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Domain.Mappers.Countries;

namespace Eduegate.Domain
{
    public class CountryMasterBL
    {
        public List<CurrencyExchangeDTO> GetBaseCurrencyCodes()
        {
            List<CurrencyExchangeDTO> dtoList = new List<CurrencyExchangeDTO>();

            var countryRepo = new CountryMasterRepository();
            List<Company> companies = countryRepo.GetCompanies();
            List<Currency> currencies = countryRepo.GetBaseCurrencyCodes();

            foreach (Currency currency in currencies)
            {
                if (companies.Where(x => x.BaseCurrencyID == currency.CurrencyID).FirstOrDefault() != null)
                {
                    CurrencyExchangeDTO dto = new CurrencyExchangeDTO();
                    dto.CompanyID = companies.Where(x => x.BaseCurrencyID == currency.CurrencyID).FirstOrDefault().CompanyID;
                    dto.BaseCurrencyCode = currency.AnsiCode;
                    dtoList.Add(dto);
                }
            }
            return dtoList;
        }

        public bool UpdateExchangeRates(List<CurrencyExchangeDTO> exchangeRates)
        {
            var countryRepo = new CountryMasterRepository();
            int companyID = exchangeRates.FirstOrDefault().CompanyID; 

            List<CompanyCurrencyMap> companyCurrencies = new List<CompanyCurrencyMap>();
            List<Currency> currenciesToUpdate = countryRepo.GetCurrenciesByCompanyID(companyID);
            foreach(Currency curr in currenciesToUpdate)
            {
                if(exchangeRates.Any(x=>x.ExchangeCurrencyCode == curr.AnsiCode ))
                {
                    CurrencyExchangeDTO dto = exchangeRates.Where(x => x.ExchangeCurrencyCode == curr.AnsiCode).FirstOrDefault();
                    companyCurrencies.Add(new CompanyCurrencyMap()
                    {
                         CompanyID = companyID,
                         CurrencyID = curr.CurrencyID,
                         ExchangeRate = dto.ExchangeRate,
                         UpdatedDate = DateTime.Now,
                    });
                }
            }
            return new CountryMasterRepository().UpdateExchangeRates(companyCurrencies);

        }

        /// <summary>
        /// Get country masters
        /// </summary>
        /// <returns>List of country masters</returns>
        public List<CountryMasterDTO> GetCountryMasters()
        {
            List<CountryMasterDTO> countryMasterDetails = new CountryMasterRepository().GetCountryMasters();

            return countryMasterDetails;
        }

        public CountryMasterDTO GetCountryDetail(long id)
        {
            Country country = new CountryMasterRepository().GetCountryDetail(id);
            CountryMasterDTO dto = CountryMapper.ToCountryDTOMap(country);
            Currency currency = new CountryMasterRepository().GetCurrencyDetail(dto.CurrencyID);
            if (currency.IsNotNull())
            {
                dto.CurrencyCode = currency.AnsiCode;
                dto.CurrencyName = currency.Name;
                dto.CurrencyCodeDisplayText = currency.DisplayCode;
            }
            return dto;
        }

        public List<CountryDTO> GetCountries()
        {
            List<CountryDTO> countriesDTO = new List<CountryDTO>();
            CountryDTO countryDTO;
            List<Country> countries = new CountryMasterRepository().GetCountries();
            var mapper = CountriesMapper.Mapper();
            foreach (Country country in countries)
            {
                Currency currency = new CountryMasterRepository().GetCurrencyDetail(country.CurrencyID.HasValue ? country.CurrencyID.Value : 0);
                countryDTO = mapper.ToDTO(country, currency);
                countriesDTO.Add(countryDTO);
            }
            return countriesDTO;
        }

        public List<CountryDTO> GetCountriesBySiteMap(int siteID)
        {
            List<CountryDTO> countriesDTO = new List<CountryDTO>();
            CountryDTO countryDTO;
            List<Country> countries = new CountryMasterRepository().GetCountriesBySiteMap(siteID);
            var mapper = CountriesMapper.Mapper();
            foreach (Country country in countries)
            {
                Currency currency = new CountryMasterRepository().GetCurrencyDetail(country.CurrencyID.HasValue ? country.CurrencyID.Value:0);
                countryDTO = mapper.ToDTO(country, currency);
                countriesDTO.Add(countryDTO);
            }
            return countriesDTO;
        }
    }
}
