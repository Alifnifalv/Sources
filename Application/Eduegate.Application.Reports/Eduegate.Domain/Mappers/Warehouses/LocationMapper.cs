using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Warehouses;

namespace Eduegate.Domain.Mappers.Warehouses
{
    public class LocationMapper : IDTOEntityMapper<LocationDTO, Location>
    {
        private CallContext _context;

        public static LocationMapper Mapper(CallContext context)
        {
            var mapper = new LocationMapper();
            mapper._context = context;
            return mapper;
        }

        public LocationDTO ToDTO(Location entity)
        {
            return new LocationDTO()
            {
                UpdatedBy = entity.UpdatedBy.HasValue ? (int?)int.Parse(entity.UpdatedBy.ToString()) : null,
                CreatedBy = entity.CreatedBy.HasValue ? (int?)int.Parse(entity.CreatedBy.ToString()) : null,
                TimeStamps = Convert.ToBase64String(entity.TimeStamps),
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
                BranchID = entity.BranchID,
                Description = entity.Description,
                LocationCode = entity.LocationCode,
                LocationIID = entity.LocationIID,
                LocationTypeID = entity.LocationTypeID,
                BarCode = entity.Barcode,
                CompanyID = entity.CompanyID != null ? entity.CompanyID : _context.CompanyID
            };
        }

        public Location ToEntity(LocationDTO dto)
        {
            var entity = new Location()
            {
                UpdatedDate = DateTime.Now,
                TimeStamps = dto.TimeStamps == null ? null : Convert.FromBase64String(dto.TimeStamps),
                UpdatedBy = int.Parse(_context.LoginID.ToString()),
                LocationIID = dto.LocationIID,
                BranchID = dto.BranchID,
                Description = dto.Description,
                LocationCode = dto.LocationCode,
                LocationTypeID = dto.LocationTypeID,
                Barcode= dto.BarCode,
                CompanyID = dto.CompanyID != null ? dto.CompanyID : _context.CompanyID
            };

            if (!dto.CreatedBy.HasValue)
            {
                entity.CreatedBy = int.Parse(_context.LoginID.ToString());
                entity.CreatedDate = DateTime.Now;
            }

            return entity;
        }
    }
}
