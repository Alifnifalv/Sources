namespace  Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("FeeGroups", Schema = "schools")]
    public partial class FeeGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FeeGroup()
        {
            FeeTypes = new HashSet<FeeType>();
            
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FeeGroupID { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public bool? IsTransport { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FeeType> FeeTypes { get; set; }

      
    }
}
