using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Enums.Accounting;

namespace Eduegate.Services.Contracts.Accounting
{
    [DataContract]
    public class CustomerAccountMapsDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long CustomerAccountMapIID { get; set; }
        [DataMember]
        public long CustomerID { get; set; }
        [DataMember]
        public Nullable<long> AccountID { get; set; }
       
    }
}
