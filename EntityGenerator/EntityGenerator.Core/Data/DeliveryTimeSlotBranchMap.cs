using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DeliveryTimeSlotBranchMaps", Schema = "inventory")]
    public partial class DeliveryTimeSlotBranchMap
    {
        [Key]
        public long DeliveryTimeSlotBranchMapID { get; set; }
        public int? DeliveryTimeSlotID { get; set; }
        public long? BranchID { get; set; }
        public int? NoOfCutOffOrder { get; set; }
        public byte? CutOffHour { get; set; }

        [ForeignKey("BranchID")]
        [InverseProperty("DeliveryTimeSlotBranchMaps")]
        public virtual Branch Branch { get; set; }
        [ForeignKey("DeliveryTimeSlotID")]
        [InverseProperty("DeliveryTimeSlotBranchMaps")]
        public virtual DeliveryTimeSlot DeliveryTimeSlot { get; set; }
    }
}
