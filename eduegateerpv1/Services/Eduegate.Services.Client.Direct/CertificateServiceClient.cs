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
using Eduegate.Services.Contracts.Settings;
using Eduegate.Services;
using Eduegate.Services.Contracts.School.CertificateIssue;

namespace Eduegate.Service.Client.Direct
{
    public class CertificateServiceClient : ICertificateService
    {

        CertificateService service = new CertificateService();
        public CertificateServiceClient(CallContext context = null, Action<string> logger = null)
        {
            service.CallContext = context;
        }

        public CertificateTemplatesDTO SaveCertificateTemplate(CertificateTemplatesDTO certificateLogsDTO)
        {
            return service.SaveCertificateTemplate(certificateLogsDTO);
        }
        public CertificateLogsDTO SaveCertificateLog(CertificateLogsDTO certificateLogsDTO)
        {
            return service.SaveCertificateLog(certificateLogsDTO);
        }

        public List<CertificateLogsDTO> GetCertificateLogDetail(long masterId)
        {
            return service.GetCertificateLogDetail(masterId);
        }
        public List<CertificateTemplatesDTO> GetCertificateTemplateDetail(string reportName)
        {
            return service.GetCertificateTemplateDetail(reportName);
        }


        public CertificateTemplatesDTO GetCertificateTemplate(string reportName)
        {
            return service.GetCertificateTemplate(reportName);
        }

        public CertificateTemplatesDTO GetCertificateTemplateByID(long masterId)
        {
            return service.GetCertificateTemplateByID(masterId);
        }

        public CertificateLogsDTO GetCertificateLog(long masterId)
        {
            return service.GetCertificateLog(masterId);
        }

        public CertificateLogsDTO GetCertificateLogByID(long masterId)
        {
            return service.GetCertificateLogByID(masterId);
        }

        public List<ViewDTO> GetViews()
        {
            return service.GetViews();
        }
    }
}
