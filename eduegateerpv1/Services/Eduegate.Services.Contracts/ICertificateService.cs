using System.Collections.Generic;
using Eduegate.Services.Contracts.School.CertificateIssue;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IGlobalSettingService" in both code and config file together.
    public interface ICertificateService
    {
        //Save Certificate Log
        CertificateLogsDTO SaveCertificateLog(CertificateLogsDTO certificateLogsDTO);

        //Certificate Template
        CertificateTemplatesDTO SaveCertificateTemplate(CertificateTemplatesDTO certificateTemplatesDTO);

        List<CertificateLogsDTO> GetCertificateLogDetail(long masterId);

        //Get Certificate Log 
        List<CertificateTemplatesDTO> GetCertificateTemplateDetail(string reportName);

        CertificateLogsDTO GetCertificateLog(long masterId);

        CertificateTemplatesDTO GetCertificateTemplate(string reportName);

        CertificateTemplatesDTO GetCertificateTemplateByID(long masterId);

        CertificateLogsDTO GetCertificateLogByID(long masterId);
    }
}