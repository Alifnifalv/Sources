using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.School.Attendences
{
    public class SaveAttendenceInfoViewModel
    {
        public long? StudentID { get; set; }
        public long? StaffID { get; set; }
        public string Reason { get; set; }
        public string AttendenceDateString { get; set; }
        public byte? PresentStatusID { get; set; }
        public int? AttendenceReasonID { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
    }
}
