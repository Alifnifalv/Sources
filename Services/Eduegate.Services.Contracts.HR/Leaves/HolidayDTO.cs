using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.HR.Leaves
{
    [DataContract]
    public class HolidayDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
         [DataMember]
        public long  HolidayIID { get; set; }
        [DataMember]
        public long?  HolidayListID { get; set; }
        [DataMember]
        public System.DateTime?  HolidayDate { get; set; }
        [DataMember]
        public string  Description { get; set; }
        [DataMember]
        public int?  CreatedBy { get; set; }
        [DataMember]
        public int?  UpdatedBy { get; set; }
        [DataMember]
        public System.DateTime?  CreatedDate { get; set; }
        [DataMember]
        public System.DateTime?  UpdatedDate { get; set; }
        [DataMember]
        public byte[]  TimeStamps { get; set; }
     
    }
}



