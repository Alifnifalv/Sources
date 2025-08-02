using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Eduegate.Framework;
using Eduegate.Framework.Payment;

namespace Eduegate.Services.Contracts
{

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWallet" in both code and config file together.
    [ServiceContract]
    public interface IWallet
    {


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "WalletTransactions/{CustomerId}/{PageNumber}/{PageSize}")]
        IEnumerable<WalletTransactionDTO> WalletTransactions(string CustomerId, string PageNumber, string PageSize);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "CreateWalletEntry")]
        long CreateWalletEntry(PaymentDTO paymentTransaction);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "UpdateWalletEntry")]
        bool UpdateWalletEntry(PaymentDTO paymentTransaction);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetWalletCustomer/{customerguid}")]
        WalletCustomerDTO GetWalletCustomer(string customerguid);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetWalletBalance/{customerid}")]
        string GetWalletBalance(string customerid);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SignoutWallet/{customerid}")]
        string SignoutWallet(string customerid);

        //[OperationContract]
        //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        //    BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "CreateVoucherWalletTransaction")]
        //long CreateVoucherWalletTransaction(Eduegate.Framework.Payment.VoucherWalletTransactionDTO voucherWalletTransaction);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "UpdateWalletTransactionStatus/{WalletTransactionId}/{StatusId}")]
        bool UpdateWalletTransactionStatus(string WalletTransactionId, string StatusId);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "CreateCustomerLog")]
        bool CreateCustomerLog(WalletCustomerLogDTO log);

    }
}
