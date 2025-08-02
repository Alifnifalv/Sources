using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Repository.Security;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Security;


namespace Eduegate.Domain.Mappers.Security
{
    public class ClaimSetClaimMapMapper : IDTOEntityMapper<ClaimSetClaimMapDTO, Eduegate.Domain.Entity.Models.ClaimSetClaimMap>
    {
        private CallContext _context;
        public static ClaimSetClaimMapMapper Mapper(CallContext context)
        {
            var mapper = new ClaimSetClaimMapMapper();
            mapper._context = context;
            return mapper;
        }
        public Eduegate.Domain.Entity.Models.ClaimSetClaimMap ToEntity(ClaimSetClaimMapDTO dto, long claimSetID)
        {
            return new Eduegate.Domain.Entity.Models.ClaimSetClaimMap()
            {
                ClaimSetClaimMapIID = claimSetID > 0 ?  new SecurityRepository().GetClaimSetClaimMapIID(claimSetID, dto.ClaimIID) : dto.ClaimSetClaimMapIID,
                ClaimID = dto.ClaimIID,
                ClaimSetID = claimSetID,
                CreatedBy = dto.CreatedBy,
                CreatedDate = dto.CreatedDate,
                UpdatedBy = dto.UpdatedBy,
                UpdatedDate = dto.UpdatedDate,
                //TimeStamps = dto.TimeStamps == null ? null : Convert.FromBase64String(dto.TimeStamps)
            };
        }

        public ClaimSetClaimMapDTO ToDTO(Eduegate.Domain.Entity.Models.ClaimSetClaimMap entity)
        {
            return new ClaimSetClaimMapDTO()
            {
                ClaimSetClaimMapIID = entity.ClaimSetClaimMapIID,
                ClaimIID = entity.ClaimID.IsNotNull() ? (long)entity.ClaimID : default(long),
                ClaimName = entity.Claim.ClaimName + "(" + entity.Claim.ClaimType.ClaimTypeName + ")",
                ClaimTypeID = (Eduegate.Services.Contracts.Enums.ClaimType)entity.Claim.ClaimTypeID,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate,
                //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps)
            };
        }

        ClaimSetClaimMapDTO IDTOEntityMapper<ClaimSetClaimMapDTO, Entity.Models.ClaimSetClaimMap>.ToDTO(Entity.Models.ClaimSetClaimMap entity)
        {
            throw new NotImplementedException();
        }

        public Entity.Models.ClaimSetClaimMap ToEntity(ClaimSetClaimMapDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
