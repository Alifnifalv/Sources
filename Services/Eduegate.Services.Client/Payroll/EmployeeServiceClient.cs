using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Payroll;
using Eduegate.Services.Contracts.HR.Payroll;
using Eduegate.Services.Contracts.CommonDTO;
using Eduegate.Services.Contracts.Contents;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.HR.Leaves;
using Microsoft.AspNetCore.Mvc;
using Eduegate.Services.Contracts.HR.Loan;
using Eduegate.Domain.Mappers.HR.Payroll;

namespace Eduegate.Service.Client.Payroll
{
    public class EmployeeServiceClient : BaseClient, IEmployeeService
    {
        private static string ServiceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string Service = string.Concat(ServiceHost, Eduegate.Framework.Helper.Constants.EMPLOYEE_SERVICE_NAME);

        public EmployeeServiceClient(CallContext context = null, Action<string> logger = null)
            : base(context, logger)
        {
        }

        public EmployeeDTO GetEmployee(long employeeID)
        {
            return ServiceHelper.HttpGetRequest<EmployeeDTO>(Service + "GetEmployee?employeeID=" + employeeID, _callContext);
        }

        public EmployeeDTO SaveEmployee(EmployeeDTO dtoList)
        {
            return ServiceHelper.HttpPostGetRequest<EmployeeDTO>(Service + "SaveEmployee", dtoList, _callContext);
        }

        public List<EmployeeDTO> GetEmployees()
        {
            return ServiceHelper.HttpGetRequest<List<EmployeeDTO>>(Service + "GetEmployees", _callContext);
        }

        public EmployeeCatalogRelationDTO SaveEmployeeCatalogRelation(List<EmployeeCatalogRelationDTO> dtos)
        {
            return JsonConvert.DeserializeObject<EmployeeCatalogRelationDTO>(ServiceHelper.HttpPostRequest<List<EmployeeCatalogRelationDTO>>(Service + "SaveEmployeeCatalogRelation", dtos, _callContext));
        }

        public List<KeyValueDTO> GetEmployeeIdNameCatalogRelation(RelationTypes relationType, long relationID)
        {
            return ServiceHelper.HttpGetRequest<List<KeyValueDTO>>(Service + "GetEmployeeIdNameCatalogRelation?relationType=" + relationType + "&relationID=" + relationID, _callContext);
        }

        public List<EmployeeDTO> SearchEmployee(string searchText, int pageSize)
        {
            return ServiceHelper.HttpGetRequest<List<EmployeeDTO>>(Service + "SearchEmployee?searchText=" + searchText + "&pageSize=" + pageSize, _callContext);
        }

        public List<EmployeeDTO> GetSalesMans()
        {
            return ServiceHelper.HttpGetRequest<List<EmployeeDTO>>(Service + "GetSalesMans", _callContext);
        }

        public List<EmployeeDTO> GetEmployeesByRoles(int roleID)
        {
            return ServiceHelper.HttpGetRequest<List<EmployeeDTO>>(Service + "GetEmployeesByRoles?roleID=" + roleID.ToString(), _callContext);
        }
        public List<SalarySlipDTO> SalaryDetails(int employyedId)
        {
            throw new NotImplementedException();
        }

        //string IEmployeeService.SaveSalaryDetail(SalarySlipDTO salaryInfo)
        //{
        //    throw new NotImplementedException();
        //}

        List<long> GetSalarySlipIDS(SalarySlipDTO salaryInfo)
        {
            throw new NotImplementedException();
        }

        public OperationResultWithIDsDTO GenerateSalarySlip(SalarySlipDTO salaryInfo)
        {
            throw new NotImplementedException();
        }
        public List<KeyValueDTO> GetEmployeeByBranch(int? branchID)
        {
            throw new NotImplementedException();
        }
        public OperationResultWithIDsDTO ModifySalarySlip(SalarySlipDTO dto)
        {
            throw new NotImplementedException();
        }
      
        public OperationResultWithIDsDTO UpdateSalarySlipPDF(List<ContentFileDTO> salarySlipPDFInfo)
        {
            throw new NotImplementedException();
        }

        public List<SalarySlipEmployeeDTO> GetSalarySlipEmployeeData(int departmentID, int Month, int Year)
        {
            throw new NotImplementedException();
        }

        public OperationResultDTO UpdateSalarySlipIsVerifiedData(long salarySlipID, bool isVerifiedStatus)
        {
            throw new NotImplementedException();
        }

