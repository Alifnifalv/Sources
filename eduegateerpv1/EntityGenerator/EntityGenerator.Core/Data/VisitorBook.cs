using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("VisitorBooks", Schema = "schools")]
    public partial class VisitorBook
    {
        [Key]
        public long VisitorBookIID { get; set; }
        public byte? VisitingPurposeID { get; set; }
        [StringLength(50)]
        public string VisitorName { get; set; }
        [StringLength(50)]
        public string PhoneNumber { get; set; }
        [StringLength(50)]
        public string IDCard { get; set; }
        [StringLength(500)]
        public string NumberOfPerson { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Date { get; set; }
        public TimeSpan? InTime { get; set; }
        public TimeSpan? OutTime { get; set; }
        [StringLength(2000)]
        public string Note { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("VisitingPurposeID")]
        [InverseProperty("VisitorBooks")]
        public virtual VisitingPurpos VisitingPurpose { get; set; }
    }
}
