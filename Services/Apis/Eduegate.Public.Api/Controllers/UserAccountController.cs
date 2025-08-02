using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Hub.Client;
using Eduegate.PublicAPI.Common;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.MobileAppWrapper;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Eduegate.Public.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserAccountController : ApiControllerBase
    {
        private readonly ILogger<UserAccountController> _logger;
        private readonly dbEduegateSchoolContext _dbContext;
        private readonly IBackgroundJobClient _backgroundJobs;
        private readonly RealtimeClient _realtimeClient;
        private readonly IHttpContextAccessor _accessor;

        public UserAccountController(ILogger<UserAccountController> logger, IHttpContextAccessor context,
            dbEduegateSchoolContext dbContext, IBackgroundJobClient backgroundJobs,
            IServiceProvider serviceProvider, RealtimeClient realtimeClient,
            IHttpContextAccessor accessor) : base(context)
        {
            _logger = logger;
            _dbContext = dbContext;
            _backgroundJobs = backgroundJobs;
            _realtimeClient = realtimeClient;
            _accessor = accessor;

        }

        [HttpGet]
        [Route("IsValidShareHolder")]
        public string IsValidShareHolder(string shareholderID)
        {
            return new UserAccountService(CallContext).IsValidShareHolder(shareholderID);
        }

        [HttpPost]
        [Route("SendOTP")]
        public void SendOTP(string mobileNumber)
        {
            new UserAccountService(CallContext).SendOTP(mobileNumber);
        }

        [HttpPost]
        [Route("SendOTPByEmiratesID")]
        public void SendOTPByEmiratesID(string shareHolderID)
        {
            new UserAccountService(CallContext).SendOTPByEmiratesID(shareHolderID);
        }

        [HttpPost]
        [Route("ValidateOTP")]
        public bool ValidateOTP(string emirateID, string mobileNumber, string otpText)
        {
            return new UserAccountService(CallContext).ValidateOTP(emirateID, mobileNumber, otpText);
        }

        [HttpGet]
        [Route("GetUsers")]
        public List<UserDTO> GetUsers()
        {
            return new UserAccountService(CallContext).GetUsers();
        }

        [HttpGet]
        [Route("GetUser")]
        public UserDTO GetUser(long loginId)
        {
            return new UserAccountService(CallContext).GetUser(loginId);
        }

        [HttpGet]
        [Route("RegisterUserDevice")]
        public OperationResultDTO RegisterUserDevice(string token, string userType)
        {
            return new UserAccountService(CallContext).RegisterUserDevice(token, userType);
        }

    }
}