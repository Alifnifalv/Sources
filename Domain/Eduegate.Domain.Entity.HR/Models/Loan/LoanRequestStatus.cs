using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.HR.Loan
{
    [Table("LoanRequestStatuses", Schema = "payroll")]
    public partial class LoanRequestStatus
    {
        public LoanRequestStatus()
        {
           
            LoanRequests = new HashSet<LoanRequest>();
        }

        [Key]
        public byte LoanRequestStatusID { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
       
        [InverseProperty("LoanRequestStatus")]
        public virtual ICollection<LoanRequest> LoanRequests { get; set; }
    }
}
