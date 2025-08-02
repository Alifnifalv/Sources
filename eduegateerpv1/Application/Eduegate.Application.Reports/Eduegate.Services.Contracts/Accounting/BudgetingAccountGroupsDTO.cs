using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Accounting
{
    [DataContract]   
     public  class BudgetingAccountGroupsDTO 
    {
        public BudgetingAccountGroupsDTO()
        {
            BudgetingAccounts = new List<BudgetingAccountsDTO>();
        }

        [DataMember]
        public long? AccountGroupID { get; set; }
        [DataMember]
        public string AccountGroup { get; set; }

        [DataMember]
        public List<BudgetingAccountsDTO> BudgetingAccounts { get; set; }
    }
}
