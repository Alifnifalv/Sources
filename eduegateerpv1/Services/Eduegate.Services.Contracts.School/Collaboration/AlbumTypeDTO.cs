using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Collaboration
{
    [DataContract]
    public class AlbumTypeDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
         [DataMember]
        public byte  AlbumTypeID { get; set; }
        [DataMember]
        public string  AlbumTypeName { get; set; }
    }
}


