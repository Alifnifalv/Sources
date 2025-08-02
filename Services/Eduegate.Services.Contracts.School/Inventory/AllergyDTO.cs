using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Inventory
{
    [DataContract]
    public class AllergyDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public int AllergyID { get; set; }


        [DataMember]
        public string AllergyName { get; set; }


        [DataMember]
        public bool? IsActive { get; set; }

        [DataMember]
        public long? StudentID { get; set; }

        [DataMember]
        public string AllergySeverityName { get; set; }

    }
}
