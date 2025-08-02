using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class TimeTableLogDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long TimeTableLogID { get; set; }

        [DataMember]
        public long? TimeTableAllocationID { get; set; }

        [DataMember]
        public int? TimeTableID { get; set; }

        [DataMember]
        public int? WeekDayID { get; set; }

        [DataMember]
        public int? ClassTimingID { get; set; }

        [DataMember]
        public int? SubjectID { get; set; }

        [DataMember]
        public long? StaffID { get; set; }

        [DataMember]
        public byte[] TimeStamps { get; set; }

        [DataMember]
        public string Notes { get; set; }

        [DataMember]
        public DateTime? AllocatedDate { get; set; }

        [DataMember]
        public int? SectionID { get; set; }

        [DataMember]
        public int? ClassId { get; set; }

        [DataMember]
        public KeyValueDTO Class { get; set; }


        [DataMember]
        public KeyValueDTO Section { get; set; }

        [DataMember]
        public KeyValueDTO Subject { get; set; }

        [DataMember]
        public KeyValueDTO TimeTable { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }


    }


}
