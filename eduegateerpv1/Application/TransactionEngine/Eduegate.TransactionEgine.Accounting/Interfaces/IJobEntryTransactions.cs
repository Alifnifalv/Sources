using Eduegate.Framework.Enums;
using Eduegate.Services.Contracts.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Eduegate.TransactionEgine.Accounting.Interfaces
{
    public interface IJobEntryTransactions
    {
        DocumentReferenceTypes ReferenceTypes { get;}
        void Process(JobEntryHeadAccountingDTO jobEntryHeadAccountingDTO);
    }
}
