using System.Collections.Generic;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Services.Contracts.Accounts.Accounting;

namespace Eduegate.Services.Contracts.Services
{
    public interface ISupplierService
    {
        SupplierDTO GetSupplier(string supplierID);

        List<SupplierDTO> GetSuppliers(string searchText, int dataSize);

        SupplierDTO SaveSupplier(SupplierDTO supplierDTO);

        List<SupplierDTO> GetSupplierBySupplierIdAndCR(string searchText);

        List<SupplierStatusDTO> GetSupplierStatuses();

        ProductPriceListSKUMapDTO GetSKUPriceDetailByBranch(long branchID, long skuMapID);

        List<SupplierAccountEntitlmentMapsDTO> SaveSupplierAccountMaps(List<SupplierAccountEntitlmentMapsDTO> SupplierAccountMapsDTOs);

        SupplierDTO GetSupplierByLoginID(long loginID);

        SKUDTO UpdateSupplierSKUInventory(SKUDTO sku);

        bool UpdateMarketPlaceOrderStatus(MarketPlaceTransactionActionDTO OrderDetail);

        SKUDTO GetSupplierProductPriceListSKUMapDetails(long SKUID);

        KeyValueDTO GetSupplierDeliveryMethod(long supplierID);

        KeyValueDTO GetSupplierReturnMethod(long supplierID); 
    }
}