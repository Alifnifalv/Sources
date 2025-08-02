using System.Collections.Generic;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Admin;

namespace Eduegate.Services.Contracts.Security
{
    public interface ISecurityService
    {
        ClaimDTO GetClaim(long claimID);
        
        ClaimDTO SaveClaim(ClaimDTO claim);

        ClaimSetDTO GetClaimSet(long claimSetID);
        
        ClaimSetDTO SaveClaimSet(ClaimSetDTO claimSet);

        List<ClaimDTO> GetClaims(long userID);

        List<ClaimDTO> GetClaimsByType(long userID, Eduegate.Services.Contracts.Enums.ClaimType claimType);

        bool HasClaimSetAccess(long claimSetID, long userID);

        bool HasClaimSetAccessByResource(string resourceID, long userID);

        Task<List<UserRoleDTO>> GetUserRoles(long loginID);

        List<ClaimDTO> GetClaimsByLoginID(long? employeeID);
    }
}