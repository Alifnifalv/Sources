using System.Collections.Generic;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.Contents;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.HR.Payroll;
using Eduegate.Services.Contracts.Notifications;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Services.Contracts.School.Students;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IReportGenerationService" in both code and config file together.
    public interface IReportGenerationService
    {
        void GenerateFeeReceiptAndSendToMail(List<FeeCollectionDTO> feeCollectionList, EmailTypes mailType);

        string SendFeeDueMailReportToParent(MailFeeDueStatementReportDTO gridData);

        void GenerateSalesInvoiceAndSendToMail(string headID, string transactionNo);

        List<ContentFileDTO> GenerateSalarySlipContentFile(List<SalarySlipDTO> salarySlipList);

        void MailSalaryslip(List<SalarySlipDTO> salarySlipList);

        ContentFileDTO GenerateTransferRequestContentFile(StudentTransferRequestDTO salarySlipList);

        ContentFileDTO GenerateProgressReportContentFile(ProgressReportDTO progressReport, string reportName);

        OperationResultDTO SendMailNotification(MailNotificationDTO mailDTO);

        OperationResultDTO MailJobOfferLetter(EmployeeSalaryStructureDTO dto, EmailTypes mailType);

    }
}