using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductAllergyMaps", Schema = "catalog")]
    public partial class ProductAllergyMap
    {
        [Key]
        public long ProductAllergyMapIID { get; set; }
        public long? ProductID { get; set; }
        public int? AllergyID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("AllergyID")]
        [InverseProperty("ProductAllergyMaps")]
        public virtual Allergy Allergy { get; set; }
        [ForeignKey("ProductID")]
        [InverseProperty("ProductAllergyMaps")]
        public virtual Product Product { get; set; }
    }
}
