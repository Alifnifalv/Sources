using System;
using System.Runtime.Serialization;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Commons;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class DocumentFileDTO
    {

        [DataMember]
        public long DocumentFileIID { get; set; }
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public string ActualFileName { get; set; }
        [DataMember]
        public Enums.EntityTypes EntityType { get; set; }
        [DataMember]
        public long? ReferenceID { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Tags { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Version { get; set; }

        [DataMember]
        public string DocumentStatusID { get; set; }
        [DataMember]
        public KeyValueDTO OwnerEmployeeID { get; set; }

        [DataMember]
        public Nullable<System.DateTime> ExpiryDate { get; set; }


        [DataMember]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [DataMember]
        public Nullable<System.DateTime> UpdatedDate { get; set; }


        [DataMember]
        public Enums.DocumentFileStatuses DocumentFileStatus { get; set; }

        [DataMember]
        public Enums.DocumentFileTypes? DocumentFileType { get; set; }

        [DataMember]
        public string TimeStamps { get; set; }
    }
}