using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("PaymentEntries", Schema = "payroll")]
    public partial class PaymentEntry
    {
        public long PayrollGenerationIID { get; set; }
        public int? CompanyID { get; set; }
        public int? BranchID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PostingDate { get; set; }
        public int? DepartmentID { get; set; }
        public int? PayrollFrequencyID { get; set; }
        public int? DesignationID { get; set; }
        public bool? IsBasedOnTimeSheet { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartPeriod { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndPeriod { get; set; }
        public int? CostCenter { get; set; }
        public long? ProjectID { get; set; }
        public long? PaymentAccountID { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
    }
}
