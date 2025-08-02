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
using Eduegate.Services.Contracts.Enums.Common;
using Eduegate.Services.Contracts.Inventory;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Services.Contracts.UrlReWriter;

namespace Eduegate.Service.Client
{
    public class ReferenceDataServiceClient : BaseClient, IReferenceData
    {
        private static string serviceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string referenceDataService = string.Concat(serviceHost, Eduegate.Framework.Helper.Constants.REFERENCE_DATA_SERVICE);

        public ReferenceDataServiceClient(CallContext context = null, Action<string> logger = null)
            : base(context, logger)
        {
        }

        public List<CountryDTO> GetCountries(bool isActiveCurrency)
        {
            return ServiceHelper.HttpGetRequest<List<CountryDTO>>(string.Concat(referenceDataService, "GetCountries?isActiveCurrency=", isActiveCurrency), _callContext, _logger);
        }

        //Get passport issue country masters
        public List<CountryMasterDTO> GetPassportIssueCountryMasters()
        {
            throw new NotImplementedException();
        }
        public List<CheckOutDTO> GetShippingAddressMasters()
        {
            throw new NotImplementedException();
        }

        public List<CheckOutDTO> GetBillingAddressMasters()
        {
            throw new NotImplementedException();
        }

        public List<KeyValueDTO> GetLookUpData(Eduegate.Services.Contracts.Enums.LookUpTypes lookType, string searchText = "", int dataSize = 0, int optionalId = 0)
        {
            return ServiceHelper.HttpGetRequest<List<KeyValueDTO>>(referenceDataService + "GetLookUpData?lookType=" + lookType + "&dataSize=" + dataSize + "&searchText=" + searchText + "&optionalId=" + optionalId, _callContext, _logger);
        }

        public List<KeyValueDTO> GetDBLookUpData(string lookType)
        {
            return ServiceHelper.HttpGetRequest<List<KeyValueDTO>>(referenceDataService + "GetLookUpData?lookType=" + lookType , _callContext, _logger);
        }

        public List<LoginStatusDTO> GetLoginStatus()
        {
            return ServiceHelper.HttpGetRequest<List<LoginStatusDTO>>(string.Concat(referenceDataService, "GetLoginStatus"), _callContext, _logger);
        }

        public List<CustomerStatusDTO> GetCustomerStatus()
        {
            return ServiceHelper.HttpGetRequest<List<CustomerStatusDTO>>(string.Concat(referenceDataService, "GetCustomerStatus"), _callContext, _logger);
        }

        public Services.Contracts.Mutual.WarehouseDTO GetWarehouse(long warehouseID)
        {
            return ServiceHelper.HttpGetRequest<WarehouseDTO>(string.Concat(referenceDataService, "GetWarehouse?warehouseID=", warehouseID), _callContext, _logger);
        }

        public Services.Contracts.Mutual.WarehouseDTO SaveWarehouse(Services.Contracts.Mutual.WarehouseDTO wareHouse)
        {
            var uri = string.Format("{0}/SaveWarehouse", referenceDataService);
            return ServiceHelper.HttpPostGetRequest<WarehouseDTO>(uri, wareHouse, _callContext, _logger);
        }


        public BranchDTO SaveBranch(BranchDTO transaction)
        {
            var uri = string.Format("{0}/SaveBranch", referenceDataService);
            return ServiceHelper.HttpPostGetRequest<BranchDTO>(uri, transaction, _callContext, _logger);
        }

        public BranchDTO GetBranch(long branchID)
        {
            return ServiceHelper.HttpGetRequest<BranchDTO>(string.Concat(referenceDataService, "GetBranch?branchID=", branchID), _callContext, _logger);
        }

        public BranchGroupDTO SaveBranchGroup(BranchGroupDTO transaction)
        {
            var uri = string.Format("{0}/SaveBranchGroup", referenceDataService);
            return ServiceHelper.HttpPostGetRequest<BranchGroupDTO>(uri, transaction, _callContext, _logger);
        }

        public BranchGroupDTO GetBranchGroup(long branchGroupID)
        {
            return ServiceHelper.HttpGetRequest<BranchGroupDTO>(string.Concat(referenceDataService, "GetBranchGroup?branchGroupID=", branchGroupID), _callContext, _logger);
        }

        public CompanyDTO GetCompany(long companyID)
        {
            return ServiceHelper.HttpGetRequest<CompanyDTO>(string.Concat(referenceDataService, "GetCompany?companyID=", companyID), _callContext, _logger);
        }

        public List<CompanyDTO> GetCompanies()
        {
            return ServiceHelper.HttpGetRequest<List<CompanyDTO>>(string.Concat(referenceDataService, "GetCompanies"), _callContext, _logger);
        }

        public CompanyDTO SaveCompany(CompanyDTO dto)
        {
            var uri = string.Format("{0}/SaveCompany", referenceDataService);
            return ServiceHelper.HttpPostGetRequest<CompanyDTO>(uri, dto, _callContext, _logger);
        }

        public DepartmentDTO GetDepartment(long departmentID)
        {
            return ServiceHelper.HttpGetRequest<DepartmentDTO>(string.Concat(referenceDataService, "GetDepartment?departmentID=", departmentID), _callContext, _logger);
        }

