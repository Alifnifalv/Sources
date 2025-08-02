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
using Eduegate.Domain.Setting;
using Eduegate.Domain.Entity.School.Models;
using System.Runtime.InteropServices;
using Eduegate.Domain.Entity.Models;
using FirebaseAdmin.Messaging;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Eduegate.Domain.Entity.HR.Leaves;
using Eduegate.Services.Contracts.Mutual;
using static Eduegate.Domain.Mappers.HR.Payroll.SalarySlipMapper;
using System.Drawing.Text;
using Eduegate.Services.Contracts.Commons;
using Microsoft.PowerBI.Api.Models;
using Microsoft.PowerBI.Api;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Services.Contracts.School.Exams;
namespace Eduegate.Domain.Mappers.HR.Payroll
{
    public class LSProvisionMapper : DTOEntityDynamicMapper
    {
        CallContext _callContext;
        public static LSProvisionMapper Mapper(CallContext context)
        {
            var mapper = new LSProvisionMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<LSESBProvisionDetailDTO>(entity);
        }
        public override string ToDTOString(BaseMasterDTO dto)
        {
            return JsonConvert.SerializeObject(dto);
        }


        public override string SaveEntity(BaseMasterDTO dto)
        {
            var slipdto = dto as LSESBProvisionDetailDTO;
            string message = string.Empty;

            return message;
        }
        public override string GetEntity(long IID)
        {
            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                var entity = dbContext.EmployeeLSProvisionHeads.Where(X => X.EmployeeLSProvisionHeadIID == IID)
                    .Include(x => x.Branch)
                    .AsNoTracking()
                    .FirstOrDefault();

                var entityDetail = dbContext.EmployeeLSProvisionDetails.Where(X => X.EmployeeLSProvisionHeadID == IID)
                  .Include(x => x.Employee).ThenInclude(x => x.Departments1)
                  .Include(x => x.Employee).ThenInclude(x => x.CalendarType)
                  .AsNoTracking()
                  .ToList();
                var detail = new EmployeeLSProvisionHeadDTO()
                {
                    BranchID = entity.BranchID,
                    Branch = entity.BranchID.HasValue ? entity.Branch.BranchName : null,
                    DocumentTypeID = entity.DocumentTypeID,
                    SalaryComponentID = entity.SalaryComponentID,
                    EntryDate = entity.EntryDate,
                    EntryNumber = entity.EntryNumber,
                    EmployeeLSProvisionHeadIID = entity.EmployeeLSProvisionHeadIID
                };
                detail.LSProvisionDetails = new List<EmployeeLSProvisionDetailDTO>();
                foreach (var edtl in entityDetail)
                {
                    var employeeLSProvisionDetailDTO = new EmployeeLSProvisionDetailDTO()
                    {
                        EmployeeLSProvisionHeadID = edtl.EmployeeLSProvisionHeadID,
                        EmployeeLSProvisionDetailIID = edtl.EmployeeLSProvisionDetailIID,
                        BasicSalary = edtl.BasicSalary ?? 0,
                        Category = edtl.Employee.CalendarType?.Description,
                        Department = edtl.Employee.Departments1.DepartmentName,
                        EmployeeCode = edtl.Employee.EmployeeCode,
                        EmployeeName = edtl.Employee.FirstName + " " + edtl.Employee.MiddleName + " " + edtl.Employee.LastName,
                        DOJ = edtl.Employee.DateOfJoining,
                        NoofLeaveSalaryDays = edtl.NoofLeaveSalaryDays,
                        LastLeaveSalaryDate = edtl.LastLeaveSalaryDate,
                        LeaveSalaryAmount = edtl.LeaveSalaryAmount,
                        EmployeeID = edtl.EmployeeID,
                        OpeningAmount = edtl.OpeningAmount,
                        Balance = edtl.Balance
                    };
                    detail.LSProvisionDetails.Add(employeeLSProvisionDetailDTO);
                }
                return ToDTOString(detail);
            }
        }


