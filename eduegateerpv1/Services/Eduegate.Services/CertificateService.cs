using System.ServiceModel;
using Eduegate.Domain;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.School.CertificateIssue;
using Eduegate.Services.Contracts.Settings;

namespace Eduegate.Services
{
    public class CertificateService : BaseService, ICertificateService
    {

        public CertificateTemplatesDTO SaveCertificateTemplate(CertificateTemplatesDTO certificateLogsDTO)
        {
            try
            {
                return new CertificateBL(CallContext).SaveCertificateTemplate(certificateLogsDTO);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<CertificateTemplatesDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }
        public CertificateLogsDTO SaveCertificateLog(CertificateLogsDTO certificateLogsDTO)
        {
            try
            {
                return new CertificateBL(CallContext).SaveCertificateLog(certificateLogsDTO);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<CertificateLogsDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }



        public List<CertificateLogsDTO> GetCertificateLogDetail(long masterId)
        {
            try
            {
                var settingDetail = new CertificateBL(CallContext).GetCertificateLogDetail(masterId);
                Eduegate.Logger.LogHelper<ProductDetail>.Info("Service Result : " + settingDetail.ToString());
                return settingDetail;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetail>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }


        public List<CertificateTemplatesDTO> GetCertificateTemplateDetail(string reportName)
        {
            try
            {
                var settingDetail = new CertificateBL(CallContext).GetCertificateTemplateDetail(reportName);
                Eduegate.Logger.LogHelper<ProductDetail>.Info("Service Result : " + settingDetail.ToString());
                return settingDetail;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetail>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }



        public List<ViewDTO> GetViews()
        {
            try
            {
                return new Domain.Setting.SettingBL(CallContext).GetViews();
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ViewDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }




        public CertificateTemplatesDTO GetCertificateTemplate(string reportName)
        {
            try
            {
                var settingDetail = new CertificateBL(CallContext).GetCertificateTemplate(reportName);
                Eduegate.Logger.LogHelper<ProductDetail>.Info("Service Result : " + settingDetail.ToString());
                return settingDetail;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetail>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public CertificateLogsDTO GetCertificateLog(long masterId)
        {
            try
            {
                var settingDetail = new CertificateBL(CallContext).GetCertificateLog(masterId);
                Eduegate.Logger.LogHelper<ProductDetail>.Info("Service Result : " + settingDetail.ToString());
                return settingDetail;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetail>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }




        public CertificateTemplatesDTO GetCertificateTemplateByID(long masterId)
        {
            try
            {
                var settingDetail = new CertificateBL(CallContext).GetCertificateTemplateByID(masterId);
                Eduegate.Logger.LogHelper<ProductDetail>.Info("Service Result : " + settingDetail.ToString());
                return settingDetail;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetail>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public CertificateLogsDTO GetCertificateLogByID(long masterId)
        {
            try
            {
                var settingDetail = new CertificateBL(CallContext).GetCertificateLogByID(masterId);
                Eduegate.Logger.LogHelper<ProductDetail>.Info("Service Result : " + settingDetail.ToString());
                return settingDetail;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetail>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }
    }
}