        public DepartmentDTO SaveDepartment(DepartmentDTO dto)
        {
            var uri = string.Format("{0}/SaveDepartment", referenceDataService);
            return ServiceHelper.HttpPostGetRequest<DepartmentDTO>(uri, dto, _callContext, _logger);
        }

        public List<DeliveryTypeDTO> GetDeliveryTypeMaster()
        {
            string uri = string.Concat(referenceDataService, "GetDeliveryTypeMaster");
            return ServiceHelper.HttpGetRequest<List<DeliveryTypeDTO>>(uri, _callContext, _logger);
        }

        public List<Services.Contracts.Search.ColumnDTO> SelectedViewColumns(Services.Contracts.Enums.SearchView view)
        {
            return ServiceHelper.HttpGetRequest<List<Services.Contracts.Search.ColumnDTO>>
                (string.Concat(referenceDataService, "SelectedViewColumns?view=", view), _callContext, _logger);
        }

        public List<PackingTypeDTO> GetPackingTypeMaster()
        {
            string uri = string.Concat(referenceDataService, "GetPackingTypeMaster");
            return ServiceHelper.HttpGetRequest<List<PackingTypeDTO>>(uri, _callContext, _logger);
        }

        public List<DocumentTypeDTO> GetDocumentTypesByID(Services.Contracts.Enums.DocumentReferenceTypes referenceType)
        {
            return ServiceHelper.HttpGetRequest<List<DocumentTypeDTO>>(referenceDataService + "GetDocumentTypesByID?referenceType=" + referenceType, _callContext);
        }

        public List<DocumentTypeDTO> GetDocumentTypesBySystem(Services.Contracts.Enums.Systems system)
        {
            return ServiceHelper.HttpGetRequest<List<DocumentTypeDTO>>(referenceDataService + "GetDocumentTypesBySystem?system=" + system, _callContext);
        }

        public List<DocumentTypeDTO> GetDocumentTypesByReferenceAndBranch(Services.Contracts.Enums.DocumentReferenceTypes type, long branchID)
        {
            return ServiceHelper.HttpGetRequest<List<DocumentTypeDTO>>(referenceDataService + "GetDocumentTypesByReferenceAndBranch?type=" + type + "&branchID=" + branchID, _callContext);
        }

        public DocumentTypeDTO GetDocumentTypesByHeadId(long headID)
        {
            return ServiceHelper.HttpGetRequest<DocumentTypeDTO>(referenceDataService + "GetDocumentReferenceTypesByTransNo?headID=" + headID, _callContext);
        }
        public KeyValueDTO GetDefaultCurrency()
        {
            return ServiceHelper.HttpGetRequest<KeyValueDTO>(referenceDataService + "GetDefaultCurrency", _callContext);
        }

        public List<Services.Contracts.Checkout.PaymentMethodDTO> GetPaymentMethods(int siteID)
        {
            return ServiceHelper.HttpGetRequest<List<Services.Contracts.Checkout.PaymentMethodDTO>>(referenceDataService + "GetPaymentMethods?siteID=" + siteID, _callContext);
        }

        public ReWriteUrlTypeDTO GetUrlTypeByCode(string code)
        {
            return ServiceHelper.HttpGetRequest<ReWriteUrlTypeDTO>(referenceDataService + "GetUrlTypeByCode?code=" + code, _callContext);
        }


        public KeyValueDTO GetDefaultCurrencyWithName()
        {
            return ServiceHelper.HttpGetRequest<KeyValueDTO>(referenceDataService + "GetDefaultCurrencyWithName", _callContext);
        }

        public List<BranchDTO> GetMarketPlaceBranches()
        {
            return ServiceHelper.HttpGetRequest<List<BranchDTO>>(referenceDataService + "GetMarketPlaceBranches", _callContext);
        }

        public KeyValueDTO GetDefaultCountryWithName()
        {
            return ServiceHelper.HttpGetRequest<KeyValueDTO>(referenceDataService + "GetDefaultCountryWithName", _callContext);
        }

        public List<TaxDetailsDTO> GetTaxTemplateDetails(int documentID)
        {
            return ServiceHelper.HttpGetRequest<List<TaxDetailsDTO>>(referenceDataService + "GetTaxTemplateDetails?documentID=" + documentID, _callContext);
        }

        public TaxDetailsDTO GetTaxTemplateItem(int taxTemplateItemID)
        {
            return ServiceHelper.HttpGetRequest<TaxDetailsDTO>(referenceDataService + "GetTaxTemplateItem?taxTemplateItemID=" + taxTemplateItemID, _callContext);
        }

        public List<CultureDataInfoDTO> GetCultureList()
        {
            return ServiceHelper.HttpGetRequest<List<CultureDataInfoDTO>>(referenceDataService + "GetCultureList", _callContext);
        }

        public List<KeyValueDTO> GetDynamicLookUpData(DynamicLookUpType lookType, string searchText)
        {
            return ServiceHelper.HttpGetRequest<List<KeyValueDTO>>(referenceDataService + "GetDynamicLookUpData?lookType" + lookType.ToString() + "&searchText=" + searchText, _callContext);
        }

        public List<KeyValueDTO> GetDynamicLookUpDataForReport(string lookupName, string searchText)
        {
            return ServiceHelper.HttpGetRequest<List<KeyValueDTO>>(referenceDataService + "GetDynamicLookUpDataForReport?lookType" + lookupName + "&searchText=" + searchText, _callContext);
        }

    }
}

