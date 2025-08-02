using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Contents
{
    [DataContract]
    public class ContentFileDTO : BaseMasterDTO
    {
        [DataMember]
        public long ContentFileIID { get; set; }

        [DataMember]
        public int? ContentTypeID { get; set; }

        [DataMember]
        public string ContentType { get; set; }

        [DataMember]
        public long? ReferenceID { get; set; }

        [DataMember]
        public string ContentFileName { get; set; }

        [DataMember]
        public byte[] ContentData { get; set; }

        [DataMember]
        public string ContentDataString { get; set; }

        [DataMember]
        public long? BranchID { get; set; }

        [DataMember]
        public string FilePath { get; set; }

        [DataMember]
        public bool? IsCompressed { get; set; }
    }
}