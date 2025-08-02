using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.Inventory;

namespace Eduegate.Service.Client.Inventory
{
    public class InventoryServiceClient : BaseClient, IInventoryService
    {
         private static string serviceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string productService = string.Concat(serviceHost, Eduegate.Framework.Helper.Constants.INVENTORY_SERVICE_NAME);
        public InventoryServiceClient(CallContext context = null, Action<string> logger = null)
            : base(context, logger)
        {
        }

        public StockVerificationDTO GetStockVerification(long headIID)
        {
            throw new NotImplementedException();
        }

        public StockVerificationDTO SaveStockVerification(StockVerificationDTO verifiedData)
        {
            throw new NotImplementedException();
        }

        public List<StockVerificationMapDTO> GetProductListsByBranchID(long branchID,DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
