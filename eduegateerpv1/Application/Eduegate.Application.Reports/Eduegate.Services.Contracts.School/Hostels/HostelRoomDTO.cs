using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Hostel
{
    [DataContract]
    public class HostelRoomDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
         [DataMember]
        public long  HostelRoomIID { get; set; }
        [DataMember]
        public string  RoomNumber { get; set; }
        [DataMember]
        public int?  HostelID { get; set; }
        [DataMember]
        public int?  RoomTypeID { get; set; }
        [DataMember]
        public int?  NumberOfBed { get; set; }
        [DataMember]
        public decimal?  CostPerBed { get; set; }
        [DataMember]
        public string  Description { get; set; }
    }
}


