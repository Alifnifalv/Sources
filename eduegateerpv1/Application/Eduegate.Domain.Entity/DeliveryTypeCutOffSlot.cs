namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.DeliveryTypeCutOffSlots")]
    public partial class DeliveryTypeCutOffSlot
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DeliveryTypeCutOffSlot()
        {
            DeliveryTypeCutOffSlotCultureDatas = new HashSet<DeliveryTypeCutOffSlotCultureData>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DeliveryTypeCutOffSlotID { get; set; }

        public int? DeliveryTypeID { get; set; }

        public byte? OccurrenceTypeID { get; set; }

        public byte? OccuranceDayID { get; set; }

        public DateTime? OccuranceDate { get; set; }

        public long? TimeSlotID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public DateTime? TimeFrom { get; set; }

        public DateTime? TimeTo { get; set; }

        public string WarningMessage { get; set; }

        public string TooltipMessage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeliveryTypeCutOffSlotCultureData> DeliveryTypeCutOffSlotCultureDatas { get; set; }

        public virtual DeliveryTypeTimeSlotMap DeliveryTypeTimeSlotMap { get; set; }

        public virtual DeliveryTypes1 DeliveryTypes1 { get; set; }
    }
}
