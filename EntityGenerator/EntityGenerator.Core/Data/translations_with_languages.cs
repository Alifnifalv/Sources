using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class translations_with_languages
    {
        public int Key { get; set; }
        [Required]
        [StringLength(50)]
        public string Type { get; set; }
        [Required]
        public string English { get; set; }
        [Required]
        public string Arabic { get; set; }
        [Required]
        public string French { get; set; }
    }
}
