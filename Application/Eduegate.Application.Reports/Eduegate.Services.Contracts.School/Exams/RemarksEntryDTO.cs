using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.School.Exams
{
    [DataContract]
    public class RemarksEntryDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public RemarksEntryDTO()
        {
            StudentsRemarks = new List<RemarksEntryStudentsDTO>();
            Class = new KeyValueDTO();
            Section = new KeyValueDTO();
        }

        [DataMember]
        public long RemarksEntryIID { get; set; }

        [DataMember]
        public int? ClassID { get; set; }

        [DataMember]
        public int? SectionID { get; set; }

        [DataMember]
        public long? TeacherID { get; set; }

        [DataMember]
        public byte? SchoolID { get; set; }

        [DataMember]
        public int? AcademicYearID { get; set; }

        [DataMember]
        public int? ExamGroupID { get; set; }

        [DataMember]
        public string ExamGroupName { get; set; }

        [DataMember]
        public KeyValueDTO Class { get; set; }

        [DataMember]
        public KeyValueDTO Section { get; set; }

        [DataMember]
        public List<RemarksEntryStudentsDTO> StudentsRemarks { get; set; }

    }
}


