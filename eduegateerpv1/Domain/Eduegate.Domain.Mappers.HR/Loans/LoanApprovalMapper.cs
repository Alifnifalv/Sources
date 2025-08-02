using Eduegate.Domain.Entity.HR.Loan;
using Eduegate.Domain.Entity.HR;
using Eduegate.Domain.Repository;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.HR.Loan;
using Microsoft.EntityFrameworkCore;
using Eduegate.Domain.Mappers.Notification.Helpers;
using Eduegate.Domain.Mappers.Notification;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using System.Runtime.Serialization;
using Eduegate.Services.Contracts.HR.Payroll;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.CommonDTO;
using System.Security.Cryptography;
using Eduegate.Domain.Setting;
using Eduegate.Domain.Mappers.HR.Payroll;
using Eduegate.Domain.Entity;
using Eduegate.Domain.Repository.Payroll;



namespace Eduegate.Domain.Mappers.HR.Loans
{
    public class LoanApprovalMapper : DTOEntityDynamicMapper
    {
        CallContext _callContext;
        public static LoanApprovalMapper Mapper(CallContext context)
        {
            var mapper = new LoanApprovalMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<LoanHeadDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private LoanHeadDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateHRContext())
            {
                var settingBL = new Domain.Setting.SettingBL(_callContext);
                var activeInstallmentStatus = settingBL.GetSettingValue<byte>("LOAN_ACTIVE_INSTALLMENT_STATUS");
                var entity = dbContext.LoanRequests.Where(X => X.LoanRequestIID == IID)
                                  .Include(i => i.Employee)
                                  .AsNoTracking()
                                  .FirstOrDefault();

                var loanHeadentity = dbContext.LoanHeads.Where(X => X.LoanRequestID == IID)
                    .Include(i => i.LoanDetails)
                    .Include(i => i.LoanStatus)
                    .Include(i => i.Employee)
                    .Include(i => i.LoanRequest)
                    .AsNoTracking()
                    .FirstOrDefault();

                var loanHeadDTO = new LoanHeadDTO()
                {
                    EmployeeID = entity.EmployeeID,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    LoanRequestDate = entity.LoanRequestDate,
                    Employee = new KeyValueDTO()
                    {
                        Value = entity.Employee.EmployeeCode + " - " + entity.Employee.FirstName + " " + entity.Employee.MiddleName + " " + entity.Employee.LastName,
                        Key = entity.EmployeeID.ToString()
                    },
                    LoanHeadIID = loanHeadentity != null ? loanHeadentity.LoanHeadIID : 0,
                    LoanRequestID = entity.LoanRequestIID,
                    LoanTypeID = entity.LoanTypeID,
                    LoanNo = loanHeadentity != null ? loanHeadentity.LoanNo : null,
                    NoOfInstallments = loanHeadentity == null ? entity.NoOfInstallments : loanHeadentity.NoOfInstallments,
                    LoanDate = loanHeadentity != null ? loanHeadentity.LoanDate : null,
                    PaymentStartDate = loanHeadentity == null ? entity.PaymentStartDate : loanHeadentity.PaymentStartDate,
                    LoanAmount = loanHeadentity == null ? entity.LoanAmount : loanHeadentity.LoanAmount,
                    InstallmentAmount = loanHeadentity == null ? entity.InstallmentAmount : loanHeadentity.InstallmentAmount,
                    Remarks = loanHeadentity == null ? entity.Remarks : loanHeadentity.Remarks,
                    LoanStatusID = loanHeadentity != null ? loanHeadentity.LoanStatusID : null,
                    LastInstallmentDate = loanHeadentity != null ? loanHeadentity.LastInstallmentDate : null,
                    LastInstallmentAmount = loanHeadentity != null ? loanHeadentity.LastInstallmentAmount : null,
                    PaymentEndDate = loanHeadentity != null ? loanHeadentity.PaymentEndDate : null,
                    LoanRequestNo = entity.LoanRequestNo,
                    LoanRequestStatusID = entity.LoanRequestStatusID
                };
                loanHeadDTO.LoanDetailDTOs = new List<LoanDetailDTO>();

                decimal sum = 0;
                int balanceAmt = 0;
                int installAmount = 0;

                decimal paidAmount = 0;
                decimal? balance = 0;

                loanHeadDTO.LoanDetailDTOs = new List<LoanDetailDTO>();
                if (loanHeadentity != null)
                {
                    foreach (var details in loanHeadentity.LoanDetails)
                    {
                        loanHeadDTO.LoanDetailDTOs.Add(new LoanDetailDTO()
                        {
                            LoanDetailID = details.LoanDetailID,

                            LoanHeadID = details.LoanHeadID,

                            InstallmentDate = details.InstallmentDate,

                            InstallmentReceivedDate = details.InstallmentReceivedDate,

                            InstallmentAmount = details.InstallmentAmount,

                            Remarks = details.Remarks,

                            IsPaid = details.IsPaid,

                            PaidAmount = details.PaidAmount,

                            LoanEntryStatusID = details.LoanEntryStatusID.HasValue ? details.LoanEntryStatusID : activeInstallmentStatus,
                        });

                    }
                    paidAmount = loanHeadDTO.LoanDetailDTOs.Sum(y => (y.PaidAmount ?? 0));
                    balance = loanHeadDTO.LoanAmount - paidAmount;
                    loanHeadDTO.Balance = balance;
                }

                else
                {
                    for (int i = 0, l = entity.NoOfInstallments.Value; i < l; i++)
                    {
                        DateTime installmentDate = entity.PaymentStartDate.Value.AddMonths(i);
                        loanHeadDTO.PaymentEndDate = installmentDate;
                        if (i == (entity.NoOfInstallments.Value - 1))
                        {
                            balanceAmt = (int)Math.Round((entity.LoanAmount ?? 0) - sum);

                            loanHeadDTO.LoanDetailDTOs.Add(new LoanDetailDTO()
                            {
                                LoanDetailID = 0,

                                LoanHeadID = null,

                                InstallmentDate = installmentDate,

                                InstallmentReceivedDate = null,

                                InstallmentAmount = balanceAmt,

                                Remarks = null,

                                IsPaid = null,

                                LoanEntryStatusID = activeInstallmentStatus

                            });

                            break;
                        }
                        else
                        {
                            installAmount = (int)Math.Round((double)(entity.LoanAmount ?? 0) / entity.NoOfInstallments.Value);
                            loanHeadDTO.LoanDetailDTOs.Add(new LoanDetailDTO()
                            {
                                LoanDetailID = 0,

                                LoanHeadID = null,

                                InstallmentDate = installmentDate,

                                InstallmentReceivedDate = null,

                                InstallmentAmount = installAmount,

                                Remarks = null,

                                IsPaid = null,

                                LoanEntryStatusID = activeInstallmentStatus

                            });

                            sum += (installAmount);
                        }

                    }
                    paidAmount = loanHeadDTO.LoanDetailDTOs.Sum(y => (y.PaidAmount ?? 0));
                    balance = loanHeadDTO.LoanAmount - paidAmount;
                    loanHeadDTO.Balance = balance;
                }
                return loanHeadDTO;
            }
        }

