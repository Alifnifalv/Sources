using System.Collections.Generic;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWallet" in both code and config file together.
    public interface IWallet
    {
        IEnumerable<WalletTransactionDTO> WalletTransactions(string CustomerId, string PageNumber, string PageSize);

        long CreateWalletEntry(PaymentDTO paymentTransaction);

        bool UpdateWalletEntry(PaymentDTO paymentTransaction);

        WalletCustomerDTO GetWalletCustomer(string customerguid);

        string GetWalletBalance(string customerid);

        string SignoutWallet(string customerid);

        //long CreateVoucherWalletTransaction(Eduegate.Framework.Payment.VoucherWalletTransactionDTO voucherWalletTransaction);

        bool UpdateWalletTransactionStatus(string WalletTransactionId, string StatusId);

        bool CreateCustomerLog(WalletCustomerLogDTO log);

    }
}