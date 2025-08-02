using System.Collections.Generic;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Services.Contracts.Checkout;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Services.Contracts.Search;
using Eduegate.Services.Contracts.UrlReWriter;
using Eduegate.Services.Contracts.Inventory;
using Eduegate.Services.Contracts.Enums.Common;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IReferenceData" in both code and config file together.
    public interface IReferenceData
    {
        List<CountryDTO> GetCountries(bool isActiveCurrency);

        List<CountryMasterDTO> GetPassportIssueCountryMasters();

        List<CheckOutDTO> GetShippingAddressMasters();

        List<CheckOutDTO> GetBillingAddressMasters();

        List<KeyValueDTO> GetLookUpData(Eduegate.Services.Contracts.Enums.LookUpTypes lookType, string searchText, int dataSize, int optionalId);

        List<KeyValueDTO> GetDBLookUpData(string lookType);

        List<LoginStatusDTO> GetLoginStatus();

        List<CustomerStatusDTO> GetCustomerStatus();

        WarehouseDTO GetWarehouse(long warehouseID);

        WarehouseDTO SaveWarehouse(WarehouseDTO transaction);

        BranchDTO GetBranch(long branchID);

        BranchDTO SaveBranch(BranchDTO transaction);

        BranchGroupDTO GetBranchGroup(long branchGroupID);

        BranchGroupDTO SaveBranchGroup(BranchGroupDTO transaction);

        List<CompanyDTO> GetCompanies();

        CompanyDTO GetCompany(long companyID);

        CompanyDTO SaveCompany(CompanyDTO transaction);

        DepartmentDTO GetDepartment(long departmentID);

        DepartmentDTO SaveDepartment(DepartmentDTO transaction);


        List<Catalog.DeliveryTypeDTO> GetDeliveryTypeMaster();

        List<ColumnDTO> SelectedViewColumns(SearchView view);

        List<Catalog.PackingTypeDTO> GetPackingTypeMaster();

        List<DocumentTypeDTO> GetDocumentTypesByID(Eduegate.Services.Contracts.Enums.DocumentReferenceTypes referenceType);

        List<DocumentTypeDTO> GetDocumentTypesBySystem(Eduegate.Services.Contracts.Enums.Systems system);

        List<DocumentTypeDTO> GetDocumentTypesByReferenceAndBranch(DocumentReferenceTypes type, long branchID);

        DocumentTypeDTO GetDocumentTypesByHeadId(long headID);

        KeyValueDTO GetDefaultCurrency();

        KeyValueDTO GetDefaultCurrencyWithName();

        KeyValueDTO GetDefaultCountryWithName();

        List<PaymentMethodDTO> GetPaymentMethods(int siteID);

        ReWriteUrlTypeDTO GetUrlTypeByCode(string code);

        List<BranchDTO> GetMarketPlaceBranches();

        List<TaxDetailsDTO> GetTaxTemplateDetails(int documentID);

        TaxDetailsDTO GetTaxTemplateItem(int taxTemplateItemID);

        List<CultureDataInfoDTO> GetCultureList();

        List<KeyValueDTO> GetDynamicLookUpData(DynamicLookUpType lookType, string searchText);

        List<KeyValueDTO> GetDynamicLookUpDataForReport(string lookupName, string searchText);
    }
}