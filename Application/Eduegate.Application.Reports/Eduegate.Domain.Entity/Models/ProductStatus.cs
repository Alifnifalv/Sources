using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductStatus
    {
        [Key]
        public byte ProductStatusID { get; set; }
        public string StatusName { get; set; }
    }
}
