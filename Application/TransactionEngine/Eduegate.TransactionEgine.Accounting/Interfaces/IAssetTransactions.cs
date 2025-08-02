using Eduegate.TransactionEgine.Accounting.ViewModels;
using Eduegate.Framework.Enums;

namespace Eduegate.TransactionEgine.Accounting.Interfaces
{
    public interface IAssetTransactions
    {
        DocumentReferenceTypes ReferenceTypes { get;}

        void Process(AssetTransactionHeadViewModel transaction);
    }
}