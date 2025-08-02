using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Payment;
using Eduegate.Hub.Client;
using Eduegate.PublicAPI.Common;
using Eduegate.Services.Contracts.PaymentGateway;
using Eduegate.Services.Contracts.Payments;
using Eduegate.Services.Payment;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Diagnostics;

namespace Eduegate.Public.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentGatewayController : ApiControllerBase
    {
        private readonly ILogger<PaymentGatewayController> _logger;
        private readonly dbEduegateSchoolContext _dbContext;
        private readonly IBackgroundJobClient _backgroundJobs;
        private readonly RealtimeClient _realtimeClient;
        private readonly IHttpContextAccessor _accessor;

        public PaymentGatewayController(ILogger<PaymentGatewayController> logger, IHttpContextAccessor context,
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
        [Route("PendingQPayPaymentProcess")]
        public string  PendingQPayPaymentProcess()
        {
            return new BillPaymentService().PendingQPayPaymentProcess();
        }

        [HttpGet]
        [Route("GetBillingInformation")]
        public BillingResponseDTO GetBillingInformation(string agentId, string token, string timeStamp,
            string StudentRollNumber, string ChildQID)
        {
            return new BillPaymentService().GetBillingInformation(agentId, token, timeStamp, StudentRollNumber, ChildQID);
        }

        [HttpGet]
        [Route("MakePayment")]
        public PaymentResponseDTO MakePayment(BankBillPaymentDTO payment)
        {
            return new BillPaymentService().MakePayment(payment);
        }




    }
}
