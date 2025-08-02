using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.OnlineExam.Models
{
    [Table("OnlineExamResultQuestionMaps", Schema = "exam")]
    public partial class OnlineExamResultQuestionMap
    {
        [Key]
        public long OnlineExamResultQuestionMapIID { get; set; }

        public long? OnlineExamResultID { get; set; }

        public long? QuestionID { get; set; }

        public decimal? Mark { get; set; }

        public string Remarks { get; set; }

        [StringLength(30)]
        public string EntryType { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public virtual OnlineExamResult OnlineExamResult { get; set; }

        public virtual Question Question { get; set; }
    }
}