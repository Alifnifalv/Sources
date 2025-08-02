using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Trantail_Narration", Schema = "account")]
    public partial class Trantail_Narration
    {
        public int TL_Narration_ID { get; set; }
        [Key]
        public int Ref_TH_ID { get; set; }
        [Key]
        public int Ref_SlNo { get; set; }
        [StringLength(2000)]
        [Unicode(false)]
        public string Narration { get; set; }
        [StringLength(2000)]
        [Unicode(false)]
        public string Narration1 { get; set; }
    }
}
