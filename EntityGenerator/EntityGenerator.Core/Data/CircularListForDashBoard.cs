using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class CircularListForDashBoard
    {
        public long ReferenceIID { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string Date { get; set; }
        [StringLength(500)]
        public string Title { get; set; }
        public string SubTitle { get; set; }
    }
}
