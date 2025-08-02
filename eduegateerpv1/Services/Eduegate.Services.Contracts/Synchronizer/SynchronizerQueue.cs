using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Synchronizer
{
    [DataContract]
    public class SynchronizerQueue : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long SkuIID { get; set; }
    }
}
