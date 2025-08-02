using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Library
{
    [DataContract]
    public class LibraryBookStatusDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
         [DataMember]
        public byte  LibraryBookStatusID { get; set; }
        [DataMember]
        public string  Description { get; set; }
    }
}


