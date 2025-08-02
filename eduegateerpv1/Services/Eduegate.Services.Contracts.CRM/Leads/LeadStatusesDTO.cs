using Eduegate.Framework.Contracts.Common;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Leads
{
    [DataContract]
    public class LeadStatusesDTO : BaseMasterDTO
    {
        [DataMember]
        public byte LeadStatusID { get; set; }

        [DataMember]
        [StringLength(50)]
        public string LeadStatusName { get; set; }

    }
}


