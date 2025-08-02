using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StockAccountMaps", Schema = "account")]
    public partial class StockAccountMap
    {
        [Key]
        public int SAMID { get; set; }
        public int? CompanyID { get; set; }
        public int? FType { get; set; }
        public long? AccountID { get; set; }
        public long? OP_AccountID { get; set; }
        public long? CL_AccountID { get; set; }
    }
}
