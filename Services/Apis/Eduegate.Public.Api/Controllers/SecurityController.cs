using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.School.Students;
using Microsoft.AspNetCore.Mvc;
using Eduegate.Domain.Entity.School.Models.School;
using Hangfire;
using Eduegate.Hub.Client;
using Eduegate.Services.MobileAppWrapper;
using Eduegate.PublicAPI.Common;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Eduegate.Public.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecurityController : ApiControllerBase
    {
        private readonly ILogger<SecurityController> _logger;
        private readonly dbEduegateSchoolContext _dbContext;
        private readonly IBackgroundJobClient _backgroundJobs;
        private readonly RealtimeClient _realtimeClient;
        private readonly IHttpContextAccessor _accessor;

        public SecurityController(ILogger<SecurityController> logger, IHttpContextAccessor context,
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

        [HttpPost]
        [Route("Login")]
        public OperationResultDTO Login(UserDTO user)
        {
            return new AppSecurityService(CallContext).Login(user);
        }

        [HttpPost]
        [Route("ParentLogin")]
        public OperationResultDTO ParentLogin(UserDTO user)
        {
            return new AppSecurityService(CallContext).ParentLogin(user);


            //var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            ////create claims details based on the user information
            //var claims = new[] {
            //            new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
            //            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            //            new Claim("callcontext", Newtonsoft.Json.JsonConvert.SerializeObject(new Eduegate.Framework.CallContext() {
            //                 EmailID =  emailID,
            //                 EmployeeID = user.Employee?.EmployeeIID,
            //                 CustomerID = user.Customer?.CustomerIID,
            //                 LoginID = user.UserID,
            //                 SupplierID = user.Supplier?.SupplierIID,
            //                 IPAddress = ipAddress,
            //                 SecuredToken = deviceId
            //            }))
            //        };

            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            //var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //var token = new JwtSecurityToken(
            //    _configuration["Jwt:Issuer"],
            //    _configuration["Jwt:Audience"],
            //    claims,
            //    expires: DateTime.UtcNow.AddMonths(6),
            //    signingCredentials: signIn);

            //return Ok(new JwtSecurityTokenHandler().WriteToken(token));

        }

        [HttpGet]
        [Route("GetParentDetails")]
        public GuardianDTO GetParentDetails(string emailID)
        {
            return new AppSecurityService(CallContext).GetParentDetails(emailID);
        }

        [HttpGet]
        [Route("GetParentDetailsByParentCode")]
        public GuardianDTO GetParentDetailsByParentCode(string parentCode, UserDTO user)
        {
            return new AppSecurityService(CallContext).GetParentDetailsByParentCode(parentCode, user);
        }

        [HttpPost]
        [Route("StaffLogin")]
        public OperationResultDTO StaffLogin(UserDTO user)
        {
            return new AppSecurityService(CallContext).StaffLogin(user);
        }

        [HttpGet]
        [Route("GetUserDetails")]
        public UserDTO GetUserDetails()
        {
            return new AppSecurityService(CallContext).GetUserDetails();
        }

        [HttpGet]
        [Route("LogOut")]
        public UserDTO LogOut()
        {
            return new AppSecurityService(CallContext).LogOut();
        }

        [HttpPost]
        [Route("GenerateApiKey")]
        public string GenerateApiKey(string uuid, string version)
        {
            return new AppSecurityService(CallContext).GenerateApiKey(uuid, version);
        }

        [HttpGet]
        [Route("ResetPasswordOTPGenerate")]
        public OperationResultDTO ResetPasswordOTPGenerate(string emailID)
        {
            return new AppSecurityService(CallContext).ResetPasswordOTPGenerate(emailID);
        }

        [HttpPost]
        [Route("ResetPasswordVerifyOTP")]
        public OperationResultDTO ResetPasswordVerifyOTP(string OTP, string email)
        {
            return new AppSecurityService(CallContext).ResetPasswordVerifyOTP(OTP, email);
        }

        [HttpPost]
        [Route("SubmitPasswordChange")]
        public OperationResultDTO SubmitPasswordChange(string email, string password)
        {
            return new AppSecurityService(CallContext).SubmitPasswordChange(email, password);
        }

        [HttpPost]
        [Route("VisitorLogin")]
        public OperationResultDTO VisitorLogin(UserDTO user)
        {
            return new AppSecurityService(CallContext).VisitorLogin(user);
        }

        [HttpPost]
        [Route("StudentLogin")]
        public OperationResultDTO StudentLogin(UserDTO user)
        {
            return new AppSecurityService(CallContext).StudentLogin(user);
        }

    }
}