using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class LocationSearchView
    {
        public long LocationIID { get; set; }
        [StringLength(50)]
        public string LocationCode { get; set; }
        [StringLength(150)]
        public string Description { get; set; }
        public int? CompanyID { get; set; }
    }
}
