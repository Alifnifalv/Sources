using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LoanTypes", Schema = "payroll")]
    public partial class LoanType
    {
        public LoanType()
        {
            LoanHeads = new HashSet<LoanHead>();
            LoanRequests = new HashSet<LoanRequest>();
        }

        [Key]
        public byte LoanTypeID { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? DocumentTypeID { get; set; }

        [ForeignKey("DocumentTypeID")]
        [InverseProperty("LoanTypes")]
        public virtual DocumentType DocumentType { get; set; }
        [InverseProperty("LoanType")]
        public virtual ICollection<LoanHead> LoanHeads { get; set; }
        [InverseProperty("LoanType")]
        public virtual ICollection<LoanRequest> LoanRequests { get; set; }
    }
}
