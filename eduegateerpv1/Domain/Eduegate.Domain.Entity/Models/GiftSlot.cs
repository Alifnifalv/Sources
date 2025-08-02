using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class GiftSlot
    {
        [Key]
        public byte SlotID { get; set; }
        public string SlotName { get; set; }
        public Nullable<short> Amount { get; set; }
        public Nullable<bool> Active { get; set; }
        public string SlotNameAr { get; set; }
    }
}
