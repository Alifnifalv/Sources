using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Allergies", Schema = "schools")]
    public partial class Allergy
    {
        public Allergy()
        {
            AllergyStudentMaps = new HashSet<AllergyStudentMap>();
            ProductAllergyMaps = new HashSet<ProductAllergyMap>();
        }

        [Key]
        public int AllergyID { get; set; }
        [StringLength(50)]
        public string AllergyName { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [InverseProperty("Allergy")]
        public virtual ICollection<AllergyStudentMap> AllergyStudentMaps { get; set; }
        [InverseProperty("Allergy")]
        public virtual ICollection<ProductAllergyMap> ProductAllergyMaps { get; set; }
    }
}
