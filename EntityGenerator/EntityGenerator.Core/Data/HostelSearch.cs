using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class HostelSearch
    {
        public int HostelID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string HostelName { get; set; }
        public byte? HostelTypeID { get; set; }
        [StringLength(50)]
        public string HostelTypeName { get; set; }
        [StringLength(50)]
        public string Address { get; set; }
        public int? InTake { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
    }
}
