using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Security;

namespace Eduegate.Domain.Mappers.Security
{
    public class ClaimSetMapper : IDTOEntityMapper<ClaimSetDTO, Eduegate.Domain.Entity.Models.ClaimSet>
    {
        private CallContext _context;

        public static ClaimSetMapper Mapper(CallContext context)
        {
            var mapper = new ClaimSetMapper();
            mapper._context = context;
            return mapper;
        }

        public Eduegate.Domain.Entity.Models.ClaimSet ToEntity(ClaimSetDTO dto)
        {
            //var claimSets = dto.ClaimSets.Select(x=> ClaimSetClaimSetMapMapper.Mapper(_context).ToEntity(x, dto.ClaimSetIID)).ToList();
            var claimSets = new List<Eduegate.Domain.Entity.Models.ClaimSetClaimSetMap>(); 
            var claims = new List<Eduegate.Domain.Entity.Models.ClaimSetClaimMap>(); 

            //we should avoid duplication.
            if (dto.ClaimSets != null)
            {
                var mapper = ClaimSetClaimSetMapMapper.Mapper(_context);
                foreach (var claimSet in dto.ClaimSets)
                {
                    var entity = mapper.ToEntity(claimSet, dto.ClaimSetIID);

                    if (entity.ClaimSetClaimSetMapIID == 0 || !claimSets.Exists(a => a.ClaimSetClaimSetMapIID == entity.ClaimSetClaimSetMapIID))
                        claimSets.Add(entity);
                }
            }

            if (dto.Claims != null)
            {
                var mapper = ClaimSetClaimMapMapper.Mapper(_context);
                foreach (var claim in dto.Claims)
                {
                    var entity = mapper.ToEntity(claim, dto.ClaimSetIID);
                    if (entity.ClaimSetClaimMapIID == 0 || !claims.Exists(a => a.ClaimSetClaimMapIID == entity.ClaimSetClaimMapIID))
                        claims.Add(entity);
                }
            }

            return new Eduegate.Domain.Entity.Models.ClaimSet()
            {
                ClaimSetIID = dto.ClaimSetIID,
                ClaimSetName = dto.ClaimSetName,
                ClaimSetClaimMaps = claims,
                ClaimSetClaimSetMaps = claimSets,
                CreatedBy = dto.ClaimSetIID <= 0 ? (int)_context.LoginID : dto.CreatedBy,
                CreatedDate =dto.ClaimSetIID <= 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedBy = int.Parse(_context.LoginID.ToString()),
                UpdatedDate =DateTime.Now,
                TimeStamps = dto.TimeStamps == null ? null : Convert.FromBase64String(dto.TimeStamps),
                CompanyID = dto.CompanyID != null? dto.CompanyID : _context.CompanyID
            };
        }

        public ClaimSetDTO ToDTO(Eduegate.Domain.Entity.Models.ClaimSet entity)
        {
            return new ClaimSetDTO()
            {
                ClaimSetIID = entity.ClaimSetIID,
                ClaimSetName = entity.ClaimSetName,
                Claims = entity.ClaimSetClaimMaps != null ? entity.ClaimSetClaimMaps.Select(x=> ClaimSetClaimMapMapper.Mapper(_context).ToDTO(x)).ToList() : null,
                ClaimSets = entity.ClaimSetClaimSetMaps!= null ?  entity.ClaimSetClaimSetMaps.Select( x=> ClaimSetClaimSetMapMapper.Mapper(_context).ToDTO(x)).ToList() : null,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate,
                TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                CompanyID = entity.CompanyID !=null ? entity.CompanyID : _context.CompanyID
            };
        }
    }
}
