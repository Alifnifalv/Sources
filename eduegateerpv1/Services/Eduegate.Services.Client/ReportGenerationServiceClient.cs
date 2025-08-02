using System;
using System.Collections.Generic;
using Eduegate.Framework;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Service.Client;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Services.Contracts.Contents;
using Eduegate.Services.Contracts.HR.Payroll;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.Notifications;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Services.Client
{
    public class ReportGenerationServiceClient : BaseClient, IReportGenerationService
    {
        public ReportGenerationServiceClient(CallContext context = null, Action<string> logger = null)
            : base(context, logger)
        {
        }

        public void GenerateFeeReceiptAndSendToMail(List<FeeCollectionDTO> feeCollectionList, EmailTypes mailType)
        {
            throw new NotImplementedException();
        }

        public string SendFeeDueMailReportToParent(MailFeeDueStatementReportDTO gridData)
        {
            throw new NotImplementedException();
        }

        public void GenerateSalesInvoiceAndSendToMail(string headID, string transactionNo)
        {
            throw new NotImplementedException();
        }

        public List<ContentFileDTO> GenerateSalarySlipContentFile(List<SalarySlipDTO> salarySlipList)
        {
            throw new NotImplementedException();
        }

        public void MailSalaryslip(List<SalarySlipDTO> salarySlipList)
        {
            throw new NotImplementedException();
        }

        public ContentFileDTO GenerateTransferRequestContentFile(StudentTransferRequestDTO salarySlipList)
        {
            throw new NotImplementedException();
        }

        public ContentFileDTO GenerateProgressReportContentFile(ProgressReportDTO progressReport, string reportName)
        {
            throw new NotImplementedException();
        }

        public OperationResultDTO SendMailNotification(MailNotificationDTO mailDTO)
        {
            throw new NotImplementedException();
        }

    }
}