using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class JobEntryDetailSearchView
    {
        [Required]
        [StringLength(6)]
        [Unicode(false)]
        public string RowCategory { get; set; }
        public byte? joboperationstatusid { get; set; }
        public long JobEntryHeadIID { get; set; }
        public long? ParentJobEntryHeadID { get; set; }
        [StringLength(50)]
        public string JobNumber { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? JobStartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? JobEndDate { get; set; }
        [StringLength(50)]
        public string TransactionTypeName { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string BranchName { get; set; }
        [StringLength(100)]
        public string EmployeeName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TransactionNo { get; set; }
        public long? ShoppingCartID { get; set; }
        [StringLength(50)]
        public string OpeationStatus { get; set; }
        public string PaymentMethod { get; set; }
        [StringLength(50)]
        public string CityName { get; set; }
        public int? CompanyID { get; set; }
    }
}
