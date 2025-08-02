using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DeliveryTypeTimeSlotMaps", Schema = "inventory")]
    public partial class DeliveryTypeTimeSlotMap
    {
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
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? NoOfCutOffOrder { get; set; }
        [StringLength(100)]
        public string SlotName { get; set; }
        public long? BranchID { get; set; }
        public bool? IsActive { get; set; }

        [ForeignKey("DeliveryTypeID")]
        [InverseProperty("DeliveryTypeTimeSlotMaps")]
        public virtual DeliveryType1 DeliveryType { get; set; }
        [InverseProperty("TimeSlot")]
        public virtual ICollection<DeliveryTypeCutOffSlot> DeliveryTypeCutOffSlots { get; set; }
        [InverseProperty("DeliveryTypeTimeSlotMap")]
        public virtual ICollection<DeliveryTypeTimeSlotMapsCulture> DeliveryTypeTimeSlotMapsCultures { get; set; }
    }
}
