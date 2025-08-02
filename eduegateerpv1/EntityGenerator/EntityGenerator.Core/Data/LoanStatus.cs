using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LoanStatuses", Schema = "payroll")]
    public partial class LoanStatus
    {
        public LoanStatus()
        {
            LoanHeads = new HashSet<LoanHead>();
        }

        [Key]
        public byte LoanStatusID { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [InverseProperty("LoanStatus")]
        public virtual ICollection<LoanHead> LoanHeads { get; set; }
    }
}
