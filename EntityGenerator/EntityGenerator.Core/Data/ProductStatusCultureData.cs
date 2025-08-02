using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductStatusCultureDatas", Schema = "catalog")]
    public partial class ProductStatusCultureData
    {
        [Key]
        public byte CoultureID { get; set; }
        [Key]
        public byte ProductStatusID { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }
    }
}
