using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.School.CertificateIssue;
using Eduegate.Services.Contracts.Settings;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IGlobalSettingService" in both code and config file together.
    [ServiceContract]
    public interface ICertificateService
    {

        //Save Certificate Log
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveCertificateLog")]
        CertificateLogsDTO SaveCertificateLog(CertificateLogsDTO certificateLogsDTO);

        //Certificate Template
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        RequestFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveCertificateTemplate")]
        CertificateTemplatesDTO SaveCertificateTemplate(CertificateTemplatesDTO certificateTemplatesDTO);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCertificateLogDetail?masterId={masterId}")]
        List<CertificateLogsDTO> GetCertificateLogDetail(long masterId);

        //Get Certificate Log 
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCertificateTemplateDetail?reportName={reportName}")]
        List<CertificateTemplatesDTO> GetCertificateTemplateDetail(string reportName);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCertificateLog?masterId={masterId}")]
        CertificateLogsDTO GetCertificateLog(long masterId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCertificateTemplate?reportName={reportName}")]
        CertificateTemplatesDTO GetCertificateTemplate(string reportName);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCertificateTemplateByID?masterId={masterId}")]
        CertificateTemplatesDTO GetCertificateTemplateByID(long masterId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCertificateLogByID?masterId={masterId}")]
        CertificateLogsDTO GetCertificateLogByID(long masterId);


    }
}
