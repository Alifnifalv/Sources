using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("OnlineExamResultQuestionMaps", Schema = "exam")]
    public partial class OnlineExamResultQuestionMap
    {
        [Key]
        public long OnlineExamResultQuestionMapIID { get; set; }
        public long? OnlineExamResultID { get; set; }
        public long? QuestionID { get; set; }
        [Column(TypeName = "decimal(10, 3)")]
        public decimal? Mark { get; set; }
        public string Remarks { get; set; }
        [StringLength(30)]
        public string EntryType { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("OnlineExamResultID")]
        [InverseProperty("OnlineExamResultQuestionMaps")]
        public virtual OnlineExamResult OnlineExamResult { get; set; }
        [ForeignKey("QuestionID")]
        [InverseProperty("OnlineExamResultQuestionMaps")]
        public virtual Question Question { get; set; }
    }
}
