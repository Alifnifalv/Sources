using System;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Eduegates;

namespace Eduegate.Service.Client
{
    public class VoucherServiceClient : BaseClient, IVoucher
    {
        private static string serviceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string voucherService = string.Concat(serviceHost, Eduegate.Framework.Helper.Constants.VOUCHER_DATA_SERVICE);

        public VoucherServiceClient(CallContext context = null, Action<string> logger = null)
            : base(context, logger)
        {
        }

        public decimal GetVoucherBalance(string voucherNumber)
        {
            return 0;
        }

        public bool UpdateVoucherBalance(string voucherNumber, string usedAmount)
        {
            var voucherUri = string.Format("{0}/{1}/{2}/{3}", voucherService, "UpdateVoucherBalance", voucherNumber, usedAmount);
            return ServiceHelper.HttpGetRequest<bool>(voucherUri, _callContext, _logger);
        }

        public bool UpdateVoucherMapStatus(string VoucherNumber)
        {
            return false;
        }

        public VoucherValidityDTO ValidateVoucher(string VoucherNumber, string GUID)
        {
            return new VoucherValidityDTO();
        }
        public VoucherMasterDTO GetVoucher(string voucherID)
        {
            return new VoucherMasterDTO();
        }
        public VoucherMasterDTO SaveVoucher(VoucherMasterDTO voucherDTO)
        {
            return new VoucherMasterDTO();
        }
        public bool UpdateVoucher(Status voucherMapStatus)
        {
            return false;
        }

        public VouchersDTO GetVoucherDetails(string voucherNo, long loginID, decimal cartAmount)
        {
            var voucherUri = string.Format("{0}/{1}?voucherNo={2}&loginID={3}&cartAmount={4}", voucherService, "GetVoucherDetails", voucherNo, loginID, cartAmount);
            return ServiceHelper.HttpGetRequest<Eduegate.Services.Contracts.VouchersDTO>(voucherUri, _callContext, _logger);
        }

        public VoucherDTO ApplyVoucher(string voucherNo)
        {
            var voucherUri = string.Format("{0}/{1}?voucherNo={2}", voucherService, "ApplyVoucher", voucherNo);
            return ServiceHelper.HttpGetRequest<VoucherDTO>(voucherUri, _callContext, _logger);
        }

        public string CreateVoucher()
        {
            var voucherUri = string.Format("", voucherService, "CreateVoucher");
            return ServiceHelper.HttpGetRequest<string>(voucherUri, _callContext, _logger);
        }
    }
}