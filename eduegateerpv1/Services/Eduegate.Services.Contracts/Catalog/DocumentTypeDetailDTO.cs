using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public class DocumentTypeDetailDTO
    {
        [DataMember]
        public long BranchDocumentTypeMapIID { get; set; }

        [DataMember]
        public long DocumentTypeID { get; set; }

        [DataMember]
        public string DocumentName { get; set; }
    }
}