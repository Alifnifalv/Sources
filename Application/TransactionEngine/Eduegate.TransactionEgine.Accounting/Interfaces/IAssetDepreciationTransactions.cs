using Eduegate.Framework.Enums;
using Eduegate.Services.Contracts.Accounts.Assets;

namespace Eduegate.TransactionEgine.Accounting.Interfaces
{
    public interface IAssetDepreciationTransactions
    {
        DocumentReferenceTypes ReferenceTypes { get;}
        void Process(AssetTransactionHeadDTO transaction);
    }
}
