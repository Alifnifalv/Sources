using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SalarySlipStatuses", Schema = "payroll")]
    public partial class SalarySlipStatus
    {
        public SalarySlipStatus()
        {
            SalarySlips = new HashSet<SalarySlip>();
        }

        [Key]
        public byte SalarySlipStatusID { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }

        [InverseProperty("SalarySlipStatus")]
        public virtual ICollection<SalarySlip> SalarySlips { get; set; }
    }
}
