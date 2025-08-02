using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    [Keyless]
    public partial class PublicHoliday
    {
        public System.DateTime HolidayDate { get; set; }
    }
}
