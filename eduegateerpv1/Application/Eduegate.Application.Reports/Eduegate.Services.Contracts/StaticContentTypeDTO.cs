using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Services.Contracts.Eduegates;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public partial class StaticContentTypeDTO
    {
        [DataMember]
        public int ContentTypeID { get; set; }
        [DataMember]
        public string ContentTypeName { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string ContentTemplateFilePath { get; set; }
        [DataMember]
        public Nullable<int> CreatedBy { get; set; }
        [DataMember]
        public Nullable<int> UpdatedBy { get; set; }
        [DataMember]
        public string CreatedDate { get; set; }
        [DataMember]
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        [DataMember]
        public string TimeStamps { get; set; }

        [DataMember]
        public List<StaticContentDataDTO> StaticContentDTOs { get; set; }
    }
}
