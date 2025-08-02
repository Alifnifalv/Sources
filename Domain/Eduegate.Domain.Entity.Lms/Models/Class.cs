using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Lms.Models
{
    [Table("Classes", Schema = "schools")]
    public partial class Class
    {
        public Class()
        {
            MeetingRequests = new HashSet<MeetingRequest>();
            Signups = new HashSet<Signup>();
            StudentClasses = new HashSet<Student>();
            StudentPreviousSchoolClassCompleteds = new HashSet<Student>();
        }

        [Key]
        public int ClassID { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(50)]
        public string ClassDescription { get; set; }

        public byte? ShiftID { get; set; }

        public byte? SchoolID { get; set; }

        public int? CostCenterID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        public int? ORDERNO { get; set; }

        public int? AcademicYearID { get; set; }

        public long? ClassGroupID { get; set; }

        public bool? IsVisible { get; set; }

        public bool? IsActive { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual School School { get; set; }

        public virtual ICollection<MeetingRequest> MeetingRequests { get; set; }

        public virtual ICollection<Signup> Signups { get; set; }

        public virtual ICollection<Student> StudentClasses { get; set; }

        public virtual ICollection<Student> StudentPreviousSchoolClassCompleteds { get; set; }
    }
}