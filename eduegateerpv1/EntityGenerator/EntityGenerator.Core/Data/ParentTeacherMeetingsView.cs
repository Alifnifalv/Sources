using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class ParentTeacherMeetingsView
    {
        public long? StudentID { get; set; }
        public long? EmployeeID { get; set; }
        public byte? SlotMapStatusID { get; set; }
        [StringLength(50)]
        public string SignupName { get; set; }
        [StringLength(100)]
        public string SlotMapStatusName { get; set; }
        public string TeacherRemarks { get; set; }
        public string ParentRemarks { get; set; }
        [StringLength(555)]
        public string Teacher { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string SlotDate { get; set; }
        [StringLength(8)]
        [Unicode(false)]
        public string StartTime { get; set; }
        [StringLength(8)]
        [Unicode(false)]
        public string EndTime { get; set; }
    }
}
