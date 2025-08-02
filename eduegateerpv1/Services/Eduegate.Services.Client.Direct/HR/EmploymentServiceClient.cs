using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.HR;
using Eduegate.Services.Contracts.Payroll;
using Eduegate.Services.Payroll;

namespace Eduegate.Service.Client.Direct.Payroll
{
    public class EmployementServiceClient : BaseClient, IEmploymentService
    {
        EmploymentService service = new EmploymentService();

        public EmployementServiceClient(CallContext context = null, Action<string> logger = null)
            : base(context, logger)
        {
        }

        public bool SaveEmployeeReq(EmploymentServiceDTO dto)
        {
            return service.SaveEmployeeReq(dto);
        }

        public EmploymentRequestDTO SaveEmploymentRequest(EmploymentRequestDTO dto)
        {
            return service.SaveEmploymentRequest(dto);
        }

        public KeyValueDTO ValidateEmploymentRequest(EmploymentRequestDTO dto, string fieldName = "")
        {
            return service.ValidateEmploymentRequest(dto, fieldName);
        }

        //public EmploymentServiceDTO GetEmploymentRequest(long RequestID)
        //{
        //    var uri = string.Format("{0}/GetEmploymentRequest", Service);
        //    var result = (ServiceHelper.HttpPostRequest(uri, RequestID, _callContext));
        //    return JsonConvert.DeserializeObject<EmploymentServiceDTO>(result);
        //}

        public List<HRV_CompanyMasterDTO> GetCompanyList()
        {
            return service.GetCompanyList();
        }

        public bool SaveEmployeeReqNoParam()
        {

            return false;
        }

        public EmploymentRequestDTO GetEmploymentRequest(long RequestID)
        {
            return service.GetEmploymentRequest(RequestID);
        }

        public string GetNextNo(int desigCode, string docType)
        {
            return service.GetNextNo(desigCode, docType);
        }

        public List<KeyValueDTO> GetVisaCompany(long shopCode)
        {
            return service.GetVisaCompany(shopCode);
        }

        public KeyValueDTO GetDefaultVisaCompany(long shopCode)
        {
            return service.GetDefaultVisaCompany(shopCode);
        }

        public EmploymentRequestDTO SaveWorkFlow(EmploymentRequestDTO dto)
        {
            return service.SaveWorkFlow(dto);
        }

        public List<KeyValueDTO> GetEmploymentRequestStatus(long empReqNo)
        {
            return service.GetEmploymentRequestStatus(empReqNo);
        }

        public JobOpeningDTO GetJobOpening(string jobID)
        {
            return service.GetJobOpening(jobID);
        }

        public List<JobOpeningDTO> GetJobOpenings(string filter)
        {
            return service.GetJobOpenings(filter);
        }

        public List<JobDepartmentDTO> GetJobDepartments(string filter)
        {
            return service.GetJobDepartments(filter);
        }

        public JobOpeningDTO SaveJobOpening(JobOpeningDTO dto)
        {
            return service.SaveJobOpening(dto);
        }

        public bool JobProfileStatusChange(string applicationID, string status)
        {
            return service.JobProfileStatusChange(applicationID, status);
        }
    }
}
