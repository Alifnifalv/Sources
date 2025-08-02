using System;
using System.Collections.Generic;
using Eduegate.Framework;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.Contents;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.HR.Payroll;
using Eduegate.Services.Contracts.Notifications;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Services.Contracts.School.Students;

namespace Eduegate.Services.Client.Direct
{
    public class ReportGenerationServiceClient : IReportGenerationService
    {
        ReportGenerationService service = new ReportGenerationService();

        public ReportGenerationServiceClient(CallContext context = null, Action<string> logger = null)
        {
            service.CallContext = context;
        }

        public void GenerateFeeReceiptAndSendToMail(List<FeeCollectionDTO> feeCollectionList, EmailTypes mailType)
        {
            service.GenerateFeeReceiptAndSendToMail(feeCollectionList, mailType);
        }

        public void GenerateSalesInvoiceAndSendToMail(string headID, string transactionNo)
        {
            service.GenerateSalesInvoiceAndSendToMail(headID, transactionNo);
        }

        public string SendFeeDueMailReportToParent(MailFeeDueStatementReportDTO gridData)
        {
            return service.SendFeeDueMailReportToParent(gridData);
        }

        public List<ContentFileDTO> GenerateSalarySlipContentFile(List<SalarySlipDTO> salarySlipList)
        {
            return service.GenerateSalarySlipContentFile(salarySlipList);
        }

        public void MailSalaryslip(List<SalarySlipDTO> salarySlipList)
        {
            service.MailSalaryslip(salarySlipList);
        }

        public ContentFileDTO GenerateTransferRequestContentFile(StudentTransferRequestDTO salarySlipList)
        {
            return service.GenerateTransferRequestContentFile(salarySlipList);
        }

        public ContentFileDTO GenerateProgressReportContentFile(ProgressReportDTO progressReport, string reportName)
        {
            return service.GenerateProgressReportContentFile(progressReport, reportName);
        }

        public OperationResultDTO SendMailNotification(MailNotificationDTO mailDTO)
        {
            return service.SendMailNotification(mailDTO);
        }

    }
}