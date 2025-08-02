using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.Inventory;
using Eduegate.Services.Inventory;

namespace Eduegate.Service.Client.Direct.Inventory
{
    public class InventoryServiceClient : BaseClient, IInventoryService
    {
        InventoryService service = new InventoryService();

        public InventoryServiceClient(CallContext context = null, Action<string> logger = null)
        {
            service.CallContext = context;
        }

        public StockVerificationDTO GetStockVerification(long headIID)
        {
            return service.GetStockVerification(headIID);
        }

        public StockVerificationDTO SaveStockVerification(StockVerificationDTO verifiedData)
        {
            return service.SaveStockVerification(verifiedData);
        }

        public List<StockVerificationMapDTO> GetProductListsByBranchID(long branchID,DateTime date)
        {
            return service.GetProductListsByBranchID(branchID,date);
        }
    }
}
