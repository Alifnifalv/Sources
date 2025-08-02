using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public partial class StaticContentDataDTO
    {
        [DataMember]
        public long ContentDataIID { get; set; }
        [DataMember]
        public Nullable<int> ContentTypeID { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string ImageFilePath { get; set; }
        [DataMember]
        public string SerializedJsonParameters { get; set; }
        [DataMember]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [DataMember]
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        [DataMember]
        public Nullable<int> CreatedBy { get; set; }
        [DataMember]
        public Nullable<int> UpdatedBy { get; set; }
        //[DataMember]
        //public byte[] TimeStamps { get; set; }
        [DataMember]
        public List<Eduegate.Services.Contracts.Commons.KeyValueParameterDTO> AdditionalParameters { get; set; }
    }
}