        public OperationResultWithIDsDTO GenerateLeaveSalaryProvision(EmployeeLSProvisionHeadDTO lSProvisionHeadDTO)
        {
            MutualRepository mutualRepository = new MutualRepository();
            var settingBL = new Domain.Setting.SettingBL(_callContext);
            var operationresultDTO = new OperationResultWithIDsDTO();
            var employeeLSProvisionDetailList = new List<EmployeeLSProvisionDetailDTO>();
            lSProvisionHeadDTO.EmployeeLSProvisionHeadIID = 0;
            var lstEmployees = new List<long>();
            lSProvisionHeadDTO.LSProvisionDetails = new List<EmployeeLSProvisionDetailDTO>();
            decimal? basicPay = 0;
            var departmentIDS = lSProvisionHeadDTO.Department.Any(y => y.Key == null) ? new List<long>(0) : lSProvisionHeadDTO.Department.Select(x => long.Parse(x.Key)).ToList();
            var employeeIDs = lSProvisionHeadDTO.Employees.Any(y => y.Key == null) ? new List<long>(0) : lSProvisionHeadDTO.Employees.Select(x => long.Parse(x.Key)).ToList();
            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                try
                {
                    var academicTypeID = settingBL.GetSettingValue<int>("ACADEMIC_CALENDAR_TYPE_ID");
                    var employees = new List<Entity.HR.Models.Employee>();
                    //
                    employees = (from e in dbContext.Employees
                                 join dt in dbContext.Departments1 on
                                 e.DepartmentID equals dt.DepartmentID
                                 where e.IsLeaveSalaryEligible == true && e.BranchID == lSProvisionHeadDTO.BranchID && e.CalendarTypeID != academicTypeID && ((employeeIDs.Count == 0 || employeeIDs.Contains(e.EmployeeIID))
                                 && (departmentIDS.Count == 0 || departmentIDS.Contains(e.DepartmentID.Value)) && e.IsActive == true)
                                 select e).Include(x => x.CalendarType).Include(x => x.Departments1).AsNoTracking().ToList();
                    if (employees.Count == 0)
                    {
                        operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                        operationresultDTO.Message = "No employees were found. Please check the eligibility and department selection!";
                        return operationresultDTO;
                    }
                    lstEmployees = employees.Select(x => x.EmployeeIID).ToList();

                    var startDate = new DateTime(lSProvisionHeadDTO.EntryDate.Value.Year, lSProvisionHeadDTO.EntryDate.Value.Month, 1);
                    var endDate = startDate.AddMonths(1);

                    var LSprovEntities = dbContext.EmployeeLSProvisionDetails
                        .Include(x => x.EmployeeLSProvisionHead)
                        .Where(ss => ss.EmployeeID.HasValue && lstEmployees.Contains(ss.EmployeeID.Value)
                        && ss.EmployeeLSProvisionHead.EntryDate.HasValue
                        && ss.EmployeeLSProvisionHead.EntryDate >= startDate
                        && ss.EmployeeLSProvisionHead.EntryDate <= endDate)
                       .ToList();

                    if (LSprovEntities.Any())
                    {
                        if (lSProvisionHeadDTO.IsRegenerate)
                        {
                            if (!LSprovEntities.Any(x => x.EmployeeLSProvisionHead.IsaccountPosted  == true))
                            {
                                var headIdsToCheck = LSprovEntities
                                .Select(e => e.EmployeeLSProvisionHeadID)
                                .Distinct()
                                .ToList();


                                dbContext.EmployeeLSProvisionDetails.RemoveRange(LSprovEntities);
                                dbContext.SaveChanges();


                                foreach (var headId in headIdsToCheck)
                                {
                                    bool hasOtherDetails = dbContext.EmployeeLSProvisionDetails
                                        .Any(d => d.EmployeeLSProvisionHeadID == headId);

                                    if (!hasOtherDetails)
                                    {
                                        var head = dbContext.EmployeeLSProvisionHeads.FirstOrDefault(h => h.EmployeeLSProvisionHeadIID == headId);
                                        if (head != null)
                                        {
                                            dbContext.EmployeeLSProvisionHeads.Remove(head);
                                        }

                                    }
                                }

                                dbContext.SaveChanges();
                                LSprovEntities.Clear();
                            }
                            else
                            {
                                operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                                operationresultDTO.Message = "Cannot be regenerated. Account has already been posted for some entries!";
                                return operationresultDTO;

                            }
                        }
                        else
                        {
                            operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                            operationresultDTO.Message = "Already generated. Please regenerate the existing entries!";
                            return operationresultDTO;
                        }
                    }


                    var existList = (from e in dbContext.EmployeeLSProvisionHeads
                                     where e.EmployeeLSProvisionHeadIID == lSProvisionHeadDTO.EmployeeLSProvisionHeadIID
                                     select e).AsNoTracking().FirstOrDefault();

                    if (existList != null && !string.IsNullOrEmpty(existList.EntryNumber))
                    {
                        lSProvisionHeadDTO.EntryNumber = existList.EntryNumber;
                    }


                    int? basicPayComponentID = new Domain.Setting.SettingBL(null).GetSettingValue<int?>("BASICPAY_ID");
                    var basicPayDets = (from e in dbContext.EmployeeSalaryStructureLeaveSalaryMaps
                                        where (e.SalaryComponentID == basicPayComponentID && lstEmployees.Contains(e.EmployeeSalaryStructure.EmployeeID ?? 0) && e.EmployeeSalaryStructure.IsActive == true)
                                        select e).Include(x => x.EmployeeSalaryStructure).AsNoTracking().ToList();
                    if (basicPayDets != null && basicPayDets.Count() > 0)
                    {
                        List<long> employeeIds = employees.Select(x => (x.EmployeeIID)).ToList();
                        int? leaveSalarydays = new Domain.Setting.SettingBL(null).GetSettingValue<int?>("LEAVE_SALARY_NOOF_MONTH_DAYS");
                        int? endOfSalaryBenefitComp = new Domain.Setting.SettingBL(null).GetSettingValue<int?>("END_OF_SERVICE_BENEFIT");

                        lSProvisionHeadDTO.SalaryComponentID = new Domain.Setting.SettingBL(null).GetSettingValue<int?>("LEAVE_SALARY");
                        lSProvisionHeadDTO.DocumentTypeID = new Domain.Setting.SettingBL(null).GetSettingValue<int?>("DOCUMENT_TYPE_LS");

                        var latestSlipDates = dbContext.SalarySlips
                                        .Where(s => lstEmployees.Contains(s.EmployeeID ?? 0)
                                        && s.SalaryComponentID == lSProvisionHeadDTO.SalaryComponentID
                                        && s.SlipDate != null)
                                        .GroupBy(s => s.EmployeeID)
                                        .Select(g => new
                                        {
                                            EmployeeID = g.Key,
                                            LastSlipDate = g.Max(s => s.SlipDate)
                                        }).ToList();

                        foreach (var employee in employees)
                        {
                            var basicPayDts = basicPayDets.Where(x => x.EmployeeSalaryStructure.EmployeeID == employee.EmployeeIID).FirstOrDefault();
                            if (basicPayDts != null)
                            {
                                basicPay = basicPayDts.Amount ?? 0;
                            }
                            else
                                basicPay = 0;
                            if (basicPay != 0)
                            {
                                var latestSlipDate = latestSlipDates.Where(x => x.EmployeeID == employee.EmployeeIID).Select(x => x.LastSlipDate).FirstOrDefault();
                                var leaveSalary = CalculateLeaveSalary(lSProvisionHeadDTO.EntryDate, employee.DateOfJoining, latestSlipDate, basicPay, leaveSalarydays);

                                var employeeLSProvisionDetailDTO = new EmployeeLSProvisionDetailDTO()
                                {
                                    // EmployeeLSProvisionHeadID = lSProvisionHeadDTO.EmployeeLSProvisionHeadIID,                                
                                    BasicSalary = basicPay ?? 0,
                                    Category = employee.CalendarType?.Description,
                                    Department = employee.Departments1.DepartmentName,
                                    EmployeeCode = employee.EmployeeCode,
                                    EmployeeName = employee.FirstName + " " + employee.MiddleName + " " + employee.LastName,
                                    DOJ = employee.DateOfJoining,
                                    NoofLeaveSalaryDays = leaveSalarydays ?? 30,
                                    LastLeaveSalaryDate = latestSlipDate,
                                    LeaveSalaryAmount = leaveSalary,
                                    EmployeeID = employee.EmployeeIID
                                };
                                employeeLSProvisionDetailList.Add(employeeLSProvisionDetailDTO);
                            }
                        }

                        lSProvisionHeadDTO.LSProvisionDetails.AddRange(employeeLSProvisionDetailList);
                    }

                }
                catch (Exception ex)
                {
                    var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                       ? ex.InnerException?.Message : ex.Message;
                    operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                    operationresultDTO.Message = errorMessage;
                    Eduegate.Logger.LogHelper<string>.Fatal($". Error message: {errorMessage}", ex);
                    return operationresultDTO;
                }

                if (employeeLSProvisionDetailList.Count() == 0)
                {
                    operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                    operationresultDTO.Message = "Not generated. Please check eligibility and Leave salary structure settings!";
                    return operationresultDTO;
                }
                else
                {
                    operationresultDTO = SaveLeaveSalaryProvision(dbContext, GetPreviousData(dbContext, lSProvisionHeadDTO, lstEmployees));
                    return operationresultDTO;
                }
            }
        }
        private EmployeeLSProvisionHeadDTO GetPreviousData(dbEduegateHRContext dbContext, EmployeeLSProvisionHeadDTO toDto, List<long> employeeIdlist)
        {
            DateTime firstDayPrevMonth = new DateTime(toDto.EntryDate.Value.Year, toDto.EntryDate.Value.Month, 1).AddMonths(-1);
            DateTime lastDayPrevMonth = new DateTime(toDto.EntryDate.Value.Year, toDto.EntryDate.Value.Month, 1).AddDays(-1);

            var prevDataDict = dbContext.EmployeeLSProvisionDetails
                .Where(ss =>
                    ss.EmployeeID.HasValue &&
                    employeeIdlist.Contains(ss.EmployeeID.Value) &&
                    ss.EmployeeLSProvisionHead.EntryDate.HasValue &&
                    ss.EmployeeLSProvisionHead.EntryDate >= firstDayPrevMonth &&
                    ss.EmployeeLSProvisionHead.EntryDate <= lastDayPrevMonth)
                .Select(ss => new
                {
                    ss.EmployeeID,
                    PrevLSAmount = ss.LeaveSalaryAmount
                })
                .AsEnumerable()
                .GroupBy(x => x.EmployeeID.Value)
                .ToDictionary(g => g.Key, g => g.First().PrevLSAmount ?? 0);


            foreach (var detail in toDto.LSProvisionDetails)
            {
                if (detail.EmployeeID.HasValue && prevDataDict.TryGetValue(detail.EmployeeID.Value, out var prevAmount))
                {
                    detail.OpeningAmount = prevAmount;
                    detail.Balance = Math.Round(((detail.LeaveSalaryAmount ?? 0) - prevAmount), 0);
                }
                else
                {
                    detail.Balance = (detail.LeaveSalaryAmount ?? 0);
                }
            }

            return toDto;
        }

        private OperationResultWithIDsDTO SaveLeaveSalaryProvision(dbEduegateHRContext dbContext, EmployeeLSProvisionHeadDTO toDto)
        {
            var operationresultDTO = new OperationResultWithIDsDTO();
            operationresultDTO.IDList = new List<long>();
            var entity = new EmployeeLSProvisionHead();
            var EntityemployeeLSProvisions = new List<EmployeeLSProvisionDetail>();
            #region Common Declarations
            Entity.Models.Settings.Sequence sequence = new Entity.Models.Settings.Sequence();
            MutualRepository mutualRepository = new MutualRepository();
            #endregion 
            try
            {

                try
                {
                    if (string.IsNullOrEmpty(toDto.EntryNumber))
                    {
                        sequence = mutualRepository.GetNextSequence("LSProvisionEntryNO", null);
                        toDto.EntryNumber = sequence.Prefix + sequence.LastSequence;
                    }
                }
                catch (Exception ex)
                {
                    operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                    operationresultDTO.Message = "Please generate sequence with 'LSProvisionEntryNO'!";
                    return operationresultDTO;
                }
                entity = new EmployeeLSProvisionHead()
                {
                    BranchID = toDto.BranchID,
                    DocumentTypeID = toDto.DocumentTypeID,
                    SalaryComponentID = toDto.SalaryComponentID,
                    EntryDate = toDto.EntryDate,
                    EntryNumber = toDto.EntryNumber,
                    EmployeeLSProvisionHeadIID = toDto.EmployeeLSProvisionHeadIID,

                };
                if (toDto.EmployeeLSProvisionHeadIID == 0)
                {
                    entity.CreatedBy = (int)_context.LoginID;
                    entity.CreatedDate = DateTime.Now;
                }
                else
                {
                    entity.UpdatedBy = (int)_context.LoginID;
                    entity.UpdatedDate = DateTime.Now;

                }
                entity.EmployeeLSProvisionDetails = new List<EmployeeLSProvisionDetail>();
                foreach (var dat in toDto.LSProvisionDetails)
                {
                    entity.EmployeeLSProvisionDetails.Add(new EmployeeLSProvisionDetail()
                    {
                        EmployeeLSProvisionDetailIID = dat.EmployeeLSProvisionDetailIID,
                        EmployeeID = dat.EmployeeID,
                        EmployeeLSProvisionHeadID = entity.EmployeeLSProvisionHeadIID,
                        BasicSalary = dat.BasicSalary,
                        OpeningAmount = dat.OpeningAmount,
                        Balance = dat.Balance,
                        NoofLeaveSalaryDays = dat.NoofLeaveSalaryDays,
                        LastLeaveSalaryDate = dat.LastLeaveSalaryDate,
                        LeaveSalaryAmount = dat.LeaveSalaryAmount,

                    });
                }


                //delete maps
                var entities = dbContext.EmployeeLSProvisionDetails.Where(x =>
                    x.EmployeeLSProvisionHeadID == entity.EmployeeLSProvisionHeadIID
                    ).AsNoTracking().ToList();


                if (entity.EmployeeLSProvisionHeadIID == 0)
                {
                    dbContext.EmployeeLSProvisionHeads.Add(entity);

                    dbContext.Entry(entity).State = EntityState.Added;
                }
                else
                {

                    foreach (var map in entity.EmployeeLSProvisionDetails)
                    {
                        foreach (var ent in entities)
                        {
                            if (ent.EmployeeID == map.EmployeeID)
                            {
                                map.EmployeeLSProvisionDetailIID = ent.EmployeeLSProvisionDetailIID;
                            }
                            if (map.EmployeeLSProvisionDetailIID == 0)
                            {
                                dbContext.Entry(map).State = EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(map).State = EntityState.Modified;
                            }
                        }
                    }

                    dbContext.Entry(entity).State = EntityState.Modified;
                }
                dbContext.SaveChanges();


                var employeeList = new List<long?>();
                employeeList = toDto.LSProvisionDetails.Select(x => x.EmployeeID).ToList();



                foreach (var det in entity.EmployeeLSProvisionDetails)
                {
                    foreach (var dtoDet in toDto.LSProvisionDetails)
                    {
                        if (det.EmployeeID == dtoDet.EmployeeID)
                        {
                            dtoDet.EmployeeLSProvisionDetailIID = det.EmployeeLSProvisionDetailIID;
                            dtoDet.EmployeeLSProvisionHeadID = det.EmployeeLSProvisionHeadID;

                        }
                    }
                }

                toDto.EmployeeLSProvisionHeadIID = entity.EmployeeLSProvisionHeadIID;
                operationresultDTO.EmployeeLSProvisionHead = toDto;
                operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Success;
                operationresultDTO.Message = "Generated successfully!";
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                       ? ex.InnerException?.Message : ex.Message;
                operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                operationresultDTO.Message = errorMessage;
                Eduegate.Logger.LogHelper<string>.Fatal($". Error message: {errorMessage}", ex);
                return operationresultDTO;
            }
            return operationresultDTO;
        }
        private decimal CalculateLeaveSalary(DateTime? referenceDate,
            DateTime? doj, DateTime? lastSalaryDate,
            decimal? basicSalary, int? leaveDays)
        {

            if (doj == null || basicSalary <= 0 || leaveDays <= 0)
            {
                return 0;
            }

            DateTime maxDate = doj.Value;
            if (lastSalaryDate != null && lastSalaryDate > maxDate)
            {
                maxDate = lastSalaryDate.Value;
            }

            double yearsWorked = (referenceDate.Value - maxDate).TotalDays / 365.0;
            if (yearsWorked <= 0)
            {
                return 0;
            }

            decimal dailySalary = (basicSalary ?? 0) / 30;

            decimal leaveSalary = Math.Round(dailySalary * (decimal)(yearsWorked * leaveDays), 0);
            return leaveSalary;
        }

        public string UpdateLSAccountPostStatus(EmployeeLSProvisionHeadDTO dto)
        {
            try
            {
                using (var dbContext = new dbEduegateHRContext())
                {
                    var employeeLSProvisionHeads = dbContext.EmployeeLSProvisionHeads.Where(m => m.EmployeeLSProvisionHeadIID == dto.EmployeeLSProvisionHeadIID).FirstOrDefault();
                    if (employeeLSProvisionHeads != null)
                    {
                        employeeLSProvisionHeads.IsaccountPosted = true;
                        employeeLSProvisionHeads.UpdatedBy = _context.LoginID.HasValue ? Convert.ToInt32(_context.LoginID) : (int?)null;
                        employeeLSProvisionHeads.UpdatedDate = DateTime.Now;
                        dbContext.Entry(employeeLSProvisionHeads).State = EntityState.Modified;
                        dbContext.SaveChanges();

                    }
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                      ? ex.InnerException?.Message : ex.Message;

                Eduegate.Logger.LogHelper<string>.Fatal($". Error message: {errorMessage}", ex);
                return errorMessage;

            }
            return "success";
        }

    }
}
