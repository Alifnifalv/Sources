using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Settings
{
    [DataContract]
    public class SequenceDTO: Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public int SequenceID { get; set; }
        [DataMember]
        public string  SequenceType { get; set; }

        [DataMember]
        public string Prefix { get; set; }

        [DataMember]
        public long? LastSequence { get; set; }

    }
}
