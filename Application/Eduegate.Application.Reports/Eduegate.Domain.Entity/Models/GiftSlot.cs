using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class GiftSlot
    {
        public byte SlotID { get; set; }
        public string SlotName { get; set; }
        public Nullable<short> Amount { get; set; }
        public Nullable<bool> Active { get; set; }
        public string SlotNameAr { get; set; }
    }
}
