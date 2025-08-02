using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class WPSListScreenView
    {
        public long WPSIID { get; set; }
        public string SalaryYear { get; set; }
        [StringLength(15)]
        public string SalaryMonth { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TotalSalaries { get; set; }
        public int? TotalRecords { get; set; }
        public long? ContentID { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
        [StringLength(15)]
        public string PayerBankShortName { get; set; }
        [StringLength(50)]
        public string BankName { get; set; }
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
