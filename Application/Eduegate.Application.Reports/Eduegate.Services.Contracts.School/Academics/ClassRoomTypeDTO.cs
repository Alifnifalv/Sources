using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class ClassRoomTypeDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
         [DataMember]
        public byte  ClassRoomTypeID { get; set; }
        [DataMember]
        public string  TypeDescription { get; set; }
        [DataMember]
        public bool?  IsShared { get; set; }
    }
}


