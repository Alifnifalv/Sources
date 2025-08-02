using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Exams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Jobs
{ 
    public class AvailableJobCriteriaMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public AvailableJobCriteriaMapDTO()
        {
            Qualification = new KeyValueDTO();
        }

        [DataMember]
        public long CriteriaID { get; set; }
        [DataMember]
        public long? JobID { get; set; }
        [DataMember]
        public int? TypeID { get; set; }
        [DataMember]
        public byte? QualificationID { get; set; }
        [DataMember]
        public string FieldOfStudy { get; set; }

        [DataMember]
        public KeyValueDTO Qualification { get; set; }

    }
}
