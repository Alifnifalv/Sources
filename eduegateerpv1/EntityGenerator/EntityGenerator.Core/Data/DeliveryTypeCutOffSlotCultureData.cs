using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DeliveryTypeCutOffSlotCultureDatas", Schema = "inventory")]
    public partial class DeliveryTypeCutOffSlotCultureData
    {
        [Key]
        public int DeliveryTypeCutOffSlotID { get; set; }
        [Key]
        public byte CultureID { get; set; }
        public string WarningMessage { get; set; }
        public string TooltipMessage { get; set; }

        [ForeignKey("CultureID")]
        [InverseProperty("DeliveryTypeCutOffSlotCultureDatas")]
        public virtual Culture Culture { get; set; }
        [ForeignKey("DeliveryTypeCutOffSlotID")]
        [InverseProperty("DeliveryTypeCutOffSlotCultureDatas")]
        public virtual DeliveryTypeCutOffSlot DeliveryTypeCutOffSlot { get; set; }
    }
}
