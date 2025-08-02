using Eduegate.Domain.Payroll;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts.Payroll;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.HR.Payroll;
using Eduegate.Domain.Mappers.HR.Payroll;
using Eduegate.Services.Contracts.CommonDTO;
using Eduegate.Services.Contracts.Contents;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.HR.Leaves;
using Eduegate.Domain.Mappers.HR.Leaves;
using Eduegate.Services.Contracts.HR.Loan;
using Eduegate.Domain.Mappers.HR.Loans;
using Eduegate.Domain.Repository.HR.Employee;
namespace Eduegate.Services.Payroll
{
    public class EmployeeService : BaseService, IEmployeeService
    {
        public List<EmployeeDTO> GetEmployees()
        {
            return new EmployeeBL(CallContext).GetEmployees();
        }

        public EmployeeDTO GetEmployee(long employeeID)
        {
            return new EmployeeBL(CallContext).GetEmployee(employeeID);
        }

        public EmployeeDTO SaveEmployee(EmployeeDTO dto)
        {
            return new EmployeeBL(CallContext).SaveEmployee(dto);
        }

        public EmployeeCatalogRelationDTO SaveEmployeeCatalogRelation(List<EmployeeCatalogRelationDTO> dtos)
        {
            return new EmployeeBL(CallContext).SaveEmployeeCatalogRelation(dtos);
        }

        public List<KeyValueDTO> GetEmployeeIdNameCatalogRelation(RelationTypes relationType, long relationID)
        {
            return new EmployeeBL(CallContext).GetEmployeeIdNameCatalogRelation(relationType, relationID);
        }

        public List<EmployeeDTO> SearchEmployee(string searchText, int pageSize)
        {
            return new EmployeeBL(CallContext).SearchEmployee(searchText, pageSize);
        }

        public List<EmployeeDTO> GetSalesMans()
        {
            throw new NotImplementedException();
        }

        public List<EmployeeDTO> GetEmployeesByRoles(int roleID)
        {
            return new EmployeeBL(CallContext).GetEmployeesByRoles(roleID);
        }

        public List<EmployeeDTO> GetEmployeesBySkus(int roleID)
        {
            return new EmployeeBL(CallContext).GetEmployeesBySkus(1);
        }

        public OperationResultWithIDsDTO GenerateSalarySlip(SalarySlipDTO salaryInfo)
        {
            return SalarySlipMapper.Mapper(CallContext).GenerateSalarySlip(salaryInfo);
        }
        public OperationResultWithIDsDTO ModifySalarySlip(SalarySlipDTO dto)
        {
            return EditSalarySlipMapper.Mapper(CallContext).ModifySalarySlip(dto);
        }
        public OperationResultWithIDsDTO UpdateSalarySlipPDF(List<ContentFileDTO> salarySlipPDFInfo)
        {
            return SalarySlipMapper.Mapper(CallContext).UpdateSalarySlipPDF(salarySlipPDFInfo);
        }

        public decimal? GetTotalSalaryAmount(long employeeID, DateTime? loanDate)
        {
            return SalarySlipMapper.Mapper(CallContext).GetTotalSalaryAmount(employeeID, loanDate);
        }
        //public List<long> GetSalarySlipIDS(SalarySlipDTO salaryInfo)
        //{
        //    return SalarySlipMapper.GetSalarySlipIDS(salaryInfo);
        //}

        public List<SalarySlipEmployeeDTO> GetSalarySlipEmployeeData(int departmentID, int month, int year, long employeeID)
        {
            return SalarySlipPublishMapper.Mapper(CallContext).GetSalarySlipEmployeeData(departmentID, month, year, employeeID);
        }

        public OperationResultDTO UpdateSalarySlipIsVerifiedData(long salarySlipID, bool isVerifiedStatus)
        {
            return SalarySlipPublishMapper.Mapper(CallContext).UpdateSalarySlipIsVerifiedData(salarySlipID, isVerifiedStatus);
        }

