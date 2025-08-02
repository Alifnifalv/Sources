using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("TimeSlotMaster", Schema = "cms")]
    public partial class TimeSlotMaster
    {
        [Key]
        public int TimeSlotID { get; set; }
        public Nullable<int> SupplierID { get; set; }
        public Nullable<byte> Cutoff { get; set; }
        public Nullable<bool> Monday { get; set; }
        public Nullable<bool> Tuesday { get; set; }
        public Nullable<bool> Wednesday { get; set; }
        public Nullable<bool> Thursday { get; set; }
        public Nullable<bool> Friday { get; set; }
        public Nullable<bool> Saturday { get; set; }
        public Nullable<bool> Sunday { get; set; }
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public Nullable<System.TimeSpan> EndTime { get; set; }
        public virtual SupplierMaster SupplierMaster { get; set; }
    }
}
