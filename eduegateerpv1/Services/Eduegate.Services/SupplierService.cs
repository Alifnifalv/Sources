using Eduegate.Domain;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Services.Contracts.Services;

namespace Eduegate.Services
{
    public class SupplierService : BaseService, ISupplierService
    {

        public SupplierDTO GetSupplier(string supplierID)
        {
            return new SupplierBL(CallContext).GetSupplier(long.Parse(supplierID));
        }

        public List<SupplierDTO> GetSuppliers(string searchText, int dataSize)
        {
            return new SupplierBL(this.CallContext).GetSuppliers(searchText, dataSize);
        }

        public SupplierDTO SaveSupplier(SupplierDTO supplierDTO)
        {
            return new SupplierBL(CallContext).SaveSupplier(supplierDTO);
        }

        public List<SupplierDTO> GetSupplierBySupplierIdAndCR(string searchText)
        {
            return new SupplierBL(this.CallContext).GetSupplierBySupplierIdAndCR(searchText);
        }

        public List<SupplierStatusDTO> GetSupplierStatuses()
        {
            return new SupplierBL(this.CallContext).GetSupplierStatuses();
        }

        public ProductPriceListSKUMapDTO GetSKUPriceDetailByBranch(long branchID, long skuMapID)
        {
            return new SupplierBL(this.CallContext).GetSKUPriceDetailByBranch(branchID, skuMapID);
        }
        public List<Eduegate.Services.Contracts.Accounting.SupplierAccountEntitlmentMapsDTO> SaveSupplierAccountMaps(List<Eduegate.Services.Contracts.Accounting.SupplierAccountEntitlmentMapsDTO> SupplierAccountMapsDTOs)
        {
            return new SupplierBL(CallContext).SaveSupplierAccountMaps(SupplierAccountMapsDTOs);
        }

        public SupplierDTO GetSupplierByLoginID(long loginID)
        {
            return new SupplierBL(CallContext).GetSupplierByLoginID(loginID);
        }

        public SKUDTO UpdateSupplierSKUInventory(SKUDTO sku)
        {
            return new SupplierBL(CallContext).UpdateSupplierSKUInventory(sku);
        }

        public SKUDTO GetSupplierProductPriceListSKUMapDetails(long SKUID)
        {
            return new SupplierBL(CallContext).GetSupplierProductPriceListSKUMapDetails(SKUID);
        }

        public bool UpdateMarketPlaceOrderStatus(MarketPlaceTransactionActionDTO OrderDetail)
        {
            return new SupplierBL(CallContext).UpdateMarketPlaceOrderStatus(OrderDetail);
        }

        public KeyValueDTO GetSupplierDeliveryMethod(long supplierID)
        {
            return new SupplierBL(CallContext).GetSupplierDeliveryMethod(supplierID);
        }

        public KeyValueDTO GetSupplierReturnMethod(long supplierID)
        { 
            return new SupplierBL(CallContext).GetSupplierReturnMethod(supplierID);
        }
    }
}
