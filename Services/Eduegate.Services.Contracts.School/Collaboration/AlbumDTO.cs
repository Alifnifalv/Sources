using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Collaboration
{
    [DataContract]
    public class AlbumDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
         [DataMember]
        public int  AlbumID { get; set; }
        [DataMember]
        public string  AlbumName { get; set; }
        [DataMember]
        public byte?  AlbumTypeID { get; set; }
    }
}


