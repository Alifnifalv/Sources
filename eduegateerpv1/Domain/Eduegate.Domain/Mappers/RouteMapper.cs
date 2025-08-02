using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Distributions;
using Eduegate.Framework.Extensions;
using Eduegate.Domain.Repository;

namespace Eduegate.Domain.Mappers
{
    public class RouteMapper : IDTOEntityMapper<RouteDTO, Route>
    {
        private CallContext _context;

        public static RouteMapper Mapper(CallContext context)
        {
            var mapper = new RouteMapper();
            mapper._context = context;
            return mapper;
        }
        public RouteDTO ToDTO(Route entity)
        {
            if (entity != null)
            {
                return new RouteDTO()
                {
                    RouteID = entity.RouteID,
                    Description = entity.Description,
                    CountryID = entity.Country.IsNotNull() ? entity.CountryID : null,
                    CountryName = entity.Country.IsNotNull() ? entity.Country.CountryName : null,
                    WarehouseID = entity.WarehouseID,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                    CompanyID = entity.CompanyID.IsNotNull() ? entity.CompanyID : _context.CompanyID
                };
            }
            else return new RouteDTO();
        }

        

        public Route ToEntity(RouteDTO dto)
        {
            if (dto != null)
            {
                return new Route()
                {
                    RouteID = dto.RouteID,
                    Description = dto.Description,
                    CountryID = dto.CountryID.IsNotNull() ? dto.CountryID : null,
                    WarehouseID = dto.WarehouseID,
                    CreatedBy = dto.CreatedBy.IsNotNull() ? dto.CreatedBy : (int)_context.LoginID,
                    CreatedDate = dto.CreatedDate.IsNotNull() ? dto.CreatedDate : DateTime.Now,
                    UpdatedBy = (int)_context.LoginID,
                    UpdatedDate = DateTime.Now,
                    //TimeStamps = dto.TimeStamps == null ? null : Convert.FromBase64String(dto.TimeStamps),
                    CompanyID = dto.CompanyID.IsNotNull() ? dto.CompanyID : _context.CompanyID
                };
            }
            else return new Route();
        }

    }
}
