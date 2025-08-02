namespace Eduegate.Domain.Entity.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ProductAllergyMaps", Schema = "catalog")]
    public partial class ProductAllergyMap
    {
        [Key]
        public long ProductAllergyMapIID { get; set; }

        public long? ProductID { get; set; }

        public int? AllergyID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public virtual Allergy Allergy { get; set; }

        public virtual Product Product { get; set; }
    }
}
