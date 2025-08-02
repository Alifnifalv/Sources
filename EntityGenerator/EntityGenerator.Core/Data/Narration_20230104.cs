using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Narration_20230104
    {
        public long TH_ID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string INVOICENO { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string VOUCHERNO { get; set; }
        public int SlNo { get; set; }
        public int? AccountID { get; set; }
        [Unicode(false)]
        public string Narration { get; set; }
    }
}
