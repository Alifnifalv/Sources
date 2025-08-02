using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("TransactionHeadPayablesMaps417576", Schema = "inventory")]
    public partial class TransactionHeadPayablesMaps417576
    {
        public long TransactionHeadPayablesMapIID { get; set; }
        public long? PayableID { get; set; }
        public long? HeadID { get; set; }
    }
}
