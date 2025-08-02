using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Students
{
    [DataContract]
    public class CastDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public byte CastID { get; set; }
        [DataMember]
        public string CastDescription { get; set; }
    }
}
