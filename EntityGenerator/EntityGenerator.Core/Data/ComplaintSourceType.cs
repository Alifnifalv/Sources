using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ComplaintSourceTypes", Schema = "schools")]
    public partial class ComplaintSourceType
    {
        [Key]
        public byte ComplaintSourceTypeID { get; set; }
        [StringLength(50)]
        public string SourceDescription { get; set; }
    }
}
