using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Services.Contracts.School.CertificateIssue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;


namespace Eduegate.Domain.Mappers.Common
{
    public class CertificateMapper 
    {
        public static CertificateMapper Mapper()
        {
            var mapper = new CertificateMapper();
            return mapper;
        }

        private CallContext _callContext;

        public static CertificateMapper Mapper(CallContext _context = null)
        {
            var mapper = new CertificateMapper();
            mapper._callContext = _context;
            return mapper;
        }

        public CertificateTemplatesDTO ToDTO(CertificateTemplate entity)
        {
            return new CertificateTemplatesDTO()
            {
                ReportName = entity.ReportName,
                CertificateName = entity.CertificateName,
                CertificateTemplateIID = entity.CertificateTemplateIID
            };
        }

        public List<CertificateTemplatesDTO> ToDTO(List<CertificateTemplate> entities)
        {
            var dtos = new List<CertificateTemplatesDTO>();

            foreach (var entity in entities)
            {
                dtos.Add(ToDTO(entity));
            }

            return dtos;
        }

        public CertificateTemplate ToEntity(CertificateTemplatesDTO dto)
        {
            return new CertificateTemplate()
            {
                CreatedBy = dto.CreatedBy,
                CreatedDate = dto.CreatedDate,
                ReportName = dto.ReportName,
                CertificateName = dto.CertificateName,
                CertificateTemplateIID = dto.CertificateTemplateIID
            };
        }

        public CertificateLogsDTO ToDTO(CertificateLog entity)
        {
            return new CertificateLogsDTO()
            {
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                CertificateTemplateIID = entity.CertificateTemplateIID,
                CertificateLogIID = entity.CertificateLogIID,
                ParameterValue = entity.ParameterValue
            };
        }

        public List<CertificateLogsDTO> ToDTO(List<CertificateLog> entities)
        {
            var dtos = new List<CertificateLogsDTO>();

            foreach (var entity in entities)
            {
                dtos.Add(ToDTO(entity));
            }

            return dtos;
        }

        public CertificateLog ToEntity(CertificateLogsDTO dto)
        {
            return new CertificateLog()
            {
                CreatedBy = dto.CreatedBy,
                CreatedDate = dto.CreatedDate,
                CertificateTemplateIID = dto.CertificateTemplateIID,
                CertificateLogIID = dto.CertificateLogIID,
                ParameterValue = dto.ParameterValue
            };
        }
    }
}
