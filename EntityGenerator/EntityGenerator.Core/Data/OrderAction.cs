using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("OrderActions", Schema = "inventory")]
    public partial class OrderAction
    {
        public int ActionID { get; set; }
        [StringLength(50)]
        public string Action { get; set; }
    }
}
