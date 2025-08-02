namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("EventAudienceTypes", Schema = "collaboration")]
    public partial class EventAudienceType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EventAudienceType()
        {
            EventAudienceMaps = new HashSet<EventAudienceMap>();
        }
        [Key]
        public byte EventAudienceTypeID { get; set; }

        [StringLength(100)]
        public string AudienceTypeName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventAudienceMap> EventAudienceMaps { get; set; }
    }
}
