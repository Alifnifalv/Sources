using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Mutual
{
    [DataContract]
    public class BranchCultureDataDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public byte CultureID { get; set; }
        [DataMember]
        public long BranchID { get; set; }
        [DataMember]
        public string BranchName { get; set; }
    }
}
