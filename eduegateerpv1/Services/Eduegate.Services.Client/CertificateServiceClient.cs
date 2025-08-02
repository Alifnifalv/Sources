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
using Eduegate.Services.Contracts.School.CertificateIssue;

namespace Eduegate.Service.Client
{
    public class CertificateServiceClient : BaseClient, ICertificateService
    {
        private static string serviceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string certificateService = string.Concat(serviceHost, "CertificateService.svc/");

        public CertificateServiceClient(CallContext context = null, Action<string> logger = null)
            : base(context, logger)
        {
        }

        public CertificateTemplatesDTO SaveCertificateTemplate(CertificateTemplatesDTO certificateTemplatesDTO)
        {
            return ServiceHelper.HttpPostGetRequest<CertificateTemplatesDTO>(string.Format("{0}/{1}", certificateService, "SaveCertificateTemplate"), certificateTemplatesDTO, _callContext, _logger);
        }
        public CertificateLogsDTO SaveCertificateLog(CertificateLogsDTO certificateLogsDTO)
        {
            return ServiceHelper.HttpPostGetRequest<CertificateLogsDTO>(string.Format("{0}/{1}", certificateService, "SaveCertificateLog"), certificateLogsDTO, _callContext, _logger);
        }

        public List<CertificateLogsDTO> GetCertificateLogDetail(long masterId)
        {
            return ServiceHelper.HttpGetRequest<List<CertificateLogsDTO>>(string.Concat(certificateService, "GetCertificateLogDetail?masterId=", masterId), _callContext, _logger);
        }

        public List<CertificateTemplatesDTO> GetCertificateTemplateDetail(string reportName)
        {
            return ServiceHelper.HttpGetRequest<List<CertificateTemplatesDTO>>(string.Concat(certificateService, "GetCertificateTemplateDetail?reportName=", reportName), _callContext, _logger);
        }



        public CertificateLogsDTO GetCertificateLog(long masterId)
        {
            return ServiceHelper.HttpGetRequest<CertificateLogsDTO>(string.Concat(certificateService, "GetCertificateLog?masterId=", masterId), _callContext, _logger);
        }

        public CertificateTemplatesDTO GetCertificateTemplate(string reportName)
        {
            return ServiceHelper.HttpGetRequest<CertificateTemplatesDTO>(string.Concat(certificateService, "GetCertificateTemplate?reportName=", reportName), _callContext, _logger);
        }

        public CertificateTemplatesDTO GetCertificateTemplateByID(long masterId)
        {
            return ServiceHelper.HttpGetRequest<CertificateTemplatesDTO>(string.Concat(certificateService, "GetCertificateTemplateByID?masterId=", masterId), _callContext, _logger);
        }

        public CertificateLogsDTO GetCertificateLogByID(long masterId)
        {
            return ServiceHelper.HttpGetRequest<CertificateLogsDTO>(string.Concat(certificateService, "GetCertificateLogByID?masterId=", masterId), _callContext, _logger);
        }

        public List<ViewDTO> GetViews()
        {
            var uri = string.Format("{0}/GetViews", certificateService);
            return ServiceHelper.HttpGetRequest<List<ViewDTO>>(uri, _callContext, _logger);
        }
    }
}
