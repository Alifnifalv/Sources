using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class May_2024PPS_BankReco_Opening
    {
        [Column(TypeName = "datetime")]
        public DateTime? Date { get; set; }
        [StringLength(255)]
        public string Ref { get; set; }
        [Column("Cheque No")]
        public double? Cheque_No { get; set; }
        [Column("Party's Name")]
        [StringLength(255)]
        public string Party_s_Name { get; set; }
        public double? Amount { get; set; }
        public double? Unmatched { get; set; }
        public double? F7 { get; set; }
        public long? TH_ID { get; set; }
        public long? TL_ID { get; set; }
        public long? Ext_Ref_ID { get; set; }
        public long? AccountID { get; set; }
    }
}
