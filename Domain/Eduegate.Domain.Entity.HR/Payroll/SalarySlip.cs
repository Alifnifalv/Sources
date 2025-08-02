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

        public byte? PayrollFrequencyID { get; set; }

        public int? SalaryComponentID { get; set; }

        public decimal? Amount { get; set; }

        public long? CreatedBy { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }
    }
}
