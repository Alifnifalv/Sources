using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Models
{
    [Keyless]
    [Table("ProductClassTypes", Schema = "catalog")]
    public partial class ProductClassType
    {
        public int? ProductClassTypeIID { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string ProductClassTypeName { get; set; }
        public bool? IsOptional { get; set; }
        public bool? IsSecondLanguage { get; set; }
        public bool? IsThirdLanguage { get; set; }
        public bool? IsStream { get; set; }
    }
}

