using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Services.Contracts.UrlReWriter;
using Eduegate.Services;
using Eduegate.Services.Contracts.Inventory;
using Eduegate.Services.Contracts.Enums.Common;
using Eduegate.Services.Contracts.Accounts.Taxes;

namespace Eduegate.Service.Client.Direct
{
    public class ReferenceDataServiceClient : BaseClient, IReferenceData
    {
        ReferenceData service = new ReferenceData();

        public ReferenceDataServiceClient(CallContext context = null, Action<string> logger = null)
        {
            service.CallContext = context;
        }

        public List<CountryDTO> GetCountries(bool isActiveCurrency)
        {
            return service.GetCountries(isActiveCurrency);
        }

        //Get passport issue country masters
        public List<CountryMasterDTO> GetPassportIssueCountryMasters()
        {
            return service.GetPassportIssueCountryMasters();
        }

        public List<CheckOutDTO> GetShippingAddressMasters()
        {
            return service.GetShippingAddressMasters();
        }

        public List<CheckOutDTO> GetBillingAddressMasters()
        {
            return service.GetBillingAddressMasters();
        }

        public List<KeyValueDTO> GetLookUpData(Eduegate.Services.Contracts.Enums.LookUpTypes lookType, string searchText = "", int dataSize = 0, int optionalId = 0)
        {
            return service.GetLookUpData(lookType, searchText, dataSize, optionalId);
        }

        public List<KeyValueDTO> GetDBLookUpData(string lookType)
        {
            return service.GetDBLookUpData(lookType);
        }

        public List<LoginStatusDTO> GetLoginStatus()
        {
            return service.GetLoginStatus();
        }

        public List<CustomerStatusDTO> GetCustomerStatus()
        {
            return service.GetCustomerStatus();
        }

        public Services.Contracts.Mutual.WarehouseDTO GetWarehouse(long warehouseID)
        {
            return service.GetWarehouse(warehouseID);
        }

        public Services.Contracts.Mutual.WarehouseDTO SaveWarehouse(Services.Contracts.Mutual.WarehouseDTO wareHouse)
        {
            return service.SaveWarehouse(wareHouse);
        }
        
        public BranchDTO SaveBranch(BranchDTO transaction)
        {
            return service.SaveBranch(transaction);
        }

        public BranchDTO GetBranch(long branchID)
        {
            return service.GetBranch(branchID);
        }

        public BranchGroupDTO SaveBranchGroup(BranchGroupDTO transaction)
        {
            return service.SaveBranchGroup(transaction);
        }

        public BranchGroupDTO GetBranchGroup(long branchGroupID)
        {
            return service.GetBranchGroup(branchGroupID);
        }

        public CompanyDTO GetCompany(long companyID)
        {
            return service.GetCompany(companyID);
        }

        public List<CompanyDTO> GetCompanies()
        {
            return service.GetCompanies();
        }

        public CompanyDTO SaveCompany(CompanyDTO dto)
        {
            return service.SaveCompany(dto);
        }

        public DepartmentDTO GetDepartment(long departmentID)
        {
            return service.GetDepartment(departmentID);
        }

        public DepartmentDTO SaveDepartment(DepartmentDTO dto)
        {
            return service.SaveDepartment(dto);
        }

        public List<DeliveryTypeDTO> GetDeliveryTypeMaster()
        {
            return service.GetDeliveryTypeMaster();
        }

        public List<Services.Contracts.Search.ColumnDTO> SelectedViewColumns(Services.Contracts.Enums.SearchView view)
        {
            return service.SelectedViewColumns(view);
        }

        public List<PackingTypeDTO> GetPackingTypeMaster()
        {
            return service.GetPackingTypeMaster();
        }

        public List<DocumentTypeDTO> GetDocumentTypesByID(Services.Contracts.Enums.DocumentReferenceTypes referenceType)
        {
            return service.GetDocumentTypesByID(referenceType);
        }

        public List<DocumentTypeDTO> GetDocumentTypesBySystem(Services.Contracts.Enums.Systems system)
        {
            return service.GetDocumentTypesBySystem(system);
        }

        public List<DocumentTypeDTO> GetDocumentTypesByReferenceAndBranch(Services.Contracts.Enums.DocumentReferenceTypes type, long branchID)
        {
            return service.GetDocumentTypesByReferenceAndBranch(type, branchID);
        }

        public DocumentTypeDTO GetDocumentTypesByHeadId(long headID)
        {
            return service.GetDocumentTypesByHeadId(headID);
        }

        public KeyValueDTO GetDefaultCurrency()
        {
            return service.GetDefaultCurrency();
        }

        public List<Services.Contracts.Checkout.PaymentMethodDTO> GetPaymentMethods(int siteID)
        {
            return service.GetPaymentMethods(siteID);
        }

        public ReWriteUrlTypeDTO GetUrlTypeByCode(string code)
        {
            return service.GetUrlTypeByCode(code);
        }

        public KeyValueDTO GetDefaultCurrencyWithName()
        {
            return service.GetDefaultCurrencyWithName();
        }

        public List<BranchDTO> GetMarketPlaceBranches()
        {
            return service.GetMarketPlaceBranches();
        }

        public KeyValueDTO GetDefaultCountryWithName()
        {
            return service.GetDefaultCountryWithName();
        }

        public List<TaxDetailsDTO> GetTaxTemplateDetails(int documentID)
        {
            return service.GetTaxTemplateDetails(documentID);
        }

        public TaxDetailsDTO GetTaxTemplateItem(int taxTemplateItemID)
        {
            return service.GetTaxTemplateItem(taxTemplateItemID);
        }

        public List<CultureDataInfoDTO> GetCultureList()
        {
            return service.GetCultureList();
        }

        public List<KeyValueDTO> GetDynamicLookUpData(DynamicLookUpType lookType, string searchText)
        {
            return service.GetDynamicLookUpData(lookType, searchText);
        }

        public List<KeyValueDTO> GetDynamicLookUpDataForReport(string lookupName, string searchText)
        {
            return service.GetDynamicLookUpDataForReport(lookupName, searchText);
        }

    }
}

