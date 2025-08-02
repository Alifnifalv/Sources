using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class StatusesCultureData
    {
        public byte CultureID { get; set; }
        public byte StatusID { get; set; }
        public string StatusName { get; set; }
        public virtual Culture Culture { get; set; }
        public virtual Status Status { get; set; }
    }
}
