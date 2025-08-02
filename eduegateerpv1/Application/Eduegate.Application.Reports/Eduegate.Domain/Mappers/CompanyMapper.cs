using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Mutual;

namespace Eduegate.Domain.Mappers
{
    public class CompanyMapper : IDTOEntityMapper<CompanyDTO, Company>
    {
        private CallContext _context;

        public static CompanyMapper Mapper(CallContext context) {
            var mapper = new CompanyMapper();
            mapper._context = context;
            return mapper;
        }

        public List<CompanyDTO> ToDTO(List<Company> entities)
        {
            var dtos = new List<CompanyDTO>();
            foreach (var entity in entities)
            {
                dtos.Add(ToDTO(entity));
            }

            return dtos;
        }

        public CompanyDTO ToDTO(Company entity)
        {
            return new CompanyDTO()
            {
                CompanyID = entity.CompanyID,
                CompanyName = entity.CompanyName,
                UpdatedBy = !entity.CreatedBy.HasValue ? (int?)null : int.Parse(entity.UpdatedBy.ToString()),
                CreatedBy = !entity.CreatedBy.HasValue ? (int?)null : int.Parse(entity.CreatedBy.ToString()),
                TimeStamps = Convert.ToBase64String(entity.TimeStamps),
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
                Address = entity.Address,
                BaseCurrencyID = entity.BaseCurrencyID,
                CountryID = entity.CountryID,
                StatusID = entity.StatusID,
                ExpiryDate = entity.ExpiryDate,
                LanguageID = entity.LanguageID,
                //Logo = entity.Logo,
                RegistraionNo = entity.RegistraionNo,
                RegistrationDate = entity.RegistrationDate,
                CurrencyCode = entity.Currency.IsNotNull() ? entity.Currency.AnsiCode : string.Empty,
                DecimalPrecision = entity.Currency.IsNotNull() ? (int)entity.Currency.DecimalPrecisions : default(int),
            };
        }

        public Company ToEntity(CompanyDTO dto)
        {
            var entity = new Company()
            {
                UpdatedDate = DateTime.Now,
                TimeStamps = dto.TimeStamps == null ? null : Convert.FromBase64String(dto.TimeStamps),
                UpdatedBy = int.Parse(_context.LoginID.ToString()),
                CompanyID = dto.CompanyID,
                CompanyName = dto.CompanyName,
                Address = dto.Address,
                BaseCurrencyID = dto.BaseCurrencyID,
                ExpiryDate = dto.ExpiryDate,
                RegistrationDate = dto.RegistrationDate,
                RegistraionNo = dto.RegistraionNo,
                LanguageID = dto.LanguageID,
                CountryID = dto.CountryID,
                StatusID = dto.StatusID
            };

            if (entity.CompanyID == 0)
            {
                entity.CreatedBy = int.Parse(_context.LoginID.ToString());
                entity.CreatedDate = DateTime.Now;
            }

            return entity;
        }
    }
}
