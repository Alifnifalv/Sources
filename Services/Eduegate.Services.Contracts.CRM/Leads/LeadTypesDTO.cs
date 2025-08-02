using Eduegate.Framework.Contracts.Common;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Leads
{
    [DataContract]
    public class LeadTypesDTO : BaseMasterDTO
    {
        [DataMember]
        public byte LeadTypeID { get; set; }

        [DataMember]
        [StringLength(50)]
        public string LeadTypeName { get; set; }

        [DataMember]        
        public string LeadSequencePrefix { get; set; }

    }
}


