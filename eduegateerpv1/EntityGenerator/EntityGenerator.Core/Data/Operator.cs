using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Operators", Schema = "mutual")]
    public partial class Operator
    {
        [Key]
        public int OperatorID { get; set; }
        [Column("Operator")]
        [StringLength(250)]
        public string Operator1 { get; set; }
        [StringLength(20)]
        public string Code { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
    }
}
