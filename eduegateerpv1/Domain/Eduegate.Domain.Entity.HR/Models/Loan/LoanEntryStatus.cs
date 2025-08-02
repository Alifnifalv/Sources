using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.HR.Loan
{
    [Table("LoanEntryStatuses", Schema = "payroll")]
    public partial class LoanEntryStatus
    {
        public LoanEntryStatus()
        {
            LoanDetails = new HashSet<LoanDetail>();
        }

        [Key]
        public byte LoanEntryStatusID { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [InverseProperty("LoanEntryStatus")]
        public virtual ICollection<LoanDetail> LoanDetails { get; set; }
    }
}
