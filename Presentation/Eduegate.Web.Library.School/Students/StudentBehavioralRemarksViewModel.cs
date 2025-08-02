using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.School.Students
{
    public class StudentBehavioralRemarksViewModel
    {
       
        public long RemarksEntryIID { get; set; }

       
        public int? ClassID { get; set; }

       
        public int? SectionID { get; set; }

       
        public long? TeacherID { get; set; }

       
        public byte? SchoolID { get; set; }

       
        public int? AcademicYearID { get; set; }

       
        public int? ExamGroupID { get; set; }

       
        public string ExamGroupName { get; set; }

       
        public string Class { get; set; }

       
        public string AcademicYear { get; set; }

       
        public string Section { get; set; }

       
        public string Teacher { get; set; }

       
        public long? StudentID { get; set; }

       
        public string StudentName { get; set; }

       
        public string Remarks { get; set; }
    }
}
