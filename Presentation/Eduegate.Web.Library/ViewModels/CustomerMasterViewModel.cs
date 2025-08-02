using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Admin;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Web.Library.ViewModels.Settings;
using Eduegate.Framework.Contracts.Common.Enums;

namespace Eduegate.Web.Library.ViewModels
{
    //[Bind(Exclude = "Status")]
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "CustomerDetails", "CRUDModel.ViewModel")]
    [DisplayName("Customer Details")]
    public class CustomerMasterViewModel : BaseMasterViewModel
    {
        private static string dateTimeFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateTimeFormat");
        public CustomerMasterViewModel()
        {            
            Login = new LoginViewModel();
            Entitlements = new EntitlementViewModel();
            Document = new DocumentViewViewModel();
            //BankAccounts = new List<BankAccountViewModel>();
            PriceListEntitlement = new PriceListEntitlement();
            IsOfflineCustomer = true; // set default value true
            ExternalSettings = new ExternalSettingsViewModel();
            Settings = new CustomerSettingsViewModel();
            SupplierAccountMaps = new SupplierAccountMapViewModel(EntityTypes.Customer);
            
            Title = new KeyValueViewModel() { Key = "1", Value = Convert.ToString(Services.Contracts.Enums.CustomerTitles.Mr) };
            IsDifferentBillingAddress = true;
            IsTermsAndConditions = true;
            IsSubscribeOurNewsLetter = true;
            Contacts = new ContactsViewModel() ;
        }
       
        public Nullable<long> LoginID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Customer ID")]
        public long CustomerIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [DisplayName("Is Offline Customer")]
        public Nullable<bool> IsOfflineCustomer { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("Title", "Numeric", false)]
        //[LookUp("LookUps.Title")]
        //[DisplayName("Title")]
        public KeyValueViewModel Title { get; set; }
        public string TitleID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-blur='CustomerFirstNameChange(CRUDModel.ViewModel.FirstName,CRUDModel.ViewModel.Contacts)'")]
        [DisplayName("First Name")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string FirstName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Middle Name")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string MiddleName {get;set;}

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, attribs: "ng-blur='CustomerLastNameChange(CRUDModel.ViewModel.LastName,CRUDModel.ViewModel.Contacts)'")]
        [DisplayName("Last Name")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 50!")]
        public string LastName { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Telephone")]
        [MaxLength(13, ErrorMessage = "Maximum Length should be within 13!")]
        public string TelephoneNumber { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[LazyLoad("", "Customer/GetEmployees", "LookUps.OwnerEmployee")]
        //[Select2("ProductManagerID", "Numeric", false, "OnChangeSelect2")]
        //[DisplayName("Product Manager")]
        public KeyValueViewModel ProductManagerID { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("CustomerGroup", "Numeric", false)]
        [DisplayName("Group")]
        [LookUp("LookUps.CustomerGroup")]
        public KeyValueViewModel Group { get; set; }
        public int GroupID { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("CustomerStatus", "Numeric", false)]
        //[DisplayName("Status")]
        //[LookUp("LookUps.CustomerStatus")]
        public KeyValueViewModel Status { get; set; }
        public int StatusID { get; set; }

        public bool IsPassword { get; set; }        
        public string CustomerName { get; set; }


        //Account Settings
        //[ContainerType(Framework.Enums.ContainerTypes.Tab, "SupplierAccount", "SupplierAccountMaps")]
        //[DisplayName("Customer Accounts")]
        public SupplierAccountMapViewModel SupplierAccountMaps { get; set; }
        //Account Settings

        //public Eduegate.Services.Contracts.Enums.CustomerStatus? Status
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(StatusID))
        //            return Eduegate.Services.Contracts.Enums.CustomerStatus.Active;
        //        else
        //            return (Eduegate.Services.Contracts.Enums.CustomerStatus)int.Parse(StatusID);
        //    }

        //    set
        //    {
        //        StatusID = ((int)value).ToString();
        //    }
        //}

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Customer CR")]
        [MaxLength(50)]
        // set default value today date
        public string CustomerCR { get; set; }

        //[Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [DisplayName("CR Expiry date")]
        // set default value today date
        public string CRExpiryDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Civil ID")]
        [MaxLength(100)]
        public string CivilIDNumber { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [LazyLoad("", "Inventories/Customer/GetCustomerByCustomerIdAndCR", "LookUps.ParentCustomers")]
        [Select2("ParentCustomerID", "Numeric", false, "OnChangeSelect2")]
        [DisplayName("Parent Customer")]
        public KeyValueViewModel ParentCustomerID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[LazyLoad("", "Supplier/GetSupplierBySupplierIdAndCR", "LookUps.SupplierMaps")]
        //[Select2("SupplierID", "Numeric", false, "OnChangeSelect2")]
        //[DisplayName("SupplierMaps")]
        public KeyValueViewModel SupplierID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [DisplayName("Has different billing address.")]
        public bool IsDifferentBillingAddress { get; set; }
        //[ControlType(Framework.Enums.ControlTypes.CheckBox)]
        //[DisplayName("Has news letter subscription")]
        public bool IsSubscribeOurNewsLetter { get; set; }
        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [DisplayName("Has accepted terms and conditions")]
        public bool IsTermsAndConditions { get; set; }

        //[ContainerType(Framework.Enums.ContainerTypes.Tab, "CustomerSettings", "Settings")]
        //[DisplayName("Customer Settings")]
        public CustomerSettingsViewModel Settings { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "CustomerLogin", "Login")]
        [DisplayName("Login Info")]
        public LoginViewModel Login { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "CustomerContacts", "Contacts")]
        [DisplayName("Contacts")]
        public ContactsViewModel Contacts { get; set; }

        //[ContainerType(Framework.Enums.ContainerTypes.Tab, "CustomerEntitlements", "Entitlements")]
        //[DisplayName("Entitlements")]
        public EntitlementViewModel Entitlements { get; set; }

        //[ContainerType(Framework.Enums.ContainerTypes.Tab, "Documents", "Documents")]
        //[DisplayName("Documents")]
        //[LazyLoad("Mutual/DocumentFile", "Mutual/GetDocumentFiles", "CRUDModel.ViewModel.Document")]
        public DocumentViewViewModel Document { get; set; }

        //[ContainerType(Framework.Enums.ContainerTypes.Tab, "PriceList", "PriceListEntitlement")]
        //[DisplayName("Price List")]
        public PriceListEntitlement PriceListEntitlement { get; set; }

        //[ContainerType(Framework.Enums.ContainerTypes.Tab, "CustomerExternalSettings", "ExternalSettings")]
        //[DisplayName("External Settings")]
        public ExternalSettingsViewModel ExternalSettings { get; set; }

        //[ContainerType(Framework.Enums.ContainerTypes.Tab, "CustomerSupplierAccount", "SupplierAccountMaps")]
        //[DisplayName("Supplier Account")]
        //public SupplierAccountMapViewModel SupplierAccountMaps { get; set; }

        public static CustomerMasterViewModel FromDTO(CustomerDTO dto)
        {
            Mapper<CustomerDTO, CustomerMasterViewModel>.CreateMap();
            Mapper<ContactDTO, ContactsViewModel>.CreateMap();
            Mapper<LoginDTO, LoginViewModel>.CreateMap();
            Mapper<PhoneDTO, PhoneViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            Mapper<EntitlementDTO, EntitlementViewModel>.CreateMap();
            Mapper<EntitlementMapDTO, EntitlementMapViewModel>.CreateMap();

            Mapper<EntityPropertyMapDTO, PhoneViewModel>.CreateMap();
            Mapper<EntityPropertyMapDTO, EmailViewModel>.CreateMap();
            Mapper<EntityPropertyMapDTO, FaxViewModel >.CreateMap();
            Mapper<BankAccountDTO, BankAccountViewModel>.CreateMap();
            Mapper<DocumentViewDTO, DocumentViewViewModel>.CreateMap();
            Mapper<DocumentFileDTO, DocumentFileViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            Mapper<PriceListEntitlementDTO, PriceListEntitlement>.CreateMap();
            Mapper<EntitlementPriceListMapDTO, EntitlementPriceListMapViewModel>.CreateMap();
            Mapper<ExternalSettingsDTO, ExternalSettingsViewModel>.CreateMap();
            Mapper<ExternalProductSettingsDTO, ExternalProductSettingsViewModel>.CreateMap();
            Mapper<CustomerSettingDTO, CustomerSettingsViewModel>.CreateMap();

            return Mapper<CustomerDTO, CustomerMasterViewModel>.Map(dto);
        }

        public static CustomerDTO ToDTO(CustomerMasterViewModel vm)
        {
            Mapper<CustomerMasterViewModel, CustomerDTO>.CreateMap();
            Mapper<ContactsViewModel, ContactDTO>.CreateMap();
            Mapper<LoginViewModel, LoginDTO>.CreateMap();
            Mapper<PhoneViewModel, PhoneDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            Mapper<EntitlementViewModel, EntitlementDTO>.CreateMap();
            Mapper<EntitlementMapViewModel, EntitlementMapDTO>.CreateMap();

            Mapper<PhoneViewModel, EntityPropertyMapDTO>.CreateMap();
            Mapper<EmailViewModel, EntityPropertyMapDTO>.CreateMap();
            Mapper<FaxViewModel, EntityPropertyMapDTO>.CreateMap();
            Mapper<DocumentViewViewModel, DocumentViewDTO>.CreateMap();
            Mapper<DocumentFileViewModel, DocumentFileDTO>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            Mapper<PriceListEntitlement, PriceListEntitlementDTO>.CreateMap();
            Mapper<EntitlementPriceListMapViewModel, EntitlementPriceListMapDTO>.CreateMap();
            Mapper<ExternalSettingsViewModel, ExternalSettingsDTO>.CreateMap();
            Mapper<ExternalProductSettingsViewModel, ExternalProductSettingsDTO>.CreateMap();
            Mapper<CustomerSettingsViewModel, CustomerSettingDTO>.CreateMap();

            var mapper = Mapper<CustomerMasterViewModel, CustomerDTO>.Map(vm);
            return mapper;
        }

        public static KeyValueViewModel ToKeyValueViewModel(CustomerDTO dto)
        {
            if (dto != null)
            {
                return new KeyValueViewModel()
                {
                    Key = dto.CustomerIID.ToString(),
                    Value = string.Concat(dto.FirstName, " ", dto.MiddleName, " ", dto.LastName),
                };
            }
            else return new KeyValueViewModel();
        }
    }
}