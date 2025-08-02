using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.ValueObjects
{
    public class DeliveryTypeCheck
    {
        [Key]
        public long SKUID { get; set; }
        public long DeliveryCount { get; set; }
    }
}
