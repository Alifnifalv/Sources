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

namespace Eduegate.Service.Client.Payroll
{
    public class EmployementServiceClient : BaseClient, IEmploymentService
    {
        private static string ServiceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string Service = string.Concat(ServiceHost, Eduegate.Framework.Helper.Constants.EMPLOYMENT_SERVICE_NAME);
        public EmployementServiceClient(CallContext context = null, Action<string> logger = null)
            : base(context, logger)
        {
        }

        public bool SaveEmployeeReq(EmploymentServiceDTO dto)
        {
            var uri = string.Format("{0}/SaveEmployeeReq", Service);
            var result = (ServiceHelper.HttpPostRequest(uri, dto, _callContext));
            return JsonConvert.DeserializeObject<bool>(result);
        }

        public EmploymentRequestDTO SaveEmploymentRequest(EmploymentRequestDTO dto)
        {
            var uri = string.Format("{0}/SaveEmploymentRequest", Service);
            var result = (ServiceHelper.HttpPostRequest(uri, dto, _callContext));
            return JsonConvert.DeserializeObject<EmploymentRequestDTO>(result);
        }

        public KeyValueDTO ValidateEmploymentRequest(EmploymentRequestDTO dto, string fieldName = "")
        {
            var uri = string.Format("{0}/ValidateEmploymentRequest?fieldName={1}", Service, fieldName);
            dto.FieldNameToValidate = fieldName;
            var result = (ServiceHelper.HttpPostRequest(uri, dto, _callContext));
            return JsonConvert.DeserializeObject<KeyValueDTO>(result);
        }

        //public EmploymentServiceDTO GetEmploymentRequest(long RequestID)
        //{
        //    var uri = string.Format("{0}/GetEmploymentRequest", Service);
        //    var result = (ServiceHelper.HttpPostRequest(uri, RequestID, _callContext));
        //    return JsonConvert.DeserializeObject<EmploymentServiceDTO>(result);
        //}

        public List<HRV_CompanyMasterDTO> GetCompanyList()
        {
            var uri = string.Format("{0}/GetCompanyList", Service);
            var result = (ServiceHelper.HttpPostRequest(uri, _callContext));
            return JsonConvert.DeserializeObject<List<HRV_CompanyMasterDTO>>(result);
        }

        public bool SaveEmployeeReqNoParam()
        {

            return false;
        }

        public EmploymentRequestDTO GetEmploymentRequest(long RequestID)
        {

            string uri = string.Format("{0}/{1}?RequestID={2}", Service, "GetEmploymentRequest", RequestID);
            return ServiceHelper.HttpGetRequest<EmploymentRequestDTO>(uri, _callContext);
        }

        public string GetNextNo(int desigCode, string docType)
        {

            string uri = string.Format("{0}/{1}?desigCode={2}&docType={3}", Service, "GetNextNo", desigCode,docType);
            return ServiceHelper.HttpGetRequest<string>(uri, _callContext);
        }

        public List<KeyValueDTO> GetVisaCompany(long shopCode)
        {
            string uri = string.Format("{0}/{1}?shopCode={2}", Service, "GetVisaCompany", shopCode);
            return ServiceHelper.HttpGetRequest<List<KeyValueDTO>>(uri, _callContext);
        }

        public KeyValueDTO GetDefaultVisaCompany(long shopCode)
        {
            string uri = string.Format("{0}/{1}?shopCode={2}", Service, "GetDefaultVisaCompany", shopCode);
            return ServiceHelper.HttpGetRequest<KeyValueDTO>(uri, _callContext);
        }

        public EmploymentRequestDTO SaveWorkFlow(EmploymentRequestDTO dto)
        {
            var uri = string.Format("{0}/SaveWorkFlow", Service);
            return ServiceHelper.HttpPostGetRequest<EmploymentRequestDTO>(uri, dto, _callContext);
        }

        public List<KeyValueDTO> GetEmploymentRequestStatus(long empReqNo)
        {
            string uri = string.Format("{0}/{1}?empReqNo={2}", Service, "GetEmploymentRequestStatus", empReqNo);
            return ServiceHelper.HttpGetRequest<List<KeyValueDTO>>(uri, _callContext);
        }

        //public JobOpeningDTO GetJobOpening(string jobID)
        //{
        //    string uri = string.Format("{0}/{1}?jobID={2}", Service, "GetJobOpening", jobID);
        //    return ServiceHelper.HttpGetRequest<JobOpeningDTO>(uri, _callContext);
        //}

        public List<JobDepartmentDTO> GetJobDepartments(string filter)
        {
            string uri = string.Format("{0}/{1}?jobID={2}", Service, "GetJobDepartments", filter);
            return ServiceHelper.HttpGetRequest<List<JobDepartmentDTO>>(uri, _callContext);
        }

        //public List<JobOpeningDTO> GetJobOpenings(string filter)
        //{
        //    string uri = string.Format("{0}/{1}?jobID={2}", Service, "GetJobOpenings", filter);
        //    return ServiceHelper.HttpGetRequest<List<JobOpeningDTO>>(uri, _callContext);
        //}

        public JobOpeningDTO SaveJobOpening(JobOpeningDTO dto)
        {
            var uri = string.Format("{0}/SaveJobOpening", Service);
            return ServiceHelper.HttpPostGetRequest<JobOpeningDTO>(uri, dto, _callContext);
        }

        public bool JobProfileStatusChange(string applicationID, string status)
        {
            string uri = string.Format("{0}/{1}?applicationID={2}&status={3}", Service, "JobProfileStatusChange", applicationID, status);
            return ServiceHelper.HttpGetRequest<bool>(uri, _callContext);
        }
    }
}
