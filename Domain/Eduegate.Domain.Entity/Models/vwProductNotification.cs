using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    [Keyless]
    public partial class vwProductNotification
    {
        public string RequestedOn { get; set; }
        public long RefProductID { get; set; }
    }
}
