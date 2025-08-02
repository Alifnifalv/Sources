using Eduegate.Domain.Report;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.Contents;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.HR.Payroll;
using Eduegate.Services.Contracts.Notifications;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Services.Contracts.School.Fees;
using Eduegate.Services.Contracts.School.Students;

namespace Eduegate.Services
{
    public class ReportGenerationService : BaseService, IReportGenerationService
    {
        public void GenerateFeeReceiptAndSendToMail(List<FeeCollectionDTO> feeCollectionList, EmailTypes mailType)
        {
            new ReportGenerationBL(this.CallContext).GenerateFeeReceiptAndSendToMail(feeCollectionList, mailType);
        }  

        public string SendFeeDueMailReportToParent(MailFeeDueStatementReportDTO gridData)
        {
            return new ReportGenerationBL(this.CallContext).SendFeeDueMailReportToParent(gridData);
        }

        public void GenerateSalesInvoiceAndSendToMail(string headID, string transactionNo)
        {
            new ReportGenerationBL(this.CallContext).GenerateSalesInvoiceAndSendToMail(headID, transactionNo);
        }

        public List<ContentFileDTO> GenerateSalarySlipContentFile(List<SalarySlipDTO> salarySlipList)
        {
            return new ReportGenerationBL(this.CallContext).GenerateSalarySlipContentFile(salarySlipList);
        }

        public void MailSalaryslip(List<SalarySlipDTO> salarySlipList)
        {
            new ReportGenerationBL(this.CallContext).MailSalaryslip(salarySlipList);
        }

        public ContentFileDTO GenerateTransferRequestContentFile(StudentTransferRequestDTO salarySlipList)
        {
            return new ReportGenerationBL(this.CallContext).GenerateTransferRequestContentFile(salarySlipList);
        }

        public ContentFileDTO GenerateProgressReportContentFile(ProgressReportDTO progressReport, string reportName)
        {
            return new ReportGenerationBL(this.CallContext).GenerateProgressReportContentFile(progressReport, reportName);
        }

        public OperationResultDTO SendMailNotification(MailNotificationDTO mailDTO)
        {
            return new Eduegate.Domain.Notification.EmailNotificationBL(this.CallContext).SendMailNotification(mailDTO);
        }  
       

        public OperationResultDTO MailJobOfferLetter(EmployeeSalaryStructureDTO dto, EmailTypes mailType)
        {
            return new ReportGenerationBL(this.CallContext).MailJobOfferLetter(dto,mailType);
        }
    }
}