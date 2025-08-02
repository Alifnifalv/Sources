using Eduegate.Domain.Entity.HR;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Framework;
using Eduegate.Services.Contracts.HR.Loan;
using Eduegate.Domain.Entity.HR.Loan;
using Eduegate.Domain.Repository;
using Eduegate.Domain.Mappers.HR.Payroll;
using Eduegate.Domain.Entity;
using Eduegate.Domain.Mappers.Notification.Notifications;
using Eduegate.Domain.Mappers.Notification.Notifications.Helpers;
namespace Eduegate.Domain.Mappers.HR.Loans
{
    internal class LoanRequestMapper : DTOEntityDynamicMapper
    {
        CallContext _callContext;
        public static LoanRequestMapper Mapper(CallContext context)
        {
            var mapper = new LoanRequestMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<LoanRequestDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private LoanRequestDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateHRContext())
            {
                var entity = dbContext.LoanRequests.Where(X => X.LoanRequestIID == IID)
                    .Include(i => i.Employee)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new LoanRequestDTO()
                {
                    EmployeeID = entity.EmployeeID,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    Employee = new KeyValueDTO()
                    {
                        Value = entity.Employee.EmployeeCode + " - " + entity.Employee.FirstName + " " + entity.Employee.MiddleName + " " + entity.Employee.LastName,
                        Key = entity.EmployeeID.ToString()
                    },
                    LoanRequestIID = entity.LoanRequestIID,
                    LoanTypeID = entity.LoanTypeID,
                    LoanRequestNo = entity.LoanRequestNo,
                    LoanRequestStatusID = entity.LoanRequestStatusID,
                    NoOfInstallments = entity.NoOfInstallments,
                    LoanRequestDate = entity.LoanRequestDate,
                    PaymentStartDate = entity.PaymentStartDate,
                    LoanAmount = entity.LoanAmount,
                    InstallmentAmount = entity.InstallmentAmount,
                    Remarks = entity.Remarks,
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as LoanRequestDTO;
            Entity.Models.Settings.Sequence sequence = new Entity.Models.Settings.Sequence();
            MutualRepository mutualRepository = new MutualRepository();
            var settingBL = new Domain.Setting.SettingBL(_callContext);
            var requestStatus = settingBL.GetSettingValue<byte>("LOAN_REQUEST_STATUS_REQUEST");

            if (!toDto.LoanTypeID.HasValue)
            {
                throw new Exception("Please select Loan/Advance Type!");
            }
            if (!toDto.LoanRequestStatusID.HasValue)
            {
                throw new Exception("Please select Loan/Advacne Request Status!");
            }
            if (toDto.LoanRequestStatusID.Value != requestStatus && toDto.LoanRequestIID != 0)
            {
                throw new Exception("The Loan/Advance Request has already been approved/processed and cannot be edited!");
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
            if (toDto.PaymentStartDate.HasValue && toDto.LoanRequestDate.HasValue && toDto.PaymentStartDate.Value <= toDto.LoanRequestDate.Value)
            {
                throw new Exception("Payment Start Date should be greater than Loan/Advancne Request Date!");
            }
            var totalSalary = new SalarySlipMapper().GetTotalSalaryAmount(toDto.EmployeeID.Value, toDto.LoanRequestDate);
            if (toDto.InstallmentAmount > totalSalary)
            {
                throw new Exception("This Loan/Advance amount has not been set due to salary considerations!");
            }
            #region Create New Sequence Number for Loan Request No.
            if (toDto.LoanRequestIID == 0)
            {
                try
                {
                    sequence = mutualRepository.GetNextSequence("LoanRequestNO", null);
                }
                catch (Exception ex)
                {
                    throw new Exception("Please generate sequence with 'LoanNO'");
                }
            }
            #endregion
            //convert the dto to entity and pass to the repository.
            var entity = new LoanRequest()
            {
                LoanRequestIID = toDto.LoanRequestIID,
                EmployeeID = toDto.EmployeeID,
                LoanTypeID = toDto.LoanTypeID,
                LoanRequestStatusID = toDto.LoanRequestStatusID,
                NoOfInstallments = toDto.NoOfInstallments,
                LoanRequestDate = toDto.LoanRequestDate,
                PaymentStartDate = toDto.PaymentStartDate,
                LoanAmount = toDto.LoanAmount,
                InstallmentAmount = toDto.InstallmentAmount,
                Remarks = toDto.Remarks,
                CreatedDate = toDto.LoanRequestIID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.LoanRequestIID > 0 ? DateTime.Now : dto.UpdatedDate,
                CreatedBy = toDto.LoanRequestIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.LoanRequestIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                LoanRequestNo = toDto.LoanRequestIID == 0 ? sequence.Prefix + sequence.LastSequence : toDto.LoanRequestNo,
            };

            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {

                if (entity.LoanRequestIID == 0)
                {
                    dbContext.LoanRequests.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    dbContext.Attach(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
            }

            try
            {
                if (toDto.LoanRequestIID == 0)
                    SendNotifications(entity);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                    ? ex.InnerException?.Message : ex.Message;

                Eduegate.Logger.LogHelper<string>.Fatal($"Loan Request Notification failed. Error message: {errorMessage}", ex);

            }

            return ToDTOString(ToDTO(entity.LoanRequestIID));
        }

        private void SendNotifications(LoanRequest entity)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var loginIDs = new List<long?>();

                if (entity != null)
                {
                    loginIDs = GetLoginIDByLoanClaims();
                }

                var message = "Loan Request " + entity.LoanRequestNo + " has been requested.";
                var title = "New Loan Request";
                var settings = NotificationSetting.GetEmployeeAppSettings();

                foreach (var login in loginIDs)
                {
                    long toLoginID = Convert.ToInt32(login);
                    long fromLoginID = _context?.LoginID != null ? (long)_context.LoginID : 2;

                    PushNotificationMapper.SendAndSavePushNotification(toLoginID, fromLoginID, message, title, settings);
                }
            }
        }

        private List<long?> GetLoginIDByLoanClaims()
        {
            var loginIDs = new List<long?>();
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var loanClaims = dbContext.ClaimSetLoginMaps.Where(a => a.ClaimSetID == 50).ToList().Select(a => a.LoginID);

                loginIDs.AddRange(loanClaims);
            }

            return loginIDs;

        }
    }
}