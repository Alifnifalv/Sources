using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("AssignmentDocument", Schema = "schools")]
    public partial class AssignmentDocument
    {
        [Key]
        public long AssignmentDocumentIID { get; set; }

        public long? AssignmentID { get; set; }

        public long? StudentID { get; set; }

        public string AssignmentDocumentName { get; set; }

        public string DocumentPath { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual Assignment Assignment { get; set; }

        public virtual Student Student { get; set; }
    }
}