using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DeliveryTimeSlots", Schema = "inventory")]
    public partial class DeliveryTimeSlot
    {
        public DeliveryTimeSlot()
        {
            DeliveryTimeSlotBranchMaps = new HashSet<DeliveryTimeSlotBranchMap>();
        }

        [Key]
        public int DeliveryTimeSlotsIID { get; set; }
        public TimeSpan? TimeFrom { get; set; }
        public TimeSpan? TimeTo { get; set; }
        public int? NoOfCutOffOrder { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? CutOffHour { get; set; }

        [InverseProperty("DeliveryTimeSlot")]
        public virtual ICollection<DeliveryTimeSlotBranchMap> DeliveryTimeSlotBranchMaps { get; set; }
    }
}
