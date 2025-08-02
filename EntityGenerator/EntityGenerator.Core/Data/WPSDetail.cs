using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("WPSDetail", Schema = "payroll")]
    public partial class WPSDetail
    {
        [Key]
        public long WPSIID { get; set; }
        public long? PayerBankDetailIID { get; set; }
        public string SalaryYear { get; set; }
        public int? SalaryMonth { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TotalSalaries { get; set; }
        public int? TotalRecords { get; set; }
        public long? ContentID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("PayerBankDetailIID")]
        [InverseProperty("WPSDetails")]
        public virtual SchoolPayerBankDetailMap PayerBankDetailI { get; set; }
    }
}
