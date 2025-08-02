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
    public class ClaimSetClaimSetMapMapper : IDTOEntityMapper<ClaimSetClaimSetMapDTO, Eduegate.Domain.Entity.Models.ClaimSetClaimSetMap>
    {
        private CallContext _context;
        public static ClaimSetClaimSetMapMapper Mapper(CallContext context)
        {
            var mapper = new ClaimSetClaimSetMapMapper();
            mapper._context = context;
            return mapper;
        }

        public Eduegate.Domain.Entity.Models.ClaimSetClaimSetMap ToEntity(ClaimSetClaimSetMapDTO dto, long claimSetID)
        {
            return new Eduegate.Domain.Entity.Models.ClaimSetClaimSetMap()
            {
                ClaimSetClaimSetMapIID = claimSetID > 0 ? new SecurityRepository().GetClaimSetClaimSetMapIID(claimSetID, dto.ClaimSetIID) : dto.ClaimSetClaimSetMapIID,
                ClaimSetID =claimSetID,
                LinkedClaimSetID = dto.ClaimSetIID,
                CreatedBy = dto.CreatedBy,
                CreatedDate = dto.CreatedDate,
                UpdatedBy = dto.UpdatedBy,
                UpdatedDate = dto.UpdatedDate,
                //TimeStamps = dto.TimeStamps == null ? null : Convert.FromBase64String(dto.TimeStamps)
            };
        }

        public ClaimSetClaimSetMapDTO ToDTO(Eduegate.Domain.Entity.Models.ClaimSetClaimSetMap entity)
        {
            return new ClaimSetClaimSetMapDTO()
            {
                ClaimSetClaimSetMapIID = entity.ClaimSetClaimSetMapIID,
                ClaimSetIID = entity.LinkedClaimSetID.IsNotNull() ? (long)entity.LinkedClaimSetID : default(long),
                ClaimSetName = new SecurityRepository().GetClaimSetName((long)entity.LinkedClaimSetID),
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate,
                //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps)
            };
        }


        public Entity.Models.ClaimSetClaimSetMap ToEntity(ClaimSetClaimSetMapDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
