using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.HR.Payroll;
using Eduegate.Services.Contracts.CommonDTO;
using Eduegate.Services.Contracts.Contents;
using Eduegate.Services.Contracts.Commons;
using System;
using Eduegate.Services.Contracts.HR.Leaves;

namespace Eduegate.Services.Contracts.Payroll
{
    [ServiceContract]
    public interface IEmployeeService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetEmployees")]
        List<EmployeeDTO> GetEmployees();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetEmployeesByRoles?roleID={roleID}")]
        List<EmployeeDTO> GetEmployeesByRoles(int roleID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetEmployee?employeeID={employeeID}")]
        EmployeeDTO GetEmployee(long employeeID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveEmployee")]
        EmployeeDTO SaveEmployee(EmployeeDTO dtoList);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveEmployeeCatalogRelation")]
        EmployeeCatalogRelationDTO SaveEmployeeCatalogRelation(List<EmployeeCatalogRelationDTO> dtos);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetEmployeeIdNameCatalogRelation?relationType={relationType}&relationID={relationID}")]
        List<KeyValueDTO> GetEmployeeIdNameCatalogRelation(RelationTypes relationType, long relationID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SearchEmployee?searchText={searchText}&pageSize={pageSize}")]
        List<EmployeeDTO> SearchEmployee(string searchText, int pageSize);


        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GenerateSalarySlip")]
        OperationResultWithIDsDTO GenerateSalarySlip(SalarySlipDTO salaryInfo);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "ModifySalarySlip")]
        OperationResultWithIDsDTO ModifySalarySlip(SalarySlipDTO dto);       

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "UpdateSalarySlipPDF")]
        OperationResultWithIDsDTO UpdateSalarySlipPDF(List<ContentFileDTO> salarySlipPDFInfo);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetSalarySlipEmployeeData?departmentID={departmentID}&Month={Month}&Year={Year})")]
        List<SalarySlipEmployeeDTO> GetSalarySlipEmployeeData(int departmentID, int month, int year);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "UpdateSalarySlipIsVerifiedData?salarySlipID={salarySlipID}&isVerifiedStatus={isVerifiedStatus}")]
        OperationResultDTO UpdateSalarySlipIsVerifiedData(long salarySlipID, bool isVerifiedStatus);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "PublishSalarySlips")]
        OperationResultDTO PublishSalarySlips(List<SalarySlipDTO> salarySlipDTOList);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "UpdateSlipData")]
        OperationResultDTO UpdateSlipData(SalarySlipDTO salarySlipDTO);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetWorkingHourDetailsByEmployee?employeeID={employeeID}")]
        List<CalendarEntryDTO> GetWorkingHourDetailsByEmployee(long employeeID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetPendingTimeSheetsDatas?employeeID={employeeID}&dateFrom={dateFrom}&dateTo={dateTo}")]
        List<EmployeeTimeSheetApprovalDTO> GetPendingTimeSheetsDatas(long employeeID, DateTime? dateFrom, DateTime? dateTo);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SavePendingTimesheetApprovalData")]
        OperationResultDTO SavePendingTimesheetApprovalData(EmployeeTimeSheetApprovalDTO timesheetApprovalDTO);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetApprovedTimeSheetsDatas?employeeID={employeeID}&dateFrom={dateFrom}&dateTo={dateTo}")]
        List<EmployeeTimeSheetApprovalDTO> GetApprovedTimeSheetsDatas(long employeeID, DateTime? dateFrom, DateTime? dateTo);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "LeaveGroupChanges?leaveGroupID={leaveGroupID}")]
        List<LeaveGroupTypeMapDTO> LeaveGroupChanges(int? leaveGroupID);      

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetEmployeeDataByLogin?loginID={loginID}")]
        EmployeeDTO GetEmployeeDataByLogin(long? loginID);

    }
}