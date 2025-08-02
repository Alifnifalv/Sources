using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class TaxSearchView
    {
        public int TaxTemplateID { get; set; }
        [StringLength(50)]
        public string TemplateName { get; set; }
        public bool? ISDefault { get; set; }
        [Required]
        [StringLength(9)]
        [Unicode(false)]
        public string Status { get; set; }
        [Required]
        [StringLength(3)]
        [Unicode(false)]
        public string DefaultTax { get; set; }
        [Required]
        [StringLength(9)]
        [Unicode(false)]
        public string TaxExcluded { get; set; }
    }
}
