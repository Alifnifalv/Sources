using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductManagerEmailLog
    {
        [Key]
        public int ProductManagerEmailLogID { get; set; }
        public System.DateTime LogDate { get; set; }
        public short ProductManagerID { get; set; }
        public short RefUserID { get; set; }
    }
}
