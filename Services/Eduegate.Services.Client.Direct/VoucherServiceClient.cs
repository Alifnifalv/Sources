using System;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Services;
using Eduegate.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Eduegates;

namespace Eduegate.Service.Client.Direct
{
    public class VoucherServiceClient : BaseClient, IVoucher
    {
        Voucher voucherService = new Voucher();

        public VoucherServiceClient(CallContext context = null, Action<string> logger = null)
            : base(context, logger)
        {
        }

        public decimal GetVoucherBalance(string voucherNumber)
        {
            return voucherService.GetVoucherBalance(voucherNumber);
        }

        public bool UpdateVoucherBalance(string voucherNumber, string usedAmount)
        {
            return voucherService.UpdateVoucherBalance(voucherNumber, usedAmount);
        }

        public bool UpdateVoucherMapStatus(string VoucherNumber)
        {
            return voucherService.UpdateVoucherMapStatus(VoucherNumber);
        }

        public VoucherValidityDTO ValidateVoucher(string VoucherNumber, string GUID)
        {
            return voucherService.ValidateVoucher(VoucherNumber, GUID);
        }
        public VoucherMasterDTO GetVoucher(string voucherID)
        {
            return voucherService.GetVoucher(voucherID);
        }
        public VoucherMasterDTO SaveVoucher(VoucherMasterDTO voucherDTO)
        {
            return voucherService.SaveVoucher(voucherDTO);
        }
        public bool UpdateVoucher(Status voucherMapStatus)
        {
            return voucherService.UpdateVoucher(voucherMapStatus);
        }

        public VouchersDTO GetVoucherDetails(string voucherNo, long loginID, decimal cartAmount)
        {
            return voucherService.GetVoucherDetails(voucherNo, loginID, cartAmount);
        }

        public VoucherDTO ApplyVoucher(string voucherNo)
        {
            return voucherService.ApplyVoucher(voucherNo);
        }

        public string CreateVoucher()
        {
            return voucherService.CreateVoucher();
        }
    }
}