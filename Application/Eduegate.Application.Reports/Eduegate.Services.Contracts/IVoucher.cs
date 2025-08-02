using System.ServiceModel;
using System.ServiceModel.Web;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Services.Contracts.Eduegates;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IVoucher" in both code and config file together.
    [ServiceContract]
    public interface IVoucher
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "VoucherBalance/{VoucherNumber}")]
        decimal GetVoucherBalance(string voucherNumber);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "UpdateVoucherBalance/{VoucherNumber}/{UsedAmount}")]
        bool UpdateVoucherBalance(string VoucherNumber, string UsedAmount);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "UpdateVoucherMapStatus/{VoucherNumber}")]
        bool UpdateVoucherMapStatus(string VoucherNumber);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "ValidateVoucher/{VoucherNumber}/{GUID}")]
        VoucherValidityDTO ValidateVoucher(string VoucherNumber, string GUID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetVoucher/{VoucherID}/")]
        VoucherMasterDTO GetVoucher(string voucherID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveVoucher")]
        VoucherMasterDTO SaveVoucher(VoucherMasterDTO voucherDTO);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "UpdateVoucher")]
        bool UpdateVoucher(Status voucherMapStatus);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetVoucherDetails?voucherNo={voucherNo}&loginID={loginID}&cartAmount={cartAmount}")]
        VouchersDTO GetVoucherDetails(string voucherNo, long loginID, decimal cartAmount);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "ApplyVoucher?voucherNo={voucherNo}")]
        VoucherDTO ApplyVoucher(string voucherNo);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "CreateVoucher")]
        string CreateVoucher();
    }
}