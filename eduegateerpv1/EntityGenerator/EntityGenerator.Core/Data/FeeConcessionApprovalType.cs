using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("FeeConcessionApprovalTypes", Schema = "schools")]
    public partial class FeeConcessionApprovalType
    {
        public FeeConcessionApprovalType()
        {
            StudentFeeConcessions = new HashSet<StudentFeeConcession>();
        }

        [Key]
        public short ConcessionApprovalTypeID { get; set; }
        [StringLength(50)]
        public string TypeName { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [InverseProperty("ConcessionApprovalType")]
        public virtual ICollection<StudentFeeConcession> StudentFeeConcessions { get; set; }
    }
}
