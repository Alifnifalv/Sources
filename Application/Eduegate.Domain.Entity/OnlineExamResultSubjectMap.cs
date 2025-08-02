namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("exam.OnlineExamResultSubjectMaps")]
    public partial class OnlineExamResultSubjectMap
    {
        [Key]
        public long OnlineExamResultSubjectMapIID { get; set; }

        public long? OnlineExamResultsID { get; set; }

        public int? SubjectID { get; set; }

        public decimal? Mark { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        [StringLength(250)]
        public string Remarks { get; set; }

        public decimal? TotalMark { get; set; }

        public virtual OnlineExamResult OnlineExamResult { get; set; }

        public virtual Subject Subject { get; set; }
    }
}
