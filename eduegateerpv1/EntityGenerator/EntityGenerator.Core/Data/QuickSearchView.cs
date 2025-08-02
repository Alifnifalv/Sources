using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class QuickSearchView
    {
        public long ID { get; set; }
        [Required]
        [StringLength(12)]
        [Unicode(false)]
        public string SearchType { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string SearchType2 { get; set; }
        [StringLength(767)]
        public string Title { get; set; }
        [StringLength(50)]
        public string SubTitle { get; set; }
    }
}
