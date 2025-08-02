using System.Collections.Generic;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.HR;

namespace Eduegate.Services.Contracts.Payroll
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEmploymentService" in both code and config file together.
    public interface IEmploymentService
    {
        bool SaveEmployeeReq(EmploymentServiceDTO dtoList);

        KeyValueDTO ValidateEmploymentRequest(EmploymentRequestDTO dto, string fieldName);

        List<HRV_CompanyMasterDTO> GetCompanyList();

        EmploymentRequestDTO SaveEmploymentRequest(EmploymentRequestDTO dto);

        EmploymentRequestDTO GetEmploymentRequest(long requestID);

        string GetNextNo(int desigCode, string docType);

        List<KeyValueDTO> GetVisaCompany(long shopCode);

        KeyValueDTO GetDefaultVisaCompany(long shopCode);

        EmploymentRequestDTO SaveWorkFlow(EmploymentRequestDTO dto);

        List<KeyValueDTO> GetEmploymentRequestStatus(long empReqNo);

        JobOpeningDTO GetJobOpening(string jobID);

        List<JobOpeningDTO> GetJobOpenings(string filter);

        List<JobDepartmentDTO> GetJobDepartments(string filter);

        JobOpeningDTO SaveJobOpening(JobOpeningDTO dto);

        bool JobProfileStatusChange(string applicationID, string status);
    }
}