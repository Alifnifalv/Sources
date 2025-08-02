using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("VechiclePickupPoints", Schema = "schools")]
    public partial class VechiclePickupPoint
    {
        public long? VechiclePickupPointIID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string PointCode { get; set; }
        [StringLength(100)]
        public string PickupPointName { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
