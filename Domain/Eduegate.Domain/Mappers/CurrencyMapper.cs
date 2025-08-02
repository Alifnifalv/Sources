using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts;

namespace Eduegate.Domain.Mappers
{
    public class CurrencyMapper  : DTOEntityDynamicMapper
    {

        
        public static CurrencyMapper Mapper(CallContext context)
        {
            var mapper = new CurrencyMapper();
            mapper._context = context;
            return mapper;
        }


        public CurrencyDTO ToDTO(Currency entity)
        {
            if (entity != null)
            {
                return new CurrencyDTO()
                {
                    CurrencyID = entity.CurrencyID,
                    CompanyID = entity.CompanyID,
                    AnsiCode = entity.AnsiCode,
                    DecimalPrecisions = entity.DecimalPrecisions,
                    ExchangeRate = entity.ExchangeRate,
                    IsEnabled = entity.IsEnabled,
                    ISOCode = entity.ISOCode,
                    Name = entity.Name,
                    NumericCode = entity.NumericCode,
                    Symbol = entity.Symbol
                };
            }
            else
            {
                return new CurrencyDTO();
            }
        }

        public Currency ToEntity(CurrencyDTO dto)
        {
            if (dto != null)
            {
                return new Currency()
                {
                    CurrencyID = dto.CurrencyID,
                    CompanyID = dto.CompanyID,
                    AnsiCode = dto.AnsiCode,
                    DecimalPrecisions = dto.DecimalPrecisions,
                    ExchangeRate = dto.ExchangeRate,
                    IsEnabled = dto.IsEnabled,
                    ISOCode = dto.ISOCode,
                    Name = dto.Name,
                    NumericCode = dto.NumericCode,
                    Symbol = dto.Symbol
                };
            }
            else
            {
                return new Currency();
            }
        }

        public List<CurrencyDTO> GetCurrencyDetails()
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var currencies = dbContext.Currencies
                    .AsNoTracking()
                    .Select(cs => new CurrencyDTO
                    {
                        CurrencyID = cs.CurrencyID,
                        Name = cs.Name,
                        ExchangeRate = cs.ExchangeRate,
                        DecimalPrecisions= cs.DecimalPrecisions,
                    }).ToList();

                return currencies;
            }
        }

    }
}
