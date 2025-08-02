using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Academics
{
    [DataContract]
    public class TimeTableAllocationDTO : BaseMasterDTO
    {
        public TimeTableAllocationDTO()
        {
            StaffIDList = new List<long>();
            AllocationIIDList = new List<long>();
            WeekDay = new KeyValueDTO();
            ClassTiming = new KeyValueDTO();
        }

        [DataMember]
        public long TimeTableAllocationIID { get; set; }

        [DataMember]
        public int? TimeTableID { get; set; }

        [DataMember]
        public int? WeekDayID { get; set; }

        [DataMember]
        public int? ClassTimingID { get; set; }

        [DataMember]
        public int? SubjectID { get; set; }

        [DataMember]
        public List<long> StaffIDList { get; set; }

        [DataMember]
        public List<long> AllocationIIDList { get; set; }

        [DataMember]
        public string StaffIDs { get; set; }

        [DataMember]
        public string AllocationIIDs { get; set; }

        [DataMember]
        public string Notes { get; set; }

        [DataMember]
        public int? SectionID { get; set; }

        [DataMember]
        public int? ClassId { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public KeyValueDTO Employee { get; set; }

        [DataMember]
        public KeyValueDTO Class { get; set; }

        [DataMember]
        public KeyValueDTO ClassTiming { get; set; }

        [DataMember]
        public KeyValueDTO Section { get; set; }

        [DataMember]
        public KeyValueDTO Subject { get; set; }

        [DataMember]
        public KeyValueDTO WeekDay { get; set; }

        [DataMember]
        public KeyValueDTO TimeTable { get; set; }

        [DataMember]
        public bool? IsBreakTime { get; set; }

        [DataMember]
        public int? TimeTableMasterID { get; set; }

        [DataMember]
        public byte? IsGenerate { get; set; }

        [DataMember]
        public DateTime? AllocatedDate { get; set; }

        //ForDashBoard TimeTable
        [DataMember]
        public string ClassSectionName { get; set; }

        [DataMember]
        public string ClassTime { get; set; }

        [DataMember]
        public string SubjectName { get; set; }

    }

    [DataContract]
    public class TimeTableAllocInfoHeaderDTO : BaseMasterDTO
    {
        [DataMember]
        public long TimeTableAllocationIID { get; set; }

        [DataMember]
        public int? TimeTableID { get; set; }

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
        public KeyValueDTO TimeTable { get; set; }

        [DataMember]
        public List<TimeTableAllocInfoDetailDTO> AllocInfoDetails { get; set; }
    }

    [DataContract]
    public class TimeTableAllocInfoDetailDTO : BaseMasterDTO
    {
        [DataMember]
        public long TimeTableAllocationIID { get; set; }

        [DataMember]
        public int? WeekDayID { get; set; }

        [DataMember]
        public int? ClassTimingID { get; set; }

        [DataMember]
        public int? SubjectID { get; set; }

        [DataMember]
        public long? StaffID { get; set; }

        [DataMember]
        public string Notes { get; set; }

        [DataMember]
        public KeyValueDTO Employee { get; set; }

        [DataMember]
        public KeyValueDTO ClassTiming { get; set; }

        [DataMember]
        public KeyValueDTO Subject { get; set; }

        [DataMember]
        public KeyValueDTO WeekDay { get; set; }

        [DataMember]
        public bool IsBreakTime { get; set; }

        [DataMember]
        public DateTime? AllocatedDate { get; set; }

        [DataMember]
        public List<long> StaffIDList { get; set; }

        [DataMember]
        public List<long> AllocationIIDList { get; set; }

        [DataMember]
        public string StaffIDs { get; set; }

        [DataMember]
        public string AllocationIIDs { get; set; }

        [DataMember]
        public string StaffNames { get; set; }
    }
}