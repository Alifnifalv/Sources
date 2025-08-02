using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Circulars
{
    [DataContract]
    public class CircularUserTypeMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public CircularUserTypeMapDTO()
        {
            CircularUserType = new KeyValueDTO();
        }

        [DataMember]
        public long CircularUserTypeMapIID { get; set; }

        [DataMember]
        public long? CircularID { get; set; }

        [DataMember]
        public byte? CircularUserTypeID { get; set; }

        [DataMember]
        public KeyValueDTO CircularUserType { get; set; }

    }
}