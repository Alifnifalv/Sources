using Eduegate.Framework.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.School.Exams
{
    [DataContract]
    public class StudentBehavioralRemarksDTO
    {     
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
        public string Class { get; set; }

        [DataMember]
        public string AcademicYear { get; set; }

        [DataMember]
        public string Section { get; set; }

        [DataMember]
        public string Teacher { get; set; }

        [DataMember]
        public long? StudentID { get; set; }

        [DataMember]
        public string StudentName { get; set; }

        [DataMember]
        public string Remarks{ get; set; }
    }
}
