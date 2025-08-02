using Eduegate.Domain.Entity.HR.Loan;
using Eduegate.Domain.Entity.HR;
using Eduegate.Domain.Repository;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.HR.Loan;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Domain.Mappers.HR.Payroll;


namespace Eduegate.Domain.Mappers.HR.Loans
{
    public class LoanMapper : DTOEntityDynamicMapper
    {
        CallContext _callContext;
        public static LoanMapper Mapper(CallContext context)
        {
            var mapper = new LoanMapper();
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
                var scheduleInstallmentstatus = settingBL.GetSettingValue<byte>("LOAN_SCHEDULE_INSTALLMENT_STATUS");               
                var paidInstallmentstatus = settingBL.GetSettingValue<byte>("LOAN_PAID_INSTALLMENT_STATUS");

                var loanHeadentity = dbContext.LoanHeads.Where(X => X.LoanHeadIID == IID)
                   .Include(i => i.LoanStatus)
                   .Include(i => i.LoanDetails)
                   .Include(i => i.Employee)
                   .Include(i => i.LoanRequest)
                   .AsNoTracking()
                   .FirstOrDefault();

                var loanHeadDTO = new LoanHeadDTO()
                {
                    EmployeeID = loanHeadentity.EmployeeID,
                    CreatedDate = loanHeadentity.CreatedDate,
                    UpdatedDate = loanHeadentity.UpdatedDate,
                    CreatedBy = loanHeadentity.CreatedBy,
                    UpdatedBy = loanHeadentity.UpdatedBy,
                    LoanRequestDate = loanHeadentity.LoanRequest.LoanRequestDate,
                    Employee = new KeyValueDTO()
                    {
                        Value = loanHeadentity.Employee.EmployeeCode + " - " + loanHeadentity.Employee.FirstName + " " + loanHeadentity.Employee.MiddleName + " " + loanHeadentity.Employee.LastName,
                        Key = loanHeadentity.EmployeeID.ToString()
                    },
                    LoanHeadIID = loanHeadentity != null ? loanHeadentity.LoanHeadIID : 0,
                    LoanRequestID = loanHeadentity.LoanRequestID,
                    LoanTypeID = loanHeadentity.LoanTypeID,
                    LoanNo = loanHeadentity != null ? loanHeadentity.LoanNo : null,
                    NoOfInstallments = loanHeadentity.NoOfInstallments,
                    LoanDate = loanHeadentity != null ? loanHeadentity.LoanDate : null,
                    PaymentStartDate = loanHeadentity == null ? loanHeadentity.PaymentStartDate : loanHeadentity.PaymentStartDate,
                    LoanAmount = loanHeadentity == null ? loanHeadentity.LoanAmount : loanHeadentity.LoanAmount,
                    InstallmentAmount = loanHeadentity == null ? loanHeadentity.InstallmentAmount : loanHeadentity.InstallmentAmount,
                    Remarks = loanHeadentity == null ? loanHeadentity.Remarks : loanHeadentity.Remarks,
                    LoanStatusID = loanHeadentity != null ? loanHeadentity.LoanStatusID : null,
                    LastInstallmentDate = loanHeadentity != null ? loanHeadentity.LastInstallmentDate : null,
                    LastInstallmentAmount = loanHeadentity != null ? loanHeadentity.LastInstallmentAmount : null,
                    PaymentEndDate = loanHeadentity != null ? loanHeadentity.PaymentEndDate : null,
                    LoanRequestNo = loanHeadentity.LoanRequest.LoanRequestNo,
                    LoanRequestStatusID = loanHeadentity.LoanRequest.LoanRequestStatusID
                };
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

                            IsDisableStatus = details.LoanEntryStatusID == scheduleInstallmentstatus || details.LoanEntryStatusID == paidInstallmentstatus ? true : false,

                        });

                    }
                }
                var paidAmount = loanHeadDTO.LoanDetailDTOs.Sum(y => (y.PaidAmount ?? 0));
                var balance = loanHeadDTO.LoanAmount - paidAmount;
                loanHeadDTO.Balance = balance;

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
                throw new Exception("Please enter Loan/Advance Amount!");
            }
            if (!toDto.NoOfInstallments.HasValue || toDto.NoOfInstallments == 0)
            {
                throw new Exception("Please enter No. of Installments!");
            }
            if (!toDto.InstallmentAmount.HasValue || toDto.InstallmentAmount == 0)
            {
                throw new Exception("Installment Amount cannot be empty!");
            }
            //if (toDto.LoanTypeID==1)
            //{ 
            
            //}
            var settingBL = new Domain.Setting.SettingBL(_callContext);
            var activeInstallmentStatus = settingBL.GetSettingValue<byte>("LOAN_ACTIVE_INSTALLMENT_STATUS");
            var paidInstallmentStatus = settingBL.GetSettingValue<byte>("LOAN_PAID_INSTALLMENT_STATUS");
            var scheduledInstallmentStatus = settingBL.GetSettingValue<byte>("LOAN_SCHEDULE_INSTALLMENT_STATUS");

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

                        PaidAmount = details.PaidAmount,

                        LoanEntryStatusID = details.LoanEntryStatusID

                    });
                }
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
            }

            return ToDTOString(ToDTO(entity.LoanHeadIID));
        }
    }
}