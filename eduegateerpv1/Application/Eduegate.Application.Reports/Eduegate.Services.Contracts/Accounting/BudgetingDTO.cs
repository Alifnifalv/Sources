using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Accounting
{
    [DataContract]
    public class BudgetingDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public BudgetingDTO()
        {
            BudgetingAccountGroups = new List<BudgetingAccountGroupsDTO>();
        }

        [DataMember]
        public List<KeyValueDTO> AccountGroups { get; set; }

        [DataMember]
        public List<BudgetingAccountGroupsDTO> BudgetingAccountGroups { get; set; }
    }
}
