using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.TransactionEgine.Accounting.ViewModels;
using Eduegate.Framework.Enums;
using Eduegate.Services.Contracts.Accounting;

namespace Eduegate.TransactionEgine.Accounting.Interfaces
{
    public interface IRVRegularReceiptAccounting
    {
        DocumentReferenceTypes ReferenceTypes { get;}
        void Process(AccountTransactionHeadDTO transaction);
    }
}
