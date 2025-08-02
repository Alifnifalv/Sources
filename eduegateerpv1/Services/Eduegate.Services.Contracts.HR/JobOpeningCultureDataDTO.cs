using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.HR
{
    [DataContract]
    public class JobOpeningCultureDataDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public byte CulturID { get; set; }
        [DataMember]
        public long JobIID { get; set; }
        [DataMember]
        public string JobTitle { get; set; }
        [DataMember]
        public string JobDescription { get; set; }
        [DataMember]
        public string JobDetail { get; set; }
    }
}
