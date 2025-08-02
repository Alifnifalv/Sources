using Eduegate.Framework.Services;

namespace Eduegate.Services
{
    //TODO: Not used
    //public class Wallet : BaseService, IWallet
    //{
    //    public Wallet()
    //    { }
        
    //    public IEnumerable<WalletTransactionDTO> WalletTransactions(string CutomerId, string PageNumber, string PageSize)
    //    {
    //        //List<WalletTransactionDetail> walletList = new List<WalletTransactionDetail>();
    //        //return walletList.ToArray();
    //        int pageNumber= Convert.ToInt32(1);
    //        int pageSize = Convert.ToInt32(10);
    //        //return new WalletBL().GetWalletTransactionDetails(CutomerId, pageNumber, pageSize);
    //        return new WalletBL(CallContext).GetWalletTransactionDetails(CutomerId, pageNumber, pageSize, this.CallContext);
    //    }

    //    public long CreateWalletEntry(PaymentDTO paymentTransaction)
    //    {
    //        return new WalletBL(CallContext).CreateWalletEntry(paymentTransaction);
    //    }

    //    public bool UpdateWalletEntry(PaymentDTO paymentTransaction)
    //    {
    //       return new WalletBL(CallContext).UpdateWalletEntry(paymentTransaction);
    //    }

    //    public WalletCustomerDTO GetWalletCustomer(string customerGuid)
    //    {
    //        return new WalletBL(CallContext).GetCustomerDetails(customerGuid);
    //    }

    //    public string GetWalletBalance(string customerId)
    //    {
    //        return new WalletBL(CallContext).GetWalletBalance(customerId).ToString();
    //    }

    //    public string SignoutWallet(string customerId)
    //    {
    //        return new WalletBL(CallContext).SignoutWallet(customerId).ToString();
    //    }

    //    public long CreateVoucherWalletTransaction(Eduegate.Framework.Payment.VoucherWalletTransactionDTO voucherWalletTransaction)
    //    {
    //        return new WalletBL(CallContext).CreateVoucherWalletTransaction(voucherWalletTransaction);
    //    }

    //    public bool UpdateWalletTransactionStatus(string walletTransactionId, string statusId)
    //    {
    //        return new WalletBL(CallContext).UpdateWalletTransactionStatus(walletTransactionId, statusId);
    //    }

    //    public bool CreateCustomerLog(WalletCustomerLogDTO log)
    //    {
    //        try
    //        {
    //            return new WalletBL(CallContext).CreateWalletCustomerLog(log);
    //        }
    //        catch (Exception ex)
    //        {
    //            Logger.LogHelper<Wallet>.Fatal(ex.Message, ex);
    //            throw new FaultException("Internal server, please check with your administrator");
    //        }
    //    }

    //}
}