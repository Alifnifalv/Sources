using Eduegate.Framework.Helper.Enums;
using Eduegate.Services.Contracts.Eduegates;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IVoucher" in both code and config file together.   
    public interface IVoucher
    {
        decimal GetVoucherBalance(string voucherNumber);

        bool UpdateVoucherBalance(string VoucherNumber, string UsedAmount);

        bool UpdateVoucherMapStatus(string VoucherNumber);

        VoucherValidityDTO ValidateVoucher(string VoucherNumber, string GUID);

        VoucherMasterDTO GetVoucher(string voucherID);

        VoucherMasterDTO SaveVoucher(VoucherMasterDTO voucherDTO);

        bool UpdateVoucher(Status voucherMapStatus);

        VouchersDTO GetVoucherDetails(string voucherNo, long loginID, decimal cartAmount);

        VoucherDTO ApplyVoucher(string voucherNo);

        string CreateVoucher();
    }
}