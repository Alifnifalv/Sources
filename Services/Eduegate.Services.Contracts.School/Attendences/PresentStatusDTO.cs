using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Attendences
{
    [DataContract]
    public class PresentStatusDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public byte PresentStatusID { get; set; }

        [DataMember]
        public string StatusDescription { get; set; }

        [DataMember]
        public string StatusTitle { get; set; }
    }
}


