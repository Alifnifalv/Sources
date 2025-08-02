using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("FeeConcessionTypes", Schema = "schools")]
    public partial class FeeConcessionType
    {
        [Key]
        public short ConcessionTypeID { get; set; }
        [StringLength(50)]
        public string TypeName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ApplicableFrom { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ApplicableTo { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
    }
}
