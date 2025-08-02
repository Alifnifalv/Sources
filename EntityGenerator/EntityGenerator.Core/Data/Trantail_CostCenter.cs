using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Trantail_CostCenter", Schema = "account")]
    public partial class Trantail_CostCenter
    {
        public int TL_CostID { get; set; }
        [Key]
        public int Ref_TH_ID { get; set; }
        [Key]
        public int Ref_SlNo { get; set; }
        public int? AccountID { get; set; }
        public int? CostCenterID { get; set; }
        [Key]
        public int SlNo { get; set; }
        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }
        public int? Ref_ID { get; set; }
    }
}
