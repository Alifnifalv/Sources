using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class PhysicalStockVerificationPickView
    {
        public long HeadIID { get; set; }
        public long? BranchID { get; set; }
        public long? EmployeeID { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public byte? TransactionStatusID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransactionDate { get; set; }
        [StringLength(2000)]
        public string PostedComments { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TransactionNo { get; set; }
        [StringLength(555)]
        public string Employee { get; set; }
        public int? NoOfItems { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
    }
}
