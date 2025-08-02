using System.Collections.Generic;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.HR.Payroll;
using Eduegate.Services.Contracts.CommonDTO;
using Eduegate.Services.Contracts.Contents;
using Eduegate.Services.Contracts.Commons;
using System;
using Eduegate.Services.Contracts.HR.Leaves;
using Eduegate.Services.Contracts.HR.Loan;

namespace Eduegate.Services.Contracts.Payroll
{
    public interface IEmployeeService
    {
        List<EmployeeDTO> GetEmployees();

        List<EmployeeDTO> GetEmployeesByRoles(int roleID);

        EmployeeDTO GetEmployee(long employeeID);

        EmployeeDTO SaveEmployee(EmployeeDTO dtoList);

        EmployeeCatalogRelationDTO SaveEmployeeCatalogRelation(List<EmployeeCatalogRelationDTO> dtos);

        List<KeyValueDTO> GetEmployeeIdNameCatalogRelation(RelationTypes relationType, long relationID);

        List<EmployeeDTO> SearchEmployee(string searchText, int pageSize);


        OperationResultWithIDsDTO GenerateSalarySlip(SalarySlipDTO salaryInfo);

        OperationResultWithIDsDTO ModifySalarySlip(SalarySlipDTO dto);

        OperationResultWithIDsDTO UpdateSalarySlipPDF(List<ContentFileDTO> salarySlipPDFInfo);

        List<SalarySlipEmployeeDTO> GetSalarySlipEmployeeData(int departmentID, int month, int year, long employeeID);

        OperationResultDTO UpdateSalarySlipIsVerifiedData(long salarySlipID, bool isVerifiedStatus);

        OperationResultDTO PublishSalarySlips(List<SalarySlipDTO> salarySlipDTOList);

        OperationResultDTO UpdateSlipData(SalarySlipDTO salarySlipDTO);

        decimal? GetTotalSalaryAmount(long employeeID, DateTime? loanDate);

        List<CalendarEntryDTO> GetWorkingHourDetailsByEmployee(long employeeID);

        List<EmployeeTimeSheetApprovalDTO> GetPendingTimeSheetsDatas(long employeeID, DateTime? dateFrom, DateTime? dateTo);

        OperationResultDTO SavePendingTimesheetApprovalData(EmployeeTimeSheetApprovalDTO timesheetApprovalDTO);

        List<EmployeeTimeSheetApprovalDTO> GetApprovedTimeSheetsDatas(long employeeID, DateTime? dateFrom, DateTime? dateTo);

        List<LeaveGroupTypeMapDTO> LeaveGroupChanges(int? leaveGroupID);

        EmployeeDTO GetEmployeeDataByLogin(long? loginID);

        bool SaveWPS(WPSDetailDTO wpsDTO);

        #region Employee Leave Salary / Final Settlement
        OperationResultWithIDsDTO GetEmployeeDetailsToSettlement(EmployeeSettlementDTO employeeSettlementDTO);

        OperationResultWithIDsDTO SaveSalarySettlement(EmployeeSettlementDTO settlementInfo);

        EmployeeSettlementDTO GetSettlementDateDetails(EmployeeSettlementDTO settlementInfo);

        #endregion  Employee Leave Salary / Final Settlement

        #region Loan 
        LoanHeadDTO FillLoanData(long? loanRequestID, long? loanHeadID);

        #endregion Loan

        public List<KeyValueDTO> GetEmployeesByDepartment(long departmentID);

        public KeyValueDTO GetSchoolPrincipalOrHeadMistress(byte schoolID);

        public KeyValueDTO GetSchoolWisePrincipal(byte schoolID);

    }
}