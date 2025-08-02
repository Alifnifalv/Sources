using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Translation
    {
        [StringLength(255)]
        public string Key { get; set; }
        [StringLength(255)]
        public string Type { get; set; }
        [StringLength(255)]
        public string English { get; set; }
        [StringLength(255)]
        public string Arabic { get; set; }
        [StringLength(255)]
        public string French { get; set; }
    }
}
