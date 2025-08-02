using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class KnowHowOption
    {
        public long KnowHowOptionIID { get; set; }
        public string KnowHowOptionText { get; set; }
        public bool IsEditable { get; set; }
    }
}
