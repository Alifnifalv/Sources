using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.HR.Payroll;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "SupplierCertificatesUpload", "CRUDModel.ViewModel.CertificatesUpload")]
    [DisplayName("CertificationUpload")]
    public class SupplierCertificatesUploadViewModel : BaseMasterViewModel
    {
        public SupplierCertificatesUploadViewModel()
        {
            QualityCertifications = new List<SupplierQualityCertificationsViewModel>() { new SupplierQualityCertificationsViewModel() };
            EnvironmentalCompliance = new List<SupplierEnvironmentalComplianceAttachmentsViewModel>() { new SupplierEnvironmentalComplianceAttachmentsViewModel() };
            SocialCompliance = new List<SupplierSocialComplianceAttachmentsViewModel>() { new SupplierSocialComplianceAttachmentsViewModel() };
            HealthandSafetyCompliance = new List<SupplierHealthandSafetyComplianceAttachmentsViewModel>() { new SupplierHealthandSafetyComplianceAttachmentsViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Quality Certifications")]
        public List<SupplierQualityCertificationsViewModel> QualityCertifications { get; set; }
        
        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Environmental Compliance")]
        public List<SupplierEnvironmentalComplianceAttachmentsViewModel> EnvironmentalCompliance { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Social Compliance")]
        public List<SupplierSocialComplianceAttachmentsViewModel> SocialCompliance { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Health and Safety Compliance")]
        public List<SupplierHealthandSafetyComplianceAttachmentsViewModel> HealthandSafetyCompliance { get; set; }

    }

    [ContainerType(Framework.Enums.ContainerTypes.Grid, "QualityCertifications", "CRUDModel.ViewModel.CertificatesUpload.QualityCertifications")]
    [DisplayName(" ")]
    public class SupplierQualityCertificationsViewModel : BaseMasterViewModel
    {
        public SupplierQualityCertificationsViewModel()
        {

        }
        [ControlType(Framework.Enums.ControlTypes.AttachmentComponent, "text-left")]
        [CustomDisplay("ISO9001")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.Documents, "ISO9001", "")]
        public long? ISO9001 { get; set; }
        public string ISO9001String { get; set; }

        [ControlType(Framework.Enums.ControlTypes.AttachmentComponent, "text-left")]
        [CustomDisplay("Other relevant ISO certifications")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.Documents, "OtherRelevantISOCertifications", "")]

        public long? OtherRelevantISOCertifications { get; set; }
        public string OtherRelevantISOCertificationString { get; set; }
    }

    [ContainerType(Framework.Enums.ContainerTypes.Grid, "EnvironmentalCompliance", "CRUDModel.ViewModel.CertificatesUpload.EnvironmentalCompliance")]
    [DisplayName("")]
    public class SupplierEnvironmentalComplianceAttachmentsViewModel : BaseMasterViewModel
    {
        public SupplierEnvironmentalComplianceAttachmentsViewModel()
        {

        }
        [ControlType(Framework.Enums.ControlTypes.AttachmentComponent, "text-left")]
        [CustomDisplay("ISO14001")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.Documents, "ISO14001", "")]
        public long? ISO14001 { get; set; }
        public string ISO14001String { get; set; }

        [ControlType(Framework.Enums.ControlTypes.AttachmentComponent, "text-left")]
        [CustomDisplay("UploadOther environmental standards")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.Documents, "OtherEnviStandards", "")]
        public long? OtherEnviStandards { get; set; }
        public string OtherEnviStandardString { get; set; }

    }


    [ContainerType(Framework.Enums.ContainerTypes.Grid, "SocialCompliance", "CRUDModel.ViewModel.CertificatesUpload.SocialCompliance")]
    [DisplayName("")]
    public class SupplierSocialComplianceAttachmentsViewModel : BaseMasterViewModel
    {
        public SupplierSocialComplianceAttachmentsViewModel()
        {

        }
        [ControlType(Framework.Enums.ControlTypes.AttachmentComponent, "text-left")]
        [CustomDisplay("SA8000")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.Documents, "SA8000", "")]
        public long? SA8000 { get; set; }
        public string SA8000String { get; set; }

        [ControlType(Framework.Enums.ControlTypes.AttachmentComponent, "text-left")]
        [CustomDisplay("Other social responsibility standards")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.Documents, "OtherSocialRespoStandards", "")]

        public long? OtherSocialRespoStandards { get; set; }
        public string OtherSocialRespoStandardString { get; set; }

    }


    [ContainerType(Framework.Enums.ContainerTypes.Grid, "HealthandSafetyCompliance", "CRUDModel.ViewModel.CertificatesUpload.HealthandSafetyCompliance")]
    [DisplayName("")]
    public class SupplierHealthandSafetyComplianceAttachmentsViewModel : BaseMasterViewModel
    {
        public SupplierHealthandSafetyComplianceAttachmentsViewModel()
        {

        }

        [ControlType(Framework.Enums.ControlTypes.AttachmentComponent, "text-left")]
        [CustomDisplay("OHSAS18001")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.Documents, "OHSAS18001", "")]
        public long? OHSAS18001 { get; set; }
        public string OHSAS18001String { get; set; }

        [ControlType(Framework.Enums.ControlTypes.AttachmentComponent, "text-left")]
        [CustomDisplay("Other relevant health and safety standards")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.Documents, "OtherRelevantHealthSafetyStandards", "")]
        public long? OtherRelevantHealthSafetyStandards { get; set; }
        public string OtherRelevantHealthSafetyStandardString { get; set; }

    }

}
