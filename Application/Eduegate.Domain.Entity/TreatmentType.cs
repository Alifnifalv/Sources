namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("saloon.TreatmentTypes")]
    public partial class TreatmentType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TreatmentType()
        {
            Services = new HashSet<Service>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TreatmentTypeID { get; set; }

        [StringLength(200)]
        public string TreatmentName { get; set; }

        public int TreatmentGroupID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Service> Services { get; set; }

        public virtual TreatmentGroup TreatmentGroup { get; set; }
    }
}
