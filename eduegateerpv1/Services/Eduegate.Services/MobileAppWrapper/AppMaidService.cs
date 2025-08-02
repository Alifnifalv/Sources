using Newtonsoft.Json;
using Eduegate.Domain.MobileAppWrapper;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.MobileAppWrapper;
using Eduegate.Services.Contracts.Payroll;
using Eduegate.Services.Contracts.Schedulers;
using System.ServiceModel;
using Eduegate.Services.Contracts.Commons;

namespace Eduegate.Services.MobileAppWrapper
{
    public class AppMaidService : BaseService, IAppMaidService
    {
        public OperationResultDTO Login(UserDTO user)
        {
            try
            {
                Logger.LogHelper<AppMaidService>.Info("Accessing Login:" + JsonConvert.SerializeObject(user) + JsonConvert.SerializeObject(CallContext));
                var returnData = new AppDataBL(CallContext).Login(user);
                Logger.LogHelper<AppMaidService>.Info("Return data:" + JsonConvert.SerializeObject(returnData));
                return returnData;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<AppMaidService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public EmployeeDTO GetEmployeeDtails(long employeeID)
        {
            try
            {
                return new AppMaidBL(CallContext).GetEmployeeDtails(employeeID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<AppMaidService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<EmployeeScheduleDTO> GetEmployeeSchedules(long employeeID, DateTime scehduleDate)
        {
            try
            {
                return new AppMaidBL(CallContext).GetEmployeeSchedules(employeeID, scehduleDate);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<AppMaidService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public EmployeeScheduleDTO GetNextScheduleDetails(long employeeID, DateTime scehduleDate)
        {
            try
            {
                return new AppMaidBL(CallContext).GetNextScheduleDetails(employeeID, scehduleDate);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<AppMaidService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public EmployeeDTO GetEmployeeDtailsByLoginUserID(long loginUserID)
        {
            try
            {
                return new AppMaidBL(CallContext).GetEmployeeDtailsByLoginUserID(loginUserID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<AppMaidService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public UserDTO GetUserDetails()
        {
            try
            {
                return new AppDataBL(CallContext).GetUserDetails();
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<AppMaidService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public string GenerateApiKey(string uuid, string version)
        {
            try
            {
                return new AppDataBL(this.CallContext).GenerateApiKey(uuid, version);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<AppMaidService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public EmployeeDTO GetMaidDtails(long maidID)
        {
            throw new NotImplementedException();
        }

        public EmployeeScheduleDetailsDTO GetEmployeeScheduleDetails(long employeeID, DateTime scehduleDate)
        {
            try
            {
                return new AppMaidBL(CallContext).GetEmployeeScheduleDetails(employeeID, scehduleDate);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<AppMaidService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public EmployeeScheduleDetailsDTO GetEmployeeScheduleDetailsByTime(long employeeID, DateTime scehduleDate, string time)
        {
            try
            {
                return new AppMaidBL(CallContext).GetEmployeeScheduleDetailsByTime(employeeID, scehduleDate, time);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<AppMaidService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public EmployeeScheduleDTO GetEmployeeScheduleByDespatchID(long despatchID)
        {
            try
            {
                return new AppMaidBL(CallContext).GetEmployeeScheduleByDespatchID(despatchID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<AppMaidService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public EmployeeScheduleDTO GetDriverScheduleByScheduleID(long scheduleID)
        {
            try
            {
                return new AppMaidBL(CallContext).GetDriverScheduleByScheduleID(scheduleID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<AppMaidService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public EmployeeScheduleSummaryInfoDTO GetEmployeeSummaryScheduleDetails(long employeeID, DateTime scehduleDate)
        {
            try
            {
                return new AppMaidBL(CallContext).GetEmployeeSummaryScheduleDetails(employeeID, scehduleDate);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<AppMaidService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public OperationResultDTO MaidScheduleUpdate(EmployeeScheduleDTO schedule)
        {
            try
            {
                return new AppMaidBL(CallContext).MaidScheduleUpdate(schedule);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<AppMaidService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public OperationResultDTO DriverScheduleUpdate(EmployeeScheduleDTO schedule)
        {
            try
            {
                return new AppMaidBL(CallContext).DriverScheduleUpdate(schedule);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<AppMaidService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public OperationResultDTO SaveGeoLocation(GeoLocationLogDTO location)
        {
            try
            {
                return new AppMaidBL(CallContext).SaveGeoLocation(location);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<AppMaidService>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }
    }
}
