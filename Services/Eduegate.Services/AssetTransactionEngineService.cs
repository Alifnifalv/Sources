using System.ServiceModel;
using Eduegate.Domain.Accounts.Assets;
using Eduegate.Domain.Setting;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Accounts.Assets;

namespace Eduegate.Services
{
    public class AssetTransactionEngineService : BaseService, IAssetTransactionEngineService
    {
        public bool SaveAssetTransaction(List<AssetTransactionHeadDTO> dtoList)
        {
            return new AssetTransactionBL(CallContext).SaveAssetTransaction(dtoList);
        }

        public List<AssetInventoryDTO> ProcessAssetInventory(List<AssetInventoryDTO> dto)
        {
            return new AssetTransactionBL(CallContext).ProcessAssetInventory(dto);
        }

        public List<AssetInventoryDTO> UpdateAssetInventory(List<AssetInventoryDTO> dto)
        {
            return new AssetTransactionBL(CallContext).UpdateAssetInventory(dto);
        }

        public bool SaveAssetInvetoryTransactions(List<AssetInventoryTransactionDTO> dto)
        {
            return new AssetTransactionBL(CallContext).SaveAssetInventoryTransactions(dto);
        }

        public bool UpdateAssetTransactionHead(AssetTransactionHeadDTO dto)
        {
            return new AssetTransactionBL(CallContext).UpdateAssetTransactionHead(dto);
        }

        public bool CancelAssetTransaction(long headID)
        {
            try
            {
                return new AssetTransactionBL(CallContext).CancelAssetTransaction(headID);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<Transaction>.Fatal(ex.Message.ToString(), ex);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public AssetTransactionDTO SaveAssetTransactions(AssetTransactionDTO transaction)
        {
            try
            {
                var dto = new AssetTransactionBL(CallContext).SaveAssetTransactions(transaction);
                var processing = new SettingBL().GetSettingValue("TransactionProcessing", TransactionProcessing.Immediate);

                if (processing == TransactionProcessing.Immediate)
                {
                    new TransactionEngineCore.AssetTransaction(null, CallContext).StartProcess(0, 0, dto.AssetTransactionHead.HeadIID);
                }

                return dto;
            }

            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<Transaction>.Fatal(ex.Message.ToString(), ex);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

    }
}