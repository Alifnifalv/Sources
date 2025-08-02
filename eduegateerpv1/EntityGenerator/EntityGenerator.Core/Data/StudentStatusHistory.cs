using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StudentStatusHistory", Schema = "schools")]
    public partial class StudentStatusHistory
    {
        [Key]
        public long SSH_ID { get; set; }
        public long? StudentID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ActiveDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DeActiveDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CampusTransferInDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CampusTransferOutDate { get; set; }
        public int? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public int? StudentStatusID { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [Required]
        public byte[] TimeStamps { get; set; }
    }
}
