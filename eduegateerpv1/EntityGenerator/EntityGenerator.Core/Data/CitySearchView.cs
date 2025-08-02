using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class CitySearchView
    {
        public int CityID { get; set; }
        [StringLength(50)]
        public string CityName { get; set; }
        public bool? IsActive { get; set; }
        public int? CompanyID { get; set; }
    }
}
