using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Contents.Enums
{
    [DataContract(Name = "ContentType")]
    public enum ContentType
    {
        [EnumMember]
        Profile,
        [EnumMember]
        Documents,
        [EnumMember]
        StudentFile,
    }
}
