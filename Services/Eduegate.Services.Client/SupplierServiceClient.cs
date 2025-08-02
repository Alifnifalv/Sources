using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Accounts.Accounting;

namespace Eduegate.Service.Client
{
    public class SupplierServiceClient : BaseClient, ISupplierService
    {
        private static string ServiceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string service = string.Concat(ServiceHost, Eduegate.Framework.Helper.Constants.SUPPLIER_SERVICE_NAME);

        public SupplierServiceClient(CallContext context = null, Action<string> logger = null)
            :base(context, logger)
        {
        }

        public Services.Contracts.SupplierDTO GetSupplier(string supplierID)
        {
            var result = ServiceHelper.HttpGetRequest(ServiceHost + Eduegate.Framework.Helper.Constants.SUPPLIER_SERVICE_NAME + "GetSupplier/" + supplierID, _callContext);
            return JsonConvert.DeserializeObject<Services.Contracts.SupplierDTO>(result);
        }

        public List<Services.Contracts.SupplierDTO> GetSuppliers(string searchText, int dataSize)
        {
            var uri = string.Format("{0}/GetSuppliers?searchText={1}&dataSize={2}", service, searchText, dataSize);
            return ServiceHelper.HttpGetRequest<List<SupplierDTO>>(uri, _callContext, _logger);
        }

        public Services.Contracts.SupplierDTO SaveSupplier(Services.Contracts.SupplierDTO supplierDTO)
        {
            throw new NotImplementedException();
        }

        public List<Services.Contracts.SupplierDTO> GetSupplierBySupplierIdAndCR(string searchText)
        {
            throw new NotImplementedException();
        }

        public List<Services.Contracts.Mutual.SupplierStatusDTO> GetSupplierStatuses()
        {
            var uri = string.Format("{0}/GetSupplierStatuses", service);
            return ServiceHelper.HttpGetRequest<List<Services.Contracts.Mutual.SupplierStatusDTO>>(uri, _callContext, _logger);
        }

        public ProductPriceListSKUMapDTO GetSKUPriceDetailByBranch(long branchID, long skuMapID)
        {
            var uri = string.Format("{0}/GetSKUPriceDetailByBranch?branchID={1}&skuMapID={2}", service, branchID, skuMapID);
            return ServiceHelper.HttpGetRequest<ProductPriceListSKUMapDTO>(uri, _callContext, _logger);
        }
       
        public List<SupplierAccountEntitlmentMapsDTO> SaveSupplierAccountMaps(List<SupplierAccountEntitlmentMapsDTO>  SupplierAccountMapsDTOs)
        {
            string uri = string.Concat(service + "\\SaveSupplierAccountMaps");
            var request = ServiceHelper.HttpPostRequest(uri, SupplierAccountMapsDTOs);
           return JsonConvert.DeserializeObject<List<SupplierAccountEntitlmentMapsDTO>>(request);
        }

        public SupplierDTO GetSupplierByLoginID(long loginId)
        {
            var uri = string.Format("{0}/GetSupplierByLoginID?loginId={1}", service, loginId);
            return ServiceHelper.HttpGetRequest<SupplierDTO>(uri, _callContext, _logger);
        }

        public SKUDTO UpdateSupplierSKUInventory(SKUDTO sku)
        {
            string uri = string.Concat(service + "UpdateSupplierSKUInventory");
            var response = ServiceHelper.HttpPostRequest(uri, sku, _callContext);
            return JsonConvert.DeserializeObject<SKUDTO>(response);
        }

        public SKUDTO GetSupplierProductPriceListSKUMapDetails(long SKUID)
        {
            var uri = string.Format("{0}/GetSupplierProductPriceListSKUMapDetails?SKUID={1}", service, SKUID);
            return ServiceHelper.HttpGetRequest<SKUDTO>(uri, _callContext, _logger);
        }

        public bool UpdateMarketPlaceOrderStatus(MarketPlaceTransactionActionDTO OrderDetail)
        {
            string uri = string.Concat(service + "UpdateMarketPlaceOrderStatus");
            var response = ServiceHelper.HttpPostRequest(uri, OrderDetail, _callContext);
            return JsonConvert.DeserializeObject<bool>(response);
        } 

        public KeyValueDTO GetSupplierDeliveryMethod(long supplierID)  
        {
            var uri = string.Format("{0}/GetSupplierDeliveryMethod?supplierID={1}", service, supplierID);
            return ServiceHelper.HttpGetRequest<KeyValueDTO>(uri, _callContext, _logger);
        }

        public KeyValueDTO GetSupplierReturnMethod(long supplierID)
        { 
            var uri = string.Format("{0}/GetSupplierReturnMethod?supplierID={1}", service, supplierID);
            return ServiceHelper.HttpGetRequest<KeyValueDTO>(uri, _callContext, _logger);
        }
    }
}
