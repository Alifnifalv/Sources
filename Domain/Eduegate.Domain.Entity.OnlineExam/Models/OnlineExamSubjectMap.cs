using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.OnlineExam.Models
{
    [Table("OnlineExamSubjectMaps", Schema = "exam")]
    public partial class OnlineExamSubjectMap
    {
        [Key]
        public long OnlineExamSubjectMapIID { get; set; }

        public long? OnlineExamID { get; set; }

        public int? SubjectID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public virtual OnlineExam OnlineExam { get; set; }

        public virtual Subject Subject { get; set; }
    }
}