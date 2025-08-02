using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.UrlReWriter
{
    [DataContract]
    public class ReWriteUrlTypeDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public UrlType UrlType { get; set; }
        [DataMember]
        public string Url { get; set; }
        [DataMember]
        public int LevelNo { get; set; }
        [DataMember]
        public long IID { get; set; }
    }
}
