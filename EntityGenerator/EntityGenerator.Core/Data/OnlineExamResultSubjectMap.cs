using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("OnlineExamResultSubjectMaps", Schema = "exam")]
    public partial class OnlineExamResultSubjectMap
    {
        [Key]
        public long OnlineExamResultSubjectMapIID { get; set; }
        public long? OnlineExamResultsID { get; set; }
        public int? SubjectID { get; set; }
        [Column(TypeName = "decimal(10, 3)")]
        public decimal? Mark { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [StringLength(250)]
        public string Remarks { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TotalMark { get; set; }

        [ForeignKey("OnlineExamResultsID")]
        [InverseProperty("OnlineExamResultSubjectMaps")]
        public virtual OnlineExamResult OnlineExamResults { get; set; }
        [ForeignKey("SubjectID")]
        [InverseProperty("OnlineExamResultSubjectMaps")]
        public virtual Subject Subject { get; set; }
    }
}
