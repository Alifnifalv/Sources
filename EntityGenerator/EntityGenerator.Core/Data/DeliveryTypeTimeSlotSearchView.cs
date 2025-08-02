using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class DeliveryTypeTimeSlotSearchView
    {
        public long DeliveryTypeTimeSlotMapIID { get; set; }
        public int? DeliveryTypeID { get; set; }
        public long? BranchID { get; set; }
        public TimeSpan? TimeFrom { get; set; }
        public TimeSpan? TimeTo { get; set; }
        public int? NoOfCutOffOrder { get; set; }
        public bool? IsCutOff { get; set; }
        public byte? CutOffDays { get; set; }
        public TimeSpan? CutOffTime { get; set; }
        public byte? CutOffHour { get; set; }
        [StringLength(100)]
        public string CutOffDisplayText { get; set; }
        [StringLength(100)]
        public string SlotName { get; set; }
        [StringLength(100)]
        public string DeliveryTypeName { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string BranchName { get; set; }
        [Required]
        [StringLength(9)]
        [Unicode(false)]
        public string Status { get; set; }
    }
}
