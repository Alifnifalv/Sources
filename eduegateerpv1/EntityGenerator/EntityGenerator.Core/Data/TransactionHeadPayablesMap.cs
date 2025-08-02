using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TransactionHeadPayablesMaps", Schema = "inventory")]
    public partial class TransactionHeadPayablesMap
    {
        [Key]
        public long TransactionHeadPayablesMapIID { get; set; }
        public long? PayableID { get; set; }
        public long? HeadID { get; set; }

        [ForeignKey("HeadID")]
        [InverseProperty("TransactionHeadPayablesMaps")]
        public virtual TransactionHead Head { get; set; }
    }
}
