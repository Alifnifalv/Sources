using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
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
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("AssignmentID")]
        [InverseProperty("AssignmentDocuments")]
        public virtual Assignment Assignment { get; set; }
        [ForeignKey("StudentID")]
        [InverseProperty("AssignmentDocuments")]
        public virtual Student Student { get; set; }
    }
}
