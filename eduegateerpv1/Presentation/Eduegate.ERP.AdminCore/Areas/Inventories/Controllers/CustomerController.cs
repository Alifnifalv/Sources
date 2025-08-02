using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper;
//using Eduegate.Framework.Web.Library.Common;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Eduegate.Domain;
using System.Threading.Tasks;

namespace Eduegate.ERP.Admin.Areas.Inventories.Controllers
{
    [Area("Inventories")]
    public class CustomerController : BaseSearchController
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Infrastructure.Enums.SearchView.Customer);

            return View(new SearchListViewModel
            {
                ControllerName = Infrastructure.Enums.SearchView.Customer.ToString(),
                ViewName = Infrastructure.Enums.SearchView.Customer,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                InfoBar = @"<li class='status-label-mobile'>
                                        <div class='right status-label'>
                                            <div class='status-label-color'><label class='status-color-label orange'></label>Draft</div>
                                            <div class='status-label-color'><label class='status-color-label  green'></label>Active</div>
                                            <div class='status-label-color'><label class='status-color-label red'></label>In Active</div>
                                        </div>
                                    </li>",
                HasFilters = metadata.HasFilters,
                FilterColumns = metadata.QuickFilterColumns,
                UserValues = metadata.UserValues
            });
        }

        [HttpGet]
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "")
        {
            return base.SearchData(Infrastructure.Enums.SearchView.Customer, currentPage, orderBy);
        }

        [HttpGet]
        public Task<IActionResult> SearchSummaryData()
        {
            return base.SearchSummaryData(Infrastructure.Enums.SearchView.CustomerSummary);
        }

        [HttpGet]
        public ActionResult Create(long ID = 0)
        {
            var viewModel = new CRUDViewModel();
            viewModel.Name = "Customer";
            viewModel.ListActionName = "Customer";
            var customerVm = new CustomerMasterViewModel()
            {
                Status = new KeyValueViewModel() { Key = "1", Value = Convert.ToString(Services.Contracts.Enums.CustomerStatus.Active) },
                Group = new KeyValueViewModel() { Key = "1", Value = Convert.ToString(Services.Contracts.Enums.CustomerGroups.BlueMember) }
            };
            var contact = new ContactsViewModel();
            //contact.Phones = PhoneViewModel.DefaultData(ClientFactory.MutualServiceClient(CallContext).GetEntityPropertiesByType((int)EntityPropertyTypes.Telephone));
            //contact.Emails = EmailViewModel.DefaultData(ClientFactory.MutualServiceClient(CallContext).GetEntityPropertiesByType((int)EntityPropertyTypes.Email));
            //contact.Faxs = FaxViewModel.DefaultData(ClientFactory.MutualServiceClient(CallContext).GetEntityPropertiesByType((int)EntityPropertyTypes.Fax));
            customerVm.Contacts.Add(contact);

            contact.Country = KeyValueViewModel.ToViewModel(ClientFactory.ReferenceDataServiceClient(CallContext).GetDefaultCountryWithName());
            var entitlement = ClientFactory.MutualServiceClient(CallContext).GetEntityTypeEntitlementByEntityType(Eduegate.Framework.Contracts.Common.Enums.EntityTypes.Customer);
            customerVm.Entitlements.EntitlementMaps = EntitlementMapViewModel.DefaultData(entitlement);
            //customerVm.PriceListEntitlement = new PriceListEntitlement();
            customerVm.PriceListEntitlement.EntitlementPriceListMaps = new List<EntitlementPriceListMapViewModel>();
            customerVm.PriceListEntitlement.EntitlementPriceListMaps = EntitlementPriceListMapViewModel.DefaultData(entitlement);


            // Setting document properties
            customerVm.Document = new DocumentViewViewModel();

            //initialize default values
            //initialize default values
            var isCityMandatory = ClientFactory.SettingServiceClient(CallContext).GetSettingDetailByCompanyWithDefault<bool>("CITYMANDATORY", CallContext.CompanyID.Value, true);
            var isAreaMandatory = ClientFactory.SettingServiceClient(CallContext).GetSettingDetailByCompanyWithDefault<bool>("AREAMANDATORY", CallContext.CompanyID.Value, true);

            foreach (var cnt in customerVm.Contacts)
            {
                cnt.IsCityMandatory = isCityMandatory; // CallContext.CompanyID == 1 ? false : true; // city mandatory only for KSA
                cnt.IsAreaMandatory = isAreaMandatory; // CallContext.CompanyID == 1 ? false : true; // city mandatory only for KSA
            }

            //viewModel.ViewModel = customerVm;
            //viewModel.IID = ID;
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "CustomerStatus", Url = "Customer/GetCustomerStatus" });
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Title", Url = "Mutual/GetLookUpData?lookType=Title" });
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "CustomerGroup", Url = "Mutual/GetLookUpData?lookType=CustomerGroup" });
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "LoginUserStatus", Url = "Mutual/GetLookUpData?lookType=LoginUserStatus" });
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "PhoneTypes", Url = "Mutual/GetEntityPropertiesByType?entityType=" + (int)EntityPropertyTypes.Telephone }); // Phone
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Countries", Url = "Mutual/GetCountries" }); // Countries
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Areas", Url = "Mutual/GetLookUpData?lookType=Area" }); // Areas
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "DocumentFileStatuses", Url = "Mutual/GetLookUpData?lookType=DocumentFileStatus" });
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "OwnerEmployee", Url = "Mutual/GetLookUpData?lookType=Manager" }); // Document Owner(Employees)
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Cities", Url = "Mutual/GetLookUpData?lookType=CityByCompany" }); // Cities
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "ContactStatus", Url = "Mutual/GetLookUpData?lookType=ContactStatus" }); // ContactStatus
            //#region  Accounts Settings
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Accounts", Url = "Mutual/GetLazyLookUpData?lookType=Account" }); // Accounts
            //viewModel.JsControllerName = "AccountMapsController";
            customerVm.SupplierAccountMaps.SupplierAccountEntitlements = SupplierAccountEntitlementViewModel.DefaultData(entitlement);
            customerVm.SupplierAccountMaps = SupplierAccountMapViewModel.SetAutoGeneratedAccounts(customerVm.SupplierAccountMaps, "GLACC_DEFAULT_CUSTOMERACCOUNT", null, null, null);
            //#endregion  Accounts Settings

            ////set initvalue
            //TempData["viewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(viewModel);
            //return RedirectToAction("Create", "CRUD");
            return Json(customerVm);
        }

        [HttpGet]
        public ActionResult Edit(long ID = 0)
        {
            return RedirectToAction("Create", new { ID = ID });
        }

        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            var vm = CustomerMasterViewModel.FromDTO(ClientFactory.CustomerServiceClient(CallContext).GetCustomer(ID.ToString()));

            //initialize default values
            var status = vm.StatusID.IsNotNull() && vm.StatusID > 0 ? ClientFactory.ReferenceDataServiceClient(CallContext).GetCustomerStatus().Where(x => x.StatusID == vm.StatusID).FirstOrDefault() : null;
            var group = vm.GroupID.IsNotNull() && vm.GroupID > 0 ? ClientFactory.CustomerServiceClient(CallContext).GetCustomerGroup(vm.GroupID.ToString()) : null;
            vm.Group = group.IsNotNull() ? new KeyValueViewModel { Key = group.CustomerGroupIID.ToString(), Value = group.GroupName } : null;
            vm.Status = status.IsNotNull() ? new KeyValueViewModel { Key = status.StatusID.ToString(), Value = status.StatusName } : null;
            var isCityMandatory = ClientFactory.SettingServiceClient(CallContext).GetSettingDetailByCompanyWithDefault<bool>("CITYMANDATORY", CallContext.CompanyID.Value, true);
            foreach (var cnt in vm.Contacts)
            {
                cnt.IsCityMandatory = isCityMandatory; // CallContext.CompanyID == 1 ? false : true; // city mandatory only for KSA
            }

            #region Account Settings 
            vm.SupplierAccountMaps = new SupplierAccountMapViewModel(Eduegate.Framework.Contracts.Common.Enums.EntityTypes.Customer);

            Eduegate.Services.Contracts.Accounting.SupplierAccountMapDTO dto = new Services.Contracts.Accounting.SupplierAccountMapDTO();
            dto.SupplierAccountEntitlements = ClientFactory.CustomerServiceClient(CallContext).GetCustomerAccountMaps(ID.ToString());
            vm.SupplierAccountMaps = SupplierAccountMapViewModel.SetSupplierAccountMapsViewModel(dto, Eduegate.Framework.Contracts.Common.Enums.EntityTypes.Customer);
            vm.SupplierAccountMaps = SupplierAccountMapViewModel.MergeEntitlements(vm.SupplierAccountMaps);
            vm.SupplierAccountMaps = SupplierAccountMapViewModel.SetAutoGeneratedAccounts(vm.SupplierAccountMaps, "GLACC_DEFAULT_CUSTOMERACCOUNT", null, null, null);
            #endregion Account Settings

            return Json(vm);
        }

        [HttpPost]
        public JsonResult Save([FromBody] CustomerMasterViewModel vm)
        {
            Eduegate.Services.Contracts.Accounting.SupplierAccountMapDTO dto = new Services.Contracts.Accounting.SupplierAccountMapDTO();
            var ValidationResult = AccountValidations(vm);
            if (ValidationResult != null)
            {
                return ValidationResult;
            }
            vm.GroupID = vm.Group.IsNotNull() && vm.Group.Key.IsNotNull() ? Convert.ToInt32(vm.Group.Key) : 0;
            vm.StatusID = vm.Status.IsNotNull() && vm.Status.Key.IsNotNull() ? Convert.ToInt32(vm.Status.Key) : 0;
            vm.Login.StatusID = string.IsNullOrEmpty(vm.Login.Status.Value) ? (LoginUserStatus?)null : (LoginUserStatus)Enum.Parse(typeof(LoginUserStatus), vm.Login.Status.Value);
            vm.TitleID = vm.Title.Key;

            // Save Customer
            var updatedDTO = ClientFactory.CustomerServiceClient(CallContext).SaveCustomer(CustomerMasterViewModel.ToDTO(vm));

            if (updatedDTO.IsNotNull())
            {
                // Save Contacts
                if (vm.Contacts.IsNotNull() && vm.Contacts.Count > 0)
                {
                    List<ContactDTO> contactDTOList = ContactsToDTO(vm.Contacts, updatedDTO.CustomerIID);
                    contactDTOList = ClientFactory.MutualServiceClient(CallContext).CreateEntityProperties(contactDTOList);
                    updatedDTO.Contacts = contactDTOList;
                }

                #region Save Supplier Accounts
                List<Services.Contracts.Accounting.SupplierAccountEntitlmentMapsDTO> dtoList =
                    SupplierAccountMapViewModel.SaveSupplierAccountMap(vm.SupplierAccountMaps, updatedDTO.CustomerIID, null, null);
                dto = new Services.Contracts.Accounting.SupplierAccountMapDTO();
                dto.SupplierAccountEntitlements = dtoList;
                #endregion

                #region Save Documents
                // Temp path
                string tempFolderPath = string.Format(@"{0}{1}\{2}\{3}",
                        new Domain.Setting.SettingBL().GetSettingValue<string>("ImagesPhysicalPath").ToString(), EduegateImageTypes.Documents.ToString(), Constants.TEMPFOLDER, CallContext.LoginID);

                // Destination path
                string destinationPath = string.Format(@"{0}{1}\{2}\{3}",
                       new Domain.Setting.SettingBL().GetSettingValue<string>("ImagesPhysicalPath").ToString(), EduegateImageTypes.Documents.ToString(), Eduegate.Framework.Contracts.Common.Enums.EntityTypes.Customer.ToString(), updatedDTO.CustomerIID);

                // Save doc reference doc tables
                //var docsDTO = DocumentFileViewModel.ToDTO(vm.Document.Documents);
                //docsDTO = new DocumentServiceClient().SaveDocuments(docsDTO);

                // Move docs
                var fileResult = new MutualController().MoveFiles(tempFolderPath, destinationPath);
                #endregion
            }

            //Changed for account settings
            CustomerMasterViewModel customerMasterViewModel = CustomerMasterViewModel.FromDTO(ClientFactory.CustomerServiceClient(CallContext).GetCustomer(updatedDTO.CustomerIID.ToString()));
            customerMasterViewModel.SupplierAccountMaps = new SupplierAccountMapViewModel(Eduegate.Framework.Contracts.Common.Enums.EntityTypes.Customer);
            customerMasterViewModel.SupplierAccountMaps = SupplierAccountMapViewModel.SetSupplierAccountMapsViewModel(dto, Eduegate.Framework.Contracts.Common.Enums.EntityTypes.Customer);
            var status = customerMasterViewModel.StatusID.IsNotNull() ? ClientFactory.ReferenceDataServiceClient(CallContext).GetCustomerStatus().Where(x => x.StatusID == customerMasterViewModel.StatusID).FirstOrDefault() : null;
            var group = customerMasterViewModel.GroupID.IsNotNull() && customerMasterViewModel.GroupID != 0 ? ClientFactory.CustomerServiceClient(CallContext).GetCustomerGroup(customerMasterViewModel.GroupID.ToString()) : null;
            customerMasterViewModel.Group = group.IsNotNull() ? new KeyValueViewModel { Key = group.CustomerGroupIID.ToString(), Value = group.GroupName } : null;
            customerMasterViewModel.Status = status.IsNotNull() ? new KeyValueViewModel { Key = status.StatusID.ToString(), Value = status.StatusName } : null;
            return Json(customerMasterViewModel);
        }

        private List<ContactDTO> ContactsToDTO(List<ContactsViewModel> contacts, long referenceID)
        {
            List<ContactDTO> contactDTOList = new List<ContactDTO>();
            ContactDTO dto = null;

            foreach (var contact in contacts)
            {
                dto = new ContactDTO();
                //dto = ContactsViewModel.ToDTO(contact);

                // Phones
                short cntPhone = 1;
                dto.Phones = new List<EntityPropertyMapDTO>();
                //foreach (var phone in contact.Phones)
                //{
                //    EntityPropertyMapDTO dtoPhone = new EntityPropertyMapDTO();
                //    // First map using Mapper
                //    dtoPhone = PhoneViewModel.ToDTO(phone);
                //    // then custome entity
                //    dtoPhone.EntityTypeID = (short)Eduegate.Framework.Enums.EntityTypes.Customer;
                //    dtoPhone.EntityPropertyTypeID = (int)EntityPropertyTypes.Telephone;
                //    dtoPhone.ReferenceID = referenceID;
                //    dtoPhone.Sequence = cntPhone;
                //    // add dtoEmail into Contact dto
                //    dto.Phones.Add(dtoPhone);
                //    cntPhone++;
                //}

                // Emails
                short cntEmail = 1;
                dto.Emails = new List<EntityPropertyMapDTO>();
                //foreach (var email in contact.Emails)
                //{
                //    EntityPropertyMapDTO dtoEmail = new EntityPropertyMapDTO();
                //    // First map using Mapper
                //    dtoEmail = EmailViewModel.ToDTO(email);
                //    // then custome entity
                //    dtoEmail.EntityTypeID = (short)Eduegate.Framework.Enums.EntityTypes.Customer;
                //    dtoEmail.EntityPropertyTypeID = (int)EntityPropertyTypes.Email;
                //    dtoEmail.ReferenceID = referenceID;
                //    dtoEmail.Sequence = cntEmail;
                //    // add dtoEmail into Contact dto
                //    dto.Emails.Add(dtoEmail);
                //    cntEmail++;
                //}

                // Faxs
                short cntFax = 1;
                dto.Faxs = new List<EntityPropertyMapDTO>();
                //foreach (var fax in contact.Faxs)
                //{
                //    EntityPropertyMapDTO dtoFax = new EntityPropertyMapDTO();
                //    // First map using Mapper
                //    dtoFax = FaxViewModel.ToDTO(fax);
                //    // then custome entity
                //    dtoFax.EntityTypeID = (short)Eduegate.Framework.Enums.EntityTypes.Customer;
                //    dtoFax.EntityPropertyTypeID = (int)EntityPropertyTypes.Fax;
                //    dtoFax.ReferenceID = referenceID;
                //    dtoFax.Sequence = cntFax;
                //    // add dtoFax into Contact dto
                //    dto.Faxs.Add(dtoFax);
                //    cntFax++;
                //}

                contactDTOList.Add(dto);
            }

            return contactDTOList;
        }

        [HttpGet]
        public JsonResult GetCustomerStatus()
        {
            List<CustomerStatusDTO> dtoList = ClientFactory.ReferenceDataServiceClient(CallContext).GetCustomerStatus();
            var vM = new List<KeyValueViewModel>();

            foreach (CustomerStatusDTO dto in dtoList)
            {
                vM.Add(new KeyValueViewModel()
                {
                    Key = dto.StatusID.ToString(),
                    Value = dto.StatusName
                });
            }

            return Json(vM);
        }

        [HttpGet]
        public ActionResult GetCustomerSummaryDetails(long ID = 0)
        {
            var dto = ClientFactory.CustomerServiceClient(CallContext).GetCustomer(ID.ToString());
            return Json(CustomerViewModel.FromCustomerDTO(dto));
        }

        [HttpGet]
        public JsonResult IsCustomerExist(string email, string phone)
        {
            // because service not allow any null or empty parameter
            var dto = ClientFactory.CustomerServiceClient(CallContext).IsCustomerExist(email, phone);
            return Json(CustomerMasterViewModel.FromDTO(dto));
        }


        [HttpGet]
        public JsonResult GetCustomerByCustomerIdAndCR(string lookupName, string searchText, bool defaultBlank = true)
        {
            try
            {
                var dtos = ClientFactory.CustomerServiceClient(CallContext).GetCustomerByCustomerIdAndCR(searchText);
                var VMs = new List<KeyValueViewModel>();
                foreach (var item in dtos)
                {
                    KeyValueViewModel vm = new KeyValueViewModel();
                    vm.Key = item.CustomerIID.ToString();
                    vm.Value = string.Concat(item.CustomerIID, "-", item.FirstName, " ", item.MiddleName, " ", item.LastName);
                    VMs.Add(vm);
                }

                if (defaultBlank)
                    VMs.Insert(0, new KeyValueViewModel() { Key = null, Value = string.Empty });


                return Json(new { LookUpName = lookupName, Data = VMs });
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<CustomerController>.Info("Exception : " + ex.Message.ToString());
                return Json(new { IsError = true, ErrorMessage = ex.Message.ToString() });
            }
        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View("DetailedView");
        }

        [HttpGet]
        public JsonResult GetEmployees(string searchText, string lookupName)
        {
            try
            {
                var dtos = ClientFactory.EmployeeServiceClient(CallContext).SearchEmployee(searchText, new Domain.Setting.SettingBL().GetSettingValue<int>("Select2DataSize"));
                var VMs = new List<KeyValueViewModel>();
                foreach (var item in dtos)
                {
                    KeyValueViewModel vm = new KeyValueViewModel();
                    vm.Key = item.EmployeeIID.ToString();
                    vm.Value = string.Concat(item.EmployeeName);
                    VMs.Add(vm);
                }

                VMs.Insert(0, new KeyValueViewModel() { Key = null, Value = string.Empty });
                return Json(new { LookUpName = lookupName, Data = VMs });
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<CustomerController>.Info("Exception : " + ex.Message.ToString());
                return Json(new { IsError = true, ErrorMessage = ex.Message.ToString() });
            }
        }

        private JsonResult AccountValidations(CustomerMasterViewModel vm)
        {
            //if (vm.SupplierAccountMaps.SupplierAccount == null)
            //{
            //    return Json(new { IsError = true, UserMessage = "Supplier Account Not selected" });
            //}
            //if (vm.SupplierAccountMaps.SupplierAccount == null)
            //{
            //    return Json(new { IsError = true, UserMessage = "Supplier Account Not selected" });
            //}
            return null;
        }

        [HttpGet]
        public bool CheckContactMobileAvailability(long contactId, string mobileNumber)
        {
            bool isAvailibilty = false;
            var result = ClientFactory.CustomerServiceClient(CallContext).CheckContactMobileAvailability(contactId, mobileNumber);

            if (result)
            {
                isAvailibilty = true;
            }

            return isAvailibilty;
        }

        public JsonResult GetContactByCustomerId(long customerId)
        {
            var contactDtos = ClientFactory.AccountServiceClient(CallContext).GetBillingShippingContact(customerId, AddressType.All);

            // get shipping contact
            var contactDto = contactDtos.Where(x => x.IsShippingAddress == true).FirstOrDefault();

            // this is we are doing becuase we need to return one address if we don't have shipping
            if (contactDtos.IsNull())
            {
                contactDto = contactDtos.FirstOrDefault();
            }

            var contact = DeliveryAddressViewViewModel.FromDTOToVM(contactDto);

            return Json(new { ShippingContact = contact });
        }
    }
}