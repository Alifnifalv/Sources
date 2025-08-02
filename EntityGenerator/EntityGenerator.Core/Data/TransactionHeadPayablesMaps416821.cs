using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("TransactionHeadPayablesMaps416821", Schema = "inventory")]
    public partial class TransactionHeadPayablesMaps416821
    {
        public long TransactionHeadPayablesMapIID { get; set; }
        public long? PayableID { get; set; }
        public long? HeadID { get; set; }
    }
}
