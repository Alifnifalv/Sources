using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.OnlineExam.Models
{
    [Table("OnlineExamResultSubjectMaps", Schema = "exam")]
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

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        [StringLength(250)]
        public string Remarks { get; set; }

        public decimal? TotalMark { get; set; }

        public virtual OnlineExamResult OnlineExamResult { get; set; }

        public virtual Subject Subject { get; set; }
    }
}