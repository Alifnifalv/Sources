using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class BasketView
    {
        public int BasketID { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        [StringLength(20)]
        public string BasketCode { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string BarCode { get; set; }
        public int? CompanyiD { get; set; }
    }
}
