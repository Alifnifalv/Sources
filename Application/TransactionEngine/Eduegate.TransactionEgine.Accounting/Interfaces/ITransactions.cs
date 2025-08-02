using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.TransactionEgine.Accounting.ViewModels;
using Eduegate.Framework.Enums;

namespace Eduegate.TransactionEgine.Accounting.Interfaces
{
    public interface ITransactions
    {
        DocumentReferenceTypes ReferenceTypes { get;}
        void Process(TransactionHeadViewModel transaction);
    }
}
