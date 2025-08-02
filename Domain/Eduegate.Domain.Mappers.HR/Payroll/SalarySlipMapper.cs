using Eduegate.Domain.Entity.HR;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.HR.Payroll;
using System.Linq;
using Newtonsoft.Json;
using Eduegate.Framework;
using Eduegate.Domain.Repository;
using Eduegate.Domain.Entity.HR.Payroll;
using System;
using System.Collections.Generic;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.CommonDTO;
using Eduegate.Services.Contracts.Contents;
using Microsoft.EntityFrameworkCore;
using Eduegate.Domain.Entity.Contents;
using Microsoft.VisualBasic;
using System.Globalization;


namespace Eduegate.Domain.Mappers.HR.Payroll
{
    public class SalarySlipMapper : DTOEntityDynamicMapper
    {
        CallContext _callContext;
        public static SalarySlipMapper Mapper(CallContext context)
        {
            var mapper = new SalarySlipMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<SalarySlipDTO>(entity);
        }
        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }


        public override string SaveEntity(BaseMasterDTO dto)
        {
            var slipdto = dto as SalarySlipDTO;
            string message = string.Empty;
            //if (slipdto.Department.Count == 0 && slipdto.Employees.Count == 0)
            //{
            //    return "0#Please Select Department or employees!";
            //}


            //var departmentIDs = slipdto.Department.Any(y => y.Key == null) ? new List<long>(0) : slipdto.Department.Select(x => long.Parse(x.Key)).ToList();
            //var employeeIDs = slipdto.Employees.Any(y => y.Key == null) ? new List<long>(0) : slipdto.Employees.Select(x => long.Parse(x.Key)).ToList();

            //message = GenerateSalarySlipByDepartment(departmentIDs, slipdto.SlipDate, slipdto.IsRegenerate, employeeIDs);


            return message;
        }

        /// <summary>
        /// Update Contentfile ID
        /// </summary>
        /// <param name="salarySlipPDFInfo"></param>
        /// <returns></returns>
        public OperationResultWithIDsDTO UpdateSalarySlipPDF(List<ContentFileDTO> salarySlipPDFInfo)
        {
            var operationResultWithIDsDTO = new OperationResultWithIDsDTO();

            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                try
                {
                    var IIDs = salarySlipPDFInfo
                        .Select(a => a.ReferenceID).ToList();

                    var entities = dbContext.SalarySlips.Where(x =>
                        IIDs.Contains(x.SalarySlipIID)).ToList();
                    // var slipdate = entities.Select(x => x.SlipDate).Distinct();
                    //var employeeIDs = entities.Select(x => x.EmployeeID).Distinct();
                    //var allEntities = dbContext.SalarySlips.Where(x =>
                    //   slipdate.Contains(x.SlipDate) && employeeIDs.Contains(x.EmployeeID)).ToList();
                    if (salarySlipPDFInfo.Any())
                    {
                        salarySlipPDFInfo.All(x =>
                        {
                            var slipdate = entities.Select(z => z.SlipDate).Distinct();
                            var employeeId = entities.Where(s => s.SalarySlipIID == x.ReferenceID).Select(em => em.EmployeeID).FirstOrDefault();
                            var allEntities = dbContext.SalarySlips.Where(xy =>
                            slipdate.Contains(xy.SlipDate) && xy.EmployeeID == employeeId).ToList();
                            if (allEntities.Any())
                            {
                                allEntities.All(en =>
                                {
                                    en.ReportContentID = x.ContentFileIID;
                                    dbContext.Entry(en).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                                    return true;
                                });
                            }
                            return true;
                        });
                    }

                    dbContext.SaveChanges();
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
                    //var errorMessage = ex.Message;
                    var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                                       ? ex.InnerException?.Message : ex.Message;

                    Eduegate.Logger.LogHelper<string>.Fatal($". Error message: {errorMessage}", ex);
                }
            }

            operationResultWithIDsDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Success;
            operationResultWithIDsDTO.Message = "Salaryslip generated successfully!";
            return operationResultWithIDsDTO;
        }
        public OperationResultWithIDsDTO GenerateSalarySlip(BaseMasterDTO dto)
        {
            var slipdto = dto as SalarySlipDTO;
            string message = string.Empty;
            var operationResultWithIDsDTO = new OperationResultWithIDsDTO();
            var departmentIDs = slipdto.Department.Any(y => y.Key == null) ? new List<long>(0) : slipdto.Department.Select(x => long.Parse(x.Key)).ToList();
            var employeeIDs = slipdto.Employees.Any(y => y.Key == null) ? new List<long>(0) : slipdto.Employees.Select(x => long.Parse(x.Key)).ToList();
            operationResultWithIDsDTO = GenerateSalarySlipByDepartment(departmentIDs, slipdto.SlipDate, slipdto.IsRegenerate, employeeIDs, slipdto.IsGenerateLeaveOrVacationsalary);
            return operationResultWithIDsDTO;
        }
        private List<SalarySlipDTO> GenerateLeaveVacationSalarySlip(dbEduegateHRContext dbContext, List<SalarySlipDTO> salarySlipDTOs)
        {
            var operationresultDTO = new OperationResultWithIDsDTO();
            var salarySlipList = new List<SalarySlip>();
            var salarySlipIDList = new List<SalarySlipDTO>();
            foreach (var salaryslipData in salarySlipDTOs)
            {
                var entity = new SalarySlip()
                {
                    EmployeeID = salaryslipData.EmployeeID,
                    SlipDate = salaryslipData.SlipDate,
                    SalaryComponentID = salaryslipData.SalaryComponentID,
                    Amount = salaryslipData.Amount,
                    Description = salaryslipData.Description,
                    SalarySlipStatusID = salaryslipData.SalarySlipStatusID,
                    IsVerified = salaryslipData.IsVerified,
                    NoOfDays = salaryslipData.NoOfDays,
                    NoOfHours = salaryslipData.NoOfHours,
                    CreatedBy = (int)_context.LoginID,
                    UpdatedBy = (int)_context.LoginID,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                };
                salarySlipList.Add(entity);
            }

            dbContext.SalarySlips.AddRange(salarySlipList);
            dbContext.SaveChanges();
            var salarySlipDate = salarySlipList.Select(x => x.SlipDate).FirstOrDefault();
            var employeeList = salarySlipList.Select(x => x.EmployeeID).ToList();
            var salarySlipDTOwithIDs =
            from s in dbContext.SalarySlips
            join e in dbContext.Employees on s.EmployeeID equals e.EmployeeIID
            where employeeList.Contains(s.EmployeeID) && s.SlipDate.Value.Date == salarySlipDate.Value.Date
            group new { s, e } by s.EmployeeID into g
            select new SalarySlipDTO()
            {
                EmployeeID = g.Key,
                SalarySlipIID = g.Max(x => x.s.SalarySlipIID),
                EmployeeCode = g.First().e.EmployeeCode,
                BranchID = g.First().e.BranchID,
                SlipDate = g.First().s.SlipDate
            };
            salarySlipIDList.AddRange(salarySlipDTOwithIDs);
            return salarySlipIDList;
        }

        public OperationResultWithIDsDTO GenerateSalarySlipByDepartment(List<long> departmentIDS, DateTime? SlipDate, bool isRegenerate, List<long> employeeIDs, bool? IsGenerateLeaveOrVacationsalary)
        {
            MutualRepository mutualRepository = new MutualRepository();
            var settingBL = new Domain.Setting.SettingBL(_callContext);
            var operationresultDTO = new OperationResultWithIDsDTO();
            var SalaryEmpIdList = new List<long>();
            var salarySlipIDList = new List<SalarySlipDTO>();
            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                try
                {
                    var employees = new List<Entity.HR.Models.Employee>();

                    employees = (from e in dbContext.Employees
                                 join ess in dbContext.EmployeeSalaryStructures on e.EmployeeIID equals ess.EmployeeID
                                 join dt in dbContext.Departments1 on
                                 e.DepartmentID equals dt.DepartmentID
                                 where ess.IsActive == true && ((employeeIDs.Count == 0 || employeeIDs.Contains(e.EmployeeIID))
                                 && (departmentIDS.Count == 0 || departmentIDS.Contains(e.DepartmentID.Value)) && e.IsActive == true)
                                 select e).AsNoTracking().ToList();

                    var lstEmployees = employees.Select(x => x.EmployeeIID).ToList();

                    if (employees.Count > 0)
                    {
                        List<long> employeeIds = employees.Select(x => (x.EmployeeIID)).ToList();
                        int? leaveSalaryComp = new Domain.Setting.SettingBL(null).GetSettingValue<int?>("LEAVE_SALARY");
                        int? endOfSalaryBenefitComp = new Domain.Setting.SettingBL(null).GetSettingValue<int?>("END_OF_SERVICE_BENEFIT");

                        var salarySlipEntities = (from ss in dbContext.SalarySlips
                                                  where employeeIds.Contains(ss.EmployeeID.Value) && (ss.SlipDate.Value.Year == SlipDate.Value.Year
                                                  && ss.SlipDate.Value.Month == SlipDate.Value.Month)
                                                  select ss).AsNoTracking().ToList();
                        var eoSBssalarySlipEntities = (from ss in dbContext.SalarySlips
                                                       where employeeIds.Contains(ss.EmployeeID.Value)
                                                       && ss.SalaryComponentID == endOfSalaryBenefitComp
                                                       select ss).AsNoTracking().ToList();
                        var salarySlipApprovedID = settingBL.GetSettingValue<byte?>("SALARYSLIPAPPROVED_ID");
                        if (salarySlipApprovedID.IsNull())
                        {
                            operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                            operationresultDTO.Message = "There is no SALARYSLIPAPPROVED_ID in settings!";
                            return operationresultDTO;
                        }
                        var leaveStatusID = settingBL.GetSettingValue<byte?>("EMPLOYEE_LEAVE_STATUS_APPROVED");
                        if (!leaveStatusID.HasValue)
                        {
                            operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                            operationresultDTO.Message = "There is no EMPLOYEE_LEAVE_STATUS_APPROVED in settings!";
                            return operationresultDTO;
                        }

                        var annualLeaveTypeID = settingBL.GetSettingValue<int?>("ANNUAL_LEAVE_TYPE");


                        if (isRegenerate == true)
                        {
                            if (!salarySlipEntities.Any(x => x.SalarySlipStatusID == salarySlipApprovedID))
                            {
                                if (eoSBssalarySlipEntities.Any())
                                {
                                    var salarySlipsToDelete = salarySlipEntities
                                        .Where(slip => !eoSBssalarySlipEntities.Any(eoSlip =>
                                            eoSlip.EmployeeID == slip.EmployeeID &&
                                            eoSlip.SlipDate.Value.Year == slip.SlipDate.Value.Year &&
                                            eoSlip.SlipDate.Value.Month == slip.SlipDate.Value.Month))
                                        .ToList();

                                    if (salarySlipsToDelete.Any())
                                    {
                                        dbContext.SalarySlips.RemoveRange(salarySlipsToDelete);
                                        dbContext.SaveChanges();
                                    }
                                }
                                else
                                {
                                    dbContext.SalarySlips
                                           .RemoveRange(salarySlipEntities);
                                    dbContext.SaveChanges();
                                    salarySlipEntities.RemoveRange(0, salarySlipEntities.Count());
                                }

                            }
                            else
                            {
                                operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                                operationresultDTO.Message = "Cannot be regenerated.Already approved some salary slips!";
                                return operationresultDTO;

                            }
                        }
                        var listOFLeaveEmployees = new List<long?>();
                        // Leave/Vacation Salary generation
                        if (IsGenerateLeaveOrVacationsalary.HasValue && IsGenerateLeaveOrVacationsalary.Value == true)
                        {


                            var vacationLeaveTypeID = settingBL.GetSettingValue<int?>("VACATION_LEAVE_TYPE");

                            var salarySlipDTOList = new List<SalarySlipDTO>();
                            var employeeSettlementDTOList = new List<EmployeeSettlementDTO>();
                            var leaveStartingDateRows = dbContext.LeaveApplications
                            .Where(a => lstEmployees.Contains(a.EmployeeID.Value) &&
                             a.LeaveStatusID == leaveStatusID & (a.ToDate != null && a.ToDate.Value.Year == SlipDate.Value.Year) &&
                             (
                             (a.LeaveTypeID == annualLeaveTypeID &&
                             (
                             a.FromDate.Value.Month == SlipDate.Value.AddMonths(1).Month))
                             ||
                             (a.LeaveTypeID == vacationLeaveTypeID &&
                             SlipDate.Value.Month >= a.FromDate.Value.Month &&
                             a.ToDate.Value.Month >= SlipDate.Value.Month)))
                             .AsNoTracking().ToList();
                            if (leaveStartingDateRows.Count > 0)
                            {
                                foreach (var employee in leaveStartingDateRows)
                                {
                                    List<SalarySlip> empSalarySlip = salarySlipEntities.Where(x => x.EmployeeID == employee.EmployeeID).ToList();
                                    if (empSalarySlip.Count() == 0)//no salaryslip exist
                                    {
                                        listOFLeaveEmployees.Add(employee.EmployeeID);

                                        var OperationResultWithIDsDTO = EmployeeSettlementMapper.Mapper(_callContext).GetEmployeeDetailsToSettlement(
                                              new EmployeeSettlementDTO()
                                              {
                                                  EmployeeID = employee.EmployeeID,
                                                  SalaryCalculationDate = SlipDate.Value,
                                                  NoofDaysInTheMonth = null,
                                                  EmployeeSettlementTypeID = 1,//TODO retrieve from settings table
                                                  IsFromSalarySlipGeneration = true
                                              });

                                        if (OperationResultWithIDsDTO.operationResult != Framework.Contracts.Common.Enums.OperationResult.Error)
                                        {
                                            salarySlipDTOList.AddRange(OperationResultWithIDsDTO.EmployeeSettlementDTO.SalarySlipDTOs);
                                            employeeSettlementDTOList.Add(OperationResultWithIDsDTO.EmployeeSettlementDTO);
                                        }

                                    }
                                }
                                if (salarySlipDTOList.Count() > 0)
                                {

                                    salarySlipIDList = GenerateLeaveVacationSalarySlip(dbContext, salarySlipDTOList);

                                }
                                if (employeeSettlementDTOList.Count > 0)
                                {
                                    EmployeeSettlementMapper.Mapper(_callContext).SaveLeaveOrVacationSalarySettlements(employeeSettlementDTOList);
                                }
                            }
                        }
                        else//Normal Salary generation
                        {
                            var previousMonth = SlipDate.Value.AddMonths(-1).Month;
                            var previousYear = SlipDate.Value.AddMonths(-1).Year;
                            var previousDate = new DateTime(previousYear, previousMonth, DateTime.DaysInMonth(previousYear, previousMonth));
                            var PrevWorkDays = DateTime.DaysInMonth(previousYear, previousMonth);
                            var workingDays = DateTime.DaysInMonth(SlipDate.Value.Year, SlipDate.Value.Month);
                            DateTime? prevPayrollCuttofDate = null;
                            DateTime? payrollCuttofDate = null;

                            #region Retrieve data from settings 

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


                            #endregion

                            #region DayDataVariables

                            var totalhrsInaMonth = (from ss in dbContext.Employees
                                                    join c in dbContext.CalendarEntries on ss.AcademicCalendarID equals c.AcademicCalendarID
                                                    where c.CalendarDate.Value.Year == SlipDate.Value.Year
                                                    && c.CalendarDate.Value.Month == SlipDate.Value.Month
                                                    select (c.NoofHours)).ToList().Sum();

                            var prevMonthData = (from asms in dbContext.SchoolDateSettingMaps
                                                 where asms.PayrollCutoffDate.Value.Year == previousYear
                                                 && asms.PayrollCutoffDate.Value.Month == previousMonth
                                                 select asms).AsNoTracking().FirstOrDefault();


                            var currentMonthData = (from asm in dbContext.SchoolDateSettingMaps
                                                    where asm.PayrollCutoffDate.Value.Year == SlipDate.Value.Year
                                                && asm.PayrollCutoffDate.Value.Month == SlipDate.Value.Month
                                                    select asm).AsNoTracking().FirstOrDefault();

                            if (prevMonthData.IsNotNull())
                            {
                                prevPayrollCuttofDate = prevMonthData.PayrollCutoffDate ?? new DateTime(previousYear, previousMonth, 26);
                                PrevWorkDays = prevMonthData.TotalWorkingDays.HasValue ? prevMonthData.TotalWorkingDays.Value : PrevWorkDays;
                            }
                            else
                            {
                                operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                                operationresultDTO.Message = "Please set Payroll date settings for the month " + new DateTime(previousYear, previousMonth, 1).ToString("MMM"); ;
                                return operationresultDTO;
                            }
                            if (currentMonthData.IsNotNull())
                            {
                                payrollCuttofDate = currentMonthData.PayrollCutoffDate ?? new DateTime(SlipDate.Value.Year, SlipDate.Value.Month, 26);
                                workingDays = currentMonthData.TotalWorkingDays.HasValue ? currentMonthData.TotalWorkingDays.Value : workingDays;
                            }
                            else
                            {
                                operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                                operationresultDTO.Message = "Please set Payroll date settings for the month " + SlipDate.Value.ToString("MMM");
                                return operationresultDTO;
                            }


                            #endregion

                            #region Calculate -no. of functional Days
                            var functionalPeriods = (from ss in dbContext.FunctionalPeriods
                                                     where ((ss.FromDate.Year >= SlipDate.Value.Year && ss.FromDate.Month <= SlipDate.Value.Month)
                                                     || (ss.ToDate.Year <= SlipDate.Value.Year && ss.ToDate.Month <= SlipDate.Value.Month)
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
                                        else if (dt.Month == SlipDate.Value.Month && dt.Year == SlipDate.Value.Year)
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

                            var empLeaves = new List<Entity.HR.Leaves.LeaveApplication>();

                            empLeaves = (from ss in dbContext.LeaveApplications
                                         where employeeIds.Contains(ss.EmployeeID.Value) && ss.IsLeaveWithoutPay == true
                                         && ss.LeaveStatusID == leaveStatusID
                                         && ss.FromDate.Value.Date > prevPayrollCuttofDate.Value.Date
                                         && ss.ToDate.Value.Date <= payrollCuttofDate.Value.Date
                                         select ss).AsNoTracking().ToList();





                            var emplSalaryStrComp = (from esc in dbContext.EmployeeSalaryStructureComponentMaps
                                                     join ess in dbContext.EmployeeSalaryStructures on esc.EmployeeSalaryStructureID equals ess.EmployeeSalaryStructureIID
                                                     where ess.IsActive == true
                                                     select esc)
                                                     .Include(i => i.EmployeeSalaryStructure)
                                                     .Include(i => i.SalaryComponent)
                                                     .AsNoTracking().ToList();


                            foreach (var employee in employees)
                            {
                                if (!listOFLeaveEmployees.Any(x => x.Value == employee.EmployeeIID))
                                {
                                    List<SalarySlip> empSalarySlip = salarySlipEntities.Where(x => x.EmployeeID == employee.EmployeeIID).ToList();

                                    if (eoSBssalarySlipEntities.Count() > 0 && eoSBssalarySlipEntities.Any(x => x.EmployeeID == employee.EmployeeIID))
                                    {
                                        empSalarySlip.RemoveRange(0, empSalarySlip.Count());
                                        if (employees.Count() == 1)
                                        {
                                            operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                                            operationresultDTO.Message = "Cannot be generated.Salary slip generated through Final Settlement process!";
                                            return operationresultDTO;
                                        }
                                    }
                                    else if (empSalarySlip.Any(x => x.SalaryComponentID == leaveSalaryComp))
                                    {
                                        empSalarySlip.RemoveRange(0, empSalarySlip.Count());
                                        if (employees.Count() == 1)
                                        {
                                            operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                                            operationresultDTO.Message = "Cannot be generated.Leave Salary generated!";
                                            return operationresultDTO;
                                        }
                                    }
                                    List<EmployeeSalaryStructureComponentMap> lstemplSalaryStrEarningComp = emplSalaryStrComp.Where(y => y.EmployeeSalaryStructure.EmployeeID == employee.EmployeeIID && y.SalaryComponent.ComponentTypeID != 2).ToList();
                                    List<EmployeeSalaryStructureComponentMap> lstemplSalaryStrDedComp = emplSalaryStrComp.Where(y => y.EmployeeSalaryStructure.EmployeeID == employee.EmployeeIID && y.SalaryComponent.ComponentTypeID == 2).ToList();
                                    List<Entity.HR.Leaves.LeaveApplication> leaveApplications = empLeaves.Where(x => x.EmployeeID == employee.EmployeeIID).ToList();

                                    if (empSalarySlip.Count == 0 && (lstemplSalaryStrEarningComp.Count() > 0 || lstemplSalaryStrDedComp.Count() > 0))
                                    {
                                        var salarySlipDetailDTO = new SalarySlipDetailDTO()
                                        {
                                            EmployeeID = employee.EmployeeIID,
                                            ArrearComponentId = arrearComponentId,
                                            BasicPayComponentID = basicPayComponentID,
                                            DateOfJoining = employee.DateOfJoining,
                                            EmployeeSalaryStructEarnCompList = lstemplSalaryStrEarningComp,
                                            EmplSalaryStrDedCompList = lstemplSalaryStrDedComp,
                                            LeaveApplications = leaveApplications,
                                            TotalWorkingDays = workingDays,
                                            prevPayrollCuttofDate = prevPayrollCuttofDate.Value,
                                            PayrollCutoffDate = payrollCuttofDate.Value,
                                            NooffunctionalDays = nooffunctionalDays,
                                            PreviousDate = previousDate,
                                            PrevWorkDays = PrevWorkDays,
                                            NoofPreviousfunctionalDays = noofPreviousfunctionalDays,
                                            PendingStatusID = pendingStatusID,
                                            TimesheetStatusID = timesheetStatusID,
                                            NormalOTComponentID = NormalOTComponentID,
                                            SpecialOTComponentID = specialOTComponentID,
                                            TotalhrsInaMonth = totalhrsInaMonth,
                                            NormalRate = normalRateAmount,
                                            SpecialRate = specialRateAmount,
                                            SpecialOTTimeTypeID = specialOTTimeTypeID,
                                            NormalOTTimeTypeID = normalOTTimeTypeID,
                                            HrsPerDay = hrsPerDay,
                                            SlipDate = SlipDate.Value,
                                            DefaultDecimalNos = defaultDecimalNos,
                                            WithCUTOFDate = withCUTOFDateValue,
                                            TimeSheetStatusID = timesheetStatusID

                                        };

                                        var id = GenerateSalarySlipByEmployee(salarySlipDetailDTO, dbContext);
                                        if (id != 0)
                                            salarySlipIDList.Add(new SalarySlipDTO() { SalarySlipIID = id, BranchID = employee.BranchID, EmployeeCode = employee.EmployeeCode, SlipDate = SlipDate });

                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        //operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                        //operationresultDTO.Message = "No Employees are available in the selected department or Salary structure is not defined!";
                        //return operationresultDTO;
                        throw new Exception("No Employees are available in the selected department or Salary structure is not defined!");
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
                    var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                       ? ex.InnerException?.Message : ex.Message;
                    operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                    operationresultDTO.Message = errorMessage;
                    Eduegate.Logger.LogHelper<string>.Fatal($". Error message: {errorMessage}", ex);
                    return operationresultDTO;
                }

            }
            if (salarySlipIDList.Count() == 0)
            {
                operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Error;
                operationresultDTO.Message = "The salary slip was not generated due to an existing one, undefined salary components, or deductions exceeding earnings!";
                return operationresultDTO;
            }
            else
            {
                operationresultDTO.operationResult = Framework.Contracts.Common.Enums.OperationResult.Success;
                operationresultDTO.SalarySlipIDList = salarySlipIDList;
                operationresultDTO.Message = "Salaryslip generated successfully!";
                return operationresultDTO;
            }

        }


        private long GenerateSalarySlipByEmployee(SalarySlipDetailDTO salarySlipDetailDTO, dbEduegateHRContext dbContext)

        {
            DateTime? dateOfJoining = salarySlipDetailDTO.DateOfJoining;
            //DateTime? salarySlipLastDate = SlipDate;
            var salarySlipLastDate = new DateTime(salarySlipDetailDTO.SlipDate.Year, salarySlipDetailDTO.SlipDate.Month, DateTime.DaysInMonth(salarySlipDetailDTO.SlipDate.Year, salarySlipDetailDTO.SlipDate.Month));
            decimal? salaryPerDay = 0;
            decimal? totalSalary = 0;
            decimal? lopAmount = 0;
            decimal arrearAmt = 0;
            decimal? arrearDays = null;
            decimal? noOfLOPDays = null;
            String description = null;
            var noofleave = 0;
            var createSlip = new SalarySlip();
            var totalWorkDays = salarySlipDetailDTO.TotalWorkingDays;
            decimal? earningssum = 0;
            decimal? dedsum = 0;
            var listSalarySlip = new List<SalarySlip>();
            var listtempSalarySlip = new List<SalarySlip>();
            MutualRepository mutualRepository = new MutualRepository();
            var settingBL = new Domain.Setting.SettingBL(_callContext);
            var worksheetTimeData = new List<EmployeeTimeSheetApproval>();
            var listHeadIds = new List<long>();
            var listDetailIds = new List<long>();
            var listLoanHeadIds = new List<long>();
            var listloanDetailIds = new List<long>();

            var loanType = settingBL.GetSettingValue<int>("LOAN_TYPE");
            var salaryAdvanceType = settingBL.GetSettingValue<int>("SALARY_ADVANCE_TYPE");
            var approvedoanStatus = settingBL.GetSettingValue<int>("LOAN_STATUS_APPROVE");
            var loanComponentID = settingBL.GetSettingValue<int>("LOAN_COMPONENT");
            var advanceComponentID = settingBL.GetSettingValue<int>("ADVANCE_COMPONENT");
            var activeInstallmentstatus = settingBL.GetSettingValue<int>("LOAN_ACTIVE_INSTALLMENT_STATUS");
            var scheduleInstallmentstatus = settingBL.GetSettingValue<byte>("LOAN_SCHEDULE_INSTALLMENT_STATUS");
            var completdLoanstatus = settingBL.GetSettingValue<byte>("COMPLETED_LOAN_STATUS");
            var paidInstallmentstatus = settingBL.GetSettingValue<byte>("LOAN_PAID_INSTALLMENT_STATUS");
            var airfareAllowanceID = settingBL.GetSettingValue<int?>("AIRFARE_TICKET_ALLOWANCE_ID");
            var airfareLOPCompID = settingBL.GetSettingValue<int?>("AIRFARE_LOP_COMPONENT_ID");
            var airfareApprovalStatus = settingBL.GetSettingValue<int?>("AIRFARE_APPROVAL_STATUS");
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            try
            {
                if (salarySlipDetailDTO.PayrollCutoffDate > dateOfJoining)
                {

                    var basicAmount = salarySlipDetailDTO.EmployeeSalaryStructEarnCompList.Where(x => x.SalaryComponentID == salarySlipDetailDTO.BasicPayComponentID).Select(x => x.Amount).FirstOrDefault();

                    #region retrieve OT & Normal hours Data

                    if (salarySlipDetailDTO.WithCUTOFDate == 1)
                    {
                        worksheetTimeData = (from asm in dbContext.EmployeeTimeSheetApprovals
                                             where asm.EmployeeID == salarySlipDetailDTO.EmployeeID && asm.TimesheetApprovalStatusID == salarySlipDetailDTO.TimesheetStatusID &&
                                             asm.TimesheetDateFrom.Value.Date > salarySlipDetailDTO.prevPayrollCuttofDate.Date
                                             && asm.TimesheetDateTo.Value.Date <= salarySlipDetailDTO.PayrollCutoffDate.Date
                                             select asm).AsNoTracking().ToList();

                    }
                    else if (salarySlipDetailDTO.WithCUTOFDate == 2)//Calculate OT of the Previous month  
                    {
                        worksheetTimeData = (from asm in dbContext.EmployeeTimeSheetApprovals
                                             where asm.EmployeeID == salarySlipDetailDTO.EmployeeID && (asm.TimesheetDateFrom.Value.Year == salarySlipDetailDTO.prevPayrollCuttofDate.Year
                                             && asm.TimesheetDateFrom.Value.Month == salarySlipDetailDTO.prevPayrollCuttofDate.Month)
                                             && (asm.TimesheetDateTo.Value.Year == salarySlipDetailDTO.prevPayrollCuttofDate.Year
                                             && asm.TimesheetDateTo.Value.Month == salarySlipDetailDTO.prevPayrollCuttofDate.Month)
                                             && asm.TimesheetApprovalStatusID == salarySlipDetailDTO.TimesheetStatusID
                                             select asm).AsNoTracking().ToList();
                    }
                    else
                    {
                        worksheetTimeData = (from asm in dbContext.EmployeeTimeSheetApprovals
                                             where asm.EmployeeID == salarySlipDetailDTO.EmployeeID && (asm.TimesheetDateFrom.Value.Year == salarySlipDetailDTO.SlipDate.Year
                                             && asm.TimesheetDateFrom.Value.Month == salarySlipDetailDTO.SlipDate.Month)
                                             && (asm.TimesheetDateTo.Value.Year == salarySlipDetailDTO.SlipDate.Year
                                             && asm.TimesheetDateTo.Value.Month == salarySlipDetailDTO.SlipDate.Month)
                                             && asm.TimesheetApprovalStatusID == salarySlipDetailDTO.TimesheetStatusID
                                             select asm).AsNoTracking().ToList();
                    }
                    var normalHours = worksheetTimeData.Sum(x => x.NormalHours);

                    var normalOTHours = worksheetTimeData.Where(x => x.TimesheetTimeTypeID == salarySlipDetailDTO.NormalOTTimeTypeID).Select(x => x.OTHours).Sum();

                    var specialOTHrs = worksheetTimeData.Where(x => x.TimesheetTimeTypeID == salarySlipDetailDTO.SpecialOTTimeTypeID).Select(x => x.OTHours).Sum();

                    #endregion

                    #region retrieve Relareted DeductionComponents

                    var componentIDs = salarySlipDetailDTO.EmployeeSalaryStructEarnCompList.Select(x => x.SalaryComponentID);
                    var relatedComponents = (from asm in dbContext.SalaryComponentRelationMaps
                                             where componentIDs.Contains(asm.SalaryComponentID) && asm.RelationTypeID == 1
                                             select asm).AsNoTracking().ToList();

                    #endregion

                    #region Calculate -no. of Leaves
                    if (salarySlipDetailDTO.LeaveApplications.Any())
                    {
                        salarySlipDetailDTO.LeaveApplications.All(x =>
                        {
                            for (var dt = x.FromDate.Value.Date; dt <= x.ToDate.Value.Date; dt = dt.AddDays(1.0))
                            {
                                //if (dt.Month == salarySlipLastDate.Value.Month && dt.Year == salarySlipLastDate.Value.Year)
                                //{
                                noofleave = noofleave + 1;
                                //}
                            }
                            return true;
                        }
                        );
                    }
                    #endregion

                    if (salarySlipDetailDTO.EmployeeSalaryStructEarnCompList.Count() > 0)
                    {

                        foreach (var employeeSalaryComponents in salarySlipDetailDTO.EmployeeSalaryStructEarnCompList)
                        {

                            #region default Salary components Insert
                            createSlip = new SalarySlip()
                            {
                                SlipDate = salarySlipDetailDTO.SlipDate,
                                EmployeeID = salarySlipDetailDTO.EmployeeID,
                                SalaryComponentID = employeeSalaryComponents.SalaryComponentID,
                                Amount = ConevrtDecimalPoints(decimal.Round((employeeSalaryComponents.Amount ?? 0), MidpointRounding.AwayFromZero), salarySlipDetailDTO.DefaultDecimalNos),
                                Description = "Allowance Amount",
                                NoOfDays = totalWorkDays - noofleave,
                                NoOfHours = normalHours,
                                SalarySlipStatusID = salarySlipDetailDTO.PendingStatusID,
                                IsVerified = false
                            };

                            createSlip.CreatedBy = Convert.ToInt32(_context.LoginID);
                            createSlip.CreatedDate = DateTime.Now;
                            earningssum += createSlip.Amount;
                            listtempSalarySlip.Add(createSlip);//Add salarySlip into list
                            #endregion

                            var salaryComponentGroupID = employeeSalaryComponents.SalaryComponent.SalaryComponentGroupID;

                            #region Arrear calculation
                            if (salarySlipDetailDTO.PreviousDate.Month == dateOfJoining.Value.Month && salarySlipDetailDTO.PreviousDate.Year == dateOfJoining.Value.Year
                                && dateOfJoining.Value.Date > salarySlipDetailDTO.prevPayrollCuttofDate)
                            {

                                if (salaryComponentGroupID == 3 && salarySlipDetailDTO.NoofPreviousfunctionalDays > 0)//Functional Allowance
                                {
                                    salaryPerDay = (employeeSalaryComponents.Amount.Value) / salarySlipDetailDTO.PrevWorkDays;
                                    totalSalary = salaryPerDay * salarySlipDetailDTO.NoofPreviousfunctionalDays;
                                    arrearAmt += (employeeSalaryComponents.Amount ?? 0);
                                    arrearDays = salarySlipDetailDTO.NoofPreviousfunctionalDays;
                                }
                                else // (Fixed Allowance or Variable allowance)
                                {
                                    salaryPerDay = (employeeSalaryComponents.Amount.Value) / salarySlipDetailDTO.PrevWorkDays;
                                    int? workingDays = (int?)(salarySlipDetailDTO.PreviousDate - dateOfJoining)?.TotalDays + 1;
                                    decimal? totalSal = (salaryPerDay ?? 0) * workingDays;
                                    arrearAmt += (totalSal ?? 0);
                                    arrearDays = workingDays;
                                }
                            }
                            #endregion
                            if (salaryComponentGroupID == 2)//Variable allowance
                            {

                                if (salarySlipLastDate.Year == dateOfJoining.Value.Year && salarySlipLastDate.Month == dateOfJoining.Value.Month
                                    && dateOfJoining.Value.Date <= salarySlipDetailDTO.PayrollCutoffDate)
                                {
                                    salaryPerDay = (employeeSalaryComponents.Amount.Value) / totalWorkDays;
                                    int? workingDays = (int?)(salarySlipLastDate - dateOfJoining)?.TotalDays + 1;
                                    int LOPDays = (totalWorkDays) - (workingDays ?? 0) + noofleave;
                                    noOfLOPDays = LOPDays;
                                    lopAmount = salaryPerDay * LOPDays;
                                    description = @"Calcualted Lop from Total Days : " + totalWorkDays + "Days Worked : " + (totalWorkDays - LOPDays) +
                                        " No. of Leaves : " + noofleave;
                                }
                                else
                                {
                                    if (noofleave > 0)
                                    {
                                        salaryPerDay = (employeeSalaryComponents.Amount.Value) / totalWorkDays;
                                        lopAmount = salaryPerDay * noofleave;
                                        description = @"Calcualted Lop from Total Days : " + totalWorkDays +
                                            " No. of Leaves : " + noofleave;
                                        noOfLOPDays = noofleave;
                                    }
                                    else
                                        lopAmount = null;
                                }
                                if (lopAmount.IsNotNull())
                                {
                                    lopAmount = lopAmount > 0 ? -1 * lopAmount : lopAmount;
                                    createSlip = new SalarySlip()
                                    {
                                        SlipDate = salarySlipDetailDTO.SlipDate,
                                        EmployeeID = salarySlipDetailDTO.EmployeeID,
                                        Description = description,
                                        NoOfDays = noOfLOPDays,
                                        NoOfHours = normalHours,
                                        IsVerified = false,
                                        SalarySlipStatusID = salarySlipDetailDTO.PendingStatusID,
                                        SalaryComponentID = relatedComponents.Where(x => x.SalaryComponentID == employeeSalaryComponents.SalaryComponentID).
                                        Select(y => y.RelatedComponentID).FirstOrDefault(),
                                        Amount = ConevrtDecimalPoints(decimal.Round((lopAmount ?? 0), MidpointRounding.AwayFromZero), salarySlipDetailDTO.DefaultDecimalNos)
                                    };
                                    createSlip.CreatedBy = Convert.ToInt32(_context.LoginID);
                                    createSlip.CreatedDate = DateTime.Now;
                                    dedsum += createSlip.Amount;
                                    listtempSalarySlip.Add(createSlip);//Add salarySlip into list
                                }
                            }
                            else if (salaryComponentGroupID == 3 && salarySlipDetailDTO.NooffunctionalDays > 0)//Functional Allowance
                            {

                                salaryPerDay = (employeeSalaryComponents.Amount.Value) / totalWorkDays;
                                int LOPDays = (totalWorkDays) - salarySlipDetailDTO.NooffunctionalDays + noofleave;
                                lopAmount = salaryPerDay * LOPDays;
                                description = @"Calcualted Lop from WorkingDays : " + totalWorkDays + " No. of Leaves : " + noofleave +
                                    "NooffunctionalDays : " + salarySlipDetailDTO.NooffunctionalDays;
                                lopAmount = lopAmount > 0 ? -1 * lopAmount : lopAmount;
                                noOfLOPDays = LOPDays;
                                createSlip = new SalarySlip()
                                {
                                    SlipDate = salarySlipDetailDTO.SlipDate,
                                    EmployeeID = salarySlipDetailDTO.EmployeeID,
                                    NoOfDays = noOfLOPDays,
                                    NoOfHours = normalHours,
                                    SalarySlipStatusID = salarySlipDetailDTO.PendingStatusID,
                                    IsVerified = false,
                                    SalaryComponentID = relatedComponents.Where(x => x.SalaryComponentID == employeeSalaryComponents.SalaryComponentID).
                                        Select(y => y.RelatedComponentID).FirstOrDefault(),
                                    Amount = ConevrtDecimalPoints(decimal.Round((lopAmount ?? 0), MidpointRounding.AwayFromZero), salarySlipDetailDTO.DefaultDecimalNos),
                                    Description = description

                                };
                                createSlip.CreatedBy = Convert.ToInt32(_context.LoginID);
                                createSlip.CreatedDate = DateTime.Now;
                                dedsum += createSlip.Amount;
                                listtempSalarySlip.Add(createSlip);//Add salarySlip into list

                            }
                        } //
                        #region insert AREAR Data 
                        if (arrearAmt > 0)
                        {
                            createSlip = new SalarySlip()
                            {
                                SlipDate = salarySlipDetailDTO.SlipDate,
                                EmployeeID = salarySlipDetailDTO.EmployeeID,
                                NoOfDays = arrearDays,
                                NoOfHours = normalHours,
                                IsVerified = false,
                                SalarySlipStatusID = salarySlipDetailDTO.PendingStatusID,
                                SalaryComponentID = int.Parse(salarySlipDetailDTO.ArrearComponentId),
                                Amount = ConevrtDecimalPoints(decimal.Round(arrearAmt, MidpointRounding.AwayFromZero), salarySlipDetailDTO.DefaultDecimalNos),
                                Description = "Calculated Arrears from Previous Month Working Days : " + salarySlipDetailDTO.PrevWorkDays
                            };
                            createSlip.CreatedBy = Convert.ToInt32(_context.LoginID);
                            createSlip.CreatedDate = DateTime.Now;
                            earningssum += createSlip.Amount;
                            listtempSalarySlip.Add(createSlip);//Add salarySlip into list
                        }
                        #endregion


                    }//
                     //Deduction Components
                    if (salarySlipDetailDTO.EmplSalaryStrDedCompList.Count() > 0)
                    {
                        foreach (var employeeSalaryComponents in salarySlipDetailDTO.EmplSalaryStrDedCompList)
                        {
                            if (!listtempSalarySlip.Any(x => x.SalaryComponentID == employeeSalaryComponents.SalaryComponentID))
                            {
                                totalSalary = employeeSalaryComponents.Amount.Value > 0 ? -1 * employeeSalaryComponents.Amount.Value : employeeSalaryComponents.Amount.Value;
                                createSlip = new SalarySlip()
                                {
                                    SlipDate = salarySlipDetailDTO.SlipDate,
                                    EmployeeID = salarySlipDetailDTO.EmployeeID,
                                    NoOfDays = totalWorkDays - noofleave,
                                    NoOfHours = normalHours,
                                    SalaryComponentID = employeeSalaryComponents.SalaryComponentID,
                                    Amount = ConevrtDecimalPoints(decimal.Round((totalSalary ?? 0), MidpointRounding.AwayFromZero), salarySlipDetailDTO.DefaultDecimalNos),
                                    IsVerified = false,
                                    SalarySlipStatusID = salarySlipDetailDTO.PendingStatusID,
                                    Description = "Normal Deduction "
                                };
                                createSlip.CreatedBy = Convert.ToInt32(_context.LoginID);
                                createSlip.CreatedDate = DateTime.Now;
                                dedsum += createSlip.Amount;
                                listtempSalarySlip.Add(createSlip);
                            }
                        }
                    }

                    #region insert OT Data
                    if (normalOTHours > 0)
                    {

                        var hourlyRate = (((basicAmount / salarySlipDetailDTO.PrevWorkDays) / salarySlipDetailDTO.HrsPerDay) ?? 0);
                        var normalOTAmount = (((hourlyRate * normalOTHours) * salarySlipDetailDTO.NormalRate) ?? 0);

                        createSlip = new SalarySlip()
                        {
                            SlipDate = salarySlipDetailDTO.SlipDate,
                            EmployeeID = salarySlipDetailDTO.EmployeeID,
                            SalaryComponentID = salarySlipDetailDTO.NormalOTComponentID,
                            Amount = ConevrtDecimalPoints(decimal.Round(normalOTAmount, MidpointRounding.AwayFromZero), salarySlipDetailDTO.DefaultDecimalNos),
                            NoOfHours = normalOTHours,
                            NoOfDays = totalWorkDays - noofleave,
                            SalarySlipStatusID = salarySlipDetailDTO.PendingStatusID,
                            IsVerified = false,
                            Description = @"Calcualted Normal OT for Basic Amount : " + basicAmount + " TotalWorkingDays :" + salarySlipDetailDTO.PrevWorkDays + " hrsPerDay :" + salarySlipDetailDTO.HrsPerDay +
                             " Normal OT Hours : " + normalOTHours +
                            " Normal OT Rate :'" + salarySlipDetailDTO.NormalRate + "'"
                        };
                        createSlip.CreatedBy = Convert.ToInt32(_context.LoginID);
                        createSlip.CreatedDate = DateTime.Now;
                        earningssum += createSlip.Amount;
                        listtempSalarySlip.Add(createSlip);//Add salarySlip into list
                    }
                    if (specialOTHrs > 0)
                    {


                        var hourlyRate = (((basicAmount / salarySlipDetailDTO.PrevWorkDays) / salarySlipDetailDTO.HrsPerDay) ?? 0);
                        var specialOTAmount = (((hourlyRate * specialOTHrs) * salarySlipDetailDTO.SpecialRate) ?? 0);

                        createSlip = new SalarySlip()
                        {
                            SlipDate = salarySlipDetailDTO.SlipDate,
                            EmployeeID = salarySlipDetailDTO.EmployeeID,
                            SalaryComponentID = salarySlipDetailDTO.SpecialOTComponentID,
                            Amount = ConevrtDecimalPoints(decimal.Round(specialOTAmount, MidpointRounding.AwayFromZero), salarySlipDetailDTO.DefaultDecimalNos),
                            NoOfHours = specialOTHrs,
                            NoOfDays = totalWorkDays - noofleave,
                            SalarySlipStatusID = salarySlipDetailDTO.PendingStatusID,
                            IsVerified = false,
                            Description = @"Calcualted Special OT for Basic Amount : " + basicAmount + " TotalWorkingDays :" + salarySlipDetailDTO.PrevWorkDays + " hrsPerDay :" + salarySlipDetailDTO.HrsPerDay +
                                " Special OT Hours : " + specialOTHrs +
                                " Special OT Rate :'" + salarySlipDetailDTO.SpecialRate + "'"
                        };
                        createSlip.CreatedBy = Convert.ToInt32(_context.LoginID);
                        createSlip.CreatedDate = DateTime.Now;
                        earningssum += createSlip.Amount;
                        listtempSalarySlip.Add(createSlip);//Add salarySlip into list
                    }

                    #endregion

                    #region Loan

                    var loanData = (from lh in dbContext.LoanHeads

                                    where lh.EmployeeID == salarySlipDetailDTO.EmployeeID
                                    && lh.LoanTypeID == loanType
                                    && lh.LoanStatusID == approvedoanStatus
                                    select lh)
                                    .Include(i => i.LoanDetails)
                                    .AsNoTracking().ToList();


                    listLoanHeadIds = loanData.Select(x => x.LoanHeadIID).Distinct().ToList();
                    var loanDetail = (from eld in dbContext.LoanDetails
                                      where listLoanHeadIds.Contains(eld.LoanHeadID.Value)
                                      && (eld.InstallmentDate.Value.Year == salarySlipDetailDTO.SlipDate.Year
                                      && eld.InstallmentDate.Value.Month == salarySlipDetailDTO.SlipDate.Month)
                                      && (eld.LoanEntryStatusID == activeInstallmentstatus || eld.LoanEntryStatusID == scheduleInstallmentstatus)
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
                        createSlip = new SalarySlip()
                        {
                            SlipDate = salarySlipDetailDTO.SlipDate,
                            EmployeeID = salarySlipDetailDTO.EmployeeID,
                            SalaryComponentID = loanComponentID,
                            Amount = ConevrtDecimalPoints(decimal.Round(installmentAmount, MidpointRounding.AwayFromZero), salarySlipDetailDTO.DefaultDecimalNos),
                            NoOfDays = totalWorkDays - noofleave,
                            SalarySlipStatusID = salarySlipDetailDTO.PendingStatusID,
                            IsVerified = false,
                            Description = description

                        };
                        createSlip.CreatedBy = Convert.ToInt32(_context.LoginID);
                        createSlip.CreatedDate = DateTime.Now;
                        dedsum += createSlip.Amount;
                        listtempSalarySlip.Add(createSlip);//Add salarySlip into list
                    }
                    #endregion Loan

                    #region Airfare Allowance

                    var airfareData = (from t in dbContext.TicketEntitilementEntries
                                       where t.EmployeeID == salarySlipDetailDTO.EmployeeID
                                       && (t.TicketIssueDate.Value.Year == salarySlipDetailDTO.SlipDate.Year
                                       && t.TicketIssueDate.Value.Month == salarySlipDetailDTO.SlipDate.Month)
                                       && t.TicketEntitlementEntryStatusID == airfareApprovalStatus
                                       select t)
                                    .AsNoTracking().FirstOrDefault();

                    description = "";
                    decimal airfareAMount = 0;
                    if (airfareData != null && airfareData.TicketfarePayable.HasValue)
                    {
                        if (airfareData.IsTicketFareReimbursed == true)
                        {
                            description = "";
                            airfareAMount = (airfareData.TicketfarePayable ?? 0) > (airfareData.TicketIssuedOrFareReimbursed ?? 0) ? (airfareData.TicketIssuedOrFareReimbursed ?? 0) : (airfareData.TicketfarePayable ?? 0);
                            var IsTicketFareReimbursed = airfareData.IsTicketFareReimbursed == true ? "Yes" : "No";
                            description = "Travel Return Airfare: " + airfareData.TravelReturnAirfare + " Travel Sector: " + airfareData.GenerateTravelSector + " Ticket Entilement: " + airfareData.TicketEntitilementPer + "Ticket Fare Payable :" + airfareData.TicketfarePayable +
                            " Ticket Fare Reimbursed :" + airfareData.TicketIssuedOrFareReimbursed +
                            " Vacation Start Date : " + Convert.ToDateTime(airfareData.VacationStartingDate).ToString(dateFormat, CultureInfo.InvariantCulture) +
                            " Is Ticket Fare Reimbursed: " + IsTicketFareReimbursed + "";
                            //" Balance to be carried forward: " + airfareData.BalanceCarriedForwardPer + " Balance ticket amount payable : " + airfareData.BalanceTicketAmountPayable + "";

                            createSlip = new SalarySlip()
                            {
                                SlipDate = salarySlipDetailDTO.SlipDate,
                                EmployeeID = salarySlipDetailDTO.EmployeeID,
                                SalaryComponentID = airfareAllowanceID,
                                Amount = ConevrtDecimalPoints(decimal.Round(airfareAMount, MidpointRounding.AwayFromZero), salarySlipDetailDTO.DefaultDecimalNos),
                                NoOfDays = totalWorkDays - noofleave,
                                SalarySlipStatusID = salarySlipDetailDTO.PendingStatusID,
                                IsVerified = false,
                                Description = description
                            };
                            createSlip.CreatedBy = Convert.ToInt32(_context.LoginID);
                            createSlip.CreatedDate = DateTime.Now;
                            earningssum += createSlip.Amount;
                            listtempSalarySlip.Add(createSlip);//Add salarySlip into list
                        }
                        else
                        {
                            description = "";
                            airfareAMount = (airfareData.BalanceTicketAmountPayable ?? 0);

                            if (airfareAMount > 0)
                            {
                                var isTicketIssued = airfareData.IsTicketFareIssued == true ? "Yes" : "No";
                                description = "Travel Return Airfare: " + airfareData.TravelReturnAirfare + " Travel Sector: " + airfareData.GenerateTravelSector + " Ticket Entilement: " + airfareData.TicketEntitilementPer + " Ticket Fare Issued: " + airfareData.TicketIssuedOrFareReimbursed +
                                " Vacation Start Date : " + Convert.ToDateTime(airfareData.VacationStartingDate).ToString(dateFormat, CultureInfo.InvariantCulture) + " Is Ticket Issue: " + isTicketIssued +
                                " Balance to be carried forward: " + airfareData.BalanceCarriedForwardPer + " Balance ticket amount payable : " + airfareData.BalanceTicketAmountPayable + "";

                                createSlip = new SalarySlip()
                                {
                                    SlipDate = salarySlipDetailDTO.SlipDate,
                                    EmployeeID = salarySlipDetailDTO.EmployeeID,
                                    SalaryComponentID = airfareAllowanceID,
                                    Amount = ConevrtDecimalPoints(decimal.Round((airfareData.TicketfarePayable ?? 0), MidpointRounding.AwayFromZero), salarySlipDetailDTO.DefaultDecimalNos),
                                    NoOfDays = totalWorkDays - noofleave,
                                    SalarySlipStatusID = salarySlipDetailDTO.PendingStatusID,
                                    IsVerified = false,
                                    Description = description
                                };
                                createSlip.CreatedBy = Convert.ToInt32(_context.LoginID);
                                createSlip.CreatedDate = DateTime.Now;
                                earningssum += createSlip.Amount;
                                listtempSalarySlip.Add(createSlip);//Add salarySlip into list
                                var dedAirfareAllowanceAmount = airfareData.TicketIssuedOrFareReimbursed > 0 ? -1 * airfareData.TicketIssuedOrFareReimbursed : airfareData.TicketIssuedOrFareReimbursed;
                                createSlip = new SalarySlip()
                                {
                                    SlipDate = salarySlipDetailDTO.SlipDate,
                                    EmployeeID = salarySlipDetailDTO.EmployeeID,
                                    SalaryComponentID = airfareLOPCompID,
                                    Amount = ConevrtDecimalPoints(decimal.Round((dedAirfareAllowanceAmount ?? 0), MidpointRounding.AwayFromZero), salarySlipDetailDTO.DefaultDecimalNos),
                                    NoOfDays = totalWorkDays - noofleave,
                                    SalarySlipStatusID = salarySlipDetailDTO.PendingStatusID,
                                    IsVerified = false,
                                    Description = description
                                };
                                createSlip.CreatedBy = Convert.ToInt32(_context.LoginID);
                                createSlip.CreatedDate = DateTime.Now;
                                dedsum += createSlip.Amount;
                                listtempSalarySlip.Add(createSlip);//Add salarySlip into list
                            }
                        }
                    }
                    #endregion Airfare Allowance

                    #region Advance


                    var advanceData = (from lh in dbContext.LoanHeads
                                       where lh.EmployeeID == salarySlipDetailDTO.EmployeeID
                                       && lh.LoanTypeID == salaryAdvanceType
                                       && lh.LoanStatusID == approvedoanStatus
                                       select lh)
                                 .Include(i => i.LoanDetails)
                                 .AsNoTracking().ToList();


                    listHeadIds = advanceData.Select(x => x.LoanHeadIID).Distinct().ToList();
                    var advanceDetail = (from eld in dbContext.LoanDetails
                                         where listHeadIds.Contains(eld.LoanHeadID.Value)
                                         && (eld.InstallmentDate.Value.Year == salarySlipDetailDTO.SlipDate.Year
                                         && eld.InstallmentDate.Value.Month == salarySlipDetailDTO.SlipDate.Month)
                                         && (eld.LoanEntryStatusID == activeInstallmentstatus || eld.LoanEntryStatusID == scheduleInstallmentstatus)
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
                            var balance = advanceAmount - paidAmount;
                            advanceSumAMount += balance;
                            description = description + "Advance No." + advance.LoanHead.LoanNo + "Advance Amount :" + advanceAmount + " Installment Amount: " + advanceSumAMount + " Paid Amount:" + paidAmount + "Balance:" + balance + "";
                        }
                        var installmentAmount = advanceSumAMount > 0 ? -1 * advanceSumAMount : advanceSumAMount;
                        createSlip = new SalarySlip()
                        {
                            SlipDate = salarySlipDetailDTO.SlipDate,
                            EmployeeID = salarySlipDetailDTO.EmployeeID,
                            SalaryComponentID = advanceComponentID,
                            Amount = ConevrtDecimalPoints(decimal.Round(installmentAmount, MidpointRounding.AwayFromZero), salarySlipDetailDTO.DefaultDecimalNos),
                            NoOfDays = totalWorkDays - noofleave,
                            SalarySlipStatusID = salarySlipDetailDTO.PendingStatusID,
                            IsVerified = false,
                            Description = description

                        };
                        createSlip.CreatedBy = Convert.ToInt32(_context.LoginID);
                        createSlip.CreatedDate = DateTime.Now;
                        dedsum += createSlip.Amount;
                        listtempSalarySlip.Add(createSlip);//Add salarySlip into list
                    }
                    #endregion Advance
                }

                if (earningssum > Math.Abs((dedsum ?? 0)))
                {
                    dbContext.SalarySlips.AddRange(listtempSalarySlip);

                    dbContext.SaveChanges();


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

                    if (listloanDetailIds.Any())
                    {
                        listloanDetailIds
                            .All(w =>
                            {
                                var dRepLoanDetails = dbContext.LoanDetails.Where(x => x.LoanDetailID == w).AsNoTracking().FirstOrDefault();
                                if (dRepLoanDetails != null)
                                {

                                    dRepLoanDetails.LoanEntryStatusID = scheduleInstallmentstatus;
                                    dbContext.Entry(dRepLoanDetails).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                    dbContext.SaveChanges();
                                }
                                return true;
                            });
                    }

                    var slipMaxID = dbContext.SalarySlips.Max(s => (long?)s.SalarySlipIID);

                    return Convert.ToInt64(slipMaxID == null ? 1 : slipMaxID.Value);
                }
                else
                { return 0; }
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<SalarySlipMapper>.Fatal(ex.Message, ex);
                throw ex;
            }
        }

        private decimal ConevrtDecimalPoints(decimal amount, int defaultDecimalNos)
        {
            //return decimal.Parse(String.Format("{0:0.{1}f}", amount, defaultDecimalNos));  

            return decimal.Parse(amount.ToString("0.000"));
        }

        public decimal? GetTotalSalaryAmount(long employeeID, DateTime? loanDate)
        {
            decimal salaryAmount = 0;
            using (var dbContext = new dbEduegateHRContext())
            {
                var emplSalaryStrComp = (from esc in dbContext.EmployeeSalaryStructureComponentMaps
                                         join ess in dbContext.EmployeeSalaryStructures on esc.EmployeeSalaryStructureID equals ess.EmployeeSalaryStructureIID
                                         where ess.EmployeeID == employeeID && ess.IsActive == true
                                         select esc)
                                         .Include(i => i.SalaryComponent)
                                         .AsNoTracking().ToList();
                var emplSalaryStrEarningComp = emplSalaryStrComp.Where(y => y.SalaryComponent.ComponentTypeID != 2).Sum(x => x.Amount ?? 0);
                var emplSalaryStrDedComp = emplSalaryStrComp.Where(y => y.SalaryComponent.ComponentTypeID == 2).Sum(x => x.Amount ?? 0);

                salaryAmount = emplSalaryStrEarningComp - emplSalaryStrDedComp;
            }
            return salaryAmount;
        }

        private class EmployeeSalaryStructureCompDTO
        {
            public EmployeeSalaryStructureCompDTO()
            {
                SalaryComponent = new KeyValueDTO();
            }

            public long? EmployeeID { get; set; }

            public long EmployeeSalaryStructureComponentMapIID { get; set; }


            public long? EmployeeSalaryStructureID { get; set; }


            public int? SalaryComponentID { get; set; }
            public byte? SalaryComponentTypeID { get; set; }


            public byte? SalaryComponentGroupID { get; set; }

            public KeyValueDTO SalaryComponent { get; set; }


            public decimal? Amount { get; set; }


            public string Formula { get; set; }
        }

        public SalarySlipDTO GetSalarySlipList(long? employeeID, int Month, int Year)
        {
            SalarySlipDTO sRetData = new SalarySlipDTO();
            using (var sContext = new dbEduegateHRContext())
            {

                // Calculate the start and end dates of the selected month
                DateTime monthStart = new DateTime(Year, Month, 1);
                DateTime monthEnd = monthStart.AddMonths(1).AddDays(-1);

                var salarySlipData = (from s in sContext.SalarySlips
                                      where s.EmployeeID == employeeID
                                      && s.SlipDate >= monthStart
                                      && s.SlipDate <= monthEnd
                                      && s.SalarySlipStatusID == 2

                                      select new SalarySlipDTO
                                      {
                                          SalarySlipIID = s.SalarySlipIID,
                                          SlipDate = s.SlipDate,
                                          EmployeeID = s.EmployeeID,
                                          SalarySlipStatusID = s.SalarySlipStatusID,
                                          ReportContentID = s.ReportContentID,
                                      }).AsNoTracking().FirstOrDefault();

                // Get the report content data for the salary slips
                if (salarySlipData != null)
                {
                    // Get the report content data for the salary slips
                    var dFndReportData = GetReportContentData(salarySlipData);
                    return dFndReportData ?? salarySlipData;
                }
                else
                {
                    // Handle case where no salary slip data is found
                    return null;
                }
                //return salarySlipData;
            }
        }

        private SalarySlipDTO GetReportContentData(SalarySlipDTO salarySlipDataList)
        {
            SalarySlipDTO dFndReportData = new SalarySlipDTO();
            using (var sContext = new dbContentContext())
            {
                var reportData = (from n in sContext.ContentFiles
                                  where n.ContentFileIID == salarySlipDataList.ReportContentID
                                  select new SalarySlipDTO
                                  {
                                      SalarySlipIID = salarySlipDataList.SalarySlipIID,
                                      SlipDate = salarySlipDataList.SlipDate,
                                      EmployeeID = salarySlipDataList.EmployeeID,
                                      ReportContentID = n.ContentFileIID,

                                  }).AsNoTracking().FirstOrDefault();

                return reportData;

            }
        }

        public class SalarySlipDetailDTO
        {
            public SalarySlipDetailDTO()
            {

                EmployeeSalaryStructEarnCompList = new List<EmployeeSalaryStructureComponentMap>();
                EmplSalaryStrDedCompList = new List<EmployeeSalaryStructureComponentMap>();
                LeaveApplications = new List<Entity.HR.Leaves.LeaveApplication>();
            }


            public long? EmployeeID { get; set; }

            public DateTime SlipDate { get; set; }

            public DateTime? DateOfJoining { get; set; }

            public List<EmployeeSalaryStructureComponentMap> EmployeeSalaryStructEarnCompList { get; set; }

            public List<EmployeeSalaryStructureComponentMap> EmplSalaryStrDedCompList { get; set; }


            public List<Entity.HR.Leaves.LeaveApplication> LeaveApplications { get; set; }


            public int TotalWorkingDays { get; set; }

            public int PrevWorkDays { get; set; }

            public int NormalOTComponentID { get; set; }

            public int NoofPreviousfunctionalDays { get; set; }

            public int BasicPayComponentID { get; set; }

            public DateTime PayrollCutoffDate { get; set; }

            public DateTime prevPayrollCuttofDate { get; set; }

            public int SpecialOTComponentID { get; set; }

            public decimal? TotalhrsInaMonth { get; set; }

            public decimal? NormalRate { get; set; }
            public decimal? SpecialRate { get; set; }

            public int HrsPerDay { get; set; }


            public byte PendingStatusID { get; set; }

            public byte TimesheetStatusID { get; set; }

            public byte TimeSheetStatusID { get; set; }


            public int NormalOTTimeTypeID { get; set; }


            public int SpecialOTTimeTypeID { get; set; }



            public string ArrearComponentId { get; set; }


            public int NooffunctionalDays { get; set; }


            public DateTime PreviousDate { get; set; }


            public byte WithCUTOFDate { get; set; }


            public int DefaultDecimalNos { get; set; }



            public long SalarySlipIID { get; set; }


        }

        public SalarySlipDTO GetSalarySlipByID(long salarySlipID)
        {
            var datas = new SalarySlipDTO();

            using (var dbContext = new dbEduegateHRContext())
            {
                var issuedStatus = new Domain.Setting.SettingBL(_callContext).GetSettingValue<byte>("SALARYSLIPSTATUS_ISSUED");

                var salarySlip = dbContext.SalarySlips.Where(a => a.SalarySlipIID == salarySlipID).FirstOrDefault();
                if (salarySlip != null)
                {
                    datas = new SalarySlipDTO()
                    {
                        SalarySlipIID = salarySlipID,
                        EmployeeID = salarySlip.EmployeeID,
                        SlipDate = salarySlip.SlipDate,
                        SalarySlipStatusID = issuedStatus,
                    };
                }
            }
            return datas;
        }

    }
}