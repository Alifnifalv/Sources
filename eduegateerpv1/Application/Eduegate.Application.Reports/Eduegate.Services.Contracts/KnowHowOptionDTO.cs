using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class KnowHowOptionDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long KnowHowOptionIID { get; set; }
        [DataMember]
        public string KnowHowOptionText { get; set; }
        [DataMember]
        public bool IsEditable { get; set; }
    }
}
