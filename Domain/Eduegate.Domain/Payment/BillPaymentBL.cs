using Eduegate.Domain.Mappers.Notification;
using Eduegate.Domain.Mappers.Notification.Notifications;
using Eduegate.Domain.Mappers.School.Fees;
using Eduegate.Domain.Repository.Payment;
using Eduegate.Domain.Repository.School;
using Eduegate.Framework;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Enums.Notifications;
using Eduegate.Services.Contracts.Notifications;
using Eduegate.Services.Contracts.PaymentGateway;
using Eduegate.Services.Contracts.School.Fees;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Domain.Payment
{
    public class BillPaymentBL
    {
        CallContext _context;

        public BillPaymentBL(CallContext context)
        {
            _context = context;
        }

        public BillingResponseDTO GetBillingInformation(string agentID, string token, string timeStamp,
            string StudentRollNumber, string ChildQID)
        {
            var tokenSetting = new Domain.Setting.SettingBL(null).GetSettingValue<string>("BillPaymentAPIToken");

            if (token != tokenSetting)
            {
                throw new Exception("Invalid token");
            }

            var agentSetting = new Domain.Setting.SettingBL(null).GetSettingValue<string>("BillPaymentAPIAgent");

            if (agentID != agentSetting)
            {
                throw new Exception("Invalid agent");
            }

            var student = new SchoolRepository().GetStudentDetail(ChildQID, StudentRollNumber);

            if (student == null)
            {
                throw new Exception("Invalid student");
            }
            else if (student.StudentPassportDetails == null || !student.StudentPassportDetails.Any(x => x.NationalIDNo == ChildQID))
            {
                throw new Exception("Invalid QID");
            }
            string merchantID = string.Empty;
            string company = string.Empty;
            var settingBL = new Domain.Setting.SettingBL(_context);
            if (student.SchoolID.HasValue && student.SchoolID == 30)
            {
                merchantID = settingBL.GetSettingValue<string>("BILL_PAYMENT_MERCHANT_PODAR");
                company = settingBL.GetSettingValue<string>("BILL_PAYMENT_COMPANY_PODAR");
            }
            else
            {
                merchantID = settingBL.GetSettingValue<string>("BILL_PAYMENT_MERCHANT_PEARL");
                company = settingBL.GetSettingValue<string>("BILL_PAYMENT_COMPANY_PEARL");
            }
            try
            {
                var feeDue = FeeDueGenerationMapper.Mapper(_context)
                    .FillPendingFeesForBank(student.ClassID.Value, student.StudentIID);

                if (feeDue.Any(x => x.ActCode != "1"))
                {
                    return new BillingResponseDTO()
                    {

                        ActCode = feeDue.Select(x => x.ActCode).FirstOrDefault(),
                        ResposeCode = feeDue.Select(x => x.ResposeCode).FirstOrDefault(),
                        ActDescription = feeDue.Select(x => x.ActDescription).FirstOrDefault(),
                        Description = feeDue.Select(x => x.ResposeDescription).FirstOrDefault(),
                        SchoolId = student.SchoolID.HasValue ? student.SchoolID.ToString() : null,
                        SchoolName = student.School.SchoolName,
                        ClassSection = student.Class.ClassDescription + '#' + student.Section.SectionName,
                        Term = String.Join(",", feeDue.Where(z => z.InvoiceAmount > 0).Select(w => w.Remarks.ToString())),//feeDue.Select(x => x.Remarks).FirstOrDefault(),
                        RollNo = student.AdmissionNumber,
                        OutstandingAmount = feeDue.Sum(x => x.InvoiceAmount).Value,
                        Remarks = "Total due amount",
                        LocalTxnDtTime = DateTime.Now,
                        BilledAmount = feeDue.Sum(x => x.InvoiceAmount).Value,
                        StudentName = student.FirstName + " " + (student.MiddleName == null ? "" : student.MiddleName) + " " + student.LastName,
                        Company = company,
                        MerchantID = merchantID
                    };
                }
                else
                {
                    return new BillingResponseDTO()
                    {
                        ActCode = "1",
                        ActDescription = "Fee Due",
                        SchoolId = student.SchoolID.HasValue ? student.SchoolID.ToString() : null,
                        SchoolName = student.School.SchoolName,
                        ClassSection = student.Class.ClassDescription + '#' + student.Section.SectionName,
                        Term = String.Join(",", feeDue.Select(w => w.Remarks.ToString())),
                        RollNo = student.AdmissionNumber,
                        OutstandingAmount = feeDue.Sum(x => x.InvoiceAmount).Value,
                        Remarks = "Total due amount",
                        LocalTxnDtTime = DateTime.Now,
                        BilledAmount = feeDue.Sum(x => x.InvoiceAmount).Value,
                        StudentName = student.FirstName + " " + (student.MiddleName == null ? "" : student.MiddleName) + " " + student.LastName,
                        Company = company,
                        MerchantID = merchantID
                    };
                }
            }
            catch (Exception ex)
            {
                return new BillingResponseDTO()
                {
                    ActCode = "1",
                    ActDescription = "Fee Due",
                    SchoolId = student.SchoolID.HasValue ? student.SchoolID.ToString() : null,
                    SchoolName = student.School.SchoolName,
                    ClassSection = student.Class.ClassDescription + '#' + student.Section.SectionName,
                    Term = "",
                    RollNo = student.AdmissionNumber,
                    OutstandingAmount = 0,
                    Remarks = "Total due amount",
                    LocalTxnDtTime = DateTime.Now,
                    BilledAmount = 0,
                    StudentName = student.FirstName + " " + (student.MiddleName == null ? "" : student.MiddleName) + " " + student.LastName,
                    Company = company,
                    MerchantID = merchantID
                };
            }
        }

        public PaymentResponseDTO MakePayment(BankBillPaymentDTO payment)
        {
            var mutualRepository = new Repository.MutualRepository();

            var tokenSetting = new Domain.Setting.SettingBL(null).GetSettingValue<string>("BillPaymentAPIToken");

            if (payment.Token != tokenSetting)
            {
                throw new Exception("Invalid token");
            }

            var agentSetting = new Domain.Setting.SettingBL(null).GetSettingValue<string>("BillPaymentAPIAgent");

            if (payment.AgentId != agentSetting)
            {
                throw new Exception("Invalid agent");
            }


            var settingValue = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ISENABLEFEECOLL_DT");

            if (string.IsNullOrEmpty(settingValue) || settingValue == "0")
            {
                return new PaymentResponseDTO()
                {
                    description = "Transaction Completed",//"Enable Direct Transfer Collection",// returnCollection.FeeReceiptNo + " - Transaction Completed",
                    referenceNumber = "12345", //returnCollection.FeeReceiptNo,
                    remarks = "Success",
                    resposeCode = 0
                };
            }
            //Temparary logs
            new PaymentRepository().MakePaymentErrorEntry(new Entity.Models.PaymentDetailsLog()
            {
                TransRef = payment.TransactionId,
                TransResult = Newtonsoft.Json.JsonConvert.SerializeObject(payment)
            });

            try
            {
                var student = new SchoolRepository().GetStudentDetail(payment.ChildQID, payment.StudentRollNumber);
                if (student == null)
                {
                    throw new Exception("Invalid student");
                }
                else if ( student.StudentPassportDetails == null ||!student.StudentPassportDetails.Any(x => x.NationalIDNo == payment.ChildQID))
                {
                    throw new Exception("Invalid QID");
                }
                Entity.Models.Settings.Sequence transactionNoSequence = new Entity.Models.Settings.Sequence();

                var dataTransferPaymodeID = int.Parse(new Domain.Setting.SettingBL(null).GetSettingValue<string>("DIRECTTRANSFERID"));
                var defaultStaffID = int.Parse(new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULTSTAFFID"));
                var bankID = int.Parse(new Domain.Setting.SettingBL(null).GetSettingValue<string>("BANKID"));
                var collectedStatusID = int.Parse(new Domain.Setting.SettingBL(null).GetSettingValue<string>("FEECOLLECTIONSTATUSID_COLLECTED"));

                try
                {
                    transactionNoSequence = mutualRepository.GetNextSequence("BILLDESK_TRANSACTION_NO", null);
                }
                catch (Exception ex)
                {
                    var data = ex;
                    transactionNoSequence = null;
                }
                var feeDue = FeeDueGenerationMapper.Mapper(_context)
                  .FillPendingFeesForBank(student.ClassID.Value, student.StudentIID);

                if (feeDue.Count() == 0 || feeDue.Sum(x => x.InvoiceAmount).Value == 0)
                {
                    return new PaymentResponseDTO()
                    {
                        description = "Transaction not Completed",// returnCollection.FeeReceiptNo + " - Transaction Completed",
                        referenceNumber = "12345",
                        remarks = "No pending fees",
                        resposeCode = 1
                    };
                }
                var feeCollection = new FeeCollectionDTO();
                var payList = new List<FeeCollectionPaymentModeMapDTO>();
                feeCollection.StudentID = student.StudentIID;
                feeCollection.ClassID = student.ClassID;
                feeCollection.SectionID = student.SectionID;
                feeCollection.SchoolID = student.SchoolID;
                feeCollection.AcadamicYearID = student.AcademicYearID;
                feeCollection.CollectionDate = System.DateTime.Now;
                feeCollection.Amount = feeDue.Sum(x => x.InvoiceAmount).Value;
                feeCollection.PaidAmount = feeDue.Sum(x => x.InvoiceAmount).Value;
                feeCollection.CashierID = defaultStaffID;
                feeCollection.CreatedBy = (int?)student.Parent?.LoginID;
                payList.Add(new FeeCollectionPaymentModeMapDTO()
                {
                    Amount = feeDue.Sum(x => x.InvoiceAmount).Value,
                    BankID = bankID,
                    PaymentModeID = dataTransferPaymodeID
                });
                feeCollection.FeeCollectionPaymentModeMapDTO = payList;
                feeCollection.FeeCollectionStatusID = collectedStatusID;
                feeCollection.GroupTransactionNumber = transactionNoSequence == null ? null : transactionNoSequence.Prefix == null ? null : transactionNoSequence.Prefix + transactionNoSequence.LastSequence;
                feeCollection.EmailID = student?.Parent?.GaurdianEmail;
                feeCollection.StudentClassCode = student?.Class?.Code;

                var feeData = new List<FeeCollectionFeeTypeDTO>();
                feeCollection.FeeTypes = new List<FeeCollectionFeeTypeDTO>();
                feeCollection.FeeFines = new List<FeeCollectionFeeFinesDTO>();
                if (feeDue.Any())
                {
                    feeDue.All(feetypess =>
                    {
                        feeData = (from n in feetypess.FeeDueFeeTypeMap
                                   where (n.Amount ?? 0) != 0
                                   select new FeeCollectionFeeTypeDTO()
                                   {
                                       Amount = (n.Amount ?? 0),
                                       Balance = 0,
                                       NowPaying = (n.Amount ?? 0) - (n.CreditNoteAmount ?? 0) -(n.CollectedAmount??0),
                                       CreditNoteAmount = n.CreditNoteAmount ?? 0,
                                       FeeDueFeeTypeMapsID = n.FeeDueFeeTypeMapsIID,
                                       CreditNoteFeeTypeMapID = n.CreditNoteFeeTypeMapID,
                                       CollectedAmount = n.CollectedAmount,
                                       StudentFeeDueID = n.StudentFeeDueID,
                                       FeeStructureFeeMapID = n.FeeStructureFeeMapID,
                                       CreatedBy = feeCollection.CreatedBy,
                                       MontlySplitMaps = !n.FeeDueMonthlySplit.Any(w => (w.Amount ?? 0) != 0) ? new List<FeeCollectionMonthlySplitDTO>()
                                                                    : (from m in n.FeeDueMonthlySplit
                                                                       where (m.Amount ?? 0) != 0
                                                                       select new FeeCollectionMonthlySplitDTO()
                                                                       {
                                                                           Amount = (m.Amount ?? 0),
                                                                           Balance =0,
                                                                           CreditNoteAmount = m.CreditNoteAmount ?? 0,
                                                                           NowPaying = (m.Amount ?? 0) - (m.CreditNoteAmount ?? 0) - (m.CollectedAmount ?? 0),
                                                                           MonthID = m.MonthID,
                                                                           FeeDueMonthlySplitID = m.FeeDueMonthlySplitIID,
                                                                           Year = m.Year,
                                                                           RefundAmount = m.RefundAmount,
                                                                           TaxAmount = m.TaxAmount,
                                                                           CreatedBy = feeCollection.CreatedBy,
                                                                           TaxPercentage = m.TaxPercentage,
                                                                       }
                                                                     ).ToList(),

                                       FeeMasterID = n.FeeMasterID,
                                       FeePeriodID = n.FeePeriodID,

                                   }).ToList();

                        if (feeData.Any())
                            feeCollection.FeeTypes.AddRange(feeData);
                        return true;
                    });
                }

                var result = FeeCollectionMapper.Mapper(_context).SaveEntity(feeCollection);

                var returnCollection = Newtonsoft.Json.JsonConvert.DeserializeObject<FeeCollectionDTO>(result);
                // EmailProcess(feeCollection);
                if (returnCollection != null)
                {
                    return new PaymentResponseDTO()
                    {
                        description = "Transaction Completed",// returnCollection.FeeReceiptNo + " - Transaction Completed",
                        referenceNumber = returnCollection.FeeReceiptNo,
                        remarks = "Success",
                        resposeCode = 0,
                        mailID = returnCollection.EmailID,
                        id = returnCollection.FeeCollectionIID,
                        schoolID = returnCollection.SchoolID,
                        admissionNumber = returnCollection.AdmissionNo,
                        receiptNo = returnCollection.FeeReceiptNo,
                        AcadamicYearID=returnCollection.AcadamicYearID,
                    };
                }
                else
                {
                    return new PaymentResponseDTO()
                    {
                        description = "Transaction not Completed",// returnCollection.FeeReceiptNo + " - Transaction Completed",
                        referenceNumber = "12345",
                        remarks = "No pending fees",
                        resposeCode = 1
                    };
                }

            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<BillPaymentBL>.Fatal(ex.Message, ex);          
                return new PaymentResponseDTO()
                {
                    description = ex?.Message,// returnCollection.FeeReceiptNo + " - Transaction Completed",
                    referenceNumber = "123456", //returnCollection.FeeReceiptNo,
                    remarks = "Failure",
                    resposeCode = 1
                };

            }
        }

        public void EmailProcess(FeeCollectionDTO collectionDTO)
        {
            var emailTemplate = new NotificationEmailTemplateDTO();
            var emailSubject = string.Empty;
            var emailBody = string.Empty;

            string toEmailID = collectionDTO?.EmailID;
            var classCode = collectionDTO?.StudentClassCode?.ToLower();

            emailTemplate = EmailTemplateMapper.Mapper(_context).GetEmailTemplateDetails(EmailTemplates.FeeReceiptMail.ToString());

            emailSubject = emailTemplate.Subject;

            emailBody = emailTemplate.EmailTemplate;

            if (string.IsNullOrEmpty(classCode))
            {
                var classID = collectionDTO?.ClassID;
                if (classID.HasValue)
                {
                    var data = new Eduegate.Domain.Setting.SettingBL(_context).GetClassDetailByClassID(classID.Value);

                    classCode = data?.Code?.ToLower();
                }
            }

            var hostDet = new Domain.Setting.SettingBL(null).GetSettingValue<string>("HOST_NAME");
            string defaultMail = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_MAIL_ID");

            var mailParameters = new Dictionary<string, string>()
            {
                { "CLASS_CODE", classCode},
            };

            try
            {
                string mailMessage = new Eduegate.Domain.Notification.EmailNotificationBL(_context).PopulateBody(toEmailID, emailBody);

                if (emailBody != "")
                {
                    if (hostDet.ToLower() == "live")
                    {
                        new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(toEmailID, emailSubject, mailMessage, EmailTypes.AutoFeeReceipt, mailParameters);
                    }
                    else
                    {
                        new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(defaultMail, emailSubject, mailMessage, EmailTypes.AutoFeeReceipt, mailParameters);
                    }
                }

            }
            catch { }

        }

    }
}