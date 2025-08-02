using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StudentTransferRequestReasons", Schema = "schools")]
    public partial class StudentTransferRequestReason
    {
        public StudentTransferRequestReason()
        {
            StudentTransferRequests = new HashSet<StudentTransferRequest>();
        }

        [Key]
        public byte TransferRequestReasonIID { get; set; }
        [StringLength(500)]
        public string Reason { get; set; }
        public bool? ISActive { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("StudentTransferRequestReasons")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("StudentTransferRequestReasons")]
        public virtual School School { get; set; }
        [InverseProperty("TransferRequestReason")]
        public virtual ICollection<StudentTransferRequest> StudentTransferRequests { get; set; }
    }
}
