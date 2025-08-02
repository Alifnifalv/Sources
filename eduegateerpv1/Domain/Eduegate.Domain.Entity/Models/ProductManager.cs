using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductManager
    {
        [Key]
        public long ProductManagerUserID { get; set; }
        public string ContactNo { get; set; }
        public string SubOrdinateEmail { get; set; }
    }
}
