using System;
using System.Collections.Generic;
using Eduegate.Framework;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Payroll;
using Eduegate.Services.Payroll;
using Eduegate.Services.Contracts.HR.Payroll;
using Eduegate.Services.Contracts.CommonDTO;
using Eduegate.Services.Contracts.Contents;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.HR.Leaves;
using Eduegate.Services.Contracts.HR.Loan;
using Eduegate.Domain.Entity.HR.Loan;
using Eduegate.Domain.Entity.HR.Payroll;

namespace Eduegate.Service.Client.Direct.Payroll
{
    public class EmployeeServiceClient : IEmployeeService
    {
        EmployeeService service = new EmployeeService();

        public EmployeeServiceClient(CallContext context = null, Action<string> logger = null)
        {
            service.CallContext = context;
        }

        public EmployeeDTO GetEmployee(long employeeID)
        {
            return service.GetEmployee(employeeID);
        }

        public EmployeeDTO SaveEmployee(EmployeeDTO dtoList)
        {
            return service.SaveEmployee(dtoList);
        }

        public List<EmployeeDTO> GetEmployees()
        {
            return service.GetEmployees();
        }

        public EmployeeCatalogRelationDTO SaveEmployeeCatalogRelation(List<EmployeeCatalogRelationDTO> dtos)
        {
            return service.SaveEmployeeCatalogRelation(dtos);
        }

        public List<KeyValueDTO> GetEmployeeIdNameCatalogRelation(RelationTypes relationType, long relationID)
        {
            return service.GetEmployeeIdNameCatalogRelation(relationType, relationID);
        }

        public List<EmployeeDTO> SearchEmployee(string searchText, int pageSize)
        {
            return service.SearchEmployee(searchText, pageSize);
        }

        public List<EmployeeDTO> GetSalesMans()
        {
            return service.GetSalesMans();
        }

        public List<EmployeeDTO> GetEmployeesByRoles(int roleID)
        {
            return service.GetEmployeesByRoles(roleID);
        }

        public OperationResultWithIDsDTO GenerateSalarySlip(SalarySlipDTO salaryInfo)
        {
            return service.GenerateSalarySlip(salaryInfo);
        }
        public List<KeyValueDTO> GetEmployeeByBranch(int? branchID)
        {
            return service.GetEmployeeByBranch(branchID);
        }
        public OperationResultWithIDsDTO ModifySalarySlip(SalarySlipDTO dto)
        {
            return service.ModifySalarySlip(dto);
        }
        public OperationResultWithIDsDTO UpdateSalarySlipPDF(List<ContentFileDTO> salarySlipPDFInfo)
        {
            return service.UpdateSalarySlipPDF(salarySlipPDFInfo);
        }

        //public List<long> GetSalarySlipIDS(SalarySlipDTO salaryInfo)
        //{
        //    return service.GetSalarySlipIDS(salaryInfo);
        //}

        public List<SalarySlipEmployeeDTO> GetSalarySlipEmployeeData(int departmentID, int month, int year, long employeeID)
        {
            return service.GetSalarySlipEmployeeData(departmentID, month, year, employeeID);
        }

        public OperationResultDTO UpdateSalarySlipIsVerifiedData(long salarySlipID, bool isVerifiedStatus)
        {
            return service.UpdateSalarySlipIsVerifiedData(salarySlipID, isVerifiedStatus);
        }

        public OperationResultDTO PublishSalarySlips(List<SalarySlipDTO> salarySlipDTOList)
        {
            return service.PublishSalarySlips(salarySlipDTOList);
        }

        public OperationResultDTO UpdateSlipData(SalarySlipDTO salarySlipDTO)
        {
            return service.UpdateSlipData(salarySlipDTO);
        }

        public List<CalendarEntryDTO> GetWorkingHourDetailsByEmployee(long employeeID)
        {
            return service.GetWorkingHourDetailsByEmployee(employeeID);
        }

        public List<EmployeeTimeSheetApprovalDTO> GetPendingTimeSheetsDatas(long employeeID, DateTime? dateFrom, DateTime? dateTo)
        {
            return service.GetPendingTimeSheetsDatas(employeeID, dateFrom, dateTo);
        }

