using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Services.Contracts.School.CertificateIssue;
using Eduegate.Services.Contracts.Settings;

namespace Eduegate.Domain
{
    public class CertificateBL
    {
        private static CertificateRepository repository = new CertificateRepository();
        private CallContext _context;

        public CertificateBL()
        {

        }

        public CertificateBL(CallContext context)
        {
            _context = context;
        }

        public List<ViewDTO> GetViews()
        {
            return Eduegate.Domain.Mappers.Common.ViewMapper.Mapper(_context).ToDTO(repository.GetViews());
        }

        public CertificateLogsDTO SaveCertificateLog(CertificateLogsDTO dto)
        {
            var mapper = Mappers.Common.CertificateMapper.Mapper(_context);
            return mapper.ToDTO(new CertificateRepository().SaveCertificateLog(mapper.ToEntity(dto)));
        }

        public CertificateTemplatesDTO SaveCertificateTemplate(CertificateTemplatesDTO dto)
        {
            var mapper = Mappers.Common.CertificateMapper.Mapper(_context);
            return mapper.ToDTO(new CertificateRepository().SaveCertificateTemplate(mapper.ToEntity(dto)));
        }


        public List<CertificateLogsDTO> GetCertificateLogDetail(long masterId)
        {
            var settingDetail = Mappers.Common.CertificateMapper.Mapper().ToDTO(repository.GetCertificateLogDetail(masterId));
            return settingDetail;
        }

        public CertificateLogsDTO GetCertificateLog(long masterId)
        {
            var settingDetail = Mappers.Common.CertificateMapper.Mapper().ToDTO(repository.GetCertificateLog(masterId));
            return settingDetail;
        }

        public List<CertificateTemplatesDTO> GetCertificateTemplateDetail(string reportName)
        {
            var settingDetail = Mappers.Common.CertificateMapper.Mapper().ToDTO(repository.GetCertificateTemplateDetail(reportName));
            return settingDetail;
        }

        public CertificateTemplatesDTO GetCertificateTemplate(string settingKey)
        {
            var settingDetail = Mappers.Common.CertificateMapper.Mapper().ToDTO(repository.GetCertificateTemplate(settingKey));
            return settingDetail;
        }

        public CertificateTemplatesDTO GetCertificateTemplateByID(long masterId)
        {
            var settingDetail = Mappers.Common.CertificateMapper.Mapper().ToDTO(repository.GetCertificateTemplateByID(masterId));
            return settingDetail;
        }

        public CertificateLogsDTO GetCertificateLogByID(long masterId)
        {
            var settingDetail = Mappers.Common.CertificateMapper.Mapper().ToDTO(repository.GetCertificateLogByID(masterId));
            return settingDetail;
        }
    }
}
