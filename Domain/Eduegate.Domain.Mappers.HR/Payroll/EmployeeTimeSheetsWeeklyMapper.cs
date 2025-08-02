using Eduegate.Domain.Entity.HR;
using Eduegate.Domain.Entity.HR.Payroll;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.HR.Employee;
using Eduegate.Services.Contracts.HR.Payroll;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Eduegate.Domain.Mappers.HR.Payroll
{
    public class EmployeeTimeSheetsWeeklyMapper : DTOEntityDynamicMapper
    {
        public static EmployeeTimeSheetsWeeklyMapper Mapper(CallContext context)
        {
            var mapper = new EmployeeTimeSheetsWeeklyMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<EmployeeTimeSheetsWeeklyDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                var entity = dbContext.EmployeeTimeSheets.Where(x => x.EmployeeTimeSheetIID == IID)
                    .Include(i => i.Employee)
                    .AsNoTracking()
                    .FirstOrDefault();

                var sheet = new EmployeeTimeSheetsWeeklyDTO()
                {
                    EmployeeTimeSheetIID = entity.EmployeeTimeSheetIID,
                    EmployeeID = entity.EmployeeID,
                    Employee = new KeyValueDTO()
                    {
                        Key = entity.EmployeeID.ToString(),
                        Value = entity.Employee != null ? entity.Employee?.FirstName + " " + entity.Employee?.MiddleName + " " + entity.Employee?.LastName : null,
                    },
                };

                var timehsheetList = dbContext.EmployeeTimeSheets.Where(s => s.EmployeeID == entity.EmployeeID)
                    .Include(i => i.Employee)
                    .Include(i => i.Task)
                    .Include(i => i.TimesheetEntryStatus)
                    .AsNoTracking()
                    .ToList();

                sheet.TimeSheet = new List<EmployeeTimeSheetsTimeDTO>();

                foreach (var sheetTime in timehsheetList)
                {
                    sheet.TimeSheet.Add(new EmployeeTimeSheetsTimeDTO()
                    {
                        EmployeeTimeSheetIID = sheetTime.EmployeeTimeSheetIID,
                        OTHours = sheetTime.OTHours,
                        NormalHours = sheetTime.NormalHours,
                        TimesheetDate = sheetTime.TimesheetDate,
                        Task = new KeyValueDTO()
                        {
                            Key = sheetTime.TaskID.ToString(),
                            Value = sheetTime.Task.Description
                        },
                        TimesheetEntryStatusID = sheetTime.TimesheetEntryStatusID,
                        TimesheetEntryStatus = new KeyValueDTO()
                        {
                            Key = sheetTime.TimesheetEntryStatusID.ToString(),
                            Value = sheetTime.TimesheetEntryStatus != null ? sheetTime.TimesheetEntryStatus?.StatusName : null,
                        },
                        FromTime = sheetTime.FromTime,
                        ToTime = sheetTime.ToTime,
                        Remarks = sheetTime.Remarks,
                        TimesheetTimeTypeID = sheetTime.TimesheetTimeTypeID,
                    });
                }

                return ToDTOString(sheet);
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as EmployeeTimeSheetsWeeklyDTO;
            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                if (toDto.EmployeeID == 0)
                {
                    if (toDto.IsSelf == true)
                    {
                        var employee = dbContext.Employees.AsNoTracking().FirstOrDefault(e => e.LoginID == _context.LoginID);

                        if (employee == null)
                        {
                            throw new Exception("This sheet is only for employees");
                        }
                        else
                        {
                            toDto.EmployeeID = employee.EmployeeIID;
                        }
                    }
                    else
                    {
                        throw new Exception("Please select an employee");
                    }
                }
                if (toDto.TimeSheet.Count == 0)
                {
                    throw new Exception("Please Select Timesheet Details!");
                }

                EmployeeTimeSheet sheet = null;
                if (toDto.TimeSheet.Count > 0)
                {
                    //var IIDs = toDto.TimeSheet
                    //   .Select(a => a.EmployeeTimeSheetIID).ToList();
                    ////delete maps
                    //var entities = dbContext.EmployeeTimeSheets.Where(x =>
                    //    (x.EmployeeID == toDto.EmployeeID) &&
                    //    !IIDs.Contains(x.EmployeeTimeSheetIID)).AsNoTracking().ToList();

                    //if (entities.IsNotNull())
                    //    dbContext.EmployeeTimeSheets.RemoveRange(entities);

                    foreach (var sheetTime in toDto.TimeSheet)
                    {
                        if (sheetTime.TaskID != 0)
                        {
                            var entity = new EmployeeTimeSheet()
                            {
                                EmployeeTimeSheetIID = sheetTime.EmployeeTimeSheetIID,
                                EmployeeID = toDto.EmployeeID,
                                OTHours = sheetTime.OTHours,
                                NormalHours = sheetTime.NormalHours,
                                TimesheetDate = sheetTime.TimesheetDate,
                                TaskID = sheetTime.TaskID,
                                CreatedBy = toDto.EmployeeTimeSheetIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                                UpdatedBy = toDto.EmployeeTimeSheetIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                                CreatedDate = toDto.EmployeeTimeSheetIID == 0 ? DateTime.Now : dto.CreatedDate,
                                UpdatedDate = toDto.EmployeeTimeSheetIID > 0 ? DateTime.Now : dto.UpdatedDate,
                                //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
                                TimesheetEntryStatusID = sheetTime.TimesheetEntryStatusID,
                                FromTime = sheetTime.FromTime,
                                ToTime = sheetTime.ToTime,
                                Remarks = sheetTime.Remarks,
                                TimesheetTimeTypeID = sheetTime.TimesheetTimeTypeID,
                            };

                            sheet = entity;

                            dbContext.EmployeeTimeSheets.Add(entity);
                            if (entity.EmployeeTimeSheetIID == 0)
                            {
                                dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                            dbContext.SaveChanges();
                        }
                    }
                }

                return GetEntity(sheet.EmployeeTimeSheetIID);
            }
        }

        //OLD GetCollectTimeSheetsData Function //06-01-2024

        //public EmployeeTimeSheetsWeeklyDTO GetCollectTimeSheetsData(long employeeID, DateTime fromDate, DateTime toDate)
        //{
        //    var emptimesheet = new EmployeeTimeSheetsWeeklyDTO();
        //    var time = new List<EmployeeTimeSheetsTimeDTO>();
        //    using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
        //    {
        //        emptimesheet = (from emptime in dbContext.EmployeeTimeSheets
        //                        join emp in dbContext.Employees on emptime.EmployeeID equals emp.EmployeeIID
        //                        where (emptime.EmployeeID == employeeID || employeeID == -1) &&
        //                        ((emptime.TimesheetDate.Date >= fromDate.Date) && (emptime.TimesheetDate.Date <= toDate.Date))
        //                        orderby emptime.EmployeeID descending
        //                        select new EmployeeTimeSheetsWeeklyDTO()
        //                        {
        //                            EmployeeTimeSheetIID = emptime.EmployeeTimeSheetIID,
        //                            EmployeeID = emptime.EmployeeID,
        //                            Employee = new KeyValueDTO()
        //                            {
        //                                Key = emptime.EmployeeID.ToString(),
        //                                Value = emptime.Employee.EmployeeName
        //                            },
        //                            TimeSheet = (from emptime in dbContext.EmployeeTimeSheets
        //                                         where (emptime.EmployeeID == employeeID || employeeID == -1) &&
        //                                         ((emptime.TimesheetDate.Date >= fromDate.Date) && (emptime.TimesheetDate.Date <= toDate.Date))
        //                                         group emptime by new
        //                                         {
        //                                             emptime.EmployeeTimeSheetIID,
        //                                             emptime.TaskID,
        //                                             emptime.Task,
        //                                             emptime.NormalHours,
        //                                             emptime.OTHours,
        //                                             emptime.TimesheetDate,
        //                                             emptime.TimesheetEntryStatusID,
        //                                             emptime.FromTime,
        //                                             emptime.ToTime,
        //                                             emptime.Remarks,

        //                                         } into timedtogrp
        //                                         select new EmployeeTimeSheetsTimeDTO()
        //                                         {
        //                                             EmployeeTimeSheetIID = timedtogrp.Key.EmployeeTimeSheetIID,
        //                                             NormalHours = timedtogrp.Key.NormalHours,
        //                                             OTHours = timedtogrp.Key.OTHours,
        //                                             TimesheetDate = timedtogrp.Key.TimesheetDate,
        //                                             TaskID = timedtogrp.Key.TaskID,
        //                                             Task = new KeyValueDTO()
        //                                             {
        //                                                 Key = timedtogrp.Key.TaskID.ToString(),
        //                                                 Value = timedtogrp.Key.Task.Description
        //                                             },
        //                                             TimesheetEntryStatusID = timedtogrp.Key.TimesheetEntryStatusID,
        //                                             FromTime = timedtogrp.Key.FromTime,
        //                                             ToTime = timedtogrp.Key.ToTime,
        //                                             Remarks = timedtogrp.Key.Remarks
        //                                         }).AsNoTracking().ToList(),

        //                        }).AsNoTracking().FirstOrDefault();
        //    }

        //    return emptimesheet;
        //}

        //New GetCollectTimeSheetsData function
        public EmployeeTimeSheetsWeeklyDTO GetCollectTimeSheetsData(long employeeID, DateTime fromDate, DateTime toDate)
        {
            var emptimesheet = new EmployeeTimeSheetsWeeklyDTO();
            var time = new List<EmployeeTimeSheetsTimeDTO>();

            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                var timeSheetQuery = from emptime in dbContext.EmployeeTimeSheets
                                     join emp in dbContext.Employees on emptime.EmployeeID equals emp.EmployeeIID
                                     where (emptime.EmployeeID == employeeID || employeeID == -1) &&
                                           (emptime.TimesheetDate.Date >= fromDate.Date && emptime.TimesheetDate.Date <= toDate.Date)
                                     orderby emptime.EmployeeID descending
                                     select new EmployeeTimeSheetsTimeDTO()
                                     {
                                         EmployeeTimeSheetIID = emptime.EmployeeTimeSheetIID,
                                         NormalHours = emptime.NormalHours,
                                         OTHours = emptime.OTHours,
                                         TimesheetDate = emptime.TimesheetDate,
                                         TaskID = emptime.TaskID,
                                         TimesheetEntryStatus = new KeyValueDTO()
                                         {
                                             Key = emptime.TimesheetEntryStatusID.ToString(),
                                             Value = emptime.TimesheetEntryStatus.StatusName
                                         },
                                         Task = new KeyValueDTO()
                                         {
                                             Key = emptime.TaskID.ToString(),
                                             Value = emptime.Task.Description
                                         },
                                         TimesheetEntryStatusID = emptime.TimesheetEntryStatusID,
                                         TimesheetTimeTypeID = emptime.TimesheetTimeTypeID,
                                         TimesheetTimeType = emptime.TimesheetTimeType.TypeNameEn,
                                         FromTime = emptime.FromTime,
                                         ToTime = emptime.ToTime,
                                         Remarks = emptime.Remarks
                                     };

                emptimesheet = new EmployeeTimeSheetsWeeklyDTO()
                {
                    TimeSheet = timeSheetQuery.AsNoTracking().ToList(),
                };
            }

            return emptimesheet;
        }

        public EmployeesDTO GetEmployeeFromEmployeeID(long employeeID)
        {
            using (var dbContext = new dbEduegateHRContext())
            {
                var dtos = new EmployeesDTO();
                var emp = dbContext.Employees.Where(a => a.EmployeeIID == employeeID)
                   .OrderByDescending(o => o.EmployeeIID)
                   .AsNoTracking()
                   .FirstOrDefault();

                var employeeDTO = new EmployeesDTO()
                {
                    EmployeeIID = emp.EmployeeIID,
                    EmployeeName = emp.EmployeeCode + " - " + emp.FirstName + " " + emp.MiddleName + " " + emp.LastName,
                };

                return employeeDTO;
            }
        }

        public List<CalendarEntryDTO> GetWorkingHourDetailsByEmployee(long employeeID)
        {
            using (var dbContext = new dbEduegateHRContext())
            {
                var calendarEntyDtos = new List<CalendarEntryDTO>();

                var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

                var empDet = dbContext.Employees.AsNoTracking().FirstOrDefault(e => e.EmployeeIID == employeeID);

                var calendarEntries = empDet != null ? dbContext.CalendarEntries.Where(c => c.AcademicCalendarID == empDet.AcademicCalendarID).AsNoTracking().ToList() : null;

                if (calendarEntries != null)
                {
                    foreach (var entry in calendarEntries)
                    {
                        calendarEntyDtos.Add(new CalendarEntryDTO()
                        {
                            CalendarEntryID = entry.CalendarEntryID,
                            AcademicCalendarID = entry.AcademicCalendarID,
                            CalendarDate = entry.CalendarDate,
                            CalendarDateString = entry.CalendarDate.HasValue ? entry.CalendarDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                            NoofHours = entry.NoofHours
                        });
                    }
                }

                return calendarEntyDtos;
            }
        }

        public List<EmployeeTimeSheetApprovalDTO> GetPendingTimeSheetsDatas(long employeeID, DateTime? dateFrom, DateTime? dateTo)
        {
            using (var dbContext = new dbEduegateHRContext())
            {
                var timeSheetDtos = new List<EmployeeTimeSheetApprovalDTO>();

                var submittedEntryStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("TIMESHEET_ENTRY_SUBMIT_STATUS_ID", (byte)2);

                var approvalApprovedStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("TIMESHEET_APPROVAL_APPROVED_STATUS_ID", (byte)2);

                if (employeeID > 0)
                {
                    var timeSheets = dbContext.EmployeeTimeSheets
                        .Where(s => s.EmployeeID == employeeID && s.TimesheetDate >= dateFrom && s.TimesheetDate <= dateTo && s.TimesheetEntryStatusID == submittedEntryStatusID)
                        .Include(i => i.Employee)
                        .Include(i => i.TimesheetEntryStatus)
                        .Include(i => i.TimesheetTimeType)
                        .Include(i => i.Task)
                        .OrderBy(o => o.TimesheetDate)
                        .AsNoTracking()
                        .ToList();

                    if (timeSheets.Count > 0)
                    {
                        var groupedTimeSheets = timeSheets.GroupBy(g => g.TimesheetDate).ToList();

                        foreach (var sheetList in groupedTimeSheets)
                        {
                            UpdateTimeSheetDatas(sheetList.ToList(), timeSheetDtos, dbContext, approvalApprovedStatusID);
                        }
                    }
                }
                else
                {
                    var timeSheets = dbContext.EmployeeTimeSheets
                           .Where(s => s.TimesheetDate >= dateFrom && s.TimesheetDate <= dateTo && s.TimesheetEntryStatusID == submittedEntryStatusID)
                           .Include(i => i.Employee)
                           .Include(i => i.TimesheetEntryStatus)
                           .Include(i => i.TimesheetTimeType)
                           .Include(i => i.Task)
                           .OrderBy(o => o.TimesheetDate)
                           .ToList();

                    if (timeSheets.Count > 0)
                    {
                        var groupedTimeSheetsByEmployee = timeSheets.GroupBy(g => g.EmployeeID).ToList();

                        foreach (var employeeSheetList in groupedTimeSheetsByEmployee)
                        {
                            if (employeeSheetList != null)
                            {
                                var groupedTimeSheetsByDate = employeeSheetList.GroupBy(g => g.TimesheetDate).ToList();

                                foreach (var sheetList in groupedTimeSheetsByDate)
                                {
                                    UpdateTimeSheetDatas(sheetList.ToList(), timeSheetDtos, dbContext, approvalApprovedStatusID);
                                }
                            }
                        }
                    }
                }

                return timeSheetDtos;
            }
        }

        private void UpdateTimeSheetDatas(List<EmployeeTimeSheet> employeeTimeSheets, List<EmployeeTimeSheetApprovalDTO> timeSheetDtos, dbEduegateHRContext dbContext, byte timesheetApprovalApprovedStatusID)
        {
            var timeSheetDetails = new List<EmployeeTimeSheetApprovalTimeDTO>();

            if (employeeTimeSheets.Count > 0)
            {
                long employeeTimeSheetApprovalID = 0;
                byte? timesheetApprovalStatusID = null;

                var timeSheetIDs = new List<long?>();

                foreach (var sheet in employeeTimeSheets)
                {
                    timeSheetIDs.Add(sheet.EmployeeTimeSheetIID);

                    timeSheetDetails.Add(new EmployeeTimeSheetApprovalTimeDTO()
                    {
                        EmployeeTimeSheetIID = sheet.EmployeeTimeSheetIID,
                        FromTime = sheet.FromTime,
                        ToTime = sheet.ToTime,
                        TimesheetTimeTypeID = sheet.TimesheetTimeTypeID,
                        TimesheetTimeTypeName = sheet.TimesheetTimeTypeID.HasValue ? sheet.TimesheetTimeType.TypeNameEn : null,
                        SheetNormalHours = sheet.NormalHours,
                        SheetOTHours = sheet.OTHours
                    });
                }

                var timeSheetApprovalMap = timeSheetIDs.Count > 0 ? dbContext.EmployeeTimesheetApprovalMaps.AsNoTracking().FirstOrDefault(m => timeSheetIDs.Contains(m.EmployeeTimeSheetID)) : null;

                if (timeSheetApprovalMap != null)
                {
                    employeeTimeSheetApprovalID = timeSheetApprovalMap.EmployeeTimesheetApprovalID.HasValue ? Convert.ToInt64(timeSheetApprovalMap.EmployeeTimesheetApprovalID) : 0;

                    timesheetApprovalStatusID = timeSheetApprovalMap.EmployeeTimeSheetApproval?.TimesheetApprovalStatusID;
                }

                if (timesheetApprovalStatusID == null || (timesheetApprovalStatusID != timesheetApprovalApprovedStatusID))
                {
                    var timeSheetIncludeType = employeeTimeSheets.FirstOrDefault(s => s.TimesheetTimeTypeID.HasValue);

                    timeSheetDtos.Add(new EmployeeTimeSheetApprovalDTO()
                    {
                        EmployeeTimeSheetApprovalIID = employeeTimeSheetApprovalID,
                        EmployeeTimeSheetID = employeeTimeSheets[0].EmployeeTimeSheetIID,
                        EmployeeID = employeeTimeSheets[0].EmployeeID,
                        EmployeeName = employeeTimeSheets[0]?.Employee?.EmployeeCode + " - " + employeeTimeSheets[0]?.Employee?.FirstName + " " + (employeeTimeSheets[0]?.Employee?.MiddleName != null ? employeeTimeSheets[0]?.Employee?.MiddleName + " " : "") + employeeTimeSheets[0]?.Employee?.LastName,
                        TimeSheetDate = employeeTimeSheets[0].TimesheetDate,
                        TotalNormalHours = employeeTimeSheets.Sum(s => s.NormalHours),
                        TotalOTHours = employeeTimeSheets.Sum(s => s.OTHours),
                        TimeSheetDetails = timeSheetDetails,
                        TimesheetTimeTypeID = timeSheetIncludeType?.TimesheetTimeTypeID,
                        TimesheetTimeTypeName = timeSheetIncludeType?.TimesheetTimeType?.TypeNameEn,
                    });
                }
            }
        }

    }
}