        public OperationResultDTO SavePendingTimesheetApprovalData(EmployeeTimeSheetApprovalDTO timesheetApprovalDTO)
        {
            return service.SavePendingTimesheetApprovalData(timesheetApprovalDTO);
        }

        public List<EmployeeTimeSheetApprovalDTO> GetApprovedTimeSheetsDatas(long employeeID, DateTime? dateFrom, DateTime? dateTo)
        {
            return service.GetApprovedTimeSheetsDatas(employeeID, dateFrom, dateTo);
        }

        public List<LeaveGroupTypeMapDTO> LeaveGroupChanges(int? leaveGroupID)
        {
            return service.LeaveGroupChanges(leaveGroupID);
        }
        public EmployeeDTO GetEmployeeDataByLogin(long? loginID)
        {
            return service.GetEmployeeDataByLogin(loginID);
        }

        public bool SaveWPS(WPSDetailDTO wpsDTO)
        {
            return service.SaveWPS(wpsDTO);
        }

        #region Employee Leave Salary / Final Settlement
        public OperationResultWithIDsDTO GetEmployeeDetailsToSettlement(EmployeeSettlementDTO employeeSettlementDTO)
        {
            return service.GetEmployeeDetailsToSettlement(employeeSettlementDTO);
        }
       
        public OperationResultWithIDsDTO SaveSalarySettlement(EmployeeSettlementDTO settlementInfo)
        {
            return service.SaveSalarySettlement(settlementInfo);
        }
        public EmployeeSettlementDTO GetSettlementDateDetails(EmployeeSettlementDTO settlementInfo)
        {
            return service.GetSettlementDateDetails(settlementInfo);
        }
        #endregion Employee Leave Salary / Final Settlement

        #region Loan 
        public LoanHeadDTO FillLoanData(long? loanRequestID, long? loanHeadID)
        {
            return service.FillLoanData(loanRequestID, loanHeadID);
        }
        public decimal? GetTotalSalaryAmount(long employeeID, DateTime? loanDate)
        {
            return service.GetTotalSalaryAmount(employeeID, loanDate);
        }
        #endregion Loan

        public List<KeyValueDTO> GetEmployeesByDepartment(long departmentID)
        {
            return service.GetEmployeesByDepartment(departmentID);
        }

        public List<KeyValueDTO> GetEmployeesByDepartmentBranch(long departmentID, int? branchID)
        {
            return service.GetEmployeesByDepartmentBranch(departmentID, branchID);
        }

        public KeyValueDTO GetSchoolPrincipal(byte schoolID)
        {
            return service.GetSchoolPrincipal(schoolID);
        }

        public KeyValueDTO GetSchoolVicePrincipal(byte schoolID)
        {
            return service.GetSchoolVicePrincipal(schoolID);
        }

        public KeyValueDTO GetSchoolHeadMistress(byte schoolID)
        {
            return service.GetSchoolHeadMistress(schoolID);
        }

        public SalarySlipDTO GetSalarySlipByID(long salarySlipID)
        {
            return service.GetSalarySlipByID(salarySlipID);
        }
        public SectorTicketAirfareDetailDTO GetSectorTicketAirfare(SectorTicketAirfareDTO sectorTicketAirfareDTO)
        {
            return service.GetSectorTicketAirfare(sectorTicketAirfareDTO);
        }
        public TicketEntitilementEntryDTO GetAirfareEntry(TicketEntitilementEntryDTO ticketEntitilementEntryDTO)
        {
            return service.GetAirfareEntry(ticketEntitilementEntryDTO);
        }
        public OperationResultWithIDsDTO GenerateLeaveSalaryProvision(EmployeeLSProvisionHeadDTO lsProvisionDTO)
        {
            return service.GenerateLeaveSalaryProvision(lsProvisionDTO);
        }
        public OperationResultWithIDsDTO GenerateESBProvision(EmployeeESBProvisionHeadDTO esbProvisionDTO)
        {
            return service.GenerateESBProvision(esbProvisionDTO);
        }

        public string UpdateESBAccountPostStatus(EmployeeESBProvisionHeadDTO dto)
        {
            return service.UpdateESBAccountPostStatus(dto);
        }
        public string UpdateLSAccountPostStatus(EmployeeLSProvisionHeadDTO dto)
        {
            return service.UpdateLSAccountPostStatus(dto);
        }
    }
}