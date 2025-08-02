using System;
using System.Collections.Generic;

namespace Eduegate.Services.Contracts.Inventory
{
    public interface IInventoryService
    {
         StockVerificationDTO GetStockVerification(long headIID);

         StockVerificationDTO SaveStockVerification(StockVerificationDTO verifiedData);

        List<StockVerificationMapDTO> GetProductListsByBranchID(long branchID,DateTime date);
    }
}