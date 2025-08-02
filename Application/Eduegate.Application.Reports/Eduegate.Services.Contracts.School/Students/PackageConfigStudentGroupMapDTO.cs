using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
namespace Eduegate.Services.Contracts.School.Students
{
    [DataContract]
    public class PackageConfigStudentGroupMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long PackageConfigStudentGroupMapIID { get; set; }
        [DataMember]
        public int? StudentGroupID { get; set; }
        [DataMember]
        public int? CreatedBy { get; set; }
        [DataMember]
        public int? UpdatedBy { get; set; }
        [DataMember]
        public DateTime? CreatedDate { get; set; }
        [DataMember]
        public DateTime? UpdatedDate { get; set; }

        [DataMember]
        public byte[] TimeStamps { get; set; }
        [DataMember]
        public long? PackageConfigID { get; set; }
        [DataMember]      
        public KeyValueDTO  StudentGroup { get; set; }
    }
}
