using Eduegate.Framework.Enums;
using Eduegate.Services.Contracts.Jobs;

namespace Eduegate.TransactionEgine.Accounting.Interfaces
{
    public interface IJobEntryTransactions
    {
        DocumentReferenceTypes ReferenceTypes { get;}
        void Process(JobEntryHeadAccountingDTO jobEntryHeadAccountingDTO);
    }
}
