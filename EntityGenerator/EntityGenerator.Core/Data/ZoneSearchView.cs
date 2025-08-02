using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class ZoneSearchView
    {
        public short ZoneID { get; set; }
        [StringLength(50)]
        public string ZoneName { get; set; }
        public int? CompanyID { get; set; }
    }
}
