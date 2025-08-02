namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("payroll.SalarySlips")]
    public partial class SalarySlip
    {
        [Key]
        public long SalarySlipIID { get; set; }

        public DateTime? SlipDate { get; set; }

        public long? EmployeeID { get; set; }

        public int? SalaryComponentID { get; set; }

        public decimal? Amount { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public string Notes { get; set; }

        public byte? SalarySlipStatusID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public long? ReportContentID { get; set; }

        public bool? IsVerified { get; set; }

        public decimal? NoOfDays { get; set; }

        public decimal? NoOfHours { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual SalaryComponent SalaryComponent { get; set; }

        public virtual SalarySlipStatus SalarySlipStatus { get; set; }
    }
}
