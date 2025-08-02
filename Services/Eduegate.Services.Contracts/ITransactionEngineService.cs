using System.Collections.Generic;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Services.Contracts
{
    public interface ITransactionEngineService
    {
        bool SaveTransaction(List<TransactionHeadDTO> dtoList);

        TransactionDTO SaveTransactions(TransactionDTO transaction);

        List<ProductInventoryDTO> ProcessProductInventory(List<ProductInventoryDTO> dto);

        List<ProductInventoryDTO> UpdateProductInventory(List<ProductInventoryDTO> dto);

        bool SaveInvetoryTransactions(List<InvetoryTransactionDTO> dto);

        bool UpdateTransactionHead(TransactionHeadDTO dto);

        bool CancelTransaction(long headID);
    }
}