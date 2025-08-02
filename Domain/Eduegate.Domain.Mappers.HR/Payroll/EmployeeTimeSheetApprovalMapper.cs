using Eduegate.Domain.Entity.HR;
using Eduegate.Domain.Entity.HR.Payroll;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.HR.Payroll;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Domain.Mappers.HR.Payroll
{
    public class EmployeeTimeSheetApprovalMapper : DTOEntityDynamicMapper
    {
        public static EmployeeTimeSheetApprovalMapper Mapper(CallContext context)
        {
            var mapper = new EmployeeTimeSheetApprovalMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<EmployeeTimeSheetApprovalDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private EmployeeTimeSheetApprovalDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateHRContext())
            {
                var entity = dbContext.EmployeeTimeSheetApprovals.Where(X => X.EmployeeTimeSheetApprovalIID == IID)
                    .Include(i => i.Employee)
                    .Include(i => i.TimesheetApprovalStatus)
                    .Include(i => i.TimesheetTimeType)
                    .Include(i => i.EmployeeTimesheetApprovalMaps)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTO(entity);
            }
        }

        private EmployeeTimeSheetApprovalDTO ToDTO(EmployeeTimeSheetApproval entity)
        {
            var approvalDTO = new EmployeeTimeSheetApprovalDTO()
            {
                EmployeeTimeSheetApprovalIID = entity.EmployeeTimeSheetApprovalIID,
                NormalHours = entity.NormalHours,
                OTHours = entity.OTHours,
                EmployeeID = entity.EmployeeID,
                TimesheetDateFrom = entity.TimesheetDateFrom,
                TimesheetDateTo = entity.TimesheetDateTo,
                TimesheetTimeTypeID = entity.TimesheetTimeTypeID,
                TimesheetApprovalStatusID = entity.TimesheetApprovalStatusID,
                Remarks = entity.Remarks,
            };

            return approvalDTO;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as EmployeeTimeSheetApprovalDTO;

            if (toDto.EmployeeID == null)
            {
                throw new Exception("Select an employee!");
            }

            //convert the dto to entity and pass to the repository.
            using (var dbContext = new dbEduegateHRContext())
            {
                var entity = new EmployeeTimeSheetApproval()
                {
                    EmployeeTimeSheetApprovalIID = toDto.EmployeeTimeSheetApprovalIID,
                    NormalHours = toDto.NormalHours.HasValue ? toDto.NormalHours : 0,
                    OTHours = toDto.OTHours.HasValue ? toDto.OTHours : 0,
                    EmployeeID = Convert.ToInt64(toDto.EmployeeID),
                    TimesheetDateFrom = toDto.TimesheetDateFrom,
                    TimesheetDateTo = toDto.TimesheetDateTo.HasValue ? toDto.TimesheetDateTo : toDto.TimesheetDateFrom,
                    TimesheetTimeTypeID = toDto.TimesheetTimeTypeID,
                    TimesheetApprovalStatusID = toDto.TimesheetApprovalStatusID,
                    Remarks = toDto.Remarks,
                    CreatedBy = toDto.EmployeeTimeSheetApprovalIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.EmployeeTimeSheetApprovalIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.EmployeeTimeSheetApprovalIID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.EmployeeTimeSheetApprovalIID > 0 ? DateTime.Now : dto.UpdatedDate,
                };

                dbContext.EmployeeTimeSheetApprovals.Add(entity);

                if (entity.EmployeeTimeSheetApprovalIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    if (entity.EmployeeTimesheetApprovalMaps.Count > 0)
                    {
                        var oldMaps = dbContext.EmployeeTimesheetApprovalMaps.Where(s => s.EmployeeTimesheetApprovalID == entity.EmployeeTimeSheetApprovalIID).AsNoTracking().ToList();

                        if (oldMaps != null && oldMaps.Count > 0)
                        {
                            dbContext.EmployeeTimesheetApprovalMaps.RemoveRange(oldMaps);
                        }

                        foreach (var approveMap in entity.EmployeeTimesheetApprovalMaps)
                        {
                            if (approveMap.EmployeeTimesheetApprovalMapIID == 0)
                            {
                                dbContext.Entry(approveMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            }
                            else
                            {
                                dbContext.Entry(approveMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                        }
                    }

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                dbContext.SaveChanges();

                return ToDTOString(ToDTO(entity.EmployeeTimeSheetApprovalIID));
            }
        }

        public OperationResultDTO SavePendingTimesheetApprovalData(EmployeeTimeSheetApprovalDTO toDto)
        {
            using (var dbContext = new dbEduegateHRContext())
            {
                var message = new OperationResultDTO();

                try
                {
                    var timesheetApprovalMaps = new List<EmployeeTimesheetApprovalMap>();

                    var approvalRejectedStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("TIMESHEET_APPROVAL_REJECTED_STATUS_ID", (byte)1);

                    var remarks = toDto.Remarks;

                    if (toDto.TimesheetApprovalStatusID == approvalRejectedStatusID)
                    {
                        var approvalStatus = dbContext.TimesheetApprovalStatuses.AsNoTracking().FirstOrDefault(s => s.TimesheetApprovalStatusID == approvalRejectedStatusID);

                        remarks = approvalStatus?.StatusName + " : " + toDto.Remarks;
                    }

                    foreach (var timesheet in toDto.TimeSheetDetails)
                    {
                        if (toDto.TimesheetApprovalStatusID == approvalRejectedStatusID)
                        {
                            var sheetData = dbContext.EmployeeTimeSheets.AsNoTracking().FirstOrDefault(y => y.EmployeeTimeSheetIID == timesheet.EmployeeTimeSheetIID);

                            if (sheetData != null)
                            {
                                sheetData.EmployeeTimeSheetIID = timesheet.EmployeeTimeSheetIID;
                                sheetData.TimesheetEntryStatusID = null;
                                sheetData.UpdatedBy = _context?.LoginID;
                                sheetData.UpdatedDate = DateTime.Now;
                                sheetData.Remarks = remarks;

                                dbContext.Entry(sheetData).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            }
                        }

                        timesheetApprovalMaps.Add(new EmployeeTimesheetApprovalMap()
                        {
                            EmployeeTimesheetApprovalID = toDto.EmployeeTimeSheetApprovalIID,
                            EmployeeTimeSheetID = timesheet.EmployeeTimeSheetIID
                        });
                    }

                    if (toDto != null)
                    {
                        var entity = new EmployeeTimeSheetApproval()
                        {
                            EmployeeTimeSheetApprovalIID = toDto.EmployeeTimeSheetApprovalIID,
                            EmployeeID = (long)toDto.EmployeeID,
                            TimesheetDateFrom = toDto.TimesheetDateFrom,
                            TimesheetDateTo = toDto.TimesheetDateTo.HasValue ? toDto.TimesheetDateTo : toDto.TimesheetDateFrom,
                            TimesheetTimeTypeID = toDto.TimesheetTimeTypeID,
                            NormalHours = toDto.NormalHours.HasValue ? toDto.NormalHours : 0,
                            OTHours = toDto.OTHours.HasValue ? toDto.OTHours : 0,
                            TimesheetApprovalStatusID = toDto.TimesheetApprovalStatusID,
                            Remarks = toDto.Remarks,
                            CreatedBy = _context?.LoginID,
                            CreatedDate = DateTime.Now,
                            EmployeeTimesheetApprovalMaps = timesheetApprovalMaps
                        };

                        if (entity.EmployeeTimeSheetApprovalIID == 0)
                        {
                            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }
                        else
                        {
                            if (entity.EmployeeTimesheetApprovalMaps.Count > 0)
                            {
                                var oldMaps = dbContext.EmployeeTimesheetApprovalMaps.Where(s => s.EmployeeTimesheetApprovalID == entity.EmployeeTimeSheetApprovalIID).AsNoTracking().ToList();

                                if (oldMaps != null && oldMaps.Count > 0)
                                {
                                    dbContext.EmployeeTimesheetApprovalMaps.RemoveRange(oldMaps);
                                }

                                foreach (var approveMap in entity.EmployeeTimesheetApprovalMaps)
                                {
                                    if (approveMap.EmployeeTimesheetApprovalMapIID == 0)
                                    {
                                        dbContext.Entry(approveMap).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                                    }
                                    else
                                    {
                                        dbContext.Entry(approveMap).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                    }
                                }
                            }

                            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                    }

                    dbContext.SaveChanges();

                    message = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Success,
                        Message = "Saved successfully!"
                    };
                }
                catch (Exception ex)
                {
                    message = new OperationResultDTO()
                    {
                        operationResult = OperationResult.Error,
                        Message = ex.Message
                    };
                }

                return message;
            }
        }

        public List<EmployeeTimeSheetApprovalDTO> GetApprovedTimeSheetsDatas(long employeeID, DateTime? dateFrom, DateTime? dateTo)
        {
            using (var dbContext = new dbEduegateHRContext())
            {
                var timeSheetDtos = new List<EmployeeTimeSheetApprovalDTO>();

                var approvalApprovedStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("TIMESHEET_APPROVAL_APPROVED_STATUS_ID", (byte)1);
                
                if (employeeID > 0)
                {
                    var timeSheets = dbContext.EmployeeTimeSheetApprovals
                        .Where(s => s.EmployeeID == employeeID && s.TimesheetDateFrom >= dateFrom && s.TimesheetDateTo <= dateTo && s.TimesheetApprovalStatusID == approvalApprovedStatusID)
                        .Include(i => i.Employee)
                        .Include(i => i.TimesheetApprovalStatus)
                        .Include(i => i.TimesheetTimeType)
                        .Include(i => i.EmployeeTimesheetApprovalMaps)
                        .AsNoTracking()
                        .ToList();

                    if (timeSheets.Count > 0)
                    {
                        UpdateTimeSheetDatas(timeSheets, timeSheetDtos, dbContext);
                    }
                }
                else
                {
                    var timeSheets = dbContext.EmployeeTimeSheetApprovals
                           .Where(s => s.TimesheetDateFrom >= dateFrom && s.TimesheetDateTo <= dateTo && s.TimesheetApprovalStatusID == approvalApprovedStatusID)
                           .Include(i => i.Employee)
                           .Include(i => i.TimesheetApprovalStatus)
                           .Include(i => i.TimesheetTimeType)
                           .Include(i => i.EmployeeTimesheetApprovalMaps)
                           .AsNoTracking()
                           .ToList();

                    if (timeSheets.Count > 0)
                    {
                        var groupedTimeSheetsByEmployee = timeSheets.GroupBy(g => g.EmployeeID).ToList();

                        foreach (var sheetList in groupedTimeSheetsByEmployee)
                        {
                            if (sheetList != null)
                            {
                                UpdateTimeSheetDatas(sheetList.ToList(), timeSheetDtos, dbContext);
                            }
                        }
                    }
                }

                return timeSheetDtos;
            }
        }

        private void UpdateTimeSheetDatas(List<EmployeeTimeSheetApproval> employeeTimeSheets, List<EmployeeTimeSheetApprovalDTO> timeSheetDtos, dbEduegateHRContext dbContext)
        {
            if (employeeTimeSheets.Count > 0)
            {
                foreach (var sheet in employeeTimeSheets)
                {
                    var timeSheetDetails = new List<EmployeeTimeSheetApprovalTimeDTO>();

                    foreach (var map in sheet.EmployeeTimesheetApprovalMaps)
                    {
                        var employeeSheetData = dbContext.EmployeeTimeSheets.AsNoTracking().FirstOrDefault(e => e.EmployeeTimeSheetIID == map.EmployeeTimeSheetID);

                        if (employeeSheetData != null)
                        {
                            timeSheetDetails.Add(new EmployeeTimeSheetApprovalTimeDTO()
                            {
                                EmployeeTimeSheetIID = employeeSheetData.EmployeeTimeSheetIID,
                                TaskID = employeeSheetData.TaskID,
                                TaskName = employeeSheetData.Task?.Description,
                                FromTime = employeeSheetData.FromTime,
                                ToTime = employeeSheetData.ToTime,
                                TimesheetTimeTypeID = employeeSheetData.TimesheetTimeTypeID,
                                TimesheetTimeTypeName = employeeSheetData.TimesheetTimeTypeID.HasValue ? employeeSheetData.TimesheetTimeType?.TypeNameEn : "NA",
                                SheetNormalHours = employeeSheetData.NormalHours,
                                SheetOTHours = employeeSheetData.OTHours,
                                Remarks = employeeSheetData.Remarks
                            });
                        }
                    }

                    timeSheetDtos.Add(new EmployeeTimeSheetApprovalDTO()
                    {
                        EmployeeTimeSheetApprovalIID = sheet.EmployeeTimeSheetApprovalIID,
                        EmployeeID = sheet.EmployeeID,
                        EmployeeName = sheet?.Employee?.EmployeeCode + " - " + sheet?.Employee?.FirstName + " " + (sheet?.Employee?.MiddleName != null ? sheet?.Employee?.MiddleName + " " : "") + sheet?.Employee?.LastName,
                        TimesheetDateFrom = sheet.TimesheetDateFrom,
                        TimesheetDateTo = sheet.TimesheetDateTo,
                        TimesheetTimeTypeID = sheet.TimesheetTimeTypeID,
                        TimesheetTimeTypeName = sheet.TimesheetTimeTypeID.HasValue ? sheet.TimesheetTimeType?.TypeNameEn : "NA",
                        NormalHours = sheet.NormalHours,
                        OTHours = sheet.OTHours,
                        TimeSheetDetails = timeSheetDetails
                    });
                }

            }
        }

    }
}