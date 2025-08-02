using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Eduegate.Domain.Entity.School.Models
{
    [Table("CircularUserTypes", Schema = "schools")]
    public partial class CircularUserType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CircularUserType()
        {
            CircularUserTypeMaps = new HashSet<CircularUserTypeMap>();
        }
        [Key]
        public byte CircularUserTypeID { get; set; }

        [StringLength(50)]
        public string UserType { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CircularUserTypeMap> CircularUserTypeMaps { get; set; }
    }
}