using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PromotionTypes", Schema = "cms")]
    public partial class PromotionType
    {
        public PromotionType()
        {
            Promotions = new HashSet<Promotion>();
        }

        [Key]
        public byte PromotionTypeID { get; set; }
        [StringLength(50)]
        public string TypeName { get; set; }

        [InverseProperty("PromotionType")]
        public virtual ICollection<Promotion> Promotions { get; set; }
    }
}
