using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Enums;
using Eduegate.TransactionEngineCore.ViewModels;

namespace Eduegate.TransactionEngineCore.Interfaces
{
    public interface ITransactions
    {
        DocumentReferenceTypes ReferenceTypes { get;}
        void Process(TransactionHeadViewModel transaction);
    }
}
