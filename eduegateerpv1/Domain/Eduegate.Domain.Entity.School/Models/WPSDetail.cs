using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("WPSDetail", Schema = "payroll")]
    public partial class WPSDetail
    {
        [Key]
        public long WPSIID { get; set; }
        public long? PayerBankDetailIID { get; set; }
        public string SalaryYear { get; set; }
        public int? SalaryMonth { get; set; }
        public decimal? TotalSalaries { get; set; }
        public int? TotalRecords { get; set; }
        public long? ContentID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }

        public virtual SchoolPayerBankDetailMap PayerBankDetailI { get; set; }
    }
}