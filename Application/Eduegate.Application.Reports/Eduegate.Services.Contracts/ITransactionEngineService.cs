using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Services.Contracts
{
    [ServiceContract]
    public interface ITransactionEngineService
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        bool SaveTransaction(List<TransactionHeadDTO> dtoList);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        TransactionDTO SaveTransactions(TransactionDTO transaction);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ProductInventoryDTO> ProcessProductInventory(List<ProductInventoryDTO> dto);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        List<ProductInventoryDTO> UpdateProductInventory(List<ProductInventoryDTO> dto);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        bool SaveInvetoryTransactions(List<InvetoryTransactionDTO> dto);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        bool UpdateTransactionHead(TransactionHeadDTO dto);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        bool CancelTransaction(long headID);
    }
}
