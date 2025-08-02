using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Mappers.Security;
using Eduegate.Domain.Repository.Security;
using Eduegate.Framework;
using Eduegate.Services.Contracts.Admin;
using Eduegate.Services.Contracts.Security;

namespace Eduegate.Domain.Security
{
    public class SecurityBL 
    {
        private CallContext _context { get; set;}

        public SecurityBL(CallContext context)
        {
            _context = context;
        }

        public ClaimDTO GetClaim(long claimID)
        {
            return ClaimMapper.Mapper(_context).ToDTO(new SecurityRepository().GetClaim(claimID));
        }

        public ClaimSetDTO GetClaimSet(long claimSetID)
        {
            return ClaimSetMapper.Mapper(_context).ToDTO(new SecurityRepository().GetClaimSet(claimSetID));
        }

        public List<ClaimDTO> GetClaims(long userID)
        {
            return ClaimMapper.Mapper(_context).ToDTO(new SecurityRepository().GetUserClaims(userID));
        }

        public List<ClaimDTO> GetClaims(long userID, int claimTypeID)
        {
            return  ClaimMapper.Mapper(_context).ToDTO(new SecurityRepository().GetUserClaims(userID, claimTypeID));
        }

        public ClaimDTO SaveClaim(ClaimDTO claimDTO)
        {
            var claimDetail = new SecurityRepository().SaveClaim(ClaimMapper.Mapper(_context).ToEntity(claimDTO));
            return ClaimMapper.Mapper(_context).ToDTO(claimDetail);
        }

        public ClaimSetDTO SaveClaimSet(ClaimSetDTO claimSetDTO)
        {
            var claimSetDetail = new SecurityRepository().SaveClaimSets(ClaimSetMapper.Mapper(_context).ToEntity(claimSetDTO));
            return ClaimSetMapper.Mapper(_context).ToDTO(claimSetDetail);
        }

        public bool HasClaimSetAccess(long claimSetID, long userID)
        {
            return new SecurityRepository().HasClaimSetAccess(claimSetID, userID);
        }

        public List<UserRoleDTO> GetUserRoles(long loginID)
        {
            return LoginRoleMapMapper.Mapper(_context).ToDTO(new SecurityRepository().GetUserRoles(loginID));
        }

        public bool HasClaimSetAccessByResource(string resourceID, long userID)
        {
            return new SecurityRepository().HasClaimSetAccessByResource(resourceID, userID);
        }

        public bool HasClaimAccess(long claimID, long userID)
        {
            var hasAccess = Framework.CacheManager.MemCacheManager<bool?>
                .Get("HASCLAIMACCESS_" + claimID.ToString() + "_" + userID.ToString());

            if (!hasAccess.HasValue)
            {
                hasAccess = new SecurityRepository().HasClaimAccess(claimID, userID);
                Framework.CacheManager.MemCacheManager<bool?>.Add(hasAccess, "HASCLAIMACCESS_" + claimID.ToString() + "_" + userID.ToString());
            }

            return hasAccess.Value;
        }
    }
}
