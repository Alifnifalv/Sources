using Newtonsoft.Json;
using Eduegate.Domain.Entity.HR;
using Eduegate.Domain.Entity.HR.Leaves;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.HR.Leaves;
using Eduegate.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Domain.Mappers.Notification;
using Eduegate.Domain.Mappers.Notification.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Eduegate.Domain.Setting;
//using Eduegate.Domain.Entity.School.Models;
using Eduegate.Services.Contracts.HR.Payroll;


namespace Eduegate.Domain.Mappers.HR.Leaves
{
    public class LeaveRequestMapper : DTOEntityDynamicMapper
    {
        CallContext _callContext;
        public static LeaveRequestMapper Mapper(CallContext context)
        {
            var mapper = new LeaveRequestMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<LeaveRequestDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private LeaveRequestDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateHRContext())
            {
                var entity = dbContext.LeaveApplications.Where(X => X.LeaveApplicationIID == IID)
                    .Include(i => i.Employee)
                    .AsNoTracking()
                    .FirstOrDefault();

                return new LeaveRequestDTO()
                {
                    //CompanyID = entity.CompanyID,
                    EmployeeID = entity.EmployeeID,
                    CreatedDate = entity.CreatedDate,
                    UpdatedDate = entity.UpdatedDate,
                    CreatedBy = entity.CreatedBy,
                    UpdatedBy = entity.UpdatedBy,
                    StaffName = entity.Employee != null ? entity.Employee.EmployeeCode + " " + entity.Employee.FirstName + " " + entity.Employee.MiddleName + " " + entity.Employee.LastName : null,
                    FromDate = entity.FromDate,
                    RejoiningDate = entity.RejoiningDate,
                    FromSessionID = entity.FromSessionID,
                    LeaveApplicationIID = entity.LeaveApplicationIID,
                    LeaveTypeID = entity.LeaveTypeID,
                    OtherReason = entity.OtherReason,
                    ToDate = entity.ToDate,
                    ToSessionID = entity.ToSessionID,
                    LeaveStatusID = entity.LeaveStatusID,
                    IsLeaveWithoutPay = entity.IsLeaveWithoutPay,
                    ExpectedRejoiningDate = entity.ExpectedRejoiningDate,
                    IsHalfDay = entity.IsHalfDay,
                };
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as LeaveRequestDTO;

            var settingBL = new Domain.Setting.SettingBL(_callContext);
            var vacationLeaveTypeID = settingBL.GetSettingValue<int>("VACATION_LEAVE_TYPE");
            var annualLeaveType = settingBL.GetSettingValue<int>("ANNUAL_LEAVE_TYPE");
            var academicTypeID = settingBL.GetSettingValue<int>("ACADEMIC_CALENDAR_TYPE_ID");

            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                var employees = dbContext.Employees.Where(a => a.EmployeeIID == toDto.EmployeeID).AsNoTracking().FirstOrDefault();


                if (employees.CalendarTypeID == academicTypeID && toDto.LeaveTypeID == annualLeaveType)
                {
                    throw new Exception("This employee is  academic staff, thus ineligible for annual leave!");
                }

                if (toDto.LeaveTypeID == vacationLeaveTypeID)
                {
                    if (employees.CalendarTypeID != 1)
                        throw new Exception("This employee is not academic staff, thus ineligible for vacation leave!");

                    var leaveStatusID = settingBL.GetSettingValue<int>("EMPLOYEE_LEAVE_STATUS_APPROVED");
                    var minNoofServiceDaysVacationSalary = settingBL.GetSettingValue<double?>("MIN_NO_DAYS_SERVICE_VACATION_SALARY");

                    //var leaveDueFromData = dbContext.LeaveApplications.Where(a => a.EmployeeID == toDto.EmployeeID && a.LeaveTypeID == vacationLeaveTypeID && a.LeaveStatusID == leaveStatusID
                    //&& a.LeaveApplicationIID != toDto.LeaveApplicationIID)
                    //.AsNoTracking()
                    //      .Max(y => (DateTime?)y.RejoiningDate);

                    //if (leaveDueFromData.HasValue)
                    //{
                    //    leaveDueFromDate = leaveDueFromData;
                    //}
                    //else
                    //{
                    // leaveDueFromDate = employees.DateOfJoining;
                    //}

                    var noOfDays = (toDto.FromDate.Value -employees.DateOfJoining.Value).TotalDays;
                    if (!minNoofServiceDaysVacationSalary.HasValue)
                        throw new Exception("Please set MIN_NO_DAYS_SERVICE_VACATION_SALARY in settings !");
                    else
                    {
                        if (noOfDays < minNoofServiceDaysVacationSalary)
                        {
                            throw new Exception("This employee cannot take vacation leave because they haven't completed the minimum days of service required.!");
                        }
                    }
                }

                //convert the dto to entity and pass to the repository.
                var entity = new LeaveApplication()
                {
                    //CompanyID = toDto.CompanyID,
                    EmployeeID = toDto.EmployeeID,
                    FromDate = toDto.FromDate,
                    FromSessionID = toDto.FromSessionID,
                    LeaveApplicationIID = toDto.LeaveApplicationIID,
                    LeaveTypeID = toDto.LeaveTypeID,
                    OtherReason = toDto.OtherReason,
                    ToDate = toDto.ToDate,
                    RejoiningDate = toDto.RejoiningDate,
                    ToSessionID = toDto.ToSessionID,
                    LeaveStatusID = toDto.LeaveStatusID,
                    IsLeaveWithoutPay = toDto.IsLeaveWithoutPay,
                    IsHalfDay = toDto.IsHalfDay,
                    ExpectedRejoiningDate = toDto.ExpectedRejoiningDate,
                    CreatedBy = toDto.LeaveApplicationIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.LeaveApplicationIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.LeaveApplicationIID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.LeaveApplicationIID > 0 ? DateTime.Now : dto.UpdatedDate,
                };


                dbContext.LeaveApplications.Add(entity);
                if (entity.LeaveApplicationIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    dbContext.SaveChanges();

                    SendPushNotification(entity);
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbContext.SaveChanges();
                }
                return ToDTOString(ToDTO(entity.LeaveApplicationIID));
            }

        }

