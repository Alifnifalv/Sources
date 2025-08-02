using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Framework
{
    [DataContract]
    public class ScreenLookupDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public bool? IsOnInit { get; set; }
        [DataMember]
        public string LookUpName { get; set; }
        [DataMember]
        public string Url { get; set; }
        [DataMember]
        public string CallBack { get; set; }
    }
}
