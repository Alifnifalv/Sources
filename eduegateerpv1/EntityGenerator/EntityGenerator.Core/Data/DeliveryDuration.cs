using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DeliveryDuration", Schema = "inventory")]
    public partial class DeliveryDuration
    {
        [Key]
        public byte DeliveryDurationID { get; set; }
        [StringLength(50)]
        public string DurationName { get; set; }
    }
}
