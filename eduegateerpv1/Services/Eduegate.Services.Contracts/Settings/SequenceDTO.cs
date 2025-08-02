using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Settings
{
    [DataContract]
    public class SequenceDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public int SquenceID { get; set; }
        [DataMember]
        public string SequenceType { get; set; }
        [DataMember]
        public string Prefix { get; set; }
        [DataMember]
        public string Format { get; set; }
        [DataMember]
        public long? LastSequence { get; set; }
        [DataMember]
        public bool? IsAuto { get; set; }
        [DataMember]
        public int? ZeroPadding { get; set; }
    }
}


