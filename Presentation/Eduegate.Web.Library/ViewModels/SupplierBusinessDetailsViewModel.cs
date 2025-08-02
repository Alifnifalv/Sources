using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Frameworks.Mvc.Attributes;
using Eduegate.Web.Library.Common;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "SupplierBusiness", "CRUDModel.ViewModel.Business")]
    public class SupplierBusinessDetailsViewModel : BaseMasterViewModel
    {
        public SupplierBusinessDetailsViewModel()
        {
            BusinessAttachment = new List<BusinessAttachmentsViewModel>() { new BusinessAttachmentsViewModel() };
        }
        //[ControlType(Framework.Enums.ControlTypes.CheckBox)]
        //[CustomDisplay("MarketPlace")]
        public bool IsMarketPlace { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [LookUp("LookUps.BusinessTypes")]
        [CustomDisplay("Business Type")]
        [Select2("BusinessType", "Numeric", false)]
        public KeyValueViewModel BusinessType { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Year Established")]
        public string YearEstablished { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [CustomDisplay("")]
        public string NewLine1 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("CR Number")]
        public string VendorCR { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("CR Start Date")]
        public string CRStartDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("CR Expiry Date")]
        public string CRExpiry { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("TIN Number")]
        public string TINNumber { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [LookUp("LookUps.Countries")]
        [Select2("TaxJurisdictionCountry", "Numeric", false)]
        [CustomDisplay("Tax Jurisdiction country")]
        public KeyValueViewModel TaxJurisdictionCountry { get; set; }
        public int? TaxJurisdictionCountryID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[CustomDisplay("DUNS Number")]
        public string DUNSNumber { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [CustomDisplay("")]
        public string NewLine2 { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label, "fullwidth")]
        //[CustomDisplay("Trade/Business license :")]
        //public string Label2 { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("License Number")]
        public string LicenseNumber { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Start Date")]
        public string LicenseStartDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Expiry Date")]
        public string LicenseExpiryDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [CustomDisplay("")]
        public string NewLine3 { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label, "fullwidth")]
        //[CustomDisplay("Establishment License :")]
        //public string Label3 { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("ID Number")]
        public string EstIDNumber { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("First Issue Date")]
        public string EstFirstIssueDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Expiry Date")]
        public string EstExpiryDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Upload Documents")]
        public List<BusinessAttachmentsViewModel> BusinessAttachment { get; set; } 

    }

    [ContainerType(Framework.Enums.ContainerTypes.Grid, "BusinessAttachment", "CRUDModel.ViewModel.Business.BusinessAttachment")]
    [DisplayName("")]
    public class BusinessAttachmentsViewModel : BaseMasterViewModel
    {
        public BusinessAttachmentsViewModel()
        {

        }

        [ControlType(Framework.Enums.ControlTypes.AttachmentComponent, "text-left")]
        [CustomDisplay("Commercial/Business Registration Number")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.Documents, "BusinessRegistration", "")]
        public long? BusinessRegistration { get; set; }

        [ControlType(Framework.Enums.ControlTypes.AttachmentComponent, "text-left")]
        [CustomDisplay("Tax Identification Number")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.Documents, "TaxIdentificationNumber", "")]
        public long? TaxIdentificationNumber { get; set; }

        [ControlType(Framework.Enums.ControlTypes.AttachmentComponent, "text-left")]
        [CustomDisplay("DUNS Number")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.Documents, "DUNSNumberUpload", "")]
        public long? DUNSNumberUpload { get; set; }

        [ControlType(Framework.Enums.ControlTypes.AttachmentComponent, "text-left")]
        [CustomDisplay("Trade/Business license")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.Documents, "TradeLicense", "")]
        public long? TradeLicense { get; set; }

        [ControlType(Framework.Enums.ControlTypes.AttachmentComponent, "text-left")]
        [CustomDisplay("Establishment License")]
        [FileUploadInfo("Content/UploadContents", EduegateImageTypes.Documents, "EstablishmentLicense", "")]
        public long? EstablishmentLicense { get; set; }

    }
}