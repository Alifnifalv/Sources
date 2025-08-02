using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Students
{
    [DataContract]
    public class HealthEntryStudentMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public HealthEntryStudentMapDTO()
        {
           
        }

        [DataMember]
        public long HealthEntryStudentMapIID { get; set; }

        [DataMember]
        public long? HealthEntryID { get; set; }

        [DataMember]
        public long? StudentID { get; set; }

        [DataMember]
        public string StudentName { get; set; }

        [DataMember]
        public decimal? Height { get; set; }

        [DataMember]
        public decimal? Weight { get; set; }

        [DataMember]
        public decimal? BMS { get; set; }

        [DataMember]
        public string Vision { get; set; }

        [DataMember]
        public string Remarks { get; set; }

    }
}



