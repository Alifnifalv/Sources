using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Security;
using Eduegate.Framework.Extensions;

namespace Eduegate.Domain.Mappers.Security
{
    public class ClaimMapper : IDTOEntityMapper<ClaimDTO, Eduegate.Domain.Entity.Models.Claim>
    {
        private CallContext _context;

        public static ClaimMapper Mapper(CallContext context)
        {
            var mapper = new ClaimMapper();
            mapper._context = context;
            return mapper;
        }

        public Eduegate.Domain.Entity.Models.Claim ToEntity(ClaimDTO dto)
        {
            return new Eduegate.Domain.Entity.Models.Claim()
            {
               ClaimIID = dto.ClaimIID,
               ClaimName = dto.ClaimName,
               ClaimTypeID = dto.ClaimTypeID > 0 ? (int)dto.ClaimTypeID : (int?)null,
               ResourceName = dto.ResourceName,
               Rights = dto.Rights,
               CreatedBy = dto.CreatedBy,
               CreatedDate = dto.CreatedDate,
               UpdatedBy = dto.UpdatedBy,
               UpdatedDate = dto.UpdatedDate,
               //TimeStamps = dto.TimeStamps == null ? null : Convert.FromBase64String(dto.TimeStamps),
               CompanyID = dto.CompanyID.IsNotNull()? dto.CompanyID : _context.CompanyID
            };
        }

        public ClaimDTO ToDTO(Eduegate.Domain.Entity.Models.Claim entity)
        {
            return new ClaimDTO()
            {
                ClaimIID = entity.ClaimIID,
                ClaimName = entity.ClaimName,
                ClaimTypeID = entity.ClaimTypeID.IsNotNull() ? (ClaimType)Convert.ToInt32(entity.ClaimTypeID) : 0,
                ResourceName = entity.ResourceName,
                Rights = entity.Rights,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate,
                //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                CompanyID = entity.CompanyID.IsNotNull() ? entity.CompanyID : _context.CompanyID
            };
        }

        public List<ClaimDTO> ToDTO(List<Eduegate.Domain.Entity.Models.Claim> entities)
        {
            var claims = new List<ClaimDTO>();

            foreach (var entity in entities)
            {
                claims.Add(ToDTO(entity));
            }
            return claims;
        }
    }
}
