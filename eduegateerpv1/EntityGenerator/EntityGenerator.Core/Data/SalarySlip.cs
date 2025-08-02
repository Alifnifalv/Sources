using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SalarySlips", Schema = "payroll")]
    [Index("EmployeeID", "SlipDate", Name = "IDX_SalarySlips_EmployeeIDSlipDate_")]
    [Index("EmployeeID", Name = "IDX_SalarySlips_EmployeeID_")]
    [Index("EmployeeID", Name = "IDX_SalarySlips_EmployeeID_SlipDate__SalaryComponentID")]
    [Index("EmployeeID", Name = "IDX_SalarySlips_EmployeeID_SlipDate__SalaryComponentID__Amount")]
    [Index("SlipDate", "EmployeeID", Name = "IDX_SalarySlips_SlipDate__EmployeeID_")]
    public partial class SalarySlip
    {
        [Key]
        public long SalarySlipIID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? SlipDate { get; set; }
        public long? EmployeeID { get; set; }
        public int? SalaryComponentID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public string Notes { get; set; }
        public byte? SalarySlipStatusID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public long? ReportContentID { get; set; }
        public bool? IsVerified { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal? NoOfDays { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal? NoOfHours { get; set; }

        [ForeignKey("EmployeeID")]
        [InverseProperty("SalarySlips")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("SalaryComponentID")]
        [InverseProperty("SalarySlips")]
        public virtual SalaryComponent SalaryComponent { get; set; }
        [ForeignKey("SalarySlipStatusID")]
        [InverseProperty("SalarySlips")]
        public virtual SalarySlipStatus SalarySlipStatus { get; set; }
    }
}
