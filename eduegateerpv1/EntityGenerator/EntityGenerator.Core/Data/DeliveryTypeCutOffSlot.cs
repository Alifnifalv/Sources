using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DeliveryTypeCutOffSlots", Schema = "inventory")]
    public partial class DeliveryTypeCutOffSlot
    {
        public DeliveryTypeCutOffSlot()
        {
            DeliveryTypeCutOffSlotCultureDatas = new HashSet<DeliveryTypeCutOffSlotCultureData>();
        }

        [Key]
        public int DeliveryTypeCutOffSlotID { get; set; }
        public int? DeliveryTypeID { get; set; }
        public byte? OccurrenceTypeID { get; set; }
        public byte? OccuranceDayID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? OccuranceDate { get; set; }
        public long? TimeSlotID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TimeFrom { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TimeTo { get; set; }
        public string WarningMessage { get; set; }
        public string TooltipMessage { get; set; }

        [ForeignKey("DeliveryTypeID")]
        [InverseProperty("DeliveryTypeCutOffSlots")]
        public virtual DeliveryType1 DeliveryType { get; set; }
        [ForeignKey("TimeSlotID")]
        [InverseProperty("DeliveryTypeCutOffSlots")]
        public virtual DeliveryTypeTimeSlotMap TimeSlot { get; set; }
        [InverseProperty("DeliveryTypeCutOffSlot")]
        public virtual ICollection<DeliveryTypeCutOffSlotCultureData> DeliveryTypeCutOffSlotCultureDatas { get; set; }
    }
}
