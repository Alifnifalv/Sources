using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.SignUp.Models
{
    [Table("Sections", Schema = "schools")]
    public partial class Section
    {
        public Section()
        {
            MeetingRequests = new HashSet<MeetingRequest>();
            Signups = new HashSet<Signup>();
            Students = new HashSet<Student>();
        }

        [Key]
        public int SectionID { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(50)]
        public string SectionName { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        [StringLength(10)]
        public string SectionCode { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual School School { get; set; }

        public virtual ICollection<MeetingRequest> MeetingRequests { get; set; }

        public virtual ICollection<Signup> Signups { get; set; }

        public virtual ICollection<Student> Students { get; set; }

    }
}