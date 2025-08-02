using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class BaseMasterDTO
    {
        [DataMember]
        public Nullable<int> CreatedBy { get; set; }

        [DataMember]
        public Nullable<int> UpdatedBy { get; set; }

        [DataMember]
        public Nullable<System.DateTime> CreatedDate { get; set; }

        [DataMember]
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        [DataMember]
        public string TimeStamps { get; set; }
    }
}
