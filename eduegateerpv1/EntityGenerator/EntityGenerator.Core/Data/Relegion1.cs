using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Relegions", Schema = "schools")]
    public partial class Relegion1
    {
        [Key]
        public byte RelegionID { get; set; }
        [StringLength(100)]
        public string RelegionName { get; set; }
        [StringLength(20)]
        public string RelegionCode { get; set; }
        public bool? IsActive { get; set; }
    }
}
