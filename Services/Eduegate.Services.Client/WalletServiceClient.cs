using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Payment;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;

namespace Eduegate.Service.Client
{
    public class WalletServiceClient : BaseClient, IWallet
    {
        private static string _serviceHost { get { return ConfigurationExtensions.GetAppConfigValue("ServiceHost"); } }
        private static string _walletService = string.Concat(_serviceHost, Framework.Helper.Constants.WALLET_SERVICE);
        public WalletServiceClient(CallContext callContext = null, Action<string> logger = null) : base(callContext, logger)
        {
        }

        public bool CreateCustomerLog(WalletCustomerLogDTO walletCustomerlog)
        {
            var result = ServiceHelper.HttpPostRequest(_walletService + "CreateCustomerLog", walletCustomerlog, _callContext);
            result = JsonConvert.DeserializeObject<string>(result);
            return Convert.ToBoolean(result);
        }

        public long CreateVoucherWalletTransaction(Framework.Payment.VoucherWalletTransactionDTO voucherWalletTransaction)
        {
            return JsonConvert.DeserializeObject<long>(ServiceHelper.HttpPostRequest(_walletService + "CreateVoucherWalletTransaction", voucherWalletTransaction, _callContext));
        }

        public long CreateWalletEntry(PaymentDTO paymentTransaction)
        {
            return JsonConvert.DeserializeObject<long>(ServiceHelper.HttpPostRequest(_walletService + "CreateWalletEntry", paymentTransaction, _callContext));
        }

        public string GetWalletBalance(string customerid)
        {
            throw new NotImplementedException();
        }

        public WalletCustomerDTO GetWalletCustomer(string customerguid)
        {
            throw new NotImplementedException();
        }

        public string SignoutWallet(string customerid)
        {
            throw new NotImplementedException();
        }

        public bool UpdateWalletEntry(PaymentDTO paymentTransaction)
        {
            throw new NotImplementedException();
        }

        public bool UpdateWalletTransactionStatus(string walletTransactionId, string statusId)
        {
            return ServiceHelper.HttpGetRequest<bool>(string.Format("{0}/{1}/{2}/{3}", _walletService, "UpdateWalletTransactionStatus", walletTransactionId, statusId), _callContext, _logger);
        }

        public IEnumerable<WalletTransactionDTO> WalletTransactions(string CustomerId, string PageNumber, string PageSize)
        {
            throw new NotImplementedException();
        }
    }
}
