using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class ClassTimingDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {

        [DataMember]
        public int ClassTimingID { get; set; }

        [DataMember]
        public int? ClassTimingSetID { get; set; }

        [DataMember]
        [StringLength(50)]
        public string TimingDescription { get; set; }

        [DataMember]
        public TimeSpan? StartTime { get; set; }

        [DataMember]
        public TimeSpan? EndTime { get; set; }

        [DataMember]
        public bool? IsBreakTime { get; set; }


        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public byte? BreakTypeID { get; set; }
    }
}