        public OperationResultDTO PublishSalarySlips(List<SalarySlipDTO> salarySlipDTOList)
        {
            throw new NotImplementedException();
        }

        public OperationResultDTO UpdateSlipData(SalarySlipDTO salarySlipDTO)
        {
            throw new NotImplementedException();
        }

        public List<CalendarEntryDTO> GetWorkingHourDetailsByEmployee(long employeeID)
        {
            throw new NotImplementedException();
        }

        public List<EmployeeTimeSheetApprovalDTO> GetPendingTimeSheetsDatas(long employeeID, DateTime? dateFrom, DateTime? dateTo)
        {
            throw new NotImplementedException();
        }

        public OperationResultDTO SavePendingTimesheetApprovalData(EmployeeTimeSheetApprovalDTO timesheetApprovalDTO)
        {
            throw new NotImplementedException();
        }

        public List<EmployeeTimeSheetApprovalDTO> GetApprovedTimeSheetsDatas(long employeeID, DateTime? dateFrom, DateTime? dateTo)
        {
            throw new NotImplementedException();
        }
        public List<LeaveGroupTypeMapDTO> LeaveGroupChanges(int? LeaveGroupID)
        {
            throw new NotImplementedException();
        }

        public EmployeeDTO GetEmployeeDataByLogin(long? loginID)
        {
            throw new NotImplementedException();
        }

        public bool SaveWPS(WPSDetailDTO wpsDTO)
        {
            throw new NotImplementedException();
        }
        #region Employee Leave Salary / Final Settlement
        public OperationResultWithIDsDTO GetEmployeeDetailsToSettlement(EmployeeSettlementDTO employeeSettlementDTO)
        {
            throw new NotImplementedException();
        }
        public OperationResultWithIDsDTO SaveSalarySettlement(EmployeeSettlementDTO settlementInfo)
        {
            throw new NotImplementedException();
        }
        public EmployeeSettlementDTO GetSettlementDateDetails(EmployeeSettlementDTO settlementInfo)
        {
            throw new NotImplementedException();
        }
        #endregion Employee Leave Salary / Final Settlement

        #region Loan 
        public LoanHeadDTO FillLoanData(long? loanRequestID, long? loanHeadID)
        {
            throw new NotImplementedException();
        }
        public decimal? GetTotalSalaryAmount(long employeeID, DateTime? loanDate)
        {
            throw new NotImplementedException();
        }

        public List<SalarySlipEmployeeDTO> GetSalarySlipEmployeeData(int departmentID, int month, int year, long employeeID)
        {
            throw new NotImplementedException();
        }
        #endregion Loan

        public List<KeyValueDTO> GetEmployeesByDepartment(long departmentID)
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetEmployeesByDepartmentBranch(long departmentID, int? branchID)
        {
            throw new NotImplementedException();
        }

        public KeyValueDTO GetSchoolPrincipal(byte schoolID)
        {
            throw new NotImplementedException();
        }

        public KeyValueDTO GetSchoolVicePrincipal(byte schoolID)
        {
            throw new NotImplementedException();
        }

        public KeyValueDTO GetSchoolHeadMistress(byte schoolID)
        {
            throw new NotImplementedException();
        }

        public SalarySlipDTO GetSalarySlipByID(long salarySlipID)
        {
            throw new NotImplementedException();
        }
        public SectorTicketAirfareDetailDTO GetSectorTicketAirfare(SectorTicketAirfareDTO sectorTicketAirfareDTO)
        {
            throw new NotImplementedException();
        }
        public TicketEntitilementEntryDTO GetAirfareEntry(TicketEntitilementEntryDTO ticketEntitilementEntryDTO)
        {
            throw new NotImplementedException();
        }
        public OperationResultWithIDsDTO GenerateLeaveSalaryProvision(EmployeeLSProvisionHeadDTO lsProvisionDTO)
        {
            throw new NotImplementedException();
        }
        public OperationResultWithIDsDTO GenerateESBProvision(EmployeeESBProvisionHeadDTO esbProvisionDTO)
        {
            throw new NotImplementedException();
        }
        public string UpdateESBAccountPostStatus(EmployeeESBProvisionHeadDTO dto)
        {
            throw new NotImplementedException();
        }
        public string UpdateLSAccountPostStatus(EmployeeLSProvisionHeadDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}