        public List<LeaveRequestDTO> GetStaffLeaveApplication(long loginID)
        {
            List<LeaveRequestDTO> staffLeaveDTO = new List<LeaveRequestDTO>();

            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            using (var dbContext = new dbEduegateHRContext())
            {
                var employeeDetail = dbContext.Employees.Where(x => x.LoginID == loginID).AsNoTracking().FirstOrDefault();

                var leaveDetList = dbContext.LeaveApplications.Where(x => x.EmployeeID == employeeDetail.EmployeeIID).AsNoTracking().OrderByDescending(x => x.CreatedDate).ToList();

                if (leaveDetList != null)
                {
                    foreach (var leave in leaveDetList)
                    {
                        staffLeaveDTO.Add(new LeaveRequestDTO
                        {
                            LeaveApplicationIID = leave.LeaveApplicationIID,
                            //CompanyID = leave.CompanyID,
                            EmployeeID = leave.EmployeeID,
                            FromDate = leave.FromDate,
                            FromSessionID = leave.FromSessionID,
                            LeaveTypeID = leave.LeaveTypeID,
                            OtherReason = leave.OtherReason,
                            ToDate = leave.ToDate,
                            ToSessionID = leave.ToSessionID,
                            LeaveStatusID = leave.LeaveStatusID,
                            CreatedBy = leave.CreatedBy,
                            UpdatedBy = leave.UpdatedBy,
                            CreatedDate = leave.CreatedDate,
                            UpdatedDate = leave.UpdatedDate,
                            AppliedDateString = leave.CreatedDate.HasValue ? leave.CreatedDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : "NA",
                            FromDateString = leave.FromDate.HasValue ? leave.FromDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : "NA",
                            ToDateString = leave.ToDate.HasValue ? leave.ToDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : "NA",
                            RejoiningDateString = leave.RejoiningDate.HasValue ? leave.RejoiningDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : "NA",
                        });
                    }
                }

                return staffLeaveDTO;
            }
        }

