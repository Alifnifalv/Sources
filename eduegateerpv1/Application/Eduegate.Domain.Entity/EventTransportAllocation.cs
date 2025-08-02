namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.EventTransportAllocation")]
    public partial class EventTransportAllocation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EventTransportAllocation()
        {
            EventTransportAllocationMaps = new HashSet<EventTransportAllocationMap>();
        }

        [Key]
        public long EventTransportAllocationIID { get; set; }

        public DateTime? EventDate { get; set; }

        public int? RouteID { get; set; }

        public long? VehicleID { get; set; }

        public long? DriverID { get; set; }

        public long? AttendarID { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public long? EventID { get; set; }

        public bool? IsPickUp { get; set; }

        public virtual Vehicle Vehicle { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Employee Employee1 { get; set; }

        public virtual SchoolEvent SchoolEvent { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventTransportAllocationMap> EventTransportAllocationMaps { get; set; }

        public virtual Routes1 Routes1 { get; set; }
    }
}