        public LoanHeadDTO FillLoanData(long? loanRequestID, long? loanHeadID)
        {
            return ToDTO(loanRequestID.Value);
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as LoanHeadDTO;
            Entity.Models.Settings.Sequence sequence = new Entity.Models.Settings.Sequence();
            MutualRepository mutualRepository = new MutualRepository();
            var settingBL = new Domain.Setting.SettingBL(_callContext);
            var activeInstallmentStatus = settingBL.GetSettingValue<byte>("LOAN_ACTIVE_INSTALLMENT_STATUS");
            var paidInstallmentStatus = settingBL.GetSettingValue<byte>("LOAN_PAID_INSTALLMENT_STATUS");
            var scheduledInstallmentStatus = settingBL.GetSettingValue<byte>("LOAN_SCHEDULE_INSTALLMENT_STATUS");
            var approveLoantStatus = settingBL.GetSettingValue<byte>("LOAN_STATUS_APPROVE");
            var rejectLoanStatus = settingBL.GetSettingValue<byte>("LOAN_STATUS_REJECT");
            var approveLoanRequestStatus = settingBL.GetSettingValue<byte>("APPROVE_LOAN_REQUEST_STATUS");
            var cancelLoanRequestStatus = settingBL.GetSettingValue<byte>("CANCEL_LOAN_REQUEST_STATUS");

            if (toDto.LoanRequestStatusID.HasValue && toDto.LoanRequestStatusID.Value == approveLoanRequestStatus)
            {
                if (toDto.PaymentEndDate == null || toDto.PaymentEndDate == DateTime.MinValue)
                {
                    throw new Exception("The payment end date is required!");
                }

                if (toDto.PaymentStartDate == null || toDto.PaymentStartDate == DateTime.MinValue)
                {
                    throw new Exception("The payment start date is required!");
                }
            }



            if (!toDto.LoanTypeID.HasValue)
            {
                throw new Exception("Please select Loan/Advance Type!");
            }
            if (!toDto.LoanStatusID.HasValue)
            {
                throw new Exception("Please select Loan/Advance Status!");
            }
            if (!toDto.LoanAmount.HasValue || toDto.LoanAmount == 0)
            {
                throw new Exception("Please enter Loan/Advacne Amount!");
            }
            if (!toDto.NoOfInstallments.HasValue || toDto.NoOfInstallments == 0)
            {
                throw new Exception("Please enter No. of Installments!");
            }
            if (!toDto.InstallmentAmount.HasValue || toDto.InstallmentAmount == 0)
            {
                throw new Exception("Installment Amount cannot be empty!");
            }
            if (toDto.LoanDate.HasValue && toDto.LoanRequestDate.HasValue && toDto.LoanDate.Value < toDto.LoanRequestDate.Value)
            {
                throw new Exception("Loan/Advance Date should be greater than Loan/Advance Request Date!");
            }
            if (toDto.PaymentStartDate.HasValue && toDto.LoanDate.HasValue && toDto.PaymentStartDate.Value <= toDto.LoanDate.Value)
            {
                throw new Exception("Payment Start Date should be greater than Loan/Advance  Date!");
            }

            var installmentSum = toDto.LoanDetailDTOs.Where(x => x.LoanEntryStatusID == activeInstallmentStatus || x.LoanEntryStatusID == paidInstallmentStatus || x.LoanEntryStatusID == scheduledInstallmentStatus).Sum(y => y.InstallmentAmount);
            if (toDto.LoanAmount != installmentSum)
            {
                throw new Exception("Loan/Advance Amount should be equal to sum of Installment Amount in the list!");
            }
            var completdLoanstatus = settingBL.GetSettingValue<byte>("COMPLETED_LOAN_STATUS");
            if (toDto.LoanStatusID.HasValue && toDto.LoanStatusID.Value == completdLoanstatus)
            {
                if (toDto.LoanAmount != toDto.LoanDetailDTOs.Sum(x => x.PaidAmount ?? 0))
                {
                    throw new Exception("The loan/advance amount is still outstanding, so cannot update the status to 'Completed'!");
                }
            }
            var totalSalary = new SalarySlipMapper().GetTotalSalaryAmount(toDto.EmployeeID.Value, toDto.LoanRequestDate);
            if (toDto.InstallmentAmount > totalSalary)
            {
                throw new Exception("This Loan/Advance amount has not been set due to salary considerations!");
            }
            #region Create New Sequence Number for Loan Request No.
            if (toDto.LoanHeadIID == 0)
            {
                try
                {
                    sequence = mutualRepository.GetNextSequence("LoanNO", null);
                }
                catch (Exception ex)
                {
                    throw new Exception("Please generate sequence with 'LoanNO'");
                }
            }
            #endregion
            //convert the dto to entity and pass to the repository.
            var entity = new LoanHead()
            {
                EmployeeID = toDto.EmployeeID,
                LoanHeadIID = toDto.LoanHeadIID,
                LoanRequestID = toDto.LoanRequestID,
                LoanTypeID = toDto.LoanTypeID,
                NoOfInstallments = toDto.NoOfInstallments,
                LoanDate = toDto.LoanDate,
                PaymentStartDate = toDto.PaymentStartDate,
                LoanAmount = toDto.LoanAmount,
                InstallmentAmount = toDto.InstallmentAmount,
                Remarks = toDto.Remarks,
                LoanStatusID = toDto.LoanStatusID,
                LastInstallmentDate = toDto.LastInstallmentDate,
                LastInstallmentAmount = toDto.LastInstallmentAmount,
                PaymentEndDate = toDto.PaymentEndDate,
                CreatedDate = toDto.LoanHeadIID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.LoanHeadIID > 0 ? DateTime.Now : dto.UpdatedDate,
                CreatedBy = toDto.LoanHeadIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.LoanHeadIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                LoanNo = toDto.LoanHeadIID == 0 ? sequence.Prefix + sequence.LastSequence : toDto.LoanNo,
            };


            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {

                var IIDs = toDto.LoanDetailDTOs
              .Select(a => a.LoanDetailID).ToList();

                //delete maps
                var entities = dbContext.LoanDetails.Where(x => x.LoanHeadID == entity.LoanHeadIID &&
                    !IIDs.Contains(x.LoanDetailID)).AsNoTracking().ToList();

                if (entities.IsNotNull())
                    dbContext.LoanDetails.RemoveRange(entities);

                entity.LoanDetails = new List<LoanDetail>();

                foreach (var details in toDto.LoanDetailDTOs)
                {
                    entity.LoanDetails.Add(new LoanDetail()
                    {
                        LoanDetailID = details.LoanDetailID,

                        LoanHeadID = details.LoanHeadID,

                        InstallmentDate = details.InstallmentDate.Value.Date,

                        InstallmentReceivedDate = details.InstallmentReceivedDate,

                        InstallmentAmount = details.InstallmentAmount,

                        Remarks = details.Remarks,

                        IsPaid = details.IsPaid,

                        LoanEntryStatusID = details.LoanEntryStatusID

                    });
                }

                if (toDto.LoanStatusID.HasValue && toDto.LoanStatusID == approveLoantStatus || toDto.LoanStatusID == 4)
                {
                    dbContext.LoanHeads.Add(entity);
                    if (entity.LoanHeadIID == 0)
                    {
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }
                    else
                    {
                        foreach (var details in entity.LoanDetails)
                        {
                            if (details.LoanDetailID != 0)
                            {
                                dbContext.Entry(details).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                            else
                            {
                                dbContext.Entry(details).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                        }

                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                    dbContext.SaveChanges();

                    try
                    {

                        SendNotifications(entity);
                    }
                    catch (Exception ex)
                    {
                        var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                            ? ex.InnerException?.Message : ex.Message;

                        Eduegate.Logger.LogHelper<string>.Fatal($"Loan Request Notification failed. Error message: {errorMessage}", ex);

                    }
                }
                #region Update Loan Request Status

                var loanRequest = dbContext.LoanRequests.Where(x => x.LoanRequestIID == toDto.LoanRequestID).FirstOrDefault();
                if (loanRequest != null)
                {
                    loanRequest.LoanRequestStatusID = toDto.LoanStatusID;

                    dbContext.Entry(loanRequest).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbContext.SaveChanges();


                }
                #endregion Update Loan Request Status

                return ToDTOString(ToDTO(entity.LoanRequestID.Value));
            }
        }

        private void SendNotifications(LoanHead entity)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var login = new EmployeeRepository().GetEmployeeByEmployeeID(entity.EmployeeID).LoginID;
                var message = string.Empty;
                var title = "Loan Request Status!";

                if (entity.LoanStatusID == 2)
                {
                    message = "Your loan request has been Approved and Loan No is " + entity.LoanNo;
                }
                else
                {
                    message = "Your loan request has been rejected by the financial manager.";
                }

                var settings = NotificationSetting.GetEmployeeAppSettings();

                long toLoginID = Convert.ToInt32(login);
                long fromLoginID = _context?.LoginID != null ? (long)_context.LoginID : 2;

                PushNotificationMapper.SendAndSavePushNotification(toLoginID, fromLoginID, message, title, settings);

            }
        }
    }
}