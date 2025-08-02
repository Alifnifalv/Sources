using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class DowloadFileDTO
    {
        [DataMember]
        public long FileMapID { get; set; }
        [DataMember]
        public string FilePath { get; set; }
    }
}
