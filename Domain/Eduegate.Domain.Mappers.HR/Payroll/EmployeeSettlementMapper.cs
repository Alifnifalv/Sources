using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.HR.Payroll;
using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Domain.Entity.HR;
using Eduegate.Framework.Extensions;
using Eduegate.Domain.Repository.Frameworks;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using Eduegate.Services.Contracts.HR.Leaves;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlTypes;
using Eduegate.Domain.Entity.HR.Payroll;
using Eduegate.Domain.Repository;
using Eduegate.Services.Contracts.CommonDTO;
using System.Globalization;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Eduegate.Services.Contracts.Catalog;
using Microsoft.VisualBasic;
using Azure.Identity;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Domain.Entity.HR.Loan;

using Eduegate.Domain.Setting;
using Eduegate.Domain.Entity.School.Models;
using System.Runtime.InteropServices;
using Eduegate.Domain.Entity.Models;
using FirebaseAdmin.Messaging;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Eduegate.Domain.Entity.HR.Leaves;
using Eduegate.Services.Contracts.Leads;
using Eduegate.Services.Contracts.Mutual;

namespace Eduegate.Domain.Mappers.HR.Payroll
{

    public class EmployeeSettlementMapper : DTOEntityDynamicMapper
    {
        CallContext _callContext;
        public static EmployeeSettlementMapper Mapper(CallContext context)
        {
            var mapper = new EmployeeSettlementMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<SalarySlipDTO>(entity);
        }
        public override string ToDTOString(BaseMasterDTO dto)
        {
            return JsonConvert.SerializeObject(dto);
        }


        public override string SaveEntity(BaseMasterDTO dto)
        {
            var slipdto = dto as SalarySlipDTO;
            string message = string.Empty;

            return message;
        }
        public override string GetEntity(long IID)
        {
            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                var entity = dbContext.EmployeeSalarySettlements.Where(X => X.EmployeeSalarySettlementIID == IID)
                    .Include(x => x.Employee)
                    .AsNoTracking()
                    .FirstOrDefault();

                var settlement = new EmployeeSettlementDTO()
                {
                    EmployeeSalarySettlementIID = entity.EmployeeSalarySettlementIID,
                    EmployeeID = entity.EmployeeID,
                    Employee = new KeyValueDTO()
                    {
                        Value = entity.Employee.EmployeeCode + " - " + entity.Employee.FirstName + " " + entity.Employee.MiddleName + " " + entity.Employee.LastName,
                        Key = entity.EmployeeID.ToString()
                    },
                    SalaryCalculationDate = entity.SalaryCalculationDate,
                    EmployeeSettlementDate = entity.EmployeeSettlementDate,
                    BranchID = entity.Employee.BranchID,
                    EmployeeCode = entity.Employee?.EmployeeCode,
                    AnnualLeaveEntitilements = entity.AnnualLeaveEntitilements,
                    LeaveDueFrom = entity.LeaveDueFrom,
                    NoofSalaryDays = entity.NoofSalaryDays,
                    LeaveStartDate = entity.LeaveStartDate,
                    LeaveEndDate = entity.LeaveEndDate,
                    NoofDaysInTheMonthLS = entity.NoofDaysInTheMonthLS,
                    NoofDaysInTheMonth = entity.NoofDaysInTheMonth,
                    EarnedLeave = entity.EarnedLeave,
                    LossofPay = entity.LossofPay,
                    EmployeeSettlementTypeID = entity.EmployeeSettlementTypeID,
                    DateOfJoining = entity.Employee.DateOfJoining,
                    DateOfLeaving = entity.DateOfLeaving,
                    EndofServiceDaysPerYear = entity.EndofServiceDaysPerYear,
                    NoofDaysInTheMonthEoSB = entity.NoofDaysInTheMonthEoSB
                };

                var settlementlist = dbContext.EmployeeSalarySettlements.Where(sa => sa.EmployeeID == entity.EmployeeID
                && sa.EmployeeSettlementTypeID == entity.EmployeeSettlementTypeID
                && sa.SalaryCalculationDate.Value.Month == settlement.SalaryCalculationDate.Value.Month
                && sa.SalaryCalculationDate.Value.Year == settlement.SalaryCalculationDate.Value.Year
                )
                    .Include(i => i.SalaryComponent)
                    .AsNoTracking()
                    .ToList();

                settlement.SalarySlipDTOs = new List<SalarySlipDTO>();

                foreach (var salarycomponent in settlementlist)
                {
                    settlement.SalarySlipDTOs.Add(new SalarySlipDTO()
                    {

                        SalaryComponentID = salarycomponent.SalaryComponentID,
                        Amount = salarycomponent.SalaryComponentAmount,
                        Description = salarycomponent.Description,
                        SalaryComponentKeyValue = salarycomponent.SalaryComponentID.HasValue ? new KeyValueDTO()
                        {
                            Key = salarycomponent.SalaryComponentID.ToString(),
                            Value = salarycomponent.SalaryComponent.Description
                        } : new KeyValueDTO(),
                        NoOfDays = salarycomponent.NoofSalaryDays,
                        ReportContentID = salarycomponent.ReportContentID,
                    });
                }

                return ToDTOString(settlement);
            }
        }

