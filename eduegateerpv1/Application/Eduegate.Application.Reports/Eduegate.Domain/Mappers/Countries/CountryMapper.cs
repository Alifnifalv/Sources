using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts;
using Eduegate.Domain.Entity.Models;

namespace Eduegate.Domain.Mappers.Countries
{
    public class CountriesMapper : IDTOEntityMapper<CountryDTO, Country>
    {
        public static CountriesMapper Mapper()
        {
            var mapper = new CountriesMapper();
            return mapper;
        }

        public CountryDTO ToDTO(Country entity)
        {
            return null;
        }
        public CountryDTO ToDTO(Country entity, Currency currencyEntity)
        {
            if (entity != null)
            {
                var dto = new CountryDTO()
                {
                    CountryID = entity.CountryID,
                    CountryCode = entity.ThreeLetterCode,
                    CountryName = entity.CountryName,
                    CurrencyID = entity.CurrencyID.HasValue?(int)entity.CurrencyID:0
                };

                return dto;
            }
            else
                return null;
        }

        public Country ToEntity(CountryDTO DTO)
        {
            return null;
        }
    }
}
