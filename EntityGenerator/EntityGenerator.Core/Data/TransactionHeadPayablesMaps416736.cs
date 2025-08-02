using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("TransactionHeadPayablesMaps416736", Schema = "inventory")]
    public partial class TransactionHeadPayablesMaps416736
    {
        public long TransactionHeadPayablesMapIID { get; set; }
        public long? PayableID { get; set; }
        public long? HeadID { get; set; }
    }
}
