using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Extensions;
using System.Globalization;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Domain;
using Eduegate.Web.Library.Common;
using Eduegate.Services.Contracts.School.Students;
using Newtonsoft.Json;
using Eduegate.Services.Contracts.Accounts.Accounting;

namespace Eduegate.Web.Library.ViewModels
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "SupplierDetails", "CRUDModel.ViewModel")]
    [DisplayName("Supplier Details")]
    public class SupplierViewModel : BaseMasterViewModel
    {
        private static string dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

        public SupplierViewModel()
        {
            Business = new SupplierBusinessDetailsViewModel();
            KeyValueEmployees = new List<KeyValueViewModel>();
            Login = new LoginViewModel();
            ExternalSettings = new ExternalSettingsViewModel();
            CertificatesUpload = new SupplierCertificatesUploadViewModel();
            ReferenceAndPastPerformance = new SupplierRefAndPastPerformanceViewModel();
            SupplierBankAccounts = new BankAccountViewModel();
        }

        public long LoginID { get; set; }

        //[ContainerType(Framework.Enums.ContainerTypes.Tab, "SupplierLogin", "Login")]
        //[CustomDisplay("Login Info")]
        public LoginViewModel Login { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[CustomDisplay("Supplier ID")]
        public long SupplierIID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[LookUp("LookUps.Title")]
        ////[Required]
        //[CustomDisplay("Title")]
        public string TitleID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[CustomDisplay("Title Name")]
        //public string TitleName { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[CustomDisplay("First Name")]
        //[MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[CustomDisplay("Middle Name")]
        //[MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string MiddleName { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[CustomDisplay("Last Name")]
        //[MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string LastName { get; set; }

        public string SupplierName { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[CustomDisplay("Vendor CR")]
        //[MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string VendorCR { get; set; }

        // Date Field
        //[ControlType(Framework.Enums.ControlTypes.DatePicker)]
        //[CustomDisplay("CR Expiry")]
        public string CRExpiry { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Supplier Code")]
        public string SupplierCode { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [CustomDisplay("")]
        public string NewLine3 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Company Name")]
        //[MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string FirstName { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Company Short Name")]
        //[MaxLength(255, ErrorMessage = "Maximum Length should be within 255!")]
        public string VendorNickName { get; set; }


        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("Communication Address")]
        public string CommunicationAddress { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [CustomDisplay("Physical Address")]
        public string PhysicalAddress { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Phone Number")]
        //[MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        public string TelephoneNumber { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Email")]
        [RegularExpression("[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "Invalid Email Address")]
        public string SupplierEmail { get; set; }


        //[Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Website URL")]
        public string WebsiteURL { get; set; }


        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [CustomDisplay("Status")]
        [LookUp("LookUps.SupplierStatuses")]
        public string SupplierStatus { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextArea)]
        //[CustomDisplay("Address")]
        //[MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        public string SupplierAddress { get; set; }


        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[CustomDisplay("Company Location")]
        //[MaxLength(255, ErrorMessage = "Maximum Length should be within 255!")]
        public string CompanyLocation { get; set; }

        //[ContainerType(Framework.Enums.ContainerTypes.Tab, "SupplierContacts", "Contacts")]
        //[CustomDisplay("Contact Persons")]
        //public List<ContactsViewModel> Contacts { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "SupplierBusiness", "Business")]
        [CustomDisplay("Business Details")]
        public SupplierBusinessDetailsViewModel Business { get; set; }

        //[ContainerType(Framework.Enums.ContainerTypes.Tab, "SupplierBranch", "Branch")]
        //[CustomDisplay("Marketplace Settings")]
        //public SupplierBranchViewModel Branch { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "SupplierBankAccounts", "SupplierBankAccounts")]
        [CustomDisplay("Bank Accounts")]
        public BankAccountViewModel SupplierBankAccounts { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "ExternalSettings", "ExternalSettings")]
        [CustomDisplay("Product/Service Information")]
        public ExternalSettingsViewModel ExternalSettings { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "SupplierCertificatesUpload", "SupplierCertificatesUpload")]
        [CustomDisplay("Complians and Certifications")]
        public SupplierCertificatesUploadViewModel CertificatesUpload { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "ReferenceAndPastPerformance", "ReferenceAndPastPerformance")]
        [CustomDisplay("Reference and Past Performance")]
        public SupplierRefAndPastPerformanceViewModel ReferenceAndPastPerformance { get; set; }

        //[ContainerType(Framework.Enums.ContainerTypes.Tab, "SupplierEntitlements", "Entitlements")]
        //[CustomDisplay("Entitlements")]
        //public EntitlementViewModel Entitlements { get; set; }

        //[ContainerType(Framework.Enums.ContainerTypes.Tab, "Documents", "Documents")]
        //[CustomDisplay("Documents")]
        //[LazyLoad("Mutual/DocumentFile", "Mutual/GetDocumentFiles", "CRUDModel.ViewModel.Document")]
        //public DocumentViewViewModel Document { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[LookUp("LookUps.SupplierStatuses")]
        //[Required]
        //[CustomDisplay("SupplierStatus")]
        public string StatusID { get; set; }

        // Get Employee id and name for PM responsibe
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[LookUp("LookUps.OwnerEmployee")]
        //[Required]
        //[Select2("KeyValueEmployee", "Numeric", true)]
        //[CustomDisplay("PM Responsible")]
        public List<KeyValueViewModel> KeyValueEmployees { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.CheckBox)]
        //[CustomDisplay("Cheque")]
        public bool IsCheque { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.DropDown)]
        //[LookUp("LookUps.ChequeTypes")]
        ////[Required]
        //[CustomDisplay("ChequeTypes")]
        public string ChequeTypeID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        //[CustomDisplay("ChequeName")]
        public string ChequeName { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.CheckBox)]
        //[CustomDisplay("BankAccount")]
        public bool IsBankAccount { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.CheckBox)]
        //[CustomDisplay("Cash")]
        public bool IsCash { get; set; }

        //[ContainerType(Framework.Enums.ContainerTypes.Tab, "Entitlement PriceList", "PriceListEntitlement")]
        //[CustomDisplay("Entitlement Price List")]
        //public PriceListEntitlement PriceListEntitlement { get; set; }

        //[ContainerType(Framework.Enums.ContainerTypes.Tab, "PriceList", "PriceListsMap")]
        //[CustomDisplay("Price List")]
        //public PriceListViewModel PriceListMap { get; set; }


        //Account Settings
        //[ContainerType(Framework.Enums.ContainerTypes.Tab, "SupplierAccount", "SupplierAccountMaps")]
        //[CustomDisplay("SupplierAccountMaps")]
        //public SupplierAccountMapViewModel SupplierAccountMaps { get; set; }
        //Account Settings


        public bool IsMarketPlace { get; set; }
        public Nullable<long> BranchID { get; set; }
        public Nullable<long> ReceivingMethodID { get; set; }
        public Nullable<long> ReturnMethodID { get; set; }
        public string BranchName { get; set; }
        public string ReceivingMethodName { get; set; }
        public string ReturnMethodName { get; set; }
        public Nullable<decimal> Profit { get; set; }


        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as SupplierDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<SupplierViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<SupplierDTO, SupplierViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var splr = dto as SupplierDTO;
            var vm = Mapper<SupplierDTO, SupplierViewModel>.Map(splr);
            vm.SupplierIID = splr.SupplierIID;
            vm.SupplierStatus = splr.StatusID.HasValue ? splr.StatusID.ToString() : null;

            //Business Detail:
            vm.Business.VendorCR = splr.BusinessDetail.VendorCR;
            vm.Business.BusinessType = splr.BusinessDetail.BusinessTypeID.HasValue ?
                                        new KeyValueViewModel
                                        {
                                            Key = splr.BusinessDetail.BusinessType.Key.ToString(),
                                            Value = splr.BusinessDetail.BusinessType.Value.ToString()
                                        } : new KeyValueViewModel();
            vm.Business.YearEstablished = splr.BusinessDetail.YearEstablished;
            vm.Business.CRStartDate = splr.BusinessDetail.CRStartDateString;
            vm.Business.CRExpiry = splr.BusinessDetail.CRExpiryDateString;
            vm.Business.TINNumber = splr.BusinessDetail.TINNumber;
            vm.Business.TaxJurisdictionCountry = splr.BusinessDetail.TaxJurisdictionCountryID.HasValue ?
                                                new KeyValueViewModel
                                                {
                                                    Key = splr.BusinessDetail.TaxJurisdictionCountry.Key.ToString(),
                                                    Value = splr.BusinessDetail.TaxJurisdictionCountry.Value.ToString()
                                                } : new KeyValueViewModel();
            vm.Business.DUNSNumber = splr.BusinessDetail.DUNSNumber;

            //BusinessAttachments Attachments
            vm.Business.BusinessAttachment = new List<BusinessAttachmentsViewModel>(); // Initialize as an empty list 
            vm.Business.BusinessAttachment.Add(new BusinessAttachmentsViewModel()
            {
                BusinessRegistration = splr.Document.BusinessRegistration,
                TaxIdentificationNumber = splr.Document.TaxIdentificationNumber,
                DUNSNumberUpload = splr.Document.DUNSNumberUpload,
                TradeLicense = splr.Document.TradeLicense,
                EstablishmentLicense = splr.Document.EstablishmentLicense,
            });

            vm.Business.LicenseNumber = splr.BusinessDetail.LicenseNumber;
            vm.Business.LicenseStartDate = splr.BusinessDetail.LicenseStartDateString;
            vm.Business.LicenseExpiryDate = splr.BusinessDetail.LicenseExpiryDateString;
            vm.Business.EstIDNumber = splr.BusinessDetail.EstIDNumber;
            vm.Business.EstFirstIssueDate = splr.BusinessDetail.EstFirstIssueDateString;
            vm.Business.EstExpiryDate = splr.BusinessDetail.EstExpiryDateString;

            //Bank Accounts :

            vm.SupplierBankAccounts.BankName = splr.BankAccounts.BankName;
            vm.SupplierBankAccounts.BankAddress = splr.BankAccounts.BankAddress;
            vm.SupplierBankAccounts.AccountNumber = splr.BankAccounts.AccountNumber;
            vm.SupplierBankAccounts.IBAN = splr.BankAccounts.IBAN;
            vm.SupplierBankAccounts.SwiftCode = splr.BankAccounts.SwiftCode;
            vm.SupplierBankAccounts.IsCreditReference = splr.BankAccounts.IsCreditReference;
            vm.SupplierBankAccounts.PaymentMaxNoOfDaysAllowed = splr.BankAccounts.PaymentMaxNoOfDaysAllowed;

            //Bank Attachments
            vm.SupplierBankAccounts.BankAttachment = new List<BankAttachmentsViewModel>(); // Initialize as an empty list 
            vm.SupplierBankAccounts.BankAttachment.Add(new BankAttachmentsViewModel()
            {
                LetterConfirmationFromBank = splr.Document.LetterConfirmationFromBank,
                LatestAuditedFinancialStatements = splr.Document.LatestAuditedFinancialStatements,
                LiabilityInsurance = splr.Document.LiabilityInsurance,
                WorkersCompensationInsurance = splr.Document.WorkersCompensationInsurance,
            });

            //PrdctCategories Attach
            vm.ExternalSettings.ProductCategoriesAttach = new List<SupplierProductCategoriesAttachmentViewModel>(); // Initialize as an empty list 
            vm.ExternalSettings.ProductCategoriesAttach.Add(new SupplierProductCategoriesAttachmentViewModel()
            {
                PrdctCategories = splr.Document.PrdctCategories,
            });


            //Certifications
            vm.CertificatesUpload.QualityCertifications = new List<SupplierQualityCertificationsViewModel>(); // Initialize as an empty list 
            vm.CertificatesUpload.QualityCertifications.Add(new SupplierQualityCertificationsViewModel()
            {
                ISO9001 = splr.Document.ISO9001,
                OtherRelevantISOCertifications = splr.Document.OtherRelevantISOCertifications,
            });

            vm.CertificatesUpload.EnvironmentalCompliance = new List<SupplierEnvironmentalComplianceAttachmentsViewModel>(); // Initialize as an empty list 
            vm.CertificatesUpload.EnvironmentalCompliance.Add(new SupplierEnvironmentalComplianceAttachmentsViewModel()
            {
                ISO14001 = splr.Document.ISO14001,
                OtherEnviStandards = splr.Document.OtherEnviStandards,
            });

            vm.CertificatesUpload.SocialCompliance = new List<SupplierSocialComplianceAttachmentsViewModel>(); // Initialize as an empty list 
            vm.CertificatesUpload.SocialCompliance.Add(new SupplierSocialComplianceAttachmentsViewModel()
            {
                SA8000 = splr.Document.SA8000,
                OtherSocialRespoStandards = splr.Document.OtherSocialRespoStandards,
            });

            vm.CertificatesUpload.HealthandSafetyCompliance = new List<SupplierHealthandSafetyComplianceAttachmentsViewModel>(); // Initialize as an empty list 
            vm.CertificatesUpload.HealthandSafetyCompliance.Add(new SupplierHealthandSafetyComplianceAttachmentsViewModel()
            {
                OHSAS18001 = splr.Document.OHSAS18001,
                OtherRelevantHealthSafetyStandards = splr.Document.OtherRelevantHealthSafetyStandards,
            });

            //Reference and Past Performance
            vm.ReferenceAndPastPerformance.NamesOfClients = splr.NamesOfClients;
            vm.ReferenceAndPastPerformance.ClientContactInformation = splr.ClientContactInformation;
            vm.ReferenceAndPastPerformance.ClientProjectDetails = splr.ClientProjectDetails;
            vm.ReferenceAndPastPerformance.PrevContractScopeOfWork = splr.PrevContractScopeOfWork;
            vm.ReferenceAndPastPerformance.PrevValueOfContracts = splr.PrevValueOfContracts;
            vm.ReferenceAndPastPerformance.PrevContractDuration = splr.PrevContractDuration;

            return vm;
        }

        public static SupplierViewModel FromDTO(SupplierDTO dto)
        {
            SupplierViewModel supplierVM = new SupplierViewModel();

            Mapper<SupplierDTO, SupplierViewModel>.CreateMap();
            Mapper<ContactDTO, ContactsViewModel>.CreateMap();
            Mapper<LoginDTO, LoginViewModel>.CreateMap();

            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();

            Mapper<EntitlementDTO, EntitlementViewModel>.CreateMap();
            Mapper<EntitlementMapDTO, EntitlementMapViewModel>.CreateMap();

            Mapper<BankAccountDTO, BankAccountViewModel>.CreateMap();

            Mapper<EntityPropertyMapDTO, EmailViewModel>.CreateMap();
            Mapper<EntityPropertyMapDTO, FaxViewModel>.CreateMap();
            Mapper<EntityPropertyMapDTO, PhoneViewModel>.CreateMap();

            Mapper<ExternalSettingsDTO, ExternalSettingsViewModel>.CreateMap();
            Mapper<ExternalProductSettingsDTO, ExternalProductSettingsViewModel>.CreateMap();

            // Document viewmodel
            Mapper<DocumentViewDTO, DocumentViewViewModel>.CreateMap();
            Mapper<DocumentFileDTO, DocumentFileViewModel>.CreateMap();
            //Supplier Account Maps
            Mapper<SupplierAccountMapDTO, SupplierAccountMapViewModel>.CreateMap();
            Mapper<SupplierAccountEntitlmentMapsDTO, SupplierAccountEntitlementViewModel>.CreateMap();


            var vm = Mapper<SupplierDTO, SupplierViewModel>.Map(dto);

            //vm.Branch.BranchID = Convert.ToString(vm.BranchID);
            //vm.Branch.IsMarketPlace = vm.IsMarketPlace;
            //vm.Branch.Branch.Key = Convert.ToString(vm.BranchID);
            //vm.Branch.Branch.Value = vm.BranchName;
            //vm.Branch.ReceivingMethodID = Convert.ToString(vm.ReceivingMethodID);
            //vm.Branch.ReceivingMethods.Key = Convert.ToString(vm.ReceivingMethodID);
            //vm.Branch.ReceivingMethods.Value = vm.ReceivingMethodName;
            //vm.Branch.ReturnMethodID = Convert.ToString(vm.ReturnMethodID);
            //vm.Branch.ReturnMethods.Key = Convert.ToString(vm.ReturnMethodID);
            //vm.Branch.ReturnMethods.Value = vm.ReturnMethodName;
            //vm.Branch.Profit = vm.Profit;

            //vm.PriceListMap.PriceLists = new List<BranchPriceListViewModel>();
            //vm.PriceListMap.PriceLists.Add(new BranchPriceListViewModel()
            //{
            //    PriceListID = dto.PriceLists.PriceListID.ToString(),
            //    PriceDescription = dto.PriceLists.PriceDescription
            //});

            //vm.SupplierAccountMaps= SupplierAccountMapViewModel.SetSupplierAccountMapsViewModel( dto.SupplierAccountMaps, EntityTypes.Supplier);
            //vm.SupplierAccountMaps = SupplierAccountMapViewModel.MergeEntitlements(vm.SupplierAccountMaps);
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<SupplierViewModel, SupplierDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<SupplierViewModel, SupplierDTO>.Map(this);

            dto.SupplierIID = this.SupplierIID;
            dto.StatusID = string.IsNullOrEmpty(this.SupplierStatus) ? (byte?)null : byte.Parse(this.SupplierStatus);

            dto.BusinessDetail = new BusinessDetailDTO
            {
                BusinessType = this.Business.BusinessType?.Key != null ? new KeyValueDTO
                {
                    Key = this.Business.BusinessType.Key.ToString(),
                    Value = this.Business.BusinessType.Value.ToString(),
                } : new KeyValueDTO(),
                YearEstablished = this.Business.YearEstablished,
                VendorCR = this.Business.VendorCR,
                CRStartDateString = this.Business.CRStartDate,
                CRExpiryDateString = this.Business.CRExpiry, 
                CRStartDate =  string.IsNullOrEmpty(this.Business.CRStartDate) ? (DateTime?)null : DateTime.ParseExact(this.Business.CRStartDate, dateFormat, CultureInfo.InvariantCulture),
                CRExpiry = string.IsNullOrEmpty(this.Business.CRExpiry) ? (DateTime?)null : DateTime.ParseExact(this.Business.CRExpiry, dateFormat, CultureInfo.InvariantCulture),
                TINNumber = this.Business.TINNumber,

                TaxJurisdictionCountry = this.Business.TaxJurisdictionCountry?.Key != null ? new KeyValueDTO
                {
                    Key = this.Business.TaxJurisdictionCountry.Key.ToString(),
                    Value = this.Business.TaxJurisdictionCountry.Value.ToString(),
                } : new KeyValueDTO(),

                DUNSNumber = this.Business.DUNSNumber,
                LicenseNumber = this.Business.LicenseNumber,
                LicenseStartDateString = this.Business.LicenseStartDate,
                LicenseExpiryDateString = this.Business.LicenseExpiryDate,

                LicenseStartDate = string.IsNullOrEmpty(this.Business.LicenseStartDate) ? (DateTime?)null : DateTime.ParseExact(this.Business.LicenseStartDate, dateFormat, CultureInfo.InvariantCulture),
                LicenseExpiryDate = string.IsNullOrEmpty(this.Business.LicenseExpiryDate) ? (DateTime?)null : DateTime.ParseExact(this.Business.LicenseExpiryDate, dateFormat, CultureInfo.InvariantCulture),

                EstIDNumber = this.Business.EstIDNumber,
                EstFirstIssueDateString = this.Business.EstFirstIssueDate,
                EstExpiryDateString = this.Business.EstExpiryDate,

                EstFirstIssueDate = string.IsNullOrEmpty(this.Business.EstFirstIssueDate) ? (DateTime?)null : DateTime.ParseExact(this.Business.EstFirstIssueDate, dateFormat, CultureInfo.InvariantCulture),
                EstExpiryDate = string.IsNullOrEmpty(this.Business.EstExpiryDate) ? (DateTime?)null : DateTime.ParseExact(this.Business.EstExpiryDate, dateFormat, CultureInfo.InvariantCulture),

            };

            dto.BankAccounts = new BankAccountDTO
            {
                BankName = this.SupplierBankAccounts.BankName,
                BankAddress = this.SupplierBankAccounts.BankAddress,
                AccountNumber = this.SupplierBankAccounts.AccountNumber,
                IBAN = this.SupplierBankAccounts.IBAN,
                SwiftCode = this.SupplierBankAccounts.SwiftCode,
                IsCreditReference = this.SupplierBankAccounts.IsCreditReference,
                PaymentMaxNoOfDaysAllowed = this.SupplierBankAccounts.PaymentMaxNoOfDaysAllowed,
            };

            dto.ExternalSettings = new ExternalSettingsDTO
            {
                ProductorServiceDescription = this.ExternalSettings.ProductorServiceDescription,
                LeadTimeDays = this.ExternalSettings.LeadTimeDays,
                MinOrderQty = this.ExternalSettings.MinOrderQty,
                Warranty_GuaranteeInfo = this.ExternalSettings.Warranty_GuaranteeInfo,
                PricingInformation = this.ExternalSettings.PricingInformation,
            };

            dto.NamesOfClients = this.ReferenceAndPastPerformance.NamesOfClients;
            dto.ClientContactInformation = this.ReferenceAndPastPerformance.ClientContactInformation;
            dto.ClientProjectDetails = this.ReferenceAndPastPerformance.ClientProjectDetails;
            dto.PrevContractScopeOfWork = this.ReferenceAndPastPerformance.PrevContractScopeOfWork;
            dto.PrevValueOfContracts = this.ReferenceAndPastPerformance.PrevValueOfContracts;
            dto.PrevContractDuration = this.ReferenceAndPastPerformance.PrevContractDuration;
            dto.Declaration = this.ReferenceAndPastPerformance.Declaration;

            dto.Document = new DocumentViewDTO
            {
                SupplierID = this.SupplierIID,
                LetterConfirmationFromBank = this.SupplierBankAccounts?.BankAttachment[1]?.LetterConfirmationFromBank,
                LatestAuditedFinancialStatements = this.SupplierBankAccounts?.BankAttachment[1]?.LatestAuditedFinancialStatements,
                LiabilityInsurance = this.SupplierBankAccounts?.BankAttachment[1].LiabilityInsurance,
                WorkersCompensationInsurance = this.SupplierBankAccounts?.BankAttachment[1].WorkersCompensationInsurance,

                PrdctCategories = this.ExternalSettings.ProductCategoriesAttach[1].PrdctCategories,

                ISO9001 = this.CertificatesUpload?.QualityCertifications[1]?.ISO9001,
                OtherRelevantISOCertifications = this.CertificatesUpload?.QualityCertifications[1]?.OtherRelevantISOCertifications,

                ISO14001 = this.CertificatesUpload.EnvironmentalCompliance[1]?.ISO14001,
                OtherEnviStandards = this.CertificatesUpload.EnvironmentalCompliance[1]?.OtherEnviStandards,

                SA8000 = this.CertificatesUpload.SocialCompliance[1]?.SA8000,
                OtherSocialRespoStandards = this.CertificatesUpload.SocialCompliance[1]?.OtherSocialRespoStandards,

                OHSAS18001 = this.CertificatesUpload.HealthandSafetyCompliance[1]?.OHSAS18001,
                OtherRelevantHealthSafetyStandards = this.CertificatesUpload.HealthandSafetyCompliance[1]?.OtherRelevantHealthSafetyStandards,

                BusinessRegistration = this.Business?.BusinessAttachment[1]?.BusinessRegistration,
                TaxIdentificationNumber = this.Business?.BusinessAttachment[1]?.TaxIdentificationNumber,
                DUNSNumberUpload = this.Business?.BusinessAttachment[1]?.DUNSNumberUpload,
                TradeLicense = this.Business?.BusinessAttachment[1]?.TradeLicense,
                EstablishmentLicense = this.Business?.BusinessAttachment[1]?.EstablishmentLicense,

            };

            return dto;
        }


        //Old code
        //public static SupplierDTO ToDTO(SupplierViewModel vm)
        //{

        //    //vm.IsMarketPlace = vm.Branch.IsMarketPlace;
        //    //vm.BranchID = string.IsNullOrEmpty(vm.Branch.BranchID) ? 0 : Convert.ToInt64(vm.Branch.BranchID);
        //    //vm.ReceivingMethodID = Convert.ToInt64(vm.Branch.ReceivingMethods.Key);
        //    //vm.ReceivingMethodName = vm.Branch.ReceivingMethods.Value;
        //    //vm.ReturnMethodID = Convert.ToInt64(vm.Branch.ReturnMethods.Key);
        //    //vm.ReturnMethodName = vm.Branch.ReturnMethods.Value;
        //    //vm.Profit = vm.Branch.Profit;
        //    Mapper<SupplierViewModel, SupplierDTO>.CreateMap();
        //    Mapper<ContactsViewModel, ContactDTO>.CreateMap();
        //    Mapper<LoginViewModel, LoginDTO>.CreateMap();

        //    Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
        //    Mapper<EntitlementViewModel, EntitlementDTO>.CreateMap();
        //    Mapper<EntitlementMapViewModel, EntitlementMapDTO>.CreateMap();
        //    Mapper<BankAccountViewModel, BankAccountDTO>.CreateMap();

        //    Mapper<EmailViewModel, EntityPropertyMapDTO>.CreateMap();
        //    Mapper<FaxViewModel, EntityPropertyMapDTO>.CreateMap();
        //    Mapper<PhoneViewModel, EntityPropertyMapDTO>.CreateMap();

        //    Mapper<ExternalSettingsViewModel, ExternalSettingsDTO>.CreateMap();
        //    Mapper<ExternalProductSettingsViewModel, ExternalProductSettingsDTO>.CreateMap();

        //    //Document viewmodel
        //    Mapper<DocumentViewViewModel, DocumentViewDTO>.CreateMap();
        //    Mapper<DocumentFileViewModel, DocumentFileDTO>.CreateMap();

        //    //SupplierAccount Map
        //    Mapper<SupplierAccountMapViewModel, SupplierAccountMapDTO>.CreateMap();
        //    Mapper<SupplierAccountEntitlementViewModel, SupplierAccountEntitlmentMapsDTO>.CreateMap();
        //    //Mapper<List<SupplierAccountMapViewModel>, List<SupplierAccountMapsDTO>>.CreateMap();



        //    return Mapper<SupplierViewModel, SupplierDTO>.Map(vm);
        //}

        public static KeyValueViewModel ToKeyValueViewModel(SupplierDTO dto)
        {
            if (dto != null)
            {
                return new KeyValueViewModel()
                {
                    Key = dto.SupplierIID.ToString(),
                    Value = string.Concat(dto.FirstName, " ", dto.MiddleName, " ", dto.LastName),
                };
            }
            else return new KeyValueViewModel();
        }

        //private static SupplierViewModel SetSupplierAccountMapsViewModel(SupplierViewModel vm, SupplierDTO dto)
        //{
        //    vm.SupplierAccountMaps.SupplierAccountEntitlements = new List<SupplierAccountEntitlementViewModel>();
        //    foreach (SupplierAccountEntitlmentMapsDTO item in  dto.SupplierAccountMaps.SupplierAccountEntitlements)
        //    {
        //        if (item.EntitlementID == null)
        //        {
        //            vm.SupplierAccountMaps.SupplierAccount = new KeyValueViewModel() { Key = item.Account.AccountID.ToString(), Value = item.Account.AccountName };
        //            vm.SupplierAccountMaps.SupplierAccountId = item.Account.AccountID;
        //            vm.SupplierAccountMaps.SupplierAccountMapIID = item.SupplierAccountMapIID;

        //        }
        //        else
        //        {
        //            SupplierAccountEntitlementViewModel viewModel = new SupplierAccountEntitlementViewModel();
        //            viewModel.AccountID = item.Account.AccountID;
        //            viewModel.AccountName = item.Account.AccountName;
        //            viewModel.Alias = item.Account.Alias;
        //            viewModel.EntitlementID = item.EntitlementID;
        //            viewModel.EntitlementName = item.EntitlementName;
        //            viewModel.GroupID = item.Account.AccountGroup.AccountGroupID;
        //            viewModel.ParentAccountID = item.Account.ParentAccount.AccountID;
        //            viewModel.SupplierAccountMapIID = item.SupplierAccountMapIID;
        //            viewModel.SupplierID = item.SupplierID;


        //            vm.SupplierAccountMaps.SupplierAccountEntitlements.Add(viewModel);
        //        }

        //    }
        //    return vm;
        //}

        //private static SupplierViewModel MergeEntitlements(SupplierViewModel supplierModel)
        //{
        //    var entitlements = new Service.Client.MutualServiceClient(null).GetEntityTypeEntitlementByEntityType(Eduegate.Services.Contracts.Enums.EntityTypes.Supplier);
        //    if (supplierModel.SupplierAccountMaps == null)
        //    {
        //        supplierModel.SupplierAccountMaps = new SupplierAccountMapViewModel();
        //    }
        //    if (supplierModel.SupplierAccountMaps.SupplierAccountEntitlements == null)
        //    {
        //        supplierModel.SupplierAccountMaps.SupplierAccountEntitlements = new List<SupplierAccountEntitlementViewModel>();
        //    }

        //    //supplierModel.SupplierAccountMaps.SupplierAccountEntitlements = new SupplierServiceClient().get

        //    foreach (KeyValueDTO keyValue in entitlements)
        //    {
        //        short entitlementId = Convert.ToSByte(keyValue.Key);
        //        var item = supplierModel.SupplierAccountMaps.SupplierAccountEntitlements.Where(b => b.EntitlementID == entitlementId).FirstOrDefault();
        //        if (item == null)
        //        {
        //            supplierModel.SupplierAccountMaps.SupplierAccountEntitlements.Add(new SupplierAccountEntitlementViewModel
        //            { EntitlementID = entitlementId, EntitlementName = keyValue.Value });
        //        }
        //    }
        //    return supplierModel;
        //}

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<SupplierDTO>(jsonString);
        }

    }
}