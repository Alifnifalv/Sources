using Eduegate.Framework.Enums;
using Eduegate.TransactionEgine.Accounting.ViewModels;

namespace Eduegate.TransactionEngineCore.Interfaces
{
    public interface IAssetTransactions
    {
        DocumentReferenceTypes ReferenceTypes { get;}
        void Process(AssetTransactionHeadViewModel transaction);
    }
}
