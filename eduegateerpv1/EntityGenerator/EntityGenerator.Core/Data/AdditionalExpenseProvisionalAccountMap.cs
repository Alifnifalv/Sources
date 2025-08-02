using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AdditionalExpenseProvisionalAccountMaps", Schema = "account")]
    public partial class AdditionalExpenseProvisionalAccountMap
    {
        [Key]
        public long AdditionalExpProvAccountMapIID { get; set; }
        public long? AdditionalExpenseID { get; set; }
        public long? AccountID { get; set; }
        public long? ProvisionalAccountID { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public bool? Isdefault { get; set; }
    }
}
