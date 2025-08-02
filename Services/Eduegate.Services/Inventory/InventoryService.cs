using Eduegate.Services.Contracts.Inventory;
using Eduegate.Framework.Services;
using Eduegate.Domain;

namespace Eduegate.Services.Inventory
{
    public class InventoryService : BaseService, IInventoryService
    {
        public StockVerificationDTO GetStockVerification(long headIID)
        {
            var dto = new TransactionBL(CallContext).GetStockVerificationDatas(headIID);
            return dto;
        }

        public List<StockVerificationMapDTO> GetProductListsByBranchID(long branchID,DateTime date)
        {
            var dto = new TransactionBL(CallContext).GetProductListsByBranchID(branchID,date);
            return dto;
        }

        public StockVerificationDTO SaveStockVerification(StockVerificationDTO verifiedData)
        {
                var dto = new TransactionBL(CallContext).SaveStockVerificationDatas(verifiedData);
                return dto;
        }
    }
}
