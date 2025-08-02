using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Framework
{
    [DataContract]
    public class CRUDDataDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long ScreenID { get; set; }
        [DataMember]
        public string Data { get; set; }
        [DataMember]
        public bool IsError { get; set; }
        [DataMember]
        public string ErrorMessage { get; set; }
    }
}