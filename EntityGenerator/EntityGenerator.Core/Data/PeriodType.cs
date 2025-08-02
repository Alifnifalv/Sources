using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PeriodTypes", Schema = "account")]
    public partial class PeriodType
    {
        [Key]
        public int PeriodTypeID { get; set; }
        [Column("PeriodType")]
        public int? PeriodType1 { get; set; }
        [StringLength(100)]
        public string PeriodTypeName { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public bool? IsActive { get; set; }
    }
}