        public string SubmitStaffLeaveApplication(LeaveRequestDTO leaveData)
        {
            using (var dbContext = new dbEduegateHRContext())
            {
                var entity = new LeaveApplication()
                {
                    LeaveApplicationIID = leaveData.LeaveApplicationIID,
                    EmployeeID = leaveData.EmployeeID,
                    LeaveTypeID = leaveData.LeaveTypeID,
                    //FromSessionID = leaveData.FromSessionID ?? 0,
                    //ToSessionID = leaveData.ToSessionID ?? 0,
                    LeaveStatusID = leaveData.LeaveStatusID,
                    OtherReason = leaveData.OtherReason,
                    FromDate = leaveData.FromDate,
                    ToDate = leaveData.ToDate,
                    CreatedBy = leaveData.LeaveApplicationIID == 0 ? (int)_context.LoginID : leaveData.CreatedBy,
                    UpdatedBy = leaveData.LeaveApplicationIID > 0 ? (int)_context.LoginID : leaveData.UpdatedBy,
                    CreatedDate = leaveData.LeaveApplicationIID == 0 ? DateTime.Now : leaveData.CreatedDate,
                    UpdatedDate = leaveData.LeaveApplicationIID > 0 ? DateTime.Now : leaveData.UpdatedDate,
                };

                if (entity.LeaveApplicationIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    dbContext.SaveChanges();

                    SendPushNotification(entity);
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbContext.SaveChanges();
                }

                //Workflows.Helpers.WorkflowGeneratorHelper.GenerateWorkflows(1, entity.LeaveApplicationIID);
            }
            return "Saved successfully!";
        }


        public List<KeyValueDTO> GetLeaveTypes()
        {
            using (dbEduegateHRContext dbContext = new dbEduegateHRContext())
            {
                var allergies = new List<KeyValueDTO>();

                var leaveTypes = dbContext.LeaveTypes.AsNoTracking().ToList();

                allergies = leaveTypes.Select(x => new KeyValueDTO()
                {
                    Key = x.LeaveTypeID.ToString(),
                    Value = x.Description,
                }).ToList();

                return allergies;
            }
        }

        public string SendPushNotification(LeaveApplication data)
        {
            using (var dbContext = new dbEduegateHRContext())
            {
                var settingData1 = new Domain.Setting.SettingBL(null).GetSettingValue<string>("Staff_leave_PushNotification_SentTo_Designations");
                var settingData2 = new Domain.Setting.SettingBL(null).GetSettingValue<string>("Emp_RoleIID_TimeTable_in-charge");

                string designationIdsString = settingData1;
                List<int?> designationIds = designationIdsString
                    .Split(',')
                    .Select(x => int.TryParse(x, out int result) ? (int?)result : null)
                    .ToList();

                var roleMapdat = int.Parse(settingData2);
                var empDetails = dbContext.Employees.AsNoTracking().FirstOrDefault(x => x.EmployeeIID == data.EmployeeID);
                var leaveTypes = dbContext.LeaveTypes.AsNoTracking().FirstOrDefault(l => l.LeaveTypeID == data.LeaveTypeID);

                var sendToEmp = dbContext.Employees.Where(e => (e.BranchID == empDetails.BranchID && designationIds.Contains(e.DesignationID)) || (e.BranchID == empDetails.BranchID && e.EmployeeRoleMaps.Any(a => a.EmployeeRoleID == roleMapdat))).AsNoTracking().ToList();

                var settings = NotificationSetting.GetEmployeeAppSettings();
                var title = "Leave applied from staff";
                var message = empDetails.EmployeeCode + " - " + empDetails.FirstName + " " + empDetails.MiddleName + " " + empDetails.LastName
                              + " : " + leaveTypes.Description + " applied from : " + data.FromDate?.ToString("dd/MM/yyyy") + " to " + data.ToDate?.ToString("dd/MM/yyyy");

                foreach (var sendTo in sendToEmp)
                {
                    if (sendTo.LoginID.HasValue)
                    {
                        PushNotificationMapper.SendAndSavePushNotification((long)sendTo.LoginID, (long)empDetails.LoginID, message, title, settings);
                    }
                }
            }

            return null;
        }

    }
}