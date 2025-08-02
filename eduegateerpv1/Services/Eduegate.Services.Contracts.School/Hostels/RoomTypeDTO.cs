using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Hostels
{
    [DataContract]
    public class RoomTypeDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
         [DataMember]
        public int  RoomTypeID { get; set; }
        [DataMember]
        public string  Description { get; set; }
    }
}


