using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SalesPromotionTypes", Schema = "marketing")]
    public partial class SalesPromotionType
    {
        public SalesPromotionType()
        {
            SalesPromotions = new HashSet<SalesPromotion>();
        }

        [Key]
        public int SalesPromotionTypeID { get; set; }
        [StringLength(50)]
        public string PromotionTypeName { get; set; }

        [InverseProperty("SalesPromotionType")]
        public virtual ICollection<SalesPromotion> SalesPromotions { get; set; }
    }
}
