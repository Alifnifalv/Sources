using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Account_Tuition_Fee_Jun_2023_20230723
    {
        public long Ext_Ref_ID { get; set; }
        public int SL_AccountID { get; set; }
        public int? FiscalYear_ID { get; set; }
        [Column(TypeName = "date")]
        public DateTime? TranDate { get; set; }
        public int? DocumentTypeID { get; set; }
        [Column(TypeName = "money")]
        public decimal? Op_Balance { get; set; }
        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }
        public long StudentID { get; set; }
    }
}
