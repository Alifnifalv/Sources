using System.ServiceModel;
using Eduegate.Domain.Mappers;
using Eduegate.Domain.Security;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts.Admin;
using Eduegate.Services.Contracts.Security;

namespace Eduegate.Services.Security
{
    public class SecurityService : BaseService, ISecurityService
    {
        public ClaimDTO GetClaim(long claimID)
        {
            try
            {
                Eduegate.Logger.LogHelper<SecurityService>.Info("Service Result : " + claimID.ToString());
                return new SecurityBL(CallContext).GetClaim(claimID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<SecurityService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public ClaimDTO SaveClaim(ClaimDTO claim)
        {
            try
            {
                Eduegate.Logger.LogHelper<SecurityService>.Info("Service Result : " + claim.ToString());
                return new SecurityBL(CallContext).SaveClaim(claim);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<SecurityService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public ClaimSetDTO GetClaimSet(long claimSetID)
        {
            try
            {
                Eduegate.Logger.LogHelper<SecurityService>.Info("Service Result : " + claimSetID.ToString());
                return new SecurityBL(CallContext).GetClaimSet(claimSetID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<SecurityService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public ClaimSetDTO SaveClaimSet(ClaimSetDTO claimSet)
        {
            try
            {
                Eduegate.Logger.LogHelper<SecurityService>.Info("Service Result : " + claimSet.ToString());
                return new SecurityBL(CallContext).SaveClaimSet(claimSet);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<SecurityService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<ClaimDTO> GetClaims(long userID)
        {
            try
            {
                Eduegate.Logger.LogHelper<SecurityService>.Info("Service Result : " + userID.ToString());
                return new SecurityBL(CallContext).GetClaims(userID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<SecurityService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<ClaimDTO> GetClaimsByType(long userID, Eduegate.Services.Contracts.Enums.ClaimType claimType)
        {
            try
            {
                Eduegate.Logger.LogHelper<SecurityService>.Info("Service Result : " + userID.ToString());
                return new SecurityBL(CallContext).GetClaims(userID, (int)claimType);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<SecurityService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool HasClaimSetAccess(long claimSetID, long userID)
        {
            try
            {
                Eduegate.Logger.LogHelper<SecurityService>.Info("Service Result : " + userID.ToString());
                return new SecurityBL(CallContext).HasClaimSetAccess(claimSetID, userID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<SecurityService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public async Task<List<UserRoleDTO>> GetUserRoles(long loginID)
        {
            try
            {
                Eduegate.Logger.LogHelper<SecurityService>.Info("Service Result : " + loginID.ToString());
                return await new SecurityBL(CallContext).GetUserRoles(loginID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<SecurityService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool HasClaimSetAccessByResource(string resourceID, long userID)
        {
            try
            {
                Eduegate.Logger.LogHelper<SecurityService>.Info("Service Result : " + userID.ToString());
                return new SecurityBL(CallContext).HasClaimSetAccessByResource(resourceID, userID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<SecurityService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<ClaimDTO> GetClaimsByLoginID(long? employeeID)
        {
            return GrantAccessMapper.Mapper(CallContext).GetClaimsByLoginID(employeeID);
        }
    }
}
