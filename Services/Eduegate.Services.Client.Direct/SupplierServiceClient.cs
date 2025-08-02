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
using Eduegate.Services;
using Eduegate.Services.Contracts.Accounts.Accounting;

namespace Eduegate.Service.Client.Direct
{
    public class SupplierServiceClient : ISupplierService
    {
        SupplierService service = new SupplierService();

        public SupplierServiceClient(CallContext context = null, Action<string> logger = null)
        {
            service.CallContext = context;
        }

        public Services.Contracts.SupplierDTO GetSupplier(string supplierID)
        {
            return service.GetSupplier(supplierID);
        }

        public List<Services.Contracts.SupplierDTO> GetSuppliers(string searchText, int dataSize)
        {
            return service.GetSuppliers(searchText, dataSize);
        }

        public Services.Contracts.SupplierDTO SaveSupplier(Services.Contracts.SupplierDTO supplierDTO)
        {
            return service.SaveSupplier(supplierDTO);
        }

        public List<Services.Contracts.SupplierDTO> GetSupplierBySupplierIdAndCR(string searchText)
        {
            return service.GetSupplierBySupplierIdAndCR(searchText);
        }

        public List<Services.Contracts.Mutual.SupplierStatusDTO> GetSupplierStatuses()
        {
            return service.GetSupplierStatuses();
        }

        public ProductPriceListSKUMapDTO GetSKUPriceDetailByBranch(long branchID, long skuMapID)
        {
            return service.GetSKUPriceDetailByBranch(branchID, skuMapID);
        }
       
        public List<SupplierAccountEntitlmentMapsDTO> SaveSupplierAccountMaps(List<SupplierAccountEntitlmentMapsDTO>  SupplierAccountMapsDTOs)
        {
            return service.SaveSupplierAccountMaps(SupplierAccountMapsDTOs);
        }

        public SupplierDTO GetSupplierByLoginID(long loginId)
        {
            return service.GetSupplierByLoginID(loginId);
        }

        public SKUDTO UpdateSupplierSKUInventory(SKUDTO sku)
        {
            return service.UpdateSupplierSKUInventory(sku);
        }

        public SKUDTO GetSupplierProductPriceListSKUMapDetails(long SKUID)
        {
            return service.GetSupplierProductPriceListSKUMapDetails(SKUID);
        }

        public bool UpdateMarketPlaceOrderStatus(MarketPlaceTransactionActionDTO OrderDetail)
        {
            return service.UpdateMarketPlaceOrderStatus(OrderDetail);
        } 

        public KeyValueDTO GetSupplierDeliveryMethod(long supplierID)  
        {
            return service.GetSupplierDeliveryMethod(supplierID);
        }

        public KeyValueDTO GetSupplierReturnMethod(long supplierID)
        {
            return service.GetSupplierReturnMethod(supplierID);
        }
    }
}
