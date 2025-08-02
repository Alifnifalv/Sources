using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class LoyaltyPointSlabMaster
    {
        public int SlabID { get; set; }
        public int SlabPoint { get; set; }
        public decimal SlabAmount { get; set; }
        public short SlabValidity { get; set; }
        public Nullable<bool> Active { get; set; }
    }
}
