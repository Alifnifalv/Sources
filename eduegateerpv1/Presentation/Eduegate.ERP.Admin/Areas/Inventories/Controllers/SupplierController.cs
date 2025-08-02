using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper;
//using Eduegate.Framework.Web.Library.Common;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Eduegate.ERP.Admin.Areas.Inventories.Controllers
{
    public class SupplierController : BaseSearchController
    {
        private static string ServiceHost { get { return ConfigurationExtensions.GetAppConfigValue("ServiceHost"); } }

        private string ReferenceServiceUrl = string.Concat(ServiceHost, Constants.REFERENCE_DATA_SERVICE);

        // GET: Supplier
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList(
                (Services.Contracts.Enums.SearchView)(int)Infrastructure.Enums.SearchView.Supplier);

            return View(new SearchListViewModel
            {
                ControllerName = Infrastructure.Enums.SearchView.Supplier.ToString(),
                ViewName = Infrastructure.Enums.SearchView.Supplier,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                UserViews = metadata.UserViews,
                SortColumns = metadata.SortColumns,
                IsMultilineEnabled = false,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                HasFilters = metadata.HasFilters,
                FilterColumns = metadata.QuickFilterColumns,
                UserValues = metadata.UserValues
            });
        }

        [HttpGet]
        public JsonResult SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {
            orderBy = orderBy.Equals("null") ? "" : orderBy;

            if (orderBy.Trim().IsNullOrEmpty())
                orderBy = "CreatedDate";
            runtimeFilter = " CompanyId = " + CallContext.CompanyID.ToString();
            return base.SearchData(Infrastructure.Enums.SearchView.Supplier, currentPage, orderBy, runtimeFilter);
        }

        [HttpGet]
        public JsonResult SearchSummaryData()
        {
            return base.SearchSummaryData(Infrastructure.Enums.SearchView.SupplierSummary);
        }

        [HttpGet]
        public ActionResult Create(long ID = 0)
        {
            var viewModel = new CRUDViewModel();
            viewModel.Name = "Supplier";
            viewModel.ListActionName = "Supplier";
            var supplierVm = new SupplierViewModel();
            var contact = new ContactsViewModel();
            contact.Phones = PhoneViewModel.DefaultData(ClientFactory.MutualServiceClient(CallContext).GetEntityPropertiesByType((int)EntityPropertyTypes.Telephone));
            contact.Emails = EmailViewModel.DefaultData(ClientFactory.MutualServiceClient(CallContext).GetEntityPropertiesByType((int)EntityPropertyTypes.Email));
            contact.Faxs = FaxViewModel.DefaultData(ClientFactory.MutualServiceClient(CallContext).GetEntityPropertiesByType((int)EntityPropertyTypes.Fax));
            supplierVm.Contacts.Add(contact);
            var bankAccount = new BankAccountViewModel();
            supplierVm.BankAccounts.Add(bankAccount);

            var entitlements = ClientFactory.MutualServiceClient(CallContext).GetEntityTypeEntitlementByEntityType(Eduegate.Framework.Contracts.Common.Enums.EntityTypes.Supplier);
            supplierVm.Entitlements.EntitlementMaps = EntitlementMapViewModel.DefaultData(entitlements);
            supplierVm.PriceListEntitlement.EntitlementPriceListMaps = EntitlementPriceListMapViewModel.DefaultData(entitlements);


            // Setting document properties
            supplierVm.Document = new DocumentViewViewModel();

            supplierVm.Branch = new SupplierBranchViewModel();

            if (ID == 0)
            {
                supplierVm.Branch.Profit = 10;
            }

            //viewModel.ViewModel = supplierVm;
            //viewModel.IID = ID;
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Title", Url = "Mutual/GetLookUpData?lookType=Title" });
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "PhoneTypes", Url = "Mutual/GetEntityPropertiesByType?entityType=" + (int)EntityPropertyTypes.Telephone }); // Phone
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "DocumentFileStatuses", Url = "Mutual/GetLookUpData?lookType=DocumentFileStatus" });
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Employees", Url = "Payroll/Employee/GetEmployees" });
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Countries", Url = "Mutual/GetCountries" }); // Countries
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "ChequeTypes", Url = "Mutual/GetEntityPropertiesByType?entityType=" + (int)EntityPropertyTypes.ChequeType }); // ChequeTypes
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "SupplierStatuses", Url = "Supplier/GetSupplierStatuses" }); // SupplierStatuses
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Products", Url = "Product/GetProducts?searchText=" + null }); // Phone
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "LoginUserStatus", Url = "Mutual/GetLookUpData?lookType=LoginUserStatus" });
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Manager", Url = "Mutual/GetLookUpData?lookType=Manager" }); // Document Owner(Employees)
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "ContactStatus", Url = "Mutual/GetLookUpData?lookType=ContactStatus" }); // ContactStatus
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "SupplierBranch", Url = "Mutual/GetLookUpData?lookType=BranchMarketPlace" });
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Areas", Url = "Mutual/GetAreaByCountryID?countryID=10003" }); // Areas
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Cities", Url = "Mutual/GetCityByCountryID?countryID=10003" }); // Cities
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "ContactStatus", Url = "Mutual/GetLookUpData?lookType=ContactStatus" }); // ContactStatus
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "ReceivingMethod", Url = "Mutual/GetLookUpData?lookType=ReceivingMethod&defaultBlank=false" }); // ContactStatus
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "ReturnMethod", Url = "Mutual/GetLookUpData?lookType=ReturnMethod&defaultBlank=false" }); // ContactStatus


            #region  Accounts Settings
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Accounts", Url = "Mutual/GetLookUpData?lookType=Account" }); // Accounts
            //viewModel.JsControllerName = "AccountMapsController";
            supplierVm.SupplierAccountMaps.SupplierAccountEntitlements = SupplierAccountEntitlementViewModel.DefaultData(entitlements);
            //supplierVm.SupplierAccountMaps = SupplierAccountMapViewModel.SetAutoGeneratedAccounts(supplierVm.SupplierAccountMaps, "GLACC_DEFAULT_SUPPLIERACCOUNT");
            #endregion  Accounts Settings

            //TempData["viewModel"] = viewModel;
            return Json(supplierVm, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(long ID = 0)
        {
            return RedirectToAction("Create", new { ID = ID });
        }

        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            SupplierViewModel supplierModel = SupplierViewModel.FromDTO(ClientFactory.SupplierServiceClient(CallContext).GetSupplier(ID.ToString()));
            // call service for selected employee 
            supplierModel.KeyValueEmployees = GetEmployeeIdNameEntityTypeRelation(supplierModel.SupplierIID);


            #region Account Settings 
            supplierModel.SupplierAccountMaps = SupplierAccountMapViewModel.
                SetAutoGeneratedAccounts(supplierModel.SupplierAccountMaps, "GLACC_DEFAULT_SUPPLIERACCOUNT", null, null, null);
            #endregion Account Settings


            return Json(supplierModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetSupplierDetails(long ID = 0)
        {
            return Json(SupplierDetailViewModel.FromDTO(ClientFactory.SupplierServiceClient(CallContext).GetSupplier(ID.ToString())), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SupplierCreation(SupplierViewModel supplierModel)
        {
            if (supplierModel.SupplierIID > 0)
            {
                var dto = ClientFactory.SupplierServiceClient(CallContext).GetSupplier(supplierModel.SupplierIID.ToString());
                supplierModel = SupplierViewModel.FromDTO(dto);

                // call service for selected employee 
                supplierModel.KeyValueEmployees = GetEmployeeIdNameEntityTypeRelation(supplierModel.SupplierIID);
            }
            else
            {
                supplierModel.Contacts = new List<ContactsViewModel>();
                ContactsViewModel contact = new ContactsViewModel();
                supplierModel.Contacts.Add(contact);
            }

            return View("SupplierCreation", supplierModel);
        }

        [HttpPost]
        public JsonResult Save(SupplierViewModel supplierModel)
        {
            try
            {
                if (supplierModel != null)
                {
                    //var ValidationResult = AccountValidations(supplierModel);
                    //if(ValidationResult !=null)
                    //{
                    //    return ValidationResult;
                    //}

                    var resultSupplierDTO = ClientFactory.SupplierServiceClient(CallContext).SaveSupplier(SupplierViewModel.ToDTO(supplierModel));

                    // Now Call Service to save [catalog].[EmployeeCatalogRelations]
                    if (resultSupplierDTO.IsNotNull() && resultSupplierDTO.SupplierIID > 0)
                    {
                        if (supplierModel.Contacts.IsNotNull() && supplierModel.Contacts.Count > 0)
                        {
                            List<ContactDTO> contactDTOList = ContactsToDTO(supplierModel.Contacts, resultSupplierDTO.SupplierIID);
                            contactDTOList = ClientFactory.MutualServiceClient(CallContext).CreateEntityProperties(contactDTOList);
                        }

                        SaveEntityTypeRelationMaps(resultSupplierDTO.SupplierIID, supplierModel.KeyValueEmployees);

                        #region Save Supplier Accounts
                        List<Services.Contracts.Accounting.SupplierAccountEntitlmentMapsDTO> dtoList =
                            SupplierAccountMapViewModel.SaveSupplierAccountMap(supplierModel.SupplierAccountMaps, resultSupplierDTO.SupplierIID, null, null);
                        #endregion

                        // Get
                        dynamic getResult = Get(resultSupplierDTO.SupplierIID);
                        supplierModel = getResult.Data;

                        #region Save Documents
                        // Temp path
                        string tempFolderPath = string.Format(@"{0}{1}\{2}\{3}",
                                ConfigurationExtensions.GetAppConfigValue("ImagesPhysicalPath").ToString(), EduegateImageTypes.Documents.ToString(), Constants.TEMPFOLDER, CallContext.LoginID);

                        // Destination path
                        string destinationPath = string.Format(@"{0}{1}\{2}\{3}",
                               ConfigurationExtensions.GetAppConfigValue("ImagesPhysicalPath").ToString(), EduegateImageTypes.Documents.ToString(), Eduegate.Framework.Contracts.Common.Enums.EntityTypes.Supplier.ToString(), resultSupplierDTO.SupplierIID);

                        // Save doc reference doc tables
                        //var docsDTO = DocumentFileViewModel.ToDTO(vm.Document.Documents);
                        //docsDTO = new DocumentServiceClient().SaveDocuments(docsDTO);

                        // Move docs
                        var fileResult = new MutualController().MoveFiles(tempFolderPath, destinationPath);
                        #endregion
                    }
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<SupplierController>.Fatal(exception.Message.ToString(), exception);
                return Json(new { IsError = !false, UserMessage = exception.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }

            return Json(supplierModel, JsonRequestBehavior.AllowGet);
        }

        private List<ContactDTO> ContactsToDTO(List<ContactsViewModel> contacts, long referenceID)
        {
            List<ContactDTO> contactDTOList = new List<ContactDTO>();
            ContactDTO dto = null;

            foreach (var contact in contacts)
            {
                dto = new ContactDTO();
                dto = ContactsViewModel.ToDTO(contact);

                // Phones
                short cntPhone = 1;
                dto.Phones = new List<EntityPropertyMapDTO>();
                foreach (var phone in contact.Phones)
                {
                    EntityPropertyMapDTO dtoPhone = new EntityPropertyMapDTO();
                    // First map using Mapper
                    dtoPhone = PhoneViewModel.ToDTO(phone);
                    // then custome entity
                    dtoPhone.EntityTypeID = (short)Eduegate.Framework.Enums.EntityTypes.Supplier;
                    dtoPhone.EntityPropertyTypeID = (int)EntityPropertyTypes.Telephone;
                    dtoPhone.ReferenceID = referenceID;
                    dtoPhone.Sequence = cntPhone;
                    // add dtoEmail into Contact dto
                    dto.Phones.Add(dtoPhone);
                    cntPhone++;
                }

                // Emails
                short cntEmail = 1;
                dto.Emails = new List<EntityPropertyMapDTO>();
                foreach (var email in contact.Emails)
                {
                    EntityPropertyMapDTO dtoEmail = new EntityPropertyMapDTO();
                    // First map using Mapper
                    dtoEmail = EmailViewModel.ToDTO(email);
                    // then custome entity
                    dtoEmail.EntityTypeID = (short)Eduegate.Framework.Enums.EntityTypes.Supplier;
                    dtoEmail.EntityPropertyTypeID = (int)EntityPropertyTypes.Email;
                    dtoEmail.ReferenceID = referenceID;
                    dtoEmail.Sequence = cntEmail;
                    // add dtoEmail into Contact dto
                    dto.Emails.Add(dtoEmail);
                    cntEmail++;
                }

                // Faxs
                short cntFax = 1;
                dto.Faxs = new List<EntityPropertyMapDTO>();
                foreach (var fax in contact.Faxs)
                {
                    EntityPropertyMapDTO dtoFax = new EntityPropertyMapDTO();
                    // First map using Mapper
                    dtoFax = FaxViewModel.ToDTO(fax);
                    // then custome entity
                    dtoFax.EntityTypeID = (short)Eduegate.Framework.Enums.EntityTypes.Supplier;
                    dtoFax.EntityPropertyTypeID = (int)EntityPropertyTypes.Fax;
                    dtoFax.ReferenceID = referenceID;
                    dtoFax.Sequence = cntFax;
                    // add dtoFax into Contact dto
                    dto.Faxs.Add(dtoFax);
                    cntFax++;
                }

                contactDTOList.Add(dto);
            }

            return contactDTOList;
        }

        [HttpGet]
        public JsonResult GetCountryMasters()
        {
            List<CountryViewModel> countryMasterViewModelList = new List<CountryViewModel>();

            try
            {
                var countryDTOList = ClientFactory.ReferenceDataServiceClient(CallContext).GetCountries(false);

                if (countryDTOList != null && countryDTOList.Count > 0)
                {
                    foreach (var country in countryDTOList)
                    {
                        var countryViewModel = new CountryViewModel();

                        countryViewModel.CountryID = country.CountryID;
                        countryViewModel.CountryName = country.CountryName;
                        countryViewModel.TelephoneCode = country.TelephoneCode;

                        countryMasterViewModelList.Add(countryViewModel);
                    }
                }
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<SupplierController>.Info("Exception : " + ex.Message.ToString());
                return Json(new { IsError = true, ErrorMessage = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { IsError = false, CountryMasters = countryMasterViewModelList }, JsonRequestBehavior.AllowGet);
        }


        protected void SaveEntityTypeRelationMaps(long supplierID, List<KeyValueViewModel> lists)
        {
            if (lists.IsNull())
                return;

            EntityTypeRelationDTO dto = new EntityTypeRelationDTO();
            dto.FromEntityTypes = Eduegate.Framework.Contracts.Common.Enums.EntityTypes.Supplier;
            dto.ToEntityTypes = Eduegate.Framework.Contracts.Common.Enums.EntityTypes.Employee;
            dto.FromRelaionID = supplierID;
            dto.ToRelaionIDs = new List<long>();
            foreach (var list in lists)
            {
                dto.ToRelaionIDs.Add(Convert.ToInt64(list.Key));
            }

            // Service Call.
            ClientFactory.MutualServiceClient(CallContext).SaveEntityTypeRelationMaps(dto);
        }

        public List<KeyValueViewModel> GetEmployeeIdNameEntityTypeRelation(long supplierID)
        {
            EntityTypeRelationDTO dto = new EntityTypeRelationDTO();
            dto.FromEntityTypes = Eduegate.Framework.Contracts.Common.Enums.EntityTypes.Supplier;
            dto.ToEntityTypes = Eduegate.Framework.Contracts.Common.Enums.EntityTypes.Employee;
            dto.FromRelaionID = supplierID;

            // Service Call.
            List<KeyValueDTO> dtoKeyValueOwners = ClientFactory.MutualServiceClient(CallContext).GetEmployeeIdNameEntityTypeRelation(dto);
            List<KeyValueViewModel> vmKeyValueOwners = dtoKeyValueOwners.Select(x => KeyValueViewModel.ToViewModel(x)).ToList();

            return vmKeyValueOwners;
        }

        [HttpGet]
        public JsonResult GetSupplierBySupplierIdAndCR(string lookupName, string searchText)
        {
            try
            {
                var dtos = ClientFactory.SupplierServiceClient(CallContext).GetSupplierBySupplierIdAndCR(searchText);
                var VMs = new List<KeyValueViewModel>();
                foreach (var item in dtos)
                {
                    KeyValueViewModel vm = new KeyValueViewModel();
                    vm.Key = item.SupplierIID.ToString();
                    vm.Value = string.Concat(item.FirstName + " ", item.MiddleName, item.LastName);
                    VMs.Add(vm);
                }

                VMs.Insert(0, new KeyValueViewModel() { Key = null, Value = string.Empty });
                return Json(new { LookUpName = lookupName, Data = VMs }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<SupplierController>.Info("Exception : " + ex.Message.ToString());
                return Json(new { IsError = true, ErrorMessage = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View("DetailedView");
        }

        [HttpGet]
        public JsonResult GetSupplierStatuses()
        {
            var dtoSupplierStatuses = ClientFactory.SupplierServiceClient(CallContext).GetSupplierStatuses();
            List<KeyValueViewModel> vmKeyValue = new List<KeyValueViewModel>();

            dtoSupplierStatuses.ToList()
                .ForEach(
                x =>
                {
                    vmKeyValue.Add(new KeyValueViewModel
                    {
                        Key = x.SupplierStatusID.ToString(),
                        Value = x.StatusName,
                    });
                });

            vmKeyValue.Insert(0, new KeyValueViewModel() { Key = null, Value = string.Empty });
            return Json(vmKeyValue, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetAutoGeneratedAccounts(long AccountID, int noOfChildAccounts, string Entity, string[] Entitlment)
        {
            var AccountsDTOList = ClientFactory.AccountingTransactionServiceClient(CallContext).GetAutoGeneratedAccounts(AccountID, noOfChildAccounts, Entity, Entitlment[0]);
            return Json(AccountsDTOList, JsonRequestBehavior.AllowGet);
        }


        private JsonResult AccountValidations(SupplierViewModel vm)
        {
            if (vm.SupplierAccountMaps.SupplierAccount == null)
            {
                return Json(new { IsError = true, UserMessage = "Supplier Account Not selected" }, JsonRequestBehavior.AllowGet);
            }
            //if (vm.SupplierAccountMaps.SupplierAccount == null)
            //{
            //    return Json(new { IsError = true, UserMessage = "Supplier Account Not selected" }, JsonRequestBehavior.AllowGet);
            //}
            return null;
        }

        [HttpGet]
        public JsonResult GetSupplierDeliveryMethod(long supplierID)
        {
            var result = ClientFactory.SupplierServiceClient(CallContext).GetSupplierDeliveryMethod(supplierID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetSupplierReturnMethod(long supplierID)
        {
            var result = ClientFactory.SupplierServiceClient(CallContext).GetSupplierReturnMethod(supplierID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}