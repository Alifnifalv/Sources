using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductSearchKeyword
    {
        [Key]
        public long LogID { get; set; }
        public string Keyword { get; set; }
        public byte Preference { get; set; }
    }
}
