using Eduegate.Framework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Eduegate.PublicAPI.Common
{
    public class ApiControllerBase : ControllerBase
    {
        protected CallContext CallContext { get; set; }

        public ApiControllerBase(IHttpContextAccessor context)
        {
            var enableToken = Environment.GetEnvironmentVariable("enableToken");

            if (!string.IsNullOrEmpty(enableToken) && enableToken == "true")
            {
                var authorizationHeader = context?.HttpContext?.Request?.Headers["Authorization"].ToString();
                if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
                {
                    var tokenString = authorizationHeader.ToString().Replace("Bearer ", string.Empty);
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.ReadToken(tokenString) as JwtSecurityToken;

                    if (token != null)
                    {
                        var callContextClaim = token.Claims.FirstOrDefault(claim => claim.Type == "callcontext");

                        if (callContextClaim != null)
                        {
                            CallContext = JsonConvert.DeserializeObject<CallContext>(callContextClaim.Value);
                        }
                    }
                }
            }
            else
            {
                var callContext = context?.HttpContext?.Request?.Headers["callcontext"];

                if (!string.IsNullOrEmpty(callContext) && callContext.Value.Count > 0)
                {
                    CallContext = JsonConvert.DeserializeObject<CallContext>(callContext);
                }
            }

            if (CallContext == null)
            {
                CallContext = new CallContext();
            }

            if (!CallContext.CompanyID.HasValue)
            {
                CallContext.CompanyID = 1;
            }

            if (string.IsNullOrEmpty(CallContext.LanguageCode))
            {
                CallContext.LanguageCode = "en";
            }
        }
    }
}