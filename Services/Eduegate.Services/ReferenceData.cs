using System.ServiceModel;
using Eduegate.Domain;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Enums.Common;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Services.Contracts.UrlReWriter;
using Eduegate.Services.Contracts.Accounts.Taxes;

namespace Eduegate.Services
{
    public class ReferenceData : BaseService, IReferenceData
    {        
        CountryMasterBL countryMasterBL = new CountryMasterBL();

        public ReferenceData()
        {
            
        }

        //Get country masters
        public List<CountryDTO> GetCountries(bool isActiveCurrency)
        {
            try
            {
                var referenceDataBL = new ReferenceDataBL(CallContext);
                List<CountryDTO> countryDetails = referenceDataBL.GetCountries(isActiveCurrency);
                return countryDetails;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ReferenceData>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        //Get passport issue country masters
        public List<CountryMasterDTO> GetPassportIssueCountryMasters()
        {
            try
            {
                var referenceDataBL = new ReferenceDataBL(CallContext);
                List<CountryMasterDTO> countryMasterDetails = referenceDataBL.GetPassportIssueCountryMasters();
                return countryMasterDetails;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Product>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        //Get shipping address
        public List<CheckOutDTO> GetShippingAddressMasters()
        {
            try
            {
                var referenceDataBL = new ReferenceDataBL(CallContext);
                List<CheckOutDTO> shippingAddressDTOList = referenceDataBL.GetShippingAddressMasters();
                return shippingAddressDTOList;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Product>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        //Get billing address
        public List<CheckOutDTO> GetBillingAddressMasters()
        {
            try
            {
                var referenceDataBL = new ReferenceDataBL(CallContext);
                List<CheckOutDTO> billingAddressDTOList = referenceDataBL.GetBillingAddressMasters();
                return billingAddressDTOList;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Product>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<KeyValueDTO> GetLookUpData(Eduegate.Services.Contracts.Enums.LookUpTypes lookType, string searchText, int dataSize, int optionalId)
        {
            try
            {
                var referenceDataBL = new ReferenceDataBL(CallContext);
                return referenceDataBL.GetLookUpData(lookType, searchText, dataSize, optionalId);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Product>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<KeyValueDTO> GetDBLookUpData(string lookType)
        {
            try
            {
                var referenceDataBL = new ReferenceDataBL(CallContext);
                return referenceDataBL.GetDBLookUpData(lookType);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Product>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<LoginStatusDTO> GetLoginStatus()
        {
            try
            {
                var referenceDataBL = new ReferenceDataBL(CallContext);
                return referenceDataBL.GetLoginStatus();
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<LoginStatusDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<CustomerStatusDTO> GetCustomerStatus()
        {
            try
            {
                var referenceDataBL = new ReferenceDataBL(CallContext);
                return referenceDataBL.GetCustomerStatus();
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<LoginStatusDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public Contracts.Mutual.WarehouseDTO GetWarehouse(long warehouseID)
        {
            try
            {
                var referenceDataBL = new ReferenceDataBL(CallContext);
                return referenceDataBL.GetWarehouse(warehouseID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Contracts.Mutual.WarehouseDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public Contracts.Mutual.WarehouseDTO SaveWarehouse(Contracts.Mutual.WarehouseDTO transaction)
        {
            try
            {
                var referenceDataBL = new ReferenceDataBL(CallContext);
                return referenceDataBL.SaveWarehouse(transaction);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Contracts.Mutual.WarehouseDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public Contracts.Mutual.BranchDTO GetBranch(long branchID)
        {
            try
            {
                var referenceDataBL = new ReferenceDataBL(CallContext);
                return referenceDataBL.GetBranch(branchID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Contracts.Mutual.BranchDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public Contracts.Mutual.BranchDTO SaveBranch(Contracts.Mutual.BranchDTO transaction)
        {
            try
            {
                var referenceDataBL = new ReferenceDataBL(CallContext);
                return referenceDataBL.SaveBranch(transaction);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Contracts.Mutual.BranchDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public Contracts.Mutual.BranchGroupDTO GetBranchGroup(long branchID)
        {
            try
            {
                var referenceDataBL = new ReferenceDataBL(CallContext);
                return referenceDataBL.GetBranchGroup(branchID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Contracts.Mutual.BranchGroupDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public Contracts.Mutual.BranchGroupDTO SaveBranchGroup(Contracts.Mutual.BranchGroupDTO transaction)
        {
            try
            {
                var referenceDataBL = new ReferenceDataBL(CallContext);
                return referenceDataBL.SaveBranchGroup(transaction);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Contracts.Mutual.BranchGroupDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }


        public Contracts.Mutual.CompanyDTO GetCompany(long companyID)
        {
            try
            {
                var referenceDataBL = new ReferenceDataBL(CallContext);
                return referenceDataBL.GetCompany(companyID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Contracts.Mutual.CompanyDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<Contracts.Mutual.CompanyDTO> GetCompanies()
        {
            try
            {
                var referenceDataBL = new ReferenceDataBL(CallContext);
                return referenceDataBL.GetCompanies();
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Contracts.Mutual.BranchGroupDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public Contracts.Mutual.CompanyDTO SaveCompany(Contracts.Mutual.CompanyDTO transaction)
        {
            try
            {
                var referenceDataBL = new ReferenceDataBL(CallContext);
                return referenceDataBL.SaveCompany(transaction);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Contracts.Mutual.BranchGroupDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public Contracts.Mutual.DepartmentDTO GetDepartment(long departmentID)
        {
            try
            {
                var referenceDataBL = new ReferenceDataBL(CallContext);
                return referenceDataBL.GetDepartment(departmentID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Contracts.Mutual.BranchGroupDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public Contracts.Mutual.DepartmentDTO SaveDepartment(Contracts.Mutual.DepartmentDTO transaction)
        {
            try
            {
                var referenceDataBL = new ReferenceDataBL(CallContext);
                return referenceDataBL.SaveDepartment(transaction);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Contracts.Mutual.BranchGroupDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<Contracts.Search.ColumnDTO> SelectedViewColumns(Contracts.Enums.SearchView view)
        {
            try
            {
                var referenceDataBL = new ReferenceDataBL(CallContext);
                return referenceDataBL.SelectedViewColumns(view);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<Contracts.Search.ColumnDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<DeliveryTypeDTO> GetDeliveryTypeMaster()
        {
            var referenceDataBL = new ReferenceDataBL(CallContext);
            return referenceDataBL.GetDeliveryTypeMaster();
        }

        public List<PackingTypeDTO> GetPackingTypeMaster()
        {
            var referenceDataBL = new ReferenceDataBL(CallContext);
            return referenceDataBL.GetPackingTypeMaster();
        }

        public List<DocumentTypeDTO> GetDocumentTypesByID(Services.Contracts.Enums.DocumentReferenceTypes referenceType)
        {
            var referenceDataBL = new ReferenceDataBL(CallContext);
            return referenceDataBL.GetDocumentTypesByID(referenceType);
        }

        public List<DocumentTypeDTO> GetDocumentTypesBySystem(Contracts.Enums.Systems system)
        {
            var referenceDataBL = new ReferenceDataBL(CallContext);
            return referenceDataBL.GetDocumentTypesBySystem(system);
        }

        public List<DocumentTypeDTO> GetDocumentTypesByReferenceAndBranch(Contracts.Enums.DocumentReferenceTypes type, long branchID)
        {
            var referenceDataBL = new ReferenceDataBL(CallContext);
            return referenceDataBL.GetDocumentTypesByReferenceAndBranch((int)type, branchID);
        }

        public DocumentTypeDTO GetDocumentTypesByHeadId(long headID)
        {
            var referenceDataBL = new ReferenceDataBL(CallContext);
            return referenceDataBL.GetDocumentTypesByHeadId(headID);
        }

        public KeyValueDTO GetDefaultCurrency()
        {
            var referenceDataBL = new ReferenceDataBL(CallContext);
            return referenceDataBL.GetDefaultCurrency();
        }

        public KeyValueDTO GetDefaultCurrencyWithName()
        {
            var referenceDataBL = new ReferenceDataBL(CallContext);
            return referenceDataBL.GetDefaultCurrencyWithName();
        }

        public List<Contracts.Checkout.PaymentMethodDTO> GetPaymentMethods(int siteID)
        {
            var referenceDataBL = new ReferenceDataBL(CallContext);
            return referenceDataBL.GetPaymentMethods(siteID);
        }

        public ReWriteUrlTypeDTO GetUrlTypeByCode(string code)
        {
            var referenceDataBL = new ReferenceDataBL(CallContext);
            return referenceDataBL.GetUrlTypeByCode(code);
        }

        public List<BranchDTO> GetMarketPlaceBranches()
        {
            var referenceDataBL = new ReferenceDataBL(CallContext);
            return referenceDataBL.GetMarketPlaceBranches();
        }

        public KeyValueDTO GetDefaultCountryWithName()
        {
            var referenceDataBL = new ReferenceDataBL(CallContext);
            return referenceDataBL.GetDefaultCountryWithName();
        }

        public List<TaxDetailsDTO> GetTaxTemplateDetails(int documentID)
        {
            return new ReferenceDataBL(CallContext).GetTaxTemplateDetails(documentID);
        }

        public TaxDetailsDTO GetTaxTemplateItem(int taxTemplateItemID)
        {
            return new ReferenceDataBL(CallContext).GetTaxTemplateItem(taxTemplateItemID);
        }

        public List<CultureDataInfoDTO> GetCultureList()
        {
            return new ReferenceDataBL(CallContext).GetCultureList();
        }

        public List<KeyValueDTO> GetDynamicLookUpData(DynamicLookUpType lookType, string searchText)
        {
            try
            {
                List<KeyValueDTO> lookUpData = new ReferenceDataBL(CallContext).GetDynamicLookUpData(lookType, searchText);
                return lookUpData;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ReferenceData>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<KeyValueDTO> GetDynamicLookUpDataForReport(string lookupName, string searchText)
        {
            try
            {
                List<KeyValueDTO> lookUpData = new ReferenceDataBL(CallContext).GetDynamicLookUpDataForReport(lookupName, searchText);
                return lookUpData;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ReferenceData>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

    }
}
