using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Eduegate.Services.Contracts.Contents;
using Eduegate.Services.Contracts.HR.Payroll;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Services.Contracts.School.Students;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IReportGenerationService" in both code and config file together.
    [ServiceContract]
    public interface IReportGenerationService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GenerateFeeReceiptAndSendToMail")]
        void GenerateFeeReceiptAndSendToMail(List<FeeCollectionDTO> feeCollectionList);


        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SendFeeDueMailReportToParent")]
        string SendFeeDueMailReportToParent(MailFeeDueStatementReportDTO gridData);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GenerateSalesInvoiceAndSendToMail")]
        void GenerateSalesInvoiceAndSendToMail(string headID, string transactionNo);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GenerateSalarySlipContentFile")]
        List<ContentFileDTO> GenerateSalarySlipContentFile(List<SalarySlipDTO> salarySlipList);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "MailSalaryslip")]
        void MailSalaryslip(List<SalarySlipDTO> salarySlipList);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GenerateTransferRequestContentFile")]
        ContentFileDTO GenerateTransferRequestContentFile(StudentTransferRequestDTO salarySlipList);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GenerateProgressReportContentFile")]
        ContentFileDTO GenerateProgressReportContentFile(ProgressReportDTO progressReport, string reportName);

    }
}