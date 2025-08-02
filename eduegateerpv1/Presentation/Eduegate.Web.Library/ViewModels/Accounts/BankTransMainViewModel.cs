using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.ViewModels.Accounts
{
    public class BankTransHeaderViewModel
    {
        public string IBAN { get; set; }
        
        public string AccountType { get; set; }
   
        public string AccountName { get; set; }
 
        public string CurrentBalance { get; set; }
   
        public string FromDate { get; set; }

        public string ToDate { get; set; }
    }

    public class BankTransHeaders
    {
        public List<BankTransHeaderViewModel> BankTransHeader { get; set; }
    }
    public class BankTransDetailViewModel
    {
        public string PostDate { get; set; }

        public string ChequeNo { get; set; }

        public string PartyRef { get; set; }

        public string ValueDate { get; set; }

        public string Description { get; set; }

        public string DebitAmount { get; set; }

        public string CreditAmount { get; set; }

        public string Balance { get; set; }
    }

    public class BankTransDetails
    {
        public List<BankTransDetailViewModel> BankTransDetail { get; set; }
    }
}
