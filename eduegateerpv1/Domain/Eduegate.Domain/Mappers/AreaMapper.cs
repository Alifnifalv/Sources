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
    public class AreaMapper : IDTOEntityMapper<AreaDTO, Area>
    {
        private CallContext _context;

        public static AreaMapper Mapper(CallContext context)
        {
            var mapper = new AreaMapper();
            mapper._context = context;
            return mapper;
        }
        public AreaDTO ToDTO(Area entity)
        {
            if (entity != null)
            {
                return new AreaDTO()
                {
                    AreaID = entity.AreaID,
                    AreaName = entity.AreaName,
                    RouteID = entity.RouteID,
                    ZoneID = entity.ZoneID,
                    CityID = entity.City.IsNotNull() ? entity.CityID : null,
                    CityName = entity.City.IsNotNull() ? entity.City.CityName : null,
                    CountryID = entity.CountryID,
                    CountryName = entity.Country.IsNotNull() ? entity.Country.CountryName : null,
                    CompanyID = entity.CompanyID,
                    IsActive = entity.IsActive,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                };
            }
            else return new AreaDTO();
        }

        public List<AreaDTO> ToDTO(List<Area> entity)
        {
            if (entity != null)
            {
                var AreaDtoList = new List<AreaDTO>();
                foreach (var ent in entity){
                    var araeDTO = new AreaDTO();
                    araeDTO.AreaID = ent.AreaID;
                    araeDTO.RouteID = ent.RouteID;
                    araeDTO.AreaName = ent.AreaName;
                    araeDTO.ZoneID = ent.ZoneID;
                    araeDTO.CityID = ent.CityID;
                    //araeDTO.CityName = new MutualRepository().GetCity((int)entity.CityID).CityName.ToString();
                    araeDTO.CountryID = ent.CountryID;
                    araeDTO.CompanyID = ent.CompanyID.IsNotNull() ? ent.CompanyID : (int)_context.CompanyID;
                    araeDTO.IsActive = ent.IsActive;
                    araeDTO.CreatedBy = ent.CreatedBy;
                    araeDTO.UpdatedBy = ent.UpdatedBy;
                    araeDTO.CreatedDate = ent.CreatedDate;
                    araeDTO.UpdatedDate = ent.UpdatedDate;
                    //araeDTO.TimeStamps = ent.TimeStamps == null ? null : Convert.ToBase64String(ent.TimeStamps);
                    AreaDtoList.Add(araeDTO);
                }
                return AreaDtoList;
            }
            else return new List<AreaDTO>();
        }

        public Area ToEntity(AreaDTO dto)
        {
            if (dto != null && dto.AreaID.HasValue)
            {
                return new Area()
                {
                    AreaID = dto.AreaID.Value,
                    AreaName = dto.AreaName,
                    RouteID = dto.RouteID,
                    ZoneID = dto.ZoneID,
                    CityID = dto.CityID.IsNotNull() ? dto.CityID : null,
                    CountryID = dto.CountryID.IsNotNull() ? dto.CountryID : null,
                    CompanyID = dto.CompanyID.IsNotNull() ? dto.CompanyID : (int)_context.CompanyID,
                    IsActive = dto.IsActive,
                    CreatedBy = dto.AreaID > 0 ? dto.CreatedBy : (int)_context.LoginID,
                    UpdatedBy = dto.AreaID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = dto.AreaID > 0 ? dto.CreatedDate : DateTime.Now,
                    UpdatedDate = dto.AreaID > 0 ? DateTime.Now : dto.UpdatedDate,
                    //TimeStamps = dto.TimeStamps == null ? null : Convert.FromBase64String(dto.TimeStamps),
                };
            }
            else return new Area();
        }

    }
}
