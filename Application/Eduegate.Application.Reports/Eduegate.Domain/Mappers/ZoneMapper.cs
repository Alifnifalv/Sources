using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Mutual;

namespace Eduegate.Domain.Mappers
{
    public class ZoneMapper : IDTOEntityMapper<ZoneDTO, Zone>
    {
        private CallContext _context;

        public static ZoneMapper Mapper(CallContext context)
        {
            var mapper = new ZoneMapper();
            mapper._context = context;
            return mapper;
        }
        public ZoneDTO ToDTO(Zone entity)
        {
            if (entity != null)
            {
                return new ZoneDTO()
                {
                    ZoneID = entity.ZoneID,
                    ZoneName = entity.ZoneName,
                    CountryID = entity.CountryID,
                    CountryName = entity.Country.IsNotNull() ? entity.Country.CountryName : null,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                    CompanyID = entity.CompanyID !=null ? entity.CompanyID : _context.CompanyID
                };
            }
            else return new ZoneDTO();
        }

        public Zone ToEntity(ZoneDTO dto)
        {
            if (dto != null)
            {
                return new Zone()
                {
                    ZoneID = dto.ZoneID,
                    CountryID = dto.CountryID,
                    ZoneName = dto.ZoneName,
                    CreatedBy = dto.ZoneID > 0 ? dto.CreatedBy : (int)_context.LoginID,
                    UpdatedBy = dto.ZoneID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = dto.ZoneID > 0 ? dto.CreatedDate : DateTime.Now,
                    UpdatedDate = dto.ZoneID > 0 ? DateTime.Now : dto.UpdatedDate,
                    TimeStamps = dto.TimeStamps == null ? null : Convert.FromBase64String(dto.TimeStamps),
                    CompanyID = dto.CompanyID.IsNotNull() ? dto.CompanyID : _context.CompanyID
                };
            }
            else return new Zone();
        }

    }
}
