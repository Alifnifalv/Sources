using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Framework.Extensions;

namespace Eduegate.Domain.Mappers
{
    public class CityMapper : IDTOEntityMapper<CityDTO, City>
    {
        private CallContext _context;

        public static CityMapper Mapper(CallContext context)
        {
            var mapper = new CityMapper();
            mapper._context = context;
            return mapper;
        }
        public CityDTO ToDTO(City entity)
        {
            if (entity != null)
            {
                return new CityDTO()
                {
                    CityID = entity.CityID,
                    CountryID = entity.CountryID,
                    CityName = entity.CityName,
                    IsActive = entity.IsActive,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                    CompanyID = entity.CompanyID != null ? entity.CompanyID : (_context.IsNotNull() ? _context.CompanyID : (int?)null)
                };
            }
            else return new CityDTO();
        }

        public List<CityDTO> ToDTOList(List<City> entity)
        {
            var cityDtoList = new List<CityDTO>();
            if (entity != null)
            {
                foreach (var item in entity)
                {
                    cityDtoList.Add(ToDTO(item));
                }
            }
            return cityDtoList;
        }

        public City ToEntity(CityDTO dto)
        {
            if (dto != null)
            {
                return new City()
                {
                    CityID = dto.CityID,
                    CountryID = (int) dto.CountryID,
                    CityName = dto.CityName,
                    IsActive = dto.IsActive,
                    CreatedBy = dto.CityID > 0 ? dto.CreatedBy : (int)_context.LoginID,
                    UpdatedBy = dto.CityID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = dto.CityID > 0 ? dto.CreatedDate : DateTime.Now,
                    UpdatedDate = dto.CityID > 0 ? DateTime.Now : dto.UpdatedDate,
                    //TimeStamps = dto.TimeStamps == null ? null : Convert.FromBase64String(dto.TimeStamps),
                    CompanyID = dto.CompanyID !=null  ? dto.CompanyID : ( _context.IsNotNull() ? _context.CompanyID : (int?) null)
                };
            }
            else return new City();
        }

    }
}
