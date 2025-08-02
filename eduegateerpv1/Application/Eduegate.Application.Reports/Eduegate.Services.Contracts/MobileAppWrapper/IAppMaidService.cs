using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.Payroll;
using Eduegate.Services.Contracts.Schedulers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Eduegate.Services.Contracts.MobileAppWrapper
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAppMaidService" in both code and config file together.
    [ServiceContract]
    public interface IAppMaidService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "Login")]
        OperationResultDTO Login(UserDTO user);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetEmployeeScheduleDetails?employeeID={employeeID}&scehduleDate={scehduleDate}")]
        EmployeeScheduleDetailsDTO GetEmployeeScheduleDetails(long employeeID, DateTime scehduleDate);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetEmployeeScheduleDetailsByTime?employeeID={employeeID}&scehduleDate={scehduleDate}&time={time}")]
        EmployeeScheduleDetailsDTO GetEmployeeScheduleDetailsByTime(long employeeID, DateTime scehduleDate, string time);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetEmployeeSummaryScheduleDetails?employeeID={employeeID}&scehduleDate={scehduleDate}")]
        EmployeeScheduleSummaryInfoDTO GetEmployeeSummaryScheduleDetails(long employeeID, DateTime scehduleDate);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetEmployeeScheduleByDespatchID?despatchID={despatchID}")]
        EmployeeScheduleDTO GetEmployeeScheduleByDespatchID(long despatchID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetDriverScheduleByScheduleID?scheduleID={scheduleID}")]
        EmployeeScheduleDTO GetDriverScheduleByScheduleID(long scheduleID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetEmployeeSchedules?employeeID={employeeID}&scehduleDate={scehduleDate}")]
        List<EmployeeScheduleDTO> GetEmployeeSchedules(long employeeID, DateTime scehduleDate);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetEmployeeDtails?employeeID={employeeID}")]
        EmployeeDTO GetEmployeeDtails(long employeeID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetMaidDtails?maidID={maidID}")]
        EmployeeDTO GetMaidDtails(long maidID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetEmployeeDtailsByLoginUserID?loginUserID={loginUserID}")]
        EmployeeDTO GetEmployeeDtailsByLoginUserID(long loginUserID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetNextScheduleDetails?employeeID={employeeID}&scehduleDate={scehduleDate}")]
        EmployeeScheduleDTO GetNextScheduleDetails(long employeeID, DateTime scehduleDate);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetUserDetails")]
        UserDTO GetUserDetails();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GenerateApiKey?uuid={uuid}&version={version}")]
        string GenerateApiKey(string uuid, string version);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "MaidScheduleUpdate")]
        OperationResultDTO MaidScheduleUpdate(EmployeeScheduleDTO schedule);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "DriverScheduleUpdate")]
        OperationResultDTO DriverScheduleUpdate(EmployeeScheduleDTO schedule);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveGeoLocation")]
        OperationResultDTO SaveGeoLocation(GeoLocationLogDTO schedule);
    }
}
