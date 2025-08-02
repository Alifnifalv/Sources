namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.DeliveryTypeTimeSlotMaps")]
    public partial class DeliveryTypeTimeSlotMap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DeliveryTypeTimeSlotMap()
        {
            DeliveryTypeCutOffSlots = new HashSet<DeliveryTypeCutOffSlot>();
            DeliveryTypeTimeSlotMapsCultures = new HashSet<DeliveryTypeTimeSlotMapsCulture>();
        }

        [Key]
        public long DeliveryTypeTimeSlotMapIID { get; set; }

        public int? DeliveryTypeID { get; set; }

        public TimeSpan? TimeFrom { get; set; }

        public TimeSpan? TimeTo { get; set; }

        public bool? IsCutOff { get; set; }

        public byte? CutOffDays { get; set; }

        public TimeSpan? CutOffTime { get; set; }

        public byte? CutOffHour { get; set; }

        [StringLength(100)]
        public string CutOffDisplayText { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public int? NoOfCutOffOrder { get; set; }

        [StringLength(100)]
        public string SlotName { get; set; }

        public long? BranchID { get; set; }

        public bool? IsActive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeliveryTypeCutOffSlot> DeliveryTypeCutOffSlots { get; set; }

        public virtual DeliveryTypes1 DeliveryTypes1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeliveryTypeTimeSlotMapsCulture> DeliveryTypeTimeSlotMapsCultures { get; set; }
    }
}
