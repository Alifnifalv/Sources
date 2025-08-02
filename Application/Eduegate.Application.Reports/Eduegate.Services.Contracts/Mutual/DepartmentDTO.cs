using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Mutual
{
    [DataContract]
    public class DepartmentDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long DepartmentID { get; set; }
        [DataMember]
        public string DepartmentName { get; set; }
        [DataMember]
        public byte StatusID { get; set; } 
        [DataMember]
        public Nullable<int> CompanyID { get; set; }
    }
}
