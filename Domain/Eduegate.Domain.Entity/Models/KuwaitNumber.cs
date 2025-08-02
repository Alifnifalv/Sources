using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class KuwaitNumber
    {
        [Key]
        public byte RangeID { get; set; }
        public Nullable<int> StartRange { get; set; }
        public Nullable<int> EndRange { get; set; }
    }
}
