using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class ScreenMetadasTranslation
    {
        public long Key { get; set; }
        [Required]
        [StringLength(50)]
        public string Type { get; set; }
        [Required]
        [StringLength(50)]
        public string English { get; set; }
        [Required]
        [StringLength(50)]
        public string Arabic { get; set; }
        [Required]
        [StringLength(100)]
        public string French { get; set; }
    }
}
