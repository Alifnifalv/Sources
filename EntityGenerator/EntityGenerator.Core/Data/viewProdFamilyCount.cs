using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class viewProdFamilyCount
    {
        [StringLength(100)]
        public string x_val { get; set; }
        public int? y_val { get; set; }
    }
}
