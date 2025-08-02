using System.ServiceModel;
using Eduegate.Domain;
using Eduegate.Domain.Setting;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;

namespace Eduegate.Services
{
    public class TransactionEngineService : BaseService,ITransactionEngineService
    {
        public bool SaveTransaction(List<Contracts.Catalog.TransactionHeadDTO> dtoList)
        {
            return new TransactionBL(CallContext).SaveTransaction(dtoList);
        }

        public List<ProductInventoryDTO> ProcessProductInventory(List<ProductInventoryDTO> dto)
        {
            return new TransactionBL(CallContext).ProcessProductInventory(dto);
        }

        public List<ProductInventoryDTO> UpdateProductInventory(List<ProductInventoryDTO> dto)
        {
            return new TransactionBL(CallContext).UpdateProductInventory(dto);
        }

        public bool SaveInvetoryTransactions(List<InvetoryTransactionDTO> dto)
        {
            return new TransactionBL(CallContext).SaveInvetoryTransactions(dto);
        }

        public bool UpdateTransactionHead(Contracts.Catalog.TransactionHeadDTO dto)
        {
            return new TransactionBL(CallContext).UpdateTransactionHead(dto);
        }

        public bool CancelTransaction(long headID)
        {
            try
            {
                return new TransactionBL(CallContext).CancelTransaction(headID);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<Transaction>.Fatal(ex.Message.ToString(), ex);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public Contracts.Catalog.TransactionDTO SaveTransactions(Contracts.Catalog.TransactionDTO transaction)
        {
            try
            {
                var dto = new TransactionBL(CallContext).SaveTransactions(transaction);
                var processing = new SettingBL().GetSettingValue("TransactionProcessing", TransactionProcessing.Immediate);

                if (processing == TransactionProcessing.Immediate)
                {
                    new TransactionEngineCore.Transaction(null).StartProcess(0, 0, dto.TransactionHead.HeadIID);
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