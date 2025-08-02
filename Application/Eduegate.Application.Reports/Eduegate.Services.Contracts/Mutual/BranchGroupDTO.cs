using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Mutual
{
    [DataContract]
    public class BranchGroupDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long BranchGroupIID { get; set; }
        [DataMember]
        public string GroupName { get; set; }
        [DataMember]
        public Nullable<Byte> StatusID { get; set; }
        [DataMember]
        public Nullable<int> CompanyID { get; set;}
    }
}
