using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Fees
{
   public class BudgetDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public int BudgetID { get; set; }

        [DataMember]
        public string BudgetCode { get; set; }

        [DataMember]
        public string BudgetName { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }
    }
}
