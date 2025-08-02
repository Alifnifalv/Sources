using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("ProductStatus", Schema = "catalog")]
    public partial class ProductStatus
    {
        [Key]
        public byte ProductStatusID { get; set; }
        public string StatusName { get; set; }
    }
}
