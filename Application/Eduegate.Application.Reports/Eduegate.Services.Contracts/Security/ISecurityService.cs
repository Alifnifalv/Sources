using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Admin;

namespace Eduegate.Services.Contracts.Security
{
    [ServiceContract]
    public interface ISecurityService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetClaim?claimID={claimID}")]
        ClaimDTO GetClaim(long claimID);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveClaim")]
        ClaimDTO SaveClaim(ClaimDTO claim);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetClaimSet?claimSetID={claimSetID}")]
        ClaimSetDTO GetClaimSet(long claimSetID);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveClaimSet")]
        ClaimSetDTO SaveClaimSet(ClaimSetDTO claimSet);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetClaims?userID={userID}")]
        List<ClaimDTO> GetClaims(long userID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetClaimsByType?userID={userID}&claimType={claimType}")]
        List<ClaimDTO> GetClaimsByType(long userID, Eduegate.Services.Contracts.Enums.ClaimType claimType);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "HasClaimSetAccess?claimSetID={claimSetID}&userID={userID}")]
        bool HasClaimSetAccess(long claimSetID, long userID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "HasClaimSetAccessByResource?resourceID={resourceID}&userID={userID}")]
        bool HasClaimSetAccessByResource(string resourceID, long userID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetUserRoles?loginID={loginID}")]
        List<UserRoleDTO> GetUserRoles(long loginID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetClaimsByLoginID?employeeID={employeeID}")]
        List<ClaimDTO> GetClaimsByLoginID(long? employeeID);
    }
}
