using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Collaboration
{
    [DataContract]
    public class EventDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
         [DataMember]
        public long  EventIID { get; set; }
        [DataMember]
        public string  EventTitle { get; set; }
        [DataMember]
        public string  Description { get; set; }
        [DataMember]
        public System.DateTime?  StartDate { get; set; }
        [DataMember]
        public System.DateTime?  EndDate { get; set; }
        [DataMember]
        public bool?  IsThisAHoliday { get; set; }
        [DataMember]
        public bool?  IsEnableReminders { get; set; }
    }
}


