using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Services.Contracts.Checkout;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Services.Contracts.Search;
using Eduegate.Services.Contracts.UrlReWriter;
using Eduegate.Services.Contracts.Inventory;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Enums.Common;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IReferenceData" in both code and config file together.
    [ServiceContract]
    public interface IReferenceData
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCountries?isActiveCurrency={isActiveCurrency}")]
        List<CountryDTO> GetCountries(bool isActiveCurrency);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetPassportIssueCountryMasters")]
        List<CountryMasterDTO> GetPassportIssueCountryMasters();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetShippingAddressMasters")]
        List<CheckOutDTO> GetShippingAddressMasters();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetBillingAddressMasters")]
        List<CheckOutDTO> GetBillingAddressMasters();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetLookUpData?lookType={lookType}&searchText={searchText}&dataSize={dataSize}&optionalId={optionalId}")]
        List<KeyValueDTO> GetLookUpData(Eduegate.Services.Contracts.Enums.LookUpTypes lookType, string searchText, int dataSize, int optionalId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetDBLookUpData?lookType={lookType}")]
        List<KeyValueDTO> GetDBLookUpData(string lookType);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetLoginStatus")]
        List<LoginStatusDTO> GetLoginStatus();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCustomerStatus")]
        List<CustomerStatusDTO> GetCustomerStatus();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetWarehouse?warehouseID={warehouseID}")]
        WarehouseDTO GetWarehouse(long warehouseID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveWarehouse")]
        WarehouseDTO SaveWarehouse(WarehouseDTO transaction);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetBranch?branchID={branchID}")]
        BranchDTO GetBranch(long branchID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveBranch")]
        BranchDTO SaveBranch(BranchDTO transaction);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetBranchGroup?branchGroupID={branchGroupID}")]
        BranchGroupDTO GetBranchGroup(long branchGroupID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveBranchGroup")]
        BranchGroupDTO SaveBranchGroup(BranchGroupDTO transaction);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCompanies")]
        List<CompanyDTO> GetCompanies();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCompany?companyID={companyID}")]
        CompanyDTO GetCompany(long companyID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveCompany")]
        CompanyDTO SaveCompany(CompanyDTO transaction);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetDepartment?departmentID={departmentID}")]
        DepartmentDTO GetDepartment(long departmentID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveDepartment")]
        DepartmentDTO SaveDepartment(DepartmentDTO transaction);


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetDeliveryTypeMaster")]
        List<Catalog.DeliveryTypeDTO> GetDeliveryTypeMaster();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SelectedViewColumns?view={view}")]
        List<ColumnDTO> SelectedViewColumns(SearchView view);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetPackingTypeMaster")]
        List<Catalog.PackingTypeDTO> GetPackingTypeMaster();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetDocumentTypesByID?referenceType={referenceType}")]
        List<DocumentTypeDTO> GetDocumentTypesByID(Eduegate.Services.Contracts.Enums.DocumentReferenceTypes referenceType);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetDocumentTypesBySystem?system={system}")]
        List<DocumentTypeDTO> GetDocumentTypesBySystem(Eduegate.Services.Contracts.Enums.Systems system);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetDocumentTypesByReferenceAndBranch?type={type}&branchID={branchID}")]
        List<DocumentTypeDTO> GetDocumentTypesByReferenceAndBranch(DocumentReferenceTypes type, long branchID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetDocumentReferenceTypesByTransNo?headID={headID}")]
        DocumentTypeDTO GetDocumentTypesByHeadId(long headID);


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetDefaultCurrency")]
        KeyValueDTO GetDefaultCurrency();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetDefaultCurrencyWithName")]
        KeyValueDTO GetDefaultCurrencyWithName();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetDefaultCountryWithName")]
        KeyValueDTO GetDefaultCountryWithName();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetPaymentMethods?siteID={siteID}")]
        List<PaymentMethodDTO> GetPaymentMethods(int siteID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetUrlTypeByCode?code={code}")]
        ReWriteUrlTypeDTO GetUrlTypeByCode(string code);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetMarketPlaceBranches")]
        List<BranchDTO> GetMarketPlaceBranches();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetTaxTemplateDetails?documentID={documentID}")]
        List<TaxDetailsDTO> GetTaxTemplateDetails(int documentID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetTaxTemplateItem?taxTemplateItemID={taxTemplateItemID}")]
        TaxDetailsDTO GetTaxTemplateItem(int taxTemplateItemID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCultureList")]
        List<CultureDataInfoDTO> GetCultureList();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetDBLookUpData?lookType={lookType}&searchText={searchText}")]
        List<KeyValueDTO> GetDynamicLookUpData(DynamicLookUpType lookType, string searchText);
    }
}
