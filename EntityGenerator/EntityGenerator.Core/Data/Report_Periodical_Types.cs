using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("Report_Periodical_Types", Schema = "account")]
    public partial class Report_Periodical_Types
    {
        public int? Type_ID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Type_Name { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Type_Remarks { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Type_Caption1 { get; set; }
        [MaxLength(50)]
        public byte[] Tyep_caption2 { get; set; }
    }
}
