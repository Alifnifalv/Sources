using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Students
{
    [DataContract]
   public class StudentGroupMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long StudentGroupMapIID { get; set; }

        [DataMember]
        public long? StudentID { get; set; }

        [DataMember]
        public int? StudentGroupID { get; set; }

        [DataMember]
        public bool? IsActive { get; set; }

        [DataMember]
        public string StudentName { get; set; }

        [DataMember]
        public string StudentGroup { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }
    }
}
