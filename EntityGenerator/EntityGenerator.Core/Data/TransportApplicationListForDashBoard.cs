using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class TransportApplicationListForDashBoard
    {
        public long ReferenceIID { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string Date { get; set; }
        [StringLength(555)]
        public string Title { get; set; }
        [StringLength(576)]
        public string SubTitle { get; set; }
    }
}
