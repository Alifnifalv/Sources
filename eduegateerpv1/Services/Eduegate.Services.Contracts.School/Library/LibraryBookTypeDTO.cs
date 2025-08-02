using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Library
{
    [DataContract]
    public class LibraryBookTypeDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
         [DataMember]
        public byte  LibraryBookTypeID { get; set; }
        [DataMember]
        public string  BookTypeName { get; set; }
    }
}


