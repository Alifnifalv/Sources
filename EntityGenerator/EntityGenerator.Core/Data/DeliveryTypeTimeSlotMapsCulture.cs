using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DeliveryTypeTimeSlotMapsCulture", Schema = "inventory")]
    public partial class DeliveryTypeTimeSlotMapsCulture
    {
        [Key]
        public byte CultureID { get; set; }
        [Key]
        public long DeliveryTypeTimeSlotMapID { get; set; }
        [StringLength(100)]
        public string CutOffDisplayText { get; set; }

        [ForeignKey("CultureID")]
        [InverseProperty("DeliveryTypeTimeSlotMapsCultures")]
        public virtual Culture Culture { get; set; }
        [ForeignKey("DeliveryTypeTimeSlotMapID")]
        [InverseProperty("DeliveryTypeTimeSlotMapsCultures")]
        public virtual DeliveryTypeTimeSlotMap DeliveryTypeTimeSlotMap { get; set; }
    }
}
