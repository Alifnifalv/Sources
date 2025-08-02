using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("RatingScales", Schema = "schools")]
    public partial class RatingScale
    {
        [Key]
        public byte RatingScaleID { get; set; }
        [StringLength(50)]
        public string RatingScaleName { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Rate { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
    }
}
