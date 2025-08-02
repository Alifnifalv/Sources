using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Accounts
{
    [DataContract]
    public class ExpenditureAllocationDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public ExpenditureAllocationDTO()
        {

        }

        [DataMember]
        public long? ExpenditureAllocationIID { get; set; }
        [DataMember]
        public int? CostCenterID { get; set; }

        [DataMember]
        public KeyValueDTO CostCenter { get; set; }

    }
}
