using System.ServiceModel;
using Eduegate.Domain;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Eduegates;

namespace Eduegate.Services
{
    public class Voucher : BaseService, IVoucher
    {
        public decimal GetVoucherBalance(string voucherNumber)
        {
            return new VoucherBL(this.CallContext).GetVoucherBalance(voucherNumber);
        }

        public bool UpdateVoucherBalance(string voucherNumber, string usedAmount)
        {
            return new VoucherBL(this.CallContext).UpdateVoucherBalance(voucherNumber, usedAmount);
        }

        public bool UpdateVoucherMapStatus(string voucherNumber)
        {
            return new VoucherBL(this.CallContext).UpdateVoucherMapStatus(voucherNumber);
        }

        public VoucherValidityDTO ValidateVoucher(string voucherNumber, string userGUID)
        {
            return new VoucherBL(this.CallContext).ValidateVoucher(voucherNumber, userGUID);
        }

        public VoucherMasterDTO GetVoucher(string voucherID)
        {
            return new VoucherBL(this.CallContext).GetVoucher(voucherID);
        }

        public VoucherMasterDTO SaveVoucher(VoucherMasterDTO voucherDTO)
        {
            return new VoucherBL(this.CallContext).SaveVoucher(voucherDTO);
        }

        public bool UpdateVoucher(Status voucherMapStatus)
        {
            var exit = false;
            try
            {
                exit = new VoucherBL(this.CallContext).UpdateVoucher(voucherMapStatus, base.CallContext);
                Eduegate.Logger.LogHelper<Voucher>.Info("Service Result : " + Convert.ToString(exit));
                exit = true;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Voucher>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
            return exit;
        }

        public VouchersDTO GetVoucherDetails(string voucherNo, long loginID, decimal cartAmount)
        {
            return new VoucherBL(CallContext).GetVoucherDetails(voucherNo, loginID, cartAmount);
        }

        public VoucherDTO ApplyVoucher(string voucherNo)
        {
            return new ShoppingCartBL(CallContext).ApplyVoucher(voucherNo, this.CallContext);
        }

        public string CreateVoucher()
        {
            try
            {
               return new VoucherBL(this.CallContext).CreateVoucher();
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Voucher>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }
    }
}
