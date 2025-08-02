using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;

namespace Eduegate.Services.Contracts.Mutual
{
    [DataContract]
    public partial class EntitlementMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long EntitlementMapID { get; set; }
        [DataMember]
        public Nullable<long> ReferenceID { get; set; }
        [DataMember]
        public Nullable<bool> IsLocked { get; set; }
        [DataMember]
        public Nullable<decimal> EntitlementAmount { get; set; }
        [DataMember]
        public Nullable<short> EntitlementDays { get; set; }
        [DataMember]
        public Nullable<byte> EntitlementID { get; set; }
        [DataMember]
        public string EntitlementName { get; set; }
    }

    
}
