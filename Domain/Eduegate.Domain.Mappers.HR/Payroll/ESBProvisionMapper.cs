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
using UglyToad.PdfPig.Graphics.Operations.PathPainting;
namespace Eduegate.Domain.Mappers.HR.Payroll
{
    public class ESBProvisionMapper : DTOEntityDynamicMapper
    {
        CallContext _callContext;
        public static ESBProvisionMapper Mapper(CallContext context)
        {
            var mapper = new ESBProvisionMapper();
            mapper._context = context;
            return mapper;
        }
        public OperationResultWithIDsDTO GenerateESBProvision(EmployeeESBProvisionHeadDTO eSBProvisionHeadDTO)

        {
            MutualRepository mutualRepository = new MutualRepository();
            var settingBL = new Domain.Setting.SettingBL(_callContext);
            var operationresultDTO = new OperationResultWithIDsDTO();
            var employeeESBProvisionDetailList = new List<EmployeeESBProvisionDetailDTO>();
            eSBProvisionHeadDTO.EmployeeESBProvisionHeadIID = 0;
            eSBProvisionHeadDTO.ESBProvisionDetails = new List<EmployeeESBProvisionDetailDTO>();
            decimal? basicPay = 0;
            var employeeIdlist = new List<long>();
            var departmentIDS = eSBProvisionHeadDTO.Department.Any(y => y.Key == null) ? new List<long>(0) : eSBProvisionHeadDTO.Department.Select(x => long.Parse(x.Key)).ToList();
            var employeeIDs = eSBProvisionHeadDTO.Employees.Any(y => y.Key == null) ? new List<long>(0) : eSBProvisionHeadDTO.Employees.Select(x => long.Parse(x.Key)).ToList();
            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                try
                {
                    var employees = new List<Entity.HR.Models.Employee>();

                    employees = (from e in dbContext.Employees
                                 join dt in dbContext.Departments1 on
                                 e.DepartmentID equals dt.DepartmentID
                                 where (e.IsEoSBEligible == true && (employeeIDs.Count == 0 || employeeIDs.Contains(e.EmployeeIID))
                                 && (departmentIDS.Count == 0 || departmentIDS.Contains(e.DepartmentID.Value)) && e.IsActive == true)
                                 select e).Include(x => x.CalendarType).Include(x => x.Departments1).AsNoTracking().ToList();
                    employeeIdlist = employees.Select(x => (x.EmployeeIID)).ToList();

                    if (employees.Count == 0)
                    {
                        operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                        operationresultDTO.Message = "No employees were found. Please check the eligibility and department selection!";
                        return operationresultDTO;
                    }

                    var startDate = new DateTime(eSBProvisionHeadDTO.EntryDate.Value.Year, eSBProvisionHeadDTO.EntryDate.Value.Month, 1);
                    var endDate = startDate.AddMonths(1);

                    var esbprovEntities = dbContext.EmployeeESBProvisionDetails
                        .Include(x => x.EmployeeESBProvisionHead)
                        .Where(ss => ss.EmployeeID.HasValue && employeeIdlist.Contains(ss.EmployeeID.Value)
                        && ss.EmployeeESBProvisionHead.EntryDate.HasValue
                        && ss.EmployeeESBProvisionHead.EntryDate >= startDate
                        && ss.EmployeeESBProvisionHead.EntryDate <= endDate)
                       .ToList();

                    if (esbprovEntities.Any())
                    {
                        if (eSBProvisionHeadDTO.IsRegenerate)
                        {
                            if (!esbprovEntities.Any(x => x.EmployeeESBProvisionHead.IsaccountPosted == true))
                            {

                                var headIdsToCheck = esbprovEntities
                                    .Select(e => e.EmployeeESBProvisionHeadID)
                                    .Distinct()
                                    .ToList();


                                dbContext.EmployeeESBProvisionDetails.RemoveRange(esbprovEntities);
                                dbContext.SaveChanges();


                                foreach (var headId in headIdsToCheck)
                                {
                                    bool hasOtherDetails = dbContext.EmployeeESBProvisionDetails
                                        .Any(d => d.EmployeeESBProvisionHeadID == headId);

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
                                esbprovEntities.Clear();
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
                            operationresultDTO.Message = "Already generated. Please regenerate the entries!";
                            return operationresultDTO;
                        }
                    }


                    var existList = (from e in dbContext.EmployeeESBProvisionHeads
                                     where e.EmployeeESBProvisionHeadIID == eSBProvisionHeadDTO.EmployeeESBProvisionHeadIID
                                     select e).AsNoTracking().FirstOrDefault();

                    if (existList != null && !string.IsNullOrEmpty(eSBProvisionHeadDTO.EntryNumber))
                    {
                        eSBProvisionHeadDTO.EntryNumber = eSBProvisionHeadDTO.EntryNumber;
                    }
                    var lstEmployees = employees.Select(x => x.EmployeeIID).ToList();

                    int? basicPayComponentID = new Domain.Setting.SettingBL(null).GetSettingValue<int?>("BASICPAY_ID");
                    var basicPayDets = (from e in dbContext.EmployeeSalaryStructureComponentMaps
                                        where (e.SalaryComponentID == basicPayComponentID
                                        && lstEmployees.Contains(e.EmployeeSalaryStructure.EmployeeID ?? 0)
                                        && e.EmployeeSalaryStructure.IsActive == true)
                                        select e).Include(x => x.EmployeeSalaryStructure).AsNoTracking().ToList();
                    if (basicPayDets != null && basicPayDets.Count() > 0)
                    {
                        List<long> employeeIds = employees.Select(x => (x.EmployeeIID)).ToList();
                        int? esbNoOfdays = new Domain.Setting.SettingBL(null).GetSettingValue<int?>("ESB_NOOF_DAYS_PER_YEAR");

                        eSBProvisionHeadDTO.SalaryComponentID = new Domain.Setting.SettingBL(null).GetSettingValue<int?>("END_OF_SERVICE_BENEFIT");
                        eSBProvisionHeadDTO.DocumentTypeID = new Domain.Setting.SettingBL(null).GetSettingValue<int?>("DOCUMENT_TYPE_ESB");
                        foreach (var employee in employees)
                        {
                            basicPay = 0;
                            var basicPayDts = basicPayDets.Where(x => x.EmployeeSalaryStructure.EmployeeID == employee.EmployeeIID).FirstOrDefault();
                            if (basicPayDts != null)
                            {
                                basicPay = basicPayDts.Amount ?? 0;
                            }
                            else
                                basicPay = 0;
                            if (basicPay != 0)
                            {
                                var esb = CalculateESB(eSBProvisionHeadDTO.EntryDate, employee.DateOfJoining, basicPay, esbNoOfdays);
                                var employeeESBProvisionDetailDTO = new EmployeeESBProvisionDetailDTO()
                                {
                                    EmployeeESBProvisionHeadID = eSBProvisionHeadDTO.EmployeeESBProvisionHeadIID,
                                    BasicSalary = basicPay ?? 0,
                                    Category = employee.CalendarType?.Description,
                                    Department = employee.Departments1.DepartmentName,
                                    EmployeeCode = employee.EmployeeCode,
                                    EmployeeName = employee.FirstName + " " + employee.MiddleName + " " + employee.LastName,
                                    DOJ = employee.DateOfJoining,
                                    NoofESBDays = esbNoOfdays ?? 30,
                                    ESBAmount = esb,
                                    EmployeeID = employee.EmployeeIID,

                                };
                                employeeESBProvisionDetailList.Add(employeeESBProvisionDetailDTO);
                            }
                        }

                        eSBProvisionHeadDTO.ESBProvisionDetails.AddRange(employeeESBProvisionDetailList);
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

                if (employeeESBProvisionDetailList.Count() == 0)
                {
                    operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                    operationresultDTO.Message = "Not generated. Please check eligibility and Leave salary structure settings!";
                    return operationresultDTO;
                }
                else
                {

                    operationresultDTO = SaveESBProvision(dbContext, GetPreviousData(dbContext, eSBProvisionHeadDTO, employeeIdlist));
                    return operationresultDTO;
                }
            }
        }
        private EmployeeESBProvisionHeadDTO GetPreviousData(dbEduegateHRContext dbContext, EmployeeESBProvisionHeadDTO toDto, List<long> employeeIdlist)
        {
            DateTime firstDayPrevMonth = new DateTime(toDto.EntryDate.Value.Year, toDto.EntryDate.Value.Month, 1).AddMonths(-1);
            DateTime lastDayPrevMonth = new DateTime(toDto.EntryDate.Value.Year, toDto.EntryDate.Value.Month, 1).AddDays(-1);

            var prevDataDict = dbContext.EmployeeESBProvisionDetails
                .Where(ss =>
                    ss.EmployeeID.HasValue &&
                    employeeIdlist.Contains(ss.EmployeeID.Value) &&
                    ss.EmployeeESBProvisionHead.EntryDate.HasValue &&
                    ss.EmployeeESBProvisionHead.EntryDate >= firstDayPrevMonth &&
                    ss.EmployeeESBProvisionHead.EntryDate <= lastDayPrevMonth)
                .Select(ss => new
                {
                    ss.EmployeeID,
                    PrevESBAmount = ss.ESBAmount
                })
                .AsEnumerable()
                .GroupBy(x => x.EmployeeID.Value)
                .ToDictionary(g => g.Key, g => g.First().PrevESBAmount ?? 0);

            if (prevDataDict.Count > 0)
            {
                foreach (var detail in toDto.ESBProvisionDetails)
                {
                    if (detail.EmployeeID.HasValue && prevDataDict.TryGetValue(detail.EmployeeID.Value, out var prevAmount))
                    {
                        detail.OpeningAmount = prevAmount;
                        detail.Balance = Math.Round(((detail.ESBAmount ?? 0) - prevAmount), 0);
                    }
                }
            }

            return toDto;
        }

        private OperationResultWithIDsDTO SaveESBProvision(dbEduegateHRContext dbContext, EmployeeESBProvisionHeadDTO toDto)
        {
            var operationresultDTO = new OperationResultWithIDsDTO();

            var entity = new EmployeeESBProvisionHead();
            var EntityemployeeESBProvisions = new List<EmployeeESBProvisionDetail>();
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
                        sequence = mutualRepository.GetNextSequence("ESBProvisionEntryNO", null);
                        toDto.EntryNumber = sequence.Prefix + sequence.LastSequence;
                    }
                }
                catch (Exception ex)
                {
                    operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                    operationresultDTO.Message = "Please generate sequence with 'ESBProvisionEntryNO'!";
                    return operationresultDTO;
                }

                entity = new EmployeeESBProvisionHead()
                {
                    BranchID = toDto.BranchID,
                    DocumentTypeID = toDto.DocumentTypeID,
                    SalaryComponentID = toDto.SalaryComponentID,
                    EntryNumber = toDto.EntryNumber,
                    EntryDate = toDto.EntryDate,
                    EmployeeESBProvisionHeadIID = toDto.EmployeeESBProvisionHeadIID,
                };
                if (toDto.EmployeeESBProvisionHeadIID == 0)
                {
                    entity.CreatedBy = (int)_context.LoginID;
                    entity.CreatedDate = DateTime.Now;
                }
                else
                {
                    entity.UpdatedBy = (int)_context.LoginID;
                    entity.UpdatedDate = DateTime.Now;
                }
                entity.EmployeeESBProvisionDetails = new List<EmployeeESBProvisionDetail>();
                foreach (var dat in toDto.ESBProvisionDetails)
                {
                    entity.EmployeeESBProvisionDetails.Add(new EmployeeESBProvisionDetail()
                    {
                        EmployeeESBProvisionDetailIID = dat.EmployeeESBProvisionDetailIID,
                        EmployeeID = dat.EmployeeID,
                        EmployeeESBProvisionHeadID = entity.EmployeeESBProvisionHeadIID,
                        BasicSalary = dat.BasicSalary,
                        ESBAmount = dat.ESBAmount,
                        NoofESBDays = dat.NoofESBDays,
                        OpeningAmount = dat.OpeningAmount,
                        Balance = dat.Balance
                    });
                }

                var IIDs = toDto.ESBProvisionDetails
                        .Select(a => a.EmployeeESBProvisionDetailIID).ToList();

                //delete maps
                var entities = dbContext.EmployeeESBProvisionDetails.Where(x =>
                    x.EmployeeESBProvisionHeadID == entity.EmployeeESBProvisionHeadIID &&
                    !IIDs.Contains(x.EmployeeESBProvisionDetailIID)).AsNoTracking().ToList();

                if (entities.IsNotNull() && entities.Count() > 0)
                    dbContext.EmployeeESBProvisionDetails.RemoveRange(entities);

                if (entity.EmployeeESBProvisionHeadIID == 0)
                {
                    dbContext.EmployeeESBProvisionHeads.Add(entity);

                    dbContext.Entry(entity).State = EntityState.Added;
                }
                else
                {
                    foreach (var map in entity.EmployeeESBProvisionDetails)
                    {
                        foreach (var ent in entities)
                        {
                            if (ent.EmployeeID == map.EmployeeID)
                            {
                                map.EmployeeESBProvisionDetailIID = ent.EmployeeESBProvisionDetailIID;
                            }
                            if (map.EmployeeESBProvisionDetailIID == 0)
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
                employeeList = toDto.ESBProvisionDetails.Select(x => x.EmployeeID).ToList();

                foreach (var det in entity.EmployeeESBProvisionDetails)
                {
                    foreach (var dtoDet in toDto.ESBProvisionDetails)
                    {
                        if (det.EmployeeID == dtoDet.EmployeeID)
                        {
                            dtoDet.EmployeeESBProvisionDetailIID = det.EmployeeESBProvisionDetailIID;
                            dtoDet.EmployeeESBProvisionHeadID = det.EmployeeESBProvisionHeadID;

                        }
                    }
                }

                toDto.EmployeeESBProvisionHeadIID = entity.EmployeeESBProvisionHeadIID;
                operationresultDTO.EmployeeESBProvisionHead = toDto;
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
        private decimal CalculateESB(DateTime? referenceDate, DateTime? doj, decimal? basicSalary, int? eSBDays)
        {

            if (doj == null || basicSalary <= 0 || eSBDays <= 0)
            {
                return 0;
            }
            double yearsWorked = (referenceDate.Value - doj.Value).TotalDays / 365.0;
            if (yearsWorked <= 0)
            {
                return 0;
            }
            decimal dailySalary = (basicSalary ?? 0) / 30;
            decimal ESB = Math.Round(dailySalary * (decimal)(yearsWorked * eSBDays), 0);
            return ESB;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<LSESBProvisionDetailDTO>(entity);
        }
        public override string ToDTOString(BaseMasterDTO dto)
        {
            return JsonConvert.SerializeObject(dto);
        }
        public override string GetEntity(long IID)
        {
            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                var entity = dbContext.EmployeeESBProvisionHeads.Where(X => X.EmployeeESBProvisionHeadIID == IID)
                    .Include(x => x.Branch)
                    .AsNoTracking()
                    .FirstOrDefault();

                var entityDetail = dbContext.EmployeeESBProvisionDetails.Where(X => X.EmployeeESBProvisionHeadID == IID)
                  .Include(x => x.Employee).ThenInclude(x => x.Departments1)
                  .Include(x => x.Employee).ThenInclude(x => x.CalendarType)
                  .AsNoTracking()
                  .ToList();
                var detail = new EmployeeESBProvisionHeadDTO()
                {
                    BranchID = entity.BranchID,
                    Branch = entity.BranchID.HasValue ? entity.Branch.BranchName : null,
                    DocumentTypeID = entity.DocumentTypeID,
                    SalaryComponentID = entity.SalaryComponentID,
                    EntryDate = entity.EntryDate,
                    EntryNumber = entity.EntryNumber,
                    EmployeeESBProvisionHeadIID = entity.EmployeeESBProvisionHeadIID
                };
                detail.ESBProvisionDetails = new List<EmployeeESBProvisionDetailDTO>();
                foreach (var edtl in entityDetail)
                {
                    var employeeESBProvisionDetailDTO = new EmployeeESBProvisionDetailDTO()
                    {
                        EmployeeESBProvisionHeadID = edtl.EmployeeESBProvisionHeadID,
                        EmployeeESBProvisionDetailIID = edtl.EmployeeESBProvisionDetailIID,
                        BasicSalary = edtl.BasicSalary ?? 0,
                        Category = edtl.Employee.CalendarType?.Description,
                        Department = edtl.Employee.Departments1.DepartmentName,
                        EmployeeCode = edtl.Employee.EmployeeCode,
                        EmployeeName = edtl.Employee.FirstName + " " + edtl.Employee.MiddleName + " " + edtl.Employee.LastName,
                        DOJ = edtl.Employee.DateOfJoining,
                        NoofESBDays = edtl.NoofESBDays,
                        LastLeaveSalaryDate = edtl.LastLeaveSalaryDate,
                        ESBAmount = edtl.ESBAmount,
                        EmployeeID = edtl.EmployeeID,
                        OpeningAmount = edtl.OpeningAmount,
                        Balance = edtl.Balance
                    };
                    detail.ESBProvisionDetails.Add(employeeESBProvisionDetailDTO);
                }

                return ToDTOString(detail);
            }
        }

        public string UpdateESBAccountPostStatus(EmployeeESBProvisionHeadDTO dto)
        {
            try
            {
                using (var dbContext = new dbEduegateHRContext())
                {
                    var employeeESBProvisionHeads = dbContext.EmployeeESBProvisionHeads.Where(m => m.EmployeeESBProvisionHeadIID == dto.EmployeeESBProvisionHeadIID).FirstOrDefault();
                    if (employeeESBProvisionHeads != null)
                    {
                        employeeESBProvisionHeads.IsaccountPosted = true;
                        employeeESBProvisionHeads.UpdatedBy = _context.LoginID.HasValue ? Convert.ToInt32(_context.LoginID) : (int?)null;
                        employeeESBProvisionHeads.UpdatedDate = DateTime.Now;
                        dbContext.Entry(employeeESBProvisionHeads).State = EntityState.Modified;
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
