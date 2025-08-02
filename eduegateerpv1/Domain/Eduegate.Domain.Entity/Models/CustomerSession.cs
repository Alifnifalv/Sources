using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    [Keyless]
    public partial class CustomerSession
    {
        public System.Guid CustomerSessionGUID { get; set; }
        public int CustomerID { get; set; }
        public System.DateTime LastAccessed { get; set; }
        public bool IsExpired { get; set; }
    }
}
