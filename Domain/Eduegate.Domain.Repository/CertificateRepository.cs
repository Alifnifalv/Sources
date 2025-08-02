using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Repository
{
    public class CertificateRepository
    {
        public List<View> GetViews()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.Views.AsNoTracking().ToList();
            }
        }

        public CertificateTemplate SaveCertificateTemplate(CertificateTemplate entity)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var existing = dbContext.CertificateTemplates.Where(a => a.ReportName == entity.ReportName && a.CertificateName == entity.CertificateName).AsNoTracking().FirstOrDefault();

                if (existing == null)
                {
                    dbContext.CertificateTemplates.Add(entity);
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    existing.ReportName = entity.ReportName;
                    existing.ReportName = entity.CertificateName;
                    existing.UpdatedBy = entity.UpdatedBy;
                    existing.UpdatedDate = entity.UpdatedDate;
                    dbContext.Entry(existing).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                dbContext.SaveChanges();
            }
            return entity;
        }

        public CertificateLog SaveCertificateLog(CertificateLog entity)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                dbContext.CertificateLogs.Add(entity);
                dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                dbContext.SaveChanges();
            }

            return entity;
        }

        public List<CertificateLog> GetCertificateLogDetail(long loginID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var certificateLog = (from CertificateLog in dbContext.CertificateLogs select CertificateLog).AsNoTracking().ToList();
                if (certificateLog == null)
                    return new List<CertificateLog>();
                else
                    return certificateLog;

            }
        }



        public List<CertificateTemplate> GetCertificateTemplateDetail(string reportName)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var CertificateTemp = (from CertificateTemplate in dbContext.CertificateTemplates
                        where CertificateTemplate.ReportName == reportName
                        select CertificateTemplate).AsNoTracking().ToList();
                if (CertificateTemp == null)
                    return new List<CertificateTemplate>();
                else
                    return CertificateTemp;
            }
        }

        public CertificateTemplate GetCertificateTemplate(string reportName)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var certificateTemp = (from certificateTemplate in dbContext.CertificateTemplates where certificateTemplate.ReportName.Equals(reportName) select certificateTemplate).AsNoTracking().FirstOrDefault();
                if (certificateTemp == null)
                    return new CertificateTemplate();
                else
                    return certificateTemp;
            }
        }

        public CertificateLog GetCertificateLog(long templateIID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var certificateLog = (from certificateLogs in dbContext.CertificateLogs where certificateLogs.CertificateTemplateIID.Equals(templateIID) select certificateLogs).FirstOrDefault();
                if (certificateLog == null)
                    return new CertificateLog();
                else
                    return certificateLog;
            }
        }


        public CertificateTemplate GetCertificateTemplateByID(long masterId)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var certificateTemp = (from certificateTemplate in dbContext.CertificateTemplates where certificateTemplate.CertificateTemplateIID.Equals(masterId) select certificateTemplate).AsNoTracking().FirstOrDefault();
                if (certificateTemp == null)
                    return new CertificateTemplate();
                else
                    return certificateTemp;
            }
        }

        public CertificateLog GetCertificateLogByID(long masterId)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var certificateLog = (from CertificateLog in dbContext.CertificateLogs where CertificateLog.CertificateLogIID.Equals(masterId) select CertificateLog).AsNoTracking().FirstOrDefault();
                if (certificateLog == null)
                    return new CertificateLog();
                else
                    return certificateLog;

            }
        }

    }
}
