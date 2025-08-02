using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("DeliveryTypeTimeSlotMapsCulture", Schema = "inventory")]
    public partial class DeliveryTypeTimeSlotMapsCulture
    {
       
        public byte CultureID { get; set; }       
        public long DeliveryTypeTimeSlotMapID { get; set; }
        public string CutOffDisplayText { get; set; }
        public virtual DeliveryTypeTimeSlotMap DeliveryTypeTimeSlotMap { get; set; }
        public virtual Culture Culture { get; set; }
    }
}
