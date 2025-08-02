using Eduegate.Frameworks.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Metadata
{
    [DataContract]
    public class ScreenShortCutDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public ScreenShortCutDTO()
        {
        }

        [DataMember]
        public long ScreenShortCutID { get; set; }
        [DataMember]
        public long ScreenID { get; set; }
        [DataMember]
        public string KeyCode { get; set; }
        [DataMember]
        public string ShortCutKey { get; set; }
        [DataMember]
        public string Action { get; set; }
    }
}
