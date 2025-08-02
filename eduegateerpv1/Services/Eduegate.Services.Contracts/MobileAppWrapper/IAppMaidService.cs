using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.Payroll;
using Eduegate.Services.Contracts.Schedulers;
using System;
using System.Collections.Generic;

namespace Eduegate.Services.Contracts.MobileAppWrapper
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAppMaidService" in both code and config file together.
    public interface IAppMaidService
    {
        OperationResultDTO Login(UserDTO user);

        EmployeeScheduleDetailsDTO GetEmployeeScheduleDetails(long employeeID, DateTime scehduleDate);

        EmployeeScheduleDetailsDTO GetEmployeeScheduleDetailsByTime(long employeeID, DateTime scehduleDate, string time);

        EmployeeScheduleSummaryInfoDTO GetEmployeeSummaryScheduleDetails(long employeeID, DateTime scehduleDate);

        EmployeeScheduleDTO GetEmployeeScheduleByDespatchID(long despatchID);

        EmployeeScheduleDTO GetDriverScheduleByScheduleID(long scheduleID);

        List<EmployeeScheduleDTO> GetEmployeeSchedules(long employeeID, DateTime scehduleDate);

        EmployeeDTO GetEmployeeDtails(long employeeID);

        EmployeeDTO GetMaidDtails(long maidID);

        EmployeeDTO GetEmployeeDtailsByLoginUserID(long loginUserID);

        EmployeeScheduleDTO GetNextScheduleDetails(long employeeID, DateTime scehduleDate);

        UserDTO GetUserDetails();

        string GenerateApiKey(string uuid, string version);

        OperationResultDTO MaidScheduleUpdate(EmployeeScheduleDTO schedule);

        OperationResultDTO DriverScheduleUpdate(EmployeeScheduleDTO schedule);

        OperationResultDTO SaveGeoLocation(GeoLocationLogDTO schedule);
    }
}