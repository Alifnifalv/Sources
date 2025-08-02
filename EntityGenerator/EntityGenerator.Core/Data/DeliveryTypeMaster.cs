using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DeliveryTypeMaster", Schema = "cms")]
    public partial class DeliveryTypeMaster
    {
        [Key]
        public int DeliveryTypeID { get; set; }
        [Required]
        [StringLength(50)]
        public string Description { get; set; }
    }
}
