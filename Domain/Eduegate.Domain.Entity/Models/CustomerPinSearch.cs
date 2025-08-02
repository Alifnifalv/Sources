using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    [Keyless]
    public partial class CustomerPinSearch
    {
        public string Telephone { get; set; }
        public string UserID { get; set; }
        public System.DateTime SearchOn { get; set; }
    }
}
