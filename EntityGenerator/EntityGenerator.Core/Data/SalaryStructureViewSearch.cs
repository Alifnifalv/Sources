using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class SalaryStructureViewSearch
    {
        public long SalaryStructureID { get; set; }
        [StringLength(500)]
        public string StructureName { get; set; }
        [Required]
        [StringLength(8)]
        [Unicode(false)]
        public string IsActive { get; set; }
        public byte? PayrollFrequencyID { get; set; }
        [StringLength(50)]
        public string PayrollFrequency { get; set; }
        public int? TimeSheetSalaryComponentID { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public int? PaymentModeID { get; set; }
        [StringLength(50)]
        public string PaymentName { get; set; }
        public long? AccountID { get; set; }
        [StringLength(50)]
        public string AccountName { get; set; }
    }
}
