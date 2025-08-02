using AppOwnsData.Models;
using AppOwnsData.Services;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Hub.Client;
using Eduegate.PublicAPI.Common;
using Eduegate.Services.MobileAppWrapper;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Rest;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Eduegate.Public.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PowerBIController : ApiControllerBase
    {


        private readonly ILogger<SchoolController> _logger;
        private readonly dbEduegateSchoolContext _dbContext;
        private readonly IBackgroundJobClient _backgroundJobs;
        private readonly RealtimeClient _realtimeClient;
        private readonly IHttpContextAccessor _accessor;
        private readonly PbiEmbedService pbiEmbedService;
        private readonly IOptions<AzureAd> azureAd;
        public PowerBIController(ILogger<SchoolController> logger, IHttpContextAccessor context,
            dbEduegateSchoolContext dbContext, IBackgroundJobClient backgroundJobs,
            IServiceProvider serviceProvider, RealtimeClient realtimeClient,
            IHttpContextAccessor accessor, PbiEmbedService pbiEmbedService, IOptions<AzureAd> azureAd) : base(context)
        {
            _logger = logger;
            _dbContext = dbContext;
            _backgroundJobs = backgroundJobs;
            _realtimeClient = realtimeClient;
            _accessor = accessor;
            this.pbiEmbedService = pbiEmbedService;
            this.azureAd = azureAd;
        }


        [HttpGet]
        [Route("GetEmbedInfoAsync")]

        public async Task<string> GetEmbedInfoAsync(long? pageID)
        {
            try
            {

                var result = new SchoolService(CallContext).GetPowerBIDataUsingPageID(pageID);


                EmbedParams embedParams = await pbiEmbedService.GetEmbedParams(new Guid(result.WorkspaceID), new Guid(result.ReportID));
                return JsonSerializer.Serialize<EmbedParams>(embedParams);
            }
            catch (HttpOperationException exc)
            {
                HttpContext.Response.StatusCode = (int)exc.Response.StatusCode;
                var message = string.Format("Status: {0} ({1})\r\nResponse: {2}\r\nRequestId: {3}", exc.Response.StatusCode, (int)exc.Response.StatusCode, exc.Response.Content, exc.Response.Headers["RequestId"].FirstOrDefault());
                return message;
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = 500;
                return ex.Message + "\n\n" + ex.StackTrace;
            }
        }

    }
}