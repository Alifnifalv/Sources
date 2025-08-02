using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts.Security;
using Eduegate.Services.Security;

namespace Eduegate.Service.Client.Direct
{
    public class SecurityServiceClient : BaseClient, ISecurityService
    {
        SecurityService service = new SecurityService();

        public SecurityServiceClient(CallContext context = null, Action<string> logger = null)
        {
            service.CallContext = context;
        }

        public ClaimDTO GetClaim(long claimID)
        {
            return service.GetClaim(claimID);
        }

        public ClaimDTO SaveClaim(ClaimDTO claim)
        {
            return service.SaveClaim(claim);
        }

        public ClaimSetDTO GetClaimSet(long claimSetID)
        {
            return service.GetClaimSet(claimSetID);
        }

        public ClaimSetDTO SaveClaimSet(ClaimSetDTO claimSet)
        {
            return service.SaveClaimSet(claimSet);
        }

        public List<ClaimDTO> GetClaims(long userID)
        {
            return service.GetClaims(userID);
        }

        public List<ClaimDTO> GetClaimsByType(long userID, Eduegate.Services.Contracts.Enums.ClaimType claimType)
        {
            return service.GetClaimsByType(userID, claimType);
        }
        public bool HasClaimSetAccess(long claimSetID, long userID)
        {
            return service.HasClaimSetAccess(claimSetID, userID);
        }

        public async Task<List<Services.Contracts.Admin.UserRoleDTO>> GetUserRoles(long loginID)
        {
            return await service.GetUserRoles(loginID);
        }

        public bool HasClaimSetAccessByResource(string resourceID, long userID)
        {
            return service.HasClaimSetAccessByResource(resourceID, userID);
        }

        public List<ClaimDTO> GetClaimsByLoginID(long? employeeID)
        {
            return service.GetClaimsByLoginID(employeeID);
        }
    }
}
