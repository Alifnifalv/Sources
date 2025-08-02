using Eduegate.Framework.Services;
using Eduegate.Services.Contracts.CRM;
using Microsoft.Identity.Client;
using Microsoft.PowerBI.Api;
using Microsoft.PowerBI.Api.Models;
using Microsoft.Rest;

namespace Eduegate.Services.CRM
{
    public class PowerBIService : BaseService, IPowerBIService
    {

        private static readonly string ClientId = "27722e62-988d-4f64-a8cc-62b49eef9286";
        private static readonly string ClientSecret = "VEN8Q~4rMlee69VEQM1spjQ.V3b6R9Z_hvGKAcc5";
        private static readonly string TenantId = "74371ba3-a603-4a16-ae30-9741f9381eaa";
        private static readonly string WorkspaceId = "6f1f62e5-9725-4235-a249-ac314b23e110";
        private static readonly string ReportId = "a7b1a371-7170-4aa0-b9d3-6ff37a659de9";

        private static readonly string[] Scopes = new string[]
        {
            "https://analysis.windows.net/powerbi/api/.default"
        };

        private static readonly string AuthorityUrl = $"https://login.microsoftonline.com/{TenantId}";

        public async Task<EmbedToken> GetEmbedToken()

        {
            try
            {
                var confidentialClient = ConfidentialClientApplicationBuilder
                    .Create(ClientId)
                    .WithClientSecret(ClientSecret)
                    .WithAuthority(new Uri(AuthorityUrl))
                    .Build();

                var authResult = await confidentialClient
                    .AcquireTokenForClient(Scopes)
                    .ExecuteAsync();

                var tokenCredentials = new TokenCredentials(authResult.AccessToken, "Bearer");
                var powerBIClient = new PowerBIClient(new Uri("https://api.powerbi.com"), tokenCredentials);

                var generateTokenRequestParameters = new GenerateTokenRequest(accessLevel: "View");

                var embedToken = await powerBIClient.Reports.GenerateTokenInGroupAsync(
                    Guid.Parse(WorkspaceId),
                    Guid.Parse(ReportId),
                    generateTokenRequestParameters
                );

                return embedToken;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting embed token: {ex.Message}");
            }
        }

    }
}