        public OperationResultDTO PublishSalarySlips(List<SalarySlipDTO> salarySlipDTOList)
        {
            return SalarySlipPublishMapper.Mapper(CallContext).PublishSalarySlips(salarySlipDTOList);
        }

        public OperationResultDTO UpdateSlipData(SalarySlipDTO salarySlipDTO)
        {
            return SalarySlipPublishMapper.Mapper(CallContext).UpdateSlipData(salarySlipDTO);
        }

        public List<CalendarEntryDTO> GetWorkingHourDetailsByEmployee(long employeeID)
        {
            return EmployeeTimeSheetsWeeklyMapper.Mapper(CallContext).GetWorkingHourDetailsByEmployee(employeeID);
        }

        public List<EmployeeTimeSheetApprovalDTO> GetPendingTimeSheetsDatas(long employeeID, DateTime? dateFrom, DateTime? dateTo)
        {
            return EmployeeTimeSheetsWeeklyMapper.Mapper(CallContext).GetPendingTimeSheetsDatas(employeeID, dateFrom, dateTo);
        }

        public OperationResultDTO SavePendingTimesheetApprovalData(EmployeeTimeSheetApprovalDTO timesheetApprovalDTO)
        {
            return EmployeeTimeSheetApprovalMapper.Mapper(CallContext).SavePendingTimesheetApprovalData(timesheetApprovalDTO);
        }

        public List<EmployeeTimeSheetApprovalDTO> GetApprovedTimeSheetsDatas(long employeeID, DateTime? dateFrom, DateTime? dateTo)
        {
            return EmployeeTimeSheetApprovalMapper.Mapper(CallContext).GetApprovedTimeSheetsDatas(employeeID, dateFrom, dateTo);
        }
        public List<LeaveGroupTypeMapDTO> LeaveGroupChanges(int? leaveGroupID)
        {
            return LeaveGroupMapper.Mapper(CallContext).LeaveGroupChanges(leaveGroupID);
        }

        public EmployeeDTO GetEmployeeDataByLogin(long? loginID)
        {
            return new EmployeeBL(CallContext).GetEmployeeDataByLogin(loginID);
        }

        public bool SaveWPS(WPSDetailDTO wpsDTO)
        {
            return WPSMapper.Mapper(CallContext).SaveWPS(wpsDTO);
        }

        public OperationResultWithIDsDTO GetEmployeeDetailsToSettlement(EmployeeSettlementDTO employeeSettlementDTO)
        {
            return EmployeeSettlementMapper.Mapper(CallContext).GetEmployeeDetailsToSettlement(employeeSettlementDTO);
        }

        public OperationResultWithIDsDTO SaveSalarySettlement(EmployeeSettlementDTO settlementInfo)
        {
            return EmployeeSettlementMapper.Mapper(CallContext).SaveSalarySettlement(settlementInfo);
        }
        public EmployeeSettlementDTO GetSettlementDateDetails(EmployeeSettlementDTO settlementInfo)
        {
            return EmployeeSettlementMapper.Mapper(CallContext).GetSettlementDateDetails(settlementInfo);
        }

        #region Loan 
        public LoanHeadDTO FillLoanData(long? loanRequestID, long? loanHeadID)
        {
            return LoanMapper.Mapper(CallContext).FillLoanData(loanRequestID, loanHeadID);
        }
        #endregion Loan

        public List<KeyValueDTO> GetEmployeesByDepartment(long departmentID)
        {
            return EmployeeRoleMapper.Mapper(CallContext).GetEmployeesByDepartment(departmentID);
        }

        public KeyValueDTO GetSchoolPrincipalOrHeadMistress(byte schoolID)
        {
            return EmployeeRoleMapper.Mapper(CallContext).GetSchoolPrincipalOrHeadMistress(schoolID);
        }

        public KeyValueDTO GetSchoolWisePrincipal(byte schoolID)
        {
            return EmployeeRoleMapper.Mapper(CallContext).GetSchoolWisePrincipal(schoolID);
        }

    }
}