using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts.Security;

namespace Eduegate.Service.Client
{
    public class SecurityServiceClient : BaseClient, ISecurityService
    {
        private static string ServiceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string service = string.Concat(ServiceHost, Eduegate.Framework.Helper.Constants.SECURITY_SERVICE_NAME);

        public SecurityServiceClient(CallContext context = null, Action<string> logger = null)
            :base(context, logger)
        {
        }

        public ClaimDTO GetClaim(long claimID)
        {
            var uri = string.Format("{0}/GetClaim?claimID={1}", service, claimID);
            return ServiceHelper.HttpGetRequest<ClaimDTO>(uri, _callContext, _logger);
        }

        public ClaimDTO SaveClaim(ClaimDTO claim)
        {
            var uri = string.Format("{0}/SaveClaim", service);
            return ServiceHelper.HttpPostGetRequest<ClaimDTO>(uri, claim, _callContext, _logger);
        }

        public ClaimSetDTO GetClaimSet(long claimSetID)
        {
            var uri = string.Format("{0}/GetClaimSet?claimSetID={1}", service, claimSetID);
            return ServiceHelper.HttpGetRequest<ClaimSetDTO>(uri, _callContext, _logger);
        }

        public ClaimSetDTO SaveClaimSet(ClaimSetDTO claimSet)
        {
            var uri = string.Format("{0}/SaveClaimSet", service);
            return ServiceHelper.HttpPostGetRequest<ClaimSetDTO>(uri, claimSet, _callContext, _logger);
        }


        public List<ClaimDTO> GetClaims(long userID)
        {
            var uri = string.Format("{0}/GetClaims?userID={1}", service, userID);
            return ServiceHelper.HttpGetRequest<List<ClaimDTO>>(uri, _callContext, _logger);
        }

        public List<ClaimDTO> GetClaimsByType(long userID, Eduegate.Services.Contracts.Enums.ClaimType claimType)
        {
            var uri = string.Format("{0}/GetClaimsByType?userID={1}&claimType={2}", service, userID, claimType);
            return ServiceHelper.HttpGetRequest<List<ClaimDTO>>(uri, _callContext, _logger);
        }
        public bool HasClaimSetAccess(long claimSetID, long userID)
        {
            var uri = string.Format("{0}/HasClaimSetAccess?claimSetID={1}&userID={2}", service, claimSetID, userID);
            return ServiceHelper.HttpGetRequest<bool>(uri, _callContext, _logger);
        }

        public Task<List<Services.Contracts.Admin.UserRoleDTO>> GetUserRoles(long loginID)
        {
            var uri = string.Format("{0}/GetUserRoles?loginID={1}", service, loginID);
            return Task.FromResult(ServiceHelper.HttpGetRequest<List<Services.Contracts.Admin.UserRoleDTO>>(uri, _callContext, _logger));
        }

        public bool HasClaimSetAccessByResource(string resourceID, long userID)
        {
            var uri = string.Format("{0}/HasClaimSetAccessByResource?resourceID={1}&userID={2}", service, resourceID, userID);
            return ServiceHelper.HttpGetRequest<bool>(uri, _callContext, _logger);
        }

        public List<ClaimDTO> GetClaimsByLoginID(long? employeeID)
        {
            var uri = string.Format("{0}/GetClaimsByLoginID?userID={1}", service, employeeID);
            return ServiceHelper.HttpGetRequest<List<ClaimDTO>>(uri, _callContext, _logger);
        }

        
    }
}
