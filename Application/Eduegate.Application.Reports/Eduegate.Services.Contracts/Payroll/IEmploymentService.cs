using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.HR;

namespace Eduegate.Services.Contracts.Payroll
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEmploymentService" in both code and config file together.
    [ServiceContract]
    public interface IEmploymentService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveEmployeeReq")]
        bool SaveEmployeeReq(EmploymentServiceDTO dtoList);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "ValidateEmploymentRequest?fieldName={fieldName}")]
        KeyValueDTO ValidateEmploymentRequest(EmploymentRequestDTO dto, string fieldName);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCompanyList")]
        List<HRV_CompanyMasterDTO> GetCompanyList();


        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveEmploymentRequest")]
        EmploymentRequestDTO SaveEmploymentRequest(EmploymentRequestDTO dto);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetEmploymentRequest?requestID={requestID}")]
        EmploymentRequestDTO GetEmploymentRequest(long requestID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetNextNo?desigCode={desigCode}&docType={docType}")]
        string GetNextNo(int desigCode, string docType);


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetVisaCompany?shopCode={shopCode}")]
        List<KeyValueDTO> GetVisaCompany(long shopCode);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetDefaultVisaCompany?shopCode={shopCode}")]
        KeyValueDTO GetDefaultVisaCompany(long shopCode);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveWorkFlow")]
        EmploymentRequestDTO SaveWorkFlow(EmploymentRequestDTO dto);


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetEmploymentRequestStatus?empReqNo={empReqNo}")]
        List<KeyValueDTO> GetEmploymentRequestStatus(long empReqNo);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetJobOpening?jobID={jobID}")]
        JobOpeningDTO GetJobOpening(string jobID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetJobOpenings?filter={filter}")]
        List<JobOpeningDTO> GetJobOpenings(string filter);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetJobDepartments?filter={filter}")]
        List<JobDepartmentDTO> GetJobDepartments(string filter);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveJobOpening")]
        JobOpeningDTO SaveJobOpening(JobOpeningDTO dto);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "JobProfileStatusChange?applicationID={applicationID}&status={status}")]
        bool JobProfileStatusChange(string applicationID, string status);
    }
}
