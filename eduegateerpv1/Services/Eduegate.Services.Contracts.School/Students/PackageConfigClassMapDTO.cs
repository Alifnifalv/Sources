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
    public class PackageConfigClassMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long PackageConfigClassMapIID { get; set; }

        [DataMember]
        public int? ClassID { get; set; }

        [DataMember]
        public long? PackageConfigID { get; set; }
        [DataMember]
        public KeyValueDTO  Class { get; set; }
    }
}
