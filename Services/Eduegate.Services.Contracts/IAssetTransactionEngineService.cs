using System.Collections.Generic;
using Eduegate.Services.Contracts.Accounts.Assets;

namespace Eduegate.Services.Contracts
{
    public interface IAssetTransactionEngineService
    {
        bool SaveAssetTransaction(List<AssetTransactionHeadDTO> dtoList);

        AssetTransactionDTO SaveAssetTransactions(AssetTransactionDTO transaction);

        List<AssetInventoryDTO> ProcessAssetInventory(List<AssetInventoryDTO> dto);

        List<AssetInventoryDTO> UpdateAssetInventory(List<AssetInventoryDTO> dto);

        bool SaveAssetInvetoryTransactions(List<AssetInventoryTransactionDTO> dto);

        bool UpdateAssetTransactionHead(AssetTransactionHeadDTO dto);

        bool CancelAssetTransaction(long headID);
    }
}