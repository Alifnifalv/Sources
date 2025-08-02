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
    public class PackageConfigStudentMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long PackageConfigStudentMapIID { get; set; }

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
        public long? StudentID { get; set; }
        [DataMember]
        public long? PackageConfigID { get; set; }
        [DataMember]
        public KeyValueDTO Student { get; set; }
    }
}
