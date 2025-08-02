using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Accounts.Accounting
{
    public class AccountAllocationDTO : BaseMasterDTO
    {
        public AccountAllocationDTO()
        {
            AllocationDetails = new List<AccountAllocationDetailDTO>();
        }

        public List<AccountAllocationDetailDTO> AllocationDetails { get; set; }
    }
}
