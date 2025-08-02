using Eduegate.Domain.Payroll;
using Eduegate.Framework.Services;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.HR;
using Eduegate.Services.Contracts.Payroll;

namespace Eduegate.Services.Payroll
{
    public class EmploymentService : BaseService, IEmploymentService
    {
        public bool SaveEmployeeReq(EmploymentServiceDTO dto)
        {
            return new EmployeeBL(CallContext).SaveEmployeeReq(dto);
        }

        public EmploymentRequestDTO SaveEmploymentRequest(EmploymentRequestDTO dto)
        {
            return new EmployeeBL(CallContext).SaveEmploymentRequest(dto);
        }

        public KeyValueDTO ValidateEmploymentRequest(EmploymentRequestDTO dto, string fieldName = "")
        {
            return new EmployeeBL(CallContext).ValidateEmployeeRequest(dto, fieldName);
        }

        public EmploymentRequestDTO GetEmploymentRequest(long requestID)
        {
            return new EmployeeBL(CallContext).GetEmploymentRequest(requestID);
        }

        public List<HRV_CompanyMasterDTO> GetCompanyList()
        {
            return new EmployeeBL(CallContext).GetCompanyList();
        }

        public List<KeyValueDTO> GetVisaCompany(long shopCode)
        {
            return new EmployeeBL(CallContext).GetVisaCompany(shopCode);
        }

        public KeyValueDTO GetDefaultVisaCompany(long shopCode)
        {
            return new EmployeeBL(CallContext).GetDefaultVisaCompany(shopCode);
        }

        public string GetNextNo(int desigCode, string docType)
        {
            return new EmployeeBL(CallContext).getNextNo(desigCode, docType);
        }

        public EmploymentRequestDTO SaveWorkFlow(EmploymentRequestDTO dto)
        {
            return new EmployeeBL(CallContext).SaveWorkFlow(dto);
        }

        public List<KeyValueDTO> GetEmploymentRequestStatus(long empReqNo)
        {
            return new EmployeeBL(CallContext).GetEmploymentRequestStatus(empReqNo);
        }

        //public JobOpeningDTO GetJobOpening(string jobID)
        //{
        //    return new EmployeeBL(CallContext).GetJobOpening(jobID);
        //}

        //public List<JobOpeningDTO> GetJobOpenings(string filter)
        //{
        //    return new EmployeeBL(CallContext).GetJobOpenings(filter);
        //}

        public List<JobDepartmentDTO> GetJobDepartments(string filter)
        {
            return new EmployeeBL(CallContext).GetJobDepartments(filter);
        }

        //public JobOpeningDTO SaveJobOpening(JobOpeningDTO dto)
        //{
        //    return new EmployeeBL(CallContext).SaveJobOpening(dto);
        //}

        public bool JobProfileStatusChange(string applicationID, string status)
        {
            return new EmployeeBL(CallContext).ArchiveJobProfile(applicationID, status);
        }
    }
}
