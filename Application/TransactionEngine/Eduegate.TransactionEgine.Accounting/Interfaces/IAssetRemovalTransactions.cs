using Eduegate.Framework.Enums;
using Eduegate.Services.Contracts.Accounts.Assets;

namespace Eduegate.TransactionEgine.Accounting.Interfaces
{
    public interface IAssetRemovalTransactions
    {
        DocumentReferenceTypes ReferenceTypes { get;}
        void Process(AssetTransactionHeadDTO transaction);
    }
}