        public void SaveLeaveOrVacationSalarySettlements(List<EmployeeSettlementDTO> dto)
        {
            try
            {
                using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
                {
                    var settingBL = new Domain.Setting.SettingBL(_callContext);
                    var approvedoanStatus = settingBL.GetSettingValue<int>("LOAN_STATUS_APPROVE");
                    var loanComponentID = settingBL.GetSettingValue<int>("LOAN_COMPONENT");
                    var advanceComponentID = settingBL.GetSettingValue<int>("ADVANCE_COMPONENT");
                    var activeInstallmentstatus = settingBL.GetSettingValue<byte>("LOAN_ACTIVE_INSTALLMENT_STATUS");
                    var scheduleInstallmentstatus = settingBL.GetSettingValue<byte>("LOAN_SCHEDULE_INSTALLMENT_STATUS");
                    var settlementEntityList = new List<EmployeeSalarySettlement>();
                    if (dto.Count > 0)
                    {
                        var employeeIds = dto.Select(x => x.EmployeeID).ToList();
                        var leaveStartDate = dto.Select(x => x.LeaveStartDate).FirstOrDefault();

                        if (leaveStartDate != null)
                        {
                            var employeeSalarySettleList = (from ss in dbContext.EmployeeSalarySettlements
                                                            where employeeIds.Contains(ss.EmployeeID) && ss.EmployeeSettlementTypeID == 1
                                                            && ss.LeaveStartDate.Value.Date == leaveStartDate.Value.Date
                                                            select ss).AsNoTracking().ToList();
                            if (employeeSalarySettleList.Count > 0 && employeeSalarySettleList.IsNotNull())
                            {
                                dbContext.EmployeeSalarySettlements.RemoveRange(employeeSalarySettleList);
                                dbContext.SaveChanges();
                            }
                        }
                        foreach (var settlements in dto)
                        {
                            foreach (var settData in settlements.SalarySlipDTOs)
                            {
                                var settlementEntity = new EmployeeSalarySettlement()
                                {
                                    EmployeeID = settlements.EmployeeID,
                                    EmployeeSettlementTypeID = settlements.EmployeeSettlementTypeID,
                                    EmployeeSettlementDate = settlements.SalaryCalculationDate,
                                    LeaveDueDate = settlements.LeaveDueFrom,
                                    LeaveDueFrom = settlements.LeaveDueFrom,
                                    LeaveStartDate = settlements.LeaveStartDate,
                                    LeaveEndDate = settlements.LeaveEndDate,
                                    SalaryCalculationDate = settlements.SalaryCalculationDate,
                                    NoofSalaryDays = settData.NoOfDays,
                                    NoofLeaveSalaryDays = settData.NoOfDays,
                                    AnnualLeaveEntitilements = settlements.AnnualLeaveEntitilements,
                                    EarnedLeave = settlements.EarnedLeave,
                                    LossofPay = settlements.LossofPay,
                                    NoofDaysInTheMonth = settlements.NoofDaysInTheMonth,
                                    NoofDaysInTheMonthLS = settlements.NoofDaysInTheMonthLS,
                                    SalaryComponentID = settData.SalaryComponentID,
                                    SalaryComponentAmount = settData.Amount,
                                    Description = settData.Description,
                                    ReportContentID = settData.ReportContentID,
                                    DateOfLeaving = settlements.DateOfLeaving,
                                    EndofServiceDaysPerYear = settlements.EndofServiceDaysPerYear,
                                    NoofDaysInTheMonthEoSB = settlements.NoofDaysInTheMonthEoSB,
                                    CreatedBy = settlements.CreatedBy.HasValue ? settlements.CreatedBy : null,
                                    UpdatedBy = settlements.CreatedBy.HasValue ? (int)_context.LoginID : null,
                                    CreatedDate = settlements.CreatedDate.HasValue ? settlements.CreatedDate : DateTime.Now,
                                    UpdatedDate = settlements.CreatedDate.HasValue ? DateTime.Now : settlements.UpdatedDate,

                                };
                                settlementEntityList.Add(settlementEntity);
                            }
                        }
                        dbContext.EmployeeSalarySettlements.AddRange(settlementEntityList);
                        dbContext.SaveChanges();

                        #region Loan/Advance                          

                        var loanData = (from lh in dbContext.LoanHeads
                                        where employeeIds.Contains(lh.EmployeeID)
                                                        && lh.LoanStatusID == approvedoanStatus
                                                        && lh.LoanDetails.Select(x => x.LoanEntryStatusID).Contains(activeInstallmentstatus)
                                        select lh)
                                        .Include(i => i.LoanDetails)
                                        .AsNoTracking().ToList();


                        var listHeadIds = loanData.Select(x => x.LoanHeadIID).Distinct().ToList();
                        var loanDetail = new List<LoanDetail>();

                        loanDetail = (from eld in dbContext.LoanDetails
                                      where listHeadIds.Contains(eld.LoanHeadID.Value)
                                      && (eld.InstallmentDate.Value.Year == leaveStartDate.Value.Year
                                      && eld.InstallmentDate.Value.Month == leaveStartDate.Value.Month)
                                      && eld.LoanEntryStatusID == activeInstallmentstatus
                                      select eld).AsNoTracking().ToList();


                        var listDetailIds = loanDetail.Select(x => x.LoanDetailID).Distinct().ToList();
                        if (listDetailIds.Any())
                        {
                            listDetailIds
                                .All(w =>
                                {
                                    var dRepLoanDetail = dbContext.LoanDetails.Where(x => x.LoanDetailID == w).AsNoTracking().FirstOrDefault();
                                    if (dRepLoanDetail != null)
                                    {
                                        dRepLoanDetail.LoanEntryStatusID = scheduleInstallmentstatus;
                                        dbContext.Entry(dRepLoanDetail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                        dbContext.SaveChanges();
                                    }
                                    return true;
                                });
                        }

                        #endregion Loan/Advance
                    }
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                         ? ex.InnerException?.Message : ex.Message;
                Eduegate.Logger.LogHelper<string>.Fatal($". Error message: {errorMessage}", ex);
            }
        }
        public OperationResultWithIDsDTO SaveSalarySettlement(EmployeeSettlementDTO dto)

        {
            var toDto = dto as EmployeeSettlementDTO;
            var operationResultWithIDsDTO = new OperationResultWithIDsDTO();
            long slipID = 0;
            try
            {
                SalarySlip slip = null;
                byte? pendingStatusID = 0;
                var employeeSalarySettleList = new List<EmployeeSalarySettlement>();

                using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
                {
                    var settingBL = new Domain.Setting.SettingBL(_callContext);

                    var pendingStatus = new SettingRepository().GetSettingDetail("SALARYSLIPSTATUS_PENDING");
                    var endOfSalaryBenefit = new Domain.Setting.SettingBL(null).GetSettingValue<byte?>("END_OF_SERVICE_BENEFIT");
                    if (pendingStatus.IsNull())
                    {
                        operationResultWithIDsDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                        operationResultWithIDsDTO.Message = "There is no SALARYSLIPSTATUS_PENDING in settings!";
                        return operationResultWithIDsDTO;
                    }
                    var salarySlipApprovedID = settingBL.GetSettingValue<byte?>("SALARYSLIPAPPROVED_ID");
                    if (salarySlipApprovedID.IsNull())
                    {
                        operationResultWithIDsDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                        operationResultWithIDsDTO.Message = "There is no SALARYSLIPAPPROVED_ID in settings!";
                        return operationResultWithIDsDTO;
                    }

                    var salarySlipEntities = new List<SalarySlip>();
                    if (toDto.EmployeeSettlementTypeID == 1)
                    {
                        salarySlipEntities = (from ss in dbContext.SalarySlips
                                              join ess in dbContext.EmployeeSalarySettlements on ss.SalaryComponentID equals ess.SalaryComponentID
                                              where ss.EmployeeID.Value == toDto.EmployeeID && ess.EmployeeSettlementTypeID == 1
                                              && (ss.SlipDate.Value.Year == toDto.SalaryCalculationDate.Value.Year
                                              && ss.SlipDate.Value.Month == toDto.SalaryCalculationDate.Value.Month)
                                              select ss).AsNoTracking().ToList();
                    }
                    else
                    {
                        salarySlipEntities = (from ss in dbContext.SalarySlips
                                              join ess in dbContext.EmployeeSalarySettlements on ss.SalaryComponentID equals ess.SalaryComponentID
                                              where ss.EmployeeID.Value == toDto.EmployeeID && ess.EmployeeSettlementTypeID == 2
                                              && (ss.SlipDate.Value.Year == toDto.SalaryCalculationDate.Value.Year
                                              && ss.SlipDate.Value.Month == toDto.SalaryCalculationDate.Value.Month)
                                              select ss).AsNoTracking().ToList();
                    }
                    var salarySlipApproved = salarySlipApprovedID;
                    if (salarySlipEntities.Any(x => x.SalarySlipStatusID == salarySlipApproved))
                    {
                        operationResultWithIDsDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                        operationResultWithIDsDTO.Message = "Cannot be saved/Modified.Already Issued!";
                        return operationResultWithIDsDTO;
                    }
                    pendingStatusID = byte.Parse(pendingStatus.SettingValue);
                    if (toDto.SalarySlipDTOs.Count > 0)
                    {
                        if (salarySlipEntities.IsNotNull())
                        {
                            dbContext.SalarySlips.RemoveRange(salarySlipEntities);
                            dbContext.SaveChanges();
                        }
                    }
                    if (toDto.EmployeeSettlementTypeID == 1)
                    {
                        employeeSalarySettleList = (from ss in dbContext.EmployeeSalarySettlements
                                                    where ss.EmployeeID.Value == toDto.EmployeeID && ss.EmployeeSettlementTypeID == 1 && (ss.SalaryCalculationDate.Value.Year == toDto.SalaryCalculationDate.Value.Year
                                                    && ss.SalaryCalculationDate.Value.Month == toDto.SalaryCalculationDate.Value.Month)
                                                    select ss).AsNoTracking().ToList();
                    }
                    else
                    {
                        employeeSalarySettleList = (from ss in dbContext.EmployeeSalarySettlements
                                                    where ss.EmployeeID.Value == toDto.EmployeeID && ss.EmployeeSettlementTypeID == 2
                                                    select ss).AsNoTracking().ToList();
                    }
                    if (employeeSalarySettleList.Count > 0 && employeeSalarySettleList.IsNotNull())
                    {
                        dbContext.EmployeeSalarySettlements.RemoveRange(employeeSalarySettleList);
                        dbContext.SaveChanges();
                    }
                }

                using (dbEduegateHRContext dbContextSal = new dbEduegateHRContext())
                {
                    var settingBL = new Domain.Setting.SettingBL(_callContext);
                    var approvedoanStatus = settingBL.GetSettingValue<int>("LOAN_STATUS_APPROVE");
                    var loanComponentID = settingBL.GetSettingValue<int>("LOAN_COMPONENT");
                    var advanceComponentID = settingBL.GetSettingValue<int>("ADVANCE_COMPONENT");
                    var activeInstallmentstatus = settingBL.GetSettingValue<byte>("LOAN_ACTIVE_INSTALLMENT_STATUS");
                    var scheduleInstallmentstatus = settingBL.GetSettingValue<byte>("LOAN_SCHEDULE_INSTALLMENT_STATUS");
                    if (toDto.SalarySlipDTOs.Count > 0)
                    {
                        foreach (var e in toDto.SalarySlipDTOs)
                        {

                            var settlementEntity = new EmployeeSalarySettlement()
                            {

                                EmployeeID = toDto.EmployeeID,
                                EmployeeSettlementTypeID = toDto.EmployeeSettlementTypeID,
                                EmployeeSettlementDate = toDto.EmployeeSettlementDate,
                                LeaveDueDate = toDto.LeaveDueFrom,
                                LeaveDueFrom = toDto.LeaveDueFrom,
                                LeaveStartDate = toDto.LeaveStartDate,
                                LeaveEndDate = toDto.LeaveEndDate,
                                SalaryCalculationDate = toDto.SalaryCalculationDate,
                                NoofSalaryDays = e.NoOfDays,
                                NoofLeaveSalaryDays = e.NoOfDays,
                                AnnualLeaveEntitilements = toDto.AnnualLeaveEntitilements,
                                EarnedLeave = toDto.EarnedLeave,
                                LossofPay = toDto.LossofPay,
                                NoofDaysInTheMonth = toDto.NoofDaysInTheMonth,
                                NoofDaysInTheMonthLS = toDto.NoofDaysInTheMonthLS,
                                SalaryComponentID = e.SalaryComponentID,
                                SalaryComponentAmount = e.Amount,
                                Description = e.Description,
                                ReportContentID = e.ReportContentID,
                                DateOfLeaving = toDto.DateOfLeaving,
                                EndofServiceDaysPerYear = toDto.EndofServiceDaysPerYear,
                                NoofDaysInTheMonthEoSB = toDto.NoofDaysInTheMonthEoSB,
                                CreatedBy = toDto.CreatedBy.HasValue ? toDto.CreatedBy : (int)_context.LoginID,
                                UpdatedBy = toDto.CreatedBy.HasValue ? (int)_context.LoginID : toDto.UpdatedBy,
                                CreatedDate = toDto.CreatedDate.HasValue ? toDto.CreatedDate : DateTime.Now,
                                UpdatedDate = toDto.CreatedDate.HasValue ? DateTime.Now : toDto.UpdatedDate,

                            };
                            if (settlementEntity.EmployeeSalarySettlementIID != 0)
                            {
                                dbContextSal.Entry(settlementEntity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                            else
                            {
                                dbContextSal.Entry(settlementEntity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                            dbContextSal.SaveChanges();
                            var entity = new SalarySlip()
                            {
                                SalarySlipIID = e.SalarySlipIID,
                                EmployeeID = toDto.EmployeeID,
                                SlipDate = toDto.SalaryCalculationDate,
                                SalaryComponentID = e.SalaryComponentID,
                                Amount = e.Amount,
                                Description = e.Description,
                                SalarySlipStatusID = pendingStatusID,
                                IsVerified = false,
                                NoOfDays = e.NoOfDays,
                                NoOfHours = e.NoOfHours,
                                CreatedBy = (int)_context.LoginID,
                                UpdatedBy = (int)_context.LoginID,
                                CreatedDate = DateTime.Now,
                                UpdatedDate = DateTime.Now,
                            };

                            slip = entity;

                            if (entity.SalarySlipIID == 0)
                            {
                                dbContextSal.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                            else
                            {
                                dbContextSal.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }

                            dbContextSal.SaveChanges();
                            slipID = entity.SalarySlipIID;


                        }
                        #region Loan/Advance                          

                        var loanData = (from lh in dbContextSal.LoanHeads
                                        where lh.EmployeeID == toDto.EmployeeID
                                                        && lh.LoanStatusID == approvedoanStatus
                                                        && lh.LoanDetails.Select(x => x.LoanEntryStatusID).Contains(activeInstallmentstatus)
                                        select lh)
                                        .Include(i => i.LoanDetails)
                                        .AsNoTracking().ToList();


                        var listHeadIds = loanData.Select(x => x.LoanHeadIID).Distinct().ToList();
                        var loanDetail = new List<LoanDetail>();
                        if (toDto.EmployeeSettlementTypeID == 1)
                        {
                            loanDetail = (from eld in dbContextSal.LoanDetails
                                          where listHeadIds.Contains(eld.LoanHeadID.Value)
                                          && (eld.InstallmentDate.Value.Year == toDto.SalaryCalculationDate.Value.Year
                                          && eld.InstallmentDate.Value.Month == toDto.SalaryCalculationDate.Value.Month)
                                          && eld.LoanEntryStatusID == activeInstallmentstatus
                                          select eld).AsNoTracking().ToList();
                        }
                        else
                        {
                            loanDetail = (from eld in dbContextSal.LoanDetails
                                          where listHeadIds.Contains(eld.LoanHeadID.Value)
                                          && eld.LoanEntryStatusID == activeInstallmentstatus
                                          select eld).AsNoTracking().ToList();
                        }

                        var listDetailIds = loanDetail.Select(x => x.LoanDetailID).Distinct().ToList();
                        if (listDetailIds.Any())
                        {
                            listDetailIds
                                .All(w =>
                                {
                                    var dRepLoanDetail = dbContextSal.LoanDetails.Where(x => x.LoanDetailID == w).AsNoTracking().FirstOrDefault();
                                    if (dRepLoanDetail != null)
                                    {
                                        dRepLoanDetail.InstallmentDate = toDto.SalaryCalculationDate;
                                        dRepLoanDetail.LoanEntryStatusID = scheduleInstallmentstatus;
                                        dbContextSal.Entry(dRepLoanDetail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                        dbContextSal.SaveChanges();
                                    }
                                    return true;
                                });
                        }

                        #endregion Loan/Advance
                    }
                }


                operationResultWithIDsDTO.SalarySlipIDList = new List<SalarySlipDTO>();

                operationResultWithIDsDTO.SalarySlipIDList.Add(new SalarySlipDTO()
                {
                    SalarySlipIID = slipID,
                    BranchID = toDto.BranchID,
                    EmployeeCode = toDto.EmployeeCode,
                    SlipDate = toDto.SalaryCalculationDate
                });

                operationResultWithIDsDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Success;
                operationResultWithIDsDTO.Message = "Successfully Saved!";

            }
            //TODO: Need to handle the exception properly
            //catch (DbEntityValidationException ex)
            //{

            //    foreach (var eve in ex.EntityValidationErrors)
            //    {
            //        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
            //                ve.PropertyName, ve.ErrorMessage);
            //        }
            //    }
            //    throw;

            //}
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                        ? ex.InnerException?.Message : ex.Message;
                operationResultWithIDsDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                operationResultWithIDsDTO.Message = errorMessage;
                Eduegate.Logger.LogHelper<string>.Fatal($". Error message: {errorMessage}", ex);
            }

            return operationResultWithIDsDTO;

        }

        public OperationResultWithIDsDTO GetEmployeeDetailsToSettlement(EmployeeSettlementDTO employeeSettlementDTO)
        {
            var Empdet = new EmployeeSettlementDTO();
            Empdet = employeeSettlementDTO;
            var operationresultDTO = new OperationResultWithIDsDTO();
            var employeeSalarySettleList = new List<EmployeeSalarySettlement>();
            var settingBL = new Domain.Setting.SettingBL(_callContext);
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
            try
            {
                using (var dbContext = new dbEduegateHRContext())
                {
                    var empData = dbContext.Employees.Where(a => a.EmployeeIID == employeeSettlementDTO.EmployeeID).AsNoTracking().FirstOrDefault();
                    if (empData != null)
                    {
                        Empdet.EmployeeTypeID = empData.CalendarTypeID;
                        var academicTypeID = settingBL.GetSettingValue<int?>("ACADEMIC_CALENDAR_TYPE_ID");

                        var annualLeaveTypeID = settingBL.GetSettingValue<int?>("ANNUAL_LEAVE_TYPE");


                        var vacationLeaveTypeID = settingBL.GetSettingValue<int?>("VACATION_LEAVE_TYPE");


                        var leaveSalary = new Domain.Setting.SettingBL(null).GetSettingValue<string>("LEAVE_SALARY");
                        var endOfSalaryBenefit = new Domain.Setting.SettingBL(null).GetSettingValue<string>("END_OF_SERVICE_BENEFIT");


                        var previousMonth = employeeSettlementDTO.SalaryCalculationDate.Value.AddMonths(-1).Month;
                        var previousYear = employeeSettlementDTO.SalaryCalculationDate.Value.AddMonths(-1).Year;
                        var prevMonthData = (from asms in dbContext.SchoolDateSettingMaps
                                             where asms.PayrollCutoffDate.Value.Year == previousYear
                                             && asms.PayrollCutoffDate.Value.Month == previousMonth
                                             select asms).AsNoTracking().FirstOrDefault();
                        if (employeeSettlementDTO.EmployeeSettlementTypeID == 1)
                        {

                            employeeSalarySettleList = (from ss in dbContext.EmployeeSalarySettlements
                                                        where ss.EmployeeID.Value == employeeSettlementDTO.EmployeeID && ss.EmployeeSettlementTypeID == 1 && (ss.SalaryCalculationDate.Value.Year == employeeSettlementDTO.SalaryCalculationDate.Value.Year
                                                        && ss.SalaryCalculationDate.Value.Month == employeeSettlementDTO.SalaryCalculationDate.Value.Month)
                                                        select ss).AsNoTracking().ToList();
                        }
                        else
                        {
                            employeeSalarySettleList = (from ss in dbContext.EmployeeSalarySettlements
                                                        where ss.EmployeeID.Value == employeeSettlementDTO.EmployeeID && ss.EmployeeSettlementTypeID == 2
                                                        select ss).AsNoTracking().ToList();
                        }
                        if (employeeSalarySettleList.Count > 0)
                        {
                            var settlementDate = employeeSalarySettleList.Select(x => x.EmployeeSettlementDate).FirstOrDefault();
                            if (employeeSettlementDTO.EmployeeSettlementTypeID == 1)//todo fill from settings
                            {
                                Empdet.Remarks = settlementDate.HasValue ? "The leave/vacation salary settlement was already done on " + settlementDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) + ". You can modify the details" : null;
                            }
                            else
                            {
                                Empdet.Remarks = settlementDate.HasValue ? "The final settlement was already done on " + settlementDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) + ". You can modify the details" : null;
                            }
                        }
                        var leaveStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("EMPLOYEE_LEAVE_STATUS_APPROVED");
                        if (leaveStatus.IsNull())
                        {
                            operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                            operationresultDTO.Message = "There is no EMPLOYEE_LEAVE_STATUS_APPROVED in settings!";
                            return operationresultDTO;
                        }
                        var leaveStatusID = byte.Parse(leaveStatus);
                        var leaveStartingDateRow = dbContext.LeaveApplications
                            .Where(a => a.EmployeeID == employeeSettlementDTO.EmployeeID &&
                                        (a.LeaveTypeID == annualLeaveTypeID || a.LeaveTypeID == vacationLeaveTypeID) &&
                                        a.LeaveStatusID == leaveStatusID //& (a.ToDate != null && a.ToDate.Value.Year == employeeSettlementDTO.SalaryCalculationDate.Value.Year) 
                                        && a.FromDate == dbContext.LeaveApplications
                                         .Where(y => y.EmployeeID == employeeSettlementDTO.EmployeeID && y.LeaveStatusID == leaveStatusID &&
                                          (y.LeaveTypeID == annualLeaveTypeID || y.LeaveTypeID == vacationLeaveTypeID))
                                         .Max(y => (DateTime?)y.FromDate))
                                         .AsNoTracking().FirstOrDefault();

                        var leaveStartDate = DateTime.Now;
                        var leaveEndDate = DateTime.Now;

                        if (leaveStartingDateRow.IsNull())
                        {
                            if (Empdet.EmployeeSettlementTypeID == 1)
                            {
                                operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                                operationresultDTO.Message = "There is no leave request for Annual leave/Vacation Leave!";
                                return operationresultDTO;
                            }
                        }
                        else
                        {
                            leaveStartDate = leaveStartingDateRow.FromDate.Value;
                            leaveEndDate = leaveStartingDateRow.ToDate.Value;
                            if (Empdet.EmployeeTypeID == academicTypeID && leaveStartingDateRow.LeaveTypeID == vacationLeaveTypeID)//Vacation Salary
                            {
                                int[] vacationMonths = GetVacationMonths(leaveStartDate, leaveEndDate);
                                if (leaveStartDate.Year == employeeSettlementDTO.SalaryCalculationDate.Value.Year &&
                                    (employeeSettlementDTO.SalaryCalculationDate.Value.Month == vacationMonths[0] ||
                                     employeeSettlementDTO.SalaryCalculationDate.Value.Month == vacationMonths[1]))
                                {
                                    if (employeeSettlementDTO.SalaryCalculationDate.Value.Month == vacationMonths[1])
                                        employeeSettlementDTO.IsSecondMonthVacation = true;
                                    employeeSettlementDTO.VacationDaysInCurrentMonth = CalculateLeaveDays(leaveStartingDateRow.FromDate.Value, leaveStartingDateRow.ToDate.Value,
                                     new DateTime(employeeSettlementDTO.SalaryCalculationDate.Value.Year, employeeSettlementDTO.SalaryCalculationDate.Value.Month, 1),
                                   new DateTime(employeeSettlementDTO.SalaryCalculationDate.Value.Year, employeeSettlementDTO.SalaryCalculationDate.Value.Month,
                                   DateTime.DaysInMonth(employeeSettlementDTO.SalaryCalculationDate.Value.Year, employeeSettlementDTO.SalaryCalculationDate.Value.Month)));

                                }
                                employeeSettlementDTO.IsVacationSalary = true;

                                employeeSettlementDTO.VacationDaysInPrevMonth = CalculateLeaveDays(leaveStartingDateRow.FromDate.Value, leaveStartingDateRow.ToDate.Value,
                                    new DateTime(prevMonthData.PayrollCutoffDate.Value.Year, prevMonthData.PayrollCutoffDate.Value.Month, 1),
                                    new DateTime(prevMonthData.PayrollCutoffDate.Value.Year, prevMonthData.PayrollCutoffDate.Value.Month,
                                    DateTime.DaysInMonth(prevMonthData.PayrollCutoffDate.Value.Year, prevMonthData.PayrollCutoffDate.Value.Month)));

                            }
                            else
                                employeeSettlementDTO.IsVacationSalary = false;
                        }
                        if (empData.DateOfJoining.IsNull() && !empData.DateOfJoining.HasValue)
                        {
                            operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                            operationresultDTO.Message = "The employee's Date of Joining has not been set!";
                            return operationresultDTO;
                        }
                        var emplLeaveSalaryStrComp = (from esc in dbContext.EmployeeSalaryStructureLeaveSalaryMaps
                                                      join ess in dbContext.EmployeeSalaryStructures on esc.EmployeeSalaryStructureID equals ess.EmployeeSalaryStructureIID
                                                      where ess.EmployeeID.Value == employeeSettlementDTO.EmployeeID && ess.IsActive == true
                                                      select esc)
                                        .Include(i => i.EmployeeSalaryStructure)
                                        .Include(i => i.SalaryComponent)
                                        .AsNoTracking().ToList();
                        if (employeeSettlementDTO.EmployeeSettlementTypeID == 1 && emplLeaveSalaryStrComp.Count() == 0)// ToDo: use from settings
                        {
                            operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                            operationresultDTO.Message = "Leave Salary structure has not defined yet!";
                            return operationresultDTO;
                        }
                        var leaveDueFromDate = empData.DateOfJoining;
                        if (leaveStartingDateRow != null)
                        {
                            var leaveDueFromData = dbContext.LeaveApplications.Where(a => a.EmployeeID == employeeSettlementDTO.EmployeeID && (a.LeaveTypeID == annualLeaveTypeID || a.LeaveTypeID == vacationLeaveTypeID) && a.LeaveStatusID == leaveStatusID
                           && a.LeaveApplicationIID != leaveStartingDateRow.LeaveApplicationIID)
                            .AsNoTracking()
                            .Max(y => (DateTime?)y.RejoiningDate);

                            if (leaveDueFromData.HasValue)
                            {
                                leaveDueFromDate = leaveDueFromData;
                            }

                        }


                        var allocatedLeaveCount = dbContext.EmployeeLeaveAllocations.Where(a => a.EmployeeID == employeeSettlementDTO.EmployeeID
                                                 && (a.LeaveTypeID == annualLeaveTypeID || a.LeaveTypeID == vacationLeaveTypeID)).AsNoTracking()
                                                 .Select(x => x.AllocatedLeaves).FirstOrDefault();
                        if (allocatedLeaveCount == null)
                        {
                            operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                            operationresultDTO.Message = "Annual Leave has not set yet for this employee!";
                            return operationresultDTO;
                        }

                        var lopDetails = dbContext.LeaveApplications
                            .Where(X => X.EmployeeID == employeeSettlementDTO.EmployeeID && X.IsLeaveWithoutPay == true
                            && X.FromDate.Value.Date > leaveDueFromDate.Value.Date
                            && X.ToDate.Value.Date <= employeeSettlementDTO.SalaryCalculationDate.Value.Date
                            && X.LeaveStatusID == leaveStatusID
                            && X.LeaveTypeID != vacationLeaveTypeID)
                            .AsNoTracking()
                            .ToList();

                        var nooflops = 0;
                        if (lopDetails.Any())
                        {
                            lopDetails.All(x =>
                            {
                                for (var dt = x.FromDate.Value.Date; dt <= x.ToDate.Value.Date; dt = dt.AddDays(1.0))
                                {
                                    nooflops = nooflops + 1;
                                }
                                return true;
                            }
                            );
                        }


                        var maxID = dbContext.EmployeeSalaryStructures.Where(a => a.EmployeeID == employeeSettlementDTO.EmployeeID && a.IsActive == true).AsNoTracking()
                                    .Max(y => (long?)y.EmployeeSalaryStructureIID);

                        Empdet.SalaryComponents = new List<EmployeeSalaryStructureComponentMapDTO>();

                        Empdet.LeaveSalaryComponents = new List<EmployeeSalaryStructureComponentMapDTO>();
                        var entitySalaryComponentMaps = dbContext.EmployeeSalaryStructureComponentMaps
                            .Where(X => X.EmployeeSalaryStructureID == maxID)
                            .Include(i => i.SalaryComponent)
                            .AsNoTracking()
                            .ToList();

                        decimal noofSalaryDays = 0;

                        Empdet.EmployeeCode = empData.EmployeeCode;
                        Empdet.BranchID = empData.BranchID;
                        Empdet.DateOfJoining = empData.DateOfJoining;
                        Empdet.NoofLOPDays = nooflops;
                        Empdet.LeaveSalaryComponentID = int.Parse(leaveSalary);
                        Empdet.GratuityComponentID = int.Parse(endOfSalaryBenefit);
                        Empdet.LeaveDueFrom = leaveDueFromDate;
                        Empdet.AnnualLeaveEntitilements = (decimal?)allocatedLeaveCount;
                        Empdet.LeaveStartDate = !leaveStartingDateRow.IsNull() ? leaveStartDate : null;
                        Empdet.LeaveEndDate = !leaveStartingDateRow.IsNull() ? leaveEndDate : null;
                        decimal noofLeaveSalaryDays = 0;
                        if (Empdet.EmployeeSettlementTypeID == 1)//Leave Salary// TODO change type with settings table field
                        {
                            noofSalaryDays = (decimal)(leaveStartDate.Date - employeeSettlementDTO.SalaryCalculationDate.Value.Date).TotalDays;
                            if (leaveDueFromDate.HasValue)
                            {
                                noofLeaveSalaryDays = ((decimal)(((leaveStartDate - leaveDueFromDate.Value).TotalDays - nooflops) / 365) * (Empdet.AnnualLeaveEntitilements.Value));
                            }
                            else
                            {
                                noofLeaveSalaryDays = ((decimal)(((leaveStartDate - empData.DateOfJoining.Value).TotalDays - nooflops) / 365) * (Empdet.AnnualLeaveEntitilements.Value));
                            }
                        }
                        else// Final Settlement 
                        {
                            noofSalaryDays = (decimal)(employeeSettlementDTO.DateOfLeaving.Value - employeeSettlementDTO.SalaryCalculationDate.Value.Date).TotalDays;
                            if (leaveDueFromDate.HasValue)
                            {
                                noofLeaveSalaryDays = ((decimal)(((employeeSettlementDTO.DateOfLeaving.Value - leaveDueFromDate.Value).TotalDays - nooflops) / 365) * (Empdet.AnnualLeaveEntitilements.Value));
                            }
                            else
                            {
                                noofLeaveSalaryDays = ((decimal)(((employeeSettlementDTO.DateOfLeaving.Value - empData.DateOfJoining.Value).TotalDays - nooflops) / 365) * (Empdet.AnnualLeaveEntitilements.Value));
                            }
                        }
                        Empdet.NoofSalaryDays = Math.Abs(ConvertDecimalPoints(decimal.Round(noofSalaryDays, MidpointRounding.AwayFromZero), 2));
                        Empdet.NoofLeaveSalaryDays = Math.Abs(ConvertDecimalPoints(decimal.Round(noofLeaveSalaryDays, MidpointRounding.AwayFromZero), 2));
                        Empdet.EarnedLeave = Empdet.NoofLeaveSalaryDays;
                        Empdet.TotalLeaveDaysAvailable = Empdet.NoofLeaveSalaryDays;
                        //Empdet.NoofDaysInTheMonth = DateTime.DaysInMonth(leaveStartDate.Year, leaveStartDate.Month);
                        operationresultDTO = GenerateSettlementSalary(Empdet);
                    }
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                        ? ex.InnerException?.Message : ex.Message;
                operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                operationresultDTO.Message = errorMessage;
                Eduegate.Logger.LogHelper<string>.Fatal($". Error message: {errorMessage}", ex);

            }
            return operationresultDTO;
        }
        private int CalculateLeaveDays(DateTime leaveFrom, DateTime leaveTo, DateTime monthStart, DateTime monthEnd)
        {
            DateTime startDate = leaveFrom > monthStart ? leaveFrom : monthStart;
            DateTime endDate = leaveTo < monthEnd ? leaveTo : monthEnd;

            if (endDate < startDate)
            {
                return 0;
            }

            return (endDate - startDate).Days;// + 1;
        }
        private int CalculateNoOfLeaveDays(dbEduegateHRContext dbContext, long? employeeID, DateTime leavedDateFrom, DateTime leaveTo, byte? leaveStatusID,
            int? vacationLeaveTypeID, int? annualLeaveTypeID, DateTime? dateOfLeaving)
        {
            var leaveFrom = leavedDateFrom.AddDays(1);
            var empLeaves = dbContext.LeaveApplications
            .Where(ss => ss.EmployeeID == employeeID &&
            ss.IsLeaveWithoutPay == true &&
                         ss.LeaveStatusID == leaveStatusID
                         && ss.LeaveTypeID != vacationLeaveTypeID
                         && ss.LeaveTypeID != annualLeaveTypeID
                         && (dateOfLeaving == null || ss.FromDate < dateOfLeaving)
                         && ss.FromDate <= leaveTo
                         && ss.ToDate >= leaveFrom)
            .AsNoTracking()
            .ToList();

            int totalLeaveDays = 0;
            foreach (var leave in empLeaves)
            {
                DateTime overlapStartDate = leave.FromDate < leaveFrom ? leaveFrom : leave.FromDate.Value;
                DateTime overlapEndDate = leave.ToDate > leaveTo ? leaveTo : leave.ToDate.Value;

                int leaveDays = (int)(overlapEndDate.Date - overlapStartDate.Date).TotalDays + 1;

                totalLeaveDays += leaveDays;
            }

            return totalLeaveDays;
        }

        static int[] GetVacationMonths(DateTime leaveDateFrom, DateTime leaveDateTo)
        {

            int[] leaveDaysInMonths = new int[12];


            for (DateTime date = leaveDateFrom; date <= leaveDateTo; date = date.AddDays(1))
            {
                leaveDaysInMonths[date.Month - 1]++;
            }


            int firstLargest = 0, secondLargest = 0;
            int firstLargestIndex = -1, secondLargestIndex = -1;

            for (int i = 0; i < leaveDaysInMonths.Length; i++)
            {
                if (leaveDaysInMonths[i] > firstLargest)
                {
                    secondLargest = firstLargest;
                    secondLargestIndex = firstLargestIndex;
                    firstLargest = leaveDaysInMonths[i];
                    firstLargestIndex = i;
                }
                else if (leaveDaysInMonths[i] > secondLargest)
                {
                    secondLargest = leaveDaysInMonths[i];
                    secondLargestIndex = i;
                }
            }


            int firstMonthID = firstLargestIndex + 1;
            int secondMonthID = secondLargestIndex + 1;

            return new int[] { firstMonthID, secondMonthID };
        }

        public EmployeeSettlementDTO GetSettlementDateDetails(EmployeeSettlementDTO settlementDTO)
        {
            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                var operationresultDTO = new OperationResultWithIDsDTO();
                try
                {
                    var settingBL = new Domain.Setting.SettingBL(_callContext);
                    settlementDTO.NoofDaysInTheMonthLS = settingBL.GetSettingValue<byte?>("LEAVE_SALARY_NOOF_MONTH_DAYS");
                    var currentMonthData = (from asm in dbContext.SchoolDateSettingMaps
                                            where asm.PayrollCutoffDate.Value.Year == settlementDTO.SalaryCalculationDate.Value.Year
                                        && asm.PayrollCutoffDate.Value.Month == settlementDTO.SalaryCalculationDate.Value.Month
                                            select asm).AsNoTracking().FirstOrDefault();

                    if (currentMonthData.IsNotNull() && currentMonthData.TotalWorkingDays.HasValue)
                    {
                        settlementDTO.NoofDaysInTheMonth = currentMonthData.TotalWorkingDays.Value;
                        //settlementDTO.NoofDaysInTheMonthLS = currentMonthData.TotalWorkingDays.Value;
                        if (settlementDTO.EmployeeSettlementTypeID == 2)//TODo get EmployeeSettlementTypeID from settings
                        {
                            //settlementDTO.NoofDaysInTheMonthLS = currentMonthData.TotalWorkingDays.Value;
                            settlementDTO.NoofDaysInTheMonthEoSB = currentMonthData.TotalWorkingDays.Value;
                            settlementDTO.EndofServiceDaysPerYear = settingBL.GetSettingValue<byte?>("ESB_NOOF_DAYS_PER_YEAR");
                        }
                    }

                }
                catch (Exception ex)
                {

                }
                return settlementDTO;
            }
        }

        public OperationResultWithIDsDTO GenerateSettlementSalary(EmployeeSettlementDTO settlementDTO)
        {
            MutualRepository mutualRepository = new MutualRepository();
            var operationresultDTO = new OperationResultWithIDsDTO();
            var SalaryEmpIdList = new List<long>();
            var employeeSettlementDTO = new EmployeeSettlementDTO();
            var settingBL = new Domain.Setting.SettingBL(_callContext);
            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                try
                {
                    var previousMonth = settlementDTO.SalaryCalculationDate.Value.AddMonths(-1).Month;
                    var previousYear = settlementDTO.SalaryCalculationDate.Value.AddMonths(-1).Year;
                    var previousDate = new DateTime(previousYear, previousMonth, DateTime.DaysInMonth(previousYear, previousMonth));
                    var PrevWorkDays = DateTime.DaysInMonth(previousYear, previousMonth);
                    var workingDays = DateTime.DaysInMonth(settlementDTO.SalaryCalculationDate.Value.Year, settlementDTO.SalaryCalculationDate.Value.Month);
                    DateTime? prevPayrollCuttofDate = null;
                    DateTime? payrollCuttofDate = null;
                    string salarySlipApprovedID = null;

                    #region Retrieve data from settings 

                    //if (isRegenerate == true)
                    //{
                    salarySlipApprovedID = (from st in dbContext.Settings where st.SettingCode == "SALARYSLIPAPPROVED_ID" select st.SettingValue).AsNoTracking().FirstOrDefault();
                    if (salarySlipApprovedID.IsNull())
                    {
                        operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                        operationresultDTO.Message = "There is no SALARYSLIPAPPROVED_ID in settings!";
                        return operationresultDTO;
                    }
                    //}

                    var arrearComponentId = (from st in dbContext.Settings where st.SettingCode == "ARREAR_COMPONENT_ID" select st.SettingValue).AsNoTracking().FirstOrDefault();
                    if (arrearComponentId.IsNull())
                    {
                        operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                        operationresultDTO.Message = "There is no ARREAR_COMPONENT_ID in settings!";
                        return operationresultDTO;
                    }

                    var OTComponentId = (from st in dbContext.Settings where st.SettingCode == "OT_COMPONENT_ID" select st.SettingValue).AsNoTracking().FirstOrDefault();
                    if (OTComponentId.IsNull())
                    {
                        operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                        operationresultDTO.Message = "There is no OT_COMPONENT_ID in settings!";
                        return operationresultDTO;
                    }
                    var NormalOTComponentID = int.Parse(OTComponentId);

                    var specialOTComponent = (from st in dbContext.Settings where st.SettingCode == "SPECIAL_OT_COMPONENT_ID" select st.SettingValue).AsNoTracking().FirstOrDefault();
                    if (specialOTComponent.IsNull())
                    {
                        operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                        operationresultDTO.Message = "There is no SPECIAL_OT_COMPONENT_ID in settings!";
                        return operationresultDTO;
                    }
                    var specialOTComponentID = int.Parse(specialOTComponent);
                    var defaultDecimalPlaces = (from st in dbContext.Settings where st.SettingCode == "DEFAULT_DECIMALPOINTS" select st.SettingValue).AsNoTracking().FirstOrDefault();
                    if (defaultDecimalPlaces.IsNull())
                    {
                        operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                        operationresultDTO.Message = "There is no defaultDecimalPlaces in settings!";
                        return operationresultDTO;
                    }
                    var defaultDecimalNos = int.Parse(defaultDecimalPlaces);

                    var basicPayComponent = (from st in dbContext.Settings where st.SettingCode == "BASICPAY_ID" select st.SettingValue).AsNoTracking().FirstOrDefault();
                    if (basicPayComponent.IsNull())
                    {
                        operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                        operationresultDTO.Message = "There is no BASICPAY_ID in settings!";
                        return operationresultDTO;
                    }
                    var basicPayComponentID = int.Parse(basicPayComponent);
                    var normalRate = (from st in dbContext.Settings where st.SettingCode == "OTNORMALRATE" select st.SettingValue).AsNoTracking().FirstOrDefault();
                    if (normalRate.IsNull())
                    {
                        operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                        operationresultDTO.Message = "There is no OTNORMALRATE in settings!";
                        return operationresultDTO;
                    }
                    var normalRateAmount = decimal.Parse(normalRate);

                    var specialRate = (from st in dbContext.Settings where st.SettingCode == "OTSPECIALRATE" select st.SettingValue).AsNoTracking().FirstOrDefault();
                    if (specialRate.IsNull())
                    {
                        operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                        operationresultDTO.Message = "There is no OTSPECIALRATE in settings!";
                        return operationresultDTO;
                    }
                    var specialRateAmount = decimal.Parse(specialRate);

                    var timesheetStatus = (from st in dbContext.Settings where st.SettingCode == "TIMESHEET_APPROVAL_APPROVED_STATUS_ID" select st.SettingValue).AsNoTracking().FirstOrDefault();
                    if (timesheetStatus.IsNull())
                    {
                        operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                        operationresultDTO.Message = "There is no TIMESHEET_APPROVAL_APPROVED_STATUS_ID in settings!";
                        return operationresultDTO;
                    }
                    var timesheetStatusID = byte.Parse(timesheetStatus);

                    var pendingStatus = (from st in dbContext.Settings where st.SettingCode == "SALARYSLIPSTATUS_PENDING" select st.SettingValue).AsNoTracking().FirstOrDefault();
                    if (pendingStatus.IsNull())
                    {
                        operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                        operationresultDTO.Message = "There is no SALARYSLIPSTATUS_PENDING in settings!";
                        return operationresultDTO;
                    }

                    var pendingStatusID = byte.Parse(pendingStatus);

                    var hrsPerDayValue = (from st in dbContext.Settings where st.SettingCode == "HOURSPERDAY" select st.SettingValue).AsNoTracking().FirstOrDefault();
                    if (hrsPerDayValue.IsNull())
                    {
                        operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                        operationresultDTO.Message = "There is no HOURSPERDAY in settings!";
                        return operationresultDTO;
                    }

                    var hrsPerDay = int.Parse(hrsPerDayValue);


                    var normalOTTimeType = (from st in dbContext.Settings where st.SettingCode == "Normal_OT_TIME_TYPE_ID" select st.SettingValue).AsNoTracking().FirstOrDefault();
                    if (normalOTTimeType.IsNull())
                    {
                        operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                        operationresultDTO.Message = "There is no Normal_OT_TIME_TYPE_ID in settings!";
                        return operationresultDTO;
                    }

                    var normalOTTimeTypeID = int.Parse(normalOTTimeType);

                    var specialOTTimeType = (from st in dbContext.Settings where st.SettingCode == "SPECIAL_OT_TIME_TYPE_ID" select st.SettingValue).AsNoTracking().FirstOrDefault();
                    if (specialOTTimeType.IsNull())
                    {
                        operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                        operationresultDTO.Message = "There is no SPECIAL_OT_TIME_TYPE_ID in settings!";
                        return operationresultDTO;
                    }

                    var specialOTTimeTypeID = int.Parse(specialOTTimeType);

                    var withCUTOFDate = (from st in dbContext.Settings where st.SettingCode == "TIMESHEETHOURS_WITH_CUTOFFDATE" select st.SettingValue).AsNoTracking().FirstOrDefault();
                    if (withCUTOFDate.IsNull())
                    {
                        operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                        operationresultDTO.Message = "There is no TIMESHEETHOURS_WITH_CUTOFFDATE in settings!";
                        return operationresultDTO;
                    }
                    var withCUTOFDateValue = byte.Parse(withCUTOFDate);

                    var leaveStatus = (from st in dbContext.Settings where st.SettingCode == "EMPLOYEE_LEAVE_STATUS_APPROVED" select st.SettingValue).AsNoTracking().FirstOrDefault();
                    if (leaveStatus.IsNull())
                    {
                        operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                        operationresultDTO.Message = "There is no EMPLOYEE_LEAVE_STATUS_APPROVED in settings!";
                        return operationresultDTO;
                    }

                    var leaveStatusID = byte.Parse(leaveStatus);

                    if (settlementDTO.EmployeeSettlementTypeID == 2)//TODo get EmployeeSettlementTypeID from settings
                    {
                        if (!settlementDTO.NoofDaysInTheMonthEoSB.HasValue)
                        {
                            var noofDaysInTheMonthEoSB = (from st in dbContext.Settings where st.SettingCode == "ESB_NOOF_MONTH_DAYS" select st.SettingValue).AsNoTracking().FirstOrDefault();
                            if (noofDaysInTheMonthEoSB.IsNull())
                            {
                                operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                                operationresultDTO.Message = "There is no ESB_NOOF_MONTH_DAYS in settings!";
                                return operationresultDTO;
                            }
                            else
                                settlementDTO.NoofDaysInTheMonthEoSB = byte.Parse(noofDaysInTheMonthEoSB);
                        }
                        if (!settlementDTO.EndofServiceDaysPerYear.HasValue)
                        {
                            var endofServiceDays = (from st in dbContext.Settings where st.SettingCode == "ESB_NOOF_DAYS_PER_YEAR" select st.SettingValue).AsNoTracking().FirstOrDefault();
                            if (endofServiceDays.IsNull())
                            {
                                operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                                operationresultDTO.Message = "There is no ESB_NOOF_MONTH_DAYS in settings!";
                                return operationresultDTO;
                            }
                            else
                                settlementDTO.EndofServiceDaysPerYear = byte.Parse(endofServiceDays);
                        }

                    }

                    var loanComponentID = (from st in dbContext.Settings where st.SettingCode == "LOAN_COMPONENT" select st.SettingValue).AsNoTracking().FirstOrDefault();
                    if (loanComponentID.IsNull())
                    {
                        operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                        operationresultDTO.Message = "There is no LOAN_COMPONENT in settings!";
                        return operationresultDTO;
                    }
                    else
                        settlementDTO.LoanComponentID = byte.Parse(loanComponentID);

                    var advanceComponentID = (from st in dbContext.Settings where st.SettingCode == "ADVANCE_COMPONENT" select st.SettingValue).AsNoTracking().FirstOrDefault();
                    if (advanceComponentID.IsNull())
                    {
                        operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                        operationresultDTO.Message = "There is no ADVANCE_COMPONENT in settings!";
                        return operationresultDTO;
                    }
                    else
                        settlementDTO.AdvanceComponentID = byte.Parse(advanceComponentID);

                    var annualLeaveTypeID = settingBL.GetSettingValue<int?>("ANNUAL_LEAVE_TYPE");
                    var vacationLeaveTypeID = settingBL.GetSettingValue<int?>("VACATION_LEAVE_TYPE");
                    #endregion


                    var employees = new List<Entity.HR.Models.Employee>();


                    employees = (from e in dbContext.Employees
                                 join ess in dbContext.EmployeeSalaryStructures on e.EmployeeIID equals ess.EmployeeID
                                 where ess.IsActive == true && e.EmployeeIID == settlementDTO.EmployeeID
                                 select e).AsNoTracking().ToList();

                    if (employees.Count > 0)
                    {
                        #region DayDataVariables

                        var totalhrsInaMonth = (from ss in dbContext.Employees
                                                join c in dbContext.CalendarEntries on ss.AcademicCalendarID equals c.AcademicCalendarID
                                                where c.CalendarDate.Value.Year == settlementDTO.SalaryCalculationDate.Value.Year
                                                && c.CalendarDate.Value.Month == settlementDTO.SalaryCalculationDate.Value.Month
                                                select (c.NoofHours)).ToList().Sum();

                        var prevMonthData = (from asms in dbContext.SchoolDateSettingMaps
                                             where asms.PayrollCutoffDate.Value.Year == previousYear
                                             && asms.PayrollCutoffDate.Value.Month == previousMonth
                                             select asms).AsNoTracking().FirstOrDefault();


                        var currentMonthData = (from asm in dbContext.SchoolDateSettingMaps
                                                where asm.PayrollCutoffDate.Value.Year == settlementDTO.SalaryCalculationDate.Value.Year
                                            && asm.PayrollCutoffDate.Value.Month == settlementDTO.SalaryCalculationDate.Value.Month
                                                select asm).AsNoTracking().FirstOrDefault();

                        if (prevMonthData.IsNotNull())
                        {
                            prevPayrollCuttofDate = prevMonthData.PayrollCutoffDate ?? new DateTime(previousYear, previousMonth, 26);
                            PrevWorkDays = prevMonthData.TotalWorkingDays.HasValue ? prevMonthData.TotalWorkingDays.Value : PrevWorkDays;
                            settlementDTO.NoofDaysInPrevMonth = PrevWorkDays;
                        }
                        else
                        {
                            operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                            operationresultDTO.Message = "Please set Payroll date settings for the month " + new DateTime(previousYear, previousMonth, 1).ToString("MMM"); ;
                            return operationresultDTO;
                        }
                        if (currentMonthData.IsNotNull())
                        {
                            payrollCuttofDate = currentMonthData.PayrollCutoffDate ?? new DateTime(settlementDTO.SalaryCalculationDate.Value.Year, settlementDTO.SalaryCalculationDate.Value.Month, 26);
                            workingDays = currentMonthData.TotalWorkingDays.HasValue ? currentMonthData.TotalWorkingDays.Value : workingDays;
                            if (!settlementDTO.NoofDaysInTheMonth.HasValue)
                                settlementDTO.NoofDaysInTheMonth = workingDays;
                        }
                        else
                        {
                            operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                            operationresultDTO.Message = "Please set Payroll date settings for the month " + settlementDTO.SalaryCalculationDate.Value.ToString("MMM");
                            return operationresultDTO;
                        }
                        #endregion

                        #region Calculate -no. of functional Days
                        var functionalPeriods = (from ss in dbContext.FunctionalPeriods
                                                 where ((ss.FromDate.Year >= settlementDTO.SalaryCalculationDate.Value.Year && ss.FromDate.Month <= settlementDTO.SalaryCalculationDate.Value.Month)
                                                 || (ss.ToDate.Year <= settlementDTO.SalaryCalculationDate.Value.Year && ss.ToDate.Month <= settlementDTO.SalaryCalculationDate.Value.Month)
                                                 ||
                                                 ((ss.FromDate.Year >= previousYear && ss.FromDate.Month <= previousMonth)
                                                 || (ss.ToDate.Year <= previousYear && ss.ToDate.Month <= previousMonth)
                                                 ))
                                                 select ss).AsNoTracking().ToList();
                        int nooffunctionalDays = 0;
                        int noofPreviousfunctionalDays = 0;
                        if (functionalPeriods.Any())
                        {
                            functionalPeriods.All(x =>
                            {
                                for (var dt = x.FromDate.Date; dt <= x.ToDate.Date; dt = dt.AddDays(1.0))
                                {
                                    if (dt.Month == previousMonth && dt.Year == previousYear)
                                    {
                                        noofPreviousfunctionalDays += 1;
                                    }
                                    else if (dt.Month == settlementDTO.SalaryCalculationDate.Value.Month && dt.Year == settlementDTO.SalaryCalculationDate.Value.Year)
                                    {
                                        nooffunctionalDays += 1;
                                    }
                                }
                                return true;
                            }
                            );
                            //noofDayswithoutfunctional = workingDays - nooffunctionalDays;
                        }
                        #endregion
                        var salarySlipEntities = new List<SalarySlip>();

                        //if (settlementDTO.EmployeeSettlementTypeID == 1)
                        //{
                        //    salarySlipEntities = (from ss in dbContext.SalarySlips
                        //                          //join ess in dbContext.EmployeeSalarySettlements on ss.SalaryComponentID equals ess.SalaryComponentID
                        //                          where ss.EmployeeID.Value == settlementDTO.EmployeeID //&& ess.EmployeeSettlementTypeID == 1
                        //                          && (ss.SlipDate.Value.Year == settlementDTO.SalaryCalculationDate.Value.Year
                        //                          && ss.SlipDate.Value.Month == settlementDTO.SalaryCalculationDate.Value.Month)
                        //                          select ss).ToList();
                        //}
                        //else
                        //{
                        salarySlipEntities = (from ss in dbContext.SalarySlips
                                                  // join ess in dbContext.EmployeeSalarySettlements on ss.SalaryComponentID equals ess.SalaryComponentID
                                              where ss.EmployeeID.Value == settlementDTO.EmployeeID //&& ess.EmployeeSettlementTypeID == 2
                                              && (ss.SlipDate.Value.Year == settlementDTO.SalaryCalculationDate.Value.Year
                                              && ss.SlipDate.Value.Month == settlementDTO.SalaryCalculationDate.Value.Month)
                                              select ss).ToList();
                        //}

                        var salarySlipApproved = byte.Parse(salarySlipApprovedID);
                        if (salarySlipEntities.Any(x => x.SalarySlipStatusID == salarySlipApproved))
                        {
                            operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                            operationresultDTO.Message = "Cannot be generated.Already issued salary slip!";
                            return operationresultDTO;
                        }
                        var empLeaves = new List<Entity.HR.Leaves.LeaveApplication>();


                        //empLeaves = dbContext.LeaveApplications
                        //    .Where(ss => ss.EmployeeID == settlementDTO.EmployeeID &&
                        //     ss.IsLeaveWithoutPay == true &&
                        //     ss.LeaveStatusID == leaveStatusID &&
                        //     ss.FromDate.Value.Date < payrollCuttofDate.Value.Date && 
                        //     ss.ToDate.Value.Date >= prevPayrollCuttofDate.Value.Date)
                        //    .AsNoTracking()
                        //.ToList();

                        settlementDTO.LossofPay = CalculateNoOfLeaveDays(dbContext, settlementDTO.EmployeeID, prevPayrollCuttofDate.Value.Date, payrollCuttofDate.Value.Date, leaveStatusID, vacationLeaveTypeID, annualLeaveTypeID, settlementDTO.DateOfLeaving);

                        var emplSalaryStrComp = (from esc in dbContext.EmployeeSalaryStructureComponentMaps
                                                 join ess in dbContext.EmployeeSalaryStructures on esc.EmployeeSalaryStructureID equals ess.EmployeeSalaryStructureIID
                                                 where ess.EmployeeID.Value == settlementDTO.EmployeeID && ess.IsActive == true
                                                 select esc)
                                                 .Include(i => i.EmployeeSalaryStructure)
                                                 .Include(i => i.SalaryComponent)
                                                 .AsNoTracking().ToList();


                        List<EmployeeSalaryStructureComponentMap> lstemplSalaryStrEarningComp = emplSalaryStrComp.Where(y => y.SalaryComponent.ComponentTypeID != 2).ToList();
                        List<EmployeeSalaryStructureComponentMap> lstemplSalaryStrDedComp = emplSalaryStrComp.Where(y => y.SalaryComponent.ComponentTypeID == 2).ToList();
                        if ((lstemplSalaryStrEarningComp.Count() > 0 || lstemplSalaryStrDedComp.Count() > 0))
                        {
                            operationresultDTO.EmployeeSettlementDTO = new EmployeeSettlementDTO();
                            operationresultDTO.EmployeeSettlementDTO = GenerateEmployeeSettlementSalarySlip(settlementDTO, dbContext,
                                      lstemplSalaryStrEarningComp, lstemplSalaryStrDedComp, empLeaves, workingDays, prevPayrollCuttofDate.Value,
                                      payrollCuttofDate.Value, arrearComponentId,
                                      nooffunctionalDays, previousDate, PrevWorkDays, noofPreviousfunctionalDays,
                                      basicPayComponentID, NormalOTComponentID, specialOTComponentID, totalhrsInaMonth, normalRateAmount, specialRateAmount,
                                      pendingStatusID, timesheetStatusID, hrsPerDay, normalOTTimeTypeID, specialOTTimeTypeID,
                                      withCUTOFDateValue, defaultDecimalNos);
                            return operationresultDTO;
                        }
                    }
                }
                //TODO: Need to handle the exception
                //catch (DbEntityValidationException ex)
                //{

                //    foreach (var eve in ex.EntityValidationErrors)
                //    {
                //        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                //        foreach (var ve in eve.ValidationErrors)
                //        {
                //            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                //                ve.PropertyName, ve.ErrorMessage);
                //        }
                //    }
                //    throw;
                //}
                catch (Exception ex)
                {
                    var screen = "";
                    if (settlementDTO != null)
                    {
                        if (settlementDTO.EmployeeSettlementTypeID == 2)
                        {
                            screen = "Final Settlement";
                        }
                        else
                        {
                            if (settlementDTO.EmployeeTypeID == 1)
                                screen = "Leave Salary Settlement";
                            else
                                screen = "Vacation Salary Settlement";
                        }
                    }

                    var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                        ? ex.InnerException?.Message : ex.Message;
                    operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                    operationresultDTO.Message = errorMessage;
                    Eduegate.Logger.LogHelper<string>.Fatal($". Error message: {screen + " :" + errorMessage}", ex);
                    return operationresultDTO;
                }

            }
            return operationresultDTO;
        }
        private EmployeeSettlementDTO GenerateEmployeeSettlementSalarySlip(EmployeeSettlementDTO settlementDTO, dbEduegateHRContext dbContext,
       List<EmployeeSalaryStructureComponentMap> employeeSalaryStructComp, List<EmployeeSalaryStructureComponentMap> lstemplSalaryStrDedComp,
       List<Entity.HR.Leaves.LeaveApplication> leaveApplications, int totalWorkingDays, DateTime prevPayrollCuttofDate, DateTime payrollCutoffDate, string arrearComponentId,
       int nooffunctionalDays, DateTime previousDate, int PrevWorkDays, int noofPreviousfunctionalDays, int basicPayComponentID, int NormalOTComponentID,
       int specialOTComponentID, decimal? totalhrsInaMonth, decimal? normalRate, decimal? specialRate, byte pendingStatusID, byte timesheetStatusID,
       int hrsPerDay, int normalOTTimeTypeID, int specialOTTimeTypeID, byte withCUTOFDate, int defaultDecimalNos)
        {
            var employee = settlementDTO.EmployeeID;
            DateTime? dateOfJoining = settlementDTO.DateOfJoining;
            DateTime? SlipDate = settlementDTO.SalaryCalculationDate;
            var salarySlipLastDate = new DateTime(SlipDate.Value.Year, SlipDate.Value.Month, DateTime.DaysInMonth(SlipDate.Value.Year, SlipDate.Value.Month));
            decimal? salaryPerDay = 0;
            decimal? totalSalary = 0;
            decimal? lopAmount = 0;
            decimal? noOfLOPDays = null;
            string description = null;
            var noofleave = 0;
            var createSlip = new SalarySlipDTO();
            var totalWorkDays = totalWorkingDays;
            var listSalarySlip = new List<SalarySlipDTO>();
            var settingBL = new Domain.Setting.SettingBL(_callContext);
            MutualRepository mutualRepository = new MutualRepository();
            var operationresultDTO = new OperationResultWithIDsDTO();
            var worksheetTimeData = new List<EmployeeTimeSheetApproval>();
            var listHeadIds = new List<long>();
            var listDetailIds = new List<long>();
            var listLoanHeadIds = new List<long>();
            var listloanDetailIds = new List<long>();

            var loanType = settingBL.GetSettingValue<int>("LOAN_TYPE");
            var salaryAdvanceType = settingBL.GetSettingValue<int>("SALARY_ADVANCE_TYPE");
            var activeInstallmentstatus = settingBL.GetSettingValue<int>("LOAN_ACTIVE_INSTALLMENT_STATUS");
            var basicAmount = employeeSalaryStructComp.Where(x => x.SalaryComponentID == basicPayComponentID).Select(x => x.Amount).FirstOrDefault();
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            var HRASalaryComponent = settingBL.GetSettingValue<int>("HRAALLOWANCE_ID");
            var minNoofServiceDaysVacationSalary = settingBL.GetSettingValue<int?>("MIN_NO_DAYS_SERVICE_VACATION_SALARY");


            var cumulativeLOPDays = settlementDTO.NoofLOPDays;
            var eligibleLSDays = minNoofServiceDaysVacationSalary - cumulativeLOPDays;
            var academicTypeID = settingBL.GetSettingValue<int>("ACADEMIC_CALENDAR_TYPE_ID");

            decimal? basicLeaveSalaryAmount = 0, hRALeaveSalaryAmount = 0;
            var emplLeaveSalaryStrComp = (from esc in dbContext.EmployeeSalaryStructureLeaveSalaryMaps
                                          join ess in dbContext.EmployeeSalaryStructures on esc.EmployeeSalaryStructureID equals ess.EmployeeSalaryStructureIID
                                          where ess.EmployeeID.Value == settlementDTO.EmployeeID && ess.IsActive == true
                                          select esc)
                                       .Include(i => i.EmployeeSalaryStructure)
                                       .Include(i => i.SalaryComponent)
                                       .AsNoTracking().ToList();
            if (emplLeaveSalaryStrComp.Count() > 0)
            {
                basicLeaveSalaryAmount = emplLeaveSalaryStrComp.Where(x => x.SalaryComponentID == basicPayComponentID).Select(x => x.Amount).FirstOrDefault();
                hRALeaveSalaryAmount = emplLeaveSalaryStrComp.Where(x => x.SalaryComponentID == HRASalaryComponent).Select(x => x.Amount).FirstOrDefault();
            }
            //Vacation Salary for whole Month
            if (settlementDTO.EmployeeSettlementTypeID == 1 && settlementDTO.EmployeeTypeID == academicTypeID && settlementDTO.IsVacationSalary == true && settlementDTO.VacationDaysInCurrentMonth == settlementDTO.NoofDaysInTheMonthLS)
            {
                var prevVacationSalaryAmount = (basicLeaveSalaryAmount ?? 0) * ((settlementDTO.VacationDaysInPrevMonth ?? 0) / (settlementDTO.NoofDaysInPrevMonth ?? 0));
                var vacationSalryCurMonth = (basicLeaveSalaryAmount ?? 0) * ((settlementDTO.VacationDaysInCurrentMonth ?? 0) / (settlementDTO.NoofDaysInTheMonthLS ?? 0));
                var cumulativeLOPDeductions = (basicLeaveSalaryAmount ?? 0) * ((cumulativeLOPDays ?? 0) / (minNoofServiceDaysVacationSalary ?? 0));
                var noOfDays = (settlementDTO.VacationDaysInPrevMonth ?? 0) + (settlementDTO.VacationDaysInCurrentMonth ?? 0);
                var fixedAllowanceAmount = emplLeaveSalaryStrComp.Where(x => x.SalaryComponent.SalaryComponentGroupID == 1).Sum(x => (x.Amount ?? 0));
                var totalVacationSalary = (prevVacationSalaryAmount) + vacationSalryCurMonth + fixedAllowanceAmount;


                #region  Related DeductionComponents


                var dedComponent = (from asm in dbContext.SalaryComponentRelationMaps
                                    where asm.SalaryComponentID == settlementDTO.VacationSalaryComponent && asm.RelationTypeID == 1
                                    select asm).AsNoTracking().FirstOrDefault();

                #endregion
                createSlip = new SalarySlipDTO()
                {
                    SlipDate = SlipDate,
                    EmployeeID = employee,
                    BranchID = settlementDTO.BranchID,
                    SalaryComponentID = settlementDTO.VacationSalaryComponent.HasValue ? settlementDTO.VacationSalaryComponent : settlementDTO.LeaveSalaryComponentID,
                    Amount = ConvertDecimalPoints(decimal.Round(totalVacationSalary, MidpointRounding.AwayFromZero), defaultDecimalNos),
                    NoOfDays = ConvertDecimalPoints(decimal.Round(noOfDays, MidpointRounding.AwayFromZero), defaultDecimalNos),
                    SalarySlipStatusID = pendingStatusID,
                    IsVerified = false,
                    Description = @"Calculated with components Basic Pay: " + basicLeaveSalaryAmount +
                               " HRA : " + hRALeaveSalaryAmount +
                               " LeaveDueFrom : '" + settlementDTO.LeaveDueFrom.Value.ToString(dateFormat, CultureInfo.InvariantCulture) + "'" +
                               " LeaveStartDate : '" + settlementDTO.LeaveStartDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) + "'" +
                               " MinNoofDaysofServiceVacationSalary : " + ConvertDecimalPoints(decimal.Round((minNoofServiceDaysVacationSalary ?? 0),
                                 MidpointRounding.AwayFromZero), defaultDecimalNos) +
                               " CumulativeLOPDays : " + ConvertDecimalPoints(decimal.Round((cumulativeLOPDays ?? 0),
                                 MidpointRounding.AwayFromZero), defaultDecimalNos) + "" +
                               " CumulativeLOPAmount : " + ConvertDecimalPoints(decimal.Round(cumulativeLOPDeductions,
                                 MidpointRounding.AwayFromZero), defaultDecimalNos) + "" +
                               " FixedAllowanceAmount : " + ConvertDecimalPoints(decimal.Round((fixedAllowanceAmount), MidpointRounding.AwayFromZero), defaultDecimalNos) + ""

                };

                createSlip.CreatedBy = _context == null ? null : Convert.ToInt32(_context.LoginID);
                createSlip.CreatedDate = DateTime.Now;

                listSalarySlip.Add(createSlip);//Add salarySlip into list

                createSlip = new SalarySlipDTO()
                {
                    SlipDate = SlipDate,
                    EmployeeID = employee,
                    BranchID = settlementDTO.BranchID,
                    NoOfDays = cumulativeLOPDays,
                    IsVerified = false,
                    SalarySlipStatusID = pendingStatusID,
                    SalaryComponentID = dedComponent.RelatedComponentID,
                    Amount = ConvertDecimalPoints(decimal.Round(-cumulativeLOPDeductions, MidpointRounding.AwayFromZero), defaultDecimalNos),
                    Description = @"Calculated with components Basic Pay: " + basicLeaveSalaryAmount +
                               " LeaveDueFrom : '" + settlementDTO.LeaveDueFrom.Value.ToString(dateFormat, CultureInfo.InvariantCulture) + "'" +
                               " LeaveStartDate : '" + settlementDTO.LeaveStartDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) + "'" +
                               " MinNoofDaysofServiceVacationSalary : " + ConvertDecimalPoints(decimal.Round((minNoofServiceDaysVacationSalary ?? 0),
                    MidpointRounding.AwayFromZero),
                    defaultDecimalNos) +
                               " CumulativeLOPDays : " + ConvertDecimalPoints(decimal.Round((cumulativeLOPDays ?? 0),
                    MidpointRounding.AwayFromZero), defaultDecimalNos) + "" +
                               " CumulativeLOPAmount : " + ConvertDecimalPoints(decimal.Round(cumulativeLOPDeductions, MidpointRounding.AwayFromZero), defaultDecimalNos) + ""

                };
                createSlip.CreatedBy = _context == null ? null : Convert.ToInt32(_context.LoginID);
                createSlip.CreatedDate = DateTime.Now;
                listSalarySlip.Add(createSlip);//Add salarySlip into list
                settlementDTO.NoofSalaryDays = noOfDays;


            }
            else
            {
                if (payrollCutoffDate > dateOfJoining)
                {
                    #region retrieve OT & Normal hours Data

                    if (withCUTOFDate == 1)
                    {
                        worksheetTimeData = (from asm in dbContext.EmployeeTimeSheetApprovals
                                             where asm.EmployeeID == employee && asm.TimesheetApprovalStatusID == timesheetStatusID &&
                                             asm.TimesheetDateFrom.Value.Date > prevPayrollCuttofDate.Date
                                             && asm.TimesheetDateTo.Value.Date <= payrollCutoffDate.Date
                                             select asm).AsNoTracking().ToList();

                    }
                    else if (withCUTOFDate == 2)//Calculate OT of the Previous month  
                    {
                        worksheetTimeData = (from asm in dbContext.EmployeeTimeSheetApprovals
                                             where asm.EmployeeID == employee && (asm.TimesheetDateFrom.Value.Year == SlipDate.Value.Year
                                             && asm.TimesheetDateFrom.Value.Month == prevPayrollCuttofDate.Month)
                                             && (asm.TimesheetDateTo.Value.Year == prevPayrollCuttofDate.Year
                                             && asm.TimesheetDateTo.Value.Month == prevPayrollCuttofDate.Month)
                                             && asm.TimesheetApprovalStatusID == timesheetStatusID
                                             select asm).AsNoTracking().ToList();
                    }
                    else
                    {
                        worksheetTimeData = (from asm in dbContext.EmployeeTimeSheetApprovals
                                             where asm.EmployeeID == employee && (asm.TimesheetDateFrom.Value.Year == SlipDate.Value.Year
                                             && asm.TimesheetDateFrom.Value.Month == SlipDate.Value.Month)
                                             && (asm.TimesheetDateTo.Value.Year == SlipDate.Value.Year
                                             && asm.TimesheetDateTo.Value.Month == SlipDate.Value.Month)
                                             && asm.TimesheetApprovalStatusID == timesheetStatusID
                                             select asm).AsNoTracking().ToList();
                    }
                    var normalHours = worksheetTimeData.Sum(x => x.NormalHours);

                    var normalOTHours = worksheetTimeData.Where(x => x.TimesheetTimeTypeID == normalOTTimeTypeID).Select(x => x.OTHours).Sum();

                    var specialOTHrs = worksheetTimeData.Where(x => x.TimesheetTimeTypeID == specialOTTimeTypeID).Select(x => x.OTHours).Sum();

                    #endregion

                    #region retrieve Relareted DeductionComponents

                    var componentIDs = employeeSalaryStructComp.Select(x => x.SalaryComponentID);
                    var relatedComponents = (from asm in dbContext.SalaryComponentRelationMaps
                                             where componentIDs.Contains(asm.SalaryComponentID) && asm.RelationTypeID == 1
                                             select asm).AsNoTracking().ToList();

                    #endregion

                    #region Calculate -no. of Leaves

                    noofleave = (int)settlementDTO.LossofPay;

                    int lastDay = DateTime.DaysInMonth(settlementDTO.SalaryCalculationDate.Value.Year, settlementDTO.SalaryCalculationDate.Value.Month);
                    DateTime lastDateOfMonth = new DateTime(settlementDTO.SalaryCalculationDate.Value.Year, settlementDTO.SalaryCalculationDate.Value.Month, lastDay);
                    var daysDiff = settlementDTO.DateOfLeaving.HasValue
                        && settlementDTO.SalaryCalculationDate.Value.Month == settlementDTO.DateOfLeaving.Value.Month
                        && settlementDTO.SalaryCalculationDate.Value.Year == settlementDTO.DateOfLeaving.Value.Year
                        && lastDateOfMonth > settlementDTO.DateOfLeaving ? (lastDateOfMonth - settlementDTO.DateOfLeaving.Value).TotalDays : 0;
                    if (settlementDTO.IsVacationSalary == true)
                    {
                        noofleave = noofleave + (settlementDTO.VacationDaysInCurrentMonth.HasValue ? (int)settlementDTO.VacationDaysInCurrentMonth.Value : 0) + (int)daysDiff;
                    }
                    var lop = (int)settlementDTO.LossofPay + (int)daysDiff;
                    #endregion

                    if (employeeSalaryStructComp.Count() > 0)
                    {
                        var daysDifference = settlementDTO.NoofDaysInTheMonth - noofleave;// settlementDTO.NoofDaysInTheMonth - settlementDTO.NoofSalaryDays;
                        settlementDTO.NoofSalaryDays = (settlementDTO.NoofDaysInTheMonth ?? 0) - (noofleave);
                        foreach (var employeeSalaryComponents in employeeSalaryStructComp)
                        {

                            #region default Salary components Insert
                            createSlip = new SalarySlipDTO()
                            {
                                SlipDate = SlipDate,
                                EmployeeID = employee,
                                BranchID = settlementDTO.BranchID,
                                SalaryComponentID = employeeSalaryComponents.SalaryComponentID,
                                Amount = ConvertDecimalPoints(decimal.Round(employeeSalaryComponents.Amount ?? 0, MidpointRounding.AwayFromZero), defaultDecimalNos),
                                Description = "Allowance Amount",
                                NoOfDays = settlementDTO.NoofSalaryDays,
                                NoOfHours = normalHours,
                                SalarySlipStatusID = pendingStatusID,
                                IsVerified = false
                            };

                            createSlip.CreatedBy = _context == null ? null : Convert.ToInt32(_context.LoginID);
                            createSlip.CreatedDate = DateTime.Now;

                            listSalarySlip.Add(createSlip);//Add salarySlip into list
                            #endregion

                            var salaryComponentGroupID = employeeSalaryComponents.SalaryComponent.SalaryComponentGroupID;


                            totalWorkDays = (int)settlementDTO.NoofDaysInTheMonth.Value;
                            totalWorkingDays = (int)settlementDTO.NoofDaysInTheMonth.Value;
                            if (salaryComponentGroupID == 2)//Variable allowance
                            {

                                if (noofleave > 0)
                                {
                                    salaryPerDay = (employeeSalaryComponents.Amount.Value) / settlementDTO.NoofDaysInTheMonth;
                                    lopAmount = salaryPerDay * noofleave;
                                    description = @"Calcualted deduction from Total Days : " + settlementDTO.NoofDaysInTheMonth +
                                        " No. of Salary Days : " + settlementDTO.NoofSalaryDays + " No. of Leave Days : " + lop + "  No. of Vacation/Leave salary Days : " + (settlementDTO.VacationDaysInCurrentMonth ?? 0);
                                    noOfLOPDays = lop;
                                }
                                else
                                    lopAmount = null;

                                if (lopAmount.IsNotNull())
                                {
                                    lopAmount = lopAmount > 0 ? -1 * lopAmount : lopAmount;
                                    createSlip = new SalarySlipDTO()
                                    {
                                        SlipDate = SlipDate,
                                        EmployeeID = employee,
                                        BranchID = settlementDTO.BranchID,
                                        Description = description,
                                        NoOfDays = lop,
                                        NoOfHours = normalHours,
                                        IsVerified = false,
                                        SalarySlipStatusID = pendingStatusID,
                                        SalaryComponentID = relatedComponents.Where(x => x.SalaryComponentID == employeeSalaryComponents.SalaryComponentID).
                                        Select(y => y.RelatedComponentID).FirstOrDefault(),
                                        Amount = ConvertDecimalPoints(decimal.Round(lopAmount ?? 0, MidpointRounding.AwayFromZero), defaultDecimalNos)
                                    };
                                    createSlip.CreatedBy = _context == null ? null : Convert.ToInt32(_context.LoginID);
                                    createSlip.CreatedDate = DateTime.Now;
                                    listSalarySlip.Add(createSlip);//Add salarySlip into list
                                }
                            }
                            else if (salaryComponentGroupID == 3 && nooffunctionalDays > 0)//Functional Allowance
                            {

                                salaryPerDay = (employeeSalaryComponents.Amount.Value) / settlementDTO.NoofDaysInTheMonth;
                                var LOPDays = (settlementDTO.NoofDaysInTheMonth - nooffunctionalDays) - noofleave;
                                lopAmount = salaryPerDay * LOPDays;
                                description = @"Calcualted deduction from Total Days : " + settlementDTO.NoofDaysInTheMonth +
                                              " No. of Salary Days : " + settlementDTO.NoofSalaryDays + " No. of Leave/vacation Salary Days : " + noofleave + " NooffunctionalDays : " + nooffunctionalDays;
                                lopAmount = lopAmount > 0 ? -1 * lopAmount : lopAmount;
                                noOfLOPDays = lop;
                                createSlip = new SalarySlipDTO()
                                {
                                    SlipDate = SlipDate,
                                    EmployeeID = employee,
                                    BranchID = settlementDTO.BranchID,
                                    NoOfDays = noOfLOPDays,
                                    NoOfHours = normalHours,
                                    SalarySlipStatusID = pendingStatusID,
                                    IsVerified = false,
                                    SalaryComponentID = relatedComponents.Where(x => x.SalaryComponentID == employeeSalaryComponents.SalaryComponentID).
                                        Select(y => y.RelatedComponentID).FirstOrDefault(),
                                    Amount = ConvertDecimalPoints(decimal.Round(lopAmount ?? 0, MidpointRounding.AwayFromZero), defaultDecimalNos),
                                    Description = description

                                };
                                createSlip.CreatedBy = _context == null ? null : Convert.ToInt32(_context.LoginID);
                                createSlip.CreatedDate = DateTime.Now;
                                listSalarySlip.Add(createSlip);//Add salarySlip into list

                            }
                        } //



                    }//
                     //Deduction Components
                    if (lstemplSalaryStrDedComp.Count() > 0)
                    {
                        foreach (var employeeSalaryComponents in lstemplSalaryStrDedComp)
                        {
                            if (!listSalarySlip.Any(x => x.SalaryComponentID == employeeSalaryComponents.SalaryComponentID))
                            {
                                totalSalary = employeeSalaryComponents.Amount.Value > 0 ? -1 * employeeSalaryComponents.Amount.Value : employeeSalaryComponents.Amount.Value;
                                createSlip = new SalarySlipDTO()
                                {
                                    SlipDate = SlipDate,
                                    EmployeeID = employee,
                                    BranchID = settlementDTO.BranchID,
                                    NoOfDays = totalWorkDays - noofleave,
                                    NoOfHours = normalHours,
                                    SalaryComponentID = employeeSalaryComponents.SalaryComponentID,
                                    Amount = ConvertDecimalPoints(decimal.Round(totalSalary ?? 0, MidpointRounding.AwayFromZero), defaultDecimalNos),
                                    IsVerified = false,
                                    SalarySlipStatusID = pendingStatusID,
                                    Description = "Normal Deduction "
                                };
                                createSlip.CreatedBy = _context == null ? null : Convert.ToInt32(_context.LoginID);
                                createSlip.CreatedDate = DateTime.Now;
                                listSalarySlip.Add(createSlip);
                            }
                        }
                    }

                    #region insert OT Data
                    if (normalOTHours > 0)
                    {

                        var hourlyRate = ((basicAmount / totalWorkingDays) / hrsPerDay) ?? 0;
                        var normalOTAmount = ((hourlyRate * normalOTHours) * normalRate) ?? 0;

                        createSlip = new SalarySlipDTO()
                        {
                            SlipDate = SlipDate,
                            EmployeeID = employee,
                            BranchID = settlementDTO.BranchID,
                            SalaryComponentID = NormalOTComponentID,
                            Amount = ConvertDecimalPoints(decimal.Round(normalOTAmount, MidpointRounding.AwayFromZero), defaultDecimalNos),
                            NoOfHours = normalOTHours,
                            NoOfDays = settlementDTO.NoofSalaryDays,
                            SalarySlipStatusID = pendingStatusID,
                            IsVerified = false,
                            Description = @"Calcualted Normal OT for Basic Amount : " + basicAmount + " TotalWorkingDays :" + totalWorkingDays + " hrsPerDay :" + hrsPerDay +
                             " Normal OT Hours : " + normalOTHours +
                            " Normal OT Rate :'" + normalRate + "'"
                        };
                        createSlip.CreatedBy = _context == null ? null : Convert.ToInt32(_context.LoginID);
                        createSlip.CreatedDate = DateTime.Now;
                        listSalarySlip.Add(createSlip);//Add salarySlip into list
                    }
                    if (specialOTHrs > 0)
                    {
                        var hourlyRate = ((basicAmount / totalWorkingDays) / hrsPerDay) ?? 0;
                        var specialOTAmount = ((hourlyRate * specialOTHrs) * specialRate) ?? 0;

                        createSlip = new SalarySlipDTO()
                        {
                            SlipDate = SlipDate,
                            EmployeeID = employee,
                            BranchID = settlementDTO.BranchID,
                            SalaryComponentID = specialOTComponentID,
                            Amount = ConvertDecimalPoints(decimal.Round(specialOTAmount, MidpointRounding.AwayFromZero), defaultDecimalNos),
                            NoOfHours = specialOTHrs,
                            NoOfDays = totalWorkDays - noofleave,
                            SalarySlipStatusID = pendingStatusID,
                            IsVerified = false,
                            Description = @"Calcualted Special OT for Basic Amount : " + basicAmount + " TotalWorkingDays :" + totalWorkingDays + " hrsPerDay :" + hrsPerDay +
                                " Special OT Hours : " + specialOTHrs +
                                " Special OT Rate :'" + specialRate + "'"
                        };
                        createSlip.CreatedBy = _context == null ? null : Convert.ToInt32(_context.LoginID);
                        createSlip.CreatedDate = DateTime.Now;
                        listSalarySlip.Add(createSlip);//Add salarySlip into list
                    }
                    #endregion


                    decimal? sumSalaryComponent = 0;
                    description = string.Empty;
                    var componentDescription = string.Empty;
                    foreach (var e in emplLeaveSalaryStrComp)
                    {
                        sumSalaryComponent = sumSalaryComponent + e.Amount;
                        componentDescription = componentDescription + e.SalaryComponent.Description + "Amount :" + e.Amount;
                    }

                    if (sumSalaryComponent != 0)
                    {

                        if (settlementDTO.EmployeeSettlementTypeID == 1 && settlementDTO.EmployeeTypeID == academicTypeID)// Academic Staff Type //checking for vaccation salary
                        {

                            var vacationSalryCurMonth = (basicLeaveSalaryAmount ?? 0) * ((settlementDTO.VacationDaysInCurrentMonth ?? 0) / (settlementDTO.NoofDaysInTheMonthLS ?? 0));
                            var vacationDaysInCurrentMonth = ConvertDecimalPoints(decimal.Round((settlementDTO.VacationDaysInCurrentMonth ?? 0), MidpointRounding.AwayFromZero), defaultDecimalNos);
                            createSlip = new SalarySlipDTO()
                            {
                                SlipDate = SlipDate,
                                EmployeeID = employee,
                                BranchID = settlementDTO.BranchID,
                                SalaryComponentID = settlementDTO.VacationSalaryComponent.HasValue ? settlementDTO.VacationSalaryComponent : settlementDTO.LeaveSalaryComponentID,
                                Amount = ConvertDecimalPoints(decimal.Round((vacationSalryCurMonth), MidpointRounding.AwayFromZero), defaultDecimalNos),
                                NoOfDays = vacationDaysInCurrentMonth,
                                SalarySlipStatusID = pendingStatusID,
                                IsVerified = false,
                                Description = @"Calculated with components Basic Pay: " + basicLeaveSalaryAmount +
                               " LeaveDueFrom : '" + settlementDTO.LeaveDueFrom.Value.ToString(dateFormat, CultureInfo.InvariantCulture) + "'" +
                               " LeaveStartDate : '" + settlementDTO.LeaveStartDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) + "'" +
                               " VacationDaysInCurrentMonth : " + vacationDaysInCurrentMonth + ""

                            };
                        }
                        else
                        {
                            var leaveSalaryAmountperDays = (sumSalaryComponent / settlementDTO.NoofDaysInTheMonthLS);
                            var leaveSalaryAmount = leaveSalaryAmountperDays * settlementDTO.NoofLeaveSalaryDays;
                            description = "";
                            if (settlementDTO.LeaveStartDate.HasValue)
                            {
                                description = @"Calculated with components : " + description +
                               " LeaveDueFrom :'" + settlementDTO.LeaveDueFrom.Value.ToString(dateFormat, CultureInfo.InvariantCulture) + "'" +
                               " LeaveStartDate :'" + settlementDTO.LeaveStartDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) + "'" +
                               " AnnualLeaveEntitilements :" + settlementDTO.AnnualLeaveEntitilements +
                               " NoofLeaveSalaryDays :" + settlementDTO.NoofLeaveSalaryDays + "" +
                               " NoofDaysInTheMonth :" + settlementDTO.NoofDaysInTheMonthLS + "";
                            }
                            else
                            {
                                description = @"Calculated with components : " + description +
                               " LeaveDueFrom :'" + settlementDTO.LeaveDueFrom.Value.ToString(dateFormat, CultureInfo.InvariantCulture) + "'" +
                               " AnnualLeaveEntitilements :" + settlementDTO.AnnualLeaveEntitilements +
                               " NoofLeaveSalaryDays :" + settlementDTO.NoofLeaveSalaryDays + "" +
                               " NoofDaysInTheMonth :" + settlementDTO.NoofDaysInTheMonthLS + "";
                            }
                            createSlip = new SalarySlipDTO()
                            {
                                SlipDate = SlipDate,
                                BranchID = settlementDTO.BranchID,
                                EmployeeID = employee,
                                SalaryComponentID = settlementDTO.LeaveSalaryComponentID,
                                Amount = ConvertDecimalPoints(decimal.Round((leaveSalaryAmount ?? 0), MidpointRounding.AwayFromZero), defaultDecimalNos),
                                NoOfDays = ConvertDecimalPoints(decimal.Round((settlementDTO.TotalLeaveDaysAvailable ?? 0), MidpointRounding.AwayFromZero), defaultDecimalNos),
                                SalarySlipStatusID = pendingStatusID,
                                IsVerified = false,
                                Description = description
                            };
                        }
                        createSlip.CreatedBy = _context == null ? null : Convert.ToInt32(_context.LoginID);
                        createSlip.CreatedDate = DateTime.Now;
                        listSalarySlip.Add(createSlip);//Add salarySlip into list
                    }
                }
                var approvedoanStatus = settingBL.GetSettingValue<int>("LOAN_STATUS_APPROVE");
                // for Final Settlement
                if (settlementDTO.EmployeeSettlementTypeID == 2)// ToDo: use from settings
                {
                    #region adding  Gratuity?EoSB Component
                    decimal? periodofService = (decimal?)(settlementDTO.DateOfLeaving.Value - settlementDTO.DateOfJoining.Value).TotalDays;
                    var noofdaysforESBs = (periodofService / 365) * settlementDTO.EndofServiceDaysPerYear.Value;

                    var EoSBSalaryAmountperDays = (basicAmount / settlementDTO.NoofDaysInTheMonthEoSB);
                    var EoSBSalaryAmount = EoSBSalaryAmountperDays * noofdaysforESBs;
                    createSlip = new SalarySlipDTO()
                    {
                        SlipDate = SlipDate,
                        EmployeeID = employee,
                        BranchID = settlementDTO.BranchID,
                        SalaryComponentID = settlementDTO.GratuityComponentID,
                        Amount = ConvertDecimalPoints(decimal.Round((EoSBSalaryAmount ?? 0), MidpointRounding.AwayFromZero), defaultDecimalNos),
                        NoOfDays = ConvertDecimalPoints(decimal.Round((noofdaysforESBs ?? 0), MidpointRounding.AwayFromZero), defaultDecimalNos),
                        SalarySlipStatusID = pendingStatusID,
                        IsVerified = false,
                        Description = @"Calculated with basicpay : " + basicAmount +
                        " DateOfJoining :'" + settlementDTO.DateOfJoining.Value.ToString(dateFormat, CultureInfo.InvariantCulture) + "'" +
                        " DateOfLeaving:'" + settlementDTO.DateOfLeaving.Value.ToString(dateFormat, CultureInfo.InvariantCulture) + "'" +
                        " LeaveDueFrom :'" + settlementDTO.LeaveDueFrom.Value.ToString(dateFormat, CultureInfo.InvariantCulture) + "'" +
                        //" LeaveStartDate :'" + settlementDTO.LeaveStartDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) + "'" +
                        " PeriodofService :" + decimal.Round((periodofService ?? 0), MidpointRounding.AwayFromZero) +
                        " NoofdaysforEoSBs :" + decimal.Round((noofdaysforESBs ?? 0), MidpointRounding.AwayFromZero) +
                        " NoofDaysInTheMonthEoSB :" + decimal.Round((settlementDTO.NoofDaysInTheMonthEoSB ?? 0), MidpointRounding.AwayFromZero) +
                        " EoSBSalaryAmountperDays :" + decimal.Round((EoSBSalaryAmountperDays ?? 0), MidpointRounding.AwayFromZero) +
                        " EoSB Entitlement per year :" + decimal.Round((settlementDTO.EndofServiceDaysPerYear ?? 0), MidpointRounding.AwayFromZero) + ""

                    };
                    createSlip.CreatedBy = _context == null ? null : Convert.ToInt32(_context.LoginID);
                    createSlip.CreatedDate = DateTime.Now;
                    listSalarySlip.Add(createSlip);//Add salarySlip into list

                    #endregion Adding Gratuity/EoSB Component

                    #region Loan

                    var loanData = (from lh in dbContext.LoanHeads
                                    where lh.EmployeeID == employee && lh.LoanTypeID == loanType && lh.LoanStatusID == approvedoanStatus
                                    select lh)
                                    .Include(i => i.LoanDetails)
                                    .AsNoTracking().ToList();
                    if (loanData.Count() > 0)
                    {
                        description = "";
                        decimal loanSumAMount = 0;
                        foreach (var loan in loanData)
                        {
                            var loanAmount = (loan.LoanAmount ?? 0);
                            var paidAmount = loan.LoanDetails.Sum(x => (x.PaidAmount ?? 0));
                            var balance = loanAmount - paidAmount;
                            loanSumAMount += balance;
                            description = description + "Loan No." + loan.LoanNo + "Loan Amount :" + loanAmount + " Paid Amount:" + paidAmount + "Pending Amount:" + balance + "";
                        }
                        var totalLoanAmount = loanSumAMount > 0 ? -1 * loanSumAMount : loanSumAMount;
                        createSlip = new SalarySlipDTO()
                        {
                            SlipDate = SlipDate,
                            EmployeeID = employee,
                            BranchID = settlementDTO.BranchID,
                            SalaryComponentID = settlementDTO.LoanComponentID,
                            Amount = ConvertDecimalPoints(decimal.Round(totalLoanAmount, MidpointRounding.AwayFromZero), defaultDecimalNos),
                            NoOfDays = ConvertDecimalPoints(decimal.Round((noofdaysforESBs ?? 0), MidpointRounding.AwayFromZero), defaultDecimalNos),
                            SalarySlipStatusID = pendingStatusID,
                            IsVerified = false,
                            Description = description

                        };
                        createSlip.CreatedBy = _context == null ? null : Convert.ToInt32(_context.LoginID);
                        createSlip.CreatedDate = DateTime.Now;
                        listSalarySlip.Add(createSlip);//Add salarySlip into list
                    }
                    #endregion Loan

                    #region Advance



                    var advanceData = (from lh in dbContext.LoanHeads
                                       where lh.EmployeeID == employee && lh.LoanTypeID == salaryAdvanceType && lh.LoanStatusID == approvedoanStatus
                                       select lh)
                                        .Include(i => i.LoanDetails)
                                        .AsNoTracking().ToList();
                    if (advanceData.Count() > 0)
                    {
                        decimal advanceSumAMount = 0;
                        description = "";
                        foreach (var advance in advanceData)
                        {
                            var advanceAmount = (advance.LoanAmount ?? 0);
                            var paidAmount = advance.LoanDetails.Sum(x => (x.PaidAmount ?? 0));
                            var balance = advanceAmount - paidAmount;
                            advanceSumAMount += balance;
                            description = description + "Advance No." + advance.LoanNo + "Advance Amount :" + advanceAmount + " Paid Amount:" + paidAmount + "Pending Amount:" + balance + "";
                        }
                        var totalAdvanceAmount = advanceSumAMount > 0 ? -1 * advanceSumAMount : advanceSumAMount;
                        createSlip = new SalarySlipDTO()
                        {
                            SlipDate = SlipDate,
                            EmployeeID = employee,
                            BranchID = settlementDTO.BranchID,
                            SalaryComponentID = settlementDTO.AdvanceComponentID,
                            Amount = ConvertDecimalPoints(decimal.Round(totalAdvanceAmount, MidpointRounding.AwayFromZero), defaultDecimalNos),
                            NoOfDays = ConvertDecimalPoints(decimal.Round((noofdaysforESBs ?? 0), MidpointRounding.AwayFromZero), defaultDecimalNos),
                            SalarySlipStatusID = pendingStatusID,
                            IsVerified = false,
                            Description = description

                        };
                        createSlip.CreatedBy = _context == null ? null : Convert.ToInt32(_context.LoginID);
                        createSlip.CreatedDate = DateTime.Now;
                        listSalarySlip.Add(createSlip);//Add salarySlip into list
                    }
                    #endregion Advance


                }
                else// Leave Salary
                {
                    #region Loan

                    var loanData = (from lh in dbContext.LoanHeads

                                    where lh.EmployeeID == employee
                                    && lh.LoanTypeID == loanType
                                    && lh.LoanStatusID == approvedoanStatus
                                    select lh)
                                    .Include(i => i.LoanDetails)
                                    .AsNoTracking().ToList();


                    listLoanHeadIds = loanData.Select(x => x.LoanHeadIID).Distinct().ToList();
                    var loanDetail = (from eld in dbContext.LoanDetails
                                      where listLoanHeadIds.Contains(eld.LoanHeadID.Value)
                                      && (eld.InstallmentDate.Value.Year == settlementDTO.SalaryCalculationDate.Value.Year
                                      && eld.InstallmentDate.Value.Month == settlementDTO.SalaryCalculationDate.Value.Month)
                                      && eld.LoanEntryStatusID == activeInstallmentstatus
                                      select eld).Include(x => x.LoanHead).AsNoTracking().ToList();

                    listloanDetailIds = loanDetail.Select(x => x.LoanDetailID).Distinct().ToList();
                    description = "";
                    decimal loanSumAMount = 0;
                    if (loanData.Count() > 0 && loanDetail.Count() > 0)
                    {
                        description = "";

                        foreach (var loan in loanDetail)
                        {
                            var loanAmount = (loan.LoanHead.LoanAmount ?? 0);
                            var paidAmount = loanDetail.Where(x => x.LoanHeadID == loan.LoanHeadID).Sum(y => (y.PaidAmount ?? 0));
                            var balance = (loan.LoanHead.LoanAmount ?? 0) - (paidAmount + loan.InstallmentAmount);
                            loanSumAMount += (loan.InstallmentAmount ?? 0);
                            description = description + "Loan No." + loan.LoanHead.LoanNo + "Loan Amount :" + loanAmount + " Installment Amount: " + loanSumAMount + " Paid Amount:" + paidAmount + "Balance:" + balance + "";
                        }
                        var installmentAmount = loanSumAMount > 0 ? -1 * loanSumAMount : loanSumAMount;
                        createSlip = new SalarySlipDTO()
                        {
                            SlipDate = SlipDate,
                            EmployeeID = employee,
                            BranchID = settlementDTO.BranchID,
                            SalaryComponentID = settlementDTO.LoanComponentID,
                            Amount = ConvertDecimalPoints(decimal.Round(installmentAmount, MidpointRounding.AwayFromZero), defaultDecimalNos),
                            NoOfDays = totalWorkDays - noofleave,
                            SalarySlipStatusID = pendingStatusID,
                            IsVerified = false,
                            Description = description

                        };
                        createSlip.CreatedBy = _context == null ? null : Convert.ToInt32(_context.LoginID);
                        createSlip.CreatedDate = DateTime.Now;
                        listSalarySlip.Add(createSlip);//Add salarySlip into list
                    }
                    #endregion Loan

                    #region Advance


                    var advanceData = (from lh in dbContext.LoanHeads
                                       where lh.EmployeeID == employee
                                       && lh.LoanTypeID == salaryAdvanceType
                                       && lh.LoanStatusID == approvedoanStatus
                                       select lh)
                                 .Include(i => i.LoanDetails)
                                 .AsNoTracking().ToList();


                    listHeadIds = advanceData.Select(x => x.LoanHeadIID).Distinct().ToList();
                    var advanceDetail = (from eld in dbContext.LoanDetails
                                         where listHeadIds.Contains(eld.LoanHeadID.Value)
                                         && (eld.InstallmentDate.Value.Year == settlementDTO.SalaryCalculationDate.Value.Year
                                         && eld.InstallmentDate.Value.Month == settlementDTO.SalaryCalculationDate.Value.Month)
                                         && eld.LoanEntryStatusID == activeInstallmentstatus
                                         select eld).Include(x => x.LoanHead).AsNoTracking().ToList();

                    listDetailIds = advanceDetail.Select(x => x.LoanDetailID).Distinct().ToList();


                    decimal advanceSumAMount = 0;
                    description = "";

                    if (advanceData.Count() > 0 && advanceDetail.Count() > 0)
                    {

                        foreach (var advance in advanceDetail)
                        {
                            var advanceAmount = (advance.LoanHead.LoanAmount ?? 0);
                            var paidAmount = advanceDetail.Where(x => x.LoanHeadID == advance.LoanHeadID).Sum(y => (y.PaidAmount ?? 0));
                            var balance = advanceAmount - (paidAmount + (advance.InstallmentAmount ?? 0));
                            advanceSumAMount += balance;
                            description = description + "Advance No." + advance.LoanHead.LoanNo + "Advance Amount :" + advanceAmount + " Installment Amount: " + advanceSumAMount + " Paid Amount:" + paidAmount + "Balance:" + balance + "";
                        }
                        var installmentAmount = advanceSumAMount > 0 ? -1 * advanceSumAMount : advanceSumAMount;
                        createSlip = new SalarySlipDTO()
                        {
                            SlipDate = SlipDate,
                            EmployeeID = employee,
                            BranchID = settlementDTO.BranchID,
                            SalaryComponentID = settlementDTO.AdvanceComponentID,
                            Amount = ConvertDecimalPoints(decimal.Round(installmentAmount, MidpointRounding.AwayFromZero), defaultDecimalNos),
                            NoOfDays = totalWorkDays - noofleave,
                            SalarySlipStatusID = pendingStatusID,
                            IsVerified = false,
                            Description = description

                        };
                        createSlip.CreatedBy = _context == null ? null : Convert.ToInt32(_context.LoginID);
                        createSlip.CreatedDate = DateTime.Now;
                        listSalarySlip.Add(createSlip);//Add salarySlip into list
                    }
                    #endregion Advance
                }

            }



            var salaryComponentIDs = listSalarySlip.Select(x => x.SalaryComponentID).ToList();
            var salaryComponents = dbContext.SalaryComponents.Where(x => salaryComponentIDs.Contains(x.SalaryComponentID)).ToList();
            foreach (var salaryComponent in listSalarySlip)
            {
                salaryComponent.SalaryComponentKeyValue = salaryComponent.SalaryComponentID.HasValue ? new KeyValueDTO()
                {
                    Key = salaryComponent.SalaryComponentID.ToString(),
                    Value = salaryComponents.FirstOrDefault(x => x.SalaryComponentID == salaryComponent.SalaryComponentID).Description
                } : new KeyValueDTO();
            }

            settlementDTO.SalarySlipDTOs = new List<SalarySlipDTO>();
            settlementDTO.SalarySlipDTOs.AddRange(listSalarySlip);
            return settlementDTO;
        }

        private decimal ConvertDecimalPoints(decimal amount, int defaultDecimalNos)
        {
            return decimal.Parse(amount.ToString("0.000"));
        }
        private decimal ConverttoTwoDecimalPoints(decimal amount, int defaultDecimalNos)
        {
            return decimal.Parse(amount.ToString("0.00"));
        }
    }